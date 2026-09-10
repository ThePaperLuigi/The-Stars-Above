using Microsoft.Xna.Framework;

namespace StarsAbove.Tiles.GoddessOfMemory
{
    public class GoddessOfMemoryArenaPhase4Tile : GoddessOfMemoryArenaTile
    {
        public override string MaskTexture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/GoddessOfMemoryArenaPhase4";
        public override string BackgroundTexture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/MemoryPhase4BG";
        public override string BorderTexture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/GoddessOfMemoryArenaPhase4Border";
        public override Color MarkerColor => new Color(240, 240, 255);
    }
}
