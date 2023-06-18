
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles
{
    public class SpaceGeometry : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Geometry");
			Main.projFrames[Projectile.type] = 7;
		}

		public override void SetDefaults() {
			Projectile.width = 80;
			Projectile.height = 30;
			Projectile.timeLeft = 450;
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

		}
		bool finished;
		
		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		// It appears that for this AI, only the ai0 field is used!
		public override void AI() {
			Lighting.AddLight(Projectile.Center, new Vector3(0.99f, 0.6f, 0.3f));
			
			if(Projectile.velocity.Y > 0)
            {
				Projectile.velocity.Y -= 0.1f;
            }
			if (Projectile.velocity.Y < 0)
			{
				Projectile.velocity.Y += 0.1f;
			}

			if (Projectile.timeLeft > 50)
            {
				Projectile.damage = 0;
            }
			if(Projectile.timeLeft < 50)
            {
				Vector2 vector8 = new Vector2(Projectile.position.X + (Projectile.width / 2), Projectile.position.Y + (Projectile.height / 2));

				Vector2 vel7 = new Vector2(1, 0);
				vel7 *= 100f;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, vel7.X, vel7.Y, Mod.Find<ModProjectile>("SpaceBlot").Type, 30, 0, Main.myPlayer);
				Vector2 vel8 = new Vector2(-1, 0);
				vel8 *= 100f;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, vel8.X, vel8.Y, Mod.Find<ModProjectile>("SpaceBlot").Type, 30, 0, Main.myPlayer);
				Projectile.Kill();
			}
			
			
			if (++Projectile.frameCounter >= 6)
			{
				Projectile.frameCounter = 0;
				
				if (++Projectile.frame > 6)
				{
					Projectile.frame = 0;

				}
				
				
			}
			
			Projectile.rotation = MathHelper.ToRadians(0f);
			


			// These dusts are added later, for the 'ExampleMod' effect
			
			
			
			

		}
	}
}
