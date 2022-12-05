using Terraria;
using SubworldLibrary;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using Terraria.ModLoader;

using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

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

			
			base.DrawMenu(gameTime);
        }
        public override void DrawSetup(GameTime gameTime)
        {



            base.DrawSetup(gameTime);
        }

        public override List<GenPass> Tasks => new List<GenPass>()
		{
			new SubworldGenPass(delegate
			{
				Main.dayTime = true;
				Main.time = 18000;
				Main.worldSurface = 600.0;
				Main.rockLayer = Main.maxTilesY;
				SubworldSystem.hideUnderworld = true;
				//Main.cloudAlpha = 0f;
				//Main.resetClouds = true;

				StructureHelper.Generator.GenerateStructure("Structures/Observatory", new Terraria.DataStructures.Point16((Main.maxTilesX/2) - 24, (Main.maxTilesY/2) - 68), StarsAbove.Instance);

				//NPC.NewNPC(new EntitySource_WorldGen(), (200 + WorldGen.genRand.Next(2) * 200) * 16, 4800, NPCID.Bird);
			})
			/*new PassLegacy("The Observatory", (progress, _) =>
			{
					Main.dayTime = true;
					
					
					Main.moonPhase = 4;
					progress.Message = "Loading"; //Sets the text above the worldgen progress bar
					Main.worldSurface = Main.maxTilesY + 250; //Hides the underground layer just out of bounds
					Main.rockLayer = Main.maxTilesY; //Hides the cavern layer way out of bounds

					


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
			})*/



		};
		public override void Load()
		{
			ModTypeLookup<Subworld>.Register(this);
			
			
		}

		public override void OnEnter()
        {
			Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().locationName = "Observatory";
			Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().loadingScreenOpacity = 1f;


		}

        public override void OnLoad()
		{


			

			//Main.cloudAlpha = 0f;
			//Main.resetClouds = true;
			
			SubworldSystem.noReturn = false;
			

		}
	}
}
