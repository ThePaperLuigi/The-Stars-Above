using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.SaltwaterScourge
{
    public class PowderKeg : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Saltwater Scourge");
			
		}

		public override void SetDefaults() {
			Projectile.width = 36;
			Projectile.height = 36;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 1200;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			//Projectile.hostile = true;


		}

		bool firstSpawn = true;
		float rotationCompletion;
		int timeBeforeDeath;
		bool readyToExplode = false;

		public override void AI() {

			Projectile.ai[0] += 1f;
			Player projOwner = Main.player[Projectile.owner];
			
			
			if(!readyToExplode)
            {
				Projectile.rotation = MathHelper.ToRadians(MathHelper.Lerp(-720, 0, Utilities.EaseHelper.InOutQuad(rotationCompletion)));

				Projectile.scale = MathHelper.Lerp(2f, 1f, rotationCompletion);
				Projectile.alpha = (int)MathHelper.Lerp(255, 0, rotationCompletion);
			}
			

			if(rotationCompletion <= 1f)
            {
				rotationCompletion += 0.02f;
				//rotationCompletionSpeed -= 0.001f;
            }
			DrawDust();
			
			DoExplosion();

			if (!projOwner.GetModPlayer<WeaponPlayer>().SaltwaterScourgeHeld)
			{
				Projectile.Kill();
			}
			if (Main.rand.NextBool(5))
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Flare,
					Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 269, Scale: 1.2f);
				dust.velocity += Projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
		}
		private void DrawDust()
        {
			for (int i = 0; i < 50; i++)
			{//Circle
				Vector2 offset = new Vector2();
				double angle = Main.rand.NextDouble() * 2d * Math.PI;
				offset.X += (float)(Math.Sin(angle) * 250);
				offset.Y += (float)(Math.Cos(angle) * 250);

				Dust d = Dust.NewDustPerfect(Projectile.Center + offset, DustID.Smoke, Vector2.Zero, 200, default(Color), 1.5f);
				Dust d2 = Dust.NewDustPerfect(Projectile.Center + offset, DustID.Flare, Vector2.Zero, 200, default(Color), 0.4f);

				d.fadeIn = 0.5f;
				d.noGravity = true;

				d2.fadeIn = 0.5f;
				d2.noGravity = true;
			}

			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile other = Main.projectile[i];

				if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner &&
					(other.type == ProjectileType<PowderKeg>()) && other.Distance(Projectile.Center) < 250)
				{
					for (int ir = 0; ir < 50; ir++)
					{
						Vector2 position = Vector2.Lerp(Projectile.Center, other.Center, (float)ir / 50);
						Dust d = Dust.NewDustPerfect(position, DustID.Smoke, null, 240, default(Color), 1.5f);
						Dust d2 = Dust.NewDustPerfect(position, DustID.Flare, null, 240, default(Color), 0.7f);

						d.fadeIn = 0.3f;
						d.noLight = true;
						d.noGravity = true;

						d2.fadeIn = 0.3f;
						d2.noLight = true;
						d2.noGravity = true;

					}

				}
			}
		}
		private void DoExplosion()
        {
			
			if(!readyToExplode)
            {
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					Projectile other = Main.projectile[i];

					if (i != Projectile.whoAmI && other.active &&
						((other.type == ProjectileType<SaltwaterSlash1>()) || (other.type == ProjectileType<SaltwaterSlash2>()) || (other.type == ProjectileType<SaltwaterCannonball>())) && other.Distance(Projectile.Center) < 50)
					{
						//Boom, and make nearby barrels boom too.

						SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Main.player[Projectile.owner].Center);
						
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileType<PowderKegExplosion>(), Projectile.damage * 4, 0f, Projectile.owner, 0f, 0f);
						Projectile.alpha = 255;
						readyToExplode = true;

						

						//Projectile.Kill();

					}
				}
			}
			else
            {
				//Projectile.ai[2]++;
				timeBeforeDeath++;
				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					Projectile other = Main.projectile[i];

					if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner &&
						(other.type == ProjectileType<PowderKeg>()) && other.Distance(Projectile.Center) < 250)
					{
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), other.Center, Vector2.Zero, ProjectileType<PowderKegExplosion>(), (int)((Projectile.damage * 4)*1.5f), 0f, Projectile.owner, 0f, 0f);
						other.Kill();
					}
				}

				if(timeBeforeDeath >= 10)
                {
					Player player = Main.player[Projectile.owner];

					player.AddBuff(BuffID.Swiftness, 480);
					//Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileType<PowderKegExplosion>(), Projectile.damage * 3, 0f, Projectile.owner, 0f, 0f);
					Projectile.Kill();
                }
			}
			
		}

        public override void Kill(int timeLeft)
        {
			// Smoke Dust spawn
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1f;
			}
			
			// Large Smoke Gore spawn
			for (int g = 0; g < 4; g++)
			{
				int goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 7f, Projectile.position.Y + (float)(Projectile.height / 2) - 7f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.2f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 7f, Projectile.position.Y + (float)(Projectile.height / 2) - 7f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.2f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 7f, Projectile.position.Y + (float)(Projectile.height / 2) - 7f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.2f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 7f, Projectile.position.Y + (float)(Projectile.height / 2) - 7f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.2f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
			}

			base.Kill(timeLeft);
        }
    }
}
