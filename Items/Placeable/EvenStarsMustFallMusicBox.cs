using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using StarsAbove.Items.Prisms;
using StarsAbove.Systems;

namespace StarsAbove.Items.Placeable
{
    public class EvenStarsMustFallMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Music Box (Tsukiyomi, the First Starfarer: Non-Expert)");
			/* Tooltip.SetDefault("" +
				"'The Extreme' - Final Fantasy 8 OST" +
				"\nArranged by Daiki Ishikawa - Composed by Nobuo Uematsu"
				+ $""); */
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Boss/Tsukiyomi/EvenStarsMustFall"), ModContent.ItemType<EvenStarsMustFallMusicBox>(), ModContent.TileType<Tiles.EvenStarsMustFallMusicBox>());// ModContent.TileType<Tiles.SunsetStardustMusicBox>());

		}

		public override void SetDefaults()
		{
			Item.useStyle = 1;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("EvenStarsMustFallMusicBox").Type;
			Item.width = 24;
			Item.height = 24;
			Item.rare = ModContent.GetInstance<StellarSpoilsRarity>().Type; // Custom Rarity
			Item.value = 1;
			Item.accessory = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ModContent.ItemType<Materials.StellarRemnant>(), 20)
				.AddCustomShimmerResult(ModContent.ItemType<Materials.StellarRemnant>(), 3)
				.AddTile(Terraria.ID.TileID.Anvils)
				.Register();
		}
	}

}
