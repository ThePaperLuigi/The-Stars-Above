
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StarsAbove.Menu
{
    public class StarsAboveMainMenu2 : ModMenu
	{
		private const string menuAssetPath = "StarsAbove/Menu"; // Creates a constant variable representing the texture path, so we don't have to write it out multiple Rotation1s

		
		public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>($"StarsAbove/Menu/StarsAboveLogo");

		//public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>($"{menuAssetPath}/Empty");

		//public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>($"{menuAssetPath}/Empty");

		public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/SunsetStardust");

		//public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<NoBackground>();

		public override string DisplayName => "The Stars Above v2.0 Menu";

		int menuTime;
		float flipTime;

		float Rotation1;
		float Rotation2;
		float Rotation3;
		float Rotation4;
		float Rotation5;
		float RotationQuadratic;

		float flipAnimationScale;

		float firstStartZoom = 2;
		float firstStartVelocity = 0.1f;
		int animationTime;
		int animationTime2;
		int animationFrame;
		int animationFrame2;
		float walkFromScreenEdgeToEdge;
		float walkFromScreenEdgeToEdgeEridani;

		float MousePositionFloatX;
		float MousePositionFloatY;

		public float quadraticFloatTimer;
		public float quadraticFloat;

		public float quadraticFloatTimer2;
		public float quadraticFloat2 = 0.5f;

		public override void OnSelected()
		{
			quadraticFloat = 0f;
			quadraticFloat2 = 0.5f;
			menuTime = 0;
			flipTime = 0;
			walkFromScreenEdgeToEdge = -1000;
			walkFromScreenEdgeToEdgeEridani = -1070;
			firstStartZoom = 2;
			firstStartVelocity = 0.1f;

			Rotation1 = 0f;
			Rotation2 = 0f;
			Rotation3 = 0f;
			Rotation4 = 0f;
			Rotation5 = 0f;

			RotationQuadratic = 0f;

			flipAnimationScale = 0f;
			
			SoundEngine.PlaySound(SoundID.Item4);
		}
		public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
		{
			

			walkFromScreenEdgeToEdge++;
			walkFromScreenEdgeToEdgeEridani++;
			menuTime++;
			animationTime++;
			animationTime2++;
			if (animationTime > 9)
            {
				animationTime = 0;
				animationFrame++;
				if (animationFrame > 7)
                {
					animationFrame = 0;
                }


            }
			if (animationTime2 > 10)
			{
				animationTime2 = 0;
				animationFrame2++;
				if (animationFrame2 > 7)
				{
					animationFrame2 = 0;
				}


			}
			flipTime += 0.01f;

			if(flipTime >= 1f)
            {
				flipTime = 0f;
            }

			/*if (Math.Abs(firstStartVelocity) > 0)
			{
				firstStartZoom += firstStartVelocity;
				firstStartVelocity -= 0.01f;
			}*/

			
			

			Rotation1 += 0.01f;
			Rotation2 -= 0.02f;
			Rotation3 += 0.03f;
			Rotation4 += 0.05f;
			Rotation5 -= 0.04f;

			MathHelper.Lerp(firstStartZoom, 0, 1f);
			//Vector2 flipScale = new Vector2(flipAnimationScale, 1);

			Main.time = 27000;
			Main.dayTime = true;

			logoRotation = 0f;
			logoScale = 0.75f;
			drawColor = Color.White;

			Vector2 logoDrawPos = new Vector2(Main.screenWidth / 2, 100f);
			Vector2 centerDrawPos = new Vector2(Main.screenWidth / 2, Main.screenHeight/2);

			Texture2D MenuBG = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/MenuBackground2");//Background
			Texture2D MenuFG = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/MenuForeground2");//Background
			Texture2D ScreenDarkened = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/Vignette");//Background
			Texture2D ShineFX = (Texture2D)ModContent.Request<Texture2D>($"StarsAbove/Menu/ShineFX");//
			Texture2D Starfarers = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/Starfarers");//

			Texture2D WhiteCircle1 = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/WhiteCircle1");//White Circle
			Texture2D WhiteCircle2 = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/WhiteCircle2");//White Circle
			Texture2D WhiteCircle3 = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/WhiteCircle3");//White Circle

			Texture2D MinorCircle1 = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/WhiteCircle3");//White Circle
			Texture2D MinorCircle2 = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/WhiteCircle3");//White Circle

			Texture2D WhiteStar1 = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/WhiteStar1");//

			Texture2D BlueShine = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/BlueShine");//

			Texture2D WhiteLine1 = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/Line1");//White Circle
			Texture2D WhiteLine2 = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/Line2");//White Circle
			Texture2D WhiteLine3 = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/Line3");//White Circle
			Texture2D WhiteLine4 = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/Line4");//White Circle

			Vector2 zero = Vector2.Zero;
			float width = (float)Main.screenWidth / (float)MenuBG.Width;
			float height = (float)Main.screenHeight / (float)MenuBG.Height;
			
			if (width != height)
			{
				if (height > width)
				{
					width = height;
					zero.X -= ((float)MenuBG.Width * width - (float)Main.screenWidth) * 0.5f;
				}
				else
				{
					zero.Y -= ((float)MenuBG.Height * width - (float)Main.screenHeight) * 0.5f;
                }
            }
			spriteBatch.Draw(MenuBG, new Vector2(zero.X + MathHelper.Lerp(-98, -82, MousePositionFloatX), zero.Y + MathHelper.Lerp(-50, -47, MousePositionFloatY)), (Rectangle?)null, Color.White, 0f, Vector2.Zero, width * 1.1f, (SpriteEffects)0, 0f);
			spriteBatch.Draw(
					ShineFX, //The texture being drawn.
					logoDrawPos, //The position of the texture.
					new Rectangle(0, 0, ShineFX.Width, ShineFX.Height),
					Color.White, //The color of the texture.
					MathHelper.ToRadians(Rotation3), // The rotation of the texture.
					ShineFX.Size() * 0.5f, //The centerpoint of the texture.
					2.3f, //The scale of the texture.
					SpriteEffects.None,
					0f);
			spriteBatch.Draw(
				ShineFX, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, ShineFX.Width, ShineFX.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation5), // The rotation of the texture.
				ShineFX.Size() * 0.5f, //The centerpoint of the texture.
				1.8f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			Texture2D AsphoRunAnimation = (Texture2D)ModContent.Request<Texture2D>($"StarsAbove/UI/CelestialCartography/RunAnimation/ARun" + animationFrame + "0");
			Texture2D EriRunAnimation = (Texture2D)ModContent.Request<Texture2D>($"StarsAbove/UI/CelestialCartography/RunAnimation/ERun" + animationFrame2 + "0");

			if (walkFromScreenEdgeToEdge - AsphoRunAnimation.Width > Main.screenWidth)
			{
				walkFromScreenEdgeToEdge = -1000 - AsphoRunAnimation.Width;

			}
			if (walkFromScreenEdgeToEdgeEridani - EriRunAnimation.Width > Main.screenWidth)
			{
				walkFromScreenEdgeToEdgeEridani = -1000 - EriRunAnimation.Width;
			}

			Texture2D Rings = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/Rings");
			
			spriteBatch.Draw(
				Rings, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, Rings.Width, Rings.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1) + MathHelper.ToRadians(190), // The rotation of the texture.
				Rings.Size() * 0.5f, //The centerpoint of the texture.
				0.7f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				Rings, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, Rings.Width, Rings.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation2) + MathHelper.ToRadians(190), // The rotation of the texture.
				Rings.Size() * 0.8f, //The centerpoint of the texture.
				0.8f, //The scale of the texture.
				SpriteEffects.None,
				0f);

            /*
			#region circle
			spriteBatch.Draw(
				WhiteCircle1, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteCircle1.Width, WhiteCircle1.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1), // The rotation of the texture.
				WhiteCircle1.Size() * 0.5f, //The centerpoint of the texture.
				2f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				WhiteCircle2, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteCircle2.Width, WhiteCircle2.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1), // The rotation of the texture.
				WhiteCircle2.Size() * 0.5f, //The centerpoint of the texture.
				2f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				WhiteCircle3, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteCircle3.Width, WhiteCircle3.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1), // The rotation of the texture.
				WhiteCircle3.Size() * 0.5f, //The centerpoint of the texture.
				1.8f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			#endregion


			#region lines
			spriteBatch.Draw(
				WhiteLine1, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteLine1.Width, WhiteLine1.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1), // The rotation of the texture.
				WhiteLine1.Size() * 0.5f, //The centerpoint of the texture.
				1.5f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				WhiteLine2, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteLine2.Width, WhiteLine2.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1), // The rotation of the texture.
				WhiteLine2.Size() * 0.5f, //The centerpoint of the texture.
				1.5f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				WhiteLine3, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteLine3.Width, WhiteLine3.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1), // The rotation of the texture.
				WhiteLine3.Size() * 0.5f, //The centerpoint of the texture.
				1.5f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				WhiteLine4, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteLine4.Width, WhiteLine4.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1), // The rotation of the texture.
				WhiteLine4.Size() * 0.5f, //The centerpoint of the texture.
				1.5f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				WhiteLine4, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteLine4.Width, WhiteLine4.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1 + MathHelper.ToRadians(170)), // The rotation of the texture.
				WhiteLine4.Size() * 0.5f, //The centerpoint of the texture.
				1.5f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				WhiteLine3, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteLine3.Width, WhiteLine3.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1) + MathHelper.ToRadians(90), // The rotation of the texture.
				WhiteLine3.Size() * 0.5f, //The centerpoint of the texture.
				1.5f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			#endregion
			spriteBatch.Draw(
				WhiteStar1, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteStar1.Width, WhiteStar1.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1) + MathHelper.ToRadians(190), // The rotation of the texture.
				WhiteStar1.Size() * 0.5f, //The centerpoint of the texture.
				1.8f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				WhiteStar1, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteStar1.Width, WhiteStar1.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1) + MathHelper.ToRadians(240), // The rotation of the texture.
				WhiteStar1.Size() * 0.5f, //The centerpoint of the texture.
				1.8f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				WhiteStar1, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteStar1.Width, WhiteStar1.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1) + MathHelper.ToRadians(40), // The rotation of the texture.
				WhiteStar1.Size() * 0.5f, //The centerpoint of the texture.
				1.8f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				WhiteStar1, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteStar1.Width, WhiteStar1.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1) + MathHelper.ToRadians(80), // The rotation of the texture.
				WhiteStar1.Size() * 0.5f, //The centerpoint of the texture.
				1.8f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				WhiteStar1, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteStar1.Width, WhiteStar1.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1) + MathHelper.ToRadians(330), // The rotation of the texture.
				WhiteStar1.Size() * 0.5f, //The centerpoint of the texture.
				1.8f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				WhiteStar1, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteStar1.Width, WhiteStar1.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1) + MathHelper.ToRadians(10), // The rotation of the texture.
				WhiteStar1.Size() * 0.5f, //The centerpoint of the texture.
				1.18f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				WhiteStar1, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteStar1.Width, WhiteStar1.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1) + MathHelper.ToRadians(180), // The rotation of the texture.
				WhiteStar1.Size() * 0.5f, //The centerpoint of the texture.
				1.18f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				WhiteStar1, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteStar1.Width, WhiteStar1.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1) + MathHelper.ToRadians(110), // The rotation of the texture.
				WhiteStar1.Size() * 0.5f, //The centerpoint of the texture.
				1.18f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				WhiteStar1, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteStar1.Width, WhiteStar1.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1) + MathHelper.ToRadians(45), // The rotation of the texture.
				WhiteStar1.Size() * 0.5f, //The centerpoint of the texture.
				0.94f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				WhiteStar1, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteStar1.Width, WhiteStar1.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1) + MathHelper.ToRadians(135), // The rotation of the texture.
				WhiteStar1.Size() * 0.5f, //The centerpoint of the texture.
				0.94f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				WhiteStar1, //The texture being drawn.
				logoDrawPos, //The position of the texture.
				new Rectangle(0, 0, WhiteStar1.Width, WhiteStar1.Height),
				Color.White, //The color of the texture.
				MathHelper.ToRadians(Rotation1) + MathHelper.ToRadians(295), // The rotation of the texture.
				WhiteStar1.Size() * 0.5f, //The centerpoint of the texture.
				0.94f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			*/
            spriteBatch.Draw(MenuFG, new Vector2(zero.X + MathHelper.Lerp(-93, -87, MousePositionFloatX), zero.Y + MathHelper.Lerp(-78, -77, MousePositionFloatY)), (Rectangle?)null, Color.White, 0f, Vector2.Zero, width * 1.1f, (SpriteEffects)0, 0f);
            
			Texture2D OceanWaves = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/OceanWaves");
            Texture2D OceanTop = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/OceanTop");
			Texture2D OceanBottom1 = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/OceanBottom1");
			Texture2D OceanBottom2 = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/OceanBottom2");
            Texture2D OceanFog = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/OceanFog");//Background


            //Animate ocean:
            quadraticFloatTimer += 0.0001f;
			quadraticFloat = InOutQuad(Pulse(quadraticFloatTimer));

			if(menuTime > 60)
            {
				quadraticFloatTimer2 += 0.0001f;

			}
			quadraticFloat2 = InOutQuad(Pulse(quadraticFloatTimer2));

			float oceanAdjustment = MathHelper.Lerp(-5, 35, quadraticFloat);
			float oceanAdjustment2 = MathHelper.Lerp(-5, 35, quadraticFloat2);

			spriteBatch.Draw(OceanBottom2, new Vector2(zero.X + MathHelper.Lerp(-93, -87, MousePositionFloatX) + oceanAdjustment2, zero.Y + MathHelper.Lerp(-78, -77, MousePositionFloatY)), (Rectangle?)null, Color.White, 0f, Vector2.Zero, width * 1.1f, (SpriteEffects)0, 0f);
			spriteBatch.Draw(OceanBottom1, new Vector2(zero.X + MathHelper.Lerp(-93, -87, MousePositionFloatX) + oceanAdjustment2, zero.Y + MathHelper.Lerp(-78, -77, MousePositionFloatY)), (Rectangle?)null, Color.White, 0f, Vector2.Zero, width * 1.1f, (SpriteEffects)0, 0f);

			spriteBatch.Draw(OceanTop, new Vector2(zero.X + MathHelper.Lerp(-93, -87, MousePositionFloatX) + oceanAdjustment, zero.Y + MathHelper.Lerp(-78, -77, MousePositionFloatY)), (Rectangle?)null, Color.White, 0f, Vector2.Zero, width * 1.1f, (SpriteEffects)0, 0f);
            spriteBatch.Draw(OceanWaves, new Vector2(zero.X + MathHelper.Lerp(-93, -87, MousePositionFloatX) + oceanAdjustment2, zero.Y + MathHelper.Lerp(-78, -77, MousePositionFloatY)), (Rectangle?)null, Color.White*0.5f, 0f, Vector2.Zero, width * 1.1f, (SpriteEffects)0, 0f);
            spriteBatch.Draw(OceanFog, new Vector2(zero.X + MathHelper.Lerp(-93, -87, MousePositionFloatX), zero.Y + MathHelper.Lerp(-78, -77, MousePositionFloatY)), (Rectangle?)null, Color.White, 0f, Vector2.Zero, width * 1.1f, (SpriteEffects)0, 0f);

            spriteBatch.Draw(Starfarers, new Vector2(zero.X + 12 + MathHelper.Lerp(42, -12, MousePositionFloatX), zero.Y + MathHelper.Lerp(-34, -30, MousePositionFloatY)), (Rectangle?)null, Color.White, 0f, Vector2.Zero, width, (SpriteEffects)0, 0f);

            spriteBatch.Draw(ScreenDarkened, new Vector2(zero.X, zero.Y), (Rectangle?)null, Color.White, 0f, Vector2.Zero, width, (SpriteEffects)0, 0f);

			//Float from 0 to 1 from left to right side of the screen
			MousePositionFloatX = ((Math.Min(Main.screenWidth, Main.MouseScreen.X) - 0) * 100) / (Main.screenWidth - 0) / 100;
			MousePositionFloatY = ((Math.Min(Main.screenHeight, Main.MouseScreen.Y) - 0) * 100) / (Main.screenHeight - 0) / 100;

			if (MousePositionFloatX < 0)
			{
				MousePositionFloatX = 0;
			}
			if (MousePositionFloatY < 0)
			{
				MousePositionFloatY = 0;
			}
			
			
			spriteBatch.Draw(
				AsphoRunAnimation, //The texture being drawn.
				new Vector2(walkFromScreenEdgeToEdge, Main.screenHeight - AsphoRunAnimation.Height / 2), //The position of the texture.
				new Rectangle(0, 0, AsphoRunAnimation.Width, AsphoRunAnimation.Height),
				Color.White, //The color of the texture.
				0, // The rotation of the texture.
				AsphoRunAnimation.Size() * 0.5f, //The centerpoint of the texture.
				1f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			spriteBatch.Draw(
				EriRunAnimation, //The texture being drawn.
				new Vector2(walkFromScreenEdgeToEdgeEridani, Main.screenHeight - EriRunAnimation.Height / 2), //The position of the texture.
				new Rectangle(0, 0, EriRunAnimation.Width, EriRunAnimation.Height),
				Color.White, //The color of the texture.
				0, // The rotation of the texture.
				EriRunAnimation.Size() * 0.5f, //The centerpoint of the texture.
				1f, //The scale of the texture.
				SpriteEffects.None,
				0f);

			if (Language.ActiveCulture.LegacyId == ((int)GameCulture.CultureName.Chinese))
			{
				//Draw over the original logo with the Chinese one
			}
			else
			{

			}

			return true;
		}
		public static float InQuad(float t) => t * t;
		public static float OutQuad(float t) => 1 - InQuad(1 - t);
		public static float InOutQuad(float t)
		{
			if (t < 0.5) return InQuad(t * 2) / 2;
			return 1 - InQuad((1 - t) * 2) / 2;
		}
		float Pulse(float time)
		{
			const float pi = (float)Math.PI;
			const float frequency = 10; // Frequency in Hz
			return (float)(0.5 * (1 + Math.Sin(2 * pi * frequency * time)));
		}

	}
}
