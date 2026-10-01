using Microsoft.Xna.Framework;
using StarsAbove.NPCs.GoddessOfMemory;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static StarsAbove.NPCs.AttackLibrary.AttackLibrary;

namespace StarsAbove.Projectiles.Bosses.GoddessOfMemory
{
    public class BossHammerOfRecognitionMark : ModProjectile
    {
        public const int ThrowInterval = 3;
        private const float MarkerRadius = 50f;

        public int BossIndex => (int)Projectile.ai[0];

        public override string Texture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/HammerOfRecognition";

        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.hide = true;
            Projectile.timeLeft = 2;
            Projectile.netImportant = true;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override void AI()
        {
            Projectile.timeLeft = 2;
            if (BossIndex < 0 || BossIndex >= Main.maxNPCs)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.Kill();
                }

                return;
            }

            NPC boss = Main.npc[BossIndex];
            if (!boss.active || boss.ModNPC is not GoddessOfMemoryBoss || boss.ai[0] == (float)ActionState.Dying ||
                Projectile.ai[1] == 0f && boss.ai[0] != (float)ActionState.Casting)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.Kill();
                }

                return;
            }

            if (Projectile.ai[1] > 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.ai[1]--;
                if (Projectile.ai[1] == 0f)
                {
                    Launch((int)Projectile.ai[2]);
                    return;
                }
            }

            Projectile.localAI[0]++;
            if (Main.dedServ || (Projectile.localAI[0] - 1f) % 10f != 0f)
            {
                return;
            }

            int dustCount = Math.Max(1, (int)Math.Ceiling(Vector2.Distance(boss.Center, Projectile.Center) / 8f));
            for (int i = 0; i <= dustCount; i++)
            {
                Vector2 position = Vector2.Lerp(boss.Center, Projectile.Center, i / (float)dustCount);
                Dust dust = Dust.NewDustPerfect(position, DustID.GemRuby, Vector2.Zero, 0, Color.White, 0.7f);
                dust.noGravity = true;
                dust.noLight = false;
            }

            for (int i = 0; i < 40; i++)
            {
                Vector2 position = Projectile.Center + (MathHelper.TwoPi * i / 40f).ToRotationVector2() * MarkerRadius;
                Dust dust = Dust.NewDustPerfect(position, DustID.GemRuby, Vector2.Zero, 0, Color.White, 0.7f);
                dust.noGravity = true;
                dust.noLight = false;
            }
        }

        public void QueueLaunch(int damage, int delay)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient || !Projectile.active || Projectile.ai[1] > 0f)
            {
                return;
            }

            Projectile.ai[1] = delay + 1f;
            Projectile.ai[2] = damage;
            Projectile.netUpdate = true;
        }

        private void Launch(int damage)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient || !Projectile.active)
            {
                return;
            }

            if (BossIndex < 0 || BossIndex >= Main.maxNPCs)
            {
                Projectile.Kill();
                return;
            }

            NPC boss = Main.npc[BossIndex];
            if (!boss.active || boss.ModNPC is not GoddessOfMemoryBoss goddessOfMemory || boss.ai[0] == (float)ActionState.Dying)
            {
                Projectile.Kill();
                return;
            }

            int projectileIndex = Projectile.NewProjectile(Projectile.GetSource_FromAI(), boss.Center, Vector2.Zero,
                ModContent.ProjectileType<BossHammerOfRecognition>(), damage, 0f, Main.myPlayer, BossIndex, Projectile.Center.X, Projectile.Center.Y);
            if (projectileIndex >= 0 && projectileIndex < Main.maxProjectiles)
            {
                goddessOfMemory.RegisterHammerOfRecognition(Main.projectile[projectileIndex].identity);
            }

            Projectile.Kill();
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
    }
}
