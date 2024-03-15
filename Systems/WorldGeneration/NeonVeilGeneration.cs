
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using ReLogic.Content;
using StarsAbove.Biomes;
using StarsAbove.Items.Materials;
using StarsAbove.Items.Weapons.Celestial;
using StarsAbove.Items.Weapons.Summon;
using SubworldLibrary;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
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
            int Index = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            if (Index != -1)
            {
                //Commented out
                //tasks.Insert(Index + 1, new NeonVeilSurfacePass("Neon Veil (Surface)", 100f));
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
            //
            
            var offsetDims = Point16.Zero;
            int neonVeilSize = 0;
            int offsetX = 0;
            //Create a list of all the structures that can be added. This doesn't include the entrance and exits. This list does not change at any time.
            List<string> neonVeilStructures = new List<string>() {
                "NeonVeilBuilding1",
                "NeonVeilBuilding2",
                "NeonVeilDecor1",
            };
            //When a structure is generated, it'll be removed from this list (neonVeilBlueprint). If the list is empty and more things need to be generated, the list will be refilled from "UsedStructures"
            List<string> neonVeilBlueprint = new List<string>();

            //TODO: Some buildings shouldn't repeat (I.E. Lightshow Tower or Garridine's outpost)
            if (Main.maxTilesX >= 8400)
            {
                //Large (or bigger) world generation
                neonVeilSize = 20;
            }
            else if (Main.maxTilesX >= 6400)
            {
                //Medium world generation
                neonVeilSize = 12;
            }
            else
            {
                //Small world generation
                neonVeilSize = 6;
            }
            for (int i = 0; i <= neonVeilSize; i++)
            {
                progress.Set(i / neonVeilSize);

                if(i == 0)
                {
                    CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "NeonVeilEntranceLeft");
                }
                else if (i == neonVeilSize)//Replace with EntranceRight
                {
                    CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "NeonVeilEntranceLeft");
                }
                else
                {   
                    if(neonVeilBlueprint.Count > 0)
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, neonVeilBlueprint[Main.rand.Next(neonVeilBlueprint.Count)]);

                    }
                    else
                    {
                        //Refresh the blueprint with already used structures
                        neonVeilBlueprint = new List<string>(neonVeilStructures);
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, neonVeilBlueprint[Main.rand.Next(neonVeilBlueprint.Count)]);

                    }
                }
            }

        }

        private static void CreateNeonVeilStructure(ref Point16 offsetDims, ref int offsetX, List<string> neonVeilBlueprint, string currentStructure)
        {
            StructureHelper.Generator.GenerateStructure("Structures/NeonVeil/" + currentStructure, new Terraria.DataStructures.Point16((Main.maxTilesX / 2) + offsetX, (Main.maxTilesY) - 200), StarsAbove.Instance);
            StructureHelper.Generator.GetDimensions("Structures/NeonVeil/" + currentStructure, StarsAbove.Instance, ref offsetDims);
            offsetX += offsetDims.X;
            if(neonVeilBlueprint.Contains(currentStructure))
            {
                neonVeilBlueprint.Remove(currentStructure);
            }
        }
    }
}
