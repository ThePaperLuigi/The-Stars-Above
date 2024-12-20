using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Other.Manifestation
{
    //
    public class SplitVertical : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Manifestation");
            Main.projFrames[Projectile.type] = 2;

            //DrawOffsetX = -60;
        }

        public override void SetDefaults()
        {
            Projectile.width = 262;
            Projectile.height = 262;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 45;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;

            Projectile.hide = false;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = ModContent.GetInstance<Systems.PsychomentDamageClass>();
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
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.SetCrit();

            for (int d = 0; d < 30; d++)
            {
                Dust.NewDust(target.Center, 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-5, 5), 150, default, 1.5f);
            }
            for (int d = 0; d < 44; d++)
            {
                Dust.NewDust(target.Center, 0, 0, DustID.LifeDrain, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default, 1.5f);
            }
            for (int d = 0; d < 26; d++)
            {
                Dust.NewDust(target.Center, 0, 0, DustID.Firework_Red, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 150, default, 1.5f);
            }

            if (target.life < (int)(target.lifeMax * 0.3) && !target.boss)
            {
                modifiers.SetInstantKill();
            }


        }

        // It appears that for this AI, only the ai0 field is used!
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, new Vector3(1f, 1f, 1f));


            // Since we access the owner player instance so much, it's useful to create a helper local variable for this
            // Sadly, Projectile/ModProjectile does not have its own
            Player projOwner = Main.player[Projectile.owner];
            // Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            Projectile.direction = projOwner.direction;
            projOwner.heldProj = Projectile.whoAmI;
            projOwner.itemTime = projOwner.itemAnimation;

            /*if(projOwner.direction == 1)
            {
				Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2) - 40;
				Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2) + 20;
			}
			else
            {
				Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2) + 20;
				Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2) + 20;
			}*/
            Projectile.position.X = ownerMountedCenter.X - Projectile.width / 2;
            Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2;

            // As long as the player isn't frozen, the spear can move
            if (!projOwner.frozen)
            {
                if (movementFactor == 0f) // When initially thrown out, the ai0 will be 0f
                {
                    movementFactor = 17f; // Make sure the spear moves forward when initially thrown out
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

            projOwner.heldProj = Projectile.whoAmI;//The projectile draws in front of the player.

            if (Projectile.frame >= 1)
            {
                Projectile.alpha += 8;
            }

            if (++Projectile.frameCounter >= 8)
            {
                Projectile.frameCounter = 0;
                if (Projectile.frame < 1)
                {
                    Projectile.frame++;
                }
                else
                {
                    Projectile.friendly = false;
                }

            }
            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }

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