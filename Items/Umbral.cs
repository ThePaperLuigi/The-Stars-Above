
using StarsAbove.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace StarsAbove.Items
{

    public class Umbral : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Umbral Item");
			/* Tooltip.SetDefault("Debug cheat item" +
				"\nEnables all ablities in the Stellar Array- do NOT deselect abilities; use the Debug Disk." +
				"" +
				""); */
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

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starshower = 2; //
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ironskin = 2; //
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().evasionmastery = 2;//
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aquaaffinity = 2;//
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().inneralchemy = 2;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().hikari = 2;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().livingdead = 2;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().umbralentropy = 2;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().celestialevanesence = 2;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().mysticforging = 2;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().butchersdozen = 2;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().flashfreeze = 2;
			//Tier 2
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>(). healthyConfidence = 2;//
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().bloomingflames = 2;//
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().astralmantle = 2;//
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().avataroflight = 2;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().afterburner = 2;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().weaknessexploit = 2;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().artofwar = 2;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aprismatism = 2;
			//Tier 3
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().beyondinfinity = 2;//
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().keyofchronology = 2;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().unbridledradiance = 2;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().beyondtheboundary = 2;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge = 6;
			return true;
		}
		
	}
}