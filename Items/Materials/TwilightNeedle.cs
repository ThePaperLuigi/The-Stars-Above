using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Materials
{
    public class TwilightNeedle : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Twilight Needle");
			Tooltip.SetDefault("Utilized to craft Starfarer Attire" +
                "\n'A mystical implement, oscillating in your hands'" +
				"");

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
				.AddIngredient(ItemID.Prismite, 1)
				.AddIngredient(ItemID.UnicornHorn, 1)
				.AddIngredient(ItemID.ButterflyDust, 3)
				.AddIngredient(ItemID.BlackFairyDust, 2)
				.AddIngredient(ItemID.SoulofLight, 8)
				.AddIngredient(ItemID.SoulofNight, 8)
				.AddIngredient(ItemID.DarkShard, 1)
				.AddIngredient(ItemID.LightShard, 1)
				.AddTile(TileID.Anvils)
				.Register();

		}

	}
}