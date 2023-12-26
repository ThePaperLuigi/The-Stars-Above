using StarsAbove.Systems;
using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Items.Accessories
{
    public class AlienCoral : StargazerRelic
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Alien Coral");

			/* Tooltip.SetDefault("[c/2DD2FE:Stargazer Relic]" +
				"\nFor each unused Stellar Array Energy point, gain 8% increased damage" +
				"\nGain 8 defense with a full Stellar Array instead" +
				"\n[c/ADEEFF:Only one Stargazer Relic can be equipped at a time]" +
				"\n'From a world alien beyond belief'"); */
			
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//The (English) text shown below your weapon's name
		}

		public override void SetDefaults() {
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ModContent.GetInstance<StellarRarity>().Type; // Custom Rarity
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<WeaponPlayer>().AlienCoral = true;
			if(player.GetModPlayer<StarsAbovePlayer>().stellarGauge < player.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax)
            {
				player.GetDamage(DamageClass.Generic) += 0.08f * (player.GetModPlayer<StarsAbovePlayer>().stellarGaugeMax - player.GetModPlayer<StarsAbovePlayer>().stellarGauge);
			}
			else
            {
				player.statDefense += 8;
            }
			
			//player.lifeRegen += 2;
			//player.respawnTimer += 600;

		}
		public override void AddRecipes()
		{
			
		}
	}
}
