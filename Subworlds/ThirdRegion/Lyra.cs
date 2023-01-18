using Microsoft.Xna.Framework;
using Terraria;
using SubworldLibrary;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using Terraria.ID;

using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Generation;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace StarsAbove
{
	public class Lyra : Subworld
	{
		public override int Width => 3600;
		public override int Height => 1800;

		//public override ModWorld modWorld => ModContent.GetInstance < your modworld here>();

		public override bool ShouldSave => false;//Anomalies don't save.
		public override bool NoPlayerSaving => false;
		public override bool NormalUpdates => false;


		public override List<GenPass> Tasks => new List<GenPass>()
		{
			new PassLegacy("Lyra", (progress, _) =>
			{
					progress.Message = "Loading"; //Sets the text above the worldgen progress bar

				

					Main.worldSurface = Main.maxTilesY/2 + 420; 
					Main.rockLayer = Main.maxTilesY/2 + 600;

				    int tileAdjustment = 200;

					//variantWorld = Main.rand.Next(3);
					StructureHelper.Generator.GenerateStructure("Structures/Lyra/Lyra1", new Terraria.DataStructures.Point16(Main.maxTilesX/2 - 1200, Main.maxTilesY/2 - tileAdjustment), StarsAbove.Instance);
					StructureHelper.Generator.GenerateStructure("Structures/Lyra/Lyra2", new Terraria.DataStructures.Point16(Main.maxTilesX/2 - 1000, Main.maxTilesY/2 - tileAdjustment), StarsAbove.Instance);
					StructureHelper.Generator.GenerateStructure("Structures/Lyra/Lyra3", new Terraria.DataStructures.Point16(Main.maxTilesX/2 - 800, Main.maxTilesY/2 - tileAdjustment), StarsAbove.Instance);

					StructureHelper.Generator.GenerateStructure("Structures/Lyra/Lyra4", new Terraria.DataStructures.Point16(Main.maxTilesX/2 - 600, Main.maxTilesY/2 - tileAdjustment), StarsAbove.Instance);

					StructureHelper.Generator.GenerateStructure("Structures/Lyra/Lyra5", new Terraria.DataStructures.Point16(Main.maxTilesX/2 - 400, Main.maxTilesY/2 - tileAdjustment), StarsAbove.Instance);
					StructureHelper.Generator.GenerateStructure("Structures/Lyra/Lyra5Extra", new Terraria.DataStructures.Point16(Main.maxTilesX/2 - 200, Main.maxTilesY/2 - tileAdjustment), StarsAbove.Instance);

					StructureHelper.Generator.GenerateStructure("Structures/Lyra/Lyra6a", new Terraria.DataStructures.Point16(Main.maxTilesX/2 - 200, Main.maxTilesY/2 - tileAdjustment), StarsAbove.Instance);
								//The player will spawn on the leftmost side of this structure below

					StructureHelper.Generator.GenerateStructure("Structures/Lyra/Lyra7", new Terraria.DataStructures.Point16(Main.maxTilesX/2, Main.maxTilesY/2 - tileAdjustment), StarsAbove.Instance);  
					StructureHelper.Generator.GenerateStructure("Structures/Lyra/Lyra8", new Terraria.DataStructures.Point16(Main.maxTilesX/2 + 200, Main.maxTilesY/2 - tileAdjustment), StarsAbove.Instance);
					StructureHelper.Generator.GenerateStructure("Structures/Lyra/Lyra9", new Terraria.DataStructures.Point16(Main.maxTilesX/2 + 400, Main.maxTilesY/2 - tileAdjustment), StarsAbove.Instance);
					StructureHelper.Generator.GenerateStructure("Structures/Lyra/Lyra10", new Terraria.DataStructures.Point16(Main.maxTilesX/2 + 600, Main.maxTilesY/2 - tileAdjustment), StarsAbove.Instance);
					StructureHelper.Generator.GenerateStructure("Structures/Lyra/Lyra11", new Terraria.DataStructures.Point16(Main.maxTilesX/2 + 800, Main.maxTilesY/2 - tileAdjustment), StarsAbove.Instance);

					for (int i = 0; i < Main.maxTilesX; i++)
					{
						for (int j = 0; j < Main.maxTilesY; j++)
						{


							progress.Set((j + i * Main.maxTilesY) / (float)(Main.maxTilesX * Main.maxTilesY)); //Controls the progress bar, should only be set between 0f and 1f
							//Main.tile[i, j].active(true);
							//Main.tile[i, j].type = TileID.Air;
						}
						if(i == Main.maxTilesX/2)
						{

						}
					}
			})

		};
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
				new Vector2(Main.screenWidth / 2 + 40, Main.screenHeight / 2 - 70), //The position of the texture.
				new Rectangle(0, 0, AsphoRunAnimation.Width, AsphoRunAnimation.Height),
				Color.White, //The color of the texture.
				0, // The rotation of the texture.
				AsphoRunAnimation.Size() * 0.5f, //The centerpoint of the texture.
				1f, //The scale of the texture.
				SpriteEffects.None,
				0f);
			Main.spriteBatch.Draw(
				EriRunAnimation, //The texture being drawn.
				new Vector2(Main.screenWidth / 2 - 40, Main.screenHeight / 2 - 70), //The position of the texture.
				new Rectangle(0, 0, EriRunAnimation.Width, EriRunAnimation.Height),
				Color.White, //The color of the texture.
				0, // The rotation of the texture.
				EriRunAnimation.Size() * 0.5f, //The centerpoint of the texture.
				1f, //The scale of the texture.
				SpriteEffects.None,
				0f);


		}

		public override bool GetLight(Tile tile, int x, int y, ref FastRandom rand, ref Vector3 color)
		{
			color = new Vector3(0.01f, 0.01f, 0.01f);
			return base.GetLight(tile, x, y, ref rand, ref color);
		}
        public override void OnLoad()
        {
			//Clear the background.
			Main.numClouds = 0;
			Main.numCloudsTemp = 0;
			Main.cloudBGAlpha = 0f;

			Main.cloudAlpha = 0f;
			Main.resetClouds = true;
			Main.moonPhase = 4;
			
        }
        public override void OnEnter()
		{

			Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().locationName = "Lyra";
			Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().loadingScreenOpacity = 1f;

		}
		public override void Load()
		{
			SubworldSystem.noReturn = true;



		}
	}
}
