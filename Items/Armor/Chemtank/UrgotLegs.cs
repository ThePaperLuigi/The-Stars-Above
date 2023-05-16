using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace StarsAbove.Items.Armor.Chemtank

{
    [AutoloadEquip(EquipType.Legs)]
	public class UrgotLegs : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dreadnought Chemtank Legs");
			// Tooltip.SetDefault("You shouldn't be able to read this!");
			ArmorIDs.Legs.Sets.HidesBottomSkin[Item.legSlot] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 24;
			Item.value = 1;
			Item.rare = 10;
			Item.vanity = true;
		}
		public override void AddRecipes()
		{
			
		}
		
	}
}