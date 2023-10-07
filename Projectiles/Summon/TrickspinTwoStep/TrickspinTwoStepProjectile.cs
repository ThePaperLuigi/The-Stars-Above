using Microsoft.Xna.Framework;
using StarsAbove.Buffs.TagDamage;
using StarsAbove.Buffs.TrickspinTwoStep;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Summon.TrickspinTwoStep
{
    public class TrickspinTwoStepProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // The following sets are only applicable to yoyo that use aiStyle 99.

            // YoyosLifeTimeMultiplier is how long in seconds the yoyo will stay out before automatically returning to the player. 
            // Vanilla values range from 3f (Wood) to 16f (Chik), and defaults to -1f. Leaving as -1 will make the time infinite.
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 2.5f;

            // YoyosMaximumRange is the maximum distance the yoyo sleep away from the player. 
            // Vanilla values range from 130f (Wood) to 400f (Terrarian), and defaults to 200f.
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 240f;

            // YoyosTopSpeed is top speed of the yoyo Projectile.
            // Vanilla values range from 9f (Wood) to 17.5f (Terrarian), and defaults to 10f.
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 14f;
        }

        public override void SetDefaults()
        {
            Projectile.width = 28; // The width of the projectile's hitbox.
            Projectile.height = 28; // The height of the projectile's hitbox.

            Projectile.aiStyle = ProjAIStyleID.Yoyo; // The projectile's ai style. Yoyos use aiStyle 99 (ProjAIStyleID.Yoyo). A lot of yoyo code checks for this aiStyle to work properly.

            Projectile.friendly = true; // Player shot projectile. Does damage to enemies but not to friendly Town NPCs.
            Projectile.DamageType = DamageClass.Summon; // Benefits from melee bonuses. MeleeNoSpeed means the item will not scale with attack speed.
            Projectile.penetrate = -1; // All vanilla yoyos have infinite penetration. The number of enemies the yoyo can hit before being pulled back in is based on YoyosLifeTimeMultiplier.
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        // notes for aiStyle 99: 
        // localAI[0] is used for timing up to YoyosLifeTimeMultiplier
        // localAI[1] can be used freely by specific types
        // ai[0] and ai[1] usually point towards the x and y world coordinate hover point
        // ai[0] is -1f once YoyosLifeTimeMultiplier is reached, when the player is stoned/frozen, when the yoyo is too far away, or the player is no longer clicking the shoot button.
        // ai[0] being negative makes the yoyo move back towards the player
        // Any AI method can be used for dust, spawning projectiles, etc specific to your yoyo.

        public override void PostAI()
        {
            if (Main.rand.NextBool(5))
            {
                //Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Sparkle>()); // Makes the projectile emit dust.
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player owner = Main.player[Projectile.owner];
            owner.AddBuff(BuffID.Swiftness, 180);
            target.AddBuff(BuffType<TrickspinTagDamage>(), 240);
            Vector2 direction = Vector2.Normalize(target.Center - Projectile.Center);
            Vector2 velocity = direction * 18f;
            if (owner.HasBuff(BuffType<MeAndMyKillingMachineBuff>()) && !owner.HasBuff(BuffType<MeAndMyKillingMachineFollowUpCooldown>()))
            {
                owner.AddBuff(BuffType<MeAndMyKillingMachineFollowUpCooldown>(), 60);
                Vector2 position = target.Center + new Vector2(Main.rand.Next(-300, -151), Main.rand.Next(-300, 301));
                if (Main.rand.NextBool())
                {
                    position = target.Center + new Vector2(Main.rand.Next(-300, -151), Main.rand.Next(-300, 301));

                }
                else
                {
                    position = target.Center + new Vector2(Main.rand.Next(150, 301), Main.rand.Next(-300, 301));

                }
                Vector2 heading = target.Center - position;
                heading.Normalize();
                heading *= new Vector2(velocity.X, velocity.Y).Length();
                velocity.X = heading.X;
                velocity.Y = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
                if (owner.whoAmI == Main.myPlayer)
                {
                    Projectile.NewProjectile(owner.GetSource_ItemUse(owner.HeldItem), position.X, position.Y, velocity.X, velocity.Y, ProjectileType<TrickspinBearAttack>(), damageDone, 0, owner.whoAmI, 0f);

                }
            }
            else
            {

            }
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Player owner = Main.player[Projectile.owner];
            if (owner.HasBuff(BuffType<KickStartBuff>()))
            {

                owner.ClearBuff(BuffType<KickStartBuff>());
                modifiers.SourceDamage += 1f;
                float dustAmount = 24f;
                float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
                for (int i = 0; i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(15f, 1f);
                    spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant);
                    int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemDiamond);
                    Main.dust[dust].scale = 1.5f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = target.Center + spinningpoint5;
                    Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
                }
                for (int i = 0; i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(15f, 1f);
                    spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant + MathHelper.ToRadians(90));
                    int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemDiamond);
                    Main.dust[dust].scale = 1.5f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = target.Center + spinningpoint5;
                    Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
                }
            }
            else
            {
                float dustAmount = 12f;
                float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
                for (int i = 0; i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(15f, 1f);
                    spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant);
                    int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemTopaz);
                    Main.dust[dust].scale = 1.5f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = target.Center + spinningpoint5;
                    Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 3f;
                }
                for (int i = 0; i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(15f, 1f);
                    spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant + MathHelper.ToRadians(90));
                    int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemTopaz);
                    Main.dust[dust].scale = 1.5f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = target.Center + spinningpoint5;
                    Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 3f;
                }
            }
            base.ModifyHitNPC(target, ref modifiers);
        }
    }
}