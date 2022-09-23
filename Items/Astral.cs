
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace StarsAbove.Items
{

    public class Astral : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Astral Item");
			Tooltip.SetDefault("Debug cheat item" +
				"\nResets the cooldown on obtaining Essences and fills the Stellar Nova Gauge" +
				"\n" +
				"");
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


			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().WeaponDialogueTimer = 0;
			
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaGauge = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().trueNovaGaugeMax;
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().theofania == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().theofania = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().laevateinn == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().laevateinn = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().kiwamiryuken == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().kiwamiryuken = 1;
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().gardenofavalon == 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().gardenofavalon = 1;
			}
			
			return true;
		}
		
	}
}