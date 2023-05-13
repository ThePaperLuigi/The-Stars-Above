
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.StellarNovas
{
    public class AsphodeneBurst3 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Asphodene");
			Main.projFrames[Projectile.type] = 14;
		}

		public override void SetDefaults() {
			Projectile.width = 200;
			Projectile.height = 200;
			Projectile.aiStyle = 0;
			Projectile.penetrate = -1;
			Projectile.scale = 1;
			Projectile.alpha = 255;
			Projectile.damage = 0;
			Projectile.hide = false;
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.light = 2f;
		}
		int timer;
		int fadeIn = 0;
		
		float projectileVelocity = 15;

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

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
			fadeIn += 5;
			if (timer == 0)
            {

            }
			if (Projectile.frame != 13)
			{
				Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
			}
			
			if (Projectile.frame != 13)
			{
				Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2) - 105;
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
			if (projectileVelocity < 0)
            {
				projectileVelocity = 0;
            }
			if(timer < 20)
            {
				Projectile.frameCounter = 0;
			}
			// As long as the player isn't frozen, the spear can move
			if (!projOwner.frozen) {
				if (movementFactor == 0f) // When initially thrown out, the ai0 will be 0f
				{
					movementFactor = 0.1f; // Make sure the spear moves forward when initially thrown out
					Projectile.netUpdate = true; // Make sure to netUpdate this spear
				}
				if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3) // Somewhere along the item animation, make sure the spear moves back
				{
					movementFactor += 0f;
				}
				else // Otherwise, increase the movement factor
				{
					movementFactor += 0f;
				}
			}
			Projectile.alpha--;
			if (++Projectile.frameCounter >= 8)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 13)
				{
					
					Projectile.frame = 13;

				}

			}
			if(Projectile.frame == 13)
            {
				Projectile.alpha += 16;
			}
			if(Projectile.alpha >= 250)
            {
				Projectile.Kill();
			}
			// Change the spear position based off of the velocity and the movementFactor
			Projectile.position += Projectile.velocity * movementFactor;
			// When we reach the end of the animation, we can kill the spear projectile
			
			// Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
			// MathHelper.ToRadians(xx degrees here)
			//projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
			// Offset by 90 degrees here
			if (Projectile.spriteDirection == -1) {
				//projectile.rotation -= MathHelper.ToRadians(90f);
			}

			// These dusts are added later, for the 'ExampleMod' effect
			if (Main.rand.NextBool(3)) {
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, 221,
					Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 269, Scale: 1.2f);
				dust.velocity += Projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
				dust.noGravity = true;
			}
			if (Main.rand.NextBool(4)) {
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, 221,
					0, 0, 269, Scale: 0.3f);
				dust.velocity += Projectile.velocity * 0.5f;
				dust.velocity *= 0.5f;
				dust.noGravity = true;
			}
		}

       
    }
}
