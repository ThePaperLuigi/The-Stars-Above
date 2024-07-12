
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Systems;
using StarsAbove.Systems;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI.Starfarers
{
    internal class StarfarerText : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
		private UIText text;
		private UIText warning;
		
		private UIText AText;
		private UIText EridaniText;
		private UIElement area;
		private UIElement area2;
		private UIElement area3;
		private UIImage barFrame;
		private UIImage bg;
		private UIImageButton imageButton;
		private UIImage bulletIndicatorOn;
		private Color gradientA;
		private Color gradientB;
		private Color finalColor;
		private Vector2 offset;
		public bool dragging = false;
		public static bool Draggable;
        static public float AdjustmentFactor = 1.5f;

        public static bool voicesDisabled;

        private UIElement face;
		public override void OnInitialize() {
			

			area = new UIElement(); 
			area.Left.Set(-25, 0f);
			area.Top.Set(160, 0f); 
			area.Width.Set(700, 0f); 
			area.Height.Set(300, 0f);
			area.HAlign = area.VAlign = 0.5f; // 1

			face = new UIElement();
			face.Width.Set(400, 0f);
			face.Height.Set(400, 0f);

			text = new UIText("", 1.2f);
			text.Width.Set(0, 0f);
			text.Height.Set(155, 0f);
			text.Top.Set(73, 0f);
			text.Left.Set(200, 0f);


			area.OnLeftMouseDown += new UIElement.MouseEvent(DragStart);
			area.OnLeftMouseUp += new UIElement.MouseEvent(DragEnd);

			

			barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/blank"));
			barFrame.Left.Set(22, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(138, 0f);
			barFrame.Height.Set(34, 0f);

			imageButton = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Button"));
			imageButton.OnLeftClick += MouseClickA;
			imageButton.Left.Set(600, 0f);
			imageButton.Top.Set(222, 0f);
			imageButton.Width.Set(70, 0f);
			imageButton.Height.Set(52, 0f);
			/*Asphodene = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/Eridani"));
			Asphodene.OnMouseOver += MouseOverA;
			Asphodene.OnClick += MouseClickA;
			Asphodene.Top.Set(0, 0f);
			Asphodene.Left.Set(0, 0f);
			Asphodene.Width.Set(0, 0f);
			Asphodene.Height.Set(0, 0f);*/





			gradientA = new Color(249, 133, 36); // 
			gradientB = new Color(255, 166, 83); //
			//area3.Append(Asphodene);
			
			area.Append(text);
			area.Append(face);
			area.Append(barFrame);
			area.Append(imageButton);
			Append(area);

			
		}
		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			/*if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogueVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue == true) && Draggable)
				return;
			if (!Draggable)
			{
				offset = new Vector2(evt.MousePosition.X - area.Left.Pixels, evt.MousePosition.Y - area.Top.Pixels);
				dragging = true;
			}*/
		}

		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			/*if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogueVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue == true) && Draggable)
				return;
			if (!Draggable)
			{
				Vector2 end = evt.MousePosition;
				dragging = false;

				area.Left.Set(end.X - offset.X, 0f);
				area.Top.Set(end.Y - offset.Y, 0f);

				Recalculate();
			}*/
		}
		private void MouseClickA(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogueVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue == true))
				return;

			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();


            if (modPlayer.dialogueScrollNumber < modPlayer.dialogue.Length)
			{
				modPlayer.dialogueScrollNumber = modPlayer.dialogue.Length;
			}
			else
            {

                modPlayer.dialogueLeft++;

				modPlayer.dialogueScrollTimer = 0;
				modPlayer.dialogueScrollNumber = 0;

				if (modPlayer.dialogueFinished) //If the dialogue ends, the next click will end the dialogue.
				{
                    if(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().globalVoiceDelayTimer < 0 && modPlayer.expression < 8)
                    {
                        if (Main.rand.NextBool())
                        {
                            Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().globalVoiceDelayTimer = StarsAbovePlayer.globalVoiceDelayMax * 60;

                            if (modPlayer.chosenStarfarer == 1)
                            {
                                if (Main.rand.NextBool())
                                {
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneDialogueEnd0);}

                                }
                                else
                                {
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneDialogueEnd1);}

                                }
                            }
                            else if (modPlayer.chosenStarfarer == 2)
                            {
                                if (Main.rand.NextBool())
                                {
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniDialogueEnd0);}

                                }
                                else
                                {
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniDialogueEnd1);}

                                }
                            }
                        }
                    }
                    

					modPlayer.starfarerDialogue = false;
					modPlayer.chosenDialogue = 0;
					modPlayer.dialogue = "";
					modPlayer.dialogueFinished = false;

				}
                else
                {
                    DialogueVoice(modPlayer.chosenStarfarer, modPlayer.expression);

                }
            }
			




			// We can do stuff in here!
		}
        private static void DialogueVoice(int chosenStarfarer, int expression)
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

                    int randomA = Main.rand.Next(0, 2);
                    int randomB = Main.rand.Next(0, 3);

                    switch (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().nextExpression)
                    {
                        case 0:

                            break;
                        case 1:
                            switch (randomA)
                            {
                                case 0:
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneAngry0);}

                                    break;

                                case 1:
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneAngry1);}

                                    break;
                            }
                            break;
                        case 2:
                            if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneWorried0);}

                            break;

                        case 3:
                            switch (randomB)
                            {
                                case 0:
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneThinking0);}

                                    break;

                                case 1:
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneThinking1);}

                                    break;
                                case 2:
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneThinking1);}

                                    break;
                            }
                            break;

                        case 4:
                            switch (randomA)
                            {
                                case 0:
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneSmug0);}

                                    break;

                                case 1:
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneSmug1);}

                                    break;
                            }
                            break;

                        case 5:
                            switch (randomA)
                            {
                                case 0:
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneHappy0);}

                                    break;

                                case 1:
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneHappy1);}

                                    break;
                            }
                            break;

                        case 6:

                            if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.AsphodeneDeadInside0);}

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
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniAngry0);}

                                    break;

                                case 1:
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniAngry1);}

                                    break;
                            }
                            break;
                        case 2:
                            if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniWorried0);}

                            break;

                        case 3:
                            switch (randomB)
                            {
                                case 0:
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniThinking0);}

                                    break;

                                case 1:
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniThinking1);}

                                    break;
                                case 2:
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniThinking1);}

                                    break;
                            }
                            break;

                        case 4:
                            switch (randomA)
                            {
                                case 0:
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniSmug0);}

                                    break;

                                case 1:
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniSmug1);}

                                    break;
                            }
                            break;

                        case 5:
                            switch (randomA)
                            {
                                case 0:
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniHappy0);}

                                    break;

                                case 1:
                                    if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniHappy1);}

                                    break;
                            }
                            break;

                        case 6:

                            if (!voicesDisabled){SoundEngine.PlaySound(StarsAboveAudio.EridaniDeadInside0);}

                            break;

                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
			// This prevents drawing unless we are using an ExampleDamageItem
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogueVisibility > 0))
				return;

			base.Draw(spriteBatch);
		}
		float moveRightAdjustment = 0f;
		float moveLeftAdjustment = 0f;
		float moveUpAdjustment = 0f;
		float moveDownAdjustment = 0f;
		protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
            // Calculate quotient
            int mouseAdjustmentX = (int)MathHelper.Lerp(-3, 3, Main.LocalPlayer.GetModPlayer<StarfarerMenuAnimation>().MousePositionFloatX);
            int mouseAdjustmentY = (int)MathHelper.Lerp(-3, 3, Main.LocalPlayer.GetModPlayer<StarfarerMenuAnimation>().MousePositionFloatY);

            int moveVar = 30;
            Rectangle staticHitbox = MovementAnimation(Main.LocalPlayer.velocity.X, Main.LocalPlayer.velocity.Y);

            Rectangle hitbox = area.GetInnerDimensions().ToRectangle();
            hitbox.X += mouseAdjustmentX;
            hitbox.Y += mouseAdjustmentY;

            Rectangle faceHitbox = face.GetInnerDimensions().ToRectangle();
            face.Left.Set(-102, 0f);
            face.Top.Set(-8, 0f);

            Rectangle faceHitboxAlt = face.GetInnerDimensions().ToRectangle();
            faceHitboxAlt.X += 18;
            faceHitboxAlt.Y += 8;
            faceHitboxAlt.Height = 400;
            faceHitboxAlt.X += mouseAdjustmentX;
            faceHitboxAlt.Y += mouseAdjustmentY;

            faceHitbox.Height = 400;
            faceHitbox.X += mouseAdjustmentX;
            faceHitbox.Y += mouseAdjustmentY;
            //Rectangle indicator = new Rectangle((600), (280), (700), (440));
            //indicator.X += 0;
            //indicator.Width -= 0;
            //indicator.Y += 0;
            //indicator.Height -= 0;

            //Rectangle dialogueBox = new Rectangle((50), (480), (700), (300));


            spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/Dialogue"), staticHitbox, Color.White * modPlayer.starfarerDialogueVisibility);
            if (modPlayer.expression >= 10)
            {
                spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/DialoguePortraits/EX" + modPlayer.expression), hitbox, Color.White * (modPlayer.starfarerDialogueVisibility - 0.3f));

            }
            else
            {
                if (modPlayer.chosenStarfarer == 1)
                {
					
                    if (modPlayer.vagrantDialogue == 2)
                    {
                        if (modPlayer.starfarerHairstyle == 1)
                        {
                            spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/DialoguePortraits/HABaseAlt1"), hitbox, Color.White * (modPlayer.starfarerDialogueVisibility - 0.3f));

                        }
                        else
                        {
                            spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/DialoguePortraits/HABase"), hitbox, Color.White * (modPlayer.starfarerDialogueVisibility - 0.3f));

                        }
                    }
                    else
                    {
                        if (modPlayer.starfarerHairstyle == 1)
                        {
                            spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/DialoguePortraits/ABaseAlt1"), hitbox, Color.White * (modPlayer.starfarerDialogueVisibility - 0.3f));

                        }
                        else
                        {
                            spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/DialoguePortraits/ABase"), hitbox, Color.White * (modPlayer.starfarerDialogueVisibility - 0.3f));

                        }

                    }
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/As1" + modPlayer.expression), faceHitbox, Color.White * (modPlayer.starfarerDialogueVisibility - 0.3f));

                    if (modPlayer.expression < 10)
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/DialoguePortraits/AOutfit" + modPlayer.starfarerOutfitVisible), hitbox, Color.White * (modPlayer.starfarerDialogueVisibility - 0.3f));

                    }
                    if (modPlayer.expression == 0 || modPlayer.expression == 1 || modPlayer.expression == 2 || modPlayer.expression == 3 || modPlayer.expression == 4 || modPlayer.expression == 6)
                    {
                        if ((modPlayer.blinkTimer > 70 && modPlayer.blinkTimer < 75) || (modPlayer.blinkTimer > 320 && modPlayer.blinkTimer < 325) || (modPlayer.blinkTimer > 420 && modPlayer.blinkTimer < 425) || modPlayer.blinkTimer > 428 && modPlayer.blinkTimer < 433)
                        {
                            spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/As1b"), faceHitbox, Color.White * (modPlayer.starfarerDialogueVisibility - 0.3f));
                        }

                    }
                }
                if (modPlayer.chosenStarfarer == 2)
                {
                    if (modPlayer.vagrantDialogue == 2)
                    {
                        if (modPlayer.starfarerHairstyle == 1)
                        {
                            spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/DialoguePortraits/HEBaseAlt1"), hitbox, Color.White * (modPlayer.starfarerDialogueVisibility - 0.3f));

                        }
                        else
                        {
                            spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/DialoguePortraits/HEBase"), hitbox, Color.White * (modPlayer.starfarerDialogueVisibility - 0.3f));

                        }
                    }
                    else
                    {
                        if (modPlayer.starfarerHairstyle == 1)
                        {
                            spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/DialoguePortraits/EBaseAlt1"), hitbox, Color.White * (modPlayer.starfarerDialogueVisibility - 0.3f));

                        }
                        else
                        {
                            spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/DialoguePortraits/EBase"), hitbox, Color.White * (modPlayer.starfarerDialogueVisibility - 0.3f));

                        }

                    }
                    spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/Er0" + modPlayer.expression), faceHitboxAlt, Color.White * (modPlayer.starfarerDialogueVisibility - 0.3f));

                    if (modPlayer.expression < 10)
                    {
                        spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/DialoguePortraits/EOutfit" + modPlayer.starfarerOutfitVisible), hitbox, Color.White * (modPlayer.starfarerDialogueVisibility - 0.3f));

                    }
                    if (modPlayer.expression == 0 || modPlayer.expression == 1 || modPlayer.expression == 2 || modPlayer.expression == 4 || modPlayer.expression == 5 || modPlayer.expression == 6)
                    {
                        if ((modPlayer.blinkTimer > 70 && modPlayer.blinkTimer < 75) || (modPlayer.blinkTimer > 320 && modPlayer.blinkTimer < 325) || (modPlayer.blinkTimer > 420 && modPlayer.blinkTimer < 425) || modPlayer.blinkTimer > 428 && modPlayer.blinkTimer < 433)
                        {
                            spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/Er0b"), faceHitboxAlt, Color.White * (modPlayer.starfarerDialogueVisibility - 0.3f));
                        }
                    }
                }
            }

            spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/DialogueTop"), staticHitbox, Color.White * modPlayer.starfarerDialogueVisibility);
            Recalculate();


        }

        public Rectangle MovementAnimation(float playerVelocityX, float playerVelocityY)
        {
            // Initial position relative to the screen center
            float startX = -25;
            float startY = 160;

            // The factor which determines the speed of rectangle movement relative to player velocity.
            // This can be adjusted based on how responsive you want the movement to be.
            float factor = AdjustmentFactor;

            // Modify the position based on the player's X and Y velocity
            float newX = startX - playerVelocityX * factor;
            float newY = startY - playerVelocityY * factor;

            // If either velocity is 0, move the rectangle back towards its original point.
            if (playerVelocityX == 0)
            {
                newX = startX;
            }
            if (playerVelocityY == 0)
            {
                newY = startY;
            }

            area.Left.Set(newX, 0f);
            area.Top.Set(newY, 0f);
            Rectangle staticHitbox = area.GetInnerDimensions().ToRectangle();
            return staticHitbox;
        }

        public override void Update(GameTime gameTime) {
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogueVisibility > 0))
			{
				area.Remove();
				return;

			}
			else
			{
				Append(area);
			}
			
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

			// Setting the text per tick to update and show our resource values.
			text.SetText($"{modPlayer.animatedDialogue}");
			


			//text.SetText($"[c/5970cf:{modPlayer.judgementGauge} / 100]");
			base.Update(gameTime);
		}
	}
}
