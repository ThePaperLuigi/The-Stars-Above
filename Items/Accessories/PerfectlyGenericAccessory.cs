using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Items.Accessories
{
    public class PerfectlyGenericAccessory : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Perfectly Generic Accessory");

			/* Tooltip.SetDefault("" +
				"Increases the damage of Aspected Weapons by 8%" +
				""+
				"\n'A bit too perfect'"); */
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//The (English) text shown below your weapon's name
		}

		public override void SetDefaults() {
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.value = Item.sellPrice(silver: 30);
			Item.rare = 4;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<WeaponPlayer>().PerfectlyGenericAccessory = true;

			//player.GetDamage(DamageClass.Generic) += 0.12f;
			//player.respawnTimer += 600;

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
