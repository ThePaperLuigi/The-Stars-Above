using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Materials
{
    public class BoltOfStarsilk : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Bolt of Starsilk");
			Tooltip.SetDefault("Utilized to craft Starfarer Attire" +
				"\n'A texture akin to twinkling stars'");

			ItemID.Sets.ItemNoGravity[Item.type] = false;
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.value = 100;
			Item.rare = ItemRarityID.Red;
			Item.maxStack = 999;
		}

		public override Color? GetAlpha(Color lightColor) {
			return Color.White;
		}

		public override void AddRecipes()
		{

			CreateRecipe(1)
				.AddIngredient(ItemID.AncientCloth, 1)
				.AddIngredient(ItemID.Silk, 1)
				.AddIngredient(ItemID.MeteoriteBar, 2)
				.AddIngredient(ItemID.FallenStar, 3)
				.AddTile(TileID.Loom)
				.Register();

		}
	}
}