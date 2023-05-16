using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using StarsAbove.Items.Materials;

namespace StarsAbove.Items.Placeable
{
	public class UnmatchingPiecesMusicBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Music Box (Everlasting Light)");
			/* Tooltip.SetDefault("" +
				"'Unmatching Pieces' - FFXIV Shadowbringers OST" +
				"\nComposed by Masayoshi Soken"
				+ $"\n"); */
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Sounds/Music/EverlastingLight"),
				ModContent.ItemType<UnmatchingPiecesMusicBox>(),
				ModContent.TileType<Tiles.UnmatchingPiecesMusicBox>());
		}

		public override void SetDefaults()
		{
			Item.useStyle = 1;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>("UnmatchingPiecesMusicBox").Type;
			Item.width = 24;
			Item.height = 24;
			Item.rare = 10;
			Item.value = 1;
			Item.accessory = true;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<DullTotemOfLight>(), 1)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}

}
