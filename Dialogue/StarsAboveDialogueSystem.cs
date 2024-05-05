using StarsAbove.Items.Consumables;
using StarsAbove.Systems;
using StarsAbove.Utilities;
using System;
using System.Collections.Generic;
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
        public static void SetupDialogueSystem(int chosenStarfarer, ref int chosenDialogue, ref bool dialoguePrep, ref int dialogueLeft, ref int expression, ref string dialogue, ref bool dialogueFinished, Player Player, Mod Mod)
        {
            
            if (chosenDialogue == 2) // Contingency text
            {
                if (chosenStarfarer == 1) // Asphodene
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        string key = $"Dialogue.IdleDialogueHardmode.Asphodene";
                        LangHelper.GetCategorySize(key);
                        expression = 2;
                        if (Main.hardMode)
                        {
                            /*dialogue = $"Sorry, {Player.name}. Nothing to" +
                            " comment on right now.";*/
                            dialogue = LangHelper.GetTextValue($"Dialogue.IdleDialogueHardmode.Asphodene", Player.name);

                        }
                        else
                        {
                            /*dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //Sorry, there's not much on my mind as of late.*/
                            dialogue = LangHelper.GetTextValue($"Dialogue.IdleDialogue.Asphodene");
                        }

                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 2;
                        if (Main.hardMode)
                        {
                            dialogue = LangHelper.GetTextValue($"Dialogue.IdleDialogueHardmode.Eridani", Player.name);
                        }
                        else
                        {
                            dialogue = LangHelper.GetTextValue($"Dialogue.IdleDialogue.Eridani", Player.name);
                        }

                    }
                }


            }//Fallback idle dialogue (timer)
             //Pre Hardmode Idle Dialogue
             //Finished rework.
            string baseKey = "Mods.StarsAbove.Dialogue.";
            #region idleDialogue
            string starfarerName = "Asphodene";
            if(chosenStarfarer == 1)
            {
                starfarerName = "Asphodene";
            }
            else if (chosenStarfarer == 2)
            {
                starfarerName = "Eridani";
            }
            if (chosenDialogue == 3) // Passive Dialogue 1
            {
                string key = "RegularIdleDialogue.NormalIdleDialogue1." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//1
            if (chosenDialogue == 4) // Passive Dialogue 2
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue2.Asphodene.1", Player.name); //It's fine to relax, but you can't forget our end goal, now.
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue2.Eridani.1", Player.name); //Rest is important.. Take as much time as you need.
                    }
                }


            }//2
            if (chosenDialogue == 5) // Passive Dialogue 2
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue3.Asphodene.1", Player.name); //Honestly, I prefer the flashy weapons. A little 'extra' never hurt anyone.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 4;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue3.Asphodene.2", Player.name); //Unless you were on the receiving end. 
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue3.Eridani.1", Player.name); //Weapon looks? They matter, but utility far outweighs cosmetic value.
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue3.Eridani.2", Player.name); //Sometimes, though... The choice is difficult. You get what I mean, right?
                    }
                }


            }//3
            if (chosenDialogue == 6) // Passive Dialogue 3
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue4.Asphodene.1", Player.name); //What is a 'Starfarer' you ask? Hmm, there's a lot to go over.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue4.Asphodene.2", Player.name); //In short, we're the manifestation of the universe's will to vanquish threats...?
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue4.Asphodene.3", Player.name); //Sorry. I think my memory's a little foggy. Just know I'm on your side, whatever may come.
                        //	" ";
                    }

                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue4.Eridani.1", Player.name); //What is a 'Starfarer'? Let me see...
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue4.Eridani.2", Player.name); //Using your terms, we would be referred to as a sort of demigod. The universe calls us into action when needed.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue4.Eridani.3", Player.name); //That wasn't very helpful? Sorry. I feel like my memories are foggy. Don't fret, though- I'm with you all the way.
                        //	" ";
                    }
                }


            }//4
            if (chosenDialogue == 7) // Passive Dialogue 4
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue5.Asphodene.1", Player.name); //Don't forget about the Stellar Array! 
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue5.Asphodene.2", Player.name); //It would be a shame if you did. I made it for you, after all.
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue5.Eridani.1", Player.name); //Have you been utilizing the Stellar Array? It'll help. Wait, forget I said that. Of course you know it'll help...
                        //	" ";
                    }

                }


            }//5
            if (chosenDialogue == 8) // Passive Dialogue
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue6.Asphodene.1", Player.name); //Hmm... 
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue6.Asphodene.2", Player.name); //Ah, it's nothing. Keep up the good work.
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue6.Eridani.1", Player.name); //...Hm.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue6.Eridani.2", Player.name); //Ah.. it's nothing.
                        //	" ";
                    }

                }


            }//6
            if (chosenDialogue == 9) // Passive Dialogue
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 4;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue7.Asphodene.1", Player.name); //{Player.name}! I have this idea about a weapon..   
                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue7.Asphodene.2", Player.name); //Wait.   
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue7.Asphodene.3", Player.name); //Uh.. Never mind. Forget I said anything.  
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue7.Asphodene.4", Player.name); //It would be far too large, to boot. And then there's the whole heating problem... 
                        //	" ";
                    }

                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue7.Eridani.1", Player.name); //So, there's this weapon idea I was thinking of, {Player.name}.  
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue7.Eridani.2", Player.name); //Wait.. Actually.. Forget it. Never mind.  
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue7.Eridani.3", Player.name); //Even if we got past the energy cost, we'd have to deal with the weight... 
                        //	" ";
                    }
                }


            }//7
            if (chosenDialogue == 10) // Passive Dialogue
            {
                if (chosenStarfarer == 1) // Asphodene  //0 Neutral | 1 Dissatisfied | | 3 4 | 4 3 | 5 Sigh | 6 Intrigued
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue8.Asphodene.1", Player.name); //Aren't some of these townspeople kind of awful?
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue8.Asphodene.2", Player.name); //Everything costs so much! And they all have a monopoly!!
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue8.Eridani.1", Player.name); //These merchants are definitely upselling their wares for far too much!
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue8.Eridani.2", Player.name); //Do you think they'd notice if we shortchanged them, just a little? We shouldn't do that? I know... ...I know.
                        //	" ";
                    }

                }


            }//8
            if (chosenDialogue == 11) // Passive Dialogue
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue9.Asphodene.1", Player.name); //Don't forget about the Stellar Array! 
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue9.Asphodene.2", Player.name); //It would be a shame if you did. I made it for you, after all.
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue9.Eridani.1", Player.name); //Have you been utilizing the Stellar Array?
                        //	" ";
                    }

                }


            }//9
             //Post Hardmode Dialogue
            if (chosenDialogue == 12) // Passive Dialogue
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue1.Asphodene.1", Player.name); //So, what's next on the agenda? Let's see..
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue1.Asphodene.2", Player.name); //Have you been working on your town? Up for some mining? Ready for the next boss?
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue1.Asphodene.3", Player.name); //It's up to you. I'll be here. 
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue1.Eridani.1", Player.name); //Mind if I suggest your next move? Um...
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue1.Eridani.2", Player.name); //If you need more town space, there's that. You might also need some more ores, unless you're ready for the next boss?
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue1.Eridani.3", Player.name); //Just suggestions. I trust your judgement.
                        //	" ";
                    }

                }


            }//1
            if (chosenDialogue == 13) // Passive Dialogue
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        if (DownedBossSystem.downedVagrant)
                        {
                            dialogueLeft = 1;
                            dialoguePrep = false;
                        }
                        else
                        {
                            dialogueLeft = 2;
                            dialoguePrep = false;
                        }

                    }
                    if (DownedBossSystem.downedVagrant)
                    {
                        if (dialogueLeft == 1)
                        {
                            expression = 1;
                            dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue2.Asphodene.1", Player.name); //Hmm. Maybe I should think of cooler lines to say when I use the Stellar Nova?.
                            //	" ";
                        }
                    }
                    else
                    {
                        if (dialogueLeft == 2)
                        {
                            expression = 3;
                            dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue2.Asphodene.2", Player.name); //I feel like I'm missing something important.. 
                            //	" ";
                        }
                        if (dialogueLeft == 1)
                        {
                            expression = 0;
                            dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue2.Asphodene.3", Player.name); //Ah well. No use worrying about what you can't solve, right?
                            //	" ";
                        }

                    }

                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        if (DownedBossSystem.downedVagrant)
                        {
                            dialogueLeft = 1;
                            dialoguePrep = false;
                        }
                        else
                        {
                            dialogueLeft = 2;
                            dialoguePrep = false;
                        }

                    }
                    if (DownedBossSystem.downedVagrant)
                    {
                        if (dialogueLeft == 2)
                        {
                            expression = 3;
                            dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue2.Eridani.1", Player.name); //Using the Stellar Novas is rather tiring. 
                            //	" ";
                        }
                        if (dialogueLeft == 1)
                        {
                            expression = 2;
                            dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue2.Eridani.2", Player.name); //There's a lot of stories about heroes borrowing powers from others, but they never tell you how hard it is on the other side...
                            //	" ";
                        }
                    }
                    else
                    {
                        if (dialogueLeft == 2)
                        {
                            expression = 3;
                            dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue2.Eridani.3", Player.name); //I feel like I'm missing something important.. 
                            //	" ";
                        }
                        if (dialogueLeft == 1)
                        {
                            expression = 0;
                            dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue2.Eridani.4", Player.name); //It's bothering me, but.. It doesn't look like we can solve it now.
                            //	" ";
                        }
                    }

                }


            }//2
            if (chosenDialogue == 14) // Passive Dialogue
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue3.Asphodene.1", Player.name); //Ah- I wonder what Eri is doing right about now?
                        //	" ";
                    }

                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue3.Eridani.1", Player.name); //I wonder how my sister is faring?  
                        //	" ";
                    }

                }


            }//3
            if (chosenDialogue == 15) // Passive Dialogue
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue4.Asphodene.1", Player.name); //I'm liking your outfit! I guess we do think alike.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue4.Asphodene.2", Player.name); //Maybe lose the shoes. 
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 4;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue4.Eridani.1", Player.name); //Is your armor up to par? Looks-wise, it's... passable.
                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue4.Eridani.2", Player.name); //Um, I'll pre-emptively say this: Say anything about my outfit and..
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue4.Eridani.3", Player.name); //I'll probably cry. 
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue4.Eridani.4", Player.name); //What? It's the truth. 
                        //	" ";
                    }
                }


            }//4
            if (chosenDialogue == 16) // Passive Dialogue
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue5.Asphodene.1", Player.name); //After all this is over with, how do you feel about joining me? Think of all the good times we've had already!
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue5.Eridani.1", Player.name); //..Maybe we should come back to this conversation. We've still got a long way to go.
                        //	" ";
                    }

                }


            }//5
            if (chosenDialogue == 17) // Passive Dialogue
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 4;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue6.Asphodene.1", Player.name); //You're getting pretty strong! Think you can beat me in a fight?
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 6;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue6.Asphodene.2", Player.name); //That was a joke. You probably could. 
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 4;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue6.Eridani.1", Player.name); //You just keep getting stronger, huh? I'll have to work harder to keep up.
                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 4;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue6.Eridani.2", Player.name); //Or not. Theoretically, if we fought, I'd probably win. Don't you think?
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue6.Eridani.3", Player.name); //Don't get any funny ideas, though. We're decidedly on the same team.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue6.Eridani.4", Player.name); //(I wonder if they caught my bluff.) 
                        //	" ";
                    }
                }


            }//6
            if (chosenDialogue == 18) // Passive Dialogue
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue7.Asphodene.1", Player.name); //I can barely imagine where some of these monsters have come from.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue7.Asphodene.2", Player.name); //Some lich somewhere ate a few bad mushrooms and just went to town, huh? Or maybe a portal to a dimension of primordial soup?
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue7.Eridani.1", Player.name); //Whoever keeps coming up with these foes has no shortage of imagination..
                        //	" ";
                    }

                }


            }//7
            if (chosenDialogue == 19) // Passive Dialogue
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue8.Asphodene.1", Player.name); //Truly... The Hallowed is just a big facade.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 4;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue8.Asphodene.2", Player.name); //For such a pretty biome, it REALLY wants to kill you.
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue8.Eridani.1", Player.name); //Strip away the Hallowed's mask, and it may even be harsher than the Underworld itself.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue8.Eridani.2", Player.name); //If you're off to farm Souls, stay safe. Even the best can can be caught off-guard.
                        //	" ";
                    }


                }


            }//8
            if (chosenDialogue == 20) // Passive Dialogue
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue9.Asphodene.1", Player.name); //We've come a long way..  don't you think?
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 4;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue9.Asphodene.2", Player.name); //That doesn't mean it's time to stop!
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue9.Eridani.1", Player.name); //Looking back on this journey.. We've come far.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue9.Eridani.2", Player.name); //Looking back on this journey.. We've come far.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue9.Eridani.3", Player.name); //And yet, we have so much more to accomplish.
                        //	" ";
                    }
                }


            }//9
            if (chosenDialogue == 21) // A world shrouded in Light
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.LightIdleDialogue.Asphodene.1", Player.name); //This endless light is awful... There must be some way to stop it.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.LightIdleDialogue.Asphodene.2", Player.name); //Perseus told us to use the 'Progenitor's Wish.' Perhaps it holds the key?
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.LightIdleDialogue.Eridani.1", Player.name); //This everlasting light is dreadful...
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.LightIdleDialogue.Eridani.2", Player.name);
                        //	" ";
                    }
                }


            }//A world shrouded in Light
            if (chosenDialogue == 22) // The first time visiting the Observatory
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 6;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 6)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //{Player.name}, you've made it! This place is the Observatory Hyperborea; our base of operations, if you will.$4
                        //	" ";
                    }
                    if (dialogueLeft == 5)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //There are quite a few Observatories around the galaxy, but we call this one home. It's not the source of our power, but acts like a magnifying glass of sorts.
                        //	" ";
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 5;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //Here, we can search the universe for heavy concentrations of mana, which is usually a bad sign. Think of it like clouds before a storm.
                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 5;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //Enough about us. You're probably wondering what YOU can do in the Observatory, right? Let me tell you. 
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //By accessing the Gateway, you can travel to other worlds. I'm sure you can already see the practical purpose of that.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //There may not be many worlds open right now, but I have a feeling when we defeat powerful foes, more places will be available. Good luck!
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 6;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 6)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //{Player.name}, welcome. This place is the Observatory Hyperborea; both Asphodene and I live here.$4
                        //	" ";
                    }
                    if (dialogueLeft == 5)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //There are a few Observatories located around the galaxy, but this is ours. Its main function is an amplifier for our power, to an extent.
                        //	" ";
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 5;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //Here, we can search the universe for heavy concentrations of mana, which is kind of like an omen for trouble. It works out more than you'd think.
                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 3;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //You're probably wondering about what you can use this Observatory for, right? Let's see...
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //By accessing the Gateway, you can travel to other worlds. I needn't elaborate on the utility of that, right?
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //There may not be many worlds open right now, but I have a feeling when we defeat powerful foes, more worlds will open.. Hopefully.
                        //	" ";
                    }
                }


            }//Observatory Hyperborea (First visit) 1
            if (chosenDialogue == 23) //Idle dialogue within the Observatory Hyperborea
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //Yeah, I wondered when you would ask about the crates and stuff around the Observatory. Well, unlike the original owners, we need to eat, obviously.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 3;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //Wait, did you seriously think we didn't have to eat? That would be awful.    
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //Oh, the crates around the Observatory? It's mostly food. Did you think we didn't have to eat? We'd be missing out on a lot.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //As long as we plan on staying here, we'll need supplies. Lucky for us, the galaxy has no shortage of abandoned planets to loot. I mean... they're not using it anymore.
                        //	" ";
                    }
                }


            }//Observatory Hyperborea (Idle) 2
            if (chosenDialogue == 24) //Explaining Cosmic Voyages
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 6;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 6)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //It's about time I explained Cosmic Voyages. Do you see that blue thing in the Observatory? If you step on it, it'll create a Gateway that you can use the Bifrost on.
                        //	" ";
                    }
                    if (dialogueLeft == 5)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //Once you're on the voyage, you can't break or place anything. Also, you can't fly or mount. If you'd like to get around, try conventional methods, like a grappling hook.
                        //	" ";
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 3;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //You have to use the Bifrost at the Gateway to return to the Observatory. Take care that  you don't get stuck, because.. Self-explanatory. How about keeping a Magic Mirror handy?
                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 1;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //Remember that you're visiting harsh places and not everything is friendly. You'll most likely be inflicted with some sort of Environmental  Turmoil, which will affect your stats.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        // dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //This is important: any abilities that have become stronger after defeating foes will be significantly weaker. It's the downside of travelling so far away from the Observatory.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //Once you've initiated the Voyage, it'll take a little while until you can go on another one. Make every journey count. Good luck out there!
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 6;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 6)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //Okay. I'm going to brief you on Cosmic Voyages. Do you see that blue platform? Stepping on it will create a Gateway that you can use the Bifrost on to initiate transit.
                        //	" ";
                    }
                    if (dialogueLeft == 5)
                    {
                        expression = 1;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //You can't break or place blocks during the Voyage. Mounts and flying are also a no-go. You'll have to resort to things like a grappling hook to navigate.
                        //	" ";
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //If you'd like to return to the Observatory, you have to use the Bifrost near the Gateway. Take care to not get trapped away from the Gateway, or bring a Magic Mirror.
                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //Remember that you're visiting harsh locales and not everything is friendly. You'll most likely be inflicted with a kind of Environmental Turmoil, which will affect your physique.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //After you begin a Cosmic Voyage, you'll have to wait a little while until you can initiate another. You should make every excursion count.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //There's one last thing. Anything that has become stronger after defeating powerful foes will be weaker. It's an unfortunate side-effect of travelling so far away from the Observatory.
                        //	" ";
                    }
                }


            }//Explaining Cosmic Voyages (Unused)
            if (chosenDialogue == 25) // The Sea of Stars
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 3;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //You're wondering how you can breathe? Simple. The Bifrost is protecting you!
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 5;
                        // dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //It draws ambient mana from nearby planets to sustain bodily functions. You can't tell me that doesn't sound neat- just don't fall too far from solid ground.
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 3;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //Eh? You're wondering how you can breathe? Ah, that would be the Bifrost you used to reach the Observatory. Right.. I should've probably explained that.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //It draws ambient mana from nearby planets and converts it to aether. It should prove ample enough for your journey. Just remember that 'down' is a perspective thing.
                        //	" ";
                    }
                }


            }//In space (Idle, Unused)

            if (chosenDialogue == 400) // Passive Dialogue
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue10.Asphodene.1", Player.name); //We've come a long way..  don't you think?
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue10.Asphodene.2", Player.name); //That doesn't mean it's time to stop!
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue10.Eridani.1", Player.name); //Looking back on this journey.. We've come far.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue10.Eridani.2", Player.name); //And yet, we have so much more to accomplish.
                        //	" ";
                    }
                }


            }
            if (chosenDialogue == 401) // Passive Dialogue
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 4;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue11.Asphodene.1", Player.name); //We've come a long way..  don't you think?
                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue11.Asphodene.2", Player.name); //That doesn't mean it's time to stop!
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 4;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue11.Asphodene.3", Player.name); //That doesn't mean it's time to stop!
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 4;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue11.Asphodene.4", Player.name); //That doesn't mean it's time to stop!
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue11.Eridani.1", Player.name); //Looking back on this journey.. We've come far.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 4;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue11.Eridani.2", Player.name); //And yet, we have so much more to accomplish.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue11.Eridani.3", Player.name); //And yet, we have so much more to accomplish.
                        //	" ";
                    }
                }


            }
            if (chosenDialogue == 402) // Passive Dialogue
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue12.Asphodene.1", Player.name); //We've come a long way..  don't you think?
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue12.Asphodene.2", Player.name); //That doesn't mean it's time to stop!
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue12.Asphodene.3", Player.name); //That doesn't mean it's time to stop!
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue12.Eridani.1", Player.name); //Looking back on this journey.. We've come far.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue12.Eridani.2", Player.name); //And yet, we have so much more to accomplish.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue12.Eridani.3", Player.name); //And yet, we have so much more to accomplish.
                        //	" ";
                    }
                }


            }
            if (chosenDialogue == 403) // Passive Dialogue
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue13.Asphodene.1", Player.name);
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue13.Asphodene.2", Player.name);
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue13.Asphodene.3", Player.name);
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue13.Eridani.1", Player.name);
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 4;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue13.Eridani.2", Player.name);
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue13.Eridani.3", Player.name);
                        //	" ";
                    }
                }


            }
            if (chosenDialogue == 404) // Passive Dialogue
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue13.Asphodene.1", Player.name); //We've come a long way..  don't you think?
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue13.Asphodene.2", Player.name); //That doesn't mean it's time to stop!
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue13.Eridani.1", Player.name); //Looking back on this journey.. We've come far.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.HardIdleDialogue13.Eridani.2", Player.name); //And yet, we have so much more to accomplish.
                        //	" ";
                    }
                }


            }
            #endregion
            #region bossDialogue
            //Boss dialogue
            if (chosenDialogue == 51) // Boss dialogue - Slime King
            {
                string category = "BossDialogue";
                string key = category + "." + "KingSlime" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
                
            }//Slime King 1
            if (chosenDialogue == 52) // Boss dialogue - Eye of Cthulu
            {
                string category = "BossDialogue";
                string key = category + "." + "CthulhuEye" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Eye of Cthulhu 2
            if (chosenDialogue == 53) // Boss dialogue - Eater of Worlds or Brain of Cthulhu
            {
                string category = "BossDialogue";
                string key = category + "." + "CorruptionBoss" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);

            }//Brain of Cthulhu / Eater of Worlds 3
            if (chosenDialogue == 54) // Boss dialogue - Queen Bee
            {
                string category = "BossDialogue";
                string key = category + "." + "QueenBee" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);


            }//Queen Bee 4 
            if (chosenDialogue == 55) // Boss dialogue - Skeletron
            {
                string category = "BossDialogue";
                string key = category + "." + "Skeletron" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);


            }//Skeletron 5 
            if (chosenDialogue == 56) // Boss dialogue - Wall of Flesh
            {
                string category = "BossDialogue";
                string key = category + "." + "WallOfFlesh" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);

            }//Wall of Flesh 6
            if (chosenDialogue == 58) // Boss dialogue - The Twins
            {
                string category = "BossDialogue";
                string key = category + "." + "Twins" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);

            }//The Twins 7
            if (chosenDialogue == 57) // Boss dialogue - The Destroyer
            {
                string category = "BossDialogue";
                string key = category + "." + "Destroyer" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);

            }//Destroyer 8
            if (chosenDialogue == 59) // Boss dialogue - Skeletron Prime
            {
                string category = "BossDialogue";
                string key = category + "." + "SkeletronPrime" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);

            }//Skeletron Prime 9
            if (chosenDialogue == 60) // Boss dialogue - All Mechanical Bosses Defeated
            {
                string category = "BossDialogue";
                string key = category + "." + "AllMechs" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//All Mechanical Bosses defeated 10
            if (chosenDialogue == 61) // Boss dialogue - Plantera
            {
                string category = "BossDialogue";
                string key = category + "." + "Plantera" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Plantera 11
            if (chosenDialogue == 62) // Boss dialogue - Golem
            {
                string category = "BossDialogue";
                string key = category + "." + "Golem" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Golem 12
            if (chosenDialogue == 63) // Boss dialogue - Duke Fishron
            {
                string category = "BossDialogue";
                string key = category + "." + "DukeFishron" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Duke Fishron 13
            if (chosenDialogue == 64) // Boss dialogue - Lunatic Cultist
            {
                string category = "BossDialogue";
                string key = category + "." + "Cultist" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Lunatic Cultist 14
            if (chosenDialogue == 65) // Boss dialogue - Moon Lord
            {
                string category = "BossDialogue";
                string key = category + "." + "MoonLord" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Moon Lord 15 
            if (chosenDialogue == 66) // Boss dialogue - Warrior of Light
            {
                string category = "BossDialogue";
                string key = category + "." + "WarriorOfLight" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Warrior of Light 16
            if (chosenDialogue == 67) // Boss dialogue - All Vanilla Bosses Defeated
            {
                if (dialoguePrep == true)
                {
                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheCosmos").Type);
                }
                string category = "BossDialogue";
                string key = category + "." + "AllVanillaBosses" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//All Vanilla Bosses 17
            if (chosenDialogue == 68) // Boss dialogue - Everything Defeated in Expert Mode
            {
                if (dialoguePrep == true)
                {
                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheBeginningAndEnd").Type);
                }
                string category = "BossDialogue";
                string key = category + "." + "VanillaAndWarrior" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Everything Vanilla + WoL 18
            if (chosenDialogue == 69) // Boss dialogue - Dioskouroi
            {
                if (dialoguePrep == true)
                {
                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("MnemonicTrace4").Type);
                }
                string category = "BossDialogue";
                string key = category + "." + "Dioskouroi" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Dioskouroi
            if (chosenDialogue == 70) // Boss dialogue - Nalhaun
            {
                if (dialoguePrep == true)
                {
                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("MnemonicTrace2").Type);
                }
                string category = "BossDialogue";
                string key = category + "." + "Nalhaun" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Nalhaun 20
            if (chosenDialogue == 71) // Boss dialogue - Penthesilea
            {
                if (dialoguePrep == true)
                {
                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("MnemonicTrace3").Type);
                }
                string category = "BossDialogue";
                string key = category + "." + "Penthesilea" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
                
            }//Penthesilea 21
            if (chosenDialogue == 72) // Boss dialogue - Arbitration
            {
                string category = "BossDialogue";
                string key = category + "." + "Arbitration" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Arbitration 22
            if (chosenDialogue == 73) // Boss dialogue - Tsukiyomi 23
            {
                if (dialoguePrep == true)
                {
                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("SpatialMemoriam").Type);
                }
                string category = "BossDialogue";
                string key = category + "." + "Tsukiyomi" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Tsukiyomi 23
            if (chosenDialogue == 74) // Boss dialogue - Queen Slime
            {
                string category = "BossDialogue";
                string key = category + "." + "QueenSlime" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Queen Slime 24
            if (chosenDialogue == 75) // Boss dialogue - Empress of Light
            {
                string category = "BossDialogue";
                string key = category + "." + "EmpressOfLight" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Empress of Light 25
            if (chosenDialogue == 76) // Boss dialogue - Deerclops
            {
                string category = "BossDialogue";
                string key = category + "." + "Deerclops" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Deerclops 26
            if (chosenDialogue == 77) // Vagrant
            {
                if (dialoguePrep == true)
                {
                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("ShatteredDisk").Type);
                }

                string category = "BossDialogue";
                string key = category + "." + "Vagrant" + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Vagrant
            //Thespian
            if (chosenDialogue == 78)
            {
                string category = "BossDialogue";
                string key = category + "." + "Thespian" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }


            //Calamity boss dialogue: (Reminder: Calamity boss dialogue starts past 200) If Calamity is not detected, stop at 23. Otherwise...

            if (chosenDialogue == 201) // Boss dialogue - Desert Scourge
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "DesertScourge" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);

            }//Desert Scourge 24
            if (chosenDialogue == 202) // Boss dialogue - Crabulon
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "Crabulon" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Crabulon 25 
            if (chosenDialogue == 203) // Boss dialogue - Hive Mind
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "HiveMind" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Hive Mind 26
            if (chosenDialogue == 204) // Boss dialogue - Perforators
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "Perforators" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Perforators 27
            if (chosenDialogue == 205) // Boss dialogue - The Slime God
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "SlimeGod" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//The Slime God 28
             //Calamity boss dialogue (Hardmode)
            if (chosenDialogue == 206) // Boss dialogue - Cryogen
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "Cryogen" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Cryogen 29
            if (chosenDialogue == 207) // Boss dialogue - Aquatic Scourge
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "AquaticScourge" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Aquatic Scourge 30
            if (chosenDialogue == 208) // Boss dialogue - Brimstone Elemental
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "BrimstoneElemental" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Brimstone Elemental 31
            if (chosenDialogue == 209) // Boss dialogue - Calamitas
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "Calamitas" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Calamitas 32
            if (chosenDialogue == 210) // Boss dialogue - Leviathan
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "Leviathan" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Leviathan 33
            if (chosenDialogue == 211) // Boss dialogue - Astrum Aureus
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "AstrumAureus" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Astrum Aureus 34
            if (chosenDialogue == 212) // Boss dialogue - Plaguebringer Goliath
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "PlaguebringerGoliath" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Plaguebringer Goliath 35
            if (chosenDialogue == 213) // Boss dialogue - Ravager
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "Ravager" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Ravager 36
            if (chosenDialogue == 214) // Boss dialogue - Astrum Deus
            {
                string category = "CalamityBossDialogue";
                string key = category + "." + "AstrumDeus" + "." + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
            }//Astrum Deus 37
            #endregion
            #region weaponDialogue

            //Weapon conversations...........................................................................................................
            if (chosenDialogue == 101) // Death in Four Acts // Der Freischutz
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;

                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheFreeshooter").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.1", Player.name); //Here's another Essence you can use. It'll make a Ranged-type weapon with unique properties. Something tells me it's cursed, so..

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.2", Player.name); //Good luck with that one!

                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;

                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheSharpshooter").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.1", Player.name); //Here's a Ranged-type essence. It looks like every fourth shot has increased offensive properties. Whatever the reason, it's powerful.

                        //	" ";
                    }

                }


            }//Death In Four Acts / Der Freischutz 1
            if (chosenDialogue == 102) // Persephone / Kazimierz Seraphim
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheHallownest").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.3", Player.name); //Here is another Essence for you. This one's from a goddess of the Underworld, or so I'm told. It seems to be a melee weapon. 

                        //	" ";
                    }

                    if (dialogueLeft == 1)
                    {

                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.4", Player.name); //Keep up the good work!


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfThePegasus").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.2", Player.name); //Here you are: another Essence. This one will create a Summon-type weapon. I trust you know what that means? ...

                        //	" ";
                    }

                    if (dialogueLeft == 2)
                    {


                        expression = 4;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.3", Player.name); //I'm telling you to try it out.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {


                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.4", Player.name); //You knew that? Never mind then.


                        //	" ";
                    }
                }


            }//Persephone / Kazimierz Seraphim 2
            if (chosenDialogue == 103) // Skofnung / Inugami Ripsaw
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfBitterfrost").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.5", Player.name); //Here is another Essence for you. Apparently, this originates from 'Midgard.' It looks to be a rather powerful melee weapon. 

                        //	" ";
                    }

                    if (dialogueLeft == 1)
                    {

                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.6", Player.name); //That's it for now. Keep up the good work!


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfFingers").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 4;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.5", Player.name); //I have quite the unique Essence for you. It looks to be a chainsaw imbued with the strength of a dog god.. Or so it seems.

                        //	" ";
                    }

                    if (dialogueLeft == 1)
                    {


                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.6", Player.name); //Whatever it may be, it should prove useful to you. Ignore the fact that it talks.


                        //	" ";
                    }
                }


            }//Skofnung / Inugami Ripsaw 3
            if (chosenDialogue == 104) // Aegis Driver / Rad Gun
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheAegis").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.7", Player.name); //Another Essence for you. This firey sword is classified as Magic. It seems to explode when sufficiently charged.

                        //	" ";
                    }

                    if (dialogueLeft == 1)
                    {

                        expression = 4;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.8", Player.name); //I trust you'll know what to do.


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfStyle").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.7", Player.name); //I've gotten an Essence for you. This one will create a Magic-type weapon. Apparently, proper timing is needed to better utilize this weapon.

                        //	" ";
                    }

                    if (dialogueLeft == 1)
                    {


                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.8", Player.name); //I have no doubts you can get a handle on it easily. Okay, saying 'no doubts' would be a lie. Maybe 1/4th of a doubt.


                        //	" ";
                    }
                }


            }//Aegis Driver / Rad Gun 4 
            if (chosenDialogue == 105) // Karlan Truesilver / Every Moment Matters
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfSilverAsh").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.9", Player.name); //An Essence, for you. This powerful Melee heirloom is said to cleave the way for those less fortunate.

                        //	" ";
                    }

                    if (dialogueLeft == 1)
                    {

                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.10", Player.name); //Good luck in crafting it.


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheVoid").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.9", Player.name); //This Essence is used to craft an upgrade to Death in Four Acts. It will most likely boast incredible Ranged power.

                        //	" ";
                    }


                }


            }//Karlan Truesilver / Every Moment Matters 5
            if (chosenDialogue == 106) // Veneration of Butterflies / Memento Muse
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfButterflies").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.11", Player.name); //This Essence is a personal creation of mine! It creates a Magic weapon that lets you enter the 'Butterfly Trance' and gain special bonuses!

                        //	" ";
                    }

                    if (dialogueLeft == 1)
                    {

                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.12", Player.name); //Please, use it if you can. I can always use more data!


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfDeathsApprentice").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.10", Player.name); //The weapon made from this Essence is based around music.. Timing your swings will be critical in using this to its fullest.

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.11", Player.name); //Also.. Doesn't it make you feel like rapping?

                        //	" ";
                    }


                }


            }//Veneration of Butterflies / Memento Muse 6
            if (chosenDialogue == 107) // Ride The Bull / Drachenlance
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheBull").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.13", Player.name); //This Essence creates a Ranged gun that perpetuates itself after killing foes! Feel the heat!!

                        //	" ";
                    }

                    if (dialogueLeft == 1)
                    {

                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.14", Player.name); //Ahem. Please consider it in your arsenal.


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheDragonslayer").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.12", Player.name); //This Essence will create a legendary weapon made for slaying dragons. Jump into the air and pierce your foes with its strength.

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.13", Player.name); //It's a Melee weapon, so craft it if you can fit it in your arsenal.

                        //	" ";
                    }


                }


            }//Ride the Bull / Drachenlance 7
            if (chosenDialogue == 108) // Crimson Outbreak / Voice Of The Fallen
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;

                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheSwarm").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.15", Player.name); //This Essence is a Ranged weapon that defeats foes with a swarm of powerful nanites. Truly fearful.

                        //	" ";
                    }

                    if (dialogueLeft == 1)
                    {

                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.16", Player.name); //If your gear supports it, try crafting this weapon!


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheFallen").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.14", Player.name); //This Magic weapon will gain critical chance when shooting, and each crit will deal increased damage. It's powerful stuff.

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.15", Player.name); //Consider it if you're able.

                        //	" ";
                    }


                }


            }//Crimson Outbreak / Voice Of The Fallen 8
            if (chosenDialogue == 109) // Plenilune Gaze / Tartaglia
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheMoonlitAdepti").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.17", Player.name); //This Essence is another Ranged weapon that allows for powerful charged attacks. Fully charged, it splits and hits multiple foes!

                        //	" ";
                    }

                    if (dialogueLeft == 1)
                    {

                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.18", Player.name); //Hey. Aren't we getting too many Ranged weapons? I'll try and find something else next time.


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheHarbinger").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.16", Player.name); //This Ranged weapon allows you to enter a Melee stance, dealing more damage to foes you've hit with arrows. It should be quite engaging.

                        //	" ";
                    }



                }


            }//Plenilune Gaze / Tartaglia 9
            if (chosenDialogue == 110) // Rex Lapis / Yunlai Stiletto
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheUnyieldingEarth").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.19", Player.name); //This is a legendary Melee artifact! The weapon this Essence crafts can inflict a crippling debuff on any foe!

                        //	" ";
                    }




                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfDrivingThunder").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.17", Player.name); //This powerful Melee relic allows for the expidenture of Mana to teleport and deal powerful damage. It's a new favorite of mine.

                        //	" ";
                    }



                }


            }//Rex Lapis / Yunlai Stiletto 10
            if (chosenDialogue == 111) // Naganadel / Suistrume
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfStarsong").Type);

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Asphodene.20", Player.name); //This Essence will create a support item this time. It'll give powerful buffs to you and your friends if you can keep it up.

                        //	" ";
                    }




                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 1;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfLunarDominion").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.Eridani.18", Player.name); //This weapon is made straight from the power of the Moon Lord and its spatial pillars. It'll do good in your hands.


                        //	" ";
                    }



                }


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
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);



            }//Nalhaun item
            if (chosenDialogue == 302) //Penth item
            {

                if (dialoguePrep == true)                                     // |
                {
                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("UnsulliedCanvas").Type);
                }
                string category = "BossItemDialogue";
                string key = category + "." + "Penth" + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);

            }//Penth item
            if (chosenDialogue == 303) //Arbiter item
            {

                if (dialoguePrep == true)                                     // |
                {
                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("DemonicCrux").Type);
                }
                string category = "BossItemDialogue";
                string key = category + "." + "Arbitration" + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);



            }//Arbiter item
            if (chosenDialogue == 304) //Warrior item
            {
                if (dialoguePrep == true)                                     // |
                {
                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("ProgenitorWish").Type);
                }
                string category = "BossItemDialogue";
                string key = category + "." + "Warrior" + starfarerName + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);



            }//Warrior item
            if (chosenDialogue == 305) //Dioskouroi Item
            {

                if (dialoguePrep == true)                                     // |
                {
                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("TwincruxPendant").Type);
                }

                string category = "BossItemDialogue";
                string key = category + "." + "Dioskouroi" + ".";
                WriteDialogue(ref dialoguePrep, ref dialogueLeft, out expression, out dialogue, ref dialogueFinished, baseKey, key);
                

            }//Dioskouroi item

            dialogue = LangHelper.Wrap(dialogue, 44);
        }

        private static void WriteDialogue(ref bool dialoguePrep, ref int dialogueLeft, out int expression, out string dialogue, ref bool dialogueFinished, string baseKey, string key)
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
            dialogue = LangHelper.GetTextValue("Dialogue." + key + dialogueLeft);
            expression = SetupExpression(LangHelper.GetTextValue(key + dialogueLeft + ".E"));
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
    public class Dialogue
    {
        //Asphodene/Eridani
        //if the dialogue is by someone else, this can vary (think of it like the dialogue's owner)
        //Note that this appears AFTER title
        public string Name { get; set; } = "";
 
        //Categories can be longer than 1 segment, the actual "category" is defined when populating dialogue
        //(BossItemDialogue vs BossDialogue.KingSlime)
        public string Title { get; set; }
        // category should point to the hjson (Dialogue.[Category].[Name].[Length] i.e. Dialogue.[BossItemDialogue].[Dioskouroi].[4])
        // dialogue should keep track of its unlock condition and if it has been read or not too
        // 
        // Given length, it should automatically populate
        public int Length { get; set; } = 1;//Starts at 1 as default

        //Dialogue, Emotion
        public Dictionary<string, string> DialoguePages { get; set; } = new Dictionary<string, string>();
        public int AssociatedItem { get; set; }
        //Unlock condition (This appears in the Archive)
        //Important info: this gives an esssence, or boss summon item, etc. (This appears in the Archive) - if there isnt any important info it defaults to unlock condition
        public string UnlockCondition { get; set; }
        public string ArchiveInfo { get; set; } //maybe this can be automatically defined for weapons taking into account AssociatedItem if Aspho/Eri's archives are correctly seperated
        //Archive Category (Idle, Boss, Weapon)
        public string ArchiveCategory { get; set; }
        public List<int> ExtraIds { get; set; } = new List<int>();

        //Maybe a place where you can define which line the item spawns on?
        public Dialogue(string name, string title, int associatedItemType, string archiveCategory)
        {
            Name = name;
            Title = title;
            AssociatedItem = associatedItemType; // if associated item is 0, there isn't an item
            int tempLength = 1;
            int.TryParse(LangHelper.GetTextValue($"Dialogue." + title + "." + name + "Length"), out tempLength);

            for (int i = 0; i < tempLength; i++)
            {
                AddPage(LangHelper.GetTextValue($"Dialogue." + title + "." + name + "." + i, Main.LocalPlayer.name), LangHelper.GetTextValue($"Dialogue." + title + "." + name + "." + i + ".Emotion"));
            }

        }
        public override bool Equals(object obj)
        {
            if (obj is Dialogue other)
            {
                return Name == other.Name && Title == other.Title;
            }
            return false;
        }
        // Add page to the dialogue
        public void AddPage(string pageContent, string emotion)
        {
            DialoguePages.Add(pageContent, emotion);
        }

        // WIP
        public void AddExtraId(int id)
        {
            ExtraIds.Add(id);
        }
    }

    
    public class DialoguePlayer : ModPlayer
    {
        public int unreadDialogueCount = 0;
        //list of dialogues, following the key of the category

        //The Spatial Disk pulls from this list. If dialogue is ready to be read, it'll be added to the Active Dialogues list.
        //Once it has been read, it'll be moved to the Archive list


        private List<Dialogue> activeDialogues = new List<Dialogue>();
        private List<Dialogue> readDialogues = new List<Dialogue>();


        //Each dialogue has a category and it checks for that

        //Step 1: after reaching certain criteria, add the dialogue. Make sure to check that the dialogue doesn't already exist.
        //This is also where you can put the "new disk dialogue available!" pop-up (maybe add a number that shows the amount of dialogue in the active list, like (5 unread dialogues))
        public void AddActiveDialogue(Dialogue dialogue)
        {
            
            activeDialogues.Add(dialogue);
        }
        public void MoveReadDialogue(Dialogue dialogue)
        {
           
            activeDialogues.Remove(dialogue);
            
            readDialogues.Add(dialogue);
        }

        // Method to get dialogues by category
        public List<Dialogue> GetActiveDialogues()
        {
            return activeDialogues;

        }
        public List<Dialogue> GetReadDialogues()
        {
            return readDialogues;

        }
        public override void SetStaticDefaults()
        {


        }
        public override void PostUpdate()
        {
            string starfarerName = "";
            if (Player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
            {
                starfarerName = "Asphodene";
            }
            else if (Player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
            {
                starfarerName = "Eridani";
            }

            string category = "WeaponDialogue";
            bool newDialogueAdded = false;

            newDialogueAdded = PopulateDialogue(starfarerName, "TestDialogue", ModContent.ItemType<SpatialDisk>(), category);
            newDialogueAdded = PopulateDialogue(starfarerName, "TestDialogue2", ModContent.ItemType<SpatialDisk>(), category);

            //Main.NewText(newDialogueAdded);

            //At the end of dialogue population, if new dialogue was added show the pop up + the amount of unread dialogue
            if(newDialogueAdded)
            {
                unreadDialogueCount = GetActiveDialogues().Count;

                InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());

            }

            base.PostUpdate();
        }
        public bool PopulateDialogue(string starfarerName, string title, int associatedItemType, string category)
        {
            var dialogueInsert = new Dialogue(starfarerName, category + "." + title, ModContent.ItemType<SpatialDisk>(), category);
            if (GetActiveDialogues().Any(d => d.Equals(dialogueInsert)))
            {
                // Optionally, handle the duplicate case, e.g., by logging or modifying behavior
                return false; // Indicates that the dialogue was not added because it was a duplicate
            }
            AddActiveDialogue(dialogueInsert);
            return true;
        }

    }
}
