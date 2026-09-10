using Microsoft.Xna.Framework;

namespace StarsAbove.Tiles.GoddessOfMemory
{
    public class GoddessOfMemoryArenaPhase2Tile : GoddessOfMemoryArenaTile
    {
        public override string MaskTexture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/GoddessOfMemoryArenaPhase2";
        public override string BackgroundTexture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/MemoryPhase2BG";
        public override string BorderTexture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/GoddessOfMemoryArenaPhase2Border";
        public override Color MarkerColor => new Color(255, 120, 150);
    }
}
