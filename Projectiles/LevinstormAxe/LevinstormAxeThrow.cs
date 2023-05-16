
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.LevinstormAxe;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.LevinstormAxe
{
    public class LevinstormAxeThrow : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Boltstorm Axe");
			
		}

		public override void SetDefaults() {
			Projectile.width = 122;
			Projectile.height = 122;
			//projectile.aiStyle = 2;//2
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.timeLeft = 35;
			Projectile.hide = true;
			Projectile.ownerHitCheck = false;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
		}

		float rotationSpeed = 3.7f;
		float throwSpeed = 10f;
		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
		

		public override void AI() {
			
			

			// Since we access the owner player instance so much, it's useful to create a helper local variable for this
			// Sadly, Projectile/ModProjectile does not have its own
			Player projOwner = Main.player[Projectile.owner];
			// Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			
			projOwner.heldProj = Projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width/2);
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height/2);
			// As long as the player isn't frozen, the spear can move
			if (!projOwner.frozen)
			{
				if (movementFactor == 0f) // When initially thrown out, the ai0 will be 0f
				{
					movementFactor = 6f; // Make sure the spear moves forward when initially thrown out
					Projectile.netUpdate = true; // Make sure to netUpdate this spear


				}
				movementFactor += throwSpeed;
				throwSpeed -= 0.6f;
				if (movementFactor < 0)
				{
					movementFactor = 0;
				}
			}
			/*if (++projectile.frameCounter >= 2)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= Main.projFrames[projectile.type])
				{
					projectile.frame = 10;
				}
			}*/
			// Change the spear position based off of the velocity and the movementFactor
			Projectile.position += Projectile.velocity * movementFactor;
			// When we reach the end of the animation, we can kill the spear projectile
			if (projOwner.itemAnimation == 1) {
				Projectile.Kill();
			}
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation -= MathHelper.ToRadians(90f);
			}

			projOwner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (projOwner.Center - Projectile.Center).ToRotation() + MathHelper.PiOver2);


			float rotationsPerSecond = rotationSpeed;
			rotationSpeed -= 0.1f;
			bool rotateClockwise = true;
			//The rotation is set here
			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation -= MathHelper.ToRadians(90f);
			}

		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			Projectile.NewProjectile(null, target.Center.X, target.Center.Y, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), ProjectileType<LevinstormLightning>(), Projectile.damage / 4, Projectile.knockBack, Projectile.owner, Main.rand.Next(0, 360) + 1000f, 1);
			if(Main.rand.NextBool())
            {
				Projectile.NewProjectile(null, target.Center.X, target.Center.Y, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), ProjectileType<LevinstormLightning>(), Projectile.damage / 4, Projectile.knockBack, Projectile.owner, Main.rand.Next(0, 360) + 1000f, 1);
				if (Main.rand.NextBool())
				{
					Projectile.NewProjectile(null, target.Center.X, target.Center.Y, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), ProjectileType<LevinstormLightning>(), Projectile.damage / 4, Projectile.knockBack, Projectile.owner, Main.rand.Next(0, 360) + 1000f, 1);

				}
			}

			if (crit && Main.player[Projectile.owner].HasBuff(BuffType<GatheringLevinstorm>()))
            {
				Projectile.NewProjectile(null, target.Center.X, target.Center.Y, 0,0, ProjectileType<LevinstormExplosion>(), damage, Projectile.knockBack, Projectile.owner);

			}

			base.OnHitNPC(target, damage, knockback, crit);
        }
    }

}
