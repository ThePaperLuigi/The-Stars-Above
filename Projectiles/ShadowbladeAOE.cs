using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles
{
    public class ShadowbladeAOE : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Shadowblade Indicator");
			
		}

		public override void SetDefaults() {
			Projectile.width = 750;
			Projectile.height = 750;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 80;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = true;
			Projectile.tileCollide = false;

		}
		bool swapAlpha;
		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		
		public override void AI() {

			Projectile.ai[0] += 1f;
			
			
				// Fade in
			if(!swapAlpha)
            {
				Projectile.alpha--;
				if (Projectile.alpha < 10)
				{
					Projectile.alpha = 10;
					swapAlpha = true;
				}
			}
			else
            {
				Projectile.alpha += 1;
			}
				

			
		}
	}
}
