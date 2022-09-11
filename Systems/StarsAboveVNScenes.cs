using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;

namespace StarsAbove
{
    //Basic outline of visual novel code: (flowchart)
    /*
	 *
	 * 
	 * */


    public static class VNScenes
    {
        /// <summary>
        /// Version 0.2 by PaperLuigi
        /// Visual novel code
        /// </summary>
        /// <param name="sceneID">The current scene.</param>
        /// <param name="sceneProgress">The progress of the scene.</param>
        /// <returns>
        /// 
        /// </returns>
        /// 

        public static object[] SetupVNSystem(int sceneID, int sceneProgress)
        {
            //Setting up the variables to be used later.
            int sceneLength = 0; //0
            bool sceneHasChoice = false; // 1
            string sceneChoice1 = ""; //2
            string sceneChoice2 = ""; //3
            int choice1Scene = 0; //4
            int choice2Scene = 0; //5
            string character1 = ""; //6
            int character1Pose = 0; //7
            int character1Expression = 0; //8
            string character2 = "None"; //9
            int character2Pose = 0; //10
            int character2Expression = 0; //11
            string name = ""; //12
            string dialogue = ""; //13

            //Test scene. Does not work.
            if (sceneID == 0)
            {
                //How long is the scene? (Scenes start at 0!)
                sceneLength = 0;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = true;

                //What appears in the choice boxes?
                sceneChoice1 =
                    "Click me to start the test dialogue again.";
                sceneChoice2 =
                    "This is the second choice.";

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 2;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;

                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;

                    //Who's name should be in the dialogue box?
                    name = "Asphodene";

                    //What is the dialogue?
                    dialogue = "" +
                                  "Test dialogue" +
                                " Speaker: Asphodene" +
                                " Pose = 0" +
                                " Expression = 0";
                }

            }

            //Asphodene's new introduction. Leads into 4 and 5.
            if (sceneID == 3)
            {
                //How long is the scene? (Scenes start at 0!)
                sceneLength = 2;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = true;

                //What appears in the choice boxes?
                sceneChoice1 =
                  "I'd like the full explanation.";
                sceneChoice2 =
                  "I'm good, thanks.";

                //What does the scene change to when you choose the first option?
                choice1Scene = 4;

                //What does the scene change to when you choose the second option?
                choice2Scene = 5;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = "" +
                                "Greetings! I am Asphodene- a Starfarer," +
                              " and in turn, that makes you my Starbearer." +
                              " " +
                              " ";
                }
                if (sceneProgress == 1)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 1;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = "" +
                                "I will lend you my strength so that we may" +
                              " defeat the threats to this world together." +
                              " " +
                              " ";
                }
                if (sceneProgress == 2)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 4;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = "" +
                                "There's more in it for you, though." +
                              " If you'd like, I can give you the description" +
                              " of what I can help you with. I recommend it!" +
                              " ";
                }
            }
            //Asphodene's full explanation.
            if (sceneID == 4)
            {
                //How long is the scene? (Scenes start at 0!)
                sceneLength = 3;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  "I'd like the full explanation.";
                sceneChoice2 =
                  "I'm good, thanks.";

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = "" +
                                "Okay, where to start..." +
                              " The Spatial Disk you used to form the contract" +
                              " can be used to access the Stellar Array, which" +
                              " provides abilities after defeating bosses. Nice, right?";
                }
                if (sceneProgress == 1)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?

                    dialogue = "" +
                                "The Spatial Disk can do a lot of other" +
                              " things, but for now, the Stellar Array" +
                              " is the most important. Remember to" +
                              " check it often!";
                }
                if (sceneProgress == 2)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 4;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = "" +
                                "One last thing.. I can turn energy from other" +
                              " worlds into direct power in the form of Essences." +
                              " Combining these with other materials should be" +
                              " enough to create powerful Aspected Weapons.";
                }
                if (sceneProgress == 3)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 5;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = "" +
                                $"Right, my power is yours." +
                              " Let's show this world what we can do." +
                              " " +
                              " ";
                }
            }
            //Asphodene's shortened explanation.
            if (sceneID == 5)
            {

                sceneLength = 0;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  " ";
                sceneChoice2 =
                  " ";

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = "" +
                                "Well, in that case..." +
                              " My power is yours." +
                              " Let's show this world what we can do." +
                              " ";
                }
            }
            //Eridani's new introduction. Leads into 7 and 8.
            if (sceneID == 6)
            {
                //How long is the scene? (Scenes start at 0!)
                sceneLength = 2;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = true;

                //What appears in the choice boxes?
                sceneChoice1 =
                "I'd like the full explanation.";
                sceneChoice2 =
                "I'm good, thanks.";

                //What does the scene change to when you choose the first option?
                choice1Scene = 7;

                //What does the scene change to when you choose the second option?
                choice2Scene = 8;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Eridani";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = "" +
                              "It's nice to meet you." +
                            " My name is Eridani.. a Starfarer." +
                            " You, in turn, are my Starbearer." +
                            " ";
                }
                if (sceneProgress == 1)
                {
                    //Who is the main character?
                    character1 = "Eridani";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 1;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = "" +
                              "Through this contract, we must work together" +
                            " to bring justice to those willing to harm" +
                            " this world." +
                            " ";
                }
                if (sceneProgress == 2)
                {
                    //Who is the main character?
                    character1 = "Eridani";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 3;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = "" +
                              "If you are willing, I can give a description" +
                            " of what I can provide you with. Personally," +
                            " I recommend it- knowledge is power, after all." +
                            " ";
                }
            }
            //Eridani's full explanation.
            if (sceneID == 7)
            {
                //How long is the scene? (Scenes start at 0!)
                sceneLength = 3;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                "I'd like the full explanation.";
                sceneChoice2 =
                "I'm good, thanks.";

                //What does the scene change to when you choose the first option?
                choice1Scene = 7;

                //What does the scene change to when you choose the second option?
                choice2Scene = 8;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Eridani";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = "" +
                              "Right. Let's begin." +
                            " With the Spatial Disk you used, you can" +
                            " access the Stellar Array. With the defeat" +
                            " of powerful foes, it will grant you strength.";
                }
                if (sceneProgress == 1)
                {
                    //Who is the main character?
                    character1 = "Eridani";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = "" +
                              "The Spatial Disk has plenty of other uses, but" +
                            " by my hypothesis we don't have access to them yet." +
                            " Hopefully, as we get stronger, more facets of the Disk" +
                            " will open up to us.";
                }
                if (sceneProgress == 2)
                {
                    //Who is the main character?
                    character1 = "Eridani";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 3;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = "" +
                              "Umm.. what next..? Right." +
                            " I can turn latent energy from other worlds into" +
                            " crystallized power- an Essence, if you will." +
                            " You can turn these Essences into Aspected Weapons.";
                }
                if (sceneProgress == 3)
                {
                    //Who is the main character?
                    character1 = "Eridani";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 4;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = "" +
                              "That should be everything." +
                            " My strength is yours... I won't let you down." +
                            " " +
                            " ";
                }
            }
            //Eridani's shortened explanation.
            if (sceneID == 8)
            {
                //How long is the scene? (Scenes start at 0!)
                sceneLength = 0;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                " ";
                sceneChoice2 =
                " ";

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Eridani";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 4;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = "" +
                              "In that case.." +
                            " My power is yours." +
                            " Let's show this world what we can do." +
                            " ";
                }
            }

            //Post-Vagrant dialogue (Asphodene ver.)
            if (sceneID == 9)
            {
                //How long is the scene? (Scenes start at 0!)
                sceneLength = 19;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                "";
                sceneChoice2 =
                "";

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 1;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = "" +
                              "I see..." +
                            " Please, excuse my test. I know it was crude," +
                            " but it's the only way I know how to judge someone." +
                            " ";
                }
                if (sceneProgress == 1)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 1;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = "" +
                              "In reality, I should have known." +
                            " My sisters would never choose unwisely." +
                            " " +
                            " ";
                }
                if (sceneProgress == 2)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 1;

                    //What is their expression?
                    character2Expression = 3;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = "" +
                              "Huh? Sister?" +
                            " No way..." +
                            " " +
                            " ";
                }
                if (sceneProgress == 3)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 1;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 1;

                    //What is their expression?
                    character2Expression = 2;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = "" +
                              "I apologize, Asphodene." +
                            " This should help." +
                            " Ring any bells?" +
                            " ";
                }
                if (sceneProgress == 4)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 1;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 1;

                    //What is their expression?
                    character2Expression = 2;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = "" +
                              "Dude. We have GOT to stop" +
                            " meeting like this." +
                            " The nerve...!" +
                            " ";
                }
                if (sceneProgress == 5)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 1;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = $"Please, let me explain, Starbearer." +
                            $" In tandem with our galaxy's myriad worlds," +
                            $" there exists Starfarers to aid their denizens." +
                            $" You are familiar with what we do.";
                }
                if (sceneProgress == 6)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 1;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = $"As we are conduits of energy," +
                           $" we choose a Starbearer to act as our envoy." +
                           $" They- or you, as it were-" +
                           $" work kind of like a magnifying glass.";
                }
                if (sceneProgress == 7)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 1;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = $"However, the First Starfarer's nominee" +
                            $" was... unfitting for their role." +
                            $" This is the First Starbearer. From what I know," +
                            $" they seem to have lost their mind and morals.";
                }
                if (sceneProgress == 8)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 1;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = $"Now, they continue to roam the galaxy," +
                            $" the Starfarer a thrall to their chosen one's" +
                            $" whims. They 'pursue good' through any" +
                            $" means necessary, often with casualties.";
                }
                if (sceneProgress == 9)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 1;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = $"This is all conjecture, but the destruction" +
                            $" is very real. It should go unsaid that we" +
                            $" can't let this continue." +
                            $" I'll need your help in the future.";
                }
                if (sceneProgress == 10)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 2;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = $"As it stands now, you have little chance" +
                            $" of besting them. However, while Asphodene" +
                            $" doesn't know it, all Starfarers have an" +
                            $" ace up their sleeve: the Stellar Nova.";
                }
                if (sceneProgress == 11)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 3;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = $"She doesn't know it, but I've" +
                            $" already bequeathed the power to her" +
                            $" the second you've won our duel." +
                            $" ";
                }
                if (sceneProgress == 12)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 3;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = $"That is all for now." +
                            $" I must continue to track the First Starbearer." +
                            $" Good luck- I will have need of you soon." +
                            $" ";
                }
                if (sceneProgress == 13)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 2;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = $"I know he's a loner, but.." +
                           $" I wish we could have spoken more." +
                           $" Stay safe, Perseus." +
                           $" ";
                }
                if (sceneProgress == 14)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = $"Huh, he was right. I do feel stronger-" +
                           $" and my hair's gone all shiny! This power" +
                           $" should be an amazing boon going forward." +
                           $" ";
                }
                if (sceneProgress == 15)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = $"Looks like my Stellar Nova is called" +
                           $" 'Theofania Inanis.' Perhaps we'll get" +
                           $" to use some more once we get stronger?" +
                           $" ";
                }
                if (sceneProgress == 16)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = $"After binding the Stellar" +
                            $" Nova Key, you should be able" +
                            $" to equip the Stellar Nova through" +
                            $" the Spatial Disk, like usual.";
                }
                if (sceneProgress == 17)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = $"You should have some" +
                            $" Prismatic Cores already. With their" +
                            $" power, I theorize you can upgrade" +
                            $" Stellar Novas with crafting.";
                }
                if (sceneProgress == 18)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = $"This is important. You need" +
                            $" Stellar Nova energy to cast Novas." +
                            $" You gain Nova energy in combat." +
                            $" It depletes outside of combat.";
                }
                if (sceneProgress == 19)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = $"OK. A lot has happened," +
                            $" but this is a huge step forward for" +
                            $" us. Please use the Novas well!" +
                            $" Bye for now.";
                }
            }
            //Post-Vagrant dialogue (Eridani ver.)
            if (sceneID == 10)
            {
                //How long is the scene? (Scenes start at 0!)
                sceneLength = 19;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                "";
                sceneChoice2 =
                "";

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 1;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = "" +
                              "I see..." +
                            " Please, excuse my test. I know it was crude," +
                            " but it's the only way I know how to judge someone." +
                            " ";
                }
                if (sceneProgress == 1)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 1;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = "" +
                              "In reality, I should have known." +
                            " My sisters would never choose unwisely." +
                            " " +
                            " ";
                }
                if (sceneProgress == 2)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 1;

                    //What is their expression?
                    character2Expression = 3;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = $"Did you just say... 'sister'?" +
                           $" You've got to be kidding..." +
                           $" ";
                }
                if (sceneProgress == 3)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 1;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 1;

                    //What is their expression?
                    character2Expression = 2;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = "" +
                              "I apologize, Eridani." +
                            " This should help." +
                            " Ring any bells?" +
                            " ";
                }
                if (sceneProgress == 4)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 1;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 2;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = "" +
                              "We haven't talked in ages-" +
                            " and you show up now...?" +
                            " " +
                            " ";
                }
                if (sceneProgress == 5)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 1;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = $"Please, let me explain, Starbearer." +
                            $" In tandem with our galaxy's myriad worlds," +
                            $" there exists Starfarers to aid their denizens." +
                            $" You are familiar with what we do.";
                }
                if (sceneProgress == 6)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 1;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = $"As we are conduits of energy," +
                           $" we choose a Starbearer to act as our envoy." +
                           $" They- or you, as it were-" +
                           $" work kind of like a magnifying glass.";
                }
                if (sceneProgress == 7)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 1;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = $"However, the First Starfarer's nominee" +
                            $" was... unfitting for their role." +
                            $" This is the First Starbearer. From what I know," +
                            $" they seem to have lost their mind and morals.";
                }
                if (sceneProgress == 8)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 1;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = $"Now, they continue to roam the galaxy," +
                            $" the Starfarer a thrall to their chosen one's" +
                            $" whims. They 'pursue good' through any" +
                            $" means necessary, often with casualties.";
                }
                if (sceneProgress == 9)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 1;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = $"This is all conjecture, but the destruction" +
                            $" is very real. It should go unsaid that we" +
                            $" can't let this continue." +
                            $" I'll need your help in the future.";
                }
                if (sceneProgress == 10)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 2;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = $"As it stands now, you have little chance" +
                            $" of besting them. However, while Eridani" +
                            $" doesn't know it, all Starfarers have an" +
                            $" ace up their sleeve: the Stellar Nova.";
                }
                if (sceneProgress == 11)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 3;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = $"She doesn't know it, but I've" +
                            $" already bequeathed the power to her" +
                            $" the second you've won our duel." +
                            $" ";
                }
                if (sceneProgress == 12)
                {
                    //Who is the main character?
                    character1 = "Perseus";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 3;


                    //Who's name should be in the dialogue box?
                    name = "Perseus";


                    //What is the dialogue?
                    dialogue = $"That is all for now." +
                            $" I must continue to track the First Starbearer." +
                            $" Good luck- I will have need of you soon." +
                            $" ";
                }
                if (sceneProgress == 13)
                {
                    //Who is the main character?
                    character1 = "Eridani";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 2;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = $"Perseus was never one for sticking around." +
                           $" I wish we could have spoken more..." +
                           $" This power is incredible, but I need to get used to it." +
                           $" ";
                }
                if (sceneProgress == 14)
                {
                    //Who is the main character?
                    character1 = "Eridani";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = $"My Stellar Nova is called" +
                            $" 'Theofania Inanis.' Perhaps we will" +
                            $" be able to utilize more once" +
                            $" we become stronger.";
                }
                if (sceneProgress == 15)
                {
                    //Who is the main character?
                    character1 = "Eridani";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = $"After binding the Stellar" +
                             $" Nova Key, you should be able" +
                             $" to equip the Stellar Nova through" +
                             $" the Spatial Disk, like usual.";
                }
                if (sceneProgress == 16)
                {
                    //Who is the main character?
                    character1 = "Eridani";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = $"With Prismatic Cores," +
                            $" you can craft upgrades to the" +
                            $" Stellar Nova, I believe. You can affix them" +
                            $" in the menu directly.";
                }
                if (sceneProgress == 17)
                {
                    //Who is the main character?
                    character1 = "Eridani";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = $"Don't forget: You need" +
                            $" Stellar Nova Energy to cast" +
                            $" Stellar Novas. You can only" +
                            $" accrue Energy in combat.";
                }
                if (sceneProgress == 18)
                {
                    //Who is the main character?
                    character1 = "Eridani";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = $"That's about it.." +
                            $" I know it's a lot to process, but" +
                            $" please use the Novas well." +
                            $" That's all for now.";
                }
                if (sceneProgress == 19)
                {
                    //Who is the main character?
                    character1 = "Eridani";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 2;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = $"...Did my hair just change color?" +
                            $" " +
                            $" " +
                            $"";
                }
            }

            //dialogue = 


            return new object[] {
            sceneLength,
            sceneHasChoice,
            sceneChoice1,
            sceneChoice2,
            choice1Scene,
            choice2Scene,
            character1,
            character1Pose,
            character1Expression,
            character2,
            character2Pose,
            character2Expression,
            name,
            dialogue
            };
        }


        



    }

    

}