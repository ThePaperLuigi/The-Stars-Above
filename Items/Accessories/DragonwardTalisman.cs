using StarsAbove.Items.Prisms;
using Terraria;using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Materials;

namespace StarsAbove.Items.Accessories
{
	public class DragonwardTalisman : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Dragonward Talisman");

			Tooltip.SetDefault("" +
				"Grants immunity to Silenced, Confused, Bleeding, and Ichor" +
				"\nGain 20 Defense above 500 HP"+
				"\n'Prevents aetherical corruption... just don't scratch it!'");
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
			player.GetModPlayer<StarsAbovePlayer>().DragonwardTalisman = true;
			player.buffImmune[BuffID.Silenced] = true;
			player.buffImmune[BuffID.Confused] = true;
			player.buffImmune[BuffID.Ichor] = true;
			player.buffImmune[BuffID.Bleeding] = true;
			
			if(player.statLife > 500)
            {
				player.statDefense += 20;
            }

		}

		

		public override void AddRecipes() {
			CreateRecipe(1)
				.AddIngredient(ItemID.HallowedBar, 12)
				.AddIngredient(ItemType<PrismaticCore>(), 8)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}
