
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using SubworldLibrary;
using StarsAbove.Buffs;
using StarsAbove.Projectiles.Otherworld;
using StarsAbove.Buffs.SubworldModifiers;
using StarsAbove.Subworlds;

namespace StarsAbove.Items.Placeable.Cosmoturgy
{

    public class CosmoturgyStationItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			
		}

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Cosmoturgy.CosmoturgyStation>(), 0);
            Item.value = Item.buyPrice(gold: 3);
			/*
            Item.createTile = ModContent.TileType<Tiles.Cosmoturgy.CosmoturgyStation>();
			Item.useTurn = true;
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Blue;
			Item.useAnimation = 20;

			Item.useTime = 20;
			Item.useStyle = 1;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.noUseGraphic = false;*/
		}

		public override void AddRecipes()
		{
			/*CreateRecipe(1)
				.AddIngredient(ItemID.Ruby, 1)
				.AddIngredient(ItemID.Topaz, 1)
				.AddIngredient(ItemID.Amber, 1)
				.AddIngredient(ItemID.Amethyst, 1)
				.AddIngredient(ItemID.Sapphire, 1)
				.AddIngredient(ItemID.Emerald, 1)
				.AddTile(TileType<Tiles.CelestriadRoot>())
				.Register();*/
		}
	}
}