
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace StarsAbove.Items.Consumables
{

    public class UnsulliedCanvas : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("The Unsullied Canvas");
			Tooltip.SetDefault("This canvas brims with possibility" +
				"\n[c/F1AF42:Summons Penthesilea, The Witch of Ink]" +
				"\nThis boss is available early, but you may find it much too difficult" +
				"\nPrepare accordingly, and consider postponing this fight until you are stronger" +
				"\nIs not consumed upon use");
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

			return !NPC.AnyNPCs(NPCType<NPCs.Penthesilea>()) && SubworldSystem.Current == null;
		}

		public override bool? UseItem(Player player) {
			if (player.whoAmI == Main.myPlayer)
			{
				// If the player using the item is the client
				// (explicitely excluded serverside here)


				int type = ModContent.NPCType<NPCs.Penthesilea>();

				if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("Magical energy coalesces around you..."), 210, 100, 175);}
				if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("Penthesilea, The Witch of Ink draws near!"), 200, 150, 125);}
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

			
			//NPC.NewNPC(null, (int)player.Center.X,(int)player.Center.Y-900, NPCType<NPCs.Penthesilea>());
			//Main.PlaySound(SoundID.Roar, player.position, 0);
			return true;
		}
		public override void AddRecipes()
		{
			//ModRecipe recipe = new ModRecipe(mod);
			//recipe.AddIngredient(ItemID.HallowedBar, 3);
			//recipe.AddIngredient(ItemID.DarkShard, 1);
			//recipe.AddIngredient(ItemID.Bone, 12);
			//recipe.AddIngredient(ItemID.ManaCrystal, 5);
			//recipe.AddTile(TileID.WorkBenches);
			//recipe.SetResult(this);
			//recipe.AddRecipe();
		}
	}
}