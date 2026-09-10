using StarsAbove.Tiles.GoddessOfMemory;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Placeable.GoddessOfMemory
{
    public class DebugGoddessOfMemoryArenaPhase1 : DebugGoddessOfMemoryArena
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.Sapphire;
        protected override int ArenaTileType => ModContent.TileType<GoddessOfMemoryArenaPhase1Tile>();
    }
}
