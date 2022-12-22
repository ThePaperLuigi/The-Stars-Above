
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI.VN
{
    internal class VN : UIState
	{
		
		private UIText text;
		private UIText speakerName;

		private UIText choice1Text;
		private UIText choice2Text;
		private UIText choice3Text;

		private UIElement area;
		private UIElement character1;
		private UIElement character2;

		private UIImage barFrame;
		
		private UIImageButton imageButton;

		private UIImageButton dialogueOption1;
		private UIImageButton dialogueOption2;
		private UIImageButton dialogueOption3;

		private Vector2 offset;

		public bool dragging = false;
		public static bool Draggable;
		public override void OnInitialize() {//512 32
			
			

			area = new UIElement(); 
			area.Left.Set(0, 0f);
			area.Top.Set(0, 0f); 
			area.Width.Set(800, 0f); 
			area.Height.Set(600, 0f);
			area.HAlign = area.VAlign = 0.5f; // 1
			//area.IgnoresMouseInteraction = true;

			

			character1 = new UIElement();
			character1.Left.Set(200, 0f);
			character1.Top.Set(45, 0f);
			character1.Width.Set(400, 0f);
			character1.Height.Set(400, 0f);
			character1.IgnoresMouseInteraction = true;

			character2 = new UIElement();
			character2.Left.Set(350, 0f);
			character2.Top.Set(45, 0f);
			character2.Width.Set(400, 0f);
			character2.Height.Set(400, 0f);
			character2.IgnoresMouseInteraction = true;
			//character1.HAlign = area.VAlign = 0.2f; // 1

			text = new UIText("", 1.2f);
			text.Width.Set(0, 0f);
			text.Height.Set(155, 0f);
			text.Top.Set(396, 0f);
			text.Left.Set(150, 0f);
			text.IgnoresMouseInteraction = true;

			speakerName = new UIText("", 1f);
			speakerName.Width.Set(0, 0f);
			speakerName.Height.Set(20, 0f);
            speakerName.Top.Set(364, 0f);
			speakerName.Left.Set(150, 0f);

			choice1Text = new UIText("", 1f);
			choice1Text.Width.Set(456, 0f);
			choice1Text.Height.Set(1, 0f);
			choice1Text.Top.Set(240, 0f);
			choice1Text.Left.Set(173, 0f);
			choice1Text.IgnoresMouseInteraction = true;

			choice2Text = new UIText("", 1f);
			choice2Text.Width.Set(456, 0f);
			choice2Text.Height.Set(1, 0f);
			choice2Text.Top.Set(291, 0f);
			choice2Text.Left.Set(173, 0f);
			choice2Text.IgnoresMouseInteraction = true;

			choice3Text = new UIText("", 1f);
			choice3Text.Width.Set(456, 0f);
			choice3Text.Height.Set(1, 0f);
			choice3Text.Top.Set(341, 0f);
			choice3Text.Left.Set(173, 0f);
			choice3Text.IgnoresMouseInteraction = true;


			barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/blank"));
			barFrame.Left.Set(22, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(138, 0f);
			barFrame.Height.Set(34, 0f);
			barFrame.IgnoresMouseInteraction = true;

			imageButton = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Button"));
			imageButton.OnClick += MouseClickA;
			imageButton.Left.Set(600, 0f);
			imageButton.Top.Set(554, 0f);
			imageButton.Width.Set(80, 0f);
			imageButton.Height.Set(32, 0f);

			dialogueOption1 = new UIImageButton(Request<Texture2D>("StarsAbove/UI/VN/choiceButton"));
			dialogueOption1.OnClick += DialogueOption1Click;
			dialogueOption1.Left.Set(144, 0f);
			dialogueOption1.Top.Set(232, 0f);
			dialogueOption1.Width.Set(512, 0f);
			dialogueOption1.Height.Set(32, 0f);

			dialogueOption2 = new UIImageButton(Request<Texture2D>("StarsAbove/UI/VN/choiceButton"));
			dialogueOption2.OnClick += DialogueOption2Click;
			dialogueOption2.Left.Set(144, 0f);
			dialogueOption2.Top.Set(282, 0f);
			dialogueOption2.Width.Set(512, 0f);
			dialogueOption2.Height.Set(32, 0f);

			dialogueOption3 = new UIImageButton(Request<Texture2D>("StarsAbove/UI/VN/choiceButton"));
			dialogueOption3.OnClick += DialogueOption3Click;
			dialogueOption3.Left.Set(144, 0f);
			dialogueOption3.Top.Set(332, 0f);
			dialogueOption3.Width.Set(512, 0f);
			dialogueOption3.Height.Set(32, 0f);

			area.Append(text);
			area.Append(speakerName);
			
			area.Append(barFrame);
			area.Append(imageButton);

			
			area.Append(character1);
			area.Append(character2);

			area.Append(dialogueOption1);
			area.Append(choice1Text);
			area.Append(dialogueOption2);
			area.Append(choice2Text);
			Append(area);

			
		}
		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerVNDialogueVisibility >= 1f))
				return;
			if (!Draggable)
			{
				offset = new Vector2(evt.MousePosition.X - area.Left.Pixels, evt.MousePosition.Y - area.Top.Pixels);
				dragging = true;
			}
		}

		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerVNDialogueVisibility >= 1f))
				return;
			if (!Draggable)
			{
				Vector2 end = evt.MousePosition;
				dragging = false;

				area.Left.Set(end.X - offset.X, 0f);
				area.Top.Set(end.Y - offset.Y, 0f);

				Recalculate();
			}
		}
		private void MouseClickA(UIMouseEvent evt, UIElement listeningElement)//Clicking the progress button.
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerVNDialogueVisibility >= 1f))
				return;

			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialogueScrollNumber < Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialogue.Length)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialogueScrollNumber = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialogue.Length;
			}
			else
            {
				if ((bool)VNScenes.SetupVNSystem(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneID, Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneProgression)[1] && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneProgression + 1 > Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneLength)//Once they've finished the dialogue, pop up the options.
				{

					if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialogueScrollNumber >= Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialogue.Length)
					{
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().VNDialogueChoice1 = (string)VNScenes.SetupVNSystem(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneID, Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneProgression)[2];
						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().VNDialogueChoice2 = (string)VNScenes.SetupVNSystem(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneID, Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneProgression)[3];

						Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().VNDialogueChoiceActive = true;
					}

				}
				else
                {
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneProgression++;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialogueScrollTimer = 0;
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialogueScrollNumber = 0;

				}

				

				
			}
			




			// We can do stuff in here!
		}
		private void DialogueOption1Click(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().VNDialogueChoiceActive))
				return;
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			modPlayer.dialogueScrollTimer = 0;
			modPlayer.dialogueScrollNumber = 0;
			modPlayer.sceneID = (int)VNScenes.SetupVNSystem(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneID, Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneProgression)[4];
			modPlayer.sceneProgression = 0;
			
			
			modPlayer.VNDialogueActive = true;
			modPlayer.VNDialogueChoiceActive = false;

		}
		private void DialogueOption2Click(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().VNDialogueChoiceActive))
				return;
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			modPlayer.dialogueScrollTimer = 0;
			modPlayer.dialogueScrollNumber = 0;
			modPlayer.sceneID = (int)VNScenes.SetupVNSystem(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneID, Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneProgression)[5];
			modPlayer.sceneProgression = 0;
			
			
			modPlayer.VNDialogueActive = true;
			modPlayer.VNDialogueChoiceActive = false;


		}

		private void DialogueOption3Click(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().VNDialogueChoiceActive))
				return;
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			modPlayer.dialogueScrollTimer = 0;
			modPlayer.dialogueScrollNumber = 0;
			modPlayer.sceneID = (int)VNScenes.SetupVNSystem(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneID, Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().sceneProgression)[16];
			modPlayer.sceneProgression = 0;


			modPlayer.VNDialogueActive = true;
			modPlayer.VNDialogueChoiceActive = false;


		}

		public override void Draw(SpriteBatch spriteBatch) {
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerVNDialogueVisibility > 0))
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			// Calculate quotient
			
			Rectangle hitbox= area.GetInnerDimensions().ToRectangle();//

			Rectangle hitbox1 = character1.GetInnerDimensions().ToRectangle();//
			Rectangle hitbox2 = character2.GetInnerDimensions().ToRectangle();//

			if(modPlayer.sceneID == -1)
            {
				return;
            }

			if (modPlayer.VNCharacter2 != "None")
            {
				hitbox1.X -= 150; //Originally, Hitbox1 is in the middle, but moves to the left if there is a Character2. Maybe one day you could Lerp this, but it doesn't really matter.

			}
			
			if (modPlayer.VNCharacter1 != "None")
			{
				//The layout for character body blanks is [First 2 characters of name][Current Pose]['Body'][Current Costume]
				//For expressions and head: [First 2 characters of name][Current pose][Expression Number / 'Head' if applicable]
				//For hair: [First 2 characters of name][Current pose]['Hair' / 'HairB' for hair behind body]['H' for Post Vagrant Hair if applicable]
				//Example of completed version:

				//Hair Back:: As0HairB | Alternate: As1HairBH
				//Head:: As0Head :: As0Head.png
				//Character Body Blanks:: AsB00 :: AsB00.png | Alternate:: AsB12 :: AsB12.png
				//Hair Front:: As0Hair | Alternate: As1HairH
				//Expressions:: As00 :: As00.png | Alternate:: As13 :: As13.png


				if (modPlayer.VNCharacter1 == "Asphodene" || modPlayer.VNCharacter1 == "Eridani")//Main characters get extra drawing.
				{
					//Draw the hair behind the body and head. Remember to take account of pre/post Vagrant color change.
					if (DownedBossSystem.downedVagrant)
					{
						spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter1.Substring(0, 2) + modPlayer.VNCharacter1Pose + "HairBH"), hitbox1, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker)));

					}
					else
					{
						spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter1.Substring(0, 2) + modPlayer.VNCharacter1Pose + "HairB"), hitbox1, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker)));

					}
					//Draw the head, accounting for pose.
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter1.Substring(0, 2) + modPlayer.VNCharacter1Pose + "Head"), hitbox1, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker)));

					//Draw the body, accounting for outfits and pose.
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter1.Substring(0, 2) + modPlayer.VNCharacter1Pose + "Body" + modPlayer.starfarerOutfitVisible), hitbox1, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker)));

					//Draw the hair on top of the head. Same deal with color change.
					if (DownedBossSystem.downedVagrant)
					{
						spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter1.Substring(0, 2) + modPlayer.VNCharacter1Pose + "HairH"), hitbox1, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker)));

					}
					else
					{
						spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter1.Substring(0, 2) + modPlayer.VNCharacter1Pose + "Hair"), hitbox1, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker)));

					}
					
					//Draw the expression, accounting for pose.
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter1.Substring(0, 2) + modPlayer.VNCharacter1Pose + modPlayer.VNCharacter1Expression), hitbox1, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker)));//Base character's expression
				}
				if(modPlayer.VNCharacter1 != "Asphodene" && modPlayer.VNCharacter1 != "Eridani")//Non-main characters get simplified drawing.
					//Input: Perseus, Pose 1, Expression 1
					//Output:
					//StarsAbove/UI/VN/Pe1 (2 letters to avoid conflicts)
					//StarsAbove/UI/VN/Pe11 (Expression takes into account current pose, remember expressions start at 0)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter1.Substring(0, 2) + modPlayer.VNCharacter1Pose), hitbox1, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker)));
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter1.Substring(0, 2) + modPlayer.VNCharacter1Pose + modPlayer.VNCharacter1Expression), hitbox1, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker)));
				}
			}
			if(modPlayer.VNCharacter1 == "Asphodene" || modPlayer.VNCharacter1 == "Eridani")//Only the main characters can blink
            {
				if(modPlayer.VNCharacter1 == "Eridani" && (modPlayer.VNCharacter1Expression == 5 || modPlayer.VNCharacter1Expression == 4))
                {

                }
				else
                {
					if ((modPlayer.blinkTimer > 70 && modPlayer.blinkTimer < 75) 
						|| (modPlayer.blinkTimer > 320 && modPlayer.blinkTimer < 325) 
						|| (modPlayer.blinkTimer > 420 && modPlayer.blinkTimer < 425) 
						|| (modPlayer.blinkTimer > 428 && modPlayer.blinkTimer < 433))
					{
						spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter1.Substring(0, 2) + modPlayer.VNCharacter1Pose + "b"), hitbox1,
							Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker)));
					}
				}
				


			}

			//example:
			//Input: Asphodene, Pose 1, Expression 1
			//Output:
			//StarsAbove/UI/VN/As1 (2 letters to avoid conflicts)
			//StarsAbove/UI/VN/As11 (Expression takes into account current pose, remember expressions start at 0)
			//StarsAbove/UI/VN/As1b (Blinking sprite)
			//
			if (modPlayer.VNCharacter2 != "None")//Character 2
            {
				if (modPlayer.VNCharacter2 == "Asphodene" || modPlayer.VNCharacter2 == "Eridani")//Main characters get extra drawing.
				{
					//Draw the hair behind the body and head. Remember to take account of pre/post Vagrant color change.
					if (DownedBossSystem.downedVagrant)
					{
						spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter2.Substring(0, 2) + modPlayer.VNCharacter2Pose + "HairBH"), hitbox2, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker2)));

					}
					else
					{
						spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter2.Substring(0, 2) + modPlayer.VNCharacter2Pose + "HairB"), hitbox2, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker2)));

					}
					//Draw the head, accounting for pose.
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter2.Substring(0, 2) + modPlayer.VNCharacter2Pose + "Head"), hitbox2, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker2)));

					//Draw the body, accounting for outfits and pose.
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter2.Substring(0, 2) + modPlayer.VNCharacter2Pose + "Body" + modPlayer.starfarerOutfitVisible), hitbox2, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker2)));


					//Draw the hair on top of the head. Same deal with color change.
					if (DownedBossSystem.downedVagrant)
					{
						spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter2.Substring(0, 2) + modPlayer.VNCharacter2Pose + "HairH"), hitbox2, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker2)));

					}
					else
					{
						spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter2.Substring(0, 2) + modPlayer.VNCharacter2Pose + "Hair"), hitbox2, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker2)));

					}
					
					//Draw the expression, accounting for pose.
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter2.Substring(0, 2) + modPlayer.VNCharacter2Pose + modPlayer.VNCharacter2Expression), hitbox2, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker2)));//Base character's expression
				}
				if (modPlayer.VNCharacter2 != "Asphodene" && modPlayer.VNCharacter2 != "Eridani")//Non-main characters get simplified drawing.
																								 //Input: Perseus, Pose 1, Expression 1
																								 //Output:
																								 //StarsAbove/UI/VN/Pe1 (2 letters to avoid conflicts)
																								 //StarsAbove/UI/VN/Pe11 (Expression takes into account current pose, remember expressions start at 0)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter2.Substring(0, 2) + modPlayer.VNCharacter2Pose), hitbox2, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker2)));
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter2.Substring(0, 2) + modPlayer.VNCharacter2Pose + modPlayer.VNCharacter2Expression), hitbox2, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker2)));
				}

				if (modPlayer.VNCharacter2 == "Asphodene" || modPlayer.VNCharacter2 == "Eridani")//Only the main characters can blink
				{
					if (modPlayer.VNCharacter2 == "Eridani" && (modPlayer.VNCharacter2Expression == 5 || modPlayer.VNCharacter2Expression == 4))
					{

					}
					else
					{
						if ((modPlayer.blinkTimer > 120 && modPlayer.blinkTimer < 125) || (modPlayer.blinkTimer > 230 && modPlayer.blinkTimer < 235) || (modPlayer.blinkTimer > 400 && modPlayer.blinkTimer < 405))//Offset the timer so they don't blink at the same time!
						{
							spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/" + modPlayer.VNCharacter2.Substring(0, 2) + modPlayer.VNCharacter2Pose + "b"), hitbox2, Color.White * (modPlayer.starfarerVNDialogueVisibility - (0.2f + modPlayer.MainSpeaker2)));
						}
					}

				}
			}
			
			//example 2:
			//Input: Eridani, Pose 0, Expression 4
			//Output:
			//StarsAbove/UI/VN/Er0 (2 letters to avoid conflicts)
			//StarsAbove/UI/VN/Er04 (Expression takes into account current pose)
			//StarsAbove/UI/VN/Er0b (Blinking sprite adds B, only needs pose to place the blink)
			//


			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/Dialogue"), hitbox, Color.White * modPlayer.starfarerVNDialogueVisibility);//The actual dialogue window. Note the need for a spot for the name of the speaker.

			//Additionally, for a little bit of extra flair, if the modPlayer.VNCharacter1 or modPlayer.VNCharacter2 name corresponds to the modPlayer.VNDialogueVisibleName,
			//it will fade in the character, and fade out the other character. If VNCharacter2 is None, this doesn't happen (This is done for now, but needs testing.)


			//In the event that the dialogue prompts choices, we need to
			//1. hide the normal progress dialogue button
			//2. fade in the two buttons in the middle
			//3. change the sceneID and sceneProgress depending on the choice you made


			Recalculate();


		}
			
		
		public override void Update(GameTime gameTime) {
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerVNDialogueVisibility > 0))
			{
				area.Remove();
				return;

			}
			else
			{
				Append(area);
			}
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().VNDialogueChoiceActive))
			{
				character1.Remove();
				character2.Remove();
				dialogueOption1.Remove();
				choice1Text.Remove();
				choice2Text.Remove();
				dialogueOption2.Remove();
				choice3Text.Remove();
				dialogueOption3.Remove();

				area.Append(imageButton);
				

			}
			else
			{
				imageButton.Remove();
				area.Append(character1);
				area.Append(character2);
				area.Append(dialogueOption1);
				area.Append(choice1Text);
				area.Append(dialogueOption2);
				area.Append(choice2Text);
				if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().VNDialogueThirdOption)
				{
					area.Append(choice3Text);
					area.Append(dialogueOption3);
					choice3Text.SetText($"{Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().VNDialogueChoice3}");
				}
				
			}
			
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

			if (modPlayer.VNCharacter1 == modPlayer.VNDialogueVisibleName)
			{
				if (modPlayer.MainSpeaker > -0.8f)
				{
					modPlayer.MainSpeaker += -0.05f;
				}

			}
			else
			{
				if (modPlayer.MainSpeaker < 0f)
				{
					modPlayer.MainSpeaker += 0.05f;
				}
			}
			if (modPlayer.VNCharacter2 == modPlayer.VNDialogueVisibleName)
			{
				if (modPlayer.MainSpeaker2 > -0.8f)
				{
					modPlayer.MainSpeaker2 += -0.05f;
				}

			}
			else
			{
				if (modPlayer.MainSpeaker2 < 0f)
				{
					modPlayer.MainSpeaker2 += 0.05f;
				}

			}
			// Setting the text per tick to update and show dialogue and speaker
			text.SetText($"{modPlayer.animatedDialogue}");
			speakerName.SetText($"{modPlayer.VNDialogueVisibleName}");

			choice1Text.SetText($"{modPlayer.VNDialogueChoice1}");
			choice2Text.SetText($"{modPlayer.VNDialogueChoice2}");
			if(modPlayer.VNDialogueThirdOption)
            {
				
				choice3Text.SetText($"{modPlayer.VNDialogueChoice3}");
			}
            else
            {
				
            }


			base.Update(gameTime);
		}
	}
}
