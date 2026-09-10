using Microsoft.Xna.Framework;

namespace StarsAbove.Tiles.GoddessOfMemory
{
    public class GoddessOfMemoryArenaPhase3Tile : GoddessOfMemoryArenaTile
    {
        public override string MaskTexture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/GoddessOfMemoryArenaPhase3";
        public override string BackgroundTexture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/MemoryPhase3BG";
        public override string BorderTexture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/GoddessOfMemoryArenaPhase3Border";
        public override Color MarkerColor => new Color(130, 240, 180);
    }
}
