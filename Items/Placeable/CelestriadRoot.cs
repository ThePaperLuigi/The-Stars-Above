
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using SubworldLibrary;
using StarsAbove.Buffs;
using StarsAbove.Projectiles.Otherworld;
using StarsAbove.Buffs.SubworldModifiers;
using StarsAbove.Subworlds;

namespace StarsAbove.Items.Placeable
{

    public class CelestriadRoot : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestriad Root");
			

			Tooltip.SetDefault("" +
				"A sapling from the heart of the universe" +
				"\nCan be used as a crafting bench to create the Stellaglyph and its upgrades" +
				"" +
				"");
			//
			//
			//"\n[c/D32C2C:Modded chests from mods added after world generation may cease to open once entering a subworld]" +
			//"\n[c/D32C2C:Mods which allow global auto-use may cause issues upon usage]" +
			//"\n[c/D32C2C:Mods which 'cull' projectiles (anti-lag mods) will cause issues]");
			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.CelestriadRoot>(), 0);

			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Red;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.noUseGraphic = false;
		}

		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
		public override bool CanUseItem(Player player)
		{
			return true;
			if (Main.netMode != NetmodeID.SinglePlayer)
			{
				if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("Subworlds have issues in Multiplayer currently. Please wait until further notice."), 190, 100, 247);}
				return false;
			}
			if (player.HasBuff(BuffType<BifrostCooldown>()))//Whenever the Bifrost is used to move between worlds, add a short cooldown to prevent AutoSwing making things a mess.
			{
				return false;
			}
			if (player.HasBuff(BuffType<VoyageCooldown>()) && SubworldSystem.IsActive<Observatory>() && player.HasBuff(BuffType<GatewayBuff>()))
			{
				if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("Cosmic Voyages are still on cooldown!"), 241, 255, 180);}
				return false;
			}
			if (NPC.downedAncientCultist && !NPC.downedMoonlord)
			{
				if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("Lunar energy prevents the activation of the Bifrost..."), 255, 255, 100);}
				return false;
			}
			if (player.inventory[58].IsAir && !player.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive && !player.GetModPlayer<StarsAbovePlayer>().stellarArray && !player.GetModPlayer<StarsAbovePlayer>().novaUIActive)
			{
				if (SubworldSystem.Current == null && player.HasBuff(BuffType<PortalReady>()))
				{

					return true;
				}
				if (SubworldSystem.Current != null && player.HasBuff(BuffType<PortalReady>()))
				{

					return true;
				}
				if (SubworldSystem.Current != null && player.HasBuff(BuffType<GatewayBuff>()))
				{
					if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("Subworlds are still being worked on in tModLoader 1.4! Thanks for your patience."), 190, 100, 247);}

					return true;
				}
				if (SubworldSystem.Current == null)
				{
					return true;
				}
			}

			return false;
		}
		public override void HoldItem(Player player)
		{
			return;
			if (SubworldSystem.Current == null)//Entering the Observatory
			{
				player.AddBuff(BuffType<GatewayBuff>(), 2);

				if (player.ownedProjectileCounts[ProjectileType<HeldGateway>()] < 1)
				{
					Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), player.position.X, player.position.Y, 0, 0, ProjectileType<HeldGateway>(), 0, 4, player.whoAmI, 0f);


				}
			}
			



			base.HoldItem(player);
		}
		public override bool? UseItem(Player player)
		{
			return true;
			//string id = "StarsAbove/Observatory";
			if (!SubworldSystem.AnyActive(Mod) && !player.HasBuff(BuffType<BifrostCooldown>()))
			{

				player.AddBuff(BuffType<BifrostCooldown>(), 240);
				SubworldSystem.Enter<Observatory>();


				//return true;
			}
			if (SubworldSystem.AnyActive(Mod))
			{

				if (!SubworldSystem.IsActive<Observatory>() && player.HasBuff(BuffType<PortalReady>())) //Leaving a Cosmic Voyage
				{
					//player.AddBuff(BuffType<Wormhole>(), 10);
					if (player.whoAmI == Main.myPlayer)
					{
						player.AddBuff(BuffType<BifrostCooldown>(), 240);
						SubworldSystem.Enter<Observatory>();
					}
					return true;
				}
				if (SubworldSystem.IsActive<Observatory>() && player.HasBuff(BuffType<PortalReady>())) //Leaving the Observatory
				{
					if (player.whoAmI == Main.myPlayer)
					{
						player.AddBuff(BuffType<BifrostCooldown>(), 240);
						SubworldSystem.Exit();
					}
					return true;
				}
				if (SubworldSystem.IsActive<Observatory>() && player.HasBuff(BuffType<GatewayBuff>()) && !player.HasBuff(BuffType<VoyageCooldown>())) //Entering a Cosmic Voyage
				{
					//player.AddBuff(BuffType<Wormhole>(), 120);
					//player.AddBuff(BuffType<VoyageCooldown>(), 36000); //Around 10 minutes?
					//player.AddBuff(BuffType<VoyageCooldown>(), 600); Debug speed.
					return true;
				}







				return true;
			}
			
			/*
			if (SubworldSystem.IsActive<Observatory>() && player.HasBuff(BuffType<GatewayBuff>()) && !player.HasBuff(BuffType<VoyageCooldown>())) //Entering a Cosmic Voyage
			{
				int randomWorld = Main.rand.Next(0, 8);
				//randomWorld = 7;
				if (randomWorld == 0)
				{
					SubworldSystem.IsActive<SeaOfStars1>(); // Drifting Ruins

				}
				if (randomWorld == 1)
				{
					SubworldSystem.IsActive<SeaOfStars2>(); // Outerworld Geode

				}
				if (randomWorld == 2)
				{
					SubworldSystem.IsActive<AncientMiningFacility>(); // Unknown Mining Facility

				}
				if (randomWorld == 3)
				{
					SubworldSystem.IsActive<RuinedSpaceship>(); // Ruined Starship

				}
				if (randomWorld == 4)
				{
					SubworldSystem.IsActive<TheDyingCitadel>(); // Big Dungeon #1 Dying Citadel

				}
				if (randomWorld == 5)
				{
					SubworldSystem.IsActive<SamuraiWar>(); // Samurai Planet

				}
				if (randomWorld == 6)
				{
					SubworldSystem.IsActive<GalacticMean>(); // The Galactic Mean

				}
				if (randomWorld == 7)
				{
					SubworldSystem.IsActive<JungleTower>(); // The Jungle Tower

				}
				//player.AddBuff(BuffType<Wormhole>(), 120);
				player.AddBuff(BuffType<VoyageCooldown>(), 36000); //Around 10 minutes?
																   //player.AddBuff(BuffType<VoyageCooldown>(), 600); Debug speed.
				return true;
			}
			*/







			return false;
		}

		public override bool CanPickup(Player player)
		{
			

			return true;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.ManaCrystal, 3)
				.AddIngredient(ItemID.FallenStar, 5)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}