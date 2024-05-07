using StarsAbove.Items.Consumables;
using StarsAbove.Systems;
using StarsAbove.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Terraria;
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
                string category = "RegularIdleDialogue";
                string key = category + "." + "NormalIdleDialogue1" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//1
            if (chosenDialogue == 4) // Passive Dialogue 2
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "NormalIdleDialogue2" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//2
            if (chosenDialogue == 5) // Passive Dialogue 2
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "NormalIdleDialogue3" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//3
            if (chosenDialogue == 6) // Passive Dialogue 3
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "NormalIdleDialogue4" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//4
            if (chosenDialogue == 7) // Passive Dialogue 4
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "NormalIdleDialogue5" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//5
            if (chosenDialogue == 8) // Passive Dialogue
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "NormalIdleDialogue6" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//6
            if (chosenDialogue == 9) // Passive Dialogue
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "NormalIdleDialogue7" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//7
            if (chosenDialogue == 10) // Passive Dialogue
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "NormalIdleDialogue8" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//8
            if (chosenDialogue == 11) // Passive Dialogue
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "NormalIdleDialogue9" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//9
             //Post Hardmode Dialogue
            if (chosenDialogue == 12) // Passive Dialogue
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue1" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//1
            if (chosenDialogue == 13) // Passive Dialogue
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue2" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//2
            if (chosenDialogue == 14) // Passive Dialogue
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue3" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//3
            if (chosenDialogue == 15) // Passive Dialogue
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue4" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//4
            if (chosenDialogue == 16) // Passive Dialogue
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue5" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//5
            if (chosenDialogue == 17) // Passive Dialogue
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue6" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//6
            if (chosenDialogue == 18) // Passive Dialogue
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue7" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//7
            if (chosenDialogue == 19) // Passive Dialogue
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue8" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//8
            if (chosenDialogue == 20) // Passive Dialogue
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue9" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//9
            if (chosenDialogue == 21) // A world shrouded in Light
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "LightIdleDialogue" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//A world shrouded in Light
            

            if (chosenDialogue == 400) // Passive Dialogue
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue10" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }
            if (chosenDialogue == 401) // Passive Dialogue
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue11" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }
            if (chosenDialogue == 402) // Passive Dialogue
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue12" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }
            if (chosenDialogue == 403) // Passive Dialogue
            {
                string category = "RegularIdleDialogue";
                string key = category + "." + "HardIdleDialogue13" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                //Scuffed solution, but writing to the archive manually here
                WriteDialogue(true, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }
            if (chosenDialogue == 404) // Passive Dialogue
            {
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
                string category = "BossDialogue";
                string key = category + "." + "KingSlime" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                
            }//Slime King 1
            if (chosenDialogue == 52) // Boss dialogue - Eye of Cthulu
            {
                string category = "BossDialogue";
                string key = category + "." + "CthulhuEye" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Eye of Cthulhu 2
            if (chosenDialogue == 53) // Boss dialogue - Eater of Worlds or Brain of Cthulhu
            {
                string category = "BossDialogue";
                string key = category + "." + "CorruptionBoss" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Brain of Cthulhu / Eater of Worlds 3
            if (chosenDialogue == 54) // Boss dialogue - Queen Bee
            {
                string category = "BossDialogue";
                string key = category + "." + "QueenBee" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Queen Bee 4 
            if (chosenDialogue == 55) // Boss dialogue - Skeletron
            {
                string category = "BossDialogue";
                string key = category + "." + "Skeletron" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);


            }//Skeletron 5 
            if (chosenDialogue == 56) // Boss dialogue - Wall of Flesh
            {
                string category = "BossDialogue";
                string key = category + "." + "WallOfFlesh" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Wall of Flesh 6
            if (chosenDialogue == 58) // Boss dialogue - The Twins
            {
                string category = "BossDialogue";
                string key = category + "." + "Twins" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//The Twins 7
            if (chosenDialogue == 57) // Boss dialogue - The Destroyer
            {
                string category = "BossDialogue";
                string key = category + "." + "Destroyer" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Destroyer 8
            if (chosenDialogue == 59) // Boss dialogue - Skeletron Prime
            {
                string category = "BossDialogue";
                string key = category + "." + "SkeletronPrime" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Skeletron Prime 9
            if (chosenDialogue == 60) // Boss dialogue - All Mechanical Bosses Defeated
            {
                string category = "BossDialogue";
                string key = category + "." + "AllMechs" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//All Mechanical Bosses defeated 10
            if (chosenDialogue == 61) // Boss dialogue - Plantera
            {
                string category = "BossDialogue";
                string key = category + "." + "Plantera" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Plantera 11
            if (chosenDialogue == 62) // Boss dialogue - Golem
            {
                string category = "BossDialogue";
                string key = category + "." + "Golem" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Golem 12
            if (chosenDialogue == 63) // Boss dialogue - Duke Fishron
            {
                string category = "BossDialogue";
                string key = category + "." + "DukeFishron" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Duke Fishron 13
            if (chosenDialogue == 64) // Boss dialogue - Lunatic Cultist
            {
                string category = "BossDialogue";
                string key = category + "." + "Cultist" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Lunatic Cultist 14
            if (chosenDialogue == 65) // Boss dialogue - Moon Lord
            {
                string category = "BossDialogue";
                string key = category + "." + "MoonLord" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Moon Lord 15 
            if (chosenDialogue == 66) // Boss dialogue - Warrior of Light
            {
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
                string key = category + "." + "Penthesilea" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
                
            }//Penthesilea 21
            if (chosenDialogue == 72) // Boss dialogue - Arbitration
            {
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
                string category = "BossDialogue";
                string key = category + "." + "QueenSlime" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Queen Slime 24
            if (chosenDialogue == 75) // Boss dialogue - Empress of Light
            {
                string category = "BossDialogue";
                string key = category + "." + "EmpressOfLight" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Empress of Light 25
            if (chosenDialogue == 76) // Boss dialogue - Deerclops
            {
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

                string category = "BossDialogue";
                string key = category + "." + "Vagrant" + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Vagrant
            //Thespian
            if (chosenDialogue == 78)
            {
                string category = "BossDialogue";
                string key = category + "." + "Thespian" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }


            //Calamity boss dialogue: (Reminder: Calamity boss dialogue starts past 200) If Calamity is not detected, stop at 23. Otherwise...

            if (chosenDialogue == 201) // Boss dialogue - Desert Scourge
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "DesertScourge" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);

            }//Desert Scourge 24
            if (chosenDialogue == 202) // Boss dialogue - Crabulon
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "Crabulon" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Crabulon 25 
            if (chosenDialogue == 203) // Boss dialogue - Hive Mind
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "HiveMind" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Hive Mind 26
            if (chosenDialogue == 204) // Boss dialogue - Perforators
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "Perforators" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Perforators 27
            if (chosenDialogue == 205) // Boss dialogue - The Slime God
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "SlimeGod" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//The Slime God 28
             //Calamity boss dialogue (Hardmode)
            if (chosenDialogue == 206) // Boss dialogue - Cryogen
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "Cryogen" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Cryogen 29
            if (chosenDialogue == 207) // Boss dialogue - Aquatic Scourge
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "AquaticScourge" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Aquatic Scourge 30
            if (chosenDialogue == 208) // Boss dialogue - Brimstone Elemental
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "BrimstoneElemental" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Brimstone Elemental 31
            if (chosenDialogue == 209) // Boss dialogue - Calamitas
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "Calamitas" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Calamitas 32
            if (chosenDialogue == 210) // Boss dialogue - Leviathan
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "Leviathan" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Leviathan 33
            if (chosenDialogue == 211) // Boss dialogue - Astrum Aureus
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "AstrumAureus" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Astrum Aureus 34
            if (chosenDialogue == 212) // Boss dialogue - Plaguebringer Goliath
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "PlaguebringerGoliath" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Plaguebringer Goliath 35
            if (chosenDialogue == 213) // Boss dialogue - Ravager
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "Ravager" + "." + starfarerName + ".";
                WriteDialogue(writeToArchive, category, chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, ref dialogueFinished, baseKey, key);
            }//Ravager 36
            if (chosenDialogue == 214) // Boss dialogue - Astrum Deus
            {
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
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheTreasury").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.21", Player.name); //The Warrior of Light has inspired this Essence. It appears to be a key to a gateway of infinite weapons, doing Summon damage.

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.22", Player.name); //If you feel inspired, try an evil laugh when firing.

                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSurpassingLimits").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.19", Player.name); //From the Warrior of Light.. this Essence allows for the usage of Limit Break. It's an incredibly strong Magic weapon.

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.20", Player.name); //'The Light will cleanse your sins!' Or something like that. 

                        //	" ";
                    }




                }


            }//Light Unrelenting / Key of the King's Law 12
            if (chosenDialogue == 115) // Izanagi's Edge / Hawkmoon
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfIzanagi").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.23", Player.name); //This Essence creates a Ranged weapon with a high Mana cost, and an incredibly high critical damage modifier.

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 5;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.24", Player.name); //Exploitable? Surely. Theorycraft is all you.

                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheHawkmoon").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.21", Player.name); //This weapon can switch between Ranged and Magic. If you time its reload correctly, it always does critical damage.

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.22", Player.name); //I'm sure you can think of something that works around that... 

                        //	" ";
                    }




                }


            }//Izanagi's Edge/ Hawkmoon 13
            if (chosenDialogue == 116) // Key of the Sinner / Crimson Sakura Alpha
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSin").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.25", Player.name); //This is a Summon weapon. It seems to have very unique scaling properties, and attacks incredibly quickly.

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.26", Player.name); //It's also a reflection of your heart... or something. I don't really get that stuff.

                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfAlpha").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.23", Player.name); //This powerful weapon allows you to use Skill Orbs to unleash special moves. It also has a super-powered burst window. It's a Melee weapon.

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.24", Player.name); //You'd be well off with it in your hands.. but it does mean you have to get up close. I don't envy you.

                        //	" ";
                    }




                }


            }//Key Of The Sinner / Crimson Sakura Alpha 14
            if (chosenDialogue == 117) // Phantom In The Mirror / Hollowheart Albion
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfThePhantom").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.27", Player.name); //This is a Summoner weapon, but you'll use it like a Melee weapon. It has the power to swap places instantly with a set place.

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.28", Player.name); //While dangerous, it looks to be pretty powerful. Surely you won't get hit.. right?

                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheHollowheart").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.25", Player.name); //This is a Summon weapon. Holding it summons two cannons to orbit you, and you can fire from both of them. Each of them apply a different debuff.

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.26", Player.name); //The important part is using the other weapon to trigger the other weapon's debuff. You can figure it out.

                        //	" ";
                    }




                }


            }//Phantom In The Mirror / Hollowheart Albion 15
            if (chosenDialogue == 118) // Vision of Euthymia / Kroniic Principality
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfEuthymia").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.29", Player.name); //This is a pretty unique weapon. When using it, it'll follow-up your attacks, and become stronger when you spend Mana.

                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTime").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.27", Player.name); //Here's another Summon weapon. You can attack normally with it, but striking foes with the Timepieces empowers the weapon's attacks.

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 4;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.28", Player.name); //Did I mention you can go back in time? It's powerful, for sure. 

                        //	" ";
                    }




                }


            }//Vision of Euthymia / Kroniic Principality 16
            if (chosenDialogue == 119) // Liberation Blazing / Unforgotten
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfLiberation").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.30", Player.name); //This Essence hails from a world embroiled in conflict... It boasts incredible power, but if you get too overzealous, it'll start to hurt you.

                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfAzakana").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.29", Player.name); //This Essence allows for the creation of unique dual blades which offer varying effects, and allows you to leave your body to overwhelm foes.

                        //	" ";
                    }





                }


            }//Liberation Blazing / Unforgotten 17
            if (chosenDialogue == 120) // Misery's Company
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfMisery").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.31", Player.name); //This Essence creates a Melee weapon that can swap between sword, scythe, or gun. You will be able to use it in nearly any situation!

                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfMisery").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.30", Player.name); //This Essence is utilized in the creation of a Melee Weapon. What sets it apart is the ability to manifest as a sword, scythe, or gun. 

                        //	" ";
                    }





                }


            }//Misery's Company 18
            if (chosenDialogue == 121) //Hullwrought
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheGunlance").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.32", Player.name); //Okay, here's a new Essence for you. It's a huge Melee lance that can be swap between a lance form and a gun form. It looks to be pretty strong!

                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheGunlance").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.31", Player.name); //Here's a new weapon for you to craft. This lance can swap between close and long range, and it can also be charged to great effect.

                        //	" ";
                    }





                }


            }//Hullwrought 19 
            if (chosenDialogue == 122) //Shadowless Cerulean
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheChimera").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.33", Player.name); //{Player.name}, take this Essence. This Melee weapon has an incredible burst, but the refractory period may be dangerous.. Give it a shot, though!

                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheChimera").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.32", Player.name); //This is for you, {Player.name}. It's a super-powered Melee weapon that has an incredible burst phase, followed by a weakness phase. Good luck using it.

                        //	" ";
                    }





                }


            }//Shadowless Cerulean 20
            if (chosenDialogue == 123) //Apalistik
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheOcean").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.34", Player.name); //Here's another Essence you can use! It has incredible synergy with Summons and gets stronger as you do, so if you plan on  fighting with allies, this is it!

                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheOcean").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.33", Player.name); //I have another Essence for you. This weapon is a Summon weapon that works in harmony with minions, and it gets stronger as you do.

                        //	" ";
                    }





                }


            }//Apalistik 21
            if (chosenDialogue == 124) //Luminary Wand 22
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheObservatory").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 2)
                    {
                        expression = 5;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.35", Player.name); //Right, I have an Essence for you. It's a Summon weapon that conjures this mysterious sentient star. It's.. too cute. Anyways...

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.36", Player.name); //It looks to be a powerful weapon, but you'll have to collect the Star Bits for full effect- meaning getting in the way of danger. Good luck..!

                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheObservatory").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.34", Player.name); //I have another Essence for you to use. This Summon-type wand calls forth this adorable little sentient star, and it just looks so.. squishable..

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.35", Player.name); //...Looking past that. You'll have to get up close to use the Star Bits, but they do quite a number on foes if you can land them.

                        //	" ";
                    }




                }


            }//Luminary Wand 22
            if (chosenDialogue == 125) //Xenoblade 23
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheBionis").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.37", Player.name); //Okay. I've got another Essence for you. This Melee weapon can swap between five different stances, granting powerful buffs in the process.

                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheBionis").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.36", Player.name); //I've given you another Essence. This is a Melee weapon that can morph between five different forms, changing the weapon's usage. Interesting.

                        //	" ";
                    }




                }


            }//Xenoblade 23
            if (chosenDialogue == 126) // Stygian Nymph / Caesura of Despair
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfDuality").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.38", Player.name); //Here's a Magic-type Essence. It'll make a scythe with incredible reach and balances between light and dark. Now THAT sounds cool. Right? Right.

                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfIRyS").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.37", Player.name); //Here is another Essence. For you. This is a Summon weapon that will transform you as well as summon crystal shards that weaken and attack nearby foes.

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.38", Player.name); //Hmm.. It's the Essence of Hope, but is there something else lurking beneath the surface? Who knows.. 

                        //	" ";
                    }




                }


            }//Stygian Nymph / Caesura of Despair 24
            if (chosenDialogue == 127) //Claimh Solais
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfRadiance").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.39", Player.name); //Here's another Essence for your use. This powerful Melee blade can deal powerful damage to all foes in your vicinity. How useful is that?

                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfRadiance").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.39", Player.name); //Right... Here's another Essence. This Melee weapon allows you to damage all nearby foes in a huge area. It should be quite useful.

                        //	" ";
                    }




                }


            }//Claimh Solais 25
            if (chosenDialogue == 128) //Penthesilea's Muse 26
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfInk").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.40", Player.name); //Here's another Essence you can prepare to use, from Penthesilea's strength. With its magical paint, we should be able to best powerful foes.

                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfInk").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.40", Player.name); //This is another Essence you can master. It will allow you to use Penthesilea's ink to supress foes and strike bosses powerfully.

                        //	" ";
                    }




                }


            }//Penthesilea's Muse 26
            if (chosenDialogue == 129) //Kifrosse 28
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfFoxfire").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.41", Player.name); //Here you go: an essence for usage in crafting. It looks like it'll apply powerful bouts of flame and frost alike. Not how I'd want to go out, personally.


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfFoxfire").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.41", Player.name); //Okay, I have another Essence for your battles. It summons a mystical fox that burns your enemies with icy energy. They might even deserve it, too.

                        //	" ";
                    }




                }


            }//Kifrosse
            if (chosenDialogue == 130) //Architect's Luminance 29
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfLuminance").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.42", Player.name); //Whoa, this is a powerful Essence. It's potential seems to be magnified based on your Aspected Damage Type. How about giving it a go?


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfLuminance").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.42", Player.name); //It looks like this next Essence is a strong one. Aspected Damage Type seems to empower the weapon further... 

                        //	" ";
                    }




                }


            }//Architect's Luminance
            if (chosenDialogue == 131) //Force Of Nature
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfBlasting").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.43", Player.name); //Okay..! Time for another Essence! This looks to be a Ranged weapon with high damage output at close range. 


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfBlasting").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.43", Player.name); //I've located another Essence for your use. It seems this Ranged-type weapon is hyperfocused on movement. How about pairing it with some mobility tools..?

                        //	" ";
                    }




                }


            }//Force-a-Nature
            if (chosenDialogue == 132) //Genocide
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfExplosions").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 5;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.44", Player.name); //This new Essence is great! It's a Ranged weapon with crazy high explosive potential. Just point it towards the bad guys.


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfExplosions").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.44", Player.name); //Another Essence is yours. It's a strong Ranged weapon with incredibly good room-clearing power. How about that?

                        //	" ";
                    }




                }


            }//Genocide
            if (chosenDialogue == 133) //Takonomicon
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfOuterGods").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 5;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.45", Player.name); //I have a new Essence for you. This is a magical Summon-type weapon that seems to scale with your own potential. Interesting..!


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfOuterGods").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.45", Player.name); //Here's another Essence for you to use. It looks to be a Summon-type weapon that grows in strength with the use of more minion summons.

                        //	" ";
                    }




                }


            }//Takonomicon
            if (chosenDialogue == 134) //Twin Stars of Albiero
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTwinStars").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.46", Player.name); //A Magic Essence is here this time. With cosmic energy, you can blast foes away with superpowered light. Sounds good to me!


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTwinStars").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.46", Player.name); //Here's a Magic-type Essence. With the power of the stars, you can melt even the strongest of foes. Give it a try..?

                        //	" ";
                    }




                }


            }//Twin Stars of Albiero
            if (chosenDialogue == 135) //Armaments of the Sky Striker
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheAerialAce").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.47", Player.name); //This Melee-type Essence should help. It looks to be a multiple-purpose power-suit with myriad attacks. Now that sounds cool!


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheAerialAce").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.47", Player.name); //A Melee Essence is here this time. With its strength, you can create a multifaceted weapon that swaps its arsenal instantly in combat.

                        //	" ";
                    }




                }


            }//Armaments of the Sky Stalker
            if (chosenDialogue == 136) //Carian Dark Moon / Konpaku Katana 
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheDarkMoon").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.48", Player.name); //{Player.name}, I have an Essence for you! It seems to create a Magic-type weapon with powerful piercing damage... It sounds rather helpful.


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheGardener").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.48", Player.name); //{Player.name}, I have an Essence for you. Looks like it'll become a Summon-type weapon... and it gains strength from grazing danger? You'll have to try it.

                        //	" ";
                    }




                }


            }//Carian Dark Moon / Konpaku Katana 
            if (chosenDialogue == 137) //Neo Dealmaker / Ashen Ambition
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheAnomaly").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.49", Player.name); //Okay- I've found another Essence you can use... I think. The only thing I can tell is that it makes a Ranged weapon. The rest is up to you.


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfAsh").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.49", Player.name); //This is another powerful Essence. Looks like it's a Melee weapon, and it has the potential to execute foes. Don't underestimate it.

                        //	" ";
                    }




                }


            }//Neo Dealmaker / Ashen Ambition
            if (chosenDialogue == 138) //Cosmic Destroyer
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheFuture").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.50", Player.name); //Okay- I've got it. Here's a Ranged-type Essence.. it looks to be something pretty powerful! Good luck.


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheFuture").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.50", Player.name); //Time for another essence- this time it's a Ranged-type weapon. Seems to be pretty powerful at general destruction.

                        //	" ";
                    }




                }


            }//Cosmic Destroyer
            if (chosenDialogue == 139) //The Only Thing I Know For Real
            {

                if (dialoguePrep == true)                                     // |
                {
                    dialogueLeft = 3;
                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfBloodshed").Type);
                    dialoguePrep = false;
                }


                if (dialogueLeft == 3)
                {
                    expression = 13;

                    dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Perseus.1", Player.name); //{Player.name}, it's good to see you again. I'm not here to ask for your help- instead, I have a gift for you.


                    //	" ";
                }
                if (dialogueLeft == 2)
                {
                    expression = 13;

                    dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Perseus.2", Player.name); //You have proven yourself as a true warrior; as such, you should have a weapon befitting your strength. So, I bestow


                    //	" ";
                }
                if (dialogueLeft == 1)
                {
                    expression = 13;

                    dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Perseus.3", Player.name); //this weapon unto you. Good luck, and good hunting.  


                    //	" ";
                }



            }//The Only Thing I Know for Real
            if (chosenDialogue == 140) //Arachnid Needlepoint
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTechnology").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.51", Player.name); //A Summon weapon's essence is here. It looks suspiciously like a whip... but it also can summon minions? Now that's peak.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.52", Player.name); //What? 'Peak?' It's new age lingo- You've definitely heard it before. You know? Like.. mountain peak? The top.. of something? Never mind.


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTechnology").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.51", Player.name); //Here's another weapon. Looks to be a summon-type whip that can deploy spider-robots to attack foes.

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 4;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.52", Player.name); //They're spiders... but not real spiders. Yep- thinking like that will stop the fight or flight response. Take notes!

                        //	" ";
                    }



                }


            }//Arachnid Needlepoint
            if (chosenDialogue == 141) //Mercy
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfBlood").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.53", Player.name); //Okay- this is a Melee weapon! My instincts tell me it's perfect for high-defense foes. What are you waiting for?


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfBlood").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.53", Player.name);
                        //dialogue = $"This is a Melee weapon. It has pretty good anti-armor properties- you can definitely put that to good use.";

                        //	" ";
                    }




                }


            }//Mercy
            if (chosenDialogue == 142) //Sakura's Vengeance
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSakura").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.54", Player.name); //This Melee blade can harness the power of the elements themselves. Just having it in your hands is quite something!


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSakura").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.54", Player.name); //Another weapon is here. It seems to be a Melee weapon- one that uses the power of the elements to attack foes.

                        //	" ";
                    }




                }


            }//Sakura's Vengeance
            if (chosenDialogue == 143) //Eternal Star
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfEternity").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.55", Player.name); //This looks to be a Magic weapon. With its power, you can call down entire stars to defeat your foes!


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.56", Player.name); //You're 2 you'll run out eventually? Yeah.. no.


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfEternity").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.55", Player.name); //This Magic-type staff will be incredibly helpful, if I do say so myself. It has the strength to conjure entire stars!

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.56", Player.name); //As long as they aren't REAL stars. There would be... a few problems.

                        //	" ";
                    }



                }


            }//Eternal Star
            if (chosenDialogue == 144) //Vermillion Daemon
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfAdagium").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.57", Player.name); //This looks to be... a Magic weapon. Strange- it feels like an entire armory is contained in this Essence. How's that possible?


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 5;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.58", Player.name); //Well, I'll let you figure that one out. Get to it- I'm curious too, you know?


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfAdagium").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.57", Player.name); //Here's another weapon- Magic, it seems. There's something strange about it- this Essence seems to hold the strength of myriad weapons together.

                        //	" ";
                    }




                }


            }//Vermilion Daemon
            if (chosenDialogue == 145) //Ozma Ascendant
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheAscendant").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.59", Player.name); //Here's a Magic weapon for your usage. Looks to me like it's pretty complicated- you're up to the task though, right? 


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheAscendant").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.58", Player.name); //Here's another unique Magic weapon. Looks to be rather complicated, so good luck using it. Don't forget the ABCs- always be casting!

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 2;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.59", Player.name); //Although, I'm not too sure that it applies here.   

                        //	" ";
                    }



                }


            }//Ozma Ascendant
            if (chosenDialogue == 146) //Dreadnought Chemtank
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfChemtech").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.60", Player.name); //Right.. Here's a Ranged-type weapon Essence.  I hope you've built up your chemical tolerance- it seems to utilize chemical energy to deal powerful damage.


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfChemtech").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.60", Player.name); //This Essence sources a Ranged weapon. It has a high focus on tight mobility, meaning it'll put you into the front lines. The strength should compensate.

                        //	" ";
                    }



                }


            }//Dreadnought Chemtank
            if (chosenDialogue == 147) //The Blood Blade
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfLifethirsting").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.61", Player.name); //Behold- a new Essence! It's Melee damage this time. It has some incredible lifesteal powers- but also seems to drain your health on use? How about giving it a go? 


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfLifethirsting").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.61", Player.name); //A Melee-type Essence is prepared this time. It seems to me like it sacrifies your health for powerful area-of-effect damage. Use it well, but know the risks. 

                        //	" ";
                    }



                }


            }//The Blood Blade
            if (chosenDialogue == 148) //The Morning Star
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfVampirism").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.62", Player.name); //Here's a Summon weapon. It's.. Well, it's a whip, but it has some unique magical properties. It has good striking power, too! 


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfVampirism").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.62", Player.name); //Right, here's a Summon-type whip. It has some magic capability, but it's a bit random in its casting. Try and use it well. 

                        //	" ";
                    }



                }


            }//The Morning Star
            if (chosenDialogue == 149) //Hunter's Symphony / Sparkblossom's Beacon
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheHunt").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 5;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.63", Player.name); //Listen close, {Player.name}! Or not. This Magic weapon is a giant horn, but get this: you can whack people with it! Rad! 


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfStaticShock").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 1;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.63", Player.name); //Be careful with this Essence- for some reason, it discharges static electricity all the time.  

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.64", Player.name); //It'll create a Summon weapon. Hopefully the electric properties carry over- it's sure to give our foes a shock. 

                        //	" ";
                    }


                }


            }//Hunter's Symphony / Sparkblossom's Beacon
            if (chosenDialogue == 150) // Virtue's Edge
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfDestiny").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.64", Player.name); //Careful with this Essence- Apparently, only the worthy can wield it at full strength.  


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.65", Player.name); //An ancient civilization's magnum opus, it seems. Don't underestimate its destructive power. 


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 3;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfDestiny").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 3)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.65", Player.name); //This Essence leads to a truly powerful weapon. It bears remnants of an ancient, advanced civilization. 


                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.66", Player.name); //Evidently, they bore proficiency in spatial manipulation. I'd like you to give it a try, but it might reject the unworthy. 


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 4;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.67", Player.name); //At this point, I wonder if there was a weapon you weren't worthy to wield. Yes, I'm praising you. 


                        //	" ";
                    }


                }


            }//Virtue's Edge
            if (chosenDialogue == 151) // Vermilion Riposte
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfBalance").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.66", Player.name); //Look here, another Essence..! It's going to be Magic-type. It seems to revolve around balancing Black and White Mana to attack. 


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.67", Player.name); //It seems that they were opposites, at least where this thing came from. Makes you wonder... 


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfBalance").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.68", Player.name); //This Magic-type Essence balances attack and healing through the dual-use of Black and White mana. 


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 3;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.69", Player.name); //Two opposite schools of learning, at least in this Essence's original world. Now isn't that thought-provoking. 


                        //	" ";
                    }


                }


            }//Vermilion Riposte
            if (chosenDialogue == 152) //Burning Desire
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheOverwhelmingBlaze").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.68", Player.name); //Thanks for waiting. Here's another Melee-type Essence for your use. While it's deceptively close-ranged, it has the potential for some crazy burst damage. 


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.69", Player.name); //Sure, it'll most likely burn you up from the inside. Just a little, though.


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheOverwhelmingBlaze").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.70", Player.name); //Apologies for the delay- here's an Essence. This Melee weapon has some recoil potential, but in exchange you could deal some explosive amounts of damage.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.71", Player.name); //I'd ask you to use it in moderation, but I'm sure you'll use it as much as you like. Bring some healing potions, at the very least.


                        //	" ";
                    }


                }


            }//Burning Desire
            if (chosenDialogue == 153) //The Everlasting Pickaxe
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheAbyss").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.70", Player.name); //Okay, here's an unorthodox Essence for you. While it functions as a pretty strong pickaxe, it looks like it also has some great damage potential.  


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.71", Player.name); //From my take, it seems to be a great off-hand weapon to pump out explosions. Everyone loves explosions, right? 


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheAbyss").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.72", Player.name); //This Essence focuses on excavation as well as damage. You can use it to move earth at significant rates, and additionally deal powerful explosive damage. 


                        //	" ";
                    }



                }


            }//The Everlasting Pickaxe
            if (chosenDialogue == 154) //The Everlasting Pickaxe
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheRenegade").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.72", Player.name); //Okay, here's an unorthodox Essence for you. While it functions as a pretty strong pickaxe, it looks like it also has some great damage potential.  


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheRenegade").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.73", Player.name); //This Essence focuses on excavation as well as damage. You can use it to move earth at significant rates, and additionally deal powerful explosive damage. 


                        //	" ";
                    }



                }


            }//El Capitan's Hardware
            if (chosenDialogue == 155) //Catalyst's Memory
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfQuantum").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.73", Player.name); //


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfQuantum").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.74", Player.name); //


                        //	" ";
                    }



                }


            }//Catalyst's Memory
            if (chosenDialogue == 156) // Gloves of the Black Silence
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSilence").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.74", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.75", Player.name);


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 3;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSilence").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 3)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.75", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 4;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.76", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.77", Player.name);


                        //	" ";
                    }


                }


            }//Gloves of the Black Silence
            if (chosenDialogue == 157) //Soul Reaver
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSouls").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.76", Player.name);


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSouls").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.78", Player.name);


                        //	" ";
                    }


                }


            }//Soul Reaver
            if (chosenDialogue == 158) //AurumEdge
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfGold").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.77", Player.name);


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfGold").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.79", Player.name);


                        //	" ";
                    }


                }


            }//Aurum Edge
            if (chosenDialogue == 159) //Farewells
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfFarewells").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.78", Player.name);


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfOffseeing").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.80", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.81", Player.name);


                        //	" ";
                    }

                }


            }//Kevesi/Agnian Farewell
            //Irminsul's Dream (Umbral) || Pod Zero-42 (Astral)
            if (chosenDialogue == 160) //
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheAutomaton").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.79", Player.name);


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfNature").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.82", Player.name);


                        //	" ";
                    }


                }


            }
            if (chosenDialogue == 161)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheTimeless").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.80", Player.name);


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheTimeless").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.83", Player.name);


                        //	" ";
                    }


                }


            }//Umbra
            if (chosenDialogue == 162)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfPiracy").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.81", Player.name);


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfPiracy").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.84", Player.name);


                        //	" ";
                    }


                }


            }//Saltwater Scourge
            if (chosenDialogue == 163)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfAbsoluteChaos").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.82", Player.name);


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfAbsoluteChaos").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.85", Player.name);


                        //	" ";
                    }


                }


            }//
            if (chosenDialogue == 164)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheWatch").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.83", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 3;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.84", Player.name);


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheWatch").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.86", Player.name);


                        //	" ";
                    }


                }


            }//Chronoclock
            if (chosenDialogue == 165)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheBehemothTyphoon").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.85", Player.name);


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheBehemothTyphoon").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.87", Player.name);


                        //	" ";
                    }


                }


            }//Nanomachina Reactor
            if (chosenDialogue == 166)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfLightning").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.86", Player.name);


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfLightning").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.88", Player.name);


                        //	" ";
                    }


                }


            }//Boltstorm Axe
            if (chosenDialogue == 167)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfNanomachines").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.87", Player.name);


                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfNanomachines").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.89", Player.name);


                        //	" ";
                    }


                }


            }//Nanomachina Reactor
            if (chosenDialogue == 168)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfDespair").Type);
                        dialoguePrep = false;
                    }


                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.88", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 4;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.89", Player.name);


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfDespair").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.90", Player.name);


                        //	" ";
                    }


                }


            }//Sanguine Despair
            if (chosenDialogue == 169)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSurya").Type);
                        dialoguePrep = false;
                    }



                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.90", Player.name);


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSurya").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.91", Player.name);


                        //	" ";
                    }


                }


            }//Sunset of the Sun God
            if (chosenDialogue == 170)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfMania").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 4;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.91", Player.name);


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfMania").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.92", Player.name);


                        //	" ";
                    }


                }


            }
            if (chosenDialogue == 171)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfAuthority").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 4;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.92", Player.name);


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfAuthority").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.93", Player.name);


                        //	" ";
                    }


                }


            }//Supreme Authority
            if (chosenDialogue == 172)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfKinetics").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 4;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.93", Player.name);


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfKinetics").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.94", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.95", Player.name);


                        //	" ";
                    }

                }


            }//Kariumu's Favor
            if (chosenDialogue == 173)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheSoldier").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 4;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.94", Player.name);


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheSoldier").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.96", Player.name);


                        //	" ";
                    }


                }


            }//Shock and Awe
            if (chosenDialogue == 174)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfDreams").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 4;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.95", Player.name);


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfDreams").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.97", Player.name);


                        //	" ";
                    }


                }


            }//Dreamer's Inkwell
            //Trickspin Two-Step
            if (chosenDialogue == 175)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSpinning").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 4;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.96", Player.name);


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSpinning").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.98", Player.name);


                        //	" ";
                    }


                }


            }
            //Dragalia
            if (chosenDialogue == 176)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheDragon").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 4;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.97", Player.name);


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheDragon").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.99", Player.name);


                        //	" ";
                    }


                }


            }
            //Gundbit
            if (chosenDialogue == 177)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfFirepower").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.98", Player.name);


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfFirepower").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.100", Player.name);


                        //	" ";
                    }
                }
            }
            //Wavedancer
            if (chosenDialogue == 178)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfDancingSeas").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 4;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.99", Player.name);


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfDancingSeas").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.101", Player.name);


                        //	" ";
                    }


                }


            }
            //Clarent
            if (chosenDialogue == 179)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfKingslaying").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 4;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.100", Player.name);


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfKingslaying").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.102", Player.name);


                        //	" ";
                    }


                }


            }
            //Thespian Weapons (Soliloquy, Havoc)
            if (chosenDialogue == 180)
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfEnergy").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.101", Player.name);


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfHydro").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.103", Player.name);


                        //	" ";
                    }


                }


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
                if (dialoguePrep == true)
                {
                    dialogueLeft = 1;
                    dialoguePrep = false;
                }
                if (LangHelper.GetTextValue("Dialogue." + key + (dialogueLeft + 1)) == baseKey + key + (dialogueLeft + 1))//If the next dialogue is going to be the end
                {
                    dialogueFinished = true;
                }
                else
                {

                }
                dialogue = LangHelper.GetTextValue("Dialogue." + key + dialogueLeft, Main.LocalPlayer.name);
                expression = SetupExpression(LangHelper.GetTextValue("Dialogue." + key + dialogueLeft + ".E"));

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
