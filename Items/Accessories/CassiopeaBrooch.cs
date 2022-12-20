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

			Tooltip.SetDefault("" +
				"[Stargazer relic]" +
				"\nP"+
				"\n'The queen of the Empire rests in eternal cryosleep, waiting for the day she can cheat death forever'");
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
