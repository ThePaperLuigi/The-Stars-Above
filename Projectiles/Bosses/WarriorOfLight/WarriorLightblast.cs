
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.WarriorOfLight
{
    public class WarriorLightblast : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
			Main.projFrames[Projectile.type] = 11;
		}

		public override void SetDefaults() {
			Projectile.width = 40;
			Projectile.height = 40;
			Projectile.timeLeft = 680;
			Projectile.penetrate = -1;
			Projectile.aiStyle = 1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.localNPCHitCooldown = -1;
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.netUpdate = true;
			AIType = ProjectileID.Bullet;
			DrawOriginOffsetY = -25;
			DrawOffsetX = -25;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			default(Effects.YellowTrail).Draw(Projectile);

			return true;
		}
		public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
			Projectile.timeLeft = 50;

             
        }

		

		public override void AI() {
			Lighting.AddLight(Projectile.Center, new Vector3(0.99f, 0.6f, 0.3f));
			
			
			
			
			if (++Projectile.frameCounter >= 4)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 11 && Projectile.timeLeft < 50)
				{
					//Main.PlaySound(SoundID.Drip, projectile.Center, 0);
					Projectile.Kill();

				}
				if (++Projectile.frame >= 4 && Projectile.timeLeft > 50)
				{
					Projectile.frame = 0;

				}
				
				
			}
			
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
			// Offset by 90 degrees here
			if (Projectile.spriteDirection == -1) {
				Projectile.rotation -= MathHelper.ToRadians(90f);
			}


			// These dusts are added later, for the 'ExampleMod' effect
			
			
			
			

		}
	}
}
