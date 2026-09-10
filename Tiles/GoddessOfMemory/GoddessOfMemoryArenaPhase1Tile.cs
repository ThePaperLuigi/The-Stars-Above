using Microsoft.Xna.Framework;

namespace StarsAbove.Tiles.GoddessOfMemory
{
    public class GoddessOfMemoryArenaPhase1Tile : GoddessOfMemoryArenaTile
    {
        public override string MaskTexture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/GoddessOfMemoryArenaPhase1";
        public override string BackgroundTexture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/MemoryPhase1BG";
        public override string BorderTexture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/GoddessOfMemoryArenaPhase1Border";
        public override Color MarkerColor => new Color(120, 190, 255);
    }
}
