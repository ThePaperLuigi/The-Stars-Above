
using StarsAbove.Items.Materials;
using Terraria;using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace StarsAbove.Items.Consumables
{

	public class SpatialMemoriam : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spatial Memoriam");
			Tooltip.SetDefault("" +
				"Overwhelmingly luminous material glitters with potential" +
				"\nCan be used to craft an incredibly powerful weapon" +
				"\n'The power of the sun, in the palm of my hand'" +
				"\n");
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 99;
			Item.rare = ItemRarityID.Red;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
		}

		int availableWeapons = 0;
		int weapon;

		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.

		public override void HoldItem(Player player)
		{

		}

		public override bool CanUseItem(Player player)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			
			return false;
		}

		public override bool? UseItem(Player player)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			




			return true;
		}

		public override void AddRecipes()
		{
			/*ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<MnemonicTrace>());
			recipe.AddIngredient(ItemType<MnemonicTrace2>());
			recipe.AddIngredient(ItemType<MnemonicTrace3>());
			recipe.AddIngredient(ItemType<MnemonicTrace4>());
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();*/
		}
	}
}