using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Systems;
using StarsAbove.Utilities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.StellarNovas.ThirteenthArcana
{
    public class ThirteenthArcanaEvoker : ModProjectile
    {
        private const int RaiseDuration = 42;
        private const int SkyHoldDuration = 12;
        private const int AimDuration = 13;
        private const int FireTime = RaiseDuration + SkyHoldDuration + AimDuration;
        private const int RecoilDuration = 14;
        private const int Duration = FireTime + 30;
        private static readonly Vector2 GripOrigin = new Vector2(26f, 11f);
        private static readonly Vector2 MuzzleOffset = new Vector2(2f, 20f) - GripOrigin;

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 22;
            Projectile.aiStyle = -1;
            Projectile.damage = 0;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = Duration;
            Projectile.scale = 0.65f;
            Projectile.hide = true;
            Projectile.netImportant = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead)
            {
                Projectile.Kill();
                return;
            }

            if (!Main.dedServ)
            {
                if (player.velocity.Y == 0)
                {
                    for (int i3 = 0; i3 < 25; i3++)
                    {
                        Dust d = Main.dust[Dust.NewDust(new Vector2(player.Center.X - player.width, player.Center.Y + player.height / 2 * player.gravDir), player.width * 2 - 3, 0, DustID.FireworkFountain_Blue, 0, Main.rand.Next(-5, -2) * player.gravDir, 150, default, 0.3f)];
                        d.velocity *= 0.1f;
                        d.fadeIn = 0.3f;
                        d.noGravity = true;
                    }
                }
                else
                {
                    int dustAmount = Projectile.ai[0] % 2f == 0f ? 2 : 3;
                    for (int i2 = 0; i2 < dustAmount; i2++)
                    {//Circle
                        Vector2 offset = new Vector2();
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        offset.X += (float)(Math.Sin(angle) * 30f);
                        offset.Y += (float)(Math.Cos(angle) * 30f);

                        Dust d2 = Dust.NewDustPerfect(player.Center + offset, DustID.FireworkFountain_Blue, player.velocity, 200, default, 0.7f);
                        d2.fadeIn = 0.0001f;
                        d2.noGravity = true;
                    }
                }
            }

            float progress = EaseHelper.OutQuad(MathHelper.Clamp(Projectile.ai[0] / RaiseDuration, 0f, 1f));
            float aimProgress = EaseHelper.OutQuad(MathHelper.Clamp((Projectile.ai[0] - RaiseDuration - SkyHoldDuration) / AimDuration, 0f, 1f));
            float raisedArmRotation = -MathHelper.ToRadians(170f) * player.direction;
            float armRotation = MathHelper.Lerp(raisedArmRotation * progress, -MathHelper.PiOver2 * player.direction, aimProgress);
            player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, armRotation);

            Vector2 handPosition = player.GetBackHandPosition(Player.CompositeArmStretchAmount.Full, armRotation);
            handPosition.Y = player.MountedCenter.Y + (handPosition.Y - player.MountedCenter.Y) * player.gravDir;
            Projectile.Center = player.RotatedRelativePoint(handPosition);
            Projectile.direction = player.direction;
            Projectile.spriteDirection = player.direction;

            Vector2 headPosition = player.RotatedRelativePoint(player.MountedCenter + new Vector2(0f, -14f * player.gravDir));
            Vector2 finalHandPosition = player.GetBackHandPosition(Player.CompositeArmStretchAmount.Full, -MathHelper.PiOver2 * player.direction);
            finalHandPosition.Y = player.MountedCenter.Y + (finalHandPosition.Y - player.MountedCenter.Y) * player.gravDir;
            finalHandPosition = player.RotatedRelativePoint(finalHandPosition);

            Vector2 mirror = new Vector2(player.direction, player.gravDir);
            Vector2 barrelDirection = new Vector2(-1f, 1f).SafeNormalize(Vector2.UnitX) * mirror;
            Vector2 muzzleOffset = MuzzleOffset * mirror * Projectile.scale;
            Vector2 toHead = headPosition - finalHandPosition;
            float lateralOffset = barrelDirection.X * muzzleOffset.Y - barrelDirection.Y * muzzleOffset.X;
            float aimRotation = toHead.ToRotation() - (float)Math.Asin(MathHelper.Clamp(lateralOffset / toHead.Length(), -1f, 1f)) - barrelDirection.ToRotation();
            Vector2 skyDirection = new Vector2(0f, -player.gravDir).RotatedBy(player.fullRotation);
            float skyRotation = skyDirection.ToRotation() - barrelDirection.ToRotation();
            float spinDirection = player.direction * player.gravDir;
            float rotationOffset = MathHelper.WrapAngle(skyRotation * spinDirection);
            if (rotationOffset < 0f)
            {
                rotationOffset += MathHelper.TwoPi;
            }
            float spinRotation = (MathHelper.TwoPi * 2f + rotationOffset) * spinDirection;
            Projectile.rotation = MathHelper.Lerp(spinRotation * progress, spinRotation + MathHelper.WrapAngle(aimRotation - spinRotation), aimProgress);
            Vector2 burstDirection = barrelDirection.RotatedBy(Projectile.rotation);
            Vector2 burstPosition = headPosition + burstDirection * 10f;
            if (Projectile.ai[0] >= FireTime)
            {
                float recoilProgress = MathHelper.Clamp((Projectile.ai[0] - FireTime) / RecoilDuration, 0f, 1f);
                Projectile.rotation += MathHelper.ToRadians(28f) * spinDirection * (1f - EaseHelper.OutQuad(recoilProgress));
            }
            Projectile.alpha = (int)(255f * Utils.GetLerpValue(FireTime + 10, Duration, Projectile.ai[0], true));

            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                if (!Main.dedServ)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.GemSapphire, Main.rand.NextVector2Circular(1.5f, 1.5f), 100, default, 0.6f);
                        dust.noGravity = true;
                    }
                }
            }

            if (Projectile.ai[0] >= FireTime && Projectile.localAI[1] == 0f)
            {
                Projectile.localAI[1] = 1f;
                if (!Main.dedServ)
                {
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_summoning, headPosition);
                    if (Main.myPlayer == Projectile.owner)
                    {
                        StarsAbovePlayer modPlayer = player.GetModPlayer<StarsAbovePlayer>();
                        modPlayer.screenShakeTimerGlobal = Math.Max(modPlayer.screenShakeTimerGlobal, -90);
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), burstPosition, burstDirection * 8.32f, ModContent.ProjectileType<ThirteenthArcanaThanatos>(), Math.Max(1, modPlayer.novaDamage), 0f, Projectile.owner);
                    }
                    for (int i = 0; i < 80; i++)
                    {
                        Vector2 velocity = burstDirection.RotatedByRandom(MathHelper.ToRadians(45f)) * Main.rand.NextFloat(2f, 8f);
                        Dust dust = Dust.NewDustPerfect(burstPosition + Main.rand.NextVector2Circular(3f, 3f), DustID.GemSapphire, velocity, 50, default, Main.rand.NextFloat(0.85f, 1.65f));
                        dust.noGravity = true;
                        Vector2 fireworkVelocity = burstDirection.RotatedByRandom(MathHelper.ToRadians(25f)) * Main.rand.NextFloat(3f, 11f);
                        Dust firework = Dust.NewDustPerfect(burstPosition + Main.rand.NextVector2Circular(3f, 3f), DustID.Firework_Blue, fireworkVelocity, 50, default, Main.rand.NextFloat(0.85f, 1.65f));
                        firework.noGravity = true;
                    }
                }
            }

            if (!Main.dedServ && Projectile.localAI[2] < 3f && Projectile.ai[0] >= FireTime + Projectile.localAI[2] * 4f)
            {
                float pulse = Projectile.localAI[2];
                float pulseRotation = Main.rand.NextFloat(MathHelper.TwoPi);
                Vector2 pulsePosition = player.RotatedRelativePoint(player.Center);
                for (int i = 0; i < 48; i++)
                {
                    Vector2 offset = Vector2.UnitX.RotatedBy(i * MathHelper.TwoPi / 48f) * new Vector2(16f + pulse * 5f, 6f + pulse * 2f);
                    offset = offset.RotatedBy(pulseRotation);
                    Vector2 velocity = offset * 0.2f;
                    Dust dust = Dust.NewDustPerfect(pulsePosition + offset, DustID.GemSapphire, velocity, 30, default, 1.1f + pulse * 0.15f);
                    dust.noGravity = true;
                }
                Projectile.localAI[2]++;
            }

            Projectile.ai[0]++;
            if (Projectile.ai[0] >= Duration)
            {
                Projectile.Kill();
            }
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            overPlayers.Add(index);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 origin = GripOrigin;
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                origin.X = texture.Width - origin.X;
                spriteEffects |= SpriteEffects.FlipHorizontally;
            }
            if (Main.player[Projectile.owner].gravDir == -1f)
            {
                origin.Y = texture.Height - origin.Y;
                spriteEffects |= SpriteEffects.FlipVertically;
            }

            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
            return false;
        }
    }
}
