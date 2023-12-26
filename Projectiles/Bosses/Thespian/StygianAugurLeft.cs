
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Thespian
{
    public class StygianAugurLeft : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
			
			
		}

		public override void SetDefaults() {
			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.timeLeft = 420;
			Projectile.penetrate = -1;
			Projectile.aiStyle = 1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.localNPCHitCooldown = -1;
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.netUpdate = true;

		}
		bool firstSpawn = true;
		float rotationSpeed = 3.7f;
		float throwSpeed = 10f;
		public override bool PreDraw(ref Color lightColor)
		{

			default(Effects.SmallBlackTrail).Draw(Projectile);

			return true;
		}
		public override void AI() {
			if(firstSpawn)
            {
				for(int i = 0; i < 10; i++)
                {
					Dust.NewDust(Projectile.Center, 0, 0, DustID.Shadowflame, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.5f);

				}
				firstSpawn = false;

			}
			Lighting.AddLight(Projectile.Center, new Vector3(0.99f, 0.6f, 0.3f));
			Projectile.alpha -= 3;
			if (Projectile.timeLeft > 360)
			{
				Projectile.hostile = false;
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
				Projectile.hostile = true;
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
				Projectile.velocity = new Vector2(-4, 0);
            }
		}
	}
}
