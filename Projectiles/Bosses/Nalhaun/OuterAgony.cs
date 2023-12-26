using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Nalhaun
{
    public class OuterAgony : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Outer Agony");
			
		}

		public override void SetDefaults() {
			Projectile.width = 1000;
			Projectile.height = 1000;
			Projectile.aiStyle = -1;
			Projectile.timeLeft = 10;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = true;
			Projectile.tileCollide = false;

		}
		bool onSpawn = true;
        public override bool PreAI()
        {
			

			return true;
        }
        public override bool CanHitPlayer(Player target)
        {
			//Within the arena.
			if (target.Distance(Projectile.Center) < 1000)
            {
				//This is Outer Agony, so you need to get into the middle.
				if (target.Distance(Projectile.Center) > 350)
				{
					if(Projectile.ai[0] <= 0)//Timer for damage.
                    {
						
						return true;
						
                    }
					else
                    {
						return false;
                    }
				}
			}
				
	

			return false;
        }
        public override void AI() {

			Projectile.timeLeft = 10;

			for (int i = 0; i < 30; i++)
			{//Circle
				Vector2 offset = new Vector2();
				double angle = Main.rand.NextDouble() * 2d * Math.PI;
				offset.X += (float)(Math.Sin(angle) * 350);
				offset.Y += (float)(Math.Cos(angle) * 350);

				Dust d = Dust.NewDustPerfect(Projectile.Center + offset, DustID.Flare, Projectile.velocity, 20, default(Color), 0.5f);

				d.fadeIn = 1f;
				d.noGravity = true;
			}
			if (Projectile.ai[0] < 60)
			{
				for (int i = 0; i < 50; i++)
				{//Circle
					Vector2 offset = new Vector2();
					double angle = Main.rand.NextDouble() * 2d * Math.PI;
					offset.X += (float)(Math.Sin(angle) * 350);
					offset.Y += (float)(Math.Cos(angle) * 350);

					Dust d = Dust.NewDustPerfect(Projectile.Center + offset, DustID.LifeDrain, Projectile.velocity, 20, default(Color), 1.5f);

					d.fadeIn = 1f;
					d.noGravity = true;
				}
			}

			Projectile.ai[0]--;
			if(Projectile.ai[0] == 0)
			{
				SoundEngine.PlaySound(StarsAboveAudio.SFX_Laevateinn, Projectile.Center);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center,Vector2.Zero, ModContent.ProjectileType<OuterAgonyAnimation>(), 0, 0f, Main.myPlayer);

				for (int i = 0; i < 55; i++)
				{
					// Charging dust
					Vector2 vector = new Vector2(
						Main.rand.Next(-548, 548) * (0.003f * 100) - 10,
						Main.rand.Next(-548, 548) * (0.003f * 100) - 10);
					Dust d = Main.dust[Dust.NewDust(
						Projectile.Center + vector, 1, 1,
						DustID.Flare, 0, 0, 255,
						new Color(1f, 1f, 1f), 1.5f)];
					
					d.velocity = -vector / 16;
					d.velocity -= Projectile.velocity / 8;
					d.noLight = true;
					d.noGravity = true;
				}
				for (int i = 0; i < 25; i++)
				{
					// Charging dust
					Vector2 vector = new Vector2(
						Main.rand.Next(-548, 548) * (0.003f * 100) - 10,
						Main.rand.Next(-548, 548) * (0.003f * 100) - 10);
					Dust d = Main.dust[Dust.NewDust(
						Projectile.Center + vector, 1, 1,
						DustID.FireworkFountain_Red, 0, 0, 255,
						new Color(1f, 1f, 1f), 1.5f)];

					d.velocity = -vector / 16;
					d.velocity -= Projectile.velocity / 8;
					d.noLight = true;
					d.noGravity = true;
				}

			}
			if(Projectile.ai[0] <= -10)
            {
				Projectile.Kill();
			}
		}
	}
}
