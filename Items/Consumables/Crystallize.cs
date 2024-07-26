using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Items.Weapons.Melee;
using StarsAbove.Systems;
using StarsAbove.Systems.Items;
using Terraria.Audio;

namespace StarsAbove.Items.Consumables
{

    public class Crystallize : ModItem
	{
		public override void SetStaticDefaults() {

			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
		}

		public override void SetDefaults() {
			Item.width = 26;
			Item.height = 26;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Red;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
			ItemID.Sets.ItemNoGravity[Item.type] = true;
			Item.ResearchUnlockCount = 0;

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
			if(player.GetModPlayer<ItemMemorySystemPlayer>().CrystalshotCartridge)
            {
				player.GetModPlayer<ItemMemorySystemPlayer>().crystalshot++;
				player.GetModPlayer<ItemMemorySystemPlayer>().crystalshot = (int)MathHelper.Clamp(player.GetModPlayer<ItemMemorySystemPlayer>().crystalshot, 0, 6);
                Rectangle textPos = new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height);
                CombatText.NewText(textPos, new Color(242, 196, 96, 255), $"{player.GetModPlayer<ItemMemorySystemPlayer>().crystalshot}", false, false);
                SoundEngine.PlaySound(SoundID.Unlock, player.Center);
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