
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace StarsAbove.Items.Lore
{

    public class MinerMemoir : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("A Miner's Memoir");
			/* Tooltip.SetDefault("" +
				"\"After the last shipment. Sat and waited until extraction. He always liked this part.\"" +
				"\n\"'This is the only respite we can afford.' Talked big. Didn't save him.\"" +
				"\n\"Word says they went bankrupt. Can't afford to send us home. That's fine.\"" +
				"\n\"I already sent my last check over. They're in good hands, I tell myself.\"" +
				"\n\"Another lie. Like the others.\"" +
				"\nThe rest is illegible"); */
			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Gray;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
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