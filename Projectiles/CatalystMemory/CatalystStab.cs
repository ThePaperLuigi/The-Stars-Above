
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.CatalystMemory
{
    public class CatalystStab : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Catalyst's Memory");

		}

		public override void SetDefaults() {
			Projectile.width = 180;
			Projectile.height = 180;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.timeLeft = 12;
			Projectile.alpha = 0;
			Projectile.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>();
			Projectile.hide = false;
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 5;
			Projectile.extraUpdates = 1;

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
			Projectile.scale = 0.7f;
			Player projOwner = Main.player[Projectile.owner];
			// Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			
			projOwner.heldProj = Projectile.whoAmI;
			Projectile.position.X = projOwner.Center.X - (float)(Projectile.width / 2);
			Projectile.position.Y = projOwner.Center.Y - (float)(Projectile.height / 2);

			// Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
			// MathHelper.ToRadians(xx degrees here)
			Projectile.rotation = Projectile.velocity.ToRotation();//+ MathHelper.ToRadians(135f)
																   // Offset by 90 degrees here
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation -= MathHelper.ToRadians(180f);
			}
			Projectile.spriteDirection = Projectile.direction;
			// As long as the player isn't frozen, the spear can move
			if (!projOwner.frozen) {
				if (movementFactor == 0f) // When initially thrown out, the ai0 will be 0f
				{
					movementFactor = 6.5f; // Make sure the spear moves forward when initially thrown out
					Projectile.netUpdate = true; // Make sure to netUpdate this spear
				}
				movementFactor += 0.1f;
			}
			// Change the spear position based off of the velocity and the movementFactor
			Projectile.position += Projectile.velocity * movementFactor;
			// When we reach the end of the animation, we can kill the spear projectile
			if (projOwner.itemTime < 1) {
				Projectile.Kill();
			}
			

			// These dusts are added later, for the 'ExampleMod' effect

		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(target.Center, 0, 0, DustID.GemAmethyst, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.4f);
				Dust.NewDust(target.Center, 0, 0, DustID.PurpleCrystalShard, Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8), 150, default(Color), 0.8f);
			}

			base.OnHitNPC(target, damage, knockback, crit);
		}
	}

	
}
