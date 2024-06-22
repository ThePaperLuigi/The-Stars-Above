
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.StellarNovas;
using StarsAbove.NPCs.Tsukiyomi;
using StarsAbove.Projectiles.Melee.ShadowlessCerulean;
using StarsAbove.Projectiles.Melee.Umbra;
using StarsAbove.Systems;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.StellarNovas.FireflyTypeIV
{
    public class FireflySkill : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 255;
            Projectile.timeLeft = 100;
            Projectile.hide = false;
            Projectile.ownerHitCheck = false;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.light = 1f;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        bool targetWasHit = false;
        Vector2 targetPosition = Vector2.Zero;
        Vector2 savedVelocity = Vector2.Zero;
        public override bool PreDraw(ref Color lightColor)
        {
            if(Projectile.alpha < 100)
            default(Effects.FireflyVFX).Draw(Projectile);

            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

            Player player = Main.player[Projectile.owner];
            savedVelocity = Projectile.velocity;
            targetWasHit = true;
            targetPosition = target.Center;
            Projectile.friendly = false;
            Projectile.alpha = 255;
            if (Main.myPlayer == player.whoAmI)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), targetPosition, Vector2.Zero, ModContent.ProjectileType<FireflySkillCombo>(), Projectile.damage, 0f, Main.myPlayer);

            }
            
            Projectile.velocity = Vector2.Zero;
            Projectile.ai[2] = 1;
            for (int d = 0; d < 20; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(1));
                float scale = -0.1f + Main.rand.NextFloat() * 0.3f;
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(target.Center, 0, 0, DustID.FireworkFountain_Green, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.8f);
                Main.dust[dustIndex].noGravity = true;

            }
            for (int d = 0; d < 20; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(1));
                float scale = -0.1f + Main.rand.NextFloat() * 0.3f;
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(target.Center, 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.8f);
                Main.dust[dustIndex].noGravity = true;

            }
            for (int d = 0; d < 20; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(3));
                float scale = 0.3f + Main.rand.NextFloat() * 0.3f;
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Green, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                Main.dust[dustIndex].noGravity = true;

            }
            for (int d = 0; d < 20; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(5));
                float scale = 0.4f + Main.rand.NextFloat() * 0.3f;
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                Main.dust[dustIndex].noGravity = true;

            }

        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width /= 2;
           
            base.ModifyDamageHitbox(ref hitbox);
        }
        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = (float)(Projectile.velocity.ToRotation() + (Projectile.spriteDirection == 1 ? 0f : Math.PI));
            int drawOffset = 0;
            if (Projectile.velocity.X < 0)
            {
                Projectile.spriteDirection = -1;
                DrawOffsetX = -80;
                drawOffset = -160;
            }
            else
            {
                DrawOffsetX = -120;
                drawOffset = 160;
            }
            
            Projectile.ai[0]++;
            if (Projectile.ai[2] != 0)
            {
                Projectile.ai[1]++;

            }
            else
            {
                Projectile.alpha -= 20;
            }

            DrawOriginOffsetY = -110;

            if (Projectile.ai[1] >= 60 && !Projectile.friendly)
            {

                //Attack finished. Resume velocity and go offscreen
                if(Projectile.frame == 0)
                {
                    Projectile.frame++;
                    Projectile.velocity = savedVelocity;

                }
                Projectile.alpha = 0;

                if (Main.player[Projectile.owner].HasBuff(BuffType<FireflyActive>()))
                {
                    Projectile.velocity *= 0.92f;

                }

            }
            if (Projectile.ai[0] == 7)
            {
                int dustAmount = 40;
                for (int i = 0; (float)i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 32f);
                    spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                    int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemEmerald);
                    Main.dust[dust].scale = 2f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = Projectile.Center + spinningpoint5;
                    Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 8f;
                }
            }
            if (Projectile.ai[0] == 9)
            {
                int dustAmount = 40;
                for (int i = 0; (float)i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 32f);
                    spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                    int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemEmerald);
                    Main.dust[dust].scale = 2f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = Projectile.Center + spinningpoint5;
                    Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 7f;
                }
            }
            if (Projectile.ai[0] == 10)
            {
                int dustAmount = 40;

                for (int i = 0; (float)i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 32f);
                    spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                    int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemTopaz);
                    Main.dust[dust].scale = 2f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = Projectile.Center + spinningpoint5;
                    Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 5f;
                }
            }
            if (Projectile.ai[0] == 1)
            {
                for (int g = 0; g < 4; g++)
                {
                    int goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f + drawOffset, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), Projectile.velocity / 20, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f + drawOffset, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), Projectile.velocity / 20, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f + drawOffset, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), Projectile.velocity / 20, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f + drawOffset, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), Projectile.velocity / 20, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                }
                for (int d = 0; d < 20; d++)//Visual effects
                {
                    Vector2 perturbedSpeed = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(24));
                    float scale = -0.1f + Main.rand.NextFloat() * 0.3f;
                    perturbedSpeed = perturbedSpeed * scale;
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X + drawOffset, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Green, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.8f);
                    Main.dust[dustIndex].noGravity = true;

                }
                for (int d = 0; d < 20; d++)//Visual effects
                {
                    Vector2 perturbedSpeed = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(36));
                    float scale = -0.1f + Main.rand.NextFloat() * 0.3f;
                    perturbedSpeed = perturbedSpeed * scale;
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X + drawOffset, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.8f);
                    Main.dust[dustIndex].noGravity = true;

                }
                for (int d = 0; d < 20; d++)//Visual effects
                {
                    Vector2 perturbedSpeed = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(38));
                    float scale = 0.1f + Main.rand.NextFloat() * 0.3f;
                    perturbedSpeed = perturbedSpeed * scale;
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X + drawOffset, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Green, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                    Main.dust[dustIndex].noGravity = true;

                }
                for (int d = 0; d < 20; d++)//Visual effects
                {
                    Vector2 perturbedSpeed = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(37));
                    float scale = 0.1f + Main.rand.NextFloat() * 0.3f;
                    perturbedSpeed = perturbedSpeed * scale;
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X + drawOffset, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Yellow, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                    Main.dust[dustIndex].noGravity = true;

                }
            }

            

        }

    }
}
