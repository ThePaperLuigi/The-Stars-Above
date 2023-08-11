using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Tsukiyomi
{
    //
    public class TsukiyomiTeleportLong : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Tsukiyomi");
			Main.projFrames[Projectile.type] = 15;
			//DrawOriginOffsetY = 30;
			//DrawOffsetX = -60;
		}
		public override void SetDefaults()
		{
			Projectile.width = 150;
			Projectile.height = 150;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 720;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			
			Projectile.hide = false;
			Projectile.ownerHitCheck = false;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.hostile = true;
			//projectile.extraUpdates = 2;
		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs

		// It appears that for this AI, only the ai0 field is used!
		bool firstSpawn = true;
		public override void AI()
		{
			Lighting.AddLight(Projectile.Center, new Vector3(1f, 0.4f, 0.64f));
			 
			 
			// Since we access the owner player instance so much, it's useful to create a helper local variable for this
			// Sadly, Projectile/ModProjectile does not have its own
			//Player projOwner = Main.player[projectile.owner];
			// Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
			//Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			//projectile.direction = projOwner.direction;
			//projOwner.heldProj = projectile.whoAmI;
			//projOwner.itemTime = projOwner.itemAnimation;
			//projectile.position.X = ownerMountedCenter.X - (float)(projectile.width / 2);
			//projectile.position.Y = ownerMountedCenter.Y - (float)(projectile.height / 2);
			// As long as the player isn't frozen, the spear can move
			Projectile.netUpdate = true; // Make sure to netUpdate this spear
			if(firstSpawn)
            {
				for (int d = 0; d < 16; d++)
				{
					Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 20, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 150, default(Color), 1f);
				}
				for (int d = 0; d < 12; d++)
				{
					Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 221, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 150, default(Color), 1f);
				}
				firstSpawn = false;
            }
			
			Projectile.ai[0]++;


			if (Projectile.frame == 9)
			{
				
			}
			
			if (++Projectile.frameCounter >= 10)
			{
				Projectile.frameCounter = 0;
				if (Projectile.frame < 4)
				{
					
					Projectile.frame++;
				}
				else
				{
					Projectile.frame = 0;
				}

			}
			

			//projectile.spriteDirection = projectile.direction = (projectile.velocity.X > 0).ToDirectionInt();
			// Adding Pi to rotation if facing left corrects the drawing
			//projectile.rotation = projectile.velocity.ToRotation() + (projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);


			// These dusts are added later, for the 'ExampleMod' effect
			/*if (Main.rand.NextBool(3))
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 60,
					projectile.velocity.X * .2f, projectile.velocity.Y * .2f, 269, Scale: 1.2f);
				dust.velocity += projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
			if (Main.rand.NextBool(4))
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 60,
					0, 0, 269, Scale: 0.3f);
				dust.velocity += projectile.velocity * 0.5f;
				dust.velocity *= 0.5f;
			}*/
		}
	}
}