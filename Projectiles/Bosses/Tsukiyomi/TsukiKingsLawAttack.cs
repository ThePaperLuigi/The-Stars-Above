using Microsoft.Xna.Framework;
using StarsAbove.NPCs.Nalhaun;
using StarsAbove.NPCs.Tsukiyomi;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Bosses.Tsukiyomi
{
    public class TsukiKingsLawAttack : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Caesura of Despair");     //The English name of the projectile
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

			Projectile.ai[0]--;
			if(Projectile.ai[0] <= 0)
            {
				Projectile.Kill();
            }

			if (firstSpawn)
			{
				
				//Projectile.ai[1] = 90;

				firstSpawn = false;
			}
			if (!NPC.AnyNPCs(ModContent.NPCType<TsukiyomiBoss>()) && !NPC.AnyNPCs(ModContent.NPCType<TsukiyomiBoss>()))
			{

				Projectile.Kill();
			}
			
			if (Projectile.alpha > 250)
			{
				Projectile.Kill();
			}
			
			Projectile.ai[1]++;

			SearchForTargets(out bool foundTarget, out Vector2 targetCenter);

			if (Projectile.ai[1] >= 60)
			{

				float speed = 30f;
				int type = ProjectileType<TsukiKingsLawProjectile>();
				int damage = 60;



				for (int ir = 0; ir < 5; ir++)
				{
					Vector2 positionNew = Vector2.Lerp(new Vector2(targetCenter.X - 600, targetCenter.Y - 800), new Vector2(targetCenter.X + 700, targetCenter.Y - 800), (float)ir / 5);
					float rotation = (float)Math.Atan2(positionNew.Y - (targetCenter.Y + (5 * 0.5f)), positionNew.X - (targetCenter.X + (5 * 0.5f)));
					Vector2 velocity = new Vector2((float)((Math.Cos(rotation) * speed) * -1), (float)((Math.Sin(rotation) * speed) * -1));
					Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y) * .2f;

					Projectile.NewProjectile(Projectile.GetSource_FromThis(), positionNew, perturbedSpeed, type, damage, 0f, Main.myPlayer);


				}
				Projectile.ai[1] = 0;
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
		public override void OnKill(int timeLeft)
		{
			


		}
		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			
		}
	}
}
