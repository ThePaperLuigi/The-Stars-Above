using Microsoft.Xna.Framework;
using StarsAbove.Items.Prisms;
using System.Collections.Generic;
using System.Linq;
using Terraria;using Terraria.DataStructures;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Materials
{
	public class BoltOfTrueStarsilk : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Bolt of True Starsilk");
			Tooltip.SetDefault("Utilized to craft Starfarer Attire" +
				"\n'Shimmers with cosmic energy'");

			ItemID.Sets.ItemNoGravity[Item.type] = false;
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.value = 350;
			Item.rare = ItemRarityID.Red;
			Item.maxStack = 999;
		}

		public override Color? GetAlpha(Color lightColor) {
			return Color.White;
		}

		public override void AddRecipes()
		{

			CreateRecipe(1)
				.AddIngredient(ItemType<BoltOfStarsilk>(), 1)
				.AddIngredient(ItemID.LunarBar, 3)
				.AddIngredient(ItemType<PrismaticCore>(), 2)
				.AddTile(TileID.Loom)
				.Register();

		}
	}
}