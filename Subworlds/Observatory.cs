using Terraria;
using SubworldLibrary;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using Terraria.ModLoader;

using static Terraria.ModLoader.ModContent;

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

        public int variantWorld;

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
			
			for (int i = 0; i < Main.maxPlayers; i++)
			{
				Player player = Main.player[i];
				if (player.active)
				{
					player.AddBuff(BuffType<Buffs.Wormhole>(), 20);  //Make sure to replace "buffType" and "timeInFrames" with actual values
					


					
					if (player.whoAmI == Main.myPlayer)
					{




					}
				}


			}
			
        }

        public override void OnLoad()
		{


			

			//Main.cloudAlpha = 0f;
			//Main.resetClouds = true;
			
			SubworldSystem.noReturn = false;
			

		}
	}
}
