
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Other.SunsetOfTheSunGod
{
    public class KarnaSpearAttack : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sunset of the Sun God");

        }

        public override void SetDefaults()
        {
            Projectile.width = 220;
            Projectile.height = 220;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.timeLeft = 15;
            Projectile.alpha = 0;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.hide = false;
            Projectile.ownerHitCheck = true;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public bool firstSpawn = true;

        public float movementFactor
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }


        public override void AI()
        {
            Player projOwner = Main.player[Projectile.owner];
            projOwner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (projOwner.Center - Projectile.Center).ToRotation() + MathHelper.PiOver2);

            DelegateMethods.v3_1 = new Vector3(0.6f, 1f, 1f) * 0.2f;
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * 10f, 8f, DelegateMethods.CastLightOpen);


            // Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            Projectile.direction = projOwner.direction;
            projOwner.heldProj = Projectile.whoAmI;
            projOwner.itemTime = projOwner.itemAnimation;
            Projectile.position.X = projOwner.Center.X - Projectile.width / 2;
            Projectile.position.Y = projOwner.Center.Y - Projectile.height / 2;
            // As long as the player isn't frozen, the spear can move
            if (!projOwner.frozen)
            {
                if (movementFactor == 0f) // When initially thrown out, the ai0 will be 0f
                {
                    movementFactor = 1f; // Make sure the spear moves forward when initially thrown out
                    Projectile.netUpdate = true; // Make sure to netUpdate this spear
                }
                if (Projectile.timeLeft < 10) // Somewhere along the item animation, make sure the spear moves back
                {

                    movementFactor += 0.4f;
                    Projectile.alpha += 20;
                }
                else // Otherwise, increase the movement factor
                {
                    movementFactor += 3.1f;
                }
            }
            // Change the spear position based off of the velocity and the movementFactor
            Projectile.position += Projectile.velocity * movementFactor;
            // When we reach the end of the animation, we can kill the spear projectile
            if (Projectile.alpha > 250)
            {
                float dustAmount = 16f;

                for (int i = 0; i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(1f, 4f);
                    spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                    int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemTopaz);
                    Main.dust[dust].scale = 1f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = Projectile.Center + spinningpoint5;
                    Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 1f;
                }
                Projectile.Kill();
            }
            // Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
            // MathHelper.ToRadians(xx degrees here)
            Projectile.rotation = Projectile.velocity.ToRotation();//+ MathHelper.ToRadians(135f)
                                                                   // Offset by 90 degrees here
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= MathHelper.ToRadians(180f);
            }
            Projectile.spriteDirection = Projectile.direction;

            if (firstSpawn)
            {
                firstSpawn = false;
                SoundEngine.PlaySound(SoundID.DD2_JavelinThrowersAttack, projOwner.position);

                float dustAmount = 16f;
                for (int i = 0; i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(1f, 4f);
                    spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                    int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemTopaz);
                    Main.dust[dust].scale = 1f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = Projectile.Center + spinningpoint5;
                    Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 3f;
                }


            }


        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            KarnaOnHitDust(target);



        }

        private void KarnaOnHitDust(NPC target)
        {

            float dustAmount = 33f;
            float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(18f, 1f);
                spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant);
                int dust = Dust.NewDust(target.Center, 0, 0, DustID.LifeDrain);
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = target.Center + spinningpoint5;
                Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
            }
        }
    }


}
