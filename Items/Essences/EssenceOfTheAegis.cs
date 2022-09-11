using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;using Terraria.DataStructures;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Essences
{
	public class EssenceOfTheAegis : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Essence of the Aegis");
			Tooltip.SetDefault("A gift from your Starfarer" +
				"\nUtilized in the creation of 'Aegis Driver'" +
				$"");

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