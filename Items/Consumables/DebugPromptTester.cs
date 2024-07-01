using StarsAbove.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace StarsAbove.Items.Consumables
{

    public class DebugPromptTester : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Debug VN Tester");
			/* Tooltip.SetDefault("Used for VN dialogue testing" +
				"\nThis is a debug item!"); */
			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Red;
			Item.useAnimation = 2;
			Item.useTime = 2;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.noUseGraphic = true;
			Item.consumable = false;
			Item.ResearchUnlockCount = 0;

		}

		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.

		public override void HoldItem(Player player)
		{
			//if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"{player.GetModPlayer<StarsAbovePlayer>().VNDialogueVisibleName}"), 250, 100, 247);}
			//if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"{player.GetModPlayer<StarsAbovePlayer>().dialogue}"), 250, 100, 247);}

		}

		public override bool CanUseItem(Player player) {

			return true;
		}

		public override bool? UseItem(Player player) {

			player.GetModPlayer<StarsAbovePlayer>().starfarerPromptCooldown = 0;
			player.GetModPlayer<StarsAbovePlayer>().seenEyeOfCthulhu = false;
            player.GetModPlayer<StarsAbovePlayer>().seenRain = false;
            player.GetModPlayer<StarsAbovePlayer>().seenJungleBiome = false;



            return true;
		}
		public override void AddRecipes()
		{
		
		}
	}
}