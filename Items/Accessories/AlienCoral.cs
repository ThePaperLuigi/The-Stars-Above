using Terraria;using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace StarsAbove.Items.Accessories
{
	public class AlienCoral : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Alien Coral");

			Tooltip.SetDefault("" +
				"For each unused Stellar Array Energy point, gain 5% damage" +
				"\nGain 10 defense with a full Stellar Array instead"+
				"\n'Can only be described as lava lamp liquid forced to abdicate its throne'");
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//The (English) text shown below your weapon's name
		}

		public override void SetDefaults() {
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.value = Item.sellPrice(silver: 30);
			Item.rare = 3;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<StarsAbovePlayer>().AlienCoral = true;
			if(player.GetModPlayer<StarsAbovePlayer>().stellarGauge < player.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax)
            {
				player.GetDamage(DamageClass.Generic) += 0.05f * (player.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax - player.GetModPlayer<StarsAbovePlayer>().stellarGauge);
			}
			else
            {
				player.statDefense += 10;
            }
			
			//player.lifeRegen += 2;
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
