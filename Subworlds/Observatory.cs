using Terraria;
using SubworldLibrary;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using Terraria.ModLoader;

using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using StarsAbove.Systems;

namespace StarsAbove.Subworlds
{
    public class Observatory : Subworld
	{
		public override string Name => "Observatory";

		public override int Width => 1750;
		public override int Height => 750;

		//public override ModWorld modWorld => ModContent.GetInstance < your modworld here>();

		public override bool ShouldSave => false;
		public override bool NoPlayerSaving => false;
		public override bool NormalUpdates => false;

		//public override bool noWorldUpdate => true;
		private const string assetPath = "StarsAbove/Subworlds/LoadingScreens";

		public override void DrawMenu(GameTime gameTime)
        {
			Texture2D MenuBG = (Texture2D)ModContent.Request<Texture2D>($"{assetPath}/DefaultLS");//Background
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
			
			Main.spriteBatch.Draw(MenuBG, zero, (Rectangle?)null, Color.White, 0f, Vector2.Zero, width, (SpriteEffects)0, 0f);
			DrawStarfarerAnimation();

			base.DrawMenu(gameTime);
        }
		int animationTime;
		int animationTime2;
		int animationFrame;
		int animationFrame2;
		private void DrawStarfarerAnimation()
        {
			Texture2D AsphoRunAnimation = (Texture2D)ModContent.Request<Texture2D>($"StarsAbove/UI/CelestialCartography/RunAnimation/ARun" + animationFrame + "0");
			Texture2D EriRunAnimation = (Texture2D)ModContent.Request<Texture2D>($"StarsAbove/UI/CelestialCartography/RunAnimation/ERun" + animationFrame2 + "0");
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
			//Shadow
			Main.spriteBatch.Draw(
				AsphoRunAnimation, //The texture being drawn.
				new Vector2(Main.screenWidth / 2 + 44, Main.screenHeight / 2 - 66), //The position of the texture.
				new Rectangle(0, 0, AsphoRunAnimation.Width, AsphoRunAnimation.Height),
				Color.Black * 0.2f, //The color of the texture.
				0, // The rotation of the texture.
				AsphoRunAnimation.Size() * 0.5f, //The centerpoint of the texture.
				1f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			Main.spriteBatch.Draw(
				EriRunAnimation, //The texture being drawn.
				new Vector2(Main.screenWidth / 2 - 36, Main.screenHeight / 2 - 66), //The position of the texture.
				new Rectangle(0, 0, EriRunAnimation.Width, EriRunAnimation.Height),
				Color.Black * 0.2f, //The color of the texture.
				0, // The rotation of the texture.
				EriRunAnimation.Size() * 0.5f, //The centerpoint of the texture.
				1f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			Main.spriteBatch.Draw(
				AsphoRunAnimation, //The texture being drawn.
				new Vector2(Main.screenWidth/2 + 40, Main.screenHeight / 2 - 70), //The position of the texture.
				new Rectangle(0, 0, AsphoRunAnimation.Width, AsphoRunAnimation.Height),
				Color.White, //The color of the texture.
				0, // The rotation of the texture.
				AsphoRunAnimation.Size() * 0.5f, //The centerpoint of the texture.
				1f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			Main.spriteBatch.Draw(
				EriRunAnimation, //The texture being drawn.
				new Vector2(Main.screenWidth/2 - 40, Main.screenHeight / 2 - 70), //The position of the texture.
				new Rectangle(0, 0, EriRunAnimation.Width, EriRunAnimation.Height),
				Color.White, //The color of the texture.
				0, // The rotation of the texture.
				EriRunAnimation.Size() * 0.5f, //The centerpoint of the texture.
				1f, //The scale of the texture.
				SpriteEffects.None,
				0f);

			
		}
        public override void DrawSetup(GameTime gameTime)
        {



            base.DrawSetup(gameTime);
        }

        public override List<GenPass> Tasks => new List<GenPass>()
		{
			new SubworldGenPass(delegate
			{
				
				Main.worldSurface = 600.0;
				Main.rockLayer = Main.maxTilesY;
				SubworldSystem.hideUnderworld = true;
				//Main.cloudAlpha = 0f;
				//Main.resetClouds = true;

				StructureHelper.Generator.GenerateStructure("Structures/Observatory", new Terraria.DataStructures.Point16((Main.maxTilesX/2) - 24, (Main.maxTilesY/2) - 68), StarsAbove.Instance);

			})
			

		};
		public override void Load()
		{
		
			
			
		}
		public override void OnEnter()
        {
			Main.dayTime = true;
			Main.time = 18000;
			//DownedBossSystem.downedWarrior = SubworldSystem.ReadCopiedWorldData<bool>("downedWarrior");

			Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().locationName = "Observatory";
			Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().loadingScreenOpacity = 1f;


		}

        public override void OnLoad()
		{

			


			//Main.cloudAlpha = 0f;
			//Main.resetClouds = true;

			//SubworldSystem.noReturn = false;
			

		}
	}
}
