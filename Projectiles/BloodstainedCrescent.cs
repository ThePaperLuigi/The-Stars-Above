
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles
{
    public class BloodstainedCrescent : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Bloodstained Crescent");
			//DrawOriginOffsetY = 50;
			//DrawOffsetX = 50;
		}

		public override void SetDefaults() {
			Projectile.width = 400;
			Projectile.height = 400;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 5;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 125;
			Projectile.penetrate = -1;
			Projectile.friendly = true;
			Projectile.minion = true;

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

			for (int d = 0; d < 6; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 20, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 16; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 132, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
			}

			// Fade in
			Projectile.alpha--;
				if (Projectile.alpha < 100)
				{
					Projectile.alpha = 100;
				}

			
		}
	}
}
