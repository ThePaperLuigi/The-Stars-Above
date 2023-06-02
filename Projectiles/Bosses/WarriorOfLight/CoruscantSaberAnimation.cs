
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.WarriorOfLight
{
    public class CoruscantSaberAnimation : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Radiate");
			
		}

		public override void SetDefaults() {
			Projectile.width = 550;
			Projectile.height = 550;
			Projectile.aiStyle = -1;
			Projectile.timeLeft = 30;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = true;
			Projectile.tileCollide = false;

		}
		int slowfade;
		bool start = true;
		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		

		
		public override void AI() {
			Projectile.damage = 0;
			if(start)
            {
				Projectile.scale = 3.5f;
				start = false;
            }
			Projectile.ai[0] += 1f;
			//Projectile.scale += 0.1f;
			//projectile.rotation++;
			float rotationsPerSecond = 2.7f;
			bool rotateClockwise = true;
			//The rotation is set here
			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
			// Fade in
			if (Projectile.ai[0] <= 30)
            {
				Projectile.alpha -= 4;
				if (Projectile.alpha < 100)
				{
					Projectile.alpha = 100;
				}
			}
			else
            {
				Projectile.alpha += 8;
            }


			
		}
	}
}
