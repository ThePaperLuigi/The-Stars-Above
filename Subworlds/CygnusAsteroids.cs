using Microsoft.Xna.Framework;
using Terraria;
using SubworldLibrary;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using Terraria.ID;

using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Generation;

namespace StarsAbove
{
    public class CygnusAsteroids : Subworld
	{
		public override int Width => 1750;
		public override int Height => 750;

		//public override ModWorld modWorld => ModContent.GetInstance < your modworld here>();

		public override bool ShouldSave => false;
		public override bool NoPlayerSaving => false;
		public override bool NormalUpdates => false;

		public int variantWorld;

		public override List<GenPass> Tasks => new List<GenPass>()
		{
			new PassLegacy("Cygnus Asteroid Field", (progress, _) =>
			{
					progress.Message = "Loading"; //Sets the text above the worldgen progress bar
					Main.worldSurface = Main.maxTilesY + 250; //Hides the underground layer just out of bounds
					Main.rockLayer = Main.maxTilesY; //Hides the cavern layer way out of bounds

					//variantWorld = Main.rand.Next(3);

					StructureHelper.Generator.GenerateStructure("Structures/SpacePlatform", new Terraria.DataStructures.Point16(Main.maxTilesX/2 - 14, Main.maxTilesY/2), StarsAbove.Instance);

				StructureHelper.Generator.GenerateStructure("Structures/MarbleDebris1", new Terraria.DataStructures.Point16(Main.maxTilesX/2 + 40, Main.maxTilesY/2 - 65), StarsAbove.Instance);

				StructureHelper.Generator.GenerateStructure("Structures/MarbleDebris2", new Terraria.DataStructures.Point16(Main.maxTilesX/2 - 80, Main.maxTilesY/2 - 60), StarsAbove.Instance);

				StructureHelper.Generator.GenerateStructure("Structures/MarbleDebris5", new Terraria.DataStructures.Point16(Main.maxTilesX/2 + 140, Main.maxTilesY/2 - 65), StarsAbove.Instance);

				StructureHelper.Generator.GenerateStructure("Structures/MarbleDebris3", new Terraria.DataStructures.Point16(Main.maxTilesX/2 + 260, Main.maxTilesY/2 - 110), StarsAbove.Instance);

				StructureHelper.Generator.GenerateStructure("Structures/MarbleDebris4", new Terraria.DataStructures.Point16(Main.maxTilesX/2 - 220, Main.maxTilesY/2 - 65), StarsAbove.Instance);




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

		public override void Load()
		{
			
			Main.dayTime = false;
			Main.time = 18000;
			Main.cloudAlpha = 0f;
			Main.resetClouds = true;
			Main.moonPhase = 4;
			SubworldSystem.noReturn = true;


		}
	}
}
