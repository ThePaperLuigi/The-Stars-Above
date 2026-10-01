using Microsoft.Xna.Framework;
using StarsAbove.NPCs.GoddessOfMemory;
using StarsAbove.Utilities;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static StarsAbove.NPCs.AttackLibrary.AttackLibrary;

namespace StarsAbove.Projectiles.Bosses.GoddessOfMemory
{
    public class BossExpungenceMark : ModProjectile
    {
        public int BossIndex = -1;

        public override string Texture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/BossConstellationAttackStar";

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

        public override void OnSpawn(IEntitySource source)
        {
            if (source is EntitySource_Parent parent && parent.Entity is NPC boss && boss.ModNPC is GoddessOfMemoryBoss)
            {
                BossIndex = boss.whoAmI;
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(BossIndex);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            BossIndex = reader.ReadInt32();
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
            int targetIndex = (int)Projectile.ai[0];
            if (targetIndex < 0 || targetIndex >= Main.maxPlayers || BossIndex < 0 || BossIndex >= Main.maxNPCs)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.Kill();
                }

                return;
            }

            Player target = Main.player[targetIndex];
            NPC boss = Main.npc[BossIndex];
            if (!target.active || target.dead || target.ghost || !boss.active || boss.ModNPC is not GoddessOfMemoryBoss || boss.ai[0] != (float)ActionState.Casting)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.Kill();
                }

                return;
            }

            Projectile.Center = target.Center;
            Projectile.localAI[0]++;
            if (Main.dedServ || (Projectile.localAI[0] - 1f) % 10f != 0f)
            {
                return;
            }

            if (Projectile.localAI[0] == 1f)
            {
                Rectangle warningArea = new Rectangle((int)target.Center.X - 20, (int)target.Top.Y - 32, 40, 16);
                CombatText.NewText(warningArea, Color.Red, LangHelper.GetTextValue("BossAttacks.SpreadOutWarning"), true);
            }

            for (int i = 0; i < 96; i++)
            {
                Vector2 direction = (MathHelper.TwoPi * i / 96f).ToRotationVector2();
                Dust dust = Dust.NewDustPerfect(Projectile.Center + direction * BossExpungenceExplosion.ExplosionRadius, DustID.GemRuby, Vector2.Zero, 0, Color.White, 0.7f);
                dust.noGravity = true;
                dust.noLight = false;
            }
        }

        public void Detonate()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient || !Projectile.active)
            {
                return;
            }

            int targetIndex = (int)Projectile.ai[0];
            if (targetIndex >= 0 && targetIndex < Main.maxPlayers)
            {
                Player target = Main.player[targetIndex];
                if (target.active && !target.dead && !target.ghost)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), target.Center, Vector2.Zero, ModContent.ProjectileType<BossExpungenceExplosion>(), BossExpungenceExplosion.ExplosionDamage, 0f, Main.myPlayer, target.whoAmI);
                }
            }

            Projectile.Kill();
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
    }
}
