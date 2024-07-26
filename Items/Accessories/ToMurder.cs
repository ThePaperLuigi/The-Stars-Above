using StarsAbove.Systems;
using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Items.Accessories
{
    public class ToMurder : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("'To Murder'");

			/* Tooltip.SetDefault("" +
				"Activates only when Defense is below 40" +
                "\nGain 10% damage, increased to 45% below 10 Defense" +
				"\nEnemies are drastically more likely to target you"+
				"\n'Can you get away with it?'"); */
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//The (English) text shown below your weapon's name
		}

		public override void SetDefaults() {
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.value = Item.sellPrice(silver: 30);
			Item.rare = ModContent.GetInstance<StellarSpoilsRarity>().Type; // Custom Rarity
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<WeaponPlayer>().ToMurder = true;

			if (player.statDefense < 30)
			{
				player.GetDamage(DamageClass.Generic) += 0.05f;
			}
			if(player.statDefense < 10)
            {
				player.GetDamage(DamageClass.Generic) += 0.10f;
			}
			player.aggro += 80;
			//player.respawnTimer += 600;

		}



		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ModContent.ItemType<Materials.StellarRemnant>(), 40)
				.AddCustomShimmerResult(ModContent.ItemType<Materials.StellarRemnant>(), 3)
				.AddTile(Terraria.ID.TileID.Anvils)
				.Register();
		}
	}
}
