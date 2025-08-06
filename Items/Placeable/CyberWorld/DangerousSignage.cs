using StarsAbove.Tiles.CyberWorld;
using Terraria.ModLoader;

namespace StarsAbove.Items.Placeable.CyberWorld
{
    public class DangerousSignage : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Foreign Vending Machine");
			// Tooltip.SetDefault("");
			
        }

		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 30;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.value = 150;
			Item.createTile = ModContent.TileType<DangerousSignageTile>();

		}

		public override void AddRecipes()
		{
			
		}
	}
}