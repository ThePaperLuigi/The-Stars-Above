using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses
{
    public class CircleAoE : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Adjustable Circle AoE");
			
		}

		public override void SetDefaults() {
			Projectile.width = 250;
			Projectile.height = 250;
			Projectile.aiStyle = -1;
			Projectile.timeLeft = 100;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = false;
			Projectile.tileCollide = false;

		}
		bool onSpawn = true;
        public override bool PreAI()
        {
			Projectile.timeLeft = (int)Projectile.ai[0];
			Projectile.width = (int)Projectile.ai[1];
			Projectile.height = (int)Projectile.ai[1];
			

			return true;
        }
        public override void AI() {

			
			Projectile.ai[0] += 1f;
			
			
				// Fade in
				Projectile.alpha--;
				if (Projectile.alpha < 100)
				{
					Projectile.alpha = 100;
				}

			
		}
	}
}
