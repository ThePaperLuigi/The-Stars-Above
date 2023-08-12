
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI.StellarNova
{
    internal class StellarNovaCutIn : UIState
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
		private UIImage bg2;
		private UIImage bg3;
		private UIImageButton imageButton;
		private UIImage bulletIndicatorOn;
		private Color gradientA;
		private Color gradientB;
		private Color finalColor;
		private Vector2 offset;
		public bool dragging = false;

		public static bool Visible;
		public static bool disableDialogue;

		public static bool ShadesVisible;


		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement(); 
			//area.Left.Set(100, 0f); // Place the resource bar to the left of the hearts.
			//area.Top.Set(0, 0f); 
			area.Width.Set(1000, 0f); 
			area.Height.Set(810, 0f);
			area.HAlign = 0.5f; // 1
			area.VAlign = 0.5f;

			barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/blank"));
			barFrame.Left.Set(-250, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(540, 0f);
			barFrame.Height.Set(710, 0f);
			bg = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/blank"));
			bg.Left.Set(-250, 0f);
			bg.Top.Set(0, 0f);
			bg.Width.Set(840, 0f);
			bg.Height.Set(810, 0f);
			bg2 = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/blank"));
			bg2.Left.Set(-250, 0f);
			bg2.Top.Set(0, 0f);
			bg2.Width.Set(840, 0f);
			bg2.Height.Set(810, 0f);
			bg3 = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/blank"));
			bg3.Left.Set(30, 0f);
			bg3.Top.Set(0, 0f);
			bg3.Width.Set(1000, 0f);
			bg3.Height.Set(114, 0f);


			/*Asphodene = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/Eridani"));
			Asphodene.OnMouseOver += MouseOverA;
			Asphodene.OnClick += MouseClickA;
			Asphodene.Top.Set(0, 0f);
			Asphodene.Left.Set(0, 0f);
			Asphodene.Width.Set(0, 0f);
			Asphodene.Height.Set(0, 0f);*/

			text = new UIText("", 2f); 
			text.Width.Set(150, 0f);
			text.Height.Set(155, 0f);
			text.Top.Set(435, 0f);
			text.Left.Set(90, 0f);
			
			


			gradientA = new Color(249, 133, 36); // 
			gradientB = new Color(255, 166, 83); //
			//area3.Append(Asphodene);
			
			barFrame.Append(text);
		
			area.Append(bg2);
			area.Append(bg);
			area.Append(bg3);
			area.Append(barFrame);
			
			Append(area);

			
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
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().NovaCutInTimer > 0 && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer != 0))
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			//area.Top.Set(50, 0f);
			// Calculate quotient
			area.VAlign = 1f;
			area.Top.Set(0 + Main.screenHeight / 8, 0f);
			area.HAlign = 0f;
			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			if(modPlayer.NovaCutInTimer > 100)
			{
				modPlayer.NovaCutInX += modPlayer.NovaCutInVelocity;
			}
			if(modPlayer.NovaCutInTimer < 40)
			{
				modPlayer.NovaCutInX -= modPlayer.NovaCutInVelocity;

			}
			hitbox.X += (int)modPlayer.NovaCutInX;
			text.Left.Set(100 + modPlayer.NovaCutInX, 0f);

			Rectangle background = bg.GetInnerDimensions().ToRectangle();
			background.X += (int)modPlayer.NovaCutInX / 3;

			Rectangle background2 = bg2.GetInnerDimensions().ToRectangle();
			background2.X += (int)modPlayer.NovaCutInX /2;

			Rectangle cutIn = bg3.GetInnerDimensions().ToRectangle();
			cutIn.X += (int)modPlayer.NovaCutInX * 2;
			//Rectangle indicator = new Rectangle((600), (280), (700), (440));
			//indicator.X += 0;
			//indicator.Width -= 0;
			//indicator.Y += 0;
			//indicator.Height -= 0;

			//Rectangle dialogueBox = new Rectangle((50), (480), (700), (300));
			if(Visible)
            {
				modPlayer.novaDialogue = "";
			}
			if(!Visible)
			{
				if (modPlayer.chosenStarfarer == 1)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/AS"), background2, Color.White * (modPlayer.NovaCutInOpacity));
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/AS1"), background, Color.White * (modPlayer.NovaCutInOpacity));

				}
				if (modPlayer.chosenStarfarer == 2)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/ES"), background2, Color.White * (modPlayer.NovaCutInOpacity));
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/ES1"), background, Color.White * (modPlayer.NovaCutInOpacity));

				}
				//spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/supernovaText"), cutIn, Color.White * (modPlayer.NovaCutInOpacity));
				Texture2D AsphodeneEye0 = (Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/AE0");
				Texture2D AsphodeneEye1 = (Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/AE1");
				Texture2D AsphodeneEye2 = (Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/AE2");
				Texture2D AsphodeneEye3 = (Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/AE3");

				if (modPlayer.chosenStarfarer == 1)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/A" + modPlayer.starfarerOutfitVisible), hitbox, Color.White * (modPlayer.NovaCutInOpacity));

					if (modPlayer.NovaCutInTimer >= 100)
					{
						spriteBatch.Draw(AsphodeneEye0, hitbox, Color.White * (modPlayer.NovaCutInOpacity));

					}
					if (modPlayer.NovaCutInTimer < 100 && modPlayer.NovaCutInTimer > 97)
					{
						spriteBatch.Draw(AsphodeneEye1, hitbox, Color.White * (modPlayer.NovaCutInOpacity));

					}
					if (modPlayer.NovaCutInTimer <= 97 && modPlayer.NovaCutInTimer > 95)
					{
						spriteBatch.Draw(AsphodeneEye2, hitbox, Color.White * (modPlayer.NovaCutInOpacity));

					}
					if (modPlayer.NovaCutInTimer <= 95)
					{
						spriteBatch.Draw(AsphodeneEye3, hitbox, Color.White * (modPlayer.NovaCutInOpacity));

					}
					


				}
				Texture2D EridaniEye0 = (Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/EE0");
				Texture2D EridaniEye1 = (Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/EE1");
				Texture2D EridaniEye2 = (Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/EE2");
				Texture2D EridaniEye3 = (Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/EE3");


				if (modPlayer.chosenStarfarer == 2)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/E" + modPlayer.starfarerOutfitVisible), hitbox, Color.White * (modPlayer.NovaCutInOpacity));

					if (modPlayer.NovaCutInTimer >= 100)
					{
						spriteBatch.Draw(EridaniEye0, hitbox, Color.White * (modPlayer.NovaCutInOpacity));

					}
					if (modPlayer.NovaCutInTimer < 100 && modPlayer.NovaCutInTimer > 97)
					{
						spriteBatch.Draw(EridaniEye1, hitbox, Color.White * (modPlayer.NovaCutInOpacity));

					}
					if (modPlayer.NovaCutInTimer <= 97 && modPlayer.NovaCutInTimer > 95)
					{
						spriteBatch.Draw(EridaniEye2, hitbox, Color.White * (modPlayer.NovaCutInOpacity));

					}
					if (modPlayer.NovaCutInTimer <= 95)
					{
						spriteBatch.Draw(EridaniEye3, hitbox, Color.White * (modPlayer.NovaCutInOpacity));

					}
					
					
				}
				if (!disableDialogue)
                {
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/NovaTextBox"), hitbox, Color.White * (modPlayer.NovaCutInOpacity));

				}
				else
                {
					modPlayer.novaDialogue = "";

				}

				if (ShadesVisible)
				{

					if (modPlayer.chosenStarfarer == 1)
					{
						spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/AShades"), hitbox, Color.White * (modPlayer.NovaCutInOpacity));
						

					}
					if (modPlayer.chosenStarfarer == 2)
					{
						spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNova/EShades"), hitbox, Color.White * (modPlayer.NovaCutInOpacity));
						

					}

				}
			}










			Recalculate();


		}

		
		public override void Update(GameTime gameTime) {
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().NovaCutInTimer > 0 && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer != 0))
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
			text.SetText($"{modPlayer.novaDialogue}");
			


			//text.SetText($"[c/5970cf:{modPlayer.judgementGauge} / 100]");
			base.Update(gameTime);
		}
	}
}
