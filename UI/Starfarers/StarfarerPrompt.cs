
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI.Starfarers
{
    internal class StarfarerPrompt : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
		private UIText text;
		private UIText warning;
		
		private UIText AText;
		private UIText EridaniText;
		private UIElement area;
		private UIElement barFrame;
		private UIElement area3;
		private UIImage bg;
		private UIImageButton imageButton;
		private UIImage bulletIndicatorOn;
		private Color gradientA;
		private Color gradientB;
		private Color finalColor;
		private Vector2 offset;
		public bool dragging = false;
		private UIElement face;

		public static Vector2 PromptPos;

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement(); 
			area.Left.Set(0, 0f); // Place the resource bar to the left of the hearts.
			area.Top.Set(0, 0f); 
			area.Width.Set(700, 0f); 
			area.Height.Set(300, 0f);
			//area.OnMouseDown += new UIElement.MouseEvent(DragStart);
			//area.OnMouseUp += new UIElement.MouseEvent(DragEnd);
			face = new UIElement();
			face.Width.Set(400, 0f);
			face.Height.Set(400, 0f);

			barFrame = new UIElement();
			barFrame.Left.Set(184, 0f);
			barFrame.Top.Set(199, 0f);
			barFrame.Width.Set(452, 0f);
			barFrame.Height.Set(4, 0f);

			//imageButton = new UIImageButton(Request<Texture2D>("StarsAbove/UI/Starfarers/Button"));
			//imageButton.OnClick += MouseClickA;
			//imageButton.Left.Set(616, 0f);
			//imageButton.Top.Set(221, 0f);
			/*Asphodene = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/Eridani"));
			Asphodene.OnMouseOver += MouseOverA;
			Asphodene.OnClick += MouseClickA;
			Asphodene.Top.Set(0, 0f);
			Asphodene.Left.Set(0, 0f);
			Asphodene.Width.Set(0, 0f);
			Asphodene.Height.Set(0, 0f);*/

			text = new UIText("", 0.9f); 
			text.Width.Set(0, 0f);
			text.Height.Set(0, 0f);
			text.Top.Set(226, 0f);
			text.Left.Set(50, 0f);
			
			


			gradientA = new Color(249, 133, 36); // 
			gradientB = new Color(255, 166, 83); //
												 //area3.Append(Asphodene);
			area.Append(face);

			area.Append(text);
			//area.Append(imageButton);
			area.Append(barFrame);
			
			Append(area);

			
		}
		private void DragStart(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().promptVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().promptIsActive == true))
				return;
			offset = new Vector2(evt.MousePosition.X - area.Left.Pixels, evt.MousePosition.Y - area.Top.Pixels);
			dragging = true;
		}

		private void DragEnd(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().promptVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().promptIsActive == true))
				return;
			Vector2 end = evt.MousePosition;
			dragging = false;

			area.Left.Set(end.X - offset.X, 0f);
			area.Top.Set(end.Y - offset.Y, 0f);

			Recalculate();
		}
		private void MouseClickA(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogueVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue == true && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1))
				return; 
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialogueLeft--;

			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialogueLeft <= 0)
			{
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerDialogue = false;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenDialogue = 0;
				Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().dialogue = "";

			}




			// We can do stuff in here!
		}


		public override void Draw(SpriteBatch spriteBatch) {
			// This prevents drawing unless we are using an ExampleDamageItem
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().promptVisibility > 0))
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			/*area.Left.Set(area.Left.Pixels + modPlayer.promptMoveIn, 0);
			if(modPlayer.starfarerPromptActiveTimer < 1)
            {
				modPlayer.promptMoveIn = 0;
            }*/
			Rectangle hitbox = area.GetInnerDimensions().ToRectangle();

			Rectangle faceHitbox = face.GetInnerDimensions().ToRectangle();
			face.Left.Set(-102, 0f);
			face.Top.Set(-8, 0f);

			Rectangle faceHitboxAlt = face.GetInnerDimensions().ToRectangle();
			faceHitboxAlt.X += 18;
			faceHitboxAlt.Y += 8;
			faceHitboxAlt.Height = 400;
			

			faceHitbox.Height = 400;
		

			//Rectangle dialogueBox = new Rectangle((50), (480), (700), (300));
			float quotient = (float)modPlayer.starfarerPromptActiveTimer / 250; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
			quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

			// Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
			Rectangle hitbox2 = barFrame.GetInnerDimensions().ToRectangle(); 
			//hitbox2.Height += 40;

			// Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
			int left = hitbox2.Left;
			int right = hitbox2.Right;
			int steps = (int)((right - left) * quotient);
			for (int i = 0; i < steps; i += 1)
			{
				//float percent = (float)i / steps; // Alternate Gradient Approach
				float percent = (float)i / (right - left);
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox2.Y, 1, hitbox2.Height), Color.Lerp(gradientA, gradientB, percent));
			}

			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/MiniDialogue"), hitbox, Color.White * modPlayer.promptVisibility);
			if (modPlayer.vagrantDialogue == 2)
			{
				if (modPlayer.chosenStarfarer == 1)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/DialoguePortraits/HABase"), hitbox, Color.White * (modPlayer.promptVisibility - 0.3f));
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/As1" + modPlayer.promptExpression), faceHitbox, Color.White * (modPlayer.promptVisibility - 0.3f));

				}
				else
                {
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/DialoguePortraits/HEBase"), hitbox, Color.White * (modPlayer.promptVisibility - 0.3f));
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/Er0" + modPlayer.promptExpression), faceHitboxAlt, Color.White * (modPlayer.promptVisibility - 0.3f));

				}

			}
			else
			{
				if (modPlayer.chosenStarfarer == 1)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/DialoguePortraits/ABase"), hitbox, Color.White * (modPlayer.promptVisibility - 0.3f));
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/As1" + modPlayer.promptExpression), faceHitbox, Color.White * (modPlayer.promptVisibility - 0.3f));

				}
				else
                {
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/DialoguePortraits/EBase"), hitbox, Color.White * (modPlayer.promptVisibility - 0.3f));
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/VN/Er0" + modPlayer.promptExpression), faceHitboxAlt, Color.White * (modPlayer.promptVisibility - 0.3f));

				}

			}

			if (modPlayer.chosenStarfarer == 1)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/DialoguePortraits/AOutfit" + modPlayer.starfarerOutfitVisible), hitbox, Color.White * modPlayer.promptVisibility);

			}
			if (modPlayer.chosenStarfarer == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/DialoguePortraits/EOutfit" + modPlayer.starfarerOutfitVisible), hitbox, Color.White * modPlayer.promptVisibility);

			}
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/MiniDialogueOverlay"), hitbox, Color.White * modPlayer.promptVisibility);






			Recalculate();


		}
			
		
		public override void Update(GameTime gameTime) {
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().promptVisibility > 0))
			{
				area.Remove();
				return;

			}
			else
			{
				Append(area);
			}

			Vector2 configVec = PromptPos;
			Left.Set(configVec.X, 1f);
			Top.Set(configVec.Y, 1f);

			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

			// Setting the text per tick to update and show our resource values.
			text.SetText($"{modPlayer.animatedPromptDialogue}");

			//text.SetText($"[c/5970cf:{modPlayer.judgementGauge} / 100]");
			base.Update(gameTime);
		}
	}
}
