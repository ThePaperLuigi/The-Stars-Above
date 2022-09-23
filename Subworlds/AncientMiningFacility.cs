using Terraria;
using SubworldLibrary;
using Terraria.WorldBuilding;
using System.Collections.Generic;

using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Generation;
namespace StarsAbove
{
    public class AncientMiningFacility : Subworld
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
			new PassLegacy("Ancient Mining Facility", (progress, _) =>
			{
				progress.Message = "Loading"; //Sets the text above the worldgen progress bar
				Main.worldSurface = Main.maxTilesY + 250; //Hides the underground layer just out of bounds
				Main.rockLayer = Main.maxTilesY; //Hides the cavern layer way out of bounds

				//variantWorld = Main.rand.Next(3);

				StructureHelper.Generator.GenerateStructure("Structures/AncientMiningFacility", new Terraria.DataStructures.Point16((Main.maxTilesX/2) - 40, (Main.maxTilesY/2) - 86), StarsAbove.Instance);

				


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
			for (int i = 0; i < Main.maxPlayers; i++)
			{
				Player player = Main.player[i];
				if (player.active)
				{
					player.AddBuff(BuffType<Buffs.Wormhole>(), 20);  //Make sure to replace "buffType" and "timeInFrames" with actual values


				}


			}
			Main.dayTime = false;
			Main.time = 18000;
			Main.cloudAlpha = 0f;
			Main.resetClouds = true;
			Main.moonPhase = 4;
			SubworldSystem.noReturn = true;


		}
	}
}
