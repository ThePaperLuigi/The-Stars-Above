
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles
{
    public class reverseRadiate : ModProjectile
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
		bool firstSpawn = true;
		int slowfade;
		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
        public override bool PreDraw(ref Color lightColor)
        {

            return base.PreDraw(ref lightColor);
        }

        public override void AI() {
			if(firstSpawn)
            {
				Projectile.scale = 6f;
				firstSpawn = false;
            }
			Projectile.damage = 0;
			Projectile.ai[0] += 1f;
			Projectile.scale -= 0.1f;
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
			if(Projectile.scale == 0f)
            {
				Projectile.Kill();
            }

			
		}
	}
}
