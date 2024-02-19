using StarsAbove.Systems;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Items.Placeable.StellarSpoils
{
	public class MiningPaintingItem : ModItem
	{
		public override void SetStaticDefaults()
		{

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			// Vanilla has many useful methods like these, use them! This substitutes setting Item.createTile and Item.placeStyle aswell as setting a few values that are common across all placeable items
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.StellarSpoils.MiningPainting>());

			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 99;
            Item.rare = ModContent.GetInstance<StellarSpoilsRarity>().Type; // Custom Rarity
            Item.value = Item.buyPrice(0, 1);
		}
	}
}