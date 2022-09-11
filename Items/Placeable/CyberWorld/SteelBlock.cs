
using Terraria;using Terraria.DataStructures;
using Terraria.ID;
// If you are using c# 6, you can use: "using static Terraria.Localization.GameCulture;" which would mean you could just write "DisplayName.AddTranslation(German, "");"
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Placeable.CyberWorld
{
	public class SteelBlock : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Riveted and bolted");
			

			
		}

		public override void SetDefaults() {
			Item.width = 12;
			Item.height = 12;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = TileType<Tiles.CyberWorld.SteelTile>();
		}

		public override void AddRecipes() {
			
		}

		
	}
}
