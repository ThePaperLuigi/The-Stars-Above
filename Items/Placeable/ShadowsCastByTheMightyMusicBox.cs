using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using StarsAbove.Items.Prisms;

namespace StarsAbove.Items.Placeable
{
    public class ShadowsCastByTheMightyMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Music Box (Nalhaun, the Burnished King - 1st Phase)");
			/* Tooltip.SetDefault("" +
				"'Shadows Cast By The Mighty: Swift Judgement' - Bravely Default 2 OST" +
				"\nComposed by REVO"
				+ $"\n"); */
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/ShadowsCastByTheMighty"),
				ModContent.ItemType<ShadowsCastByTheMightyMusicBox>(),
				ModContent.TileType<Tiles.ShadowsCastByTheMightyMusicBox>());
		}

		public override void SetDefaults()
		{
			Item.useStyle = 1;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("ShadowsCastByTheMightyMusicBox").Type;
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
