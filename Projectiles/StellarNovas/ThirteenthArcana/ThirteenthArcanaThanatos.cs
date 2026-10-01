using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Systems;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.StellarNovas.ThirteenthArcana
{
    public class ThirteenthArcanaThanatos : ModProjectile
    {
        public const int TravelDuration = 24;
        public const int AnimationStart = 6;
        public const int AnimationFrameTime = 6;
        public const int AnimationFrames = 5;
        public const int BurstTime = AnimationStart + (AnimationFrames - 1) * AnimationFrameTime - 1;
        public const int HoldDuration = BurstTime - TravelDuration;
        public const int RingCount = 5;
        public const int RingInterval = 12;
        public const int FinalRingDelay = 3;
        public const int FadeOutStart = BurstTime + (RingCount - 1) * RingInterval + 4;
        public const float BurstRadius = 1000f;
        public const int Duration = FadeOutStart + 24;

        private bool damageActive;
        private readonly Dictionary<int, NPC> executionTargets = new Dictionary<int, NPC>();
        private readonly Dictionary<int, int> executionRoots = new Dictionary<int, int>();
        private readonly HashSet<int> hitExecutionTargets = new HashSet<int>();
        private readonly List<(float Time, int Frame)> spritePulses = new List<(float Time, int Frame)>();

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = AnimationFrames;
        }

        public override void SetDefaults()
        {
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.aiStyle = -1;
            Projectile.alpha = 255;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = Duration;
            Projectile.netImportant = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override bool? CanDamage()
        {
            return Projectile.ai[1] != 1f && damageActive;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return target.CanBeChasedBy(Projectile) && !target.townNPC && !NPCID.Sets.CountsAsCritter[target.type];
        }

        public override bool CanHitPvp(Player target)
        {
            return false;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox = new Rectangle((int)(Projectile.Center.X - BurstRadius), (int)(Projectile.Center.Y - BurstRadius), (int)(BurstRadius * 2f), (int)(BurstRadius * 2f));
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return targetHitbox.Distance(Projectile.Center) <= BurstRadius;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!damageActive || Projectile.localAI[0] != RingCount - 1 || IsBoss(target))
            {
                return;
            }

            NPC root = target.realLife >= 0 && target.realLife < Main.maxNPCs ? Main.npc[target.realLife] : target;
            if (IsBoss(root))
            {
                return;
            }

            executionTargets[root.whoAmI] = root;
            executionRoots[target.whoAmI] = root.whoAmI;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (damageActive && executionRoots.TryGetValue(target.whoAmI, out int root))
            {
                hitExecutionTargets.Add(root);
            }
        }

        public override bool? CanCutTiles()
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

            Projectile.direction = player.direction;
            Projectile.spriteDirection = Projectile.direction;
            bool revival = Projectile.ai[1] == 1f;
            int ringCount = revival ? 1 : RingCount;
            int fadeOutStart = revival ? BurstTime + 4 : FadeOutStart;
            int duration = fadeOutStart + 24;

            if (Projectile.ai[0] >= fadeOutStart)
            {
                Projectile.frame = Math.Max(0, Main.projFrames[Type] - 2 - (int)((Projectile.ai[0] - fadeOutStart) / AnimationFrameTime));
            }
            else if (Projectile.ai[0] >= AnimationStart && Projectile.frame < Main.projFrames[Type] - 1 && ++Projectile.frameCounter >= AnimationFrameTime)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
            }

            float remainingTime = TravelDuration - Projectile.ai[0];
            if (remainingTime > 0f)
            {
                Projectile.velocity *= (remainingTime - 1f) / remainingTime;
            }
            else
            {
                Projectile.velocity = Vector2.Zero;
            }

            Projectile.rotation = Projectile.velocity.X * 0.004f;
            float fadeIn = Utils.GetLerpValue(0f, 12f, Projectile.ai[0], true);
            float fadeOut = Utils.GetLerpValue(fadeOutStart, duration, Projectile.ai[0], true);
            Projectile.alpha = (int)(255f * (1f - fadeIn * (1f - fadeOut)));

            if (!Main.dedServ)
            {
                Lighting.AddLight(Projectile.Center, new Vector3(0.15f, 0.35f, 0.7f) * Projectile.Opacity);
                if (Projectile.ai[0] % 2f == 0f)
                {
                    DrawSapphireSwirl(Projectile.Center, 195f, Projectile.ai[0] * 0.25f, Projectile.Opacity, 16);
                }

            }

            if (Projectile.frame == Main.projFrames[Type] - 1 && Projectile.localAI[0] < ringCount && Projectile.ai[0] >= BurstTime + Projectile.localAI[0] * RingInterval)
            {
                bool finalRing = Projectile.localAI[0] == ringCount - 1;
                if (!Main.dedServ)
                {
                    DrawSapphireRing(revival ? RingCount - 1 : Projectile.localAI[0]);
                    if (Main.myPlayer == Projectile.owner)
                    {
                        StarsAbovePlayer modPlayer = player.GetModPlayer<StarsAbovePlayer>();
                        modPlayer.screenShakeTimerGlobal = Math.Max(modPlayer.screenShakeTimerGlobal, -94);
                    }
                    DrawSapphireSwirl(Projectile.Center, 300f, Main.rand.NextFloat(MathHelper.TwoPi));
                    if (!revival)
                    {
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            NPC target = Main.npc[i];
                            if (CanHitNPC(target) != false && target.Hitbox.Distance(Projectile.Center) <= BurstRadius)
                            {
                                DrawTargetEffect(target);
                            }
                        }
                    }
                }

                if (revival && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    StarsAbovePlayer.ReviveWithThirteenthArcana();
                }

                if (!revival && Main.myPlayer == Projectile.owner)
                {
                    Array.Clear(Projectile.localNPCImmunity, 0, Projectile.localNPCImmunity.Length);
                    damageActive = true;
                    Projectile.friendly = true;
                    Projectile.Damage();
                    Projectile.friendly = false;
                    damageActive = false;

                    if (finalRing)
                    {
                        int executions = 0;
                        foreach (int root in hitExecutionTargets)
                        {
                            NPC target = executionTargets[root];
                            if (IsBoss(target))
                            {
                                continue;
                            }

                            if (target.active && target.life > 0)
                            {
                                player.StrikeNPCDirect(target, new NPC.HitInfo { InstantKill = true });
                            }

                            if (!target.active)
                            {
                                executions++;
                            }
                        }

                        if (executions > 0)
                        {
                            StarsAbovePlayer modPlayer = player.GetModPlayer<StarsAbovePlayer>();
                            modPlayer.novaGauge = Math.Min(modPlayer.trueNovaGaugeMax, modPlayer.novaGauge + executions * 10f);
                            modPlayer.novaGaugeChangeAlpha = 1f;
                            modPlayer.novaGaugeChangeAlphaSlow = 1f;
                        }
                    }
                }
                Projectile.localAI[0]++;
            }

            if (!Main.dedServ && Projectile.localAI[0] == ringCount && Projectile.localAI[1] == 0f && Projectile.ai[0] >= BurstTime + (ringCount - 1) * RingInterval + FinalRingDelay)
            {
                Projectile.localAI[1] = 1f;
                DrawSapphireRing(RingCount - 1, true);
                if (Main.myPlayer == Projectile.owner)
                {
                    StarsAbovePlayer modPlayer = player.GetModPlayer<StarsAbovePlayer>();
                    modPlayer.screenShakeTimerGlobal = Math.Max(modPlayer.screenShakeTimerGlobal, -94);
                }
            }

            Projectile.ai[0]++;
            if (Projectile.ai[0] >= duration)
            {
                Projectile.Kill();
            }
        }

        private static bool IsBoss(NPC target)
        {
            return target.boss || NPCID.Sets.ShouldBeCountedAsBoss[target.type]
                || target.type == NPCID.EaterofWorldsHead
                || target.type == NPCID.EaterofWorldsBody
                || target.type == NPCID.EaterofWorldsTail
                || target.type == NPCID.SkeletronHand
                || target.type == NPCID.PrimeCannon
                || target.type == NPCID.PrimeSaw
                || target.type == NPCID.PrimeVice
                || target.type == NPCID.PrimeLaser
                || target.type == NPCID.TheDestroyerBody
                || target.type == NPCID.TheDestroyerTail
                || target.type == NPCID.WallofFleshEye
                || target.type == NPCID.GolemHead
                || target.type == NPCID.GolemHeadFree
                || target.type == NPCID.GolemFistLeft
                || target.type == NPCID.GolemFistRight
                || target.type == NPCID.MoonLordHead
                || target.type == NPCID.MoonLordHand;
        }

        private static void DrawTargetEffect(NPC target)
        {
            float radius = Math.Max(target.width, target.height) * 0.5f + 12f;
            float rotation = Main.rand.NextFloat(MathHelper.TwoPi);
            for (int i = 0; i < 48; i++)
            {
                Vector2 direction = Vector2.UnitX.RotatedBy(i * MathHelper.TwoPi / 48f);
                Vector2 ovalOffset = (direction * new Vector2(radius, radius * 0.35f)).RotatedBy(rotation);
                Dust oval = Dust.NewDustPerfect(target.Center + ovalOffset, DustID.GemSapphire, new Vector2(0f, -4f).RotatedBy(rotation), 30, default, 1.25f);
                oval.noGravity = true;
                Dust circle = Dust.NewDustPerfect(target.Center + direction * (radius + 12f), DustID.GemSapphire, direction * 9f, 30, default, 1.4f);
                circle.noGravity = true;
            }

            for (int i = 0; i < 24; i++)
            {
                Vector2 velocity = new Vector2(0f, -1f).RotatedBy(rotation).RotatedByRandom(MathHelper.ToRadians(25f)) * Main.rand.NextFloat(4f, 12f);
                Dust firework = Dust.NewDustPerfect(target.Center + Main.rand.NextVector2Circular(4f, 4f), DustID.Firework_Blue, velocity, 50, default, Main.rand.NextFloat(0.85f, 1.65f));
                firework.noGravity = true;
            }
        }

        private void DrawSapphireRing(float pulse, bool secondary = false)
        {
            spritePulses.Add((Projectile.ai[0], Projectile.frame));
            SoundEngine.PlaySound(SoundID.DD2_PhantomPhoenixShot with { Variants = new[] { 2 }, MaxInstances = 0 }, Projectile.Center);
            bool finalRing = pulse == RingCount - 1;
            int dustAmount = finalRing ? 128 : 96;
            float rotation = Main.rand.NextFloat(MathHelper.TwoPi);
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 direction = (Vector2.UnitX.RotatedBy(i * MathHelper.TwoPi / dustAmount) * new Vector2(1f, finalRing ? 1f : 0.35f)).RotatedBy(rotation);
                Vector2 position = Projectile.Center + direction * (16f + pulse * 8f);
                Dust dust = Dust.NewDustPerfect(position, DustID.GemSapphire, direction * (52f + pulse * 8f), 30, default, 1.5f + pulse * 0.15f);
                dust.noGravity = true;
            }
            if (secondary)
            {
                return;
            }

            int fireworkAmount = finalRing ? 120 : 40;
            float fireworkRadius = finalRing ? 10f : 6f;
            for (int i = 0; i < fireworkAmount; i++)
            {
                Vector2 velocity = Vector2.UnitX.RotatedBy(Main.rand.NextFloat(MathHelper.TwoPi)) * Main.rand.NextFloat(finalRing ? 6f : 4f, finalRing ? 18f : 12f) * 3f;
                Dust firework = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(fireworkRadius, fireworkRadius), DustID.Firework_Blue, velocity, 50, default, Main.rand.NextFloat(finalRing ? 1.1f : 0.85f, finalRing ? 1.9f : 1.65f));
                firework.noGravity = true;
            }
        }

        public static void DrawSapphireSwirl(Vector2 center, float radius, float rotation, float opacity = 1f, int dustAmount = 64)
        {
            if (Main.dedServ || opacity <= 0f)
            {
                return;
            }

            for (int i = 0; i < dustAmount; i++)
            {
                float progress = i / (float)dustAmount;
                Vector2 offset = Vector2.UnitX.RotatedBy(rotation + progress * MathHelper.TwoPi * 1.5f) * radius * MathHelper.Lerp(0.35f, 1f, progress);
                Vector2 velocity = offset.RotatedBy(MathHelper.PiOver2).SafeNormalize(Vector2.UnitY) * 2.5f - offset * 0.045f;
                Dust dust = Dust.NewDustPerfect(center + offset, DustID.GemSapphire, velocity, (int)MathHelper.Lerp(220f, 40f, opacity), default, Main.rand.NextFloat(0.9f, 1.25f) * opacity);
                dust.fadeIn = 0.35f * opacity;
                dust.noGravity = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Type], frameY: Projectile.frame);
            SpriteEffects spriteEffects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            foreach (var pulse in spritePulses)
            {
                float progress = Utils.GetLerpValue(0f, 18f, Projectile.ai[0] - pulse.Time, true);
                if (progress >= 1f)
                {
                    continue;
                }

                float opacity = (1f - progress) * (1f - progress);
                float scale = Projectile.scale * MathHelper.Lerp(1f, 3f, 1f - opacity);
                Rectangle pulseRectangle = texture.Frame(1, Main.projFrames[Type], frameY: pulse.Frame);
                Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, pulseRectangle, Color.White * (0.6f * opacity), Projectile.rotation, pulseRectangle.Size() / 2f, scale, spriteEffects, 0);
            }
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, sourceRectangle, Color.White * Projectile.Opacity, Projectile.rotation, sourceRectangle.Size() / 2f, Projectile.scale, spriteEffects, 0);
            return false;
        }
    }
}
