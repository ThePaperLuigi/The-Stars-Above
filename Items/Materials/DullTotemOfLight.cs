using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;using Terraria.DataStructures;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Materials
{
	public class DullTotemOfLight : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Totem of Light");
			Tooltip.SetDefault("An effigy supposedly given form to grant strength to the Warrior of Light" +
				"\nUtilized in the creation of artifacts and weapons");
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;

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