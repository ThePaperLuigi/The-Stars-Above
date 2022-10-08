using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace StarsAbove.Items.Consumables
{

    public class TestDummySpawner : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Test Dummy Spawner");
			Tooltip.SetDefault("Summons a test dummy" +
				"\nOnly works in Singleplayer" +
				"\nIs not consumed upon use");
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
		}

		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
		

		public override bool? UseItem(Player player) {
			if (player.whoAmI == Main.myPlayer)
			{
				// If the player using the item is the client
				// (explicitely excluded serverside here)


				int type = ModContent.NPCType<NPCs.DummyEnemy>();


				NPC.SpawnOnPlayer(player.whoAmI, type);
			}
			
			
			//Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}
		public override void AddRecipes()
		{
			//ModRecipe recipe = new ModRecipe(mod);
			//recipe.AddIngredient(ItemID.NightKey, 1);
			//recipe.AddIngredient(ItemID.DarkShard, 1);
			//recipe.AddTile(TileID.AdamantiteForge);
			//recipe.SetResult(this);
			//recipe.AddRecipe();
		}
	}
}