using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Essences
{
    public class EssenceOfSugar : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Essence of the Phantom");
			/* Tooltip.SetDefault("A gift from your Starfarer" +
				"\nUtilized in the creation of 'Phantom in the Mirror'" +
				$""); */

			ItemID.Sets.ItemNoGravity[Item.type] = false;
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.value = 100;
			Item.rare = ModContent.GetInstance<Systems.StellarRarity>().Type;
			Item.maxStack = 999;
		}

		public override Color? GetAlpha(Color lightColor) {
			return Color.White;
		}

		
	}
}