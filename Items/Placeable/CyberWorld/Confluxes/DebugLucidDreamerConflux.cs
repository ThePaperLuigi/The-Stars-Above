using StarsAbove.Tiles;
using StarsAbove.Tiles.CyberWorld;
using StarsAbove.Tiles.CyberWorld.Confluxes;
using Terraria.ModLoader;

namespace StarsAbove.Items.Placeable.CyberWorld.Confluxes
{
    public class DebugLucidDreamerConflux : ModItem
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
			Item.createTile = ModContent.TileType<ConfluxLucidDreamer>();
            Item.ResearchUnlockCount = 0;

		}

		public override void AddRecipes()
		{
			
		}
	}
}