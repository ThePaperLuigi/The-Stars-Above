using StarsAbove.Tiles.GoddessOfMemory;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Placeable.GoddessOfMemory
{
    public class DebugGoddessOfMemoryArenaIntermission : DebugGoddessOfMemoryArena
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.Amethyst;
        protected override int ArenaTileType => ModContent.TileType<GoddessOfMemoryArenaIntermissionTile>();
    }
}
