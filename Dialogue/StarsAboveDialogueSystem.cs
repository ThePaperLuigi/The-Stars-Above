using StarsAbove.Items.Consumables;
using StarsAbove.Systems;
using StarsAbove.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;

namespace StarsAbove.Dialogue
{
    public class StarsAboveDialogueSystem : ModSystem
    {
        //Old dialogue system.
        public static void SetupDialogueSystem(bool writeToArchive, int chosenStarfarer, ref int chosenDialogue, ref bool dialoguePrep, ref int dialogueLeft, ref int expression, ref string dialogue, ref bool dialogueFinished, Player Player, Mod Mod)
        {
            string baseKey = "Mods.StarsAbove.Dialogue.";
            string starfarerName = "Asphodene";
            if (chosenStarfarer == 1)
            {
                starfarerName = "Asphodene";
            }
            else if (chosenStarfarer == 2)
            {
                starfarerName = "Eridani";
            }


            
            if (chosenDialogue == 2) // Contingency text
            {
                if (Main.hardMode)
                {
                    string category = "IdleDialogueHardmode";
                    string key = category + "." + starfarerName + ".";
                    WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                }
                else
                {
                    string category = "IdleDialogue";
                    string key = category + "." + starfarerName + ".";
                    WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                }
            }//Fallback idle dialogue (timer)

             //Pre Hardmode Idle Dialogue
             //Finished rework.
            #region idleDialogue
            
            if (chosenDialogue == 3) // Passive Dialogue 1
            {
                if (dialoguePrep)
                {
                     
                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "NormalIdleDialogue1" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//1
            if (chosenDialogue == 4) // Passive Dialogue 2
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "NormalIdleDialogue2" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//2
            if (chosenDialogue == 5) // Passive Dialogue 2
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "NormalIdleDialogue3" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//3
            if (chosenDialogue == 6) // Passive Dialogue 3
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "NormalIdleDialogue4" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//4
            if (chosenDialogue == 7) // Passive Dialogue 4
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "NormalIdleDialogue5" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//5
            if (chosenDialogue == 8) // Passive Dialogue
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "NormalIdleDialogue6" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//6
            if (chosenDialogue == 9) // Passive Dialogue
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "NormalIdleDialogue7" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//7
            if (chosenDialogue == 10) // Passive Dialogue
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "NormalIdleDialogue8" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//8
            if (chosenDialogue == 11) // Passive Dialogue
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "NormalIdleDialogue9" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//9
             //Post Hardmode Dialogue
            if (chosenDialogue == 12) // Passive Dialogue
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue1" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//1
            if (chosenDialogue == 13) // Passive Dialogue
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue2" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//2
            if (chosenDialogue == 14) // Passive Dialogue
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue3" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//3
            if (chosenDialogue == 15) // Passive Dialogue
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue4" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//4
            if (chosenDialogue == 16) // Passive Dialogue
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue5" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//5
            if (chosenDialogue == 17) // Passive Dialogue
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue6" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//6
            if (chosenDialogue == 18) // Passive Dialogue
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue7" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//7
            if (chosenDialogue == 19) // Passive Dialogue
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue8" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//8
            if (chosenDialogue == 20) // Passive Dialogue
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue9" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//9
            if (chosenDialogue == 21) // A world shrouded in Light
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "LightIdleDialogue" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//A world shrouded in Light
            

            if (chosenDialogue == 400) // Passive Dialogue
            {
                if (dialoguePrep)
                {
                     


                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue10" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }
            if (chosenDialogue == 401) // Passive Dialogue
            {
                if (dialoguePrep)
                {
                     


                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue11" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }
            if (chosenDialogue == 402) // Passive Dialogue
            {
                if (dialoguePrep)
                {
                     


                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue12" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }
            if (chosenDialogue == 403) // Passive Dialogue
            {
                if (dialoguePrep)
                {
                     


                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue13" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            if (chosenDialogue == 404) // Passive Dialogue
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue14" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            #endregion
            #region bossDialogue
            //Boss dialogue
            if (chosenDialogue == 51) // Boss dialogue - Slime King
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "BossDialogue";
                string key = category + "." + "KingSlime" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                
            }//Slime King 1
            if (chosenDialogue == 52) // Boss dialogue - Eye of Cthulu
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "BossDialogue";
                string key = category + "." + "CthulhuEye" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Eye of Cthulhu 2
            if (chosenDialogue == 53) // Boss dialogue - Eater of Worlds or Brain of Cthulhu
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "BossDialogue";
                string key = category + "." + "CorruptionBoss" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Brain of Cthulhu / Eater of Worlds 3
            if (chosenDialogue == 54) // Boss dialogue - Queen Bee
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "BossDialogue";
                string key = category + "." + "QueenBee" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Queen Bee 4 
            if (chosenDialogue == 55) // Boss dialogue - Skeletron
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "BossDialogue";
                string key = category + "." + "Skeletron" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Skeletron 5 
            if (chosenDialogue == 56) // Boss dialogue - Wall of Flesh
            {
                if (dialoguePrep)
                {
                     


                }
                string category = "BossDialogue";
                string key = category + "." + "WallOfFlesh" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Wall of Flesh 6
            if (chosenDialogue == 58) // Boss dialogue - The Twins
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "BossDialogue";
                string key = category + "." + "Twins" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//The Twins 7
            if (chosenDialogue == 57) // Boss dialogue - The Destroyer
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "BossDialogue";
                string key = category + "." + "Destroyer" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Destroyer 8
            if (chosenDialogue == 59) // Boss dialogue - Skeletron Prime
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "BossDialogue";
                string key = category + "." + "SkeletronPrime" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Skeletron Prime 9
            if (chosenDialogue == 60) // Boss dialogue - All Mechanical Bosses Defeated
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "BossDialogue";
                string key = category + "." + "AllMechs" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//All Mechanical Bosses defeated 10
            if (chosenDialogue == 61) // Boss dialogue - Plantera
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "BossDialogue";
                string key = category + "." + "Plantera" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Plantera 11
            if (chosenDialogue == 62) // Boss dialogue - Golem
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "BossDialogue";
                string key = category + "." + "Golem" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Golem 12
            if (chosenDialogue == 63) // Boss dialogue - Duke Fishron
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "BossDialogue";
                string key = category + "." + "DukeFishron" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Duke Fishron 13
            if (chosenDialogue == 64) // Boss dialogue - Lunatic Cultist
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "BossDialogue";
                string key = category + "." + "Cultist" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Lunatic Cultist 14
            if (chosenDialogue == 65) // Boss dialogue - Moon Lord
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "BossDialogue";
                string key = category + "." + "MoonLord" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Moon Lord 15 
            if (chosenDialogue == 66) // Boss dialogue - Warrior of Light
            {
                if (dialoguePrep == true)
                {
                     

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("MnemonicTrace").Type);
                }
                string category = "BossDialogue";
                string key = category + "." + "WarriorOfLight" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Warrior of Light 16
            if (chosenDialogue == 67) // Boss dialogue - All Vanilla Bosses Defeated
            {
                if (dialoguePrep == true)
                {
                     

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheCosmos").Type);
                }
                string category = "BossDialogue";
                string key = category + "." + "AllVanillaBosses" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//All Vanilla Bosses 17
            if (chosenDialogue == 68) // Boss dialogue - Everything Defeated in Expert Mode
            {
                if (dialoguePrep == true)
                {
                     

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheBeginningAndEnd").Type);
                }
                string category = "BossDialogue";
                string key = category + "." + "VanillaAndWarrior" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Everything Vanilla + WoL 18
            if (chosenDialogue == 69) // Boss dialogue - Dioskouroi
            {
                if (dialoguePrep == true)
                {
                     

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("MnemonicTrace4").Type);
                }
                string category = "BossDialogue";
                string key = category + "." + "Dioskouroi" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Dioskouroi
            if (chosenDialogue == 70) // Boss dialogue - Nalhaun
            {
                if (dialoguePrep == true)
                {
                     

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("MnemonicTrace2").Type);
                }
                string category = "BossDialogue";
                string key = category + "." + "Nalhaun" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Nalhaun 20
            if (chosenDialogue == 71) // Boss dialogue - Penthesilea
            {
                if (dialoguePrep == true)
                {
                     

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("MnemonicTrace3").Type);
                }
                string category = "BossDialogue";
                string key = category + "." + "Penth" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                
            }//Penthesilea 21
            if (chosenDialogue == 72) // Boss dialogue - Arbitration
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "BossDialogue";
                string key = category + "." + "Arbitration" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Arbitration 22
            if (chosenDialogue == 73) // Boss dialogue - Tsukiyomi 23
            {
                if (dialoguePrep == true)
                {
                     

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("SpatialMemoriam").Type);
                }
                string category = "BossDialogue";
                string key = category + "." + "Tsukiyomi" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Tsukiyomi 23
            if (chosenDialogue == 74) // Boss dialogue - Queen Slime
            {
                if (dialoguePrep)
                {
                     


                }
                string category = "BossDialogue";
                string key = category + "." + "QueenSlime" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Queen Slime 24
            if (chosenDialogue == 75) // Boss dialogue - Empress of Light
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "BossDialogue";
                string key = category + "." + "EmpressOfLight" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Empress of Light 25
            if (chosenDialogue == 76) // Boss dialogue - Deerclops
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "BossDialogue";
                string key = category + "." + "Deerclops" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Deerclops 26
            if (chosenDialogue == 77) // Vagrant
            {
                if (dialoguePrep == true)
                {
                     

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("ShatteredDisk").Type);
                }

                string category = "BossItemDialogue";
                string key = category + "." + "Vagrant" + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Vagrant
            //Thespian
            if (chosenDialogue == 78)
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "BossDialogue";
                string key = category + "." + "Thespian" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }


            //Calamity boss dialogue: (Reminder: Calamity boss dialogue starts past 200) If Calamity is not detected, stop at 23. Otherwise...

            if (chosenDialogue == 201) // Boss dialogue - Desert Scourge
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "CalamityBossDialogue";
                string key = category + "." + "DesertScourge" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Desert Scourge 24
            if (chosenDialogue == 202) // Boss dialogue - Crabulon
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "CalamityBossDialogue";
                string key = category + "." + "Crabulon" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Crabulon 25 
            if (chosenDialogue == 203) // Boss dialogue - Hive Mind
            {
                if (dialoguePrep)
                {
                     


                }
                string category = "CalamityBossDialogue";
                string key = category + "." + "HiveMind" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Hive Mind 26
            if (chosenDialogue == 204) // Boss dialogue - Perforators
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "CalamityBossDialogue";
                string key = category + "." + "Perforators" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Perforators 27
            if (chosenDialogue == 205) // Boss dialogue - The Slime God
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "CalamityBossDialogue";
                string key = category + "." + "SlimeGod" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//The Slime God 28
             //Calamity boss dialogue (Hardmode)
            if (chosenDialogue == 206) // Boss dialogue - Cryogen
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "CalamityBossDialogue";
                string key = category + "." + "Cryogen" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Cryogen 29
            if (chosenDialogue == 207) // Boss dialogue - Aquatic Scourge
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "CalamityBossDialogue";
                string key = category + "." + "AquaticScourge" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Aquatic Scourge 30
            if (chosenDialogue == 208) // Boss dialogue - Brimstone Elemental
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "CalamityBossDialogue";
                string key = category + "." + "BrimstoneElemental" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Brimstone Elemental 31
            if (chosenDialogue == 209) // Boss dialogue - Calamitas
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "CalamityBossDialogue";
                string key = category + "." + "Calamitas" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Calamitas 32
            if (chosenDialogue == 210) // Boss dialogue - Leviathan
            {
                if (dialoguePrep)
                {
                     


                }
                string category = "CalamityBossDialogue";
                string key = category + "." + "Leviathan" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Leviathan 33
            if (chosenDialogue == 211) // Boss dialogue - Astrum Aureus
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "CalamityBossDialogue";
                string key = category + "." + "AstrumAureus" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Astrum Aureus 34
            if (chosenDialogue == 212) // Boss dialogue - Plaguebringer Goliath
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "CalamityBossDialogue";
                string key = category + "." + "PlaguebringerGoliath" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Plaguebringer Goliath 35
            if (chosenDialogue == 213) // Boss dialogue - Ravager
            {
                if (dialoguePrep)
                {
                     

                }
                string category = "CalamityBossDialogue";
                string key = category + "." + "Ravager" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Ravager 36
            if (chosenDialogue == 214) // Boss dialogue - Astrum Deus
            {
                if(dialoguePrep)
                {
                     

                }
                string category = "CalamityBossDialogue";
                string key = category + "." + "AstrumDeus" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Astrum Deus 37
            #endregion
            #region weaponDialogue

            //Weapon conversations...........................................................................................................
            if (chosenDialogue == 101) // Death in Four Acts // Der Freischutz
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheFreeshooter").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheSharpshooter").Type);

                    }
                }
                string category = "WeaponDialogue";
                string key = category + "." + "Skeleton" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                
            }//Death In Four Acts / Der Freischutz 1
            if (chosenDialogue == 102) // Persephone / Kazimierz Seraphim
            {
                if(dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheHallownest").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfThePegasus").Type);

                    }
                }
                
                string category = "WeaponDialogue";
                string key = category + "." + "Underworld" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Persephone / Kazimierz Seraphim 2
            if (chosenDialogue == 103) // Skofnung / Inugami Ripsaw
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfBitterfrost").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfFingers").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "QueenBee" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);



            }//Skofnung / Inugami Ripsaw 3
            if (chosenDialogue == 104) // Aegis Driver / Rad Gun
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheAegis").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfStyle").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "KingSlime" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Aegis Driver / Rad Gun 4 
            if (chosenDialogue == 105) // Karlan Truesilver / Every Moment Matters
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSilverAsh").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheVoid").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "WallOfFlesh" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Karlan Truesilver / Every Moment Matters 5
            if (chosenDialogue == 106) // Veneration of Butterflies / Memento Muse
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfButterflies").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfDeathsApprentice").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "MechBoss" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Veneration of Butterflies / Memento Muse 6
            if (chosenDialogue == 107) // Ride The Bull / Drachenlance
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheBull").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheDragonslayer").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "AllMechBoss" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Ride the Bull / Drachenlance 7
            if (chosenDialogue == 108) // Crimson Outbreak / Voice Of The Fallen
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheSwarm").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheFallen").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "Plantera" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Crimson Outbreak / Voice Of The Fallen 8
            if (chosenDialogue == 109) // Plenilune Gaze / Tartaglia
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheMoonlitAdepti").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheHarbinger").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "Golem" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Plenilune Gaze / Tartaglia 9
            if (chosenDialogue == 110) // Rex Lapis / Yunlai Stiletto
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheUnyieldingEarth").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfDrivingThunder").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "LunaticCultist" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Rex Lapis / Yunlai Stiletto 10
            if (chosenDialogue == 111) // Naganadel / Suistrume
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfStarsong").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfLunarDominion").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "MoonLord" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Naganadel / Suistrume 11
            if (chosenDialogue == 112) // Light Unrelenting / Key of the King's Law
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheTreasury").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSurpassingLimits").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "WarriorOfLight" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);



            }//Light Unrelenting / Key of the King's Law 12
            if (chosenDialogue == 115) // Izanagi's Edge / Hawkmoon
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfIzanagi").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheHawkmoon").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "Vagrant" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Izanagi's Edge/ Hawkmoon 13
            if (chosenDialogue == 116) // Key of the Sinner / Crimson Sakura Alpha
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSin").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfAlpha").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "DukeFishron" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Key Of The Sinner / Crimson Sakura Alpha 14
            if (chosenDialogue == 117) // Phantom In The Mirror / Hollowheart Albion
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfThePhantom").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheHollowheart").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "Nalhaun" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Phantom In The Mirror / Hollowheart Albion 15
            if (chosenDialogue == 118) // Vision of Euthymia / Kroniic Principality
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfEuthymia").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTime").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "Penthesilea" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Vision of Euthymia / Kroniic Principality 16
            if (chosenDialogue == 119) // Liberation Blazing / Unforgotten
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfLiberation").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfAzakana").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "LiberationAzakana" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Liberation Blazing / Unforgotten 17
            if (chosenDialogue == 120) // Misery's Company
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfMisery").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Misery" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Misery's Company 18
            if (chosenDialogue == 121) //Hullwrought
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheGunlance").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Hullwrought" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Hullwrought 19 
            if (chosenDialogue == 122) //Shadowless Cerulean
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheChimera").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Hullwrought" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Shadowless Cerulean 20
            if (chosenDialogue == 123) //Apalistik
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheOcean").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Apalistik" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Apalistik 21
            if (chosenDialogue == 124) //Luminary Wand 22
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheObservatory").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Luminary" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Luminary Wand 22
            if (chosenDialogue == 125) //Xenoblade 23
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheBionis").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Xenoblade" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Xenoblade 23
            if (chosenDialogue == 126) // Stygian Nymph / Caesura of Despair
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfDuality").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfIRyS").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "DualityIrys" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Stygian Nymph / Caesura of Despair 24
            if (chosenDialogue == 127) //Claimh Solais
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfRadiance").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Claimh" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Claimh Solais 25
            if (chosenDialogue == 128) //Penthesilea's Muse 26
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfInk").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "PenthesileaMuse" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);



            }//Penthesilea's Muse 26
            if (chosenDialogue == 129) //Kifrosse 28
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfFoxfire").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Kifrosse" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Kifrosse
            if (chosenDialogue == 130) //Architect's Luminance 29
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfLuminance").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Architect" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Architect's Luminance
            if (chosenDialogue == 131) //Force Of Nature
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfBlasting").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "ForceOfNature" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

                

            }//Force-a-Nature
            if (chosenDialogue == 132) //Genocide
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfExplosions").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Genocide" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Genocide
            if (chosenDialogue == 133) //Takonomicon
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfOuterGods").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Takonomicon" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Takonomicon
            if (chosenDialogue == 134) //Twin Stars of Albiero
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTwinStars").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "TwinStarsAlbiero" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Twin Stars of Albiero
            if (chosenDialogue == 135) //Armaments of the Sky Striker
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheAerialAce").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Armaments" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Armaments of the Sky Stalker
            if (chosenDialogue == 136) //Carian Dark Moon / Konpaku Katana 
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheDarkMoon").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheGardener").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "CarianKonpaku" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Carian Dark Moon / Konpaku Katana 
            if (chosenDialogue == 137) //Neo Dealmaker / Ashen Ambition
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheAnomaly").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfAsh").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "NeonAshen" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

                


            }//Neo Dealmaker / Ashen Ambition
            if (chosenDialogue == 138) //Cosmic Destroyer
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheFuture").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "CosmicDestroyer" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

                
            }//Cosmic Destroyer
            if (chosenDialogue == 139) //The Only Thing I Know For Real
            {
                

            }//The Only Thing I Know for Real
            if (chosenDialogue == 140) //Arachnid Needlepoint
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTechnology").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Arachnid" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);



            }//Arachnid Needlepoint
            if (chosenDialogue == 141) //Mercy
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfBlood").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Mercy" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Mercy
            if (chosenDialogue == 142) //Sakura's Vengeance
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSakura").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Sakura" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Sakura's Vengeance
            if (chosenDialogue == 143) //Eternal Star
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfEternity").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Eternal" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Eternal Star
            if (chosenDialogue == 144) //Vermillion Daemon
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfAdagium").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Vermillion" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Vermilion Daemon
            if (chosenDialogue == 145) //Ozma Ascendant
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheAscendant").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Ozma" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Ozma Ascendant
            if (chosenDialogue == 146) //Dreadnought Chemtank
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfChemtech").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Dreadnought" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Dreadnought Chemtank
            if (chosenDialogue == 147) //The Blood Blade
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfLifethirsting").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "BloodBlade" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);



            }//The Blood Blade
            if (chosenDialogue == 148) //The Morning Star
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfVampirism").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "MorningStar" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//The Morning Star
            if (chosenDialogue == 149) //Hunter's Symphony / Sparkblossom's Beacon
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheHunt").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfStaticShock").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "HunterSparkblossom" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Hunter's Symphony / Sparkblossom's Beacon
            if (chosenDialogue == 150) // Virtue's Edge
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfDestiny").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Virtue" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Virtue's Edge
            if (chosenDialogue == 151) // Vermilion Riposte
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfBalance").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Riposte" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Vermilion Riposte
            if (chosenDialogue == 152) //Burning Desire
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheOverwhelmingBlaze").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "BurningDesire" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Burning Desire
            if (chosenDialogue == 153) //The Everlasting Pickaxe
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheAbyss").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "EverlastingPickaxe" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//The Everlasting Pickaxe
            if (chosenDialogue == 154) //El Capitan's Hardware
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheRenegade").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "CapitanHardware" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//El Capitan's Hardware
            if (chosenDialogue == 155) //Catalyst's Memory
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfQuantum").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "CatalystMemory" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Catalyst's Memory
            if (chosenDialogue == 156) // Gloves of the Black Silence
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSilence").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Gloves" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Gloves of the Black Silence
            if (chosenDialogue == 157) //Soul Reaver
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSouls").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "SoulReaver" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Soul Reaver
            if (chosenDialogue == 158) //AurumEdge
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfGold").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "AurumEdge" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Aurum Edge
            if (chosenDialogue == 159) //Farewells
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfFarewells").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfOffseeing").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "Farewells" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Kevesi/Agnian Farewell
            //Irminsul's Dream (Umbral) || Pod Zero-42 (Astral)
            if (chosenDialogue == 160) //
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheAutomaton").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfNature").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "IrminsulPod" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            if (chosenDialogue == 161)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheTimeless").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Umbra" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Umbra
            if (chosenDialogue == 162)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfPiracy").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "SaltwaterScourge" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Saltwater Scourge
            if (chosenDialogue == 163)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfAbsoluteChaos").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Chaos" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//
            if (chosenDialogue == 164)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheWatch").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Chronoclock" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Chronoclock
            //Kiss of Death
            if (chosenDialogue == 165)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheBehemothTyphoon").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "KissOfDeath" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//
            if (chosenDialogue == 166)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfLightning").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Boltstorm" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Boltstorm Axe
            if (chosenDialogue == 167)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfNanomachines").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "NanomachinaReactor" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Nanomachina Reactor
            if (chosenDialogue == 168)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfDespair").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Sanguine" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Sanguine Despair
            if (chosenDialogue == 169)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSurya").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Sunset" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Sunset of the Sun God
            if (chosenDialogue == 170)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfMania").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Mania" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            if (chosenDialogue == 171)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfAuthority").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "SupremeAuthority" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Supreme Authority
            if (chosenDialogue == 172)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfKinetics").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Kinetic" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Kariumu's Favor
            if (chosenDialogue == 173)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheSoldier").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "ShockAndAwe" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Shock and Awe
            if (chosenDialogue == 174)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfDreams").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Dreamer" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Dreamer's Inkwell
            //Trickspin Two-Step
            if (chosenDialogue == 175)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSpinning").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Trickspin" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            //Dragalia
            if (chosenDialogue == 176)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheDragon").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Dragalia" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            //Gundbit
            if (chosenDialogue == 177)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfFirepower").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Gundbit" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            //Wavedancer
            if (chosenDialogue == 178)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfDancingSeas").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Wavedancer" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }
            //Clarent
            if (chosenDialogue == 179)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfKingslaying").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Clarent" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            //Thespian Weapons (Soliloquy, Havoc)
            if (chosenDialogue == 180)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    if (chosenStarfarer == 1)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfEnergy").Type);

                    }
                    else if (chosenStarfarer == 2)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfHydro").Type);

                    }
                }

                string category = "WeaponDialogue";
                string key = category + "." + "SoliloquyHavoc" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            if (chosenDialogue == 181)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheWolf").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Wolvesbane" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            if (chosenDialogue == 182)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSugar").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Sugarball" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            if (chosenDialogue == 183)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfCookies").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "OrbitalExpresswayPlush" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            if (chosenDialogue == 184)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfNecrosis").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "StringOfCurses" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            if (chosenDialogue == 185)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfChionicEnergy").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "Phasmasaber" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            if (chosenDialogue == 186)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfThePhoenix").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "StarphoenixFunnel" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            if (chosenDialogue == 187)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheShield").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "LegendaryShield" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            if (chosenDialogue == 188)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfASingularity").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "CloakOfAnArbiter" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            if (chosenDialogue == 189)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheHuntress").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "TwoCrownBow" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            if (chosenDialogue == 190)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheRifle").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "InheritedCaseM4A1" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            if (chosenDialogue == 191)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheDarkMaker").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "DreadmotherDarkIdol" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            if (chosenDialogue == 192)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheStars").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "QuisUtDeus" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            if (chosenDialogue == 193)
            {
                if (dialoguePrep)
                {
                    DialogueStartVoiceReward(chosenStarfarer, expression);

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheWhiteNight").Type);

                }

                string category = "WeaponDialogue";
                string key = category + "." + "ParadiseLost" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }

            #endregion
            //Boss item dialogues.
            if (chosenDialogue == 301) //Nalhaun item
            {

                if (dialoguePrep == true)                                     // |
                {
                     

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("AncientShard").Type);
                }
                string category = "BossItemDialogue";
                string key = category + "." + "Nalhaun" + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);



            }//Nalhaun item
            if (chosenDialogue == 302) //Penth item
            {

                if (dialoguePrep == true)                                     // |
                {
                     

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("UnsulliedCanvas").Type);
                }
                string category = "BossItemDialogue";
                string key = category + "." + "Penth" + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Penth item
            if (chosenDialogue == 303) //Arbiter item
            {

                if (dialoguePrep == true)                                     // |
                {
                     

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("DemonicCrux").Type);
                }
                string category = "BossItemDialogue";
                string key = category + "." + "Arbitration" + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);



            }//Arbiter item
            if (chosenDialogue == 304) //Warrior item
            {
                if (dialoguePrep == true)                                     // |
                {
                     

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("ProgenitorWish").Type);
                }
                string category = "BossItemDialogue";
                string key = category + "." + "Warrior" + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);



            }//Warrior item
            if (chosenDialogue == 305) //Dioskouroi Item
            {

                if (dialoguePrep == true)                                     // |
                {
                     

                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("TwincruxPendant").Type);
                }

                string category = "BossItemDialogue";
                string key = category + "." + "Dioskouroi" + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                

            }//Dioskouroi item

            
            if(!writeToArchive)
            {
                dialogue = LangHelper.Wrap(dialogue, 44);

            }
        }

        private static void DialogueStartVoice(int chosenStarfarer, int expression)
        {
            if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().globalVoiceDelayTimer > 0)
            {
                return;
            }
            //Voice system. This has a chance to play when dialogue starts and when its progressed (through clicking the dialogue UI)
            if (chosenStarfarer == 1)
            {
                int randomDialogue = Main.rand.Next(0, 100);
                if (randomDialogue < 65)
                {
                    //65% chance nothing plays.
                }
                else
                {
                    Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().globalVoiceDelayTimer = StarsAbovePlayer.globalVoiceDelayMax * 60;

                    //50% chance the generic dialogue start line plays, 50% chance a line based on the current emotion plays. When clicking dialogue this will just be the emotion line.
                    int randomVoiceLine = Main.rand.Next(0, 2);
                    switch (randomVoiceLine)
                    {
                        case 0:
                            int randomVoice = Main.rand.Next(0, 2);
                            switch (randomVoice)
                            {
                                case 0:
                                    SoundEngine.PlaySound(StarsAboveAudio.AsphodeneDialogueStart0);

                                    break;

                                case 1:
                                    SoundEngine.PlaySound(StarsAboveAudio.AsphodeneDialogueStart1);

                                    break;
                            }
                            break;

                        case 1:
                            int randomA = Main.rand.Next(0, 2);
                            int randomB = Main.rand.Next(0, 3);

                            switch (expression)
                            {
                                case 0:

                                    break;
                                case 1:
                                    switch (randomA)
                                    {
                                        case 0:
                                            SoundEngine.PlaySound(StarsAboveAudio.AsphodeneAngry0);

                                            break;

                                        case 1:
                                            SoundEngine.PlaySound(StarsAboveAudio.AsphodeneAngry1);

                                            break;
                                    }
                                    break;
                                case 2:
                                    SoundEngine.PlaySound(StarsAboveAudio.AsphodeneWorried0);

                                    break;

                                case 3:
                                    switch (randomB)
                                    {
                                        case 0:
                                            SoundEngine.PlaySound(StarsAboveAudio.AsphodeneThinking0);

                                            break;

                                        case 1:
                                            SoundEngine.PlaySound(StarsAboveAudio.AsphodeneThinking1);

                                            break;
                                        case 2:
                                            SoundEngine.PlaySound(StarsAboveAudio.AsphodeneThinking1);

                                            break;
                                    }
                                    break;

                                case 4:
                                    switch (randomA)
                                    {
                                        case 0:
                                            SoundEngine.PlaySound(StarsAboveAudio.AsphodeneSmug0);

                                            break;

                                        case 1:
                                            SoundEngine.PlaySound(StarsAboveAudio.AsphodeneSmug1);

                                            break;
                                    }
                                    break;

                                case 5:
                                    switch (randomA)
                                    {
                                        case 0:
                                            SoundEngine.PlaySound(StarsAboveAudio.AsphodeneHappy0);

                                            break;

                                        case 1:
                                            SoundEngine.PlaySound(StarsAboveAudio.AsphodeneHappy1);

                                            break;
                                    }
                                    break;

                                case 6:

                                    SoundEngine.PlaySound(StarsAboveAudio.AsphodeneDeadInside0);

                                    break;

                            }

                            break;
                    }
                }
            }
            else if (chosenStarfarer == 2)
            {
                int randomDialogue = Main.rand.Next(0, 100);
                if (randomDialogue < 65)
                {
                    //65% chance nothing plays.
                }
                else
                {
                    Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().globalVoiceDelayTimer = StarsAbovePlayer.globalVoiceDelayMax * 60;

                    //50% chance the generic dialogue start line plays, 50% chance a line based on the current emotion plays. When clicking dialogue this will just be the emotion line.
                    int randomVoiceLine = Main.rand.Next(0, 2);
                    switch (randomVoiceLine)
                    {
                        case 0:
                            int randomVoice = Main.rand.Next(0, 2);
                            switch (randomVoice)
                            {
                                case 0:
                                    SoundEngine.PlaySound(StarsAboveAudio.EridaniDialogueStart0);

                                    break;

                                case 1:
                                    SoundEngine.PlaySound(StarsAboveAudio.EridaniDialogueStart1);

                                    break;
                            }
                            break;

                        case 1:
                            int randomA = Main.rand.Next(0, 2);
                            int randomB = Main.rand.Next(0, 3);

                            switch (expression)
                            {
                                case 0:

                                    break;
                                case 1:
                                    switch (randomA)
                                    {
                                        case 0:
                                            SoundEngine.PlaySound(StarsAboveAudio.EridaniAngry0);

                                            break;

                                        case 1:
                                            SoundEngine.PlaySound(StarsAboveAudio.EridaniAngry1);

                                            break;
                                    }
                                    break;
                                case 2:
                                    SoundEngine.PlaySound(StarsAboveAudio.EridaniWorried0);

                                    break;

                                case 3:
                                    switch (randomB)
                                    {
                                        case 0:
                                            SoundEngine.PlaySound(StarsAboveAudio.EridaniThinking0);

                                            break;

                                        case 1:
                                            SoundEngine.PlaySound(StarsAboveAudio.EridaniThinking1);

                                            break;
                                        case 2:
                                            SoundEngine.PlaySound(StarsAboveAudio.EridaniThinking1);

                                            break;
                                    }
                                    break;

                                case 4:
                                    switch (randomA)
                                    {
                                        case 0:
                                            SoundEngine.PlaySound(StarsAboveAudio.EridaniSmug0);

                                            break;

                                        case 1:
                                            SoundEngine.PlaySound(StarsAboveAudio.EridaniSmug1);

                                            break;
                                    }
                                    break;

                                case 5:
                                    switch (randomA)
                                    {
                                        case 0:
                                            SoundEngine.PlaySound(StarsAboveAudio.EridaniHappy0);

                                            break;

                                        case 1:
                                            SoundEngine.PlaySound(StarsAboveAudio.EridaniHappy1);

                                            break;
                                    }
                                    break;

                                case 6:

                                    SoundEngine.PlaySound(StarsAboveAudio.EridaniDeadInside0);

                                    break;

                            }

                            break;
                    }
                }
            }
        }
        private static void DialogueStartVoiceReward(int chosenStarfarer, int expression)
        {
            if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().globalVoiceDelayTimer > 0)
            {
                return;
            }

            //Voice system. This has a chance to play when dialogue starts and when its progressed (through clicking the dialogue UI)
            if (chosenStarfarer == 1)
            {
                int randomDialogue = Main.rand.Next(0, 100);
                if (randomDialogue < 35)
                {
                    //35% chance nothing plays.
                }
                else
                {
                    Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().globalVoiceDelayTimer = StarsAbovePlayer.globalVoiceDelayMax * 60;

                    int randomVoiceLine = Main.rand.Next(0, 5);
                    switch (randomVoiceLine)
                    {
                        case 0:
                            SoundEngine.PlaySound(StarsAboveAudio.AsphodeneNewUnlock0);

                            break;
                        case 1:
                            SoundEngine.PlaySound(StarsAboveAudio.AsphodeneNewUnlock1);

                            break;
                        case 2:
                            SoundEngine.PlaySound(StarsAboveAudio.AsphodeneNewUnlock2);

                            break;
                        case 3:
                            SoundEngine.PlaySound(StarsAboveAudio.AsphodeneNewUnlock3);

                            break;
                        case 4:
                            SoundEngine.PlaySound(StarsAboveAudio.AsphodeneNewUnlock4);

                            break;


                    }
                }
            }
            else if (chosenStarfarer == 2)
            {
                int randomDialogue = Main.rand.Next(0, 100);
                if (randomDialogue < 35)
                {
                    //35% chance nothing plays.
                }
                else
                {
                    Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().globalVoiceDelayTimer = StarsAbovePlayer.globalVoiceDelayMax * 60;


                    int randomVoiceLine = Main.rand.Next(0, 5);
                    switch (randomVoiceLine)
                    {
                        case 0:
                            SoundEngine.PlaySound(StarsAboveAudio.EridaniNewUnlock0);

                            break;
                        case 1:
                            SoundEngine.PlaySound(StarsAboveAudio.EridaniNewUnlock1);

                            break;
                        case 2:
                            SoundEngine.PlaySound(StarsAboveAudio.EridaniNewUnlock2);

                            break;
                        case 3:
                            SoundEngine.PlaySound(StarsAboveAudio.EridaniNewUnlock3);

                            break;
                        case 4:
                            SoundEngine.PlaySound(StarsAboveAudio.EridaniNewUnlock4);

                            break;


                    }
                }
            }
        }
        private static void WriteDialogue(bool writeToArchive, string category, int chosenDialogue, ref bool dialoguePrep, ref int dialogueLeft, ref int expression, ref string dialogue, ref bool dialogueFinished, string baseKey, string key)
        {  
            if (writeToArchive)
            {
                
                if (category == "IdleDialogue" || category == "IdleDialogueHardmode" || category == "RegularIdleDialogue")
                {
                    // Retrieve the player's mod player instance
                    var archivePlayer = Main.LocalPlayer.GetModPlayer<ArchivePlayer>();

                    // Construct a new ArchiveListing instance
                    var newArchiveListing = new ArchiveListing(
                        LangHelper.GetTextValue("Dialogue." + key + "Name"), // Name of the archive listing
                        LangHelper.GetTextValue("Dialogue." + key + "Description"), // Description of the listing
                        true, // Unlock requirement
                        chosenDialogue, // Corresponding dialogue ID
                        LangHelper.GetTextValue("Dialogue." + key + "UnlockCondition") // Unlock requirements UNUSED
                    );

                    // Check if the new ArchiveListing already exists in the IdleArchiveList
                    bool exists = archivePlayer.IdleArchiveList.Any(archive => archive.DialogueID == newArchiveListing.DialogueID);

                    // Add the new ArchiveListing to the list if it doesn't already exist
                    if (!exists)
                    {
                        archivePlayer.IdleArchiveList.Add(newArchiveListing);
                    }                   
                }
                if (category == "BossDialogue" || category == "BossItemDialogue" || category == "CalamityBossDialogue")
                {

                    // Retrieve the player's mod player instance
                    var archivePlayer = Main.LocalPlayer.GetModPlayer<ArchivePlayer>();

                    // Construct a new ArchiveListing instance
                    var newArchiveListing = new ArchiveListing(
                        LangHelper.GetTextValue("Dialogue." + key + "Name"), // Name of the archive listing
                        LangHelper.GetTextValue("Dialogue." + key + "Description"), // Description of the listing
                        true, // Unlock requirement
                        chosenDialogue, // Corresponding dialogue ID
                        LangHelper.GetTextValue("Dialogue." + key + "UnlockCondition") // Unlock requirements UNUSED
                    );

                    // Check if the new ArchiveListing already exists in the IdleArchiveList
                    bool exists = archivePlayer.BossArchiveList.Any(archive => archive.DialogueID == newArchiveListing.DialogueID);

                    // Add the new ArchiveListing to the list if it doesn't already exist
                    if (!exists)
                    {
                        archivePlayer.BossArchiveList.Add(newArchiveListing);
                    }
                    else
                    {

                    }
                }
                if (category == "WeaponDialogue")
                {
                    // Retrieve the player's mod player instance
                    var archivePlayer = Main.LocalPlayer.GetModPlayer<ArchivePlayer>();

                    // Construct a new ArchiveListing instance
                    var newArchiveListing = new ArchiveListing(
                        LangHelper.GetTextValue("Dialogue." + key + "Name"), // Name of the archive listing
                        LangHelper.GetTextValue("Dialogue." + key + "Description"), // Description of the listing
                        true, // Unlock requirement
                        chosenDialogue, // Corresponding dialogue ID
                        LangHelper.GetTextValue("Dialogue." + key + "UnlockCondition") // Unlock requirements UNUSED
                    );

                    // Check if the new ArchiveListing already exists in the IdleArchiveList
                    bool exists = archivePlayer.WeaponArchiveList.Any(archive => archive.DialogueID == newArchiveListing.DialogueID);

                    // Add the new ArchiveListing to the list if it doesn't already exist
                    if (!exists)
                    {
                        archivePlayer.WeaponArchiveList.Add(newArchiveListing);
                    }
                }
            }
            else
            {
                dialogue = LangHelper.GetTextValue("Dialogue." + key + dialogueLeft, Main.LocalPlayer.name);
                expression = SetupExpression(LangHelper.GetTextValue("Dialogue." + key + dialogueLeft + ".E"));
                Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().nextExpression = SetupExpression(LangHelper.GetTextValue("Dialogue." + key + (dialogueLeft + 1) + ".E"));
                if (dialoguePrep == true)
                {
                    dialogueLeft = 1;
                    DialogueStartVoice(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer, expression);
                    dialoguePrep = false;
                }
                if (LangHelper.GetTextValue("Dialogue." + key + (dialogueLeft + 1), Main.LocalPlayer.name) == baseKey + key + (dialogueLeft + 1))//If the next dialogue is going to be the end
                {
                    dialogueFinished = true;
                }
                else
                {

                }
                

            }
        }
        private static void AddDialogueToArchive(int dialogueID, string baseKey, string key)
        {

        }
        private static int SetupExpression(string key)
        {
            switch(key)
            {
                case "Neutral":

                    return 0;
                case "Angry":

                    return 1;
                case "Worried":

                    return 2;
                case "Thinking":

                    return 3;
                case "Smug":

                    return 4;
                case "Happy":

                    return 5;
                case "DeadInside":

                    return 6;
                case "PerseusNeutral":

                    return 12;
            }
            return 0;
        }
    }
}
