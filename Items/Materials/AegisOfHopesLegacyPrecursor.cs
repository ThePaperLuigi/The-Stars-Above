using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Materials
{
    public class AegisOfHopesLegacyPrecursor : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Sundered Hero's Attire");
			Tooltip.SetDefault("[c/956BE3:Starfarer Attire Precursor]" +
                "\nThe tattered remains of storied garb from legends past" +
				"\nUtilized to craft 'Aegis of Hope's Legacy'");
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