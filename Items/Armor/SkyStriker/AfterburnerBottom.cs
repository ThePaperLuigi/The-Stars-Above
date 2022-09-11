using Terraria;using Terraria.DataStructures;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;
using Terraria.ID;
using StarsAbove.Items.Materials;
using StarsAbove.Items.Prisms;

namespace StarsAbove.Items.Armor.SkyStriker

{
	[AutoloadEquip(EquipType.Legs)]
	public class AfterburnerBottom : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sky Striker Armor");
			Tooltip.SetDefault("You shouldn't be able to read this!");
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