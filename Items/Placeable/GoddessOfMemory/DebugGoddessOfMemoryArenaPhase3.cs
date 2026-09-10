using StarsAbove.Tiles.GoddessOfMemory;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Placeable.GoddessOfMemory
{
    public class DebugGoddessOfMemoryArenaPhase3 : DebugGoddessOfMemoryArena
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.Emerald;
        protected override int ArenaTileType => ModContent.TileType<GoddessOfMemoryArenaPhase3Tile>();
    }
}
