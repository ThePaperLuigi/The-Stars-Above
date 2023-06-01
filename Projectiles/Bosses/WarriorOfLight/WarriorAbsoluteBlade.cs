
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.WarriorOfLight
{
    public class WarriorAbsoluteBlade : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
			Main.projFrames[Projectile.type] = 10;
			
			
		}

		public override void SetDefaults() {
			Projectile.width = 18;
			Projectile.height = 48;
			Projectile.timeLeft = 800;
			Projectile.penetrate = -1;
			Projectile.aiStyle = 1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.localNPCHitCooldown = -1;
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.netUpdate = true;
			AIType = ProjectileID.Bullet;
			DrawOffsetX = -19;

		}
		bool finished;
		float rotationSpeed = 3.7f;
		float throwSpeed = 10f;
		public override bool PreDraw(ref Color lightColor)
		{

			default(Effects.SmallWhiteTrail).Draw(Projectile);

			return true;
		}
		public override void AI() {
			Lighting.AddLight(Projectile.Center, new Vector3(0.99f, 0.6f, 0.3f));
			Projectile.alpha -= 3;
			if(Projectile.frame != 9)
            {
				Projectile.damage = 0;
				Projectile.velocity = Vector2.Zero;
				float rotationsPerSecond = rotationSpeed;
				rotationSpeed -= 3f;
				bool rotateClockwise = true;
				//The rotation is set here
				Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
				if (Projectile.spriteDirection == -1)
				{
					Projectile.rotation -= MathHelper.ToRadians(90f);
				}
			}
			else
            {
				Projectile.damage = 100;
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
				Projectile.velocity = new Vector2(0, -3);
            }
			
			if (++Projectile.frameCounter >= 11)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 9)
				{
					Projectile.frame = 9;
					
				}
				
			}
			
			
			// Offset by 90 degrees here
			if (Projectile.spriteDirection == -1) {
				Projectile.rotation -= MathHelper.ToRadians(90f);
			}


			
		}
	}
}
