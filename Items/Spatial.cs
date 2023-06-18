
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace StarsAbove.Items
{

    public class Spatial : ModItem
	{
		

		public override void SetStaticDefaults() {
			
			// DisplayName.SetDefault("test item");
			// Tooltip.SetDefault($"");

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
			//item.UseSound = SoundID.Item44;
			Item.consumable = false;
		}


		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override void HoldItem(Player player)
		{
			
			base.HoldItem(player);
		}

		public override bool CanUseItem(Player player) {
			
			return true;
		}

		public override bool? UseItem(Player player) {

			
			return true;
		}
		
	}
}