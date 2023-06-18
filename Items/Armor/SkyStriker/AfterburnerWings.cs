
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace StarsAbove.Items.Armor.SkyStriker
{
    [AutoloadEquip(EquipType.Wings)]
	public class AfterburnerWings : ModItem
	{
		

		public override void SetStaticDefaults() {
			// Tooltip.SetDefault("You shouldn't be able to read this!");
		}

		public override void SetDefaults() {
			Item.width = 22;
			Item.height = 20;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.accessory = true; Item.ResearchUnlockCount = 0;

		}
		//these wings use the same values as the solar wings
		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.wingTimeMax = 180;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend) {
			ascentWhenFalling = 0.85f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration) {
			speed = 9f;
			acceleration *= 2.5f;
		}

		
	}
}