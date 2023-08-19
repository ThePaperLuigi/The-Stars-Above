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

            //Intro Scene
            if (sceneID == 0)
            {
                //How long is the scene? (Scenes start at 0!)
                sceneLength = 0;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = true;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 2;

                //What does the scene change to when you choose the second option?
                choice2Scene = 1;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "None";

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
                    name = "???";

                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress);
                }

            }
            if (sceneID == 1)
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
                    character1 = "None";

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
                    name = "???";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
            }
            //World sync scene
            if (sceneID == 2)
            {
                //How long is the scene? (Scenes start at 0!)
                sceneLength = 0;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = true;

                ///Does this scene have a third dialogue option?
                thirdOption = true;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");
                sceneChoice3 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.3");

                //What does the scene change to when you choose the first option?
                choice1Scene = 1;

                //What does the scene change to when you choose the second option?
                choice2Scene = 1;

                //What does the scene change to when you choose the second option?
                choice3Scene = 1;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "None";

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
                    name = "???";

                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
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
                sceneLength = 15;

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
                if (sceneProgress == 8)
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
                if (sceneProgress == 9)
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
                if (sceneProgress == 10)
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
                if (sceneProgress == 11)
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
                    character2Expression = 1;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 12)
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
                if (sceneProgress == 13)
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
                if (sceneProgress == 14)
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
                if (sceneProgress == 15)
                {
                    //Who is the main character?
                    character1 = "Asphodene";

                    //What is their pose?
                    character1Pose = 0;

                    //What is their expression? (0 Neutral 1 Angry 2 Worried 3 Thinking 4 Intrigued/Smug 5 Happy)
                    character1Expression = 5;


                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "Eridani";

                    //What is their pose?
                    character2Pose = 0;

                    //What is their expression?
                    character2Expression = 5;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


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
                sceneLength = 10;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

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
                    name = "Garridine";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 8)
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
                    name = "Garridine";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 9)
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
                    character2Expression = 5;


                    //Who's name should be in the dialogue box?
                    name = "Asphodene";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 10)
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
                    character2Expression = 4;


                    //Who's name should be in the dialogue box?
                    name = "Garridine";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }

            }

            //Garridine's intro dialogue (Eridani)
            if (sceneID == 22)
            {
                //Include all scenes
                sceneLength = 10;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

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
                    name = "Garridine";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 8)
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
                    name = "Garridine";


                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }
                if (sceneProgress == 9)
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
                if (sceneProgress == 10)
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
                    character2Expression = 4;


                    //Who's name should be in the dialogue box?
                    name = "Garridine";


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

            //Yojimbo talks about...?
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


            //Garridine's quests. Includes 

            // Quest complete
            if (sceneID == 199)
            {
                //Include all scenes
                sceneLength = 1;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Garridine";
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
                    character2 = "None";
                    //What is their pose?
                    character2Pose = 0;
                    //What is their expression?
                    character2Expression = 0;
                    //Who's name should be in the dialogue box?
                    name = "Garridine";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }


            }

            // Quest complete
            if (sceneID == 200)
            {
                //Include all scenes
                sceneLength = 1;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Garridine";
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
                    character1Expression = 4;
                    //Who is the sub character? If there is no second character, write "None";
                    character2 = "None";
                    //What is their pose?
                    character2Pose = 0;
                    //What is their expression?
                    character2Expression = 0;
                    //Who's name should be in the dialogue box?
                    name = "Garridine";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }


            }

            // Quest #1, Waterbolt
            if (sceneID == 201)
            {
                //Include all scenes
                sceneLength = 1;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Garridine";
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
                    character2 = "None";
                    //What is their pose?
                    character2Pose = 0;
                    //What is their expression?
                    character2Expression = 0;
                    //Who's name should be in the dialogue box?
                    name = "Garridine";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }


            }

            // Quest #2, Kevesi or Agnian Farewell
            if (sceneID == 202)
            {
                //Include all scenes
                sceneLength = 1;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Garridine";
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
                    character2 = "None";
                    //What is their pose?
                    character2Pose = 0;
                    //What is their expression?
                    character2Expression = 0;
                    //Who's name should be in the dialogue box?
                    name = "Garridine";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }


            }

            // Quest #3, Golden Butterfly
            if (sceneID == 203)
            {
                //Include all scenes
                sceneLength = 1;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Garridine";
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
                    character2 = "None";
                    //What is their pose?
                    character2Pose = 0;
                    //What is their expression?
                    character2Expression = 0;
                    //Who's name should be in the dialogue box?
                    name = "Garridine";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }


            }

            // Quest #4, Flying Carpet
            if (sceneID == 204)
            {
                //Include all scenes
                sceneLength = 1;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Garridine";
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
                    character2 = "None";
                    //What is their pose?
                    character2Pose = 0;
                    //What is their expression?
                    character2Expression = 0;
                    //Who's name should be in the dialogue box?
                    name = "Garridine";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }


            }

            // Quest #5, Lava Charm
            if (sceneID == 205)
            {
                //Include all scenes
                sceneLength = 1;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Garridine";
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
                    character2 = "None";
                    //What is their pose?
                    character2Pose = 0;
                    //What is their expression?
                    character2Expression = 0;
                    //Who's name should be in the dialogue box?
                    name = "Garridine";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }


            }

            // Quest #6, Lifeform Analyzer
            if (sceneID == 206)
            {
                //Include all scenes
                sceneLength = 1;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Garridine";
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
                    character2 = "None";
                    //What is their pose?
                    character2Pose = 0;
                    //What is their expression?
                    character2Expression = 0;
                    //Who's name should be in the dialogue box?
                    name = "Garridine";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }


            }

            // Quest #7, Mechanical Lens
            if (sceneID == 207)
            {
                //Include all scenes
                sceneLength = 1;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Garridine";
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
                    character2 = "None";
                    //What is their pose?
                    character2Pose = 0;
                    //What is their expression?
                    character2Expression = 0;
                    //Who's name should be in the dialogue box?
                    name = "Garridine";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }


            }

            // Quest #8, Mana Flower
            if (sceneID == 208)
            {
                //Include all scenes
                sceneLength = 1;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Garridine";
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
                    character2 = "None";
                    //What is their pose?
                    character2Pose = 0;
                    //What is their expression?
                    character2Expression = 0;
                    //Who's name should be in the dialogue box?
                    name = "Garridine";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }


            }

            // Quest #9, Beozar
            if (sceneID == 209)
            {
                //Include all scenes
                sceneLength = 1;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Garridine";
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
                    character2 = "None";
                    //What is their pose?
                    character2Pose = 0;
                    //What is their expression?
                    character2Expression = 0;
                    //Who's name should be in the dialogue box?
                    name = "Garridine";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }


            }

            // Quest #10, Umbrella
            if (sceneID == 210)
            {
                //Include all scenes
                sceneLength = 1;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Garridine";
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
                    character2 = "None";
                    //What is their pose?
                    character2Pose = 0;
                    //What is their expression?
                    character2Expression = 0;
                    //Who's name should be in the dialogue box?
                    name = "Garridine";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }


            }

            // Quest #11, Acutator
            if (sceneID == 211)
            {
                //Include all scenes
                sceneLength = 1;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Garridine";
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
                    character2 = "None";
                    //What is their pose?
                    character2Pose = 0;
                    //What is their expression?
                    character2Expression = 0;
                    //Who's name should be in the dialogue box?
                    name = "Garridine";
                    //What is the dialogue?
                    dialogue = LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Dialogue." + sceneProgress, Main.LocalPlayer.name);
                }


            }

            // Quest #12, Bone Torch
            if (sceneID == 212)
            {
                //Include all scenes
                sceneLength = 1;

                //Does this scene have a dialouge choice at the end of it?
                sceneHasChoice = false;

                //What appears in the choice boxes?
                sceneChoice1 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.1");
                sceneChoice2 =
                  LangHelper.GetTextValue($"Dialogue.VNDialogue.SceneID." + sceneID + ".Choices.2");

                //What does the scene change to when you choose the first option?
                choice1Scene = 0;

                //What does the scene change to when you choose the second option?
                choice2Scene = 0;

                if (sceneProgress == 0)
                {
                    //Who is the main character?
                    character1 = "Garridine";
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
                    character2 = "None";
                    //What is their pose?
                    character2Pose = 0;
                    //What is their expression?
                    character2Expression = 0;
                    //Who's name should be in the dialogue box?
                    name = "Garridine";
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