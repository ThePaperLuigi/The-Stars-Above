
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.Subworlds;
using StarsAbove.Items.Materials;
using StarsAbove.Items.Placeable;
using StarsAbove.Items.Prisms;
using StarsAbove.Subworlds;
using StarsAbove.Utilities;
using SubworldLibrary;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI.CelestialCartography
{
    internal class CelestialCompass : UIState
	{
		
		private UIText descriptionText;
		private UIText threatText;
		private UIText lootText;
		private UIText requirementText;
		private UIText titleText;
		private UIElement area;

		private UIElement TextboxRegion;
		private UIElement CompassRegion;
		private UIElement GearRegion;

		private UIImage Starmap;
		private UIImage StarmapStars;

		private UIImageButton exit;

		//Locations
		private UIImageButton Home;
		private UIImageButton Observatory;
		private UIImageButton CygnusAsteroids;
		private UIImageButton Antlia;
		private UIImageButton Aquarius;
		private UIImageButton Caelum;
		private UIImageButton Lyra;
		private UIImageButton Pyxis;

		private UIImageButton MiningStationAries;
		private UIImageButton Scorpius;
		private UIImageButton Serpens;
		private UIImageButton Tucana;
		private UIImageButton Corvus;

		private UIImageButton DreamingCity;
		private UIImageButton Theranhad;
		private UIImageButton Celestia;
		private UIImageButton FaintArchives;
		private UIImageButton UltraPlant;

		private UIImageButton QuestionMark;

		public static bool DisableMultiplayerCompatibility = false;

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.


			area = new UIElement();
			area.Top.Set(0, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(1000, 0f);
			area.Height.Set(700, 0f);
			area.HAlign = area.VAlign = 0.5f; // 1

			TextboxRegion = new UIElement();
			TextboxRegion.Width.Set(400,0f);
			TextboxRegion.Height.Set(400,0f);


			Starmap = new UIImage(Request<Texture2D>("StarsAbove/UI/blank"));
			Starmap.Left.Set(80, 0f);
			Starmap.Top.Set(0, 0f);
			Starmap.Width.Set(1000, 0f);
			Starmap.Height.Set(700, 0f);
			Starmap.IgnoresMouseInteraction = true;

			StarmapStars = new UIImage(Request<Texture2D>("StarsAbove/UI/CelestialCartography/StarmapStars"));
			StarmapStars.Left.Set(80, 0f);
			StarmapStars.Top.Set(0, 0f);
			StarmapStars.Width.Set(1000, 0f);
			StarmapStars.Height.Set(700, 0f);
			StarmapStars.IgnoresMouseInteraction = true;

			CompassRegion = new UIElement();//UIImage(Request<Texture2D>("StarsAbove/UI/CelestalCartography/CelestialCompass"));
			CompassRegion.Left.Set(0, 0f);
			CompassRegion.Top.Set(200, 0f);
			CompassRegion.Width.Set(600, 0f);
			CompassRegion.Height.Set(600, 0f);

			GearRegion = new UIElement();//UIImage(Request<Texture2D>("StarsAbove/UI/CelestalCartography/CelestialCompass"));
			GearRegion.Left.Set(116, 0f);
			GearRegion.Top.Set(310, 0);
			GearRegion.Width.Set(600, 0f);
			GearRegion.Height.Set(600, 0f);

			exit = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Reset"));
			exit.OnLeftClick += ExitMenu;
			exit.Width.Set(70, 0f);
			exit.Height.Set(52, 0f);
			exit.Left.Set(152, 0f);
			exit.Top.Set(80, 0);

			Home = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Home"));
			Home.OnLeftClick += TeleportHome;
			Home.Width.Set(80, 0f);
			Home.Height.Set(80, 0f);
			Home.Left.Set(0, 0f);
			Home.Top.Set(0, 0f);

			Observatory = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Observatory"));
			Observatory.OnLeftClick += TeleportObservatory;
			Observatory.Width.Set(80, 0f);
			Observatory.Height.Set(80, 0f);
			Observatory.Left.Set(0, 0f);
			Observatory.Top.Set(0, 0f);

			CygnusAsteroids = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/CygnusAsteroids"));
			CygnusAsteroids.OnLeftClick += TeleportCygnus;
			CygnusAsteroids.Width.Set(80, 0f);
			CygnusAsteroids.Height.Set(80, 0f);
			CygnusAsteroids.Left.Set(0, 0f);
			CygnusAsteroids.Top.Set(0, 0f);

			Antlia = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Antlia"));
			//Antlia.OnClick += TeleportAntlia;
			Antlia.Width.Set(80, 0f);
			Antlia.Height.Set(80, 0f);
			Antlia.Left.Set(0, 0f);
			Antlia.Top.Set(0, 0f);

			Aquarius = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Aquarius"));
			//Aquarius.OnClick += TeleportAquarius;
			Aquarius.Width.Set(80, 0f);
			Aquarius.Height.Set(80, 0f);
			Aquarius.Left.Set(0, 0f);
			Aquarius.Top.Set(0, 0f);

			Caelum = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Caelum"));
			Caelum.OnLeftClick += TeleportCaelum;
			Caelum.Width.Set(80, 0f);
			Caelum.Height.Set(80, 0f);
			Caelum.Left.Set(0, 0f);
			Caelum.Top.Set(0, 0f);

			Lyra = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Lyra"));
			Lyra.OnLeftClick += TeleportLyra;
			Lyra.Width.Set(80, 0f);
			Lyra.Height.Set(80, 0f);
			Lyra.Left.Set(0, 0f);
			Lyra.Top.Set(0, 0f);

			Pyxis = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Pyxis"));
			Pyxis.OnLeftClick += TeleportPyxis;
			Pyxis.Width.Set(80, 0f);
			Pyxis.Height.Set(80, 0f);
			Pyxis.Left.Set(0, 0f);
			Pyxis.Top.Set(0, 0f);

			MiningStationAries = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/MiningStationAries"));
			MiningStationAries.OnLeftClick += TeleportAries;
			MiningStationAries.Width.Set(80, 0f);
			MiningStationAries.Height.Set(80, 0f);
			MiningStationAries.Left.Set(0, 0f);
			MiningStationAries.Top.Set(0, 0f);

			Scorpius = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Scorpius"));
			Scorpius.OnLeftClick += TeleportScorpius;
			Scorpius.Width.Set(80, 0f);
			Scorpius.Height.Set(80, 0f);
			Scorpius.Left.Set(0, 0f);
			Scorpius.Top.Set(0, 0f);

			Serpens = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Serpens"));
			Serpens.OnLeftClick += TeleportSerpens;
			Serpens.Width.Set(80, 0f);
			Serpens.Height.Set(80, 0f);
			Serpens.Left.Set(0, 0f);
			Serpens.Top.Set(0, 0f);

			Tucana = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Tucana"));
			Tucana.OnLeftClick += TeleportTucana;
			Tucana.Width.Set(80, 0f);
			Tucana.Height.Set(80, 0f);
			Tucana.Left.Set(0, 0f);
			Tucana.Top.Set(0, 0f);

			Corvus = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Corvus"));
			Corvus.OnLeftClick += TeleportCorvus;
			Corvus.Width.Set(80, 0f);
			Corvus.Height.Set(80, 0f);
			Corvus.Left.Set(0, 0f);
			Corvus.Top.Set(0, 0f);

			//New v1.5 Locations
			DreamingCity = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/DreamingCity"));
			DreamingCity.OnLeftClick += TeleportDreamingCity;
			DreamingCity.Width.Set(80, 0f);
			DreamingCity.Height.Set(80, 0f);
			DreamingCity.Left.Set(0, 0f);
			DreamingCity.Top.Set(0, 0f);
			Theranhad = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Theranhad"));
			Theranhad.OnLeftClick += TeleportTheranhad;
			Theranhad.Width.Set(80, 0f);
			Theranhad.Height.Set(80, 0f);
			Theranhad.Left.Set(0, 0f);
			Theranhad.Top.Set(0, 0f);
			FaintArchives = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/FaintArchives"));
			FaintArchives.OnLeftClick += TeleportFaintArchives;
			FaintArchives.Width.Set(80, 0f);
			FaintArchives.Height.Set(80, 0f);
			FaintArchives.Left.Set(0, 0f);
			FaintArchives.Top.Set(0, 0f);
			UltraPlant = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/UltraPlant"));
			UltraPlant.OnLeftClick += TeleportUltraPlant;
			UltraPlant.Width.Set(80, 0f);
			UltraPlant.Height.Set(80, 0f);
			UltraPlant.Left.Set(0, 0f);
			UltraPlant.Top.Set(0, 0f);
			Celestia = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Celestia"));
			Celestia.OnLeftClick += TeleportCelestia;
			Celestia.Width.Set(80, 0f);
			Celestia.Height.Set(80, 0f);
			Celestia.Left.Set(0, 0f);
			Celestia.Top.Set(0, 0f);

			QuestionMark = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/QuestionMark"));
			//QuestionMark.OnClick += TeleportTucana;
			QuestionMark.Width.Set(80, 0f);
			QuestionMark.Height.Set(80, 0f);
			QuestionMark.Left.Set(0, 0f);
			QuestionMark.Top.Set(0, 0f);

			titleText = new UIText("", 1.2f); // text to show stat
			titleText.Width.Set(1, 0f);
			titleText.Height.Set(1, 0f);
			titleText.Top.Set(0, 0f);
			titleText.Left.Set(0, 0f);

			descriptionText = new UIText("", 1f); // text to show stat
			descriptionText.Width.Set(1, 0f);
			descriptionText.Height.Set(1, 0f);
			descriptionText.Top.Set(0, 0f);
			descriptionText.Left.Set(0, 0f);

			requirementText = new UIText("", 0.8f); // text to show stat
			requirementText.Width.Set(1, 0f);
			requirementText.Height.Set(1, 0f);
			requirementText.Top.Set(0, 0f);
			requirementText.Left.Set(0, 0f);

			threatText = new UIText("", 0.8f); // text to show stat
			threatText.Width.Set(1, 0f);
			threatText.Height.Set(1, 0f);
			threatText.Top.Set(0, 0f);
			threatText.Left.Set(0, 0f);

			lootText = new UIText("", 0.8f); // text to show stat
			lootText.Width.Set(1, 0f);
			lootText.Height.Set(1, 0f);
			lootText.Top.Set(0, 0f);
			lootText.Left.Set(0, 0f);


			area.Append(Starmap);
			area.Append(CompassRegion);
			area.Append(GearRegion);
			area.Append(StarmapStars);

			area.Append(Home);
			area.Append(Observatory);
			area.Append(CygnusAsteroids);
			area.Append(Antlia);
			area.Append(Aquarius);
			area.Append(Caelum);
			area.Append(Lyra);
			area.Append(Pyxis);
			area.Append(MiningStationAries);
			area.Append(Scorpius);
			area.Append(Serpens);
			area.Append(Tucana);
			area.Append(Corvus);

			area.Append(DreamingCity);
			area.Append(Celestia);
			area.Append(Theranhad);
			area.Append(FaintArchives);
			area.Append(UltraPlant);

			area.Append(QuestionMark);

			area.Append(TextboxRegion);
			area.Append(titleText);
			area.Append(descriptionText);
			area.Append(threatText);
			area.Append(requirementText);
			area.Append(lootText);

			area.Append(exit);
			Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch) {
			if (Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCompassVisibility <= 0f)
				return;

			base.Draw(spriteBatch);
		}
		private void ExitMenu(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;
			Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuActive = true;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaDialogueScrollTimer = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollNumber = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogueScrollTimer = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarArrayMoveIn = -15f;


			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StellarNova.AfterCartographyDialogue.Asphodene", Main.LocalPlayer);
			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerMenuDialogue = LangHelper.GetTextValue($"StarfarerMenuDialogue.StellarNova.AfterCartographyDialogue.Eridani", Main.LocalPlayer);
			}
			//Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().animatedDescription = "";


			// We can do stuff in here!
		}
		private void TeleportHome(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;

			SubworldSystem.Exit();

			Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;
			
			/*if (Main.netMode == NetmodeID.SinglePlayer)
			{

			}
			else
			{
				ModPacket packet = ModContent.GetInstance<StarsAbove>().GetPacket();
				packet.Write((byte)0); // id
				packet.Write("Home"); // message
				packet.Send();
			}
			*/

		}
		private void TeleportObservatory(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;

			if (Main.netMode == NetmodeID.MultiplayerClient && !DisableMultiplayerCompatibility)
			{
				if(!Main.LocalPlayer.HasBuff(BuffType<SubworldLootCooldown>()) && Main.myPlayer == Main.LocalPlayer.whoAmI)
                {
					Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ModContent.ItemType<CelestriadRoot>());
					Main.LocalPlayer.AddBuff(BuffType<SubworldLootCooldown>(), 36000);
					Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.MultiplayerCompatibility"), 215, 215, 255);
				}
				else
                {
					Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.LootCooldown"), 215, 215, 255);
				}
				return;

			}

			if(Main.netMode == NetmodeID.SinglePlayer)
            {
				SubworldSystem.Enter("StarsAbove/Observatory");

			}
			else
            {
				ModPacket packet = ModContent.GetInstance<StarsAbove>().GetPacket();
				packet.Write((byte)0); // id
				packet.Write("Observatory"); // message
				packet.Send();
			}
			
			
			
			Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;

		}

		private void TeleportCygnus(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;

			
			if(Main.LocalPlayer.HasBuff(BuffType<PortalReady>()) || Main.LocalPlayer.HasBuff(BuffType<StellaglyphReady>()))
            {
				
				if (Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().stellaglyphTier >= 1)
				{
					if (Main.netMode == NetmodeID.MultiplayerClient && !DisableMultiplayerCompatibility)
					{
						if (!Main.LocalPlayer.HasBuff(BuffType<SubworldLootCooldown>()) && Main.myPlayer == Main.LocalPlayer.whoAmI)
						{
							Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ModContent.ItemType<PrismaticCore>(), 5);
							Main.LocalPlayer.AddBuff(BuffType<SubworldLootCooldown>(), 36000);
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.MultiplayerCompatibility"), 215, 215, 255);
						}
						else
						{
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.LootCooldown"), 215, 215, 255);
						}
						return;

					}
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						SubworldSystem.Enter("StarsAbove/CygnusAsteroids");

					}
					else
					{
						ModPacket packet = ModContent.GetInstance<StarsAbove>().GetPacket();
						packet.Write((byte)0); // id
						packet.Write("CygnusAsteroids"); // message
						packet.Send();
					}
					Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;

				}
				else
                {
					if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
					{
						Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.WeakStellaglyph"), 255, 126, 114);
					}
				}
				
			}
			else
            {
				if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
				{
					Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.NoPortal"), 255, 126, 114);
				}
			}
			

		}
		private void TeleportAries(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;

			if (NPC.LunarApocalypseIsUp)
			{
				if (Main.netMode != NetmodeID.Server) { Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.LunarEvents"), 255, 255, 100); }
				return;
			}

			if (Main.LocalPlayer.HasBuff(BuffType<PortalReady>()) || Main.LocalPlayer.HasBuff(BuffType<StellaglyphReady>()))
			{
				if (Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().stellaglyphTier >= 1)
				{
					if (Main.netMode == NetmodeID.MultiplayerClient && !DisableMultiplayerCompatibility)
					{
						if (!Main.LocalPlayer.HasBuff(BuffType<SubworldLootCooldown>()) && Main.myPlayer == Main.LocalPlayer.whoAmI)
						{
							Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ModContent.ItemType<PrismaticCore>(), 5);
							Main.LocalPlayer.AddBuff(BuffType<SubworldLootCooldown>(), 36000);
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.MultiplayerCompatibility"), 215, 215, 255);
						}
						else
						{
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.LootCooldown"), 215, 215, 255);
						}
						return;

					}
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						SubworldSystem.Enter("StarsAbove/MiningStationAries");

					}
					else
					{
						ModPacket packet = ModContent.GetInstance<StarsAbove>().GetPacket();
						packet.Write((byte)0); // id
						packet.Write("MiningStationAries"); // message
						packet.Send();
					}
					Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;

				}
				else
				{
					if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
					{
						Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.WeakStellaglyph"), 255, 126, 114);
					}
				}

			}
			else
			{
				if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
				{
					Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.NoPortal"), 255, 126, 114);
				}
			}


		}
		private void TeleportCaelum(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;


			if (Main.LocalPlayer.HasBuff(BuffType<PortalReady>()) || Main.LocalPlayer.HasBuff(BuffType<StellaglyphReady>()))
			{
				if (Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().stellaglyphTier >= 2)//Tier 2
				{
					if (Main.netMode == NetmodeID.MultiplayerClient && !DisableMultiplayerCompatibility)
					{
						if (!Main.LocalPlayer.HasBuff(BuffType<SubworldLootCooldown>()) && Main.myPlayer == Main.LocalPlayer.whoAmI)
						{
							Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ModContent.ItemType<InertShard>(), 5);
							Main.LocalPlayer.AddBuff(BuffType<SubworldLootCooldown>(), 36000);
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.MultiplayerCompatibility"), 215, 215, 255);
						}
						else
						{
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.LootCooldown"), 215, 215, 255);
						}
						return;

					}
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						SubworldSystem.Enter("StarsAbove/BleachedPlanet");

					}
					else
					{
						ModPacket packet = ModContent.GetInstance<StarsAbove>().GetPacket();
						packet.Write((byte)0); // id
						packet.Write("BleachedPlanet"); // message
						packet.Send();
					}
					Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;

				}
				else
				{
					if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
					{
						Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.WeakStellaglyph"), 255, 126, 114);
					}
				}

			}
			else
			{
				if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
				{
					Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.NoPortal"), 255, 126, 114);
				}
			}


		}
		private void TeleportSerpens(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;


			if (Main.LocalPlayer.HasBuff(BuffType<PortalReady>()) || Main.LocalPlayer.HasBuff(BuffType<StellaglyphReady>()))
			{
				if (Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().stellaglyphTier >= 2)//Tier 2
				{
					if (Main.netMode == NetmodeID.MultiplayerClient && !DisableMultiplayerCompatibility)
					{
						if (!Main.LocalPlayer.HasBuff(BuffType<SubworldLootCooldown>()) && Main.myPlayer == Main.LocalPlayer.whoAmI)
						{
							Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ItemID.VilePowder, 15);
							Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ItemID.RottenChunk, 15);

							Main.LocalPlayer.AddBuff(BuffType<SubworldLootCooldown>(), 36000);
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.MultiplayerCompatibility"), 215, 215, 255);
						}
						else
						{
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.LootCooldown"), 215, 215, 255);
						}
						return;

					}
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						SubworldSystem.Enter("StarsAbove/Serpens");

					}
					else
					{
						ModPacket packet = ModContent.GetInstance<StarsAbove>().GetPacket();
						packet.Write((byte)0); // id
						packet.Write("Serpens"); // message
						packet.Send();
					}
					Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;

				}
				else
				{
					if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
					{
						Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.WeakStellaglyph"), 255, 126, 114);
					}
				}

			}
			else
			{
				if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
				{
					Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.NoPortal"), 255, 126, 114);
				}
			}


		}
		private void TeleportScorpius(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;


			if (Main.LocalPlayer.HasBuff(BuffType<PortalReady>()) || Main.LocalPlayer.HasBuff(BuffType<StellaglyphReady>()))
			{
				if (Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().stellaglyphTier >= 2)//Tier 2
				{
					if (Main.netMode == NetmodeID.MultiplayerClient && !DisableMultiplayerCompatibility)
					{
						if (!Main.LocalPlayer.HasBuff(BuffType<SubworldLootCooldown>()) && Main.myPlayer == Main.LocalPlayer.whoAmI)
						{
							Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ItemID.Vertebrae, 15);
							Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ItemID.ViciousPowder, 15);


							Main.LocalPlayer.AddBuff(BuffType<SubworldLootCooldown>(), 36000);
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.MultiplayerCompatibility"), 215, 215, 255);
						}
						else
						{
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.LootCooldown"), 215, 215, 255);
						}
						return;

					}
					SubworldSystem.Enter<Scorpius>();
					Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;

				}
				else
				{
					if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
					{
						Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.WeakStellaglyph"), 255, 126, 114);
					}
				}

			}
			else
			{
				if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
				{
					Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.NoPortal"), 255, 126, 114);
				}
			}


		}
		private void TeleportTucana(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;


			if (Main.LocalPlayer.HasBuff(BuffType<PortalReady>()) || Main.LocalPlayer.HasBuff(BuffType<StellaglyphReady>()))
			{
				if (Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().stellaglyphTier >= 2)//Tier 2
				{
					if (Main.netMode == NetmodeID.MultiplayerClient && !DisableMultiplayerCompatibility)
					{
						if (!Main.LocalPlayer.HasBuff(BuffType<SubworldLootCooldown>()) && Main.myPlayer == Main.LocalPlayer.whoAmI)
						{
							Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ItemID.JungleSpores, 15);
							Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ItemID.Stinger, 5);

							Main.LocalPlayer.AddBuff(BuffType<SubworldLootCooldown>(), 36000);
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.MultiplayerCompatibility"), 215, 215, 255);
						}
						else
						{
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.LootCooldown"), 215, 215, 255);
						}
						return;

					}
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						SubworldSystem.Enter("StarsAbove/Tucana");

					}
					else
					{
						ModPacket packet = ModContent.GetInstance<StarsAbove>().GetPacket();
						packet.Write((byte)0); // id
						packet.Write("Tucana"); // message
						packet.Send();
					}
					Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;

				}
				else
				{
					if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
					{
						Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.WeakStellaglyph"), 255, 126, 114);
					}
				}

			}
			else
			{
				if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
				{
					Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.NoPortal"), 255, 126, 114);
				}
			}


		}
		private void TeleportCorvus(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;


			if (Main.LocalPlayer.HasBuff(BuffType<PortalReady>()) || Main.LocalPlayer.HasBuff(BuffType<StellaglyphReady>()))
			{
				if (Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().stellaglyphTier >= 2)//Tier 2
				{
					if (Main.netMode == NetmodeID.MultiplayerClient && !DisableMultiplayerCompatibility)
					{
						if (!Main.LocalPlayer.HasBuff(BuffType<SubworldLootCooldown>()) && Main.myPlayer == Main.LocalPlayer.whoAmI)
						{
							Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ModContent.ItemType<BandedTenebrium>(), 5);

							Main.LocalPlayer.AddBuff(BuffType<SubworldLootCooldown>(), 36000);
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.MultiplayerCompatibility"), 215, 215, 255);
						}
						else
						{
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.LootCooldown"), 215, 215, 255);
						}
						return;

					}
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						SubworldSystem.Enter("StarsAbove/Corvus");

					}
					else
					{
						ModPacket packet = ModContent.GetInstance<StarsAbove>().GetPacket();
						packet.Write((byte)0); // id
						packet.Write("Corvus"); // message
						packet.Send();
					}
					Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;

				}
				else
				{
					if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
					{
						Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.WeakStellaglyph"), 255, 126, 114);
					}
				}

			}
			else
			{
				if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
				{
					Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.NoPortal"), 255, 126, 114);
				}
			}


		}
		private void TeleportDreamingCity(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;

			return;
			if (Main.LocalPlayer.HasBuff(BuffType<PortalReady>()) || Main.LocalPlayer.HasBuff(BuffType<StellaglyphReady>()))
			{
				if (Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().stellaglyphTier >= 3)//Tier 3
				{
					if (Main.netMode == NetmodeID.MultiplayerClient && !DisableMultiplayerCompatibility)
					{
						if (!Main.LocalPlayer.HasBuff(BuffType<SubworldLootCooldown>()) && Main.myPlayer == Main.LocalPlayer.whoAmI)
						{
							//Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ModContent.ItemType<BandedTenebrium>(), 5);

							Main.LocalPlayer.AddBuff(BuffType<SubworldLootCooldown>(), 36000);
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.MultiplayerCompatibility"), 215, 215, 255);
						}
						else
						{
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.LootCooldown"), 215, 215, 255);
						}
						return;

					}
					//SubworldSystem.Enter<DreamingCity>();
					Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;

				}
				else
				{
					if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
					{
						Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.WeakStellaglyph"), 255, 126, 114);
					}
				}

			}
			else
			{
				if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
				{
					Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.NoPortal"), 255, 126, 114);
				}
			}


		}
		private void TeleportTheranhad(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;

			return;
			if (Main.LocalPlayer.HasBuff(BuffType<PortalReady>()) || Main.LocalPlayer.HasBuff(BuffType<StellaglyphReady>()))
			{
				if (Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().stellaglyphTier >= 3)//Tier 3
				{
					if (Main.netMode == NetmodeID.MultiplayerClient && !DisableMultiplayerCompatibility)
					{
						if (!Main.LocalPlayer.HasBuff(BuffType<SubworldLootCooldown>()) && Main.myPlayer == Main.LocalPlayer.whoAmI)
						{

							Main.LocalPlayer.AddBuff(BuffType<SubworldLootCooldown>(), 36000);
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.MultiplayerCompatibility"), 215, 215, 255);
						}
						else
						{
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.LootCooldown"), 215, 215, 255);
						}
						return;

					}
					//SubworldSystem.Enter<Corvus>();
					Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;

				}
				else
				{
					if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
					{
						Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.WeakStellaglyph"), 255, 126, 114);
					}
				}

			}
			else
			{
				if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
				{
					Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.NoPortal"), 255, 126, 114);
				}
			}


		}
		private void TeleportFaintArchives(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;

			return;
			if (Main.LocalPlayer.HasBuff(BuffType<PortalReady>()) || Main.LocalPlayer.HasBuff(BuffType<StellaglyphReady>()))
			{
				if (Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().stellaglyphTier >= 3)//Tier 3
				{
					if (Main.netMode == NetmodeID.MultiplayerClient && !DisableMultiplayerCompatibility)
					{
						if (!Main.LocalPlayer.HasBuff(BuffType<SubworldLootCooldown>()) && Main.myPlayer == Main.LocalPlayer.whoAmI)
						{

							Main.LocalPlayer.AddBuff(BuffType<SubworldLootCooldown>(), 36000);
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.MultiplayerCompatibility"), 215, 215, 255);
						}
						else
						{
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.LootCooldown"), 215, 215, 255);
						}
						return;

					}
					//SubworldSystem.Enter<Corvus>();
					Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;

				}
				else
				{
					if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
					{
						Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.WeakStellaglyph"), 255, 126, 114);
					}
				}

			}
			else
			{
				if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
				{
					Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.NoPortal"), 255, 126, 114);
				}
			}


		}
		private void TeleportCelestia(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;

			return;
			if (Main.LocalPlayer.HasBuff(BuffType<PortalReady>()) || Main.LocalPlayer.HasBuff(BuffType<StellaglyphReady>()))
			{
				if (Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().stellaglyphTier >= 2)//Tier 2
				{
					if (Main.netMode == NetmodeID.MultiplayerClient && !DisableMultiplayerCompatibility)
					{
						if (!Main.LocalPlayer.HasBuff(BuffType<SubworldLootCooldown>()) && Main.myPlayer == Main.LocalPlayer.whoAmI)
						{

							Main.LocalPlayer.AddBuff(BuffType<SubworldLootCooldown>(), 36000);
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.MultiplayerCompatibility"), 215, 215, 255);
						}
						else
						{
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.LootCooldown"), 215, 215, 255);
						}
						return;

					}
					//SubworldSystem.Enter<Corvus>();
					Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;

				}
				else
				{
					if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
					{
						Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.WeakStellaglyph"), 255, 126, 114);
					}
				}

			}
			else
			{
				if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
				{
					Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.NoPortal"), 255, 126, 114);
				}
			}


		}
		private void TeleportUltraPlant(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;

			return;
			if (Main.LocalPlayer.HasBuff(BuffType<PortalReady>()) || Main.LocalPlayer.HasBuff(BuffType<StellaglyphReady>()))
			{
				if (Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().stellaglyphTier >= 2)//Tier 2
				{
					if (Main.netMode == NetmodeID.MultiplayerClient && !DisableMultiplayerCompatibility)
					{
						if (!Main.LocalPlayer.HasBuff(BuffType<SubworldLootCooldown>()) && Main.myPlayer == Main.LocalPlayer.whoAmI)
						{

							Main.LocalPlayer.AddBuff(BuffType<SubworldLootCooldown>(), 36000);
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.MultiplayerCompatibility"), 215, 215, 255);
						}
						else
						{
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.LootCooldown"), 215, 215, 255);
						}
						return;

					}
					//SubworldSystem.Enter<Corvus>();
					Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;

				}
				else
				{
					if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
					{
						Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.WeakStellaglyph"), 255, 126, 114);
					}
				}

			}
			else
			{
				if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
				{
					Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.NoPortal"), 255, 126, 114);
				}
			}


		}
		private void TeleportPyxis(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;


			if (Main.LocalPlayer.HasBuff(BuffType<PortalReady>()) || Main.LocalPlayer.HasBuff(BuffType<StellaglyphReady>()))
			{
				if (Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().stellaglyphTier >= 2)//Tier 2
				{
					if (Main.netMode == NetmodeID.MultiplayerClient && !DisableMultiplayerCompatibility)
					{
						if (Main.myPlayer == Main.LocalPlayer.whoAmI)
						{
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.MultiplayerNoLoot"), 215, 215, 255);
							return;

						}
						
					}
					if (Main.netMode == NetmodeID.SinglePlayer)
					{
						SubworldSystem.Enter("StarsAbove/Pyxis");

					}
					else
					{
						ModPacket packet = ModContent.GetInstance<StarsAbove>().GetPacket();
						packet.Write((byte)0); // id
						packet.Write("Pyxis"); // message
						packet.Send();
					}
					Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;
				}
				else
				{
					if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
					{
						Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.WeakStellaglyph"), 255, 126, 114);
					}
				}

			}
			else
			{
				if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
				{
					Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.NoPortal"), 255, 126, 114);
				}
			}


		}
		private void TeleportLyra(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;


			if (Main.LocalPlayer.HasBuff(BuffType<PortalReady>()) || Main.LocalPlayer.HasBuff(BuffType<StellaglyphReady>()))
			{
				if (Main.LocalPlayer.HasBuff(BuffType<VoyageCooldown>()))
				{
					if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
					{
						Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.AnomalyCooldown"), 255, 126, 114);
					}

				}
				else
                {
					if (Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().stellaglyphTier >= 3)//Tier 3
					{
						if (Main.netMode == NetmodeID.MultiplayerClient && !DisableMultiplayerCompatibility)
						{
							if (!Main.LocalPlayer.HasBuff(BuffType<SubworldLootCooldown>()) && Main.myPlayer == Main.LocalPlayer.whoAmI)
							{
								Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ModContent.ItemType<Items.Consumables.DemonicCrux>(), 1);
								Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_GiftOrReward(), ModContent.ItemType<Items.Accessories.AlienCoral>(), 1);

								Main.LocalPlayer.AddBuff(BuffType<SubworldLootCooldown>(), 36000);
								Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.MultiplayerCompatibility"), 215, 215, 255);
							}
							else
							{
								Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.LootCooldown"), 215, 215, 255);
							}
							return;

						}
						if (Main.netMode == NetmodeID.SinglePlayer)
						{
							SubworldSystem.Enter("StarsAbove/Lyra");

						}
						else
						{
							ModPacket packet = ModContent.GetInstance<StarsAbove>().GetPacket();
							packet.Write((byte)0); // id
							packet.Write("Lyra"); // message
							packet.Send();
						}
						Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;
						Main.LocalPlayer.AddBuff(BuffType<VoyageCooldown>(), 108000);

					}
					else
					{
						if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
						{
							Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.WeakStellaglyph"), 255, 126, 114);
						}
					}
				}
				

			}
			else
			{
				if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI)
				{
					Main.NewText(LangHelper.GetTextValue($"CosmicVoyages.Warnings.NoPortal"), 255, 126, 114);
				}
			}


		}
		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);
			var modPlayer = Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>();
			UI.StarfarerMenu.StarfarerMenu.AdjustAreaBasedOnPlayerVelocity(ref area, 60,0);

			Rectangle starmapHitbox = Starmap.GetInnerDimensions().ToRectangle();
			Rectangle hitbox = CompassRegion.GetInnerDimensions().ToRectangle();
			Rectangle gearHitbox = GearRegion.GetInnerDimensions().ToRectangle();

			Vector2 compassCenter = new Vector2(CompassRegion.GetInnerDimensions().Width, CompassRegion.GetInnerDimensions().Height)/2;
			Vector2 gearCenter = new Vector2(GearRegion.GetInnerDimensions().Width/2, GearRegion.GetInnerDimensions().Height / 2) ;
			/*spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassCenter"),
				hitbox,
				Color.White * (modPlayer.CelestialCompassVisibility));*/
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/Starmap"), starmapHitbox, Color.White * modPlayer.CelestialCompassVisibility);


			Starmap.Color = Color.White * (modPlayer.CelestialCompassVisibility);
			
			StarmapStars.Color = Color.White * (modPlayer.StarmapStarsAlpha);

			//This will change based on your selected location!
			if (modPlayer.locationMapName != "")
			{ 
				if (Observatory.IsMouseHovering)
				{
						spriteBatch.Draw(
					(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassObservatory"),
					hitbox,
					null,
					Color.White * (modPlayer.CelestialCompassVisibility),
					MathHelper.ToRadians(0),
					compassCenter,
					SpriteEffects.None,
					1f);
				}
				if (Home.IsMouseHovering)
				{
					spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassHome"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
				}
				if (CygnusAsteroids.IsMouseHovering)
				{
					spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassCygnusAsteroids"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
				}
				if (Lyra.IsMouseHovering)
				{
					spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassLyra"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
				}
				if (Pyxis.IsMouseHovering)
				{
					spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassPyxis"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
				}
				if (DreamingCity.IsMouseHovering)
				{
					spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassPurplePlanet"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
				}
				if (Theranhad.IsMouseHovering)
				{
					spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassUnknown"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
				}
				if (UltraPlant.IsMouseHovering)
				{
					spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassUltraPlant"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
				}
				if (Celestia.IsMouseHovering)
				{
					spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassWhitePlanet"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
				}
				if (FaintArchives.IsMouseHovering)
				{
					spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassWhitePlanet"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
				}
				if (Serpens.IsMouseHovering)
				{
					spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassCorruption"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
				}
				if (Scorpius.IsMouseHovering)
				{
					spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassCrimson"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
				}
				if (Caelum.IsMouseHovering)
				{
					spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassBleached"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
				}
				if (Aquarius.IsMouseHovering)
				{
					spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassOcean"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
				}
				if (Tucana.IsMouseHovering)
				{
					spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassJungle"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
				}
				if (Corvus.IsMouseHovering)
				{
					spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassCorvus"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
				}
				if (Antlia.IsMouseHovering)
				{
					spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassGasGiant"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
				}
				if (MiningStationAries.IsMouseHovering)
				{
					spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassCave"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
				}
				if (QuestionMark.IsMouseHovering)
				{
					spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassEmpty"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
				}
			}
			else
            {
				spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLocations/CompassEmpty"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
			}

			


			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/RotationRing"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(modPlayer.CelestialCompassRotation + modPlayer.CelestialCompassInitialVelocity),//Very mechanical animation, ends on neutral
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/Gear"),
				gearHitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(modPlayer.CelestialCompassRotation - modPlayer.CelestialCompassInitialVelocity),//Very mechanical animation, ends on neutral
				gearCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLowerBase"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),//Static
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassTopBase"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(modPlayer.CelestialCompassRotation - modPlayer.CelestialCompassInitialVelocity),//Very mechanical animation, ends on neutral
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/RotatingPiece1"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(modPlayer.CelestialCompassRotation - modPlayer.CelestialCompassInitialVelocity),//
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/Telescope"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians((modPlayer.CelestialCompassRotation - modPlayer.CelestialCompassInitialVelocity) - 25),//
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/Pointer"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(-modPlayer.CelestialCompassRotation2 + 150),//Very fluid animation. Subtract/add a static number to set position.
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/OrbitingPin"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(modPlayer.CelestialCompassRotation3),//Constant orbit.
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/OrbitingPin"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(modPlayer.CelestialCompassRotation3 + 180),//Constant orbit + offset.
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/LowerPiece1"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(modPlayer.CelestialCompassRotation - modPlayer.CelestialCompassInitialVelocity),
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/RingFrames/RingFrame" + modPlayer.CelestialCompassFrame),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationDescriptionTextBox"), TextboxRegion.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.locationDescriptionAlpha);

			if (!SubworldSystem.AnyActive<StarsAbove>())
			{
				Main.spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Home"), Home.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CelestialCompassVisibility);

			}
			if (SubworldSystem.IsActive<Observatory>())
			{
				Main.spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Observatory"), Observatory.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CelestialCompassVisibility);

			}
			if (SubworldSystem.IsActive<CygnusAsteroids>())
			{
				Main.spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/CygnusAsteroids"), CygnusAsteroids.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CelestialCompassVisibility);

			}
			if (SubworldSystem.IsActive<MiningStationAries>())
			{
				Main.spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/MiningStationAries"), MiningStationAries.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CelestialCompassVisibility);

			}
			if (SubworldSystem.IsActive<Tucana>())
			{
				Main.spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Tucana"), Tucana.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CelestialCompassVisibility);

			}
			if (SubworldSystem.IsActive<Corvus>())
			{
				Main.spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Corvus"), Corvus.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CelestialCompassVisibility);

			}
			if (SubworldSystem.IsActive<Lyra>())
			{
				Main.spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Lyra"), Lyra.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CelestialCompassVisibility);

			}
			if (SubworldSystem.IsActive<Pyxis>())
			{
				Main.spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Pyxis"), Pyxis.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CelestialCompassVisibility);

			}
			if (SubworldSystem.IsActive<Serpens>())
			{
				Main.spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Serpens"), Serpens.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CelestialCompassVisibility);

			}
			if (SubworldSystem.IsActive<BleachedPlanet>())
			{
				Main.spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Caelum"), Caelum.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CelestialCompassVisibility);

			}
			if (SubworldSystem.IsActive<Scorpius>())
			{
				Main.spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Scorpius"), Scorpius.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CelestialCompassVisibility);

			}
			Recalculate();
		}
		

		public override void Update(GameTime gameTime) {
			if (Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCompassVisibility <= 0f)
			{
				area.Remove();
				return;

			}
			else
			{
				Append(area);
			}
			var modPlayer = Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>();
			//Icons are here because they can be easily moved with hot reloads
			//reset
			modPlayer.locationMapName = "";
			modPlayer.locationDescription = "";
			modPlayer.locationLoot = "";
			modPlayer.locationThreat = "";
			modPlayer.locationRequirement = "";

			Home.Top.Set(320, 0);
			Home.Left.Set(700, 0);
			if(Home.IsMouseHovering)
            {
				string location = "Home";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");

			}

			Observatory.Top.Set(300, 0);
			Observatory.Left.Set(645, 0);
			Observatory.Width.Set(80, 0);
			if(Observatory.IsMouseHovering)
            {
				string location = "Observatory";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");
			}
			
			CygnusAsteroids.Top.Set(380, 0);
			CygnusAsteroids.Left.Set(500, 0);
			if (CygnusAsteroids.IsMouseHovering)
			{
				string location = "CygnusAsteroids";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");
			}

			//Antlia.Top.Set(380, 0);
			//Antlia.Left.Set(750, 0);
			Antlia.Top.Set(444, 0);
			Antlia.Left.Set(400, 0);
			if (Antlia.IsMouseHovering)
			{
				string location = "GasGiant";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");

			}

			Aquarius.Top.Set(310, 0);
			Aquarius.Left.Set(810, 0);
			if (Aquarius.IsMouseHovering)
			{
				string location = "WaterPlanet";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");

			}
			
			Caelum.Top.Set(480, 0);
			Caelum.Left.Set(670, 0);
			if (Caelum.IsMouseHovering)
			{
				string location = "BleachedPlanet";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");

			}

			Lyra.Top.Set(180, 0);
			Lyra.Left.Set(750, 0);
			if (Lyra.IsMouseHovering)
			{
				string location = "AlienPlanet";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");

			}

			Pyxis.Top.Set(380, 0);
			Pyxis.Left.Set(350, 0);
			if (Pyxis.IsMouseHovering)
			{
				string location = "Pyxis";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");

			}

			MiningStationAries.Top.Set(450, 0);
			MiningStationAries.Left.Set(530, 0);
			if (MiningStationAries.IsMouseHovering)
			{
				string location = "MiningStation";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");

			}

			Scorpius.Top.Set(360, 0);
			Scorpius.Left.Set(850, 0);
			if (Scorpius.IsMouseHovering)
			{
				string location = "CrimsonPlanet";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");

			}

			Serpens.Top.Set(420, 0);
			Serpens.Left.Set(630, 0);
			if (Serpens.IsMouseHovering)
			{
				string location = "CorruptedPlanet";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");

			}

			Tucana.Top.Set(350, 0);
			Tucana.Left.Set(550, 0);
			if (Tucana.IsMouseHovering)
			{
				string location = "JunglePlanet";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");

			}

			//Corvus.Top.Set(444, 0);
			//Corvus.Left.Set(400, 0);
			Corvus.Top.Set(410, 0);
			Corvus.Left.Set(770, 0);
			if (Corvus.IsMouseHovering)
			{
				string location = "Corvus";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");

			}

			DreamingCity.Top.Set(260, 0);
			DreamingCity.Left.Set(490, 0);
			if (DreamingCity.IsMouseHovering)
			{
				string location = "DreamingCity";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");

			}

			Theranhad.Top.Set(90, 0);
			Theranhad.Left.Set(710, 0);
			if (Theranhad.IsMouseHovering)
			{
				string location = "Theranhad";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");

			}

			FaintArchives.Top.Set(310, 0);
			FaintArchives.Left.Set(270, 0);
			if (FaintArchives.IsMouseHovering)
			{
				string location = "FaintArchives";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");

			}

			UltraPlant.Top.Set(460, 0);
			UltraPlant.Left.Set(260, 0);
			if (UltraPlant.IsMouseHovering)
			{
				string location = "UltraPlant";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");

			}

			Celestia.Top.Set(130, 0);
			Celestia.Left.Set(610, 0);
			if (Celestia.IsMouseHovering)
			{
				string location = "Celestia";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");

			}

			QuestionMark.Top.Set(150, 0);
			QuestionMark.Left.Set(350, 0);
			if (QuestionMark.IsMouseHovering)
			{
				string location = "QuestionMark";
				modPlayer.locationMapName = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Name");
				modPlayer.locationDescription = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Description");
				modPlayer.locationThreat = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Threat");
				modPlayer.locationRequirement = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Requirement");
				modPlayer.locationLoot = LangHelper.GetTextValue("CosmicVoyages.MapText." + location + ".Loot");

			}

			CompassRegion.Top.Set(240 - modPlayer.locationDescriptionAlpha * 100 - modPlayer.quadraticFloat*4, 0);
			GearRegion.Top.Set(350 - modPlayer.locationDescriptionAlpha * 100 - modPlayer.quadraticFloat*4, 0);
			//area.Left.Set(60, 0);
			//Starmap.Left.Set(80, 0);
			

			TextboxRegion.Top.Set(250, 0);
			TextboxRegion.Left.Set(-195, 0);

			titleText.SetText($"{modPlayer.locationMapName}");
			titleText.Top.Set(280, 0);
			titleText.Left.Set(-160,0);

			descriptionText.SetText(LangHelper.Wrap($"{modPlayer.locationDescription}", 40));
			descriptionText.Top.Set(310, 0);
			descriptionText.Left.Set(-160, 0);

			threatText.SetText(LangHelper.Wrap($"{modPlayer.locationThreat}", 50));
			threatText.Top.Set(524, 0);
			threatText.Left.Set(-135, 0);

			requirementText.SetText(LangHelper.Wrap($"{modPlayer.locationRequirement}", 50));
			requirementText.Top.Set(546, 0);
			requirementText.Left.Set(-135, 0);

			lootText.SetText(LangHelper.Wrap($"{modPlayer.locationLoot}", 50));
			lootText.Top.Set(568, 0);
			lootText.Left.Set(-135, 0);

			base.Update(gameTime);
		}

	}
}
