
using Microsoft.Xna.Framework;
using System;
using System.Security.Policy;
using Terraria;using Terraria.GameContent;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles
{
	public class BrushStroke : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Brush Stroke");
			//DrawOriginOffsetY = -150;
			Main.projFrames[Projectile.type] = 6;
		}

		public override void SetDefaults() {
			Projectile.width = 50;
			Projectile.height = 357;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 200;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = true;


		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		
		public override void AI() {
			float rotationValue = MathHelper.ToRadians(Main.rand.Next(0, 364));
			Projectile.damage = 0;
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] == 1)
			{

				Projectile.rotation += rotationValue - MathHelper.ToRadians(90);

			}
			if (Projectile.ai[0] >= 70)
			{
				if (++Projectile.frameCounter >= 3)
				{
					Projectile.frameCounter = 0;
					if (Projectile.frame < 6)
					{
						Projectile.frame++;
					}
					else
					{
						for (int d = 0; d < 8; d++)
						{
							float Speed2 = Main.rand.NextFloat(18, 38);  //projectile speed
							Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotationValue) * Speed2) * -1), (float)((Math.Sin(rotationValue) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(5)); // 30 degree spread.
							int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 82, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
							Main.dust[dustIndex].noGravity = true;
						}
						for (int d = 0; d < 5; d++)
						{
							float Speed3 = Main.rand.NextFloat(20, 44);  //projectile speed
							Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotationValue) * Speed3) * -1), (float)((Math.Sin(rotationValue) * Speed3) * -1)).RotatedByRandom(MathHelper.ToRadians(10)); // 30 degree spread.
							int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 82, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
							Main.dust[dustIndex].noGravity = true;
						}
						for (int d = 0; d < 8; d++)
						{
							float Speed2 = Main.rand.NextFloat(-18, -38);  //projectile speed
							Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotationValue) * Speed2) * -1), (float)((Math.Sin(rotationValue) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(5)); // 30 degree spread.
							int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 82, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
							Main.dust[dustIndex].noGravity = true;
						}
						for (int d = 0; d < 5; d++)
						{
							float Speed3 = Main.rand.NextFloat(-20, -44);  //projectile speed
							Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotationValue) * Speed3) * -1), (float)((Math.Sin(rotationValue) * Speed3) * -1)).RotatedByRandom(MathHelper.ToRadians(10)); // 30 degree spread.
							int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 82, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
							Main.dust[dustIndex].noGravity = true;
						}
						Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center, Vector2.Zero, ProjectileType<BrushStrokeDamage>(), 200, 0f, Projectile.owner, 0f, 0f);
						Projectile.Kill();
					}

				}
			}
			

			// Fade in
			Projectile.alpha-=10;
				if (Projectile.alpha < 100)
				{
					Projectile.alpha = 100;
				}

			
		}
	}
}
