using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Essences
{
    public class EssenceOfTheMoonlitAdepti : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Essence of The Moonlit Adepti");
			/* Tooltip.SetDefault("A gift from your Starfarer" +
				"\nUtilized in the creation of 'Plenilune Gaze'" +
				$""); */

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

		
	}
}