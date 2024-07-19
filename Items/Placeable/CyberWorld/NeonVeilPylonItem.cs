
using StarsAbove.Tiles.CyberWorld;
using Terraria.Enums;
using Terraria.ModLoader;

namespace StarsAbove.Items.Placeable.CyberWorld
{
	
	public class NeonVeilPylonItem : ModItem
	{
		public override void SetDefaults() {
			// Basically, this a just a shorthand method that will set all default values necessary to place
			// the passed in tile type; in this case, the Example Pylon tile.
			Item.DefaultToPlaceableTile(ModContent.TileType<NeonVeilPylon>());
			
		}
	}
}
