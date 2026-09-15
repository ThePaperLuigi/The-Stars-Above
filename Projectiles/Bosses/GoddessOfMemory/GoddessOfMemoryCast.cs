using Terraria;
using static StarsAbove.NPCs.AttackLibrary.AttackLibrary;

namespace StarsAbove.Projectiles.Bosses.GoddessOfMemory
{
    public class GoddessOfMemoryCast : GoddessOfMemorySpriteProjectile
    {
        protected override int FrameCount => 8;
        protected override float AnimationSpeed => 3f;

        protected override bool ShouldEndAnimation(NPC boss)
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.active && projectile.ModProjectile is GoddessOfMemorySpin or GoddessOfMemorySpinfade)
                {
                    return true;
                }
            }

            if (Projectile.ai[0] > 0f)
            {
                return Projectile.ai[1] >= Projectile.ai[0];
            }

            return boss.ai[0] != (float)ActionState.Casting;
        }
    }
}
