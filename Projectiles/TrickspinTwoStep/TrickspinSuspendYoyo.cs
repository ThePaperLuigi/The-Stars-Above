
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using System;
using Terraria.ModLoader;
using StarsAbove.Buffs;

namespace StarsAbove.Projectiles.TrickspinTwoStep
{

    public class TrickspinSuspendYoyo : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 1;
			Main.projPet[Projectile.type] = true;
			//ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			//ProjectileID.Sets.Homing[Projectile.type] = true;
			//ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; //This is necessary for right-click targeting
		}

		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 28;
			Projectile.height = 28;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.minion = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 240;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			//ProjectileType<TakodachiRound>();


		}

		public override bool? CanCutTiles()
		{
			return false;
		}

		public override bool MinionContactDamage()
		{
			return true;
		}

		public override void AI()
		{
			float rotationsPerSecond = 0.3f;
			bool rotateClockwise = true;
			//The rotation is set here
			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);


			Player owner = Main.player[Projectile.owner];
			owner.GetModPlayer<WeaponPlayer>().TrickspinCenter = Projectile.Center;
			if (!CheckActive(owner))
			{
				return;
			}

			Projectile.velocity *= 0.94f;
			if(Projectile.velocity.X < 1 && Projectile.ai[0] < 180)
            {
				owner.GetModPlayer<WeaponPlayer>().TrickspinReady = true;

				for (int i2 = 0; i2 < 5; i2++)
				{//Circle
					Vector2 offset = new Vector2();
					double angle = Main.rand.NextDouble() * 2d * Math.PI;
					offset.X += (float)(Math.Sin(angle) * 400f);
					offset.Y += (float)(Math.Cos(angle) * 400f);

					Dust d2 = Dust.NewDustPerfect(Projectile.Center + offset, DustID.PurificationPowder, Projectile.velocity, 0, default(Color), 0.7f);
					d2.fadeIn = 0.0001f;
					d2.noGravity = true;
				}
				Projectile.friendly = false;

			}
			else
            {
				owner.GetModPlayer<WeaponPlayer>().TrickspinReady = false;

				Projectile.friendly = true;
            }
			
			if (Projectile.Distance(owner.Center) > 400)
			{
				Projectile.ai[0] = 180;

			}
			Projectile.ai[0]++;
			if(Projectile.ai[0] > 180)
			{
				Projectile.ai[1]++;

				Projectile.position.X = MathHelper.Lerp(Projectile.position.X , owner.Center.X - Projectile.width / 2, Projectile.ai[1] / 60);
				Projectile.position.Y = MathHelper.Lerp(Projectile.position.Y , owner.Center.Y - Projectile.height / 2, Projectile.ai[1] / 60);

			}
			if(Projectile.ai[1] >= 20)
            {
				Projectile.Kill();
            }
		}

        
        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
		{
			if (owner.dead || !owner.active)
			{
				

				return false;
			}


			return true;
		}

		
		public override void Kill(int timeLeft)
		{
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.AmberBolt, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default(Color), 0.5f);
				Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default(Color), 0.4f);

			}
			
		}
		
	}
}

