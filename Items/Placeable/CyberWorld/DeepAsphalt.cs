
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Placeable.CyberWorld
{
    public class DeepAsphalt : ModItem
	{
		public override void SetStaticDefaults() {
			// Tooltip.SetDefault("Riveted and bolted");
			

			
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
			Item.createTile = TileType<Tiles.CyberWorld.DeepAsphalt>(); Item.ResearchUnlockCount = 0;

		}

		public override void AddRecipes() {
			
		}

		
	}
}
