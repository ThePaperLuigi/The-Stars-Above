using Terraria.DataStructures;

namespace StarsAbove.Projectiles.Bosses.GoddessOfMemory
{
    public class GoddessOfMemoryArenaPhaseFinal : GoddessOfMemoryBackgroundIntermission
    {
        public override string BackgroundTexture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/MemoryPhaseFinal";
        public override string BorderTexture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/GoddessOfMemoryArenaPhase1Border";

        public override string Texture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/GoddessOfMemoryArenaPhaseFinal";

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.ai[0] = 1f;
            Projectile.ai[1] = 0f;
        }
    }
}
