using Terraria;using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace StarsAbove.Items.Accessories
{
	public class Glitterglue : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Teacup of Glitterglue");

			Tooltip.SetDefault("" +
                "Striking foes has a 5% chance to inflict Glitterglue for 4 seconds" +
				"\nAttacks on foes inflicted with Glitterglue have a 30% chance to turn non-critical strikes critical" +
                "\n'Sniffing not recommended'" +
				"");
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//The (English) text shown below your weapon's name
		}

		public override void SetDefaults() {
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.value = Item.sellPrice(silver: 30);
			Item.rare = ItemRarityID.Purple;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<StarsAbovePlayer>().Glitterglue = true;
		}

		

		/*public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LifeCrystal, 2);
			recipe.AddIngredient(ItemID.ManaCrystal, 2);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}*/
	}
}
