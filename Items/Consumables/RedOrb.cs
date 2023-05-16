using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace StarsAbove.Items.Consumables
{

    public class RedOrb : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Red Orb");
			/* Tooltip.SetDefault("Red orb." +
				"\n[c/F1AF42:Should not be able to be read.]" +
				"\n"); */
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
			ItemID.Sets.ItemNoGravity[Item.type] = true;
		}

		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
		public override bool ItemSpace(Player player)
		{
			return true;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		public override bool CanPickup(Player player)
		{
			return true;
		}

		public override bool OnPickup(Player player)
		{
			if(player.HeldItem.ModItem is CrimsonSakuraAlpha)
            {
				player.AddBuff(BuffType<Buffs.RedOrb>(), 1200);
			}
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