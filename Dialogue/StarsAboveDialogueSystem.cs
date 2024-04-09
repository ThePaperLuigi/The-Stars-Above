using StarsAbove.Systems;
using StarsAbove.Utilities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Dialogue
{
    public class StarsAboveDialogueSystem : ModSystem
    {
        //Old dialogue system.
        public static void SetupDialogueSystem(int chosenStarfarer, ref int chosenDialogue, ref bool dialoguePrep, ref int dialogueLeft, ref int expression, ref string dialogue, Player Player, Mod Mod)
        {

            if (chosenDialogue == 1) // Introduction dialogue. Unused.
            {
                if (chosenStarfarer == 1) // Asphodene
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 6;

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 6)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //Greetings! I am Asphodene- a Starfarer, and in turn, that makes you my Starbearer.



                    }
                    if (dialogueLeft == 5)
                    {
                        expression = 1;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //I will lend you my strength so that we may defeat the threats to this world together.
                        //	" ";
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //You can use the Spatial Disk I've given you to access the Stellar Array, which provides you with powerful abilities after defeating bosses. Nice, right?
                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        // dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //I've added the 'Bifrost' to your inventory. There's a lot to go over, but in short, it'll let you traverse the galaxy when used. We can talk more about it later.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //Also, I'll supply you with Essences when I can. Think of them as memories of other worlds. You can combine them with materials to create powerful Aspected Weapons.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 4;
                        // dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //Right. My power is yours. Let's show this world what we can do.
                        //	" ";
                    }
                }
                if (chosenStarfarer == 2)
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 6;

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 6)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //It's nice to meet you. My name is Eridani.. a Starfarer. You, in turn, are my Starbearer.
                    }
                    if (dialogueLeft == 5)
                    {
                        expression = 1;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //Through this contract, we must work together to bring justice to those willing to harm this world.
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //You can use the Spatial Disk I've given you to access the Stellar Array. It will become stronger after defeating bosses, and will grant you strength in return.
                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //I've added the Bifrost to your inventory. At a later date, I can explain what it does, but in short, it will allow traversal of the universe.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //Furthermore, I will grant you Essences. Essences are otherworldy memories that you can combine with this world's materials to forge Aspected Weapons.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 5;
                        //dialogue = LangHelper.GetTextValue($"Dialogue.WeaponDialogue.", Player.name); //My strength is yours... I will not let you down.
                    }
                }


            }//Introduction
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
            #region idleDialogue
            if (chosenDialogue == 3) // Passive Dialogue 1
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
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue1.Asphodene.1", Player.name); //My sister likely despises the un6 rising every night.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue1.Asphodene.2", Player.name); //What about me? 
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 4;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue1.Asphodene.3", Player.name); //As long as they have money, it's a benefit to us, right?
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
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue1.Eridani.1", Player.name); //Those disgusting creatures rising from the earth every night...
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.RegularIdleDialogue.NormalIdleDialogue1.Eridani.2", Player.name); //Ew. Let's dispatch of them quickly. And- be mindful of your hygiene after.
                    }
                }


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

            //wow this code sucks! Too lazy to refactor atm.
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
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.KingSlime.Asphodene.1", Player.name); //You've defeated the Slime King, huh? Good job.
                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.KingSlime.Asphodene.2", Player.name); //It may have been rather weak, but it's a surefire step forwards nevertheless.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.KingSlime.Asphodene.3", Player.name); //Did anyone else see that ninja thing? Was it just me?
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 5;
                        if (Main.expertMode == true)
                        {
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.KingSlime.Asphodene.4", Player.name); //Somehow.. it was strong enough to unlock a part of the Stellar Array! Check it out later, OK?
                        }
                        else
                        {
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.KingSlime.Asphodene.5", Player.name); //...Well, that's all for now. Keep it up.
                        }
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
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.KingSlime.Eridani.1", Player.name); //It may have seemed weak, but the Slime King was truly quite the threat.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.KingSlime.Eridani.2", Player.name); //Let's hope it stays dead. 
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 5;
                        if (Main.expertMode == true)
                        {
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.KingSlime.Eridani.3", Player.name); //Something about this foe has unlocked a part of the Stellar Array. Perhaps it was fiercer than usual?
                        }
                        else
                        {
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.KingSlime.Eridani.4", Player.name); //However, if it does return, you should be able to easily defeat it anew.
                        }
                        //	" ";
                    }
                }


            }//Slime King 1
            if (chosenDialogue == 52) // Boss dialogue - Eye of Cthulu
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 5;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 5)
                    {
                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.CthulhuEye.Asphodene.1", Player.name); //'Eye of Cthulhu'? 
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.CthulhuEye.Asphodene.2", Player.name); //Doesn't that imply there would be more of this 'Cthulhu'?
                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.CthulhuEye.Asphodene.3", Player.name); //If it ever reared its head, it would be quite the formidable foe indeed.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.CthulhuEye.Asphodene.4", Player.name); //Let's prepare for that occasion... Just in case.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.CthulhuEye.Asphodene.5", Player.name); //Hey.. defeating that foe was enough! Looks like an ability in the Stellar Array is ready.
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
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.CthulhuEye.Eridani.1", Player.name); //For how fearsome it looked, the Eye of Cthulhu wasn't too terrible, was it?
                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.CthulhuEye.Eridani.2", Player.name); //It seems to have shed its skin to display quite an array of teeth. If I didn't know better, it looked like it was trying to prove something. Weirdo.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.CthulhuEye.Eridani.3", Player.name); //I have a suspicion that it's materials can be utilized later on. Simply keep it in mind.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.CthulhuEye.Eridani.4", Player.name); //In other news.. an ability in the Stellar Array is now available. Use it well.
                        //	" ";CthulhuEye
                    }
                }


            }//Eye of Cthulhu 2
            if (chosenDialogue == 53) // Boss dialogue - Eater of Worlds or Brain of Cthulhu
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        if (WorldGen.crimson)
                        {
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.CorruptionBoss.Asphodene.1", Player.name); //Huh. I guess there really was more 'Cthulhu.' Somehow, it doesn't suprise me as much as I would have thought.
                        }
                        else
                        {
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.CorruptionBoss.Asphodene.2", Player.name); //Glad we stopped that thing before it lived up to its name. 
                        }
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {

                        if (WorldGen.crimson)
                        {
                            expression = 1;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.CorruptionBoss.Asphodene.3", Player.name); //Those materials are... something... Wow. How bloody.
                        }
                        else
                        {
                            expression = 4;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.CorruptionBoss.Asphodene.4", Player.name); //Hey, the weapons that you can make from it look pretty powerful! What do you think?
                        }

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {


                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.CorruptionBoss.Asphodene.5", Player.name); //In other news... Looks like an ability has unlocked in the Stellar Array! You should definitely consider it.


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
                        if (WorldGen.crimson)
                        {
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.CorruptionBoss.Eridani.1", Player.name); //Another organ of this 'Cthulhu' and it attacked with magical manipulation... Also- yuck?
                        }
                        else
                        {
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.CorruptionBoss.Eridani.2", Player.name); //This 'Eater of Worlds' was just a big worm. I'm kind of disappointed. Let's file a complaint with whichever clown named this thing.
                        }
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {


                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.CorruptionBoss.Eridani.3", Player.name); //Looks like it unlocked an ability in the Stellar Array. Look it over when you're free.


                        //	" ";
                    }
                }


            }//Brain of Cthulhu / Eater of Worlds 3
            if (chosenDialogue == 54) // Boss dialogue - Queen Bee
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.QueenBee.Asphodene.1", Player.name); //Well, I guess I should have expected something like this within the Jungle.

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {

                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.QueenBee.Asphodene.2", Player.name); //Good work. Looks like another ability is unlocked in the Stellar Array as well.


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

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.QueenBee.Eridani.1", Player.name); //Seriously? A giant bee... Somehow, I might prefer it over smaller insects... Easier to hit, you know.

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {


                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.QueenBee.Eridani.2", Player.name); //Ahem. At the very least, it unlocked an ability in the Stellar Array.


                        //	" ";
                    }
                }


            }//Queen Bee 4 
            if (chosenDialogue == 55) // Boss dialogue - Skeletron
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Skeletron.Asphodene.1", Player.name); //The Clothier was truly a gigantic evil skeleton monster.. Cute. At the very least, you have access to the Dungeon now.

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {

                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Skeletron.Asphodene.2", Player.name); //And with this, another ability is unlocked in the Stellar Array.


                        //	" ";
                    }

                    if (dialogueLeft == 1)
                    {

                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Skeletron.Asphodene.3", Player.name); //Well, that's all for now.


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
                        expression = 2;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Skeletron.Eridani.1", Player.name); //That poor Clothier, cursed to become Skeletron. It seems his defeat has granted us access to the Dungeon. This may be in bad taste, but...

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {


                        expression = 4;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Skeletron.Eridani.2", Player.name); //If the embodiment of your emotions is a huge monster... maybe, just maybe, the curse wasn't the sole factor.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {


                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Skeletron.Eridani.3", Player.name); //If anyone asks, you didn't hear that from me.


                        //	" ";
                    }
                }


            }//Skeletron 5 
            if (chosenDialogue == 56) // Boss dialogue - Wall of Flesh
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 4;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WallOfFlesh.Asphodene.1", Player.name); //Whoa.. I've never seen anything like this.. ever. The entire world seems to be bursting with new magical energy. 

                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {

                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WallOfFlesh.Asphodene.2", Player.name); //It looks like the Wall of Flesh was either guarding or preventing something drastic from happening. That's my take on it.


                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {

                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WallOfFlesh.Asphodene.3", Player.name); //Just to let you know, there's another upgrade in the Stellar Array now. It seems to be more powerful than the ones you've used before.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {

                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WallOfFlesh.Asphodene.4", Player.name); //Also, it seems to have dropped something.. Whatever it is, it feels.. familiar. Did I forget about something? No, that isn't it. Please be careful.


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
                        expression = 3;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WallOfFlesh.Eridani.1", Player.name); //Wow.. the world itself seems to have been drastically altered after the defeat of the Wall of Flesh...

                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {


                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WallOfFlesh.Eridani.2", Player.name); //I've never seen anything like it. What was this thing hiding...?


                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {


                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WallOfFlesh.Eridani.3", Player.name); //This power seems to have activated the first Tier 2 ability in the Stellar Array. I hope you can utilize it well.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {


                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WallOfFlesh.Eridani.4", Player.name); //Something else dropped from it. I'm not sure what it is, but it feels like I should know. Please be mindful of the danger...


                        //	" ";
                    }
                }


            }//Wall of Flesh 6
            if (chosenDialogue == 58) // Boss dialogue - The Twins
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Twins.Asphodene.1", Player.name); //So someone went and made two Eyes instead of one? Good for them, but in my opinion, the design is kind of ruined already.

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {

                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Twins.Asphodene.2", Player.name); //If you're going to make a robot eye, try and hide the obvious weak spot where the iris is. Well, I don't create giant evil robotic eyes, so maybe they know something..?


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {

                        if (Player.GetModPlayer<StarsAbovePlayer>().bloomingflames == 0)
                        {
                            expression = 0;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Twins.Asphodene.3", Player.name); //It looks as if another upgrade is available in the Stellar Array. That's it for now.
                        }
                        else
                        {
                            expression = 0;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Twins.Asphodene.4", Player.name);
                        }
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
                        expression = 3;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Twins.Eridani.1", Player.name); //Well, those mechanical facsimiles are 6. We're not rating boss monsters, or anything, but... I'm going to have to dock some points.

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {


                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Twins.Eridani.2", Player.name); //I'm giving it a 5/10. The sheet metal plating was a good start, but why would you ever even think about using flesh for the exterior? Whatever.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {


                        if (Player.GetModPlayer<StarsAbovePlayer>().bloomingflames == 0)
                        {
                            expression = 0;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Twins.Eridani.3", Player.name); //It looks as if yet another upgrade is available in the Stellar Array. How serendipitous.
                        }
                        else
                        {
                            expression = 0;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Twins.Eridani.4", Player.name);
                        }

                        //	" ";
                    }
                }
            }//The Twins 7
            if (chosenDialogue == 57) // Boss dialogue - The Destroyer
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Destroyer.Asphodene.1", Player.name); //'The Destroyer...' I'm not one to talk, but if you name something 'The Destroyer' I'm gonna expect more than a big worm.

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {

                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Destroyer.Asphodene.2", Player.name); //Its absurd length was just a detriment to its strength. I hope whoever makes these things is taking note.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {

                        if (Player.GetModPlayer<StarsAbovePlayer>().bloomingflames == 0)
                        {
                            expression = 0;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Destroyer.Asphodene.3", Player.name); //It looks as if another upgrade is available in the Stellar Array. That's all, {Player.name}.
                        }
                        else
                        {
                            expression = 0;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Destroyer.Asphodene.4", Player.name);
                        }
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

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Destroyer.Eridani.1", Player.name); //I guess the Destroyer was more bark than bite, huh. Maybe too much of the budget was spent on the design aspect.

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {


                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Destroyer.Eridani.2", Player.name); //Whatever the case, it's gone now, so no use comtemplating it.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {


                        if (Player.GetModPlayer<StarsAbovePlayer>().bloomingflames == 0)
                        {
                            expression = 0;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Destroyer.Eridani.3", Player.name); //It looks as if yet another upgrade is available in the Stellar Array. How serendipitous.
                        }
                        else
                        {
                            expression = 0;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Destroyer.Eridani.4", Player.name); //Well, that's everything. Unless you wanted to chat?
                        }

                        //	" ";
                    }
                }
            }//Destroyer 8
            if (chosenDialogue == 59) // Boss dialogue - Skeletron Prime
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.SkeletronPrime.Asphodene.1", Player.name); //You've done it! Skeletron Prime has been defeated. I can definitely say it was a step up from 'Version 1.0.'

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {

                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.SkeletronPrime.Asphodene.2", Player.name); //The appendages were a smart idea, but it still lacked a proper body. A shame.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {

                        if (Player.GetModPlayer<StarsAbovePlayer>().bloomingflames == 0)
                        {
                            expression = 0;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.SkeletronPrime.Asphodene.3", Player.name); //It looks as if another upgrade is available in the Stellar Array. That's all, {Player.name}.
                        }
                        else
                        {
                            expression = 0;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.SkeletronPrime.Asphodene.4", Player.name);
                        }
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
                        expression = 5;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.SkeletronPrime.Eridani.1", Player.name); //And that takes care of Skeletron Prime. It certainly looked the part... if you're a total edgelord.

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {


                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.SkeletronPrime.Eridani.2", Player.name); //If you took offense, I apologize. You can be as edgy as you want. Uh, I mean.. anyways... 


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {


                        if (Player.GetModPlayer<StarsAbovePlayer>().bloomingflames == 0)
                        {
                            expression = 0;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.SkeletronPrime.Eridani.3", Player.name); //It looks as if yet another upgrade is available in the Stellar Array. How serendipitous.
                        }
                        else
                        {
                            expression = 0;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.SkeletronPrime.Eridani.4", Player.name);
                        }

                        //	" ";
                    }
                }
            }//Skeletron Prime 9
            if (chosenDialogue == 60) // Boss dialogue - All Mechanical Bosses Defeated
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.AllMechs.Asphodene.1", Player.name); //Actually.. It looks like you've bested all the Mechanical Bosses! This is definitely a step forward.

                        //	" ";
                    }

                    if (dialogueLeft == 1)
                    {


                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.AllMechs.Asphodene.2", Player.name); //This victory has lead to another upgrade in- you guessed it, the Stellar Array. 

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
                        expression = 5;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.AllMechs.Eridani.1", Player.name); //It seems that was the last of the Mechanical Bosses. This is a great step forward! 

                        //	" ";
                    }

                    if (dialogueLeft == 1)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.AllMechs.Eridani.2", Player.name); //This victory has brought another upgrade to the Stellar Array. 


                        //	" ";
                    }

                }
            }//All Mechanical Bosses defeated 10
            if (chosenDialogue == 61) // Boss dialogue - Plantera
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Plantera.Asphodene.1", Player.name); //And there it goes.. the terror of the jungle, Plantera. Congratulations on your victory. 

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {


                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Plantera.Asphodene.2", Player.name); //I sense the Dungeon has changed... and the Jungle Temple is now available. It's your choice what to tackle next.

                    }
                    if (dialogueLeft == 1)
                    {


                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Plantera.Asphodene.3", Player.name); //Another upgrade is now available in the Stellar Array.  

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
                        expression = 5;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Plantera.Eridani.1", Player.name); //That marks the end of Plantera. Very good work. You should be able to access the Jungle Temple now.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Plantera.Eridani.2", Player.name); //I also believe there's new materials to be found within the Dungeon. Great for us, not so much for them. Tough luck.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Plantera.Eridani.3", Player.name); //Another upgrade has been unlocked in the Stellar Array.

                        //	" ";
                    }
                }
            }//Plantera 11
            if (chosenDialogue == 62) // Boss dialogue - Golem
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Golem.Asphodene.1", Player.name); //You've done it! Golem is no more. You're unstoppable.  

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {


                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Golem.Asphodene.2", Player.name); //Something about our next enemy feels.. dangerous. It seems spatial energy has congregated around the Dungeon's entrance.

                    }
                    if (dialogueLeft == 1)
                    {

                        if (Main.expertMode)
                        {
                            expression = 5;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Golem.Asphodene.3", Player.name); //Another upgrade is now available in the Stellar Array. Looks pretty strong.
                        }
                        else
                        {
                            expression = 0;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Golem.Asphodene.4", Player.name); //
                        }


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
                        expression = 5;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Golem.Eridani.1", Player.name); //The Lizhard Temple's beast Golem is confirmed 6.  In terms of mechanical beasts, I'm kind of.. disappointed? 

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Golem.Eridani.2", Player.name); //Something strange has appeared in front of the Dungeon. It seems otherwordly in nature... Be prepared for anything.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {


                        if (Main.expertMode)
                        {
                            expression = 5;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Golem.Eridani.3", Player.name); //Another upgrade is now available in the Stellar Array. It will surely be useful.
                        }
                        else
                        {
                            expression = 0;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Golem.Eridani.4", Player.name);
                        }

                        //	" ";
                    }
                }
            }//Golem 12
            if (chosenDialogue == 63) // Boss dialogue - Duke Fishron
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.DukeFishron.Asphodene.1", Player.name); //Such a powerful creature hiding deep within the Ocean.. Luckily, you avoided being turned into fish food. 

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {


                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.DukeFishron.Asphodene.2", Player.name); //Where did that saying even come from..? What IS fish food? More important questions left unanswered...



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
                        expression = 5;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.DukeFishron.Eridani.1", Player.name); //Duke Fishron has been defeated. That was a close one, but you've o-fish-ally won. ...

                        //	" ";
                    }

                    if (dialogueLeft == 1)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.DukeFishron.Eridani.2", Player.name); //I might have fished too hard for that one. You know what..? Let's leave the puns to Asphodene. I guess there's a certain skill to it.


                        //	" ";
                    }
                }
            }//Duke Fishron 13
            if (chosenDialogue == 64) // Boss dialogue - Lunatic Cultist
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Cultist.Asphodene.1", Player.name); //It is as I feared... The Celestial Pillars have appeared upon this land. They herald the coming of the end.. The Moon Lord.

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {


                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Cultist.Asphodene.2", Player.name); //Don't lose hope, though. We can beat this. The experiences you had.. The battles you've fought.. they will culminate in this ultimate encounter.

                    }
                    if (dialogueLeft == 1)
                    {

                        if (Main.expertMode)
                        {
                            expression = 5;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Cultist.Asphodene.3", Player.name); //At the very least, you have another ability ready in the Stellar Array. It will definitely help.
                        }
                        else
                        {
                            expression = 0;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Cultist.Asphodene.4", Player.name);

                        }


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
                        expression = 2;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Cultist.Eridani.1", Player.name); //I knew it.. the Celestial Pillars have descended. This does not bode well for this world.

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Cultist.Eridani.2", Player.name); //All hope is not lost, however. If there is anyone who could confront the Moon Lord, it is you. I will be by your side.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {


                        if (Main.expertMode)
                        {
                            expression = 0;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Cultist.Eridani.3", Player.name); //At the very least, the Stellar Array has a new ability available. Let us hope it will come in handy. 
                        }
                        else
                        {
                            expression = 1;
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Cultist.Eridani.4", Player.name);
                        }

                        //	" ";
                    }
                }
            }//Lunatic Cultist 14
            if (chosenDialogue == 65) // Boss dialogue - Moon Lord
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 4;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.MoonLord.Asphodene.1", Player.name); //{Player.name}! You've done it! The Moon Lord.. has been vanquished! You have done this world a great service. 

                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {


                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.MoonLord.Asphodene.2", Player.name); //We've come a long way. You have grown stronger than I have ever imagined. However, I can sense it isn't over yet.

                    }
                    if (dialogueLeft == 2)
                    {

                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.MoonLord.Asphodene.3", Player.name); //The defeat of the Moon Lord has upgraded your Stellar Array twice! Such power...



                    }
                    if (dialogueLeft == 1)
                    {


                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.MoonLord.Asphodene.4", Player.name); //The sky's light has grown incredibly harsh. Something's definitely wrong... 

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
                        expression = 5;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.MoonLord.Eridani.1", Player.name); //The Moon Lord has been defeated.. What a wonderful victory. 

                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {



                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.MoonLord.Eridani.2", Player.name); //You have grown incredibly strong.. This world owes you for its survival. And yet.. I feel something is off.


                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {

                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.MoonLord.Eridani.3", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {


                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.MoonLord.Eridani.4", Player.name); //The light has grown incredibly harsh. Something's definitely wrong...

                    }
                }
            }//Moon Lord 15 
            if (chosenDialogue == 66) // Boss dialogue - Warrior of Light
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("MnemonicTrace").Type);
                        dialogueLeft = 6;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 6)
                    {
                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WarriorOfLight.Asphodene.1", Player.name); //I see we were wrong.. The Warrior thought he was saving the universe, but it was truly the lies of the First Starfarer.. How cruel.

                        //	" ";
                    }
                    if (dialogueLeft == 5)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WarriorOfLight.Asphodene.2", Player.name); //The Warrior of Light.. The original Starbearer has been led astray. He managed to get away, but... Night has returned to this world, finally!

                        //	" ";
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WarriorOfLight.Asphodene.3", Player.name); //We finally have the last piece of the puzzle. The First Starfarer showed her hand, and this means we have a chance.

                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {



                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WarriorOfLight.Asphodene.4", Player.name); //Once you've collected all the Mnemonic Traces, use it within the Observatory. It will take us straight to the First Starfarer.



                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WarriorOfLight.Asphodene.5", Player.name); //I probably don't need to tell you this, but she's not going to parley. Prepare for a hard fight.. Perhaps the hardest fight so far.



                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WarriorOfLight.Asphodene.6", Player.name); //The Warrior of Light has strengthened your Stellar Array. Hopefully it'll be enough... 



                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("MnemonicTrace").Type);
                        dialogueLeft = 6;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 6)
                    {
                        expression = 5;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WarriorOfLight.Eridani.1", Player.name); //You have brought down the Warrior of Light. He's managed to escape, but nighttime has returned to this world regardless.

                        //	" ";
                    }
                    if (dialogueLeft == 5)
                    {



                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WarriorOfLight.Eridani.2", Player.name); //It seems we were misguided. The Warrior believed he was saving the universe, but it was, in truth, all lies from his Starfarer.


                        //	" ";
                    }
                    if (dialogueLeft == 4)
                    {



                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WarriorOfLight.Eridani.3", Player.name); //With this final Mnemonic Trace.. I see.. It all makes sense now. {Player.name}, I need to you combine all of the Mnemonic Traces together.


                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {



                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WarriorOfLight.Eridani.4", Player.name); //It will lead us directly to the First Starfarer through the Observatory. After that.. We must defeat her. At any cost. Prepare yourself well.


                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WarriorOfLight.Eridani.5", Player.name); //Anyways... The radiance of the Warrior has upgraded the Stellar Array. That's definitely a boon.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.WarriorOfLight.Eridani.6", Player.name); //Remember: the First Starfarer. We can not let this opportunity slip away. This will likely be the fight of our lives.. Prepare yourself well.



                    }
                }
            }//Warrior of Light 16
            if (chosenDialogue == 67) // Boss dialogue - All Vanilla Bosses Defeated
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheCosmos").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.AllVanillaBosses.Asphodene.1", Player.name); //{Player.name}.. You've defeated so many powerful foes... You must be nearing the pinnacle of your powers!

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.AllVanillaBosses.Asphodene.2", Player.name); //I've granted you a special ability in the Stellar Array. Use it wisely.$4



                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.AllVanillaBosses.Asphodene.3", Player.name); //And, a special gift. This is the crystallized power of the Starfarers. The weapon born from it will be incredibly powerful!



                    }

                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 4;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheCosmos").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 5;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.AllVanillaBosses.Eridani.1", Player.name); //The enemies of this world shudder when they feel your presence.. You are the hero of this land.

                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {



                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.AllVanillaBosses.Eridani.2", Player.name); //I'm proud. You have become  stronger than I would have ever imagined..


                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.AllVanillaBosses.Eridani.3", Player.name); //This is a gift from me to you. It's a special ability in the Stellar Array. Use it well.$4


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.AllVanillaBosses.Eridani.4", Player.name); //One more thing. The crystallized power of the Starfarers.. It should be able to make an incredibly strong weapon.


                        //	" ";
                    }
                }
            }//All Vanilla Bosses 17
            if (chosenDialogue == 68) // Boss dialogue - Everything Defeated in Expert Mode
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 4;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheBeginningAndEnd").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.VanillaAndWarrior.Asphodene.1", Player.name); //You've done it. This world is free.. All that threatens it is no more. Seriously, this is a incredible achievement.

                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {



                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.VanillaAndWarrior.Asphodene.2", Player.name); //Don't worry. I'm not going anywhere yet. Who knows what might crop up? $4



                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.VanillaAndWarrior.Asphodene.3", Player.name); //I have an Essence for you... It is the strongest Essence I have. Be sure to treat the finished product with care.



                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.VanillaAndWarrior.Asphodene.4", Player.name); //Your overwhelming power has activated the final ability within the Stellar Array. It's done.. finally!



                    }

                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 4;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("EssenceOfTheBeginningAndEnd").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 5;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.VanillaAndWarrior.Eridani.1", Player.name); //You've done it, {Player.name}. Seriously.. I can't believe it. Everything threatening this world has been bested by your hand.

                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {



                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.VanillaAndWarrior.Eridani.2", Player.name); //I'll stay for now, though. Just in case anything else appears, though. It's not like I'd miss you or anything.


                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.VanillaAndWarrior.Eridani.3", Player.name); //This is a first.. Your own strength has activated the final. ability in the Stellar Array. Finally. Nothing else to unlock.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.VanillaAndWarrior.Eridani.4", Player.name); //Lastly... This Essence that I have given you.. It is the strongest weapon I know. Good luck creating it, {Player.name}.


                        //	" ";
                    }
                }
            }//Everything Vanilla + WoL 18
            if (chosenDialogue == 69) // Boss dialogue - Dioskouroi
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 4;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("MnemonicTrace4").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Dioskouroi.Asphodene.1", Player.name); //So that was the Burnished King. His blade was incredibly powerful, not to mention the magic at his disposal. It was a triumph you survived that.

                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Dioskouroi.Asphodene.2", Player.name); //From what Perseus told me, it looks like the Burnished King was defeated by the First Starbearer. 



                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Dioskouroi.Asphodene.3", Player.name); //As for why he told US to fight, I don't know. However.. I found this shard of power after the battle. I'll let you hold on to it. Anyways..



                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Dioskouroi.Asphodene.4", Player.name); //I think we can reuse his attacks as a weapon in our arsenal, as a new Stellar Nova. Maybe because of the Starbearer's influence?



                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 4;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("MnemonicTrace4").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Dioskouroi.Eridani.1", Player.name); //The Burnished King has fallen. What a powerful foe. I shudder to think how we would have fared had we fought him in his prime.

                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Dioskouroi.Eridani.2", Player.name); //From what Perseus told me, The Burnished King was laid low by the First Starbearer ages ago. 



                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Dioskouroi.Eridani.3", Player.name); //As for why he told us to fight him, I don't know. However.. I found this shard after the battle. I'll let you hold on to it. Anyways..



                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Dioskouroi.Eridani.4", Player.name); //His weapon, Ars Laevateinn. We can use it for ourselves, as a Stellar Nova. Maybe because of the Starbearer's influence?


                        //	" ";
                    }

                }
            }//Dioskouroi
            if (chosenDialogue == 70) // Boss dialogue - Nalhaun
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 4;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("MnemonicTrace2").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Nalhaun.Asphodene.1", Player.name); //So that was the Burnished King. His blade was incredibly powerful, not to mention the magic at his disposal. It was a triumph you survived that.

                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Nalhaun.Asphodene.2", Player.name); //From what Perseus told me, it looks like the Burnished King was defeated by the First Starbearer. 



                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Nalhaun.Asphodene.3", Player.name); //As for why he told US to fight, I don't know. However.. I found this shard of power after the battle. I'll let you hold on to it. Anyways..



                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Nalhaun.Asphodene.4", Player.name); //I think we can reuse his attacks as a weapon in our arsenal, as a new Stellar Nova. Maybe because of the Starbearer's influence?



                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 4;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("MnemonicTrace2").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Nalhaun.Eridani.1", Player.name); //The Burnished King has fallen. What a powerful foe. I shudder to think how we would have fared had we fought him in his prime.

                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Nalhaun.Eridani.2", Player.name); //From what Perseus told me, The Burnished King was laid low by the First Starbearer ages ago. 



                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Nalhaun.Eridani.3", Player.name); //As for why he told us to fight him, I don't know. However.. I found this shard after the battle. I'll let you hold on to it. Anyways..



                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Nalhaun.Eridani.4", Player.name); //His weapon, Ars Laevateinn. We can use it for ourselves, as a Stellar Nova. Maybe because of the Starbearer's influence?


                        //	" ";
                    }

                }
            }//Nalhaun 20
            if (chosenDialogue == 71) // Boss dialogue - Penthesilea
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("MnemonicTrace3").Type);
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Penth.Asphodene.1", Player.name); //Penthesilea, was it? She was quite the challenge. What a unique way of casting magic. 

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Penth.Asphodene.2", Player.name); //Her magic overflows with spatial power. It looks like the First Starbearer's  influence is here, meaning we can use a new Stellar Nova.



                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Penth.Asphodene.3", Player.name); //The battle left behind this mysterious object. You can hold on to it for now.  



                    }

                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("MnemonicTrace3").Type);
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Penth.Eridani.1", Player.name); //You've bested the Witch of Ink. She was quite the magic user, no? It's truly a shame she was hostile. 

                        //	" ";
                    }

                    if (dialogueLeft == 2)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Penth.Eridani.2", Player.name); //Her powerful magic... We can use it for ourselves as a new Nova. It must be from the remnants of the First Starbearer Perseus mentioned...


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Penth.Eridani.3", Player.name); //The battle left behind this mysterious shard. I'll entrust it to you.  



                    }
                }
            }//Penthesilea 21
            if (chosenDialogue == 72) // Boss dialogue - Arbitration
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        //Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("MnemonicTrace4").Type);
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Arbitration.Asphodene.1", Player.name); //That foe was truly fearful. I'm glad you were able to defeat it... Who knows what chaos it would bring if left unchecked?

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Arbitration.Asphodene.2", Player.name); //We can re-appropriate its strength, I think. It can become a new Stellar Nova, thanks to the First Starbearer's influence. Let's use it well.



                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Arbitration.Asphodene.3", Player.name); //It looks like another item has appeared. You can have it- it's probably better off in your hands, anyways. 



                    }

                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        //Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("MnemonicTrace4").Type);
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Arbitration.Eridani.1", Player.name); //You have defeated that monstrocity! The world is better because of your deeds. If we failed, the world itself may have been consumed by the void.

                        //	" ";
                    }

                    if (dialogueLeft == 2)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Arbitration.Eridani.2", Player.name); //We can use his overwhelming strength... It has manifested as a new Stellar Nova, thanks to the First Starbearer's influence. 


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Arbitration.Eridani.3", Player.name); //It looks like another shard has appeared. I'll leave it to you.  



                    }
                }
            }//Arbitration 22
            if (chosenDialogue == 73) // Boss dialogue - Tsukiyomi 23
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("SpatialMemoriam").Type);
                        dialogueLeft = 4;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Tsukiyomi.Asphodene.1", Player.name); //It.. it's over. Tsukiyomi has vanished. We're still missing so many clues... She doesn't seem evil... so what caused her to do what she did?

                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Tsukiyomi.Asphodene.2", Player.name); //Hopefully one day, we'll find the answers to the questions we seek.   



                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Tsukiyomi.Asphodene.3", Player.name); //I've gathered some of the leftover residue from the fight. It will likely have use as a stand-in to an Essence, for creating a  weapon.



                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Tsukiyomi.Asphodene.4", Player.name); //With this victory.. the universe is safe. There may be more evils ahead, but know that nothing is insurmountable. We've come this far.



                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("SpatialMemoriam").Type);
                        dialogueLeft = 4;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Tsukiyomi.Eridani.1", Player.name); //We've finally done it. Tsukiyomi's defeat has been confirmed. She's gone, but hopefully our troubles with her are at an end.

                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Tsukiyomi.Eridani.2", Player.name); //What ails me is her motives... She did horrible things... but was it malice, or a greater purpose...? I hope we find the answer.



                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Tsukiyomi.Eridani.3", Player.name); //I've gone through the work of absorbing some residue energy and have turned it into the Spatial Memoriam. You can use it like an Essence.



                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Tsukiyomi.Eridani.4", Player.name); //The universe is safe. There is still work to do, but take heart; if we've come this far, nothing can stop us now.



                    }

                }
            }//Tsukiyomi 23
            if (chosenDialogue == 74) // Boss dialogue - Queen Slime
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.QueenSlime.Asphodene.1", Player.name); //That's that. Queen Slime is defeated. What have we learned today? Apparently there's a royal court of slimes, and the King Slime wasn't just an outlier. 

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {


                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.QueenSlime.Asphodene.2", Player.name); //That doesn't even make any sense. They're slime. I literally, actually, seriously don't even think slime has the capacity to think. 

                    }
                    if (dialogueLeft == 1)
                    {


                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.QueenSlime.Asphodene.3", Player.name); //Who knows.   

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

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.QueenSlime.Eridani.1", Player.name); //Right, we've defeated the Queen Slime. To be honest, I'm slightly curious: why are there 'kings' and 'queens' of the slime world? 

                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {



                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.QueenSlime.Eridani.2", Player.name); //How much power do the royal slimes even have? Are they legitimate rulers? 


                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.QueenSlime.Eridani.3", Player.name); //Does the royal slime court send their subjects to attack random people? Do they even have the right to own property?


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.QueenSlime.Eridani.4", Player.name); //Fight back, oppressed slimes! Take back your freedom!


                        //	" ";
                    }
                }
            }//Queen Slime 24
            if (chosenDialogue == 75) // Boss dialogue - Empress of Light
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.EmpressOfLight.Asphodene.1", Player.name); //Great work- the Empress of Light is no more. Nobody else is going to say it, so I will-  did it have a big nose, or was it smiling? I couldn't tell.  

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {


                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.EmpressOfLight.Asphodene.2", Player.name); //Maybe if she stood still at all... 

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

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.EmpressOfLight.Eridani.1", Player.name); //Congratulations, you've bested the Empress of Light. Let's leave the bugs alone now, okay?   

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 2;
                        if (NPC.downedFishron)
                        {
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.EmpressOfLight.Eridani.2", Player.name); //If I had a nickel for bosses summoned with insects... We fought that rather large fish, right? That's two. Not very much, is it. I was hoping for a trend. 
                            if (NPC.downedQueenBee)
                            {
                                dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.EmpressOfLight.Eridani.3", Player.name); //If I had a nickel for bosses summoned with insects... We fought that rather large fish, right? That's two. Not very much, is it. I was hoping for a trend. Maybe we can count the Queen Bee?
                            }

                        }
                        else
                        {
                            dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.EmpressOfLight.Eridani.4", Player.name); //Somehow... I feel like there's yet another boss we'll have to use an insect to summon. That's going to be rather bothersome. 
                        }



                        //	" ";
                    }

                }
            }//Empress of Light 25
            if (chosenDialogue == 76) // Boss dialogue - Deerclops
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Deerclops.Asphodene.1", Player.name); //Okay, we've taken care of that strange abomination.  I have no idea where it even came from.   

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {


                        expression = 4;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Deerclops.Asphodene.2", Player.name); //I'm getting rather hungry- how about we go find something to eat so that we don't starve...? Pfft. 

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

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Deerclops.Eridani.1", Player.name); //Great work- you've defeated that strange deer thing.   

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {




                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Deerclops.Eridani.2", Player.name); //It was a little terrifying, but luckily it seems its previous prey was confined to the ground. Looks like we had home turf advantage here. 




                        //	" ";
                    }

                }
            }//Deerclops 26
            if (chosenDialogue == 77) // Vagrant
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("ShatteredDisk").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.VagrantAsphodene.1", Player.name); //So that was the Burnished King. His blade was incredibly powerful, not to mention the magic at his disposal. It was a triumph you survived that.

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.VagrantAsphodene.2", Player.name); //So that was the Burnished King. His blade was incredibly powerful, not to mention the magic at his disposal. It was a triumph you survived that.



                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.VagrantAsphodene.3", Player.name); //So that was the Burnished King. His blade was incredibly powerful, not to mention the magic at his disposal. It was a triumph you survived that.



                    }

                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("ShatteredDisk").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.VagrantEridani.1", Player.name); //So that was the Burnished King. His blade was incredibly powerful, not to mention the magic at his disposal. It was a triumph you survived that.
                    }
                    if (dialogueLeft == 2)
                    {

                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.VagrantEridani.2", Player.name); //So that was the Burnished King. His blade was incredibly powerful, not to mention the magic at his disposal. It was a triumph you survived that.

                    }
                    if (dialogueLeft == 1)
                    {

                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.VagrantEridani.3", Player.name); //So that was the Burnished King. His blade was incredibly powerful, not to mention the magic at his disposal. It was a triumph you survived that.

                    }


                }
            }//Vagrant
            //Thespian
            if (chosenDialogue == 78)
            {
                if (chosenStarfarer == 1) // Asphodene
                {
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {



                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Thespian.Asphodene.1", Player.name);



                    }
                    if (dialogueLeft == 1)
                    {



                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Thespian.Asphodene.2", Player.name);



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
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Thespian.Eridani.1", Player.name);

                    }
                    if (dialogueLeft == 1)
                    {

                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.BossDialogue.Thespian.Eridani.2", Player.name);

                    }


                }
            }


            //Calamity boss dialogue: (Reminder: Calamity boss dialogue starts past 200) If Calamity is not detected, stop at 23. Otherwise...

            if (chosenDialogue == 201) // Boss dialogue - Desert Scourge
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
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.DesertScourge.Asphodene.1", Player.name); //You've defeated that evil serpent. I do wonder, though... Where did it come from?
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.DesertScourge.Asphodene.2", Player.name); //Looking closely, it seems to have been an aquatic monster.. once upon a time. So.. why was it in the desert?
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.DesertScourge.Asphodene.3", Player.name); //This world's secrets are still a mystery to us. Anyway, good job regardless.
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
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.DesertScourge.Eridani.1", Player.name); //You've bested the scourge of the sands. As for its origin, your guess is as good as mine. However...
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.DesertScourge.Eridani.2", Player.name); //It definitely was not made for tunneling. Instead, I'd postulate it was originally an aquatic beast. So.. what was it doing here? This world still holds many secrets...
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.DesertScourge.Eridani.3", Player.name); //Anywho... you fought bravely. I believe the loot will serve us well.

                        //	" ";
                    }
                }


            }//Desert Scourge 24
            if (chosenDialogue == 202) // Boss dialogue - Crabulon
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {//                                                                  | Safe dialogue limit.
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Crabulon.Asphodene.1", Player.name); //Okay.. so that just happened. Now that I've got a better look at the corpse of this mushroom crab thing, the truth is revealed- and it's not pretty.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Crabulon.Asphodene.2", Player.name); //It looks like the fungus have reanimated the crab's corpse, and paraded it around like a marionette. That's.. disgusting.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Crabulon.Asphodene.3", Player.name); //Whatever. No need to dawdle on that not-so-fun-fact. It's 6! Good job on that, by the way.
                        //	" ";
                    }

                }
                if (chosenStarfarer == 2)  //placeholder
                {//                                                              |
                    if (dialoguePrep == true)
                    {


                        dialogueLeft = 3;

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Crabulon.Eridani.1", Player.name); //Crabulon is no more. When I first laid eyes on it, I assumed it was some sort of mutated sea beast, akin to the Desert Scourge.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Crabulon.Eridani.2", Player.name); //However... the reality is much more sinister. It looks to me as if parasitic mushrooms were using this giant crab as a host to feed themselves more efficiently.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Crabulon.Eridani.3", Player.name); //Now that I'm reflecting on it, I feel sick. Thanks for putting an end to it.

                        //	" ";
                    }
                }


            }//Crabulon 25 
            if (chosenDialogue == 203) // Boss dialogue - Hive Mind
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {//                                                                  | Safe dialogue limit.
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.HiveMind.Asphodene.1", Player.name); //The Hive Mind has been defeated. It isn't the only source of the Corruption, but killing it definitely didn't do the Corruption any favors.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.HiveMind.Asphodene.2", Player.name); //That's about it for now. 

                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {//                                                              |
                    if (dialoguePrep == true)
                    {


                        dialogueLeft = 2;

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.HiveMind.Eridani.1", Player.name); //You've rid the world of the Hive Mind. While it isn't the main vector for the Corruption's influence, stifling it has probably helped alleviate some evil.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.HiveMind.Eridani.2", Player.name); //Let's keep on the lookout for any more threats the Corruption bears.
                        //	" ";
                    }

                }


            }//Hive Mind 26
            if (chosenDialogue == 204) // Boss dialogue - Perforators
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {//                                                                  | Safe dialogue limit.
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Perforators.Asphodene.1", Player.name); //You've bested the Perforator Hive. I wonder what causes the Crimson to coalese its evil in the form of spongy, fleshy monsters.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Perforators.Asphodene.2", Player.name); //It hungered for pure soil, no doubt wanting to spread the world's evil further. Of course, it isn't doing any of that anymore.

                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {//                                                              |
                    if (dialoguePrep == true)
                    {


                        dialogueLeft = 2;

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Perforators.Eridani.1", Player.name); //You've beat the Perforator Hive, and those abhorrent worms that joined it in combat. I'm not sure, but seeing fleshy things seriously makes my skin crawl.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Perforators.Eridani.2", Player.name); //Well, everything living is made of flesh. Ugh, you know what I mean...
                        //	" ";
                    }

                }


            }//Perforators 27
            if (chosenDialogue == 205) // Boss dialogue - The Slime God
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {//                                                                  | Safe dialogue limit.
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 4;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.SlimeGod.Asphodene.1", Player.name); //The Slime God is no more! It turns out that those huge red and purple slimes weren't actually part of the Slime God, but just strong minions.
                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.SlimeGod.Asphodene.2", Player.name); //I'm glad we've defeated it, but I seriously doubt this defeat means the end of all slimes. Sadly, there's just.. so many of them. 

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 4;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.SlimeGod.Asphodene.3", Player.name); //Wait a second.. Why am I 2? They explode in like three seconds! I mean, if we keep killing them, they'll go extinct!

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 6;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.SlimeGod.Asphodene.4", Player.name); //In a few centuries or so... Ugh.  

                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {//                                                              |
                    if (dialoguePrep == true)
                    {


                        dialogueLeft = 3;

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.SlimeGod.Eridani.1", Player.name); //Finally. The Slime God has been slain. Of course, this doesn't mean the end of slime as we know it. Seriously, they're just everywhere.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.SlimeGod.Eridani.2", Player.name); //I'm pretty sure they can split to make more. ...I'm no expert on the etymology of slimes or anything.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.SlimeGod.Eridani.3", Player.name); //It couldn't be a bad way to pass the time, though. Maybe I'll do some light reading. 
                        //	" ";
                    }

                }


            }//The Slime God 28
             //Calamity boss dialogue (Hardmode)
            if (chosenDialogue == 206) // Boss dialogue - Cryogen
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {//                                                                  | Safe dialogue limit.
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 3;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Cryogen.Asphodene.1", Player.name); //You've defeated Cryogen! And.. it turns out, 'Cryogen' was a prison for the Archmage. 
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Cryogen.Asphodene.2", Player.name); //From what the Archmage can tell us, it seems he was imprisoned in Cryogen by the Jungle Tyrant Yharim. If I recall correctly, we've heard about him before.

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Cryogen.Asphodene.3", Player.name); //Whoever they are, it seems they've caused this world no shortage of troubles. Hopefully, we can begin to unravel more of this mystery as we venture forth.

                        //	" ";
                    }



                }
                if (chosenStarfarer == 2)  //placeholder
                {//                                                              |
                    if (dialoguePrep == true)
                    {


                        dialogueLeft = 3;

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Cryogen.Eridani.1", Player.name); //Cryogen has been slain. And out from its corpse arrives this Archmage. From what he has to say, he was trapped there by an evil king...
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Cryogen.Eridani.2", Player.name); //Yharim, the Jungle Tyrant. Their reputation precedes them.. I have a feeling that they've been the source for all this world's many ailments.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Cryogen.Eridani.3", Player.name); //We must endeavor to unravel more of this story. I trust you're in agreement. 
                        //	" ";
                    }

                }


            }//Cryogen 29
            if (chosenDialogue == 207) // Boss dialogue - Aquatic Scourge
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {//                                                                  | Safe dialogue limit.
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {

                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.AquaticScourge.Asphodene.1", Player.name); //The Aquatic Scourge is no more, huh? So my hypothesis was correct; the Desert Scourge was displaced and dried... and we just fought its ancestor.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.AquaticScourge.Asphodene.2", Player.name); //The Sea King told us of what transpired. Yharim's forces are powerful indeed.. We have to stop their rampant evil! 

                        //	" ";
                    }




                }
                if (chosenStarfarer == 2)  //placeholder
                {//                                                              |
                    if (dialoguePrep == true)
                    {


                        dialogueLeft = 2;

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.AquaticScourge.Eridani.1", Player.name); //The Aquatic Scourge is defeated. So this is what the Desert Scourge could have been, had the ocean not been reduced to a desert.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.AquaticScourge.Eridani.2", Player.name); //From what the Sea King tells us, that was another one of Yharim's doings. They bring destruction wherever they roam... We must put an end to them.
                        //	" ";
                    }


                }


            }//Aquatic Scourge 30
            if (chosenDialogue == 208) // Boss dialogue - Brimstone Elemental
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {//                                                                  | Safe dialogue limit.
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {

                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.BrimstoneElemental.Asphodene.1", Player.name); //You've defeated the Brimstone Elemental. It seems they were once a revered goddess of the Underworld's mighty civilization, before Calamitas rent it asunder.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.BrimstoneElemental.Asphodene.2", Player.name); //I say we confront her sooner, than later. She must be incredibly powerful, but.. We have to do something, at least. 

                        //	" ";
                    }




                }
                if (chosenStarfarer == 2)  //placeholder
                {//                                                              |
                    if (dialoguePrep == true)
                    {


                        dialogueLeft = 2;

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.BrimstoneElemental.Eridani.1", Player.name); //The fire-borne spirit is no more. The land of brimstone must have been powerful, to worship this spirit as such. We merely fought her at her weakest.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.BrimstoneElemental.Eridani.2", Player.name); //If what we know is correct, Calamitas was the eventual end of the brimstone kingdom. While I want to put an end to her rampages, we must become stronger first.
                        //	" ";
                    }


                }


            }//Brimstone Elemental 31
            if (chosenDialogue == 209) // Boss dialogue - Calamitas
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {//                                                                  | Safe dialogue limit.
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {

                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Calamitas.Asphodene.1", Player.name); //...Calamitas is no more. Or, so we thought. What we faced was naught but a replica of the real thing. Even so, it was an incredibly powerful foe.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Calamitas.Asphodene.2", Player.name); //We must endeavor to get stronger. We have to! With things like this roaming the world, who else can stop them..? 

                        //	" ";
                    }




                }
                if (chosenStarfarer == 2)  //placeholder
                {//                                                              |
                    if (dialoguePrep == true)
                    {


                        dialogueLeft = 2;

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Calamitas.Eridani.1", Player.name); //We've defeated Calamitas... but.. It was naught but a clone of the real foe. Even as a duplicate, it stood as one of our toughest challenges yet.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Calamitas.Eridani.2", Player.name); //Who knows what the real thing can do.. We have to become stronger.  
                        //	" ";
                    }


                }


            }//Calamitas 32
            if (chosenDialogue == 210) // Boss dialogue - Leviathan
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {//                                                                  | Safe dialogue limit.
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {

                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Leviathan.Asphodene.1", Player.name); //Finally, we've bested Anahita and her Leviathan. What a thrilling battle... You've fought bravely, even when the fight looked insurmountable.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 5;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Leviathan.Asphodene.2", Player.name); //Hopefully, what we've obtained from their remains shall help us in the future. Man, we're on a roll! 

                        //	" ";
                    }




                }
                if (chosenStarfarer == 2)  //placeholder
                {//                                                              |
                    if (dialoguePrep == true)
                    {


                        dialogueLeft = 3;

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Leviathan.Eridani.1", Player.name); //You've defeated the Leviathan and Anahita. A battle well fought- when the Leviathan appeared, I feared the worst. 
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 4;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Leviathan.Eridani.2", Player.name); //I'll try not to doubt you in the future. After all, you've got my help.  
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Leviathan.Eridani.3", Player.name); //I am helping, right? Right...  
                        //	" ";
                    }

                }


            }//Leviathan 33
            if (chosenDialogue == 211) // Boss dialogue - Astrum Aureus
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {//                                                                  | Safe dialogue limit.
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {

                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.AstrumAureus.Asphodene.1", Player.name); //We've done in Astrum Aureus. It's a good reminder that the Astral Infection consumes indiscriminately. Even modern mechanics aren't safe from it.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.AstrumAureus.Asphodene.2", Player.name); //Even so, it's solid proof that we're getting stronger. Can you imagine fighting that right after we fought Skeletron? Exactly!

                        //	" ";
                    }




                }
                if (chosenStarfarer == 2)  //placeholder
                {//                                                              |
                    if (dialoguePrep == true)
                    {


                        dialogueLeft = 2;

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.AstrumAureus.Eridani.1", Player.name); //That's that. Astrum Aureus is 6. Even as the Astral Infection made a perversion of the original, it beat with a heart of iron still.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.AstrumAureus.Eridani.2", Player.name); //The Astral Infection... It's dangerous. At least we've made some good progress, though. Don't forget that. 
                        //	" ";
                    }


                }


            }//Astrum Aureus 34
            if (chosenDialogue == 212) // Boss dialogue - Plaguebringer Goliath
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {//                                                                  | Safe dialogue limit.
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {

                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.PlaguebringerGoliath.Asphodene.1", Player.name); //Nice, you've bested the Plaguebringer Goliath. The ingenuity of man never ceases to amaze, even if their knowledge usually is imparted in hyper-active death machines.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.PlaguebringerGoliath.Asphodene.2", Player.name); //Well, what can you do. There's people like us here to take care of that part.  

                        //	" ";
                    }




                }
                if (chosenStarfarer == 2)  //placeholder
                {//                                                              |
                    if (dialoguePrep == true)
                    {


                        dialogueLeft = 3;

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.PlaguebringerGoliath.Eridani.1", Player.name); //Seriously.. what's with the Jungle? At least the Plaguebringer Goliath is 6. Even so, I can't shake it out of my mind. I can't handle bugs, like.. at all.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.PlaguebringerGoliath.Eridani.2", Player.name); //Bugs, bugs..  They continue to rear their ugly heads wherever I look..! 
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.PlaguebringerGoliath.Eridani.3", Player.name); //Whoops.. I was venting a little. Let's just move on. Good job defeating that foe. 
                        //	" ";
                    }


                }


            }//Plaguebringer Goliath 35
            if (chosenDialogue == 213) // Boss dialogue - Ravager
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {//                                                                  | Safe dialogue limit.
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 2;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {

                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Ravager.Asphodene.1", Player.name); //The Ravager is gone from this world. It was concieved in retaliation against the conquests of the Jungle Tyrant, but it evidently did little in that regard.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Ravager.Asphodene.2", Player.name); //At the very least, we could finally let it rest. If this is what humans were led to, what truly happened all those years ago that could lead to such desperation?

                        //	" ";
                    }




                }
                if (chosenStarfarer == 2)  //placeholder
                {//                                                              |
                    if (dialoguePrep == true)
                    {


                        dialogueLeft = 2;

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 2)
                    {

                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Ravager.Eridani.1", Player.name); //We've defeated the Ravager. Once upon a time, it served as a grim last resort employed against the Jungle Tyrant. Now it can finally rest.


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.Ravager.Eridani.2", Player.name); //Even in death, it stalked without purpose. I'm glad we bested it, but its existence does nothing but give credence to the tales of Yharim's overwhelming might.

                        //	" ";
                    }


                }


            }//Ravager 36
            if (chosenDialogue == 214) // Boss dialogue - Astrum Deus
            {
                if (chosenStarfarer == 1) // Asphodene  //placeholder
                {//                                                                  | Safe dialogue limit.
                    if (dialoguePrep == true)
                    {
                        dialogueLeft = 4;
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 4)
                    {

                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.AstrumDeus.Asphodene.1", Player.name); //We've defeated Astrum Deus. Astrum Deus is- or was- one of the many gods that ruled over the universe. As far as I can remember, it was gone before I was born.


                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.AstrumDeus.Asphodene.2", Player.name); //However... Astrum Deus can regenerate itself. Through assimilating life around its corpse, it can eventually return to the cosmos. It's kin to the story of the ouroboros.

                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 3;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.AstrumDeus.Asphodene.3", Player.name); //Let's return to what I said earlier. Astrum Deus was a GOD. What we fought was much weaker. What in the world was able to defeat a deity of that caliber?

                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.AstrumDeus.Asphodene.4", Player.name); //This was a well fought victory... Even so, stay vigilant. I have a feeling our culprit is closer than we think.

                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {//                                                              |
                    if (dialoguePrep == true)
                    {


                        dialogueLeft = 4;

                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.AstrumDeus.Eridani.1", Player.name); //Great work. You've defeated Astrum Deus. I have some personal knowledge, so allow me to share... 
                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 1;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.AstrumDeus.Eridani.2", Player.name); //Astrum Deus is one of many cosmic deities. It disappeared before I was born, but knowledge states that it can assimilate life near its corpse to regenerate itself.
                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 2;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.AstrumDeus.Eridani.3", Player.name); //So, that begs the question.. What could be walking this mortal plane able to befall an actual, literal god? What we fought was but a child in comparison.
                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 0;
                        dialogue = LangHelper.GetTextValue($"Dialogue.CalamityBossDialogue.AstrumDeus.Eridani.4", Player.name); //Let's shelve that train of thought. While I have my fears, this is our victory. Congratulations. 
                        //	" ";
                    }

                }


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
                    dialogueLeft = 4;
                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("AncientShard").Type);
                    dialoguePrep = false;
                }


                if (dialogueLeft == 4)
                {
                    expression = 12;

                    dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.Nalhaun.1", Player.name); //We meet again, {Player.name}. I need your help.  

                    //	" ";
                }
                if (dialogueLeft == 3)
                {
                    expression = 12;

                    dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.Nalhaun.2", Player.name); //I've given you the 'Ancient Shard' With it, you can call forth a powerful foe, the Burnished King. 

                    //	" ";
                }
                if (dialogueLeft == 2)
                {
                    expression = 12;

                    dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.Nalhaun.3", Player.name); //If my hypothesis is correct, he may be an important step towards confronting the First Starbearer, and stopping his rampage.

                    //	" ";
                }
                if (dialogueLeft == 1)
                {
                    expression = 12;

                    dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.Nalhaun.4", Player.name); //Prepare yourself: this fight will not be easy.  

                    //	" ";
                }





            }//Nalhaun item
            if (chosenDialogue == 302) //Penth item
            {

                if (dialoguePrep == true)                                     // |
                {
                    dialogueLeft = 4;
                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("UnsulliedCanvas").Type);
                    dialoguePrep = false;
                }


                if (dialogueLeft == 4)
                {
                    expression = 12;

                    dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.Penth.1", Player.name); //We meet again, {Player.name}. Again, I need your assistance.  



                    //	" ";
                }
                if (dialogueLeft == 3)
                {
                    expression = 12;

                    dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.Penth.2", Player.name); //This is the 'Unsullied Canvas.' It will draw the Witch of Ink out of hiding. 

                    //	" ";
                }
                if (dialogueLeft == 2)
                {
                    expression = 12;
                    dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.Penth.3", Player.name); //This is another important step in the pursuit of the the First Starbearer. 


                    //	" ";
                }
                if (dialogueLeft == 1)
                {
                    expression = 12;

                    dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.Penth.4", Player.name); //Please prepare yourself. This will be a hard fight.  

                    //	" ";
                }





            }//Penth item
            if (chosenDialogue == 303) //Arbiter item
            {

                if (dialoguePrep == true)                                     // |
                {
                    dialogueLeft = 4;
                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("DemonicCrux").Type);
                    dialoguePrep = false;
                }


                if (dialogueLeft == 4)
                {
                    expression = 12;

                    dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.Arbitration.1", Player.name); //We meet again, {Player.name}. Again, I have another foe for you to defeat.  



                    //	" ";
                }
                if (dialogueLeft == 3)
                {
                    expression = 12;

                    dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.Arbitration.2", Player.name); //I've given you the 'Beating Crux' When used, it will call forth a powerful demon. 

                    //	" ";
                }
                if (dialogueLeft == 2)
                {
                    expression = 12;
                    dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.Arbitration.3", Player.name); //This is another important step in the pursuit of the the First Starbearer. 


                    //	" ";
                }
                if (dialogueLeft == 1)
                {
                    expression = 12;

                    dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.Arbitration.4", Player.name); //Prepare yourself well.   

                    //	" ";
                }





            }//Arbiter item
            if (chosenDialogue == 304) //Warrior item
            {



                if (chosenStarfarer == 1) // Asphodene  //placeholder | 7 6 Inside
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 12;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("ProgenitorWish").Type);
                        dialoguePrep = false;
                    }
                    if (dialogueLeft == 12)
                    {
                        expression = 3;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorAsphodene.1", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 11)
                    {
                        expression = 12;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorAsphodene.2", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 10)
                    {
                        expression = 1;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorAsphodene.3", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 9)
                    {
                        expression = 12;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorAsphodene.4", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 8)
                    {
                        expression = 1;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorAsphodene.5", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 7)
                    {
                        expression = 12;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorAsphodene.6", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 6)
                    {
                        expression = 1;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorAsphodene.7", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 5)
                    {
                        expression = 12;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorAsphodene.8", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 12;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorAsphodene.9", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 12;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorAsphodene.10", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 12;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorAsphodene.11", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 2;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorAsphodene.12", Player.name);


                        //	" ";
                    }


                }
                if (chosenStarfarer == 2)  //placeholder
                {
                    if (dialoguePrep == true)                                     // |
                    {
                        dialogueLeft = 12;
                        Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("ProgenitorWish").Type);
                        dialoguePrep = false;
                    }

                    if (dialogueLeft == 12)
                    {
                        expression = 3;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorEridani.1", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 11)
                    {
                        expression = 12;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorEridani.2", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 10)
                    {
                        expression = 1;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorEridani.3", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 9)
                    {
                        expression = 12;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorEridani.4", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 8)
                    {
                        expression = 1;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorEridani.5", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 7)
                    {
                        expression = 12;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorEridani.6", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 6)
                    {
                        expression = 1;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorEridani.7", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 5)
                    {
                        expression = 12;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorEridani.8", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 4)
                    {
                        expression = 12;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorEridani.9", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 3)
                    {
                        expression = 12;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorEridani.10", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 2)
                    {
                        expression = 12;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorEridani.11", Player.name);


                        //	" ";
                    }
                    if (dialogueLeft == 1)
                    {
                        expression = 2;

                        dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.WarriorEridani.12", Player.name);


                        //	" ";
                    }

                }





            }//Warrior item
            if (chosenDialogue == 305) //Dioskouroi Item
            {

                if (dialoguePrep == true)                                     // |
                {
                    dialogueLeft = 4;
                    Player.QuickSpawnItem(Player.GetSource_GiftOrReward(), Mod.Find<ModItem>("TwincruxPendant").Type);
                    dialoguePrep = false;
                }


                if (dialogueLeft == 4)
                {
                    expression = 12;

                    dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.Dioskouroi.1", Player.name); //{0}, you can tell as well as I that the situation has turned dire.  



                    //	" ";
                }
                if (dialogueLeft == 3)
                {
                    expression = 12;

                    dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.Dioskouroi.2", Player.name); //The First Starbearer's gaze has been turned to you, and his power has blanketed the world in overwhelming Light. 

                    //	" ";
                }
                if (dialogueLeft == 2)
                {
                    expression = 12;
                    dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.Dioskouroi.3", Player.name); //I have given you the 'Progenitor's Wish.' It will serve as a way to draw him out. From then on, it will be your strength against his determination.


                    //	" ";
                }
                if (dialogueLeft == 1)
                {
                    expression = 12;

                    dialogue = LangHelper.GetTextValue($"Dialogue.BossItemDialogue.Dioskouroi.4", Player.name); //Please, you must win this fight. The stakes have never been higher.  

                    //	" ";
                }

            }//Dioskouroi item

            dialogue = LangHelper.Wrap(dialogue, 44);
        }
        //Dialouge system rework.
        public override void PostSetupContent()
        {
           /* var archive = new DialogueArchive();

            var dialogue1 = new Dialogue(a);
            dialogue1.AddPage(a);
            dialogue1.AddExtraId(1);

            //populate the archive here
            archive.AddDialogue(dialogue1);

            //Remember, dialogue needs to be accessed with the spatial disk and should really be able to be accessed without adding any extra lines
            base.PostSetupContent();*/
        }
    }
    public class Dialogue
    {
        //Asphodene/Eridani
        //if the dialogue is by someone else, this can vary
        public string Name { get; set; }

        //Categories can be longer than 1 segment
        //(BossItemDialogue vs BossDialogue.KingSlime)
        public string Category { get; set; }
        // category should point to the hjson (Dialogue.[Category].[Name].[Length] i.e. Dialogue.[BossItemDialogue].[Dioskouroi].[4])
        // dialogue should keep track of its unlock condition and if it has been read or not too
        // 
        // Given length, it should automatically populate
        public int Length { get; set; }

        //Dialogue, Emotion
        public Dictionary<string, string> DialoguePages { get; set; } = new Dictionary<string, string>();
        public string AssociatedItem { get; set; }
        //Unlock condition (This appears in the Archive)
        //Important info: this gives an esssence, or boss summon item, etc. (This appears in the Archive) - if there isnt any important info it defaults to unlock condition
        public string UnlockCondition { get; set; }
        public string ArchiveInfo { get; set; } //maybe this can be automatically defined for weapons taking into account AssociatedItem if Aspho/Eri's archives are correctly seperated
        //Archive Category (Idle, Boss, Weapon)
        public string ArchiveCategory { get; set; }
        public List<int> ExtraIds { get; set; } = new List<int>();

        //Maybe a place where you can define which line the item spawns on?
        public Dialogue(string name, string category, int length, string associatedItem, string archiveCategory)
        {
            Name = name;
            Category = category;
            AssociatedItem = associatedItem; // if associated item is 0, there isn't an item

            for(int i = 0; i < Length; i++)
            {
                AddPage(LangHelper.GetTextValue($"Dialogue." + category + "." + name + "." + i, Main.LocalPlayer.name), LangHelper.GetTextValue($"Dialogue." + category + "." + name + "." + i + ".Emotion"));
            }
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
    
    
}
