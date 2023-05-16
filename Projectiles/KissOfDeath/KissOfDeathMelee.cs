using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.KissOfDeath
{
    //
    public class KissOfDeathMelee : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Kiss of Death");
			Main.projFrames[Projectile.type] = 10;
		}
		public override void SetDefaults()
		{
			Projectile.width = 600;
			Projectile.height = 84;
			Projectile.aiStyle = 0;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;

			Projectile.hide = true;
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		// It appears that for this AI, only the ai0 field is used!
		public override void AI()
		{
			Lighting.AddLight(Projectile.Center, new Vector3(1f, 0.4f, 0.64f));

			Projectile.scale = 1f;
			// Since we access the owner player instance so much, it's useful to create a helper local variable for this
			// Sadly, Projectile/ModProjectile does not have its own
			Player projOwner = Main.player[Projectile.owner];
			// Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			projOwner.heldProj = Projectile.whoAmI;
			Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2);
			// As long as the player isn't frozen, the spear can move
			if (!projOwner.frozen)
			{
				if (movementFactor == 0f) // When initially thrown out, the ai0 will be 0f
				{
					movementFactor = 34f; // Make sure the spear moves forward when initially thrown out
					Projectile.netUpdate = true; // Make sure to netUpdate this spear
				}
				if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3) // Somewhere along the item animation, make sure the spear moves back
				{
					movementFactor -= 0f;
				}
				else // Otherwise, increase the movement factor
				{
					movementFactor += 0.3f;
				}
			}
			if (Projectile.frame == 10)
			{
				Projectile.Kill();
			}
			
			if (++Projectile.frameCounter >= 2)
			{
				Projectile.frameCounter = 0;
				if (Projectile.frame < 10)
				{
					Projectile.frame++;
				}
				else
				{
					
				}

			}
			if (projOwner.itemAnimation == 0)
			{
				
			}
			// Change the spear position based off of the velocity and the movementFactor
			Projectile.position += Projectile.velocity * movementFactor;
			// When we reach the end of the animation, we can kill the spear projectile
			
			// Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
			// MathHelper.ToRadians(xx degrees here)
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
			// Offset by 90 degrees here
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation -= MathHelper.ToRadians(90f);
			}

			
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			Player projOwner = Main.player[Projectile.owner];
			projOwner.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;

			for (int d = 0; d < 23; d++)//Visual effects
			{
				Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(6));
				float scale = 1f + (Main.rand.NextFloat() * 0.6f);
				perturbedSpeed = perturbedSpeed * scale;
				int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.WhiteTorch, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 0.6f);
				Main.dust[dustIndex].noGravity = true;

			}
			for (int d = 0; d < 18; d++)
			{
				Dust.NewDust(target.Center, 0, 0, DustID.AmberBolt, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.7f);
				Dust.NewDust(target.Center, 0, 0, DustID.FireworkFountain_Yellow, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.3f);

			}


			base.OnHitNPC(target, damage, knockback, crit);
		}
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
			if(Projectile.ai[1] > 0)
            {
				crit = true;
            }
             
        }
    }
}