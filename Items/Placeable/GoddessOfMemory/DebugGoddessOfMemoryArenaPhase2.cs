using StarsAbove.Tiles.GoddessOfMemory;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Placeable.GoddessOfMemory
{
    public class DebugGoddessOfMemoryArenaPhase2 : DebugGoddessOfMemoryArena
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.Ruby;
        protected override int ArenaTileType => ModContent.TileType<GoddessOfMemoryArenaPhase2Tile>();
    }
}
