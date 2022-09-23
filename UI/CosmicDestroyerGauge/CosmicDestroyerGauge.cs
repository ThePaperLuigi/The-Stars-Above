
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Items;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI.CosmicDestroyerGauge
{
    internal class CosmicDestroyerGauge : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
		private UIText text;
		private UIElement area;
		private UIImage barFrame;
		private Color gradientA;
		private Color gradientB;
		private Color finalColor;

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement();
			//area.Left.Set(-area.Width.Pixels - 1050, 1f); // Place the resource bar to the left of the hearts.
			area.Top.Set(-30, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(146, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(112, 0f);
			area.HAlign = area.VAlign = 0.5f; // 1

			barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/blank"));
			barFrame.Left.Set(22, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(138, 0f);
			barFrame.Height.Set(40, 0f);

			text = new UIText("", 1.5f); // text to show stat
			text.Width.Set(138, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(40, 0f);
			text.Left.Set(0, 0f);

			gradientA = new Color(188, 255, 249); // 
			gradientB = new Color(188, 255, 249); //
			finalColor = new Color(0, 224, 255);

			area.Append(text);
			area.Append(barFrame);
			Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch) {
			if (!(Main.LocalPlayer.HeldItem.ModItem is CosmicDestroyer))
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);

			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			// Calculate quotient
			float quotient = (float)modPlayer.duality / 100; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
			quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

			// Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 12;
			hitbox.Width -= 24;
			hitbox.Y += 10;
			hitbox.Height -= 24;
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CosmicDestroyerGauge/CosmicDestroyerGaugeEmpty"), area.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CosmicDestroyerGaugeVisibility);
			if(modPlayer.CosmicDestroyerGauge > 9)
            {
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CosmicDestroyerGauge/CD1"), area.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CosmicDestroyerGaugeVisibility);

			}
			if (modPlayer.CosmicDestroyerGauge > 18)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CosmicDestroyerGauge/CD2"), area.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CosmicDestroyerGaugeVisibility);

			}
			if (modPlayer.CosmicDestroyerGauge > 27)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CosmicDestroyerGauge/CD3"), area.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CosmicDestroyerGaugeVisibility);

			}
			if (modPlayer.CosmicDestroyerGauge > 36)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CosmicDestroyerGauge/CD4"), area.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CosmicDestroyerGaugeVisibility);

			}
			if (modPlayer.CosmicDestroyerGauge > 45)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CosmicDestroyerGauge/CD5"), area.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CosmicDestroyerGaugeVisibility);

			}
			if (modPlayer.CosmicDestroyerGauge > 54)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CosmicDestroyerGauge/CD6"), area.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CosmicDestroyerGaugeVisibility);

			}
			if (modPlayer.CosmicDestroyerGauge > 63)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CosmicDestroyerGauge/CD7"), area.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CosmicDestroyerGaugeVisibility);

			}
			if (modPlayer.CosmicDestroyerGauge > 72)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CosmicDestroyerGauge/CD8"), area.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CosmicDestroyerGaugeVisibility);

			}
			if (modPlayer.CosmicDestroyerGauge > 81)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CosmicDestroyerGauge/CD9"), area.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CosmicDestroyerGaugeVisibility);

			}
			if (modPlayer.CosmicDestroyerGauge > 90)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CosmicDestroyerGauge/CD10"), area.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CosmicDestroyerGaugeVisibility);

			}
			if (modPlayer.CosmicDestroyerGauge >= 100)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CosmicDestroyerGauge/CD11"), area.GetInnerDimensions().ToRectangle(), Color.White * modPlayer.CosmicDestroyerGaugeVisibility);

			}

		}
		public override void Update(GameTime gameTime) {
			if (!(Main.LocalPlayer.HeldItem.ModItem is CosmicDestroyer))
				return;


			// Setting the text per tick to update and show our resource values.
			text.SetText($"");
			base.Update(gameTime);
		}
	}
}
