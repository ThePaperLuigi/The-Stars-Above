
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using ReLogic.Content;
using StarsAbove.Biomes;
using SubworldLibrary;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.WorldBuilding;

namespace StarsAbove.Systems.WorldGeneration
{

    public class NeonVeilGeneration : ModSystem
    {
        public static LocalizedText NeonVeilGenerationMessage { get; private set; }

        public override void SetStaticDefaults()
        {
            NeonVeilGenerationMessage = Language.GetOrRegister(Mod.GetLocalizationKey($"WorldGen.{nameof(NeonVeilGenerationMessage)}"));
        }

        // 4. We use the ModifyWorldGenTasks method to tell the game the order that our world generation code should run
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            // 5. We use FindIndex to locate the index of the vanilla world generation task called "Shinies". This ensures our code runs at the correct step.

            //Change this index to much later!
            int HellforgeIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Hellforge"));
            if (HellforgeIndex != -1)
            {
                // 6. We register our world generation pass by passing in an instance of our custom GenPass class below. The GenPass class will execute our world generation code.
                tasks.Insert(HellforgeIndex + 1, new NeonVeilSurfacePass("Neon Veil (Surface)", 100f));
            }
        }

    }

    public class NeonVeilSurfacePass : GenPass
    {
        public NeonVeilSurfacePass(string name, float loadWeight) : base(name, loadWeight)
        {
        }

        // 8. The ApplyPass method is where the actual world generation code is placed.
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = NeonVeilGeneration.NeonVeilGenerationMessage.Value;

            //The Underworld is the lower 200 blocks of the world.

            //The underworld bg is about 159 blocks from the top.

            //The plan is: Generate the aboveground Neon Veil- generates building -> empty space -> building -> empty space. Depending on the random building selected.
            //The upper 90 blocks is the surface, the lower 70 blocks is the depths. 20 blocks under the surface is Deep Asphalt

            //The surface and the depths are generated seperately, so it makes for more interesting gameplay. However...
            //Each depth structure has to be the same width as the surface. The depths need to all connect to one another
            
            //Additionally, there should always be a main town hall structure with a route to the depths, and under that is the Resonant Figment. of course the door to that is locked until later

            //Make sure the upper layers are null tiles so they can be replaced by stone if necessary

            //Test spawn
            //StructureHelper.Generator.GenerateStructure("Structures/NeonVeil/TestTower", new Terraria.DataStructures.Point16((Main.maxTilesX / 2), (Main.maxTilesY) - 200), StarsAbove.Instance);







            /*
            // 10. Here we use a for loop to run the code inside the loop many times. This for loop scales to the product of Main.maxTilesX, Main.maxTilesY, and 2E-05. 2E-05 is scientific notation and equal to 0.00002. Sometimes scientific notation is easier to read when dealing with a lot of zeros.
            // 11. In a small world, this math results in 4200 * 1200 * 0.00002, which is about 100. This means that we'll run the code inside the for loop 100 times. This is the amount Crimtane or Demonite will spawn. Since we are scaling by both dimensions of the world size, the amount spawned will adjust automatically to different world sizes for a consistent distribution of ores.
            for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
            {
                // 12. We randomly choose an x and y coordinate. The x coordinate is chosen from the far left to the far right coordinates. The y coordinate, however, is choosen from between GenVars.worldSurfaceLow and the bottom of the map. We can use this technique to determine the depth that our ore should spawn at.
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)GenVars.worldSurfaceLow, Main.maxTilesY);

                // 13. Finally, we do the actual world generation code. In this example, we use the WorldGen.TileRunner method. This method spawns splotches of the Tile type we provide to the method. The behavior of TileRunner is detailed in the Useful Methods section below.
                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), TileID.CobaltBrick);
            }*/
        }
    }
}
