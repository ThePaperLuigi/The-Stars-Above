
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.BlackSilence
{
    public class AllasStab : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Gloves of the Black Silence");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 76;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 200;
			Projectile.height = 200;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.timeLeft = 15;
			Projectile.alpha = 0;
			Projectile.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>();
			Projectile.hide = false;
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		// It appears that for this AI, only the ai0 field is used!
		public override void AI() {
			// Since we access the owner player instance so much, it's useful to create a helper local variable for this
			// Sadly, Projectile/ModProjectile does not have its own
			Player projOwner = Main.player[Projectile.owner];
			// Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			projOwner.heldProj = Projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			Projectile.position.X = projOwner.Center.X - (float)(Projectile.width / 2);
			Projectile.position.Y = projOwner.Center.Y - (float)(Projectile.height / 2);
			// As long as the player isn't frozen, the spear can move
			if (!projOwner.frozen) {
				if (movementFactor == 0f) // When initially thrown out, the ai0 will be 0f
				{
					movementFactor = 10f; // Make sure the spear moves forward when initially thrown out
					Projectile.netUpdate = true; // Make sure to netUpdate this spear
				}
				if (Projectile.timeLeft < 10) // Somewhere along the item animation, make sure the spear moves back
				{
					movementFactor += 0.4f;
					Projectile.alpha += 20;
				}
				else // Otherwise, increase the movement factor
				{
					movementFactor += 2.1f;
				}
			}
			// Change the spear position based off of the velocity and the movementFactor
			Projectile.position += Projectile.velocity * movementFactor;
			// When we reach the end of the animation, we can kill the spear projectile
			if (Projectile.alpha > 250) {
				Projectile.Kill();
			}
			// Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
			// MathHelper.ToRadians(xx degrees here)
			Projectile.rotation = Projectile.velocity.ToRotation();//+ MathHelper.ToRadians(135f)
			
			Projectile.alpha -= 40;

			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation -= MathHelper.ToRadians(180f);
			}
			Projectile.spriteDirection = Projectile.direction;

			// These dusts are added later, for the 'ExampleMod' effect

		}

		public override bool PreDraw(ref Color lightColor)
		{
			default(Effects.WhiteTrail).Draw(Projectile);

			return true;
		}
	}

	
}
