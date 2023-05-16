using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Items.Armor.SkyStriker

{
    [AutoloadEquip(EquipType.Legs)]
	public class SniperBottom : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sky Striker Armor");
			// Tooltip.SetDefault("You shouldn't be able to read this!");
        }

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 24;
			Item.value = 1;
			Item.rare = 10;
			Item.vanity = true;
		}
		
	}
}