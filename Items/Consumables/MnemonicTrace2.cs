
using StarsAbove.Items.Materials;
using Terraria;using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace StarsAbove.Items.Consumables
{

	public class MnemonicTrace2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mnemonic Trace");
			Tooltip.SetDefault("A trace of the First Starbearer's rampage" +
				"\n'Memories of apocalypse'" +
				"\n");
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
			
			return true;
		}

		public override bool? UseItem(Player player)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			




			return true;
		}

		public override void AddRecipes()
		{
			
		}
	}
}