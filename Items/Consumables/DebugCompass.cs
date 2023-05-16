using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using SubworldLibrary;

namespace StarsAbove.Items.Consumables
{

    public class DebugCompass : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Debug Compass");
			/* Tooltip.SetDefault("Activates Celestial Cartography menu when held" +
				"\n[c/F1AF42:Debug item]" +
				"\n"); */
			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.White;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
			ItemID.Sets.ItemNoGravity[Item.type] = true;
		}
        public override void HoldItem(Player player)
        {
			player.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = true;
			//Test
			SubworldSystem.Exit();

            base.HoldItem(player);
        }

        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
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

		public override bool OnPickup(Player player)
		{
			
			return false;
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