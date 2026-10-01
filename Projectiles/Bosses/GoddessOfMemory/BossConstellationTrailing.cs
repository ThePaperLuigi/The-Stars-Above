using Microsoft.Xna.Framework;
using StarsAbove.NPCs.GoddessOfMemory;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static StarsAbove.NPCs.AttackLibrary.AttackLibrary;

namespace StarsAbove.Projectiles.Bosses.GoddessOfMemory
{
    public class BossConstellationTrailing : ModProjectile
    {
        private Vector2[] path;
        private int castTimer;
        private int starIndex;

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
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            int targetIndex = (int)Projectile.ai[0];
            int bossIndex = (int)Projectile.ai[2];
            if (targetIndex < 0 || targetIndex >= Main.maxPlayers || bossIndex < 0 || bossIndex >= Main.maxNPCs || Projectile.ai[1] <= 0f)
            {
                Projectile.Kill();
                return;
            }

            Player target = Main.player[targetIndex];
            NPC boss = Main.npc[bossIndex];
            if (!target.active || target.dead || !boss.active || boss.ModNPC is not GoddessOfMemoryBoss || boss.ai[0] == (float)ActionState.Dying)
            {
                Projectile.Kill();
                return;
            }

            if (path == null)
            {
                path = CreateConstellationPath(Projectile.Center, target.Center, Main.rand.Next(5, 8), true);
                path[0] = Projectile.Center;
                PlayConstellationSound(Projectile.Center, false);
            }

            int duration = (path.Length - 1) * BossConstellationAttackStar.SpawnInterval + BossConstellationAttackStar.ActivationDelay;
            if (starIndex < path.Length && castTimer >= starIndex * BossConstellationAttackStar.SpawnInterval)
            {
                SpawnConstellationStar(Projectile.GetSource_FromAI(), path, starIndex, Projectile.damage, duration - castTimer);
                starIndex++;
            }

            if (castTimer < duration)
            {
                castTimer++;
                return;
            }

            Vector2 explosionPosition = path[path.Length - 1];
            PlayConstellationSound(explosionPosition, true);
            Projectile.ai[1]--;
            if (Projectile.ai[1] <= 0f)
            {
                Projectile.Kill();
                return;
            }

            Projectile.Center = explosionPosition;
            path = null;
            castTimer = 0;
            starIndex = 0;
            Projectile.netUpdate = true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
    }
}
