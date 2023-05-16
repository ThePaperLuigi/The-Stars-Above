
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles
{
    public class radiate : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Radiate");
			
		}

		public override void SetDefaults() {
			Projectile.width = 250;
			Projectile.height = 250;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 300;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = true;


		}
		int slowfade;
		bool start = true;
		public override void AI() {
			if (start)
			{
				Projectile.scale = 0.1f;
				start = false;
			}
			Projectile.damage = 0;
			Projectile.ai[0] += 1f;
			Projectile.scale += 0.1f;
			//projectile.rotation++;
			float rotationsPerSecond = 0.7f;
			bool rotateClockwise = true;
			//The rotation is set here
			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
			// Fade in
			if (Projectile.ai[0] <= 50)
            {
				Projectile.alpha -= 4;
				if (Projectile.alpha < 0)
				{
					Projectile.alpha = 0;
				}
			}
			else
            {
				Projectile.alpha += 2;
            }


			
		}
	}
}
