using StarsAbove.Items.Prisms;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Accessories
{
    public class CassiopeaBrooch : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Cassiopea's Brooch");

			Tooltip.SetDefault("[c/2DD2FE:Stargazer Relic]" +
				"\nUpon entering combat, gain Battle High for 12 seconds" +
				"\nBattle High doubles outgoing damage and increases movement speed by 30%" +
                "\nAdditionally, Battle High gains bonus effects at certain HP thresholds" +
				"\n500 HP or above: 40% increased damage" +
                "\nBelow 500 HP: 20% increased damage" +
                "\nBelow 300 HP: 10 defense" +
                "\nBelow 100 HP: 30 defense" +
                "\nDefeating foes grants 4 seconds of Battle High" +
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
			Item.rare = ItemRarityID.LightPurple;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			

		}

		

		public override void AddRecipes() {
			
		}
	}
}
