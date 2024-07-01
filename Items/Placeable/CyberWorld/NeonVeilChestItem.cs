using StarsAbove.Tiles.CyberWorld;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Placeable.CyberWorld
{
	public class NeonVeilChestItem : ModItem
	{
		public override void SetDefaults() {
			Item.DefaultToPlaceableTile(ModContent.TileType<NeonVeilChest>());
			// Item.placeStyle = 1; // Use this to place the chest in its locked style
			Item.width = 26;
			Item.height = 22;
			Item.value = 500;
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
			
		}
	}

	public class NeonVeilChestKey : ModItem
	{
		public override void SetStaticDefaults() {
			Item.ResearchUnlockCount = 3; // Biome keys usually take 1 item to research instead.
		}

		public override void SetDefaults() {
			Item.CloneDefaults(ItemID.GoldenKey);
		}
	}
}
