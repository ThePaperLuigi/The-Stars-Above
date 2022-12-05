
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Subworlds;
using StarsAbove.Utilities;
using SubworldLibrary;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI.CelestialCartography
{
    internal class CelestialCompass : UIState
	{
		
		private UIText text;
		private UIElement area;

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
		private UIImageButton MiningStationAries;
		private UIImageButton Scorpius;
		private UIImageButton Serpens;
		private UIImageButton Tucana;

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.


			area = new UIElement();
			area.Top.Set(0, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(1000, 0f);
			area.Height.Set(700, 0f);
			area.HAlign = area.VAlign = 0.5f; // 1

			Starmap = new UIImage(Request<Texture2D>("StarsAbove/UI/CelestialCartography/Starmap"));
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
			exit.OnClick += ExitMenu;
			exit.Width.Set(70, 0f);
			exit.Height.Set(52, 0f);
			exit.Left.Set(152, 0f);
			exit.Top.Set(520, 0f);

			Home = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Home"));
			Home.OnClick += TeleportHome;
			Home.Width.Set(80, 0f);
			Home.Height.Set(80, 0f);
			Home.Left.Set(0, 0f);
			Home.Top.Set(0, 0f);

			Observatory = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Observatory"));
			Observatory.OnClick += TeleportObservatory;
			Observatory.Width.Set(80, 0f);
			Observatory.Height.Set(80, 0f);
			Observatory.Left.Set(0, 0f);
			Observatory.Top.Set(0, 0f);

			CygnusAsteroids = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/CygnusAsteroids"));
			CygnusAsteroids.OnClick += TeleportCygnus;
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
			//Caelum.OnClick += TeleportCaelum;
			Caelum.Width.Set(80, 0f);
			Caelum.Height.Set(80, 0f);
			Caelum.Left.Set(0, 0f);
			Caelum.Top.Set(0, 0f);

			Lyra = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Lyra"));
			//Lyra.OnClick += TeleportLyra;
			Lyra.Width.Set(80, 0f);
			Lyra.Height.Set(80, 0f);
			Lyra.Left.Set(0, 0f);
			Lyra.Top.Set(0, 0f);

			MiningStationAries = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/MiningStationAries"));
			//MiningStationAries.OnClick += TeleportMiningStationAries;
			MiningStationAries.Width.Set(80, 0f);
			MiningStationAries.Height.Set(80, 0f);
			MiningStationAries.Left.Set(0, 0f);
			MiningStationAries.Top.Set(0, 0f);

			Scorpius = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Scorpius"));
			//Scorpius.OnClick += TeleportScorpius;
			Scorpius.Width.Set(80, 0f);
			Scorpius.Height.Set(80, 0f);
			Scorpius.Left.Set(0, 0f);
			Scorpius.Top.Set(0, 0f);

			Serpens = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Serpens"));
			//Serpens.OnClick += TeleportSerpens;
			Serpens.Width.Set(80, 0f);
			Serpens.Height.Set(80, 0f);
			Serpens.Left.Set(0, 0f);
			Serpens.Top.Set(0, 0f);

			Tucana = new UIImageButton(Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Tucana"));
			//Tucana.OnClick += TeleportTucana;
			Tucana.Width.Set(80, 0f);
			Tucana.Height.Set(80, 0f);
			Tucana.Left.Set(0, 0f);
			Tucana.Top.Set(0, 0f);

			text = new UIText("", 1.5f); // text to show stat
			text.Width.Set(1, 0f);
			text.Height.Set(1, 0f);
			text.Top.Set(0, 0f);
			text.Left.Set(0, 0f);

			
			
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
			area.Append(MiningStationAries);
			area.Append(Scorpius);
			area.Append(Serpens);
			area.Append(Tucana);

			area.Append(text);
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

			//Check to see if this works in Multiplayer after the SubLib update comes out.
			Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;
			SubworldSystem.Exit();

		}
		private void TeleportObservatory(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;

			//Check to see if this works in Multiplayer after the SubLib update comes out.
			Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;
			SubworldSystem.Enter<Observatory>();

		}

		private void TeleportCygnus(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0 || !Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
				return;

			//Check to see if this works in Multiplayer after the SubLib update comes out.
			Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive = false;
			//SubworldSystem.Enter<Observatory>();

		}
		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);
			var modPlayer = Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>();

			Rectangle starmapHitbox = Starmap.GetInnerDimensions().ToRectangle();
			Rectangle hitbox = CompassRegion.GetInnerDimensions().ToRectangle();
			Rectangle gearHitbox = GearRegion.GetInnerDimensions().ToRectangle();

			Vector2 compassCenter = new Vector2(CompassRegion.GetInnerDimensions().Width, CompassRegion.GetInnerDimensions().Height)/2;
			Vector2 gearCenter = new Vector2(GearRegion.GetInnerDimensions().Width/2, GearRegion.GetInnerDimensions().Height / 2) ;
			/*spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassCenter"),
				hitbox,
				Color.White * (modPlayer.CelestialCompassVisibility));*/

			Starmap.Color = Color.White * (modPlayer.CelestialCompassVisibility);
			
			StarmapStars.Color = Color.White * (modPlayer.StarmapStarsAlpha);

			//This will change based on your selected location!
			if (modPlayer.locationMapName != "")
			{ 
				if (modPlayer.locationMapName == "Observatory Hyperborea")
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
				if (modPlayer.locationMapName == "Home")
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
				if (modPlayer.locationMapName == "Cygnus Asteroid Field")
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
				if (modPlayer.locationMapName == "Lyra")
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
				if (modPlayer.locationMapName == "Serpens")
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
				if (modPlayer.locationMapName == "Scorpius")
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
				if (modPlayer.locationMapName == "Caelum")
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
				if (modPlayer.locationMapName == "Aquarius")
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
				if (modPlayer.locationMapName == "Tucana")
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
				if (modPlayer.locationMapName == "Antlia")
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
				if (modPlayer.locationMapName == "Mining Station Aries")
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

			if (!SubworldSystem.AnyActive<StarsAbove>())
			{
				Main.spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Home"), Home.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CelestialCompassVisibility);

			}
			if (SubworldSystem.IsActive<Observatory>())
			{
				Main.spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationIcons/Observatory"), Observatory.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CelestialCompassVisibility);

			}
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

			Home.Top.Set(320, 0);
			Home.Left.Set(700, 0);
			if(Home.IsMouseHovering)
            {
				modPlayer.locationMapName = "Home";
				
            }

			Observatory.Top.Set(300, 0);
			Observatory.Left.Set(660, 0);
			Observatory.Width.Set(60, 0);
			if(Observatory.IsMouseHovering)
            {
				modPlayer.locationMapName = "Observatory Hyperborea";
			}
			
			CygnusAsteroids.Top.Set(380, 0);
			CygnusAsteroids.Left.Set(500, 0);
			if (CygnusAsteroids.IsMouseHovering)
			{
				modPlayer.locationMapName = "Cygnus Asteroid Field";
			}

			Antlia.Top.Set(380, 0);
			Antlia.Left.Set(750, 0);
			if (Antlia.IsMouseHovering)
			{
				modPlayer.locationMapName = "Antlia";

			}

			Aquarius.Top.Set(310, 0);
			Aquarius.Left.Set(810, 0);
			if (Aquarius.IsMouseHovering)
			{
				modPlayer.locationMapName = "Aquarius";

			}

			Caelum.Top.Set(480, 0);
			Caelum.Left.Set(670, 0);
			if (Caelum.IsMouseHovering)
			{
				modPlayer.locationMapName = "Caelum";

			}

			Lyra.Top.Set(460, 0);
			Lyra.Left.Set(750, 0);
			if (Lyra.IsMouseHovering)
			{
				modPlayer.locationMapName = "Lyra";

			}

			MiningStationAries.Top.Set(450, 0);
			MiningStationAries.Left.Set(530, 0);
			if (MiningStationAries.IsMouseHovering)
			{
				modPlayer.locationMapName = "Mining Station Aries";

			}

			Scorpius.Top.Set(360, 0);
			Scorpius.Left.Set(850, 0);
			if (Scorpius.IsMouseHovering)
			{
				modPlayer.locationMapName = "Scorpius";

			}

			Serpens.Top.Set(420, 0);
			Serpens.Left.Set(630, 0);
			if (Serpens.IsMouseHovering)
			{
				modPlayer.locationMapName = "Serpens";

			}

			Tucana.Top.Set(350, 0);
			Tucana.Left.Set(550, 0);
			if (Tucana.IsMouseHovering)
			{
				modPlayer.locationMapName = "Tucana";

			}
			//CompassRegion.Top.Set(260, 0);
			
			// Setting the text per tick to update and show our resource values.
			text.SetText($"");
			//text.Left.Set(Main.mouseX - 25, 0f); // Place the resource bar to the left of the hearts.
			//text.Top.Set(Main.mouseY, 0f);
			base.Update(gameTime);
		}
	}
}
