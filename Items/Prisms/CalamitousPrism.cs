using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;using Terraria.DataStructures;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Prisms
{
	public class CalamitousPrism : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Calamitous Prism");
			Tooltip.SetDefault("[c/FF8C6D:Unique Stellar Prism]" +
				"\nDoes nothing." +
				"");

			ItemID.Sets.ItemNoGravity[Item.type] = false;
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.value = 100;
			Item.rare = ItemRarityID.Red;
			Item.maxStack = 1;
		}

		public override void OnCraft(Recipe recipe)
		{
			
			base.OnCraft(recipe);
		}
		

		public override Color? GetAlpha(Color lightColor) {
			return Color.White;
		}

		public override void AddRecipes()
		{
			
			
		}
	}
}