
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Tsukiyomi
{
	public class TsukiDarkmoonSpawnEmpowered : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Carian Dark Moon");
			Main.projFrames[Projectile.type] = 1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 108;
			Projectile.height = 108;
			Projectile.aiStyle = 1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.timeLeft = 90;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.hide = false;
			Projectile.ownerHitCheck = false;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			AIType = ProjectileID.Bullet;
		}

		
		public override void AI() {
			
			Projectile.rotation = MathHelper.ToRadians(135f) + MathHelper.ToRadians(-90f);
			
			if (Projectile.spriteDirection == -1) {
				Projectile.rotation -= MathHelper.ToRadians(90f);
			}
			Projectile.velocity *= 0.1f;

			

			if(Projectile.timeLeft < 40)
            {
				Projectile.alpha += 25;
            }
			
			
		}

		
	}
}
