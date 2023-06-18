using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using StarsAbove.Items.Prisms;

namespace StarsAbove.Items.Placeable
{
    public class TheExtremeMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Music Box (Tsukiyomi, the First Starfarer: Non-Expert)");
			/* Tooltip.SetDefault("" +
				"'The Extreme' - Final Fantasy 8 OST" +
				"\nArranged by Daiki Ishikawa - Composed by Nobuo Uematsu"
				+ $""); */
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/TheExtreme"), ModContent.ItemType<TheExtremeMusicBox>(), ModContent.TileType<Tiles.TheExtremeMusicBox>());// ModContent.TileType<Tiles.SunsetStardustMusicBox>());

		}

		public override void SetDefaults()
		{
			Item.useStyle = 1;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("TheExtremeMusicBox").Type;
			Item.width = 24;
			Item.height = 24;
			Item.rare = 10;
			Item.value = 1;
			Item.accessory = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<PrismaticCore>(), 8)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}

}
