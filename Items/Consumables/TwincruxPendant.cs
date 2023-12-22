
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace StarsAbove.Items.Consumables
{

    public class TwincruxPendant : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("The Twincrux Pendant");
			/* Tooltip.SetDefault("This charm appears to be pulling itself apart" +
				"\n[c/F1AF42:Summons Dioskouroi, the Twin Forces]" +
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

			return !NPC.AnyNPCs(NPCType<NPCs.Dioskouroi.CastorBoss>()) && SubworldSystem.Current == null;
		}

		public override bool? UseItem(Player player) {
			if (player.whoAmI == Main.myPlayer)
			{
				// If the player using the item is the client
				// (explicitely excluded serverside here)


				int type1 = ModContent.NPCType<NPCs.Dioskouroi.PolluxBoss>();
				int type2 = ModContent.NPCType<NPCs.Dioskouroi.CastorBoss>();
				int type3 = ModContent.NPCType<NPCs.Dioskouroi.DioskouroiWallsNPC>();

				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					// If the player is not in multiplayer, spawn directly
					NPC.SpawnOnPlayer(player.whoAmI, type1);
					NPC.SpawnOnPlayer(player.whoAmI, type2);
					NPC.NewNPC(null, (int)player.Center.X, (int)player.Center.Y, type3);

				}
				else
				{
					// If the player is in multiplayer, request a spawn
					// This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in MinionBossBody
					NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type1);
					NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type2);
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						NPC.NewNPC(null, (int)player.Center.X, (int)player.Center.Y, type3);
					}
						

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