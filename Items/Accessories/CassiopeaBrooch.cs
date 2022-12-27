using StarsAbove.Items.Prisms;
using StarsAbove.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Accessories
{
    public class CassiopeaBrooch : StargazerRelic
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Cassiopea's Brooch");

			Tooltip.SetDefault("[c/2DD2FE:Stargazer Relic]" +
				"\nNo effect" +
				"\n[c/ADEEFF:Only one Stargazer Relic can be equipped at a time]" +
				"\n'The Empire's Queen sleeps, waiting to live forever'");
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			//The (English) text shown below your weapon's name
		}

		public override void SetDefaults() {
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.value = Item.sellPrice(silver: 30);
			Item.rare = ModContent.GetInstance<StellarRarity>().Type; // Custom Rarity
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			

		}

		

		public override void AddRecipes() {
			
		}
	}
}
