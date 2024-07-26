
using StarsAbove.Systems;
using StarsAbove.Utilities;
using SubworldLibrary;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace StarsAbove.Items.Consumables
{

    public class ElectrumScissors : ModItem
	{
		public override void SetStaticDefaults() {
			
		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
            Item.rare = ModContent.GetInstance<StellarSpoilsRarity>().Type; // Custom Rarity
            Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.ResearchUnlockCount = 0;

		}

		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.

		public override void HoldItem(Player player)
		{
			
		}

		public override bool CanUseItem(Player player) {

			return true;
		}

		public override bool? UseItem(Player player) {
			player.GetModPlayer<StarsAbovePlayer>().cyberpunkHairstyleUnlocked = true;
            SoundEngine.PlaySound(StarsAboveAudio.SFX_DealmakerCharged);
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.UnlockedHairstyle"), 241, 255, 180); }



            return true;
		}
        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ModContent.ItemType<Materials.StellarRemnant>(), 40)
                .AddCustomShimmerResult(ModContent.ItemType<Materials.StellarRemnant>(), 3)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}