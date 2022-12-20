using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Materials
{
    public class InertShard : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Inert Shard");
			Tooltip.SetDefault("A fragment of the past, devoid of both mana and hue alike" +
                "\nObtained on a planet bleached white" +
				"\nCan be used to craft a unique accessory" +
				"" +
				"");
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

		public override void AddRecipes()
		{
			
		}
	}
}