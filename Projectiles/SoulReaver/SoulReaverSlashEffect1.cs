using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.SoulReaver
{
    //
    public class SoulReaverSlashEffect1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Reaver");
			Main.projFrames[Projectile.type] = 4;
			//DrawOriginOffsetY = 30;
			//DrawOffsetX = -60;
		}
		public override void SetDefaults()
		{
			Projectile.width = 600;
			Projectile.height = 600;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 50;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			
			Projectile.hide = false;
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
			DrawOriginOffsetY = 150;
		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		// It appears that for this AI, only the ai0 field is used!
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(target.Center, 0, 0, DustID.Clentaminator_Purple, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.4f);

			}
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(target.Center, 0, 0, DustID.PurpleCrystalShard, Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8), 150, default(Color), 0.5f);

			}
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(target.Center, 0, 0, DustID.FireworkFountain_Pink, Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-7, 7), 150, default(Color), 0.6f);

			}

			base.OnHitNPC(target, damage, knockback, crit);
		}
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			if(target.HasBuff(BuffID.ShadowFlame))
            {
				damage = (int)(damage * 1.5);
            }

            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        public override void AI()
		{
			Lighting.AddLight(Projectile.Center, new Vector3(1f, 1f, 1f));
			 
			 
			// Since we access the owner player instance so much, it's useful to create a helper local variable for this
			// Sadly, Projectile/ModProjectile does not have its own
			Player projOwner = Main.player[Projectile.owner];
			// Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			projOwner.itemTime = projOwner.itemAnimation;
			Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2);
			// As long as the player isn't frozen, the spear can move
			if (!projOwner.frozen)
			{
				if (movementFactor == 0f) // When initially thrown out, the ai0 will be 0f
				{
					movementFactor = 14f; // Make sure the spear moves forward when initially thrown out
					Projectile.netUpdate = true; // Make sure to netUpdate this spear
				}
				if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3) // Somewhere along the item animation, make sure the spear moves back
				{
					movementFactor -= 0f;
				}
				else // Otherwise, increase the movement factor
				{
					movementFactor += 0.7f;
				}
			}
			if (Projectile.frame >= 3)
			{
				Projectile.alpha += 22;
			}
			
			if (++Projectile.frameCounter >= 5)
			{
				Projectile.frameCounter = 0;
				if (Projectile.frame < 3)
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
			if (Projectile.alpha >= 250)
			{
				Projectile.Kill();
			}
			Projectile.scale += 0.001f;
			Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
			// Adding Pi to rotation if facing left corrects the drawing
			Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);


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