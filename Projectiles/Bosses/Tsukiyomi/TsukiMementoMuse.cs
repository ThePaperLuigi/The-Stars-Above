
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Tsukiyomi
{
    public class TsukiMementoMuse : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Memento Muse");
			
		}

		public override void SetDefaults() {
			Projectile.width = 108;
			Projectile.height = 108;
			//projectile.aiStyle = 2;//2
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.timeLeft = 160;
			Projectile.hide = false;
			Projectile.ownerHitCheck = false;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.ignoreWater = true;
		}

		float rotationSpeed = 4.7f;
		
		
		public override void AI() {

			if(Projectile.timeLeft < 10)
            {
				Projectile.alpha += 15;
			}
			
			float rotationsPerSecond = rotationSpeed;
			//rotationSpeed -= 0.1f;
			bool rotateClockwise = true;
			//The rotation is set here
			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
			

		}
	}

}
