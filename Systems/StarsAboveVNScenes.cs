using StarsAbove.Utilities;
using Terraria;

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
            bool thirdOption = false;//14
            string sceneChoice3 = "";//15
            int choice3Scene = 0;//16

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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID."); //Test dialogue Speaker: Asphodene Pose = 0 Expression = 0
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
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID."+ sceneID +".Dialogue." + sceneProgress);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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

                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.6.Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.6.Choices.2");

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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID."+ sceneID +".Dialogue." + sceneProgress);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.1"); //I see... Please, excuse my test. I know it was crude, but it's the only way I know how to judge someone. 
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.2"); //In reality, I should have known. My sisters would never choose unwisely.  
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.3"); //Huh? Sister? No way...  
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.4"); //I apologize, Asphodene. This should help. Ring any bells? 
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.5"); //Dude. We have GOT to stop meeting like this. The nerve...! 
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.6"); //Please, let me explain, Starbearer. In tandem with our galaxy's myriad worlds, there exists Starfarers to aid their denizens. You are familiar with what we do.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.7"); //As we are conduits of energy, we choose a Starbearer to act as our envoy. They- or you, as it were- work kind of like a magnifying glass.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.8"); //However, the First Starfarer's nominee was... unfitting for their role. This is the First Starbearer. From what I know, they seem to have lost their mind and morals.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.9"); //Now, they continue to roam the galaxy, the Starfarer a thrall to their chosen one's whims. They 'pursue good' through any means necessary, often with casualties.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.10"); //This is all conjecture, but the destruction is very real. It should go unsaid that we can't let this continue. I'll need your help in the future.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.11"); //As it stands now, you have little chance of besting them. However, while Asphodene doesn't know it, all Starfarers have an ace up their sleeve: the Stellar Nova.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.12"); //She doesn't know it, but I've already bequeathed the power to her the second you've won our duel. 
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.13"); //That is all for now. I must continue to track the First Starbearer. Good luck- I will have need of you soon. 
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.14"); //I know he's a loner, but.. I wish we could have spoken more. Stay safe, Perseus. 
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.15"); //Huh, he was right. I do feel stronger- and my hair's gone all shiny! This power should be an amazing boon going forward. 
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.16"); //Looks like my Stellar Nova is called 'Theofania Inanis.' Perhaps we'll get to use some more once we get stronger? 
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.17"); //After binding the Stellar Nova Key, you should be able to equip the Stellar Nova through the Spatial Disk, like usual.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.18"); //You should have some Prismatic Cores already. With their power, I theorize you can upgrade Stellar Novas with crafting.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.19"); //This is important. You need Stellar Nova energy to cast Novas. You gain Nova energy in combat. It depletes outside of combat.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.9.Dialogue.20"); //OK. A lot has happened, but this is a huge step forward for us. Please use the Novas well! Bye for now.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.1"); //I see... Please, excuse my test. I know it was crude, but it's the only way I know how to judge someone. 
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.2"); //In reality, I should have known. My sisters would never choose unwisely.  
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.3");
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.4"); //I apologize, Eridani. This should help. Ring any bells? 
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.5"); //We haven't talked in ages- and you show up now...?  
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.6"); //Please, let me explain, Starbearer. In tandem with our galaxy's myriad worlds, there exists Starfarers to aid their denizens. You are familiar with what we do.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.7"); //As we are conduits of energy, we choose a Starbearer to act as our envoy. They- or you, as it were- work kind of like a magnifying glass.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.8"); //However, the First Starfarer's nominee was... unfitting for their role. This is the First Starbearer. From what I know, they seem to have lost their mind and morals.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.9"); //Now, they continue to roam the galaxy, the Starfarer a thrall to their chosen one's whims. They 'pursue good' through any means necessary, often with casualties.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.10"); //This is all conjecture, but the destruction is very real. It should go unsaid that we can't let this continue. I'll need your help in the future.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.11"); //As it stands now, you have little chance of besting them. However, while Eridani doesn't know it, all Starfarers have an ace up their sleeve: the Stellar Nova.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.12"); //She doesn't know it, but I've already bequeathed the power to her the second you've won our duel. 
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.13"); //That is all for now. I must continue to track the First Starbearer. Good luck- I will have need of you soon. 
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.14"); //Perseus was never one for sticking around. I wish we could have spoken more... This power is incredible, but I need to get used to it. 
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.15"); //My Stellar Nova is called 'Theofania Inanis.' Perhaps we will be able to utilize more once we become stronger.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.16"); //After binding the Stellar Nova Key, you should be able to equip the Stellar Nova through the Spatial Disk, like usual.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.17"); //With Prismatic Cores, you can craft upgrades to the Stellar Nova, I believe. You can affix them in the menu directly.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.18"); //Don't forget: You need Stellar Nova Energy to cast Stellar Novas. You can only accrue Energy in combat.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.19"); //That's about it.. I know it's a lot to process, but please use the Novas well. That's all for now.
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID.10.Dialogue.20"); //...Did my hair just change color?  
                }
            }
            //Asphodene introduces the Astrolabe.
            if (sceneID == 11)
            {
                //How long is the scene? (Scenes start at 0!)
                sceneLength = 3;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //Does this scene have a third dialogue option?
                thirdOption = true;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");
                sceneChoice3 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.3");


                //What does the scene change to when you choose the first option?
                choice1Scene = 4;

                //What does the scene change to when you choose the second option?
                choice2Scene = 5;

                //Same for the third scene?
                choice3Scene = 6;

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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 1)
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 3)
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
            }
            //Eridani introduces the Astrolabe
            if (sceneID == 12)
            {
                //How long is the scene? (Scenes start at 0!)
                sceneLength = 3;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //Does this scene have a third dialogue option?
                thirdOption = true;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");
                sceneChoice3 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.3");


                //What does the scene change to when you choose the first option?
                choice1Scene = 4;

                //What does the scene change to when you choose the second option?
                choice2Scene = 5;

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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 1)
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 2)
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
            }
            //The Starfarers introduce Cosmic Voyages part 1.
            if (sceneID == 13)
            {
                //How long is the scene? (Scenes start at 0!)
                sceneLength = 7;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = true;

                //Does this scene have a third dialogue option?
                thirdOption = true;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");
                sceneChoice3 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.3");


                //What does the scene change to when you choose the first option?
                choice1Scene = 14;

                //What does the scene change to when you choose the second option?
                choice2Scene = 15;

                //Same for the third scene?
                choice3Scene = 16;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 1)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 1;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 2)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 1;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 3)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 4;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 4)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 2;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 5)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 1;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 6)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 3;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 7)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 3;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
            }
            //Option 1.
            if (sceneID == 14)
            {
                //Include all scenes
                sceneLength = 3;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = true;

                //What appears in the choice boxes?
                sceneChoice1 =
                 LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 17;

                //What does the scene change to when you choose the second option?
                choice2Scene = 18;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 1;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 1)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 2)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 3)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
            }
            //Option 2.
            if (sceneID == 15)
            {
                //Include all scenes
                sceneLength = 4;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = true;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 17;

                //What does the scene change to when you choose the second option?
                choice2Scene = 18;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 2)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 3)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 4)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
            }
            //Option 3.
            if (sceneID == 16)
            {
                //Include all scenes
                sceneLength = 2;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = true;

                //What appears in the choice boxes?
                sceneChoice1 =
                 LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 17;

                //What does the scene change to when you choose the second option?
                choice2Scene = 18;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 1;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 3;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 2)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                
            }
            //The Starfarers introduce Cosmic Voyages part 2.
            if (sceneID == 17)
            {
                //How long is the scene? (Scenes start at 0!)
                sceneLength = 7;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = true;

                //Does this scene have a third dialogue option?
                thirdOption = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");
                sceneChoice3 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.3");


                //What does the scene change to when you choose the first option?
                choice1Scene = 18;

                //What does the scene change to when you choose the second option?
                choice2Scene = 17;

                //Same for the third scene?
                //choice3Scene = 16;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 1)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 1;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 2)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 1;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 3)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 4;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 4)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 2;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 5)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 1;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 1;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 6)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 3;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 7)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 3;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
            }
            //End of the explaining.
            if (sceneID == 18)
            {
                //Include all scenes
                sceneLength = 0;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                 LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 17;

                //What does the scene change to when you choose the second option?
                choice2Scene = 18;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                

            }
            //Yojimbo's intro dialogue (Asphodene)
            if (sceneID == 19)
            {
                //Include all scenes
                sceneLength = 8;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 17;

                //What does the scene change to when you choose the second option?
                choice2Scene = 18;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";

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
                    name = "Yojimbo";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 1)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 1;

                    //What is their expression?
                    character2Expression = 2;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 2)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 1;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 1;

                    //What is their expression?
                    character2Expression = 1;


                    //Who's name should be in the dialogue box?
                    name = "Yojimbo";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 3)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Yojimbo";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 4)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Yojimbo";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 5)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 6)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Yojimbo";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 7)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 5;


                    //Who's name should be in the dialogue box?
                    name = "Yojimbo";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 8)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Yojimbo";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
            }
            //Yojimbo's intro dialogue (Eridani)
            if (sceneID == 20)
            {
                //Include all scenes
                sceneLength = 8;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 17;

                //What does the scene change to when you choose the second option?
                choice2Scene = 18;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 1)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 2)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 3)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 4)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 5)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 6)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 7)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 8)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
            }

            //Garridine's intro dialogue (Asphodene)
            if (sceneID == 21)
            {
                //Include all scenes
                sceneLength = 7;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = true;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 21;//placeholder

                //What does the scene change to when you choose the second option?
                choice2Scene = 21;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Garridine";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 1;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 2;


                    //Who's name should be in the dialogue box?
                    name = "Garridine";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 1)
                {
                    //Who is the main character?
                    character1 = "Garridine";

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
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 2)
                {
                    //Who is the main character?
                    character1 = "Garridine";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 4;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 3;


                    //Who's name should be in the dialogue box?
                    name = "Garridine";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 3)
                {
                    //Who is the main character?
                    character1 = "Garridine";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 5;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Garridine";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 4)
                {
                    //Who is the main character?
                    character1 = "Garridine";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 3;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Garridine";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 5)
                {
                    //Who is the main character?
                    character1 = "Garridine";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 1;

                    //What is their expression?
                    character2Expression = 4;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 6)
                {
                    //Who is the main character?
                    character1 = "Garridine";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 1;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Garridine";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 7)
                {
                    //Who is the main character?
                    character1 = "Garridine";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Asphodene";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }

            }

            //Garridine's intro dialogue (Eridani)
            if (sceneID == 22)
            {
                //Include all scenes
                sceneLength = 7;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = true;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 21;//placeholder

                //What does the scene change to when you choose the second option?
                choice2Scene = 21;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Garridine";

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
                    name = "Garridine";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 1)
                {
                    //Who is the main character?
                    character1 = "Garridine";

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
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 2)
                {
                    //Who is the main character?
                    character1 = "Garridine";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 4;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 3;


                    //Who's name should be in the dialogue box?
                    name = "Garridine";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 3)
                {
                    //Who is the main character?
                    character1 = "Garridine";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 5;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Garridine";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 4)
                {
                    //Who is the main character?
                    character1 = "Garridine";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 3;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Garridine";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 5)
                {
                    //Who is the main character?
                    character1 = "Garridine";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 1;

                    //What is their expression?
                    character2Expression = 4;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 6)
                {
                    //Who is the main character?
                    character1 = "Garridine";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 1;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Garridine";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 7)
                {
                    //Who is the main character?
                    character1 = "Garridine";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 0;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 0;


                    //Who's name should be in the dialogue box?
                    name = "Eridani";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }

            }

            //Yojimbo talks about the Ardor
            if (sceneID == 100)
            {
                //Include all scenes
                sceneLength = 9;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 17;

                //What does the scene change to when you choose the second option?
                choice2Scene = 18;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 1)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 2)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 3)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 4)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 5)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 6)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 7)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 8)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 9)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }

            }

            //Yojimbo talks about the Empire
            if (sceneID == 101)
            {
                //Include all scenes
                sceneLength = 9;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 17;

                //What does the scene change to when you choose the second option?
                choice2Scene = 18;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 1)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 2)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 3)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 4)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 5)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 6)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 7)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 8)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 9)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 10)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }

            }

            //Yojimbo talks about the galaxy, Moirae. 
            if (sceneID == 102)
            {
                //Include all scenes
                sceneLength = 8;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 17;

                //What does the scene change to when you choose the second option?
                choice2Scene = 18;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 1)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 2)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 3)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 4)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 5)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 6)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 7)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 8)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                

            }

            //Yojimbo talks about the galaxy, Moirae. 
            if (sceneID == 103)
            {
                //Include all scenes
                sceneLength = 0;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 17;

                //What does the scene change to when you choose the second option?
                choice2Scene = 18;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Yojimbo";
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
                    name = "Yojimbo";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                


            }

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
            dialogue,
            thirdOption,
            sceneChoice3,
            choice3Scene
            };
        }


        



    }

    

}