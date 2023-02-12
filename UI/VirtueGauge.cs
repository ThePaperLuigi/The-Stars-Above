
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Items;
using Terraria;using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI
{
	internal class VirtueGauge : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
		private UIText text;
		private UIElement area;
		private UIImage barFrame;
		private Color gradientA;
		private Color gradientB;
		private Color gradientC;
		private Color gradientD;

		private Color finalcolorA;
		private Color finalcolorB;

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement();
			area.Top.Set(120, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(182, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(60, 0f);
			area.HAlign = area.VAlign = 0.5f; // 1

			barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/VirtueGauge"));
			barFrame.Left.Set(22, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(138, 0f);
			barFrame.Height.Set(34, 0f);

			text = new UIText("", 1.5f); // text to show stat
			text.Width.Set(138, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(40, 0f);
			text.Left.Set(0, 0f);

			gradientA = new Color(35, 125, 205); // 
			gradientB = new Color(80, 191, 255); //

			gradientC = new Color(97, 54, 189); // 
			gradientD = new Color(194, 80, 255); //

			finalcolorA = new Color(0, 232, 255); // 
			finalcolorB = new Color(193, 0, 255); //

			area.Append(text);
			area.Append(barFrame);
			Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch) {
			if (!(Main.LocalPlayer.HeldItem.ModItem is VirtuesEdge))
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);

			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			// Calculate quotient
			float quotient = (float)modPlayer.VirtueGauge / 100; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
			quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

			// Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 12;
			hitbox.Width -= 24;
			hitbox.Y += 8;
			hitbox.Height -= 16;

			// Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
			int left = hitbox.Left;
			int right = hitbox.Right;
			int steps = (int)((right - left) * quotient);
			
			if(modPlayer.VirtueMode == 0)
            {
				for (int i = 0; i < steps; i += 1)
				{
					//float percent = (float)i / steps; // Alternate Gradient Approach
					float percent = (float)i / (right - left);
					spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height - 3), Color.Lerp(gradientA, gradientB, percent));
					if (i >= 113)
					{
						spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left, hitbox.Y, 113, hitbox.Height), finalcolorA);

					}
				}
			}
			if(modPlayer.VirtueMode == 1)
            {
				for (int i = 0; i < steps; i += 1)
				{
					//float percent = (float)i / steps; // Alternate Gradient Approach
					float percent = (float)i / (right - left);
					spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height - 3), Color.Lerp(gradientC, gradientD, percent));
					if (i >= 113)
					{
						spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left, hitbox.Y, 113, hitbox.Height), finalcolorB);

					}
				}
			}
		}
		public override void Update(GameTime gameTime) {
			if (!(Main.LocalPlayer.HeldItem.ModItem is VirtuesEdge))
				return;


			// Setting the text per tick to update and show our resource values.
			text.SetText($"");
			base.Update(gameTime);
		}
	}
}
