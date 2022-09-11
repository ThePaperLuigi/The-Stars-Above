
using StarsAbove.NPCs;
using SubworldLibrary;
using Terraria;using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace StarsAbove.Items.Consumables
{
	
	public class ProgenitorWish : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("The Progenitor's Wish");
			Tooltip.SetDefault("This shard is the culmination of billions of ferverent prayers" +
				"\n[c/F1AF42:Summons The Warrior of Light]" +
				"\nIs not consumed upon use");
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
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
		}

		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
		public override bool CanUseItem(Player player) {
			
			return !NPC.AnyNPCs(NPCType<NPCs.WarriorOfLight>()) && SubworldSystem.Current == null;
		}

		public override bool? UseItem(Player player) {
			if (player.whoAmI == Main.myPlayer)
			{
				// If the player using the item is the client
				// (explicitely excluded serverside here)
				

				int type = ModContent.NPCType<NPCs.WarriorOfLight>();

				if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("The echoes of past eons begins to ebb and flow..."), 210, 100, 175);}
				if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("The Warrior of Light descends!"), 200, 150, 125);}
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					// If the player is not in multiplayer, spawn directly
					NPC.SpawnOnPlayer(player.whoAmI, type);
				}
				else
				{
					// If the player is in multiplayer, request a spawn
					// This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in MinionBossBody
					NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: type);
				}
			}
			
			//NPC.NewNPC(null, (int)player.Center.X,(int)player.Center.Y, NPCType<NPCs.WarriorOfLight>());
			//Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}
		public override void AddRecipes()
		{
			//ModRecipe recipe = new ModRecipe(mod);
			//recipe.AddIngredient(ItemID.NightKey, 1);
			//recipe.AddIngredient(ItemID.DarkShard, 1);
			//recipe.AddTile(TileID.AdamantiteForge);
			//recipe.SetResult(this);
			//recipe.AddRecipe();
		}
	}
}