using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Materials
{
    public class BandedTenebrium : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Banded Tenebrium");
			/* Tooltip.SetDefault("Utilized to upgrade the Stellaglyph and craft certain Stellar Foci" +
                "" +
                "\n'Cold to the heat'" +
				""); */
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

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