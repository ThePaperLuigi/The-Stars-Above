
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles
{
    public class Entropy : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Entropy");
			Main.projFrames[Projectile.type] = 7;
		}

		public override void SetDefaults() {
			Projectile.width = 200;
			Projectile.height = 200;
			Projectile.timeLeft = 300;
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
				
				Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, 0, 0, Mod.Find<ModProjectile>("EntropyDamage").Type, 100, 0, Main.myPlayer);
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
