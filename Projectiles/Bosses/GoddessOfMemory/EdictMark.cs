using Microsoft.Xna.Framework;
using StarsAbove.NPCs.GoddessOfMemory;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static StarsAbove.NPCs.AttackLibrary.AttackLibrary;

namespace StarsAbove.Projectiles.Bosses.GoddessOfMemory
{
    public class EdictMark : ModProjectile
    {
        public const float CircleRadius = 120f;
        public const float OuterRadius = 240f;
        public const int SpawnInterval = 12;
        public const int ActivationDelay = 30;
        public const int DisappearDelay = 12;

        private readonly bool[] circleHitPlayers = new bool[Main.maxPlayers];
        private readonly bool[] donutHitPlayers = new bool[Main.maxPlayers];
        private readonly bool[] warningShown = new bool[2];
        private readonly bool[] explosionShown = new bool[2];
        private float initialRotation;
        private int spawnAnimationAge;

        public int SpawnDelay;
        public int AttackIdentity = -1;
        public int BossIndex => (int)Projectile.ai[0];
        public bool IsSpawned => SpawnDelay <= 0;
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = EdictTriggerDonut.Lifetime;
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
            writer.Write(spawnAnimationAge);
            writer.Write(SpawnDelay);
            writer.Write(AttackIdentity);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            initialRotation = reader.ReadSingle();
            spawnAnimationAge = reader.ReadInt32();
            SpawnDelay = reader.ReadInt32();
            AttackIdentity = reader.ReadInt32();
            UpdateSpawnAnimation();
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public void Activate(bool donut)
        {
            int timerIndex = donut ? 2 : 1;
            if (Main.netMode == NetmodeID.MultiplayerClient || !IsSpawned || Projectile.ai[timerIndex] != 0f)
            {
                return;
            }

            Projectile.ai[timerIndex] = 1f;
            Projectile.netUpdate = true;
        }

        public override bool? CanDamage()
        {
            return IsSpawned && (IsExploding(Projectile.ai[1]) || IsExploding(Projectile.ai[2]));
        }

        public override bool CanHitPlayer(Player target)
        {
            return IsSpawned && target.active && !target.dead && !target.ghost &&
                (IsExploding(Projectile.ai[1]) && !circleHitPlayers[target.whoAmI] && TouchesCircle(target.Hitbox) ||
                IsExploding(Projectile.ai[2]) && !donutHitPlayers[target.whoAmI] && TouchesDonut(target.Hitbox));
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox = new Rectangle((int)(Projectile.Center.X - OuterRadius), (int)(Projectile.Center.Y - OuterRadius), (int)(OuterRadius * 2f), (int)(OuterRadius * 2f));
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return IsSpawned && (IsExploding(Projectile.ai[1]) && TouchesCircle(targetHitbox) ||
                IsExploding(Projectile.ai[2]) && TouchesDonut(targetHitbox));
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (IsExploding(Projectile.ai[1]) && !circleHitPlayers[target.whoAmI] && TouchesCircle(target.Hitbox))
            {
                circleHitPlayers[target.whoAmI] = true;
            }
            else if (IsExploding(Projectile.ai[2]) && TouchesDonut(target.Hitbox))
            {
                donutHitPlayers[target.whoAmI] = true;
            }
        }

        public override void AI()
        {
            if (BossIndex < 0 || BossIndex >= Main.maxNPCs || !Main.npc[BossIndex].active ||
                Main.npc[BossIndex].ModNPC is not GoddessOfMemoryBoss || Main.npc[BossIndex].ai[0] == (float)ActionState.Dying)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.Kill();
                }

                return;
            }

            if (SpawnDelay > 0)
            {
                SpawnDelay--;
                if (SpawnDelay == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.netUpdate = true;
                }

                return;
            }

            if (spawnAnimationAge < ActivationDelay)
            {
                spawnAnimationAge++;
            }

            UpdateSpawnAnimation();
            for (int i = 1; i <= 2; i++)
            {
                if (Projectile.ai[i] <= 0f || Projectile.ai[i] > ActivationDelay + DisappearDelay)
                {
                    continue;
                }

                Projectile.ai[i]++;
                if (Main.netMode != NetmodeID.MultiplayerClient &&
                    (Projectile.ai[i] == ActivationDelay + 1 || Projectile.ai[i] == ActivationDelay + DisappearDelay + 1))
                {
                    Projectile.netUpdate = true;
                }

                if (!Main.dedServ)
                {
                    UpdateParticles(i == 2, Projectile.ai[i]);
                }
            }

            if (Main.netMode != NetmodeID.MultiplayerClient &&
                Projectile.ai[1] > ActivationDelay + DisappearDelay && Projectile.ai[2] > ActivationDelay + DisappearDelay)
            {
                Projectile.Kill();
            }
        }

        private void UpdateSpawnAnimation()
        {
            float progress = MathHelper.Clamp(spawnAnimationAge / (float)ActivationDelay, 0f, 1f);
            float easedProgress = 1f - (1f - progress) * (1f - progress);
            Projectile.scale = MathHelper.Lerp(1.4f, 1f, easedProgress);
            Projectile.rotation = MathHelper.Lerp(initialRotation, 0f, easedProgress);
        }

        private static bool IsExploding(float timer)
        {
            return timer > ActivationDelay && timer <= ActivationDelay + DisappearDelay;
        }

        private bool TouchesCircle(Rectangle hitbox)
        {
            Vector2 closestPoint = hitbox.ClosestPointInRect(Projectile.Center);
            return Vector2.DistanceSquared(Projectile.Center, closestPoint) <= CircleRadius * CircleRadius;
        }

        private bool TouchesDonut(Rectangle hitbox)
        {
            Vector2 closestPoint = hitbox.ClosestPointInRect(Projectile.Center);
            float farthestX = Math.Max(Math.Abs(hitbox.Left - Projectile.Center.X), Math.Abs(hitbox.Right - Projectile.Center.X));
            float farthestY = Math.Max(Math.Abs(hitbox.Top - Projectile.Center.Y), Math.Abs(hitbox.Bottom - Projectile.Center.Y));
            return Vector2.DistanceSquared(Projectile.Center, closestPoint) <= OuterRadius * OuterRadius &&
                farthestX * farthestX + farthestY * farthestY >= CircleRadius * CircleRadius;
        }

        private void UpdateParticles(bool donut, float timer)
        {
            int effectIndex = donut ? 1 : 0;
            int dustType = donut ? DustID.GemEmerald : DustID.GemRuby;
            if (timer <= ActivationDelay && (!warningShown[effectIndex] || (int)timer % 10 == 0))
            {
                SpawnCircle(CircleRadius, dustType, 48, 0f, 0.8f, 0);
                if (donut)
                {
                    SpawnCircle(OuterRadius, dustType, 80, 0f, 0.8f, 0);
                }

                warningShown[effectIndex] = true;
            }

            if (IsExploding(timer) && !explosionShown[effectIndex])
            {
                SoundEngine.PlaySound(SoundID.DD2_PhantomPhoenixShot with { Volume = 0.2f, Variants = new[] { 0 } }, Projectile.Center);
                SpawnCircle(CircleRadius, dustType, 48, donut ? 0f : 4f, 1.3f, 0);
                if (donut)
                {
                    SpawnCircle(OuterRadius, dustType, 80, 0f, 1.3f, 0);
                    for (int i = 0; i < 96; i++)
                    {
                        Vector2 direction = Main.rand.NextVector2CircularEdge(1f, 1f);
                        float radius = MathF.Sqrt(Main.rand.NextFloat(CircleRadius * CircleRadius, OuterRadius * OuterRadius));
                        Dust dust = Dust.NewDustPerfect(Projectile.Center + direction * radius, dustType, direction.RotatedBy(MathHelper.PiOver2) * 4f, 0, Color.White, 1.3f);
                        dust.noGravity = true;
                        dust.noLight = false;
                    }
                }
                else
                {
                    for (int i = 0; i < 48; i++)
                    {
                        Vector2 velocity = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(3f, 8f);
                        Dust dust = Dust.NewDustPerfect(Projectile.Center, dustType, velocity, 0, Color.White, 1.8f);
                        dust.noGravity = true;
                        dust.noLight = false;
                    }
                }

                explosionShown[effectIndex] = true;
            }

            if (donut && timer <= ActivationDelay + DisappearDelay && (int)timer % 3 == 0)
            {
                for (int i = 0; i < 16; i++)
                {
                    Vector2 direction = (MathHelper.TwoPi * i / 16f + timer * 0.14f).ToRotationVector2();
                    float radius = MathHelper.Lerp(CircleRadius + 16f, OuterRadius - 16f, (i % 3) / 2f);
                    Dust dust = Dust.NewDustPerfect(Projectile.Center + direction * radius, dustType, direction.RotatedBy(MathHelper.PiOver2) * 3f, 0, Color.White, 1f);
                    dust.noGravity = true;
                    dust.noLight = false;
                }
            }
        }

        private void SpawnCircle(float radius, int dustType, int dustCount, float speed, float scale, int alpha)
        {
            for (int i = 0; i < dustCount; i++)
            {
                Vector2 direction = (MathHelper.TwoPi * i / dustCount).ToRotationVector2();
                Dust dust = Dust.NewDustPerfect(Projectile.Center + direction * radius, dustType, direction * speed, alpha, Color.White, scale);
                dust.noGravity = true;
                dust.noLight = false;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            bool circleActive = Projectile.ai[1] > 0f && Projectile.ai[1] <= ActivationDelay + DisappearDelay;
            bool donutActive = Projectile.ai[2] > 0f && Projectile.ai[2] <= ActivationDelay + DisappearDelay;
            return circleActive ? donutActive ? Color.Yellow : Color.Red : donutActive ? Color.LimeGreen : Color.White;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return IsSpawned && (Projectile.ai[1] <= ActivationDelay || Projectile.ai[2] <= ActivationDelay);
        }
    }
}
