using StarsAbove.Tiles.GoddessOfMemory;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Placeable.GoddessOfMemory
{
    public class DebugGoddessOfMemoryArenaPhase4 : DebugGoddessOfMemoryArena
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.Diamond;
        protected override int ArenaTileType => ModContent.TileType<GoddessOfMemoryArenaPhase4Tile>();
    }
}
