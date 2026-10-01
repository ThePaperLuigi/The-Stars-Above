using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.GoddessOfMemory
{
    public class BossConstellationAttackStar : ModProjectile
    {
        public const int SpawnInterval = 6;
        public const int ActivationDelay = 30;
        public const int DisappearDelay = 12;
        private const int TrailingFadeDelay = 30;
        internal const float ExplosionRadius = 120f;

        private float initialRotation;
        private bool connectionDrawn;
        private bool warningShown;
        private float DetonationDelay => Projectile.ai[1];
        private bool IsLastStar => Projectile.ai[2] == 1f;
        private float DisappearTime => (IsLastStar ? DetonationDelay : SpawnInterval + TrailingFadeDelay) + DisappearDelay;
        public Vector2? PreviousStarPosition;

        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 2;
            Projectile.netImportant = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                initialRotation = Main.rand.NextFloat(-MathHelper.Pi, MathHelper.Pi);
            }

            UpdateSpawnAnimation();
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(initialRotation);
            writer.Write(PreviousStarPosition.HasValue);
            if (PreviousStarPosition.HasValue)
            {
                writer.Write(PreviousStarPosition.Value.X);
                writer.Write(PreviousStarPosition.Value.Y);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            initialRotation = reader.ReadSingle();
            PreviousStarPosition = reader.ReadBoolean() ? new Vector2(reader.ReadSingle(), reader.ReadSingle()) : null;
            UpdateSpawnAnimation();
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override bool? CanDamage()
        {
            return IsLastStar && Projectile.ai[0] == DetonationDelay;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (CanDamage() != true)
            {
                return false;
            }

            Vector2 closestPoint = targetHitbox.ClosestPointInRect(Projectile.Center);
            return Vector2.DistanceSquared(Projectile.Center, closestPoint) <= ExplosionRadius * ExplosionRadius;
        }

        public override void AI()
        {
            Projectile.timeLeft = 2;
            Projectile.ai[0]++;
            UpdateSpawnAnimation();

            if (!Main.dedServ)
            {
                if (!connectionDrawn && PreviousStarPosition.HasValue)
                {
                    Vector2 previousPosition = PreviousStarPosition.Value;
                    int dustCount = Math.Max(1, (int)Math.Ceiling(Vector2.Distance(previousPosition, Projectile.Center) / 4f));
                    for (int i = 0; i <= dustCount; i++)
                    {
                        Vector2 position = Vector2.Lerp(previousPosition, Projectile.Center, i / (float)dustCount);
                        Dust dust = Dust.NewDustPerfect(position, DustID.GemDiamond, Vector2.Zero, 0, Color.White, 1.2f);
                        dust.noGravity = true;
                        dust.noLight = false;
                    }

                    connectionDrawn = true;
                }

                if (IsLastStar && Projectile.ai[0] < DetonationDelay && (!warningShown || (Projectile.ai[0] - 1f) % 10f == 0f))
                {
                    for (int i = 0; i < 96; i++)
                    {
                        Vector2 direction = (MathHelper.TwoPi * i / 96f).ToRotationVector2();
                        Dust dust = Dust.NewDustPerfect(Projectile.Center + direction * ExplosionRadius, DustID.GemRuby, Vector2.Zero, 0, Color.White, 0.7f);
                        dust.noGravity = true;
                        dust.noLight = false;
                    }

                    warningShown = true;
                }
            }

            if (IsLastStar && Projectile.ai[0] == DetonationDelay && !Main.dedServ)
            {
                for (int i = 0; i < 48; i++)
                {
                    Vector2 direction = (MathHelper.TwoPi * i / 48f).ToRotationVector2();
                    Dust dust = Dust.NewDustPerfect(Projectile.Center + direction * ExplosionRadius, DustID.GemDiamond, direction * 5f, 0, Color.White, 1.5f);
                    dust.noGravity = true;
                    dust.noLight = false;
                }

                for (int i = 0; i < 60; i++)
                {
                    Vector2 velocity = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(3f, 9f);
                    Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.GemDiamond, velocity, 0, Color.White, 2f);
                    dust.noGravity = true;
                    dust.noLight = false;
                }
            }

            if (Projectile.ai[0] >= DisappearTime)
            {
                Projectile.Kill();
            }
        }

        private void UpdateSpawnAnimation()
        {
            float progress = MathHelper.Clamp(Projectile.ai[0] / ActivationDelay, 0f, 1f);
            float easedProgress = 1f - (1f - progress) * (1f - progress);
            Projectile.scale = MathHelper.Lerp(1.4f, 1f, easedProgress);
            Projectile.rotation = MathHelper.Lerp(initialRotation, 0f, easedProgress);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return IsLastStar ? Color.Red : Color.White * MathHelper.Clamp((DisappearTime - Projectile.ai[0]) / DisappearDelay, 0f, 1f);
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.dedServ || !IsLastStar)
            {
                return;
            }

            for (int i = 0; i < 12; i++)
            {
                Vector2 velocity = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(1f, 3f);
                Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.GemDiamond, velocity, 0, Color.White, 1.2f);
                dust.noGravity = true;
                dust.noLight = false;
            }
        }
    }
}
