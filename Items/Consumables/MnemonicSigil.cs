
using StarsAbove.Buffs;
using StarsAbove.NPCs.Tsukiyomi;
using StarsAbove.Subworlds;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace StarsAbove.Items.Consumables
{

    public class MnemonicSigil : ModItem
	{
		public override void SetStaticDefaults()
		{
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			// DisplayName.SetDefault("Mnemonic Sigil");
			/* Tooltip.SetDefault("The combined memories of worlds defeated by the First Starfarer" +
				"\nCan be used to be taken to the [c/7FC1EF:Eternal Confluence]" +
				"\nUse again within the center of the [c/7FC1EF:Eternal Confluence] to summon [c/F1AF42:Tsukiyomi, the First Starfarer]" +
				"\n[c/7FC1EF:Will summon Tsukiyomi normally in Multiplayer]" +
				"\nIs not consumed upon use" +
				"\n'...'" +
				"\n"); */
			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Red;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.noUseGraphic = true;
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
			//return !NPC.AnyNPCs(NPCType<NPCs.Tsukiyomi>());
			
			return (!NPC.AnyNPCs(NPCType<TsukiyomiBoss>()));
		}

		public override bool? UseItem(Player player)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			
			int type = ModContent.NPCType<TsukiyomiBoss>();

			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				//NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
			}
			else
            {
				

			}
			//var tilePos = player.Bottom.ToTileCoordinates16();
			//Tile tile = Framing.GetTileSafely(tilePos.X, tilePos.Y);
			//tile.TileType == TileID.AmethystGemspark &&
			if (SubworldSystem.IsActive<EternalConfluence>())
			{

				if (Main.netMode != NetmodeID.Server) { Main.NewText(Language.GetTextValue("The expanse around you begins to contract..."), 210, 100, 175); }
				//if (Main.netMode != NetmodeID.Server) { Main.NewText(Language.GetTextValue("Tsukiyomi appears before you!"), 200, 150, 125); }
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
			else
			{
				SubworldSystem.Enter("StarsAbove/EternalConfluence");
			}

			/*if(!SubworldSystem.IsActive<EternalConfluence>())
            {
				if (Main.netMode == NetmodeID.MultiplayerClient)
				{
					NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: type);
				}
				else
				{
					NPC.SpawnOnPlayer(player.whoAmI, type);

				}
			}
			
			*/





			return true;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemType<MnemonicTrace>())
				.AddIngredient(ItemType<MnemonicTrace2>())
				.AddIngredient(ItemType<MnemonicTrace3>())
				.AddIngredient(ItemType<MnemonicTrace4>())
				.AddTile(TileType<Tiles.CelestriadRoot>())
				.Register();
		}
	}
}