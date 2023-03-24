using Microsoft.Xna.Framework;
using StarsAbove.NPCs.Dioskouroi;
using StarsAbove.NPCs.Nalhaun;
using StarsAbove.NPCs.Tsukiyomi;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Dioskouroi
{
    public class CastorIgnitionBolt : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Blazing Blast");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 50;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}

		public override void SetDefaults() {
			Projectile.width = 22;               //The width of projectile hitbox
			Projectile.height = 22;              //The height of projectile hitbox
			Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = true;         //Can the projectile deal damage to the player?
			//projectile.minion = true;           //Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 99;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 680;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}
		public override bool PreDraw(ref Color lightColor)
		{
			default(Effects.OrangeTrail).Draw(Projectile);

			return true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity) {
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 5 times
			
			return false;
		}
		float spawnProgress;
		bool firstSpawn = true;
		double deg;
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			//Orbit Nalhaun. ai[0] is the max orbit distance, ai[1] is the rotation starting position

			if (firstSpawn)
			{
				
				//Projectile.ai[1] = 90;

				firstSpawn = false;
			}
			if (!NPC.AnyNPCs(ModContent.NPCType<CastorBoss>()))
			{

				Projectile.Kill();
			}
			spawnProgress += 0.005f;
			spawnProgress = Math.Clamp(spawnProgress, 0, 1f);
			if (spawnProgress >= 1f)
            {
				Projectile.Kill();
			}
			if (Projectile.alpha > 250)
			{
				Projectile.Kill();
			}
			
			deg = Projectile.ai[1];
			Projectile.ai[0]--;
			Projectile.ai[1] += 1f;

			//deg = 10;

			double rad = deg * (Math.PI / 180);
			double dist = MathHelper.Lerp(1, 1200, EaseHelper.InOutQuad(spawnProgress));

			SearchForTargets(out bool foundTarget, out Vector2 targetCenter);

			if (Projectile.ai[0] == 60)
			{
				if (foundTarget)
				{

					Vector2 position = Projectile.Center;

					float launchSpeed = 12f;
					Vector2 direction = Vector2.Normalize(targetCenter - Projectile.Center);
					Projectile.velocity = direction * launchSpeed;
				}

			}
			else
			{
				if (Projectile.ai[0] > 60)
				{
					for (int i = 0; i < Main.maxNPCs; i++)
					{
						NPC other = Main.npc[i];

						if (other.active && (other.type == ModContent.NPCType<CastorBoss>()))

						{
							Projectile.position.X = other.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
							Projectile.position.Y = other.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
							Projectile.rotation = Vector2.Normalize(other.Center - Projectile.Center).ToRotation() + MathHelper.ToRadians(0f);
							return;
						}
					}
				}
			}
			
		}
		private void SearchForTargets(out bool foundTarget, out Vector2 targetCenter)
		{


			targetCenter = Projectile.position;
			foundTarget = false;



			if (!foundTarget)
			{
				// This code is required either way, used for finding a target
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];

					if (npc.active && npc.type == ModContent.NPCType<CastorBoss>())//Change later
					{
						foundTarget = true;
						targetCenter = Main.player[npc.target].Center;
					}
				}
			}

			// friendly needs to be set to true so the minion can deal contact damage
			// friendly needs to be set to false so it doesn't damage things like target dummies while idling
			// Both things depend on if it has a target or not, so it's just one assignment here
			// You don't need this assignment if your minion is shooting things instead of dealing contact damage
			//Projectile.friendly = foundTarget;
		}
		public override void Kill(int timeLeft)
		{
			for (int d = 0; d < 12; d++)
			{
				Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default(Color), 1.5f);
			}


		}
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (target.type == ModContent.NPCType<NPCs.Dioskouroi.CastorBoss>())
			{
				damage = 0;
			}
			if (target.type == ModContent.NPCType<NPCs.Dioskouroi.PolluxBoss>())
			{
				damage *= 3;
				crit = true;
			}

		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (target.GetModPlayer<BossPlayer>().temperatureGaugeHot > 0)
			{
				target.GetModPlayer<BossPlayer>().temperatureGaugeHot += 10;

			}

			target.AddBuff(BuffID.OnFire, 60);
			base.OnHitPlayer(target, damage, crit);
		}

	}
}
