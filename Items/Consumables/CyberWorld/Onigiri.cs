using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;


namespace StarsAbove.Items.Consumables.CyberWorld
{

    public class Onigiri : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Foreign Rice Ball");
			/* Tooltip.SetDefault("A tasty snack" +
				"\nHas no effect" +
				"\n"); */
			//ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.White;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.UseSound = SoundID.Item2;
			Item.consumable = true;
			ItemID.Sets.ItemNoGravity[Item.type] = false;
		}

		
		public override bool ItemSpace(Player player)
		{
			return true;
		}

		public override bool CanPickup(Player player)
		{
			return true;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}

		
		public override bool CanUseItem(Player player) {

			return true;
		}

		public override bool? UseItem(Player player) {
			
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