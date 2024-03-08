
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace StarsAbove.Items.Consumables
{

    public class DebugPosition : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Debug Subworld Position Finder");
			/* Tooltip.SetDefault("Used for subworld calibration" +
				"\nThis is a debug item!"); */
			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Red;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
			Item.ResearchUnlockCount = 0;

		}

		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.

		public override void HoldItem(Player player)
		{
			
		}

		public override bool CanUseItem(Player player) {

			return true;
		}

		public override bool? UseItem(Player player) {
			if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"Active Subworld:{SubworldSystem.Current}"), 241, 255, 180);}
			if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"Position:{player.position.X}X, {player.position.Y}Y"), 241, 255, 180);}
			if (Main.netMode != NetmodeID.Server) { Main.NewText(Language.GetTextValue($"Position to Tile Coordinates:{player.Center.ToTileCoordinates()}"), 241, 255, 180); }

			if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"Center:{player.Center.X}X, {player.Center.Y}Y"), 141, 205, 180);}
			var tilePos = player.Bottom.ToTileCoordinates16();
			Tile tile = Framing.GetTileSafely(tilePos.X, tilePos.Y);
			if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"Current tile:{tile.TileType}."), 141, 205, 180);}

            if (Main.netMode != NetmodeID.Server) { Main.NewText(Language.GetTextValue($"World height:{Main.UnderworldLayer}."), 141, 205, 180); }



            return true;
		}
		public override void AddRecipes()
		{
		
		}
	}
}