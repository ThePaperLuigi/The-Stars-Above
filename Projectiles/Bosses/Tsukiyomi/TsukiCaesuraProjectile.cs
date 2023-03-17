using Microsoft.Xna.Framework;
using StarsAbove.NPCs.Nalhaun;
using StarsAbove.NPCs.Tsukiyomi;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Tsukiyomi
{
    public class TsukiCaesuraProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Caesura of Despair");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 50;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}

		public override void SetDefaults() {
			Projectile.width = 68;               //The width of projectile hitbox
			Projectile.height = 68;              //The height of projectile hitbox
			Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			//projectile.minion = true;           //Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 99;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 680;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			//AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}
		public override bool PreDraw(ref Color lightColor)
		{
			default(Effects.PinkTrail).Draw(Projectile);

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

			//Orbit. ai[1] is the rotation starting position

			if (firstSpawn)
			{
				
				//Projectile.ai[1] = 90;

				firstSpawn = false;
			}
			if (!NPC.AnyNPCs(ModContent.NPCType<TsukiyomiBoss>()) && !NPC.AnyNPCs(ModContent.NPCType<TsukiyomiBoss>()))
			{

				Projectile.Kill();
			}
			if (spawnProgress < 0.8)
			{
				Projectile.hostile = false;
				spawnProgress += 0.03f;

			}
			else
			{
				
				spawnProgress += 0.01f;
				//Projectile.alpha += 35;
			}
			spawnProgress = Math.Clamp(spawnProgress, 0, 1f);
			if (spawnProgress >= 1f)
            {
				Projectile.hostile = true;
			}
			if (Projectile.alpha > 250)
			{
				Projectile.Kill();
			}
			
			deg = Projectile.ai[1];
			Projectile.ai[1] += 1f;

			//deg = 10;

			double rad = deg * (Math.PI / 180);
			double dist = MathHelper.Lerp(1200, 200, EaseHelper.InOutQuad(spawnProgress));
			
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC other = Main.npc[i];

				if (other.active && (other.type == ModContent.NPCType<TsukiyomiBoss>() || other.type == ModContent.NPCType<NalhaunBossPhase2>()))

				{
					Projectile.position.X = other.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
					Projectile.position.Y = other.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
					Projectile.rotation = Vector2.Normalize(other.Center - Projectile.Center).ToRotation() + MathHelper.ToRadians(0f);
					
				}
			}

			Projectile.ai[0]++;

			SearchForTargets(out bool foundTarget, out Vector2 targetCenter);

			if (Projectile.ai[0] >= 30)
			{
				int type = ModContent.ProjectileType<TsukiBolt>();


				Vector2 position = Projectile.Center;

				float launchSpeed = 4f;
				Vector2 direction = Vector2.Normalize(targetCenter - Projectile.Center);
				Vector2 velocity = direction * launchSpeed;

				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)((Math.Cos(Projectile.rotation + MathHelper.ToRadians(90f)) * launchSpeed) * -1), (float)((Math.Sin(Projectile.rotation + MathHelper.ToRadians(90f)) * launchSpeed) * -1), type, 35, 0f, Main.myPlayer);

				Projectile.ai[0] = 0;
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

					if (npc.active && npc.type == ModContent.NPCType<TsukiyomiBoss>() || npc.type == ModContent.NPCType<NalhaunBossPhase2>())
					{
						foundTarget = true;
						targetCenter = Main.player[npc.target].Center;
					}
				}
			}

		}
		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			//Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			for (int d = 0; d < 12; d++)
			{
				Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default(Color), 1.5f);
			}
			// Play explosion sound
			//Main.PlaySound(SoundID.Drown, projectile.position);
			// Smoke Dust spawn

			// Fire Dust spawn (CHANGE TO ICE DUST)

			// Large Smoke Gore spawn


		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 60);
			base.OnHitPlayer(target, damage, crit);
		}
	}
}
