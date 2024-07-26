
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
            int Index = tasks.FindIndex(genpass => genpass.Name.Equals("Larva"));
            if (Index != -1)
            {
                //Commented out
                tasks.Insert(Index + 1, new NeonVeilSurfacePass("Neon Veil", 10f));
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

            
            
            var offsetDims = Point16.Zero;
            int neonVeilSize = 0;
            int offsetX = 0;

            if (Main.specialSeedWorld)
            {
                offsetX = (Main.maxTilesX / 10);
            }
            //Create a list of all the structures that can be added. This doesn't include the entrance and exits. This list does not change at any time.
            List<string> neonVeilStructures = new List<string>() {
                "NeonVeilBuilding1",
                "NeonVeilBuilding2",
                "NeonVeilBuilding3",
                "NeonVeilBuilding4",
                "NeonVeilBuilding5",
                "NeonVeilBuilding6",
                "NeonVeilBuilding7",
                "NeonVeilSub1",
                "NeonVeilSub2",
                "NeonVeilSub3",
            };
            //When a structure is generated, it'll be removed from this list (neonVeilBlueprint). If the list is empty and more things need to be generated, the list will be refilled from "UsedStructures"
            List<string> neonVeilBlueprint = new List<string>();

            //TODO: Some buildings shouldn't repeat (I.E. Lightshow Tower or Garridine's outpost)
            if (Main.maxTilesX >= 8400)
            {
                //Large (or bigger) world generation
                neonVeilSize = 45;
            }
            else if (Main.maxTilesX >= 6400)
            {
                //Medium world generation
                neonVeilSize = 28;
            }
            else
            {
                //Small world generation (same as medium)
                neonVeilSize = 20;
            }
            for (int i = 0; i <= neonVeilSize; i++)
            {
                progress.Set(i / neonVeilSize);

                if(i == 0)
                {
                    CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "NeonVeilEntranceL");
                }
                else if (i == neonVeilSize)//Replace with EntranceRight
                {
                    CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "NeonVeilEntranceR");
                    return;
                }

                if(neonVeilSize >= 45)//Large world
                {
                    //Important Structures
                    if (i == 23)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "NeonVeilLighthouse");
                    }
                    else if (i == 12)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "NeonVeilGarden");
                    }
                    else if (i == 37)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "NeonVeilBar");
                    }

                    //Confluxes
                    else if (i == 5)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxAuricExalt");
                    }
                    else if (i == 10)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxBloodyBanquet");
                    }
                    else if (i == 15)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxCrescentMeteor");
                    }
                    else if (i == 20)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxDeadbloom");
                    }
                    else if (i == 25)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxDreadMechanical");
                    }
                    else if (i == 30)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxLucidDreamer");
                    }
                    else if (i == 35)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxLuminousHallow");
                    }
                    else if (i == 40)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxRoyalSunrise");
                    }
                    else
                    {
                        if (neonVeilBlueprint.Count > 0)
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
                else if(neonVeilSize >= 28)//Medium world
                {
                    //Important Structures
                    if (i == 5)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "NeonVeilLighthouse");
                    }
                    else if (i == 14)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "NeonVeilGarden");
                    }
                    else if (i == 22)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "NeonVeilBar");
                    }

                    //Confluxes
                    else if (i == 3)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxAuricExalt");
                    }
                    else if (i == 6)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxBloodyBanquet");
                    }
                    else if (i == 9)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxCrescentMeteor");
                    }
                    else if (i == 12)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxDeadbloom");
                    }
                    else if (i == 15)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxDreadMechanical");
                    }
                    else if (i == 18)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxLucidDreamer");
                    }
                    else if (i == 21)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxLuminousHallow");
                    }
                    else if (i == 24)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxRoyalSunrise");
                    }
                    else
                    {
                        if (neonVeilBlueprint.Count > 0)
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
                else//Small world
                {
                    //Important Structures
                    if (i == 7)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "NeonVeilLighthouse");
                    }
                    else if (i == 10)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "NeonVeilGarden");
                    }
                    else if (i == 17)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "NeonVeilBar");
                    }

                    //Confluxes
                    else if (i == 2)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxAuricExalt");
                    }
                    else if (i == 4)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxBloodyBanquet");
                    }
                    else if (i == 6)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxCrescentMeteor");
                    }
                    else if (i == 8)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxDeadbloom");
                    }
                    else if (i == 12)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxDreadMechanical");
                    }
                    else if (i == 14)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxLucidDreamer");
                    }
                    else if (i == 16)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxLuminousHallow");
                    }
                    else if (i == 18)//
                    {
                        CreateNeonVeilStructure(ref offsetDims, ref offsetX, neonVeilBlueprint, "ConfluxRoyalSunrise");
                    }
                    else
                    {
                        if (neonVeilBlueprint.Count > 0)
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


        }

        private static void CreateNeonVeilStructure(ref Point16 offsetDims, ref int offsetX, List<string> neonVeilBlueprint, string currentStructure)
        {
            
            StructureHelper.Generator.GenerateStructure("Structures/NeonVeil/" + currentStructure, new Terraria.DataStructures.Point16((Main.maxTilesX / 2) + offsetX, (Main.maxTilesY) - 190), StarsAbove.Instance);
            StructureHelper.Generator.GetDimensions("Structures/NeonVeil/" + currentStructure, StarsAbove.Instance, ref offsetDims);

            //For the width of the structure, clear lava above it
            for (int i = (Main.maxTilesX / 2) + offsetX; i < (Main.maxTilesX / 2) + offsetX + offsetDims.X; i++)
            {
                for (int j = (Main.maxTilesY) - 400; j < Main.maxTilesY; j++)
                {
                    Tile tile = Main.tile[i, j];
                    if (tile.LiquidType == LiquidID.Lava)
                        tile.LiquidAmount = 0;
                }
            }

            offsetX += offsetDims.X;
                       
            if (neonVeilBlueprint.Contains(currentStructure))
            {
                neonVeilBlueprint.Remove(currentStructure);
            }
        }
    }

}
