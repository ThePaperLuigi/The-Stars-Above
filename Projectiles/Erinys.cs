
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles
{
    public class Erinys : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Erinys");
			Main.projFrames[Projectile.type] = 3;
		}

		public override void SetDefaults() {
			Projectile.width = 300;
			Projectile.height = 300;
			Projectile.aiStyle = 0;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 190;
			Projectile.scale = 1;
			Projectile.alpha = 255;
			Projectile.damage = 0;
			Projectile.hide = false;
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
		}
		int timer;
		bool firstTick = true;
		
		float projectileVelocity = 15;

		
		
		// It appears that for this AI, only the ai0 field is used!
		public override void AI() {
			timer++;
			// Since we access the owner player instance so much, it's useful to create a helper local variable for this
			// Sadly, Projectile/ModProjectile does not have its own
			Player projOwner = Main.player[Projectile.owner];
			// Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			projOwner.heldProj = Projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			//projectile.scale += 0.001f;
			if (timer == 0)
            {

            }
			if (Projectile.frame != 13)
			{
				Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
			}
			if (Projectile.frame != 13)
			{
				Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2) - 180;
			}
			if(Projectile.frame == 1)
            {
				if(firstTick)
                {
					Vector2 placement2 = new Vector2((Projectile.Center.X), Projectile.Center.Y);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),placement2.X, placement2.Y, 0, 0, Mod.Find<ModProjectile>("ClaimhBurst").Type, (int)Projectile.ai[0], 0f, 0, 0);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),placement2.X, placement2.Y, 0, 0, Mod.Find<ModProjectile>("radiateDamage").Type, 0, 0f, 0, 0);
					//Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/HolyStab"));

					firstTick = false;
                }
				
				for (int d = 0; d < 6; d++)
				{
					Dust.NewDust(new Vector2(Projectile.Center.X - 15,Projectile.Center.Y), 0, 0, DustType<Dusts.Star>(), 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
				}

				for (int d = 0; d < 9; d++)
				{
					Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 133, 0f + Main.rand.Next(-26, 26), 0f + Main.rand.Next(-26, 26), 150, default(Color), 1.5f);
				}
				
			}

			if (timer >= 60)
            {
				
				
				

			}
			else
            {
				Projectile.alpha -= 5;
				
				//projectile.position.Y = ownerMountedCenter.Y - (float)(projectile.height / 2) - 100;
				//projectile.position.Y += projectileVelocity;
               // projectileVelocity -= (float)0.3;
            }
			if(projectileVelocity < 0)
            {
				projectileVelocity = 0;
            }
			if(timer < 20)
            {
				Projectile.frameCounter = 0;
			}
			// As long as the player isn't frozen, the spear can move
			
			Projectile.alpha--;
			if (++Projectile.frameCounter >= 8)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 2)
				{
					
					Projectile.frame = 2;

				}

			}
			if(Projectile.timeLeft <= 60)
            {
				Projectile.alpha += 16;
			}
			if(Projectile.alpha >= 250)
            {
				Projectile.Kill();
			}
			// Change the spear position based off of the velocity and the movementFactor
			
			// When we reach the end of the animation, we can kill the spear projectile
			
			// Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
			// MathHelper.ToRadians(xx degrees here)
			//projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
			// Offset by 90 degrees here
			if (Projectile.spriteDirection == -1) {
				//projectile.rotation -= MathHelper.ToRadians(90f);
			}

			// These dusts are added later, for the 'ExampleMod' effect
			
		}
	}
}
