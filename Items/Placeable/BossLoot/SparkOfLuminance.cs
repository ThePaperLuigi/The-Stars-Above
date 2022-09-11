using StarsAbove.Items.Materials;
using StarsAbove.Items.Prisms;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Items.Placeable.BossLoot
{
	public class SparkOfLuminance : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spark of Luminance");
			Tooltip.SetDefault("Changes the nearby environment to Light Everlasting" +
				$"");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			// Vanilla has many useful methods like these, use them! This substitutes setting Item.createTile and Item.placeStyle aswell as setting a few values that are common across all placeable items
			// The place style (here by default 0) is important if you decide to have more than one relic share the same tile type (more on that in the tiles' code)
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.BossLoot.EverlastingLightMonolith>(), 0);

			Item.width = 30;
			Item.height = 40;
			Item.maxStack = 99;
			Item.rare = ItemRarityID.Red;
			//Item.master = true; // This makes sure that "Master" displays in the tooltip, as the rarity only changes the item name color
			Item.value = Item.buyPrice(0, 5);
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<PrismaticCore>())
				.AddIngredient(ItemType<DullTotemOfLight>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}