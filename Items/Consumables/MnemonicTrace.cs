using StarsAbove.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace StarsAbove.Items.Consumables
{

    public class MnemonicTrace : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mnemonic Trace");
			/* Tooltip.SetDefault("A trace of the First Starfarer's rampage" +
				"\n'Memories of salvation'" +
				"\n"); */
			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 99;
			Item.rare = ItemRarityID.Red;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
		}

		int availableWeapons = 0;
		int weapon;

		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.

		public override void HoldItem(Player player)
		{

		}

		public override bool CanUseItem(Player player)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			
			return true;
		}

		public override bool? UseItem(Player player)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

			if(modPlayer.MnemonicDialogue1 == 1 && modPlayer.MnemonicDialogue2 == 1 && modPlayer.MnemonicDialogue3 == 1)
			{
                modPlayer.dialogueScrollTimer = 0;
                modPlayer.dialogueScrollNumber = 0;
                modPlayer.sceneProgression = 0;
                modPlayer.sceneID = 304;
                modPlayer.VNDialogueActive = true;

                modPlayer.MnemonicDialogue4 = 1;
            }
			else
			{
                modPlayer.dialogueScrollTimer = 0;
                modPlayer.dialogueScrollNumber = 0;
                modPlayer.sceneProgression = 0;
                modPlayer.sceneID = 303;
                modPlayer.VNDialogueActive = true;

            }
            



            return true;
		}

		public override void AddRecipes()
		{
			
		}
	}
}