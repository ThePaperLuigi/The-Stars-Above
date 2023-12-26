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
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;

namespace StarsAbove.Subworlds
{
    public class Katabasis : Subworld
	{
		public override string Name => "Katabasis";

		public override int Width => 1750;
		public override int Height => 750;

		//public override bool ShouldSave => false;
		//public override bool NoPlayerSaving => false;
		//public override bool NormalUpdates => false;

		//public override bool noWorldUpdate => true;
		private const string assetPath = "StarsAbove/Subworlds/LoadingScreens";

		public override void DrawMenu(GameTime gameTime)
        {
			
        }
		
        public override void DrawSetup(GameTime gameTime)
        {



            base.DrawSetup(gameTime);
        }
        public override List<GenPass> Tasks => new() { new PassLegacy("Subworld", SubworldGeneration) };
        private void SubworldGeneration(GenerationProgress progress, GameConfiguration configuration)
        {
            Main.spawnTileX = 929;
            Main.spawnTileY = 333;

            Main.worldSurface = 600.0;
            Main.rockLayer = Main.maxTilesY;
            SubworldSystem.hideUnderworld = true;

            StructureHelper.Generator.GenerateStructure("Structures/Katabasis", new Terraria.DataStructures.Point16((Main.maxTilesX / 2) - 24, (Main.maxTilesY / 2) - 68), StarsAbove.Instance);

        }
       
		public override void Load()
		{
		
			
			
		}
		public override void OnEnter()
        {
            //Clear the background.
            Main.numClouds = 0;
            Main.numCloudsTemp = 0;
            Main.cloudBGAlpha = 0f;

            Main.cloudAlpha = 0f;
            Main.resetClouds = true;
            Main.moonPhase = 4;
            //DownedBossSystem.downedWarrior = SubworldSystem.ReadCopiedWorldData<bool>("downedWarrior");

            Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().locationName = "Katabasis";
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
