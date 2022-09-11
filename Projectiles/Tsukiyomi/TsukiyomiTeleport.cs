using Microsoft.Xna.Framework;
using Terraria;using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using StarsAbove.Items.Essences;
using StarsAbove.Projectiles;
using Terraria;using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Tsukiyomi
{
	//
	public class TsukiyomiTeleport : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tsukiyomi");
			Main.projFrames[Projectile.type] = 9;
			//DrawOriginOffsetY = 30;
			//DrawOffsetX = -60;
		}
		public override void SetDefaults()
		{
			Projectile.width = 300;
			Projectile.height = 300;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 120;
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
			
			if(Projectile.ai[0] == 0)
            {

            }
			Projectile.ai[0]++;


			if (Projectile.frame == 9)
			{
				
			}
			
			if (++Projectile.frameCounter >= 10)
			{
				Projectile.frameCounter = 0;
				if (Projectile.frame < 9)
				{
					
					Projectile.frame++;
				}
				else
				{
					
					Projectile.Kill();
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