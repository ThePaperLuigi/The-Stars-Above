
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace StarsAbove.Items.Lore
{

    public class MinerMemoir1 : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("A Miner's Memoir 1");
			/* Tooltip.SetDefault("[Part 1 of 2]" +
				$"\nConsume to unlock the story '{DisplayName.ToString}' in the Archive" +
                "\nCan also be sold for a high price at shops"); */
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
			Item.value = 10000;
		}

		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
		public override bool CanUseItem(Player player) {

			return false;
		}

		public override bool? UseItem(Player player) {
			
			return true;
		}
		public override void AddRecipes()
		{
			//ModRecipe recipe = new ModRecipe(mod);
			//recipe.AddIngredient(ItemID.HallowedBar, 3);
			//recipe.AddIngredient(ItemID.DarkShard, 1);
			//recipe.AddIngredient(ItemID.Bone, 12);
			//recipe.AddIngredient(ItemID.ManaCrystal, 5);
			//recipe.AddTile(TileID.WorkBenches);
			//recipe.SetResult(this);
			//recipe.AddRecipe();
		}
	}
}