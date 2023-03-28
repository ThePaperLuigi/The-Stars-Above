using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Materials
{
    public class FaerieVoyagerAttirePrecursor : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Tattered Fae Attire");
			Tooltip.SetDefault("[c/956BE3:Starfarer Attire Precursor]" +
                "\nThe tattered remains of storied garb from legends past" +
				"\nUtilized to craft 'Attire of the Faerie Voyager'");
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			ItemID.Sets.ItemNoGravity[Item.type] = false;
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.value = 100;
			Item.rare = ItemRarityID.Red;
			Item.maxStack = 1;
		}

		public override Color? GetAlpha(Color lightColor) {
			return Color.White;
		}

		
	}
}