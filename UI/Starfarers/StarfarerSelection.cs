
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Utilities;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI.Starfarers
{
    internal class StarfarerSelection : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
		private UIText text;
		private UIText warning;
		private UIText description;
		private UIText AsphodeneText;
		private UIText EridaniText;
		private UIElement area;
		private UIElement area2;
		private UIElement area3;
		private UIImage barFrame;

		private UIImage Asphodene;
		private UIImage Eridani;

		private UIImage bg;
		private UIImageButton imageButton;
		private UIImage bulletIndicatorOn;
		private Color gradientA;
		private Color gradientB;
		private Color finalColor;

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement(); 
			area.Width.Set(700, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(700, 0f);
			area.HAlign = area.VAlign = 0.5f; // 1

			area2 = new UIElement();
			area2.Left.Set(250, 0f);
			area2.Width.Set(300, 0f); 
			area2.Height.Set(400, 0f);
			area2.OnMouseOver += MouseOverA;
			area2.OnLeftClick += MouseClickA;
			area2.OnMouseOut += MouseOffA;
			area2.HAlign = area2.VAlign = 0.5f; // 1

			area3 = new UIElement();
			area3.Left.Set(-250, 0f);
			area3.Width.Set(300, 0f);
			area3.Height.Set(700, 0f);
			area3.OnMouseOver += MouseOverB;
			area3.OnMouseOut += MouseOffB;
			area3.OnLeftClick += MouseClickB;
			area3.HAlign = area3.VAlign = 0.5f; // 1

			Asphodene = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/blank"));
			Asphodene.Left.Set(350, 0f);
			Asphodene.Top.Set(0, 0f);
			Asphodene.Width.Set(400, 0f);
			Asphodene.Height.Set(650, 0f);

			Eridani = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/blank"));
			Eridani.Left.Set(-40, 0f);
			Eridani.Top.Set(0, 0f);
			Eridani.Width.Set(400, 0f);
			Eridani.Height.Set(650, 0f);

			barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/blank"));
			barFrame.Left.Set(0, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(700, 0f);
			barFrame.Height.Set(700, 0f);

			/*Asphodene = new UIImage(Request<Texture2D>("StarsAbove/UI/Starfarers/Eridani"));
			Asphodene.OnMouseOver += MouseOverA;
			Asphodene.OnClick += MouseClickA;
			Asphodene.Top.Set(0, 0f);
			Asphodene.Left.Set(0, 0f);
			Asphodene.Width.Set(0, 0f);
			Asphodene.Height.Set(0, 0f);*/

			text = new UIText("", 1.4f); 
			text.Width.Set(250, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(490, 0f);
			text.Left.Set(90, 0f);
			warning = new UIText("", 1f); // text to show stat
			warning.Width.Set(250, 0f);
			warning.Height.Set(34, 0f);
			warning.Top.Set(530, 0f);
			warning.Left.Set(180, 0f);
			description = new UIText("", 1.2f); // text to show stat
			description.Width.Set(250, 0f);
			description.Height.Set(34, 0f);
			description.Top.Set(590, 0f);
			description.Left.Set(-100, 0f);


			gradientA = new Color(249, 133, 36); // 
			gradientB = new Color(255, 166, 83); //
			//area3.Append(Asphodene);
			
			area.Append(text);
			area.Append(warning);
			area.Append(barFrame);
			area.Append(description);
			area.Append(Asphodene);
			area.Append(Eridani);
			area.Append(area2);
			area.Append(area3);

			Append(area);

			
		}

		private void MouseClickA(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().StarfarerSelectionVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer < 1))
				return;
			if (Main.netMode != NetmodeID.Server) { Main.NewText(LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerSelection.Asphodene.2"), 190, 100, 247); }
			if (Main.netMode != NetmodeID.Server) { Main.NewText(LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerSelection.ManaIncrease"), 190, 100, 247); }
			if (Main.netMode != NetmodeID.Server) { Main.NewText(LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerSelection.Talk"), 241, 255, 180); }
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarerEffect = true;

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer = 1;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = false;
			// We can do stuff in here!
		}

		private void MouseOverA(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().StarfarerSelectionVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer < 1))
				return;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().AsphodeneHighlighted = true;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().AsphodeneXVelocity = 5;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerSelection.Asphodene.1");
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

			description.SetText($"{modPlayer.description}");
			// We can do stuff in here!
		}

		private void MouseOffA(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().StarfarerSelectionVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer < 1))
				return;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().AsphodeneHighlighted = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = false;

			// We can do stuff in here!
		}
		private void MouseClickB(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().StarfarerSelectionVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer < 1))
				return;
			if (Main.netMode != NetmodeID.Server){Main.NewText(LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerSelection.Eridani.2"), 190, 100, 247);}
			if (Main.netMode != NetmodeID.Server){Main.NewText(LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerSelection.ManaIncrease"), 190, 100, 247);}
			if (Main.netMode != NetmodeID.Server){Main.NewText(LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerSelection.Talk"), 241, 255, 180);}
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer = 2;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarerEffect = true;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = false;

			// We can do stuff in here!
		}

		private void MouseOverB(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().StarfarerSelectionVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer < 1))
				return;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerSelection.Eridani.1");
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

			description.SetText($"{modPlayer.description}");
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = true;

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().EridaniHighlighted = true;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().EridaniXVelocity = -5;

			// We can do stuff in here!
		}

		private void MouseOffB(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().StarfarerSelectionVisibility >= 2f && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer < 1))
				return;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().EridaniHighlighted = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().description = "";
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().textVisible = false;
			// We can do stuff in here!
		}

		public override void Draw(SpriteBatch spriteBatch) {
			// This prevents drawing unless we are using an ExampleDamageItem
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().StarfarerSelectionVisibility > 0 && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer < 1))
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			// Calculate quotient
			
			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 12;
			hitbox.Width -= 24;
			hitbox.Y += 8;
			hitbox.Height -= 16;

			Rectangle indicator = barFrame.GetInnerDimensions().ToRectangle();
			indicator.X += 0;
			indicator.Width -= 0;
			indicator.Y += 0;
			indicator.Height -= 0;

			Rectangle AsphodeneStatic = Asphodene.GetInnerDimensions().ToRectangle();
			Rectangle AsphodeneI = Asphodene.GetInnerDimensions().ToRectangle();
			AsphodeneI.X += modPlayer.AsphodeneX/2;

			Rectangle EridaniStatic = Eridani.GetInnerDimensions().ToRectangle();
			Rectangle EridaniI = Eridani.GetInnerDimensions().ToRectangle();
			EridaniI.X += modPlayer.EridaniX/2;

			//spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/base"), indicator, Color.White * modPlayer.StarfarerSelectionVisibility);

			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/AsphodeneS"), AsphodeneI, Color.White * (modPlayer.StarfarerSelectionVisibility - 0.3f));
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/Asphodene"), AsphodeneStatic, Color.White * (modPlayer.StarfarerSelectionVisibility - 0.5f));

			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/EridaniS"), EridaniI, Color.White * (modPlayer.StarfarerSelectionVisibility - 0.3f));
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/Eridani"), EridaniStatic, Color.White * (modPlayer.StarfarerSelectionVisibility - 0.5f));

			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Starfarers/text"), indicator, Color.White * modPlayer.StarfarerSelectionVisibility);

			if ((modPlayer.StarfarerSelectionVisibility >= 2f))
			{
				base.DrawSelf(spriteBatch);

			}
			description.SetText($"{modPlayer.description}");

			Recalculate();


		}
			
		
		public override void Update(GameTime gameTime) {
			
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().StarfarerSelectionVisibility > 0 && Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 0))
			{
				area.Remove();
				return;

			}
			else
			{
				Append(area);
			}
			/*area.Left.Pixels = (Main.screenWidth / 2);
			area.Top.Pixels = (Main.screenHeight / 2);
			area2.Left.Pixels = (Main.screenWidth / 2);
			area2.Top.Pixels = (Main.screenHeight / 2) - 250 + 12;
			area3.Left.Pixels = (Main.screenWidth / 2);
			area3.Top.Pixels = (Main.screenHeight / 2) - 250 + 12;*/
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			// Setting the text per tick to update and show our resource values.

			description.SetText($"{modPlayer.description}");
			text.SetText(LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerSelection.Choice"));
			warning.SetText(LangHelper.GetTextValue($"StarfarerMenuDialogue.StarfarerSelection.Warning"));
			//text.SetText($"[c/5970cf:{modPlayer.judgementGauge} / 100]");
			base.Update(gameTime);
		}
	}
}
