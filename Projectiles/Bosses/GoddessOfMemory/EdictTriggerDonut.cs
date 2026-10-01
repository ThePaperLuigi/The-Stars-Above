using Microsoft.Xna.Framework;
using StarsAbove.NPCs.GoddessOfMemory;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static StarsAbove.NPCs.AttackLibrary.AttackLibrary;

namespace StarsAbove.Projectiles.Bosses.GoddessOfMemory
{
    public class EdictTriggerDonut : ModProjectile
    {
        public const int MovementDelay = 30;
        public const float MoveSpeed = 1f;
        public const float TriggerOffset = 200f;
        public const int Height = 1600;
        public const int Lifetime = 3600;

        public int AttackIdentity = -1;

        public int BossIndex => (int)Projectile.ai[0];

        protected virtual bool IsDonut => true;

        public override string Texture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/BossConstellationAttackStar";

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = Height;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.hide = true;
            Projectile.timeLeft = Lifetime;
            Projectile.netImportant = true;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(AttackIdentity);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            AttackIdentity = reader.ReadInt32();
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
            if (BossIndex < 0 || BossIndex >= Main.maxNPCs)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.Kill();
                }

                return;
            }

            NPC boss = Main.npc[BossIndex];
            if (!boss.active || boss.ModNPC is not GoddessOfMemoryBoss || boss.ai[0] == (float)ActionState.Dying)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.Kill();
                }

                return;
            }

            Projectile.velocity = Vector2.Zero;
            if (Projectile.ai[2] < 0f)
            {
                Projectile.ai[2]++;
                return;
            }

            bool reachedDestination = false;
            if (Projectile.ai[2] >= MovementDelay)
            {
                float distance = Projectile.ai[1] - Projectile.Center.X;
                reachedDestination = Math.Abs(distance) <= MoveSpeed;
                Projectile.Center = new Vector2(reachedDestination ? Projectile.ai[1] : Projectile.Center.X + MathHelper.Clamp(distance, -MoveSpeed, MoveSpeed), Projectile.Center.Y);
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                foreach (Projectile projectile in Main.projectile)
                {
                    if (projectile.active && projectile.ModProjectile is EdictMark mark && mark.BossIndex == BossIndex && mark.AttackIdentity == AttackIdentity && Projectile.Hitbox.Intersects(projectile.Hitbox))
                    {
                        mark.Activate(IsDonut);
                    }
                }

                if (reachedDestination)
                {
                    Projectile.Kill();
                    return;
                }

                if (Projectile.ai[2] % 60f == 0f)
                {
                    Projectile.netUpdate = true;
                }
            }

            if (!Main.dedServ && Projectile.ai[2] % 4f == 0f)
            {
                int dustType = IsDonut ? DustID.GemEmerald : DustID.GemRuby;
                for (int offset = 0; offset <= Projectile.height; offset += 20)
                {
                    Vector2 position = new Vector2(Projectile.Center.X, Projectile.Top.Y + offset);
                    Dust dust = Dust.NewDustPerfect(position, dustType, Vector2.Zero, 0, default, 1.2f);
                    dust.noGravity = true;
                    dust.noLight = false;
                }
            }

            Projectile.ai[2]++;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
    }
}
