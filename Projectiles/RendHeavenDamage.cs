
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using StarsAbove.Systems;

namespace StarsAbove.Projectiles
{
    public class RendHeavenDamage : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Rend Heaven");
			
		}

		public override void SetDefaults() {
			Projectile.width = 420;
			Projectile.height = 2048;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.damage = 200;
			Projectile.penetrate = -1;
			Projectile.hostile = true;
			Projectile.tileCollide = false;

		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		
		public override void AI() {

			Projectile.ai[0] += 1f;
			SoundEngine.PlaySound(StarsAboveAudio.SFX_GunbladeImpact, Projectile.Center);

			for (int d = 0; d < 130; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 0, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-100, 100), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 144; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 0, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-150, 150), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 126; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 133, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-160, 160), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 30; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 7, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-130, 130), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 40; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 269, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-130, 130), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 50; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 78, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-150, 150), 150, default(Color), 1.5f);
			}
			// Smoke Dust spawn
			/*for (int i = 0; i < 70; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-1, 1), 0f + Main.rand.Next(-160, 160), 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 4.4f;
			}
			// Fire Dust spawn
			for (int i = 0; i < 80; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-160, 160), 100, default(Color), 3f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 5f;
				dustIndex = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-160, 160), 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 3f;
			}*/
			// Large Smoke Gore spawn
			
			// Fade in
			Projectile.alpha--;
				if (Projectile.alpha < 100)
				{
					Projectile.alpha = 100;
				}

			
		}
	}
}
