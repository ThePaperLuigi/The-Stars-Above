using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Materials
{
    public class EnigmaticDust : ModItem
	{
		public override void SetStaticDefaults() {
			/* Tooltip.SetDefault("The residue of a mysterious individual" +
				"\n" +
				"\n"); */
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;

			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 8));
			ItemID.Sets.ItemNoGravity[Item.type] = true;
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.value = 100;
			Item.rare = 8;
			Item.maxStack = 999;
		}

		public override Color? GetAlpha(Color lightColor) {
			return Color.White;
		}


	}
}