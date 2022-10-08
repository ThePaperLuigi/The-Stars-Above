using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Materials
{
    public class WadOfTenebrium : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Banded Tenebrium");
			Tooltip.SetDefault("Sold at a very high price to shops" +
                "\n'Binded sheets of darkened metal'" +
				"");

			ItemID.Sets.ItemNoGravity[Item.type] = false;
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.value = 1000;
			Item.rare = ItemRarityID.Red;
			Item.maxStack = 999;
		}

		public override Color? GetAlpha(Color lightColor) {
			return Color.White;
		}

		
	}
}