
using StarsAbove.Subworlds;
using StarsAbove.Systems;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace StarsAbove.Items.Consumables
{

    public class DemonicCrux : ModItem
	{

		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("The Beating Crux");
			/* Tooltip.SetDefault("This object feels worryingly wrong" +
				"\n[c/F1AF42:Summons Arbitration]" +
				"\nThis boss is available early, but you may find it much too difficult" +
				"\nPrepare accordingly, and consider postponing this fight until you are stronger" +
				"\nIs not consumed upon use"); */
			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Red;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
		}

		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
		public override bool CanUseItem(Player player) {

			return !NPC.AnyNPCs(NPCType<NPCs.Arbitration.ArbitrationBoss>()) && SubworldSystem.Current == null;
		}

		public override bool? UseItem(Player player) {
			if (player.whoAmI == Main.myPlayer)
			{
				player.GetModPlayer<SubworldPlayer>().anomalyTimer = 1;
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    int type = ModContent.NPCType<NPCs.Arbitration.ArbitrationBoss>();

                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
                    return false;
                }
                else
                {

					SubworldSystem.Enter<Katabasis>();
                }

			}
			
			//NPC.NewNPC(null, (int)player.Center.X,(int)player.Center.Y-900, NPCType<NPCs.Arbitration.ArbitrationBoss>());
			//Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}
		public override void AddRecipes()
		{
			
		}
	}
}