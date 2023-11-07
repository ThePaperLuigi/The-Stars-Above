
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace StarsAbove.Items.Consumables
{

    public class MalsaineDraught : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("The Ancient Crown");
			/* Tooltip.SetDefault("This crown howls with power" +
				"\n[c/F1AF42:Summons Nalhaun, the Burnished King]" +
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

			return !NPC.AnyNPCs(NPCType<NPCs.Nalhaun.NalhaunBoss>()) && !NPC.AnyNPCs(NPCType<NPCs.Nalhaun.NalhaunBossPhase2>()) && SubworldSystem.Current == null;
		}

		public override bool? UseItem(Player player) {
			
			if (player.whoAmI == Main.myPlayer)
			{
				// If the player using the item is the client
				// (explicitely excluded serverside here)


				int type = ModContent.NPCType<NPCs.Nalhaun.NalhaunBoss>();

				//if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("A mysterious throne descends from the heavens..."), 210, 100, 175);}
				if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("Nalhaun, the Burnished King appears!"), 200, 150, 125);}
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					// If the player is not in multiplayer, spawn directly
					NPC.SpawnOnPlayer(player.whoAmI, type);
				}
				else
				{
					// If the player is in multiplayer, request a spawn
					
					NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
				}
			}
			
			//NPC.NewNPC(null, (int)player.Center.X,(int)player.Center.Y-900, NPCType<NPCs.Nalhaun>());
			//Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}

		
	}
}