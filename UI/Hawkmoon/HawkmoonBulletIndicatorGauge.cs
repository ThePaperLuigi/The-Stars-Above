
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Items;
using Terraria;using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI.Hawkmoon
{
	internal class HawkmoonBulletIndicatorGauge : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
		private UIText text;
		private UIElement area;
		private UIImage barFrame;
		private UIImage bulletIndicator;
		private UIImage bulletIndicatorOn;
		private Color gradientA;
		private Color gradientB;
		private Color finalColor;

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement();
			area.Left.Set(Main.mouseX, 0f); // Place the resource bar to the left of the hearts.
											area.Top.Set(Main.mouseY , 0f); // Placing it just a bit below the top of the screen.
											Recalculate();
			area.Width.Set(182, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(60, 0f);

			barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/Hawkmoon/blank"));
			barFrame.Left.Set(22, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(138, 0f);
			barFrame.Height.Set(34, 0f);


			/*
			bulletIndicator = new UIImage(Request<Texture2D>("StarsAbove/UI/Hawkmoon/"));
			bulletIndicator.Left.Set(62 , 0f);
			bulletIndicator.Top.Set(0, 0f);
			bulletIndicator.Width.Set(34, 0f);
			bulletIndicator.Height.Set(34, 0f);*/
			
			text = new UIText("", 1.2f); // text to show stat
			text.Width.Set(138, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(40, 0f);
			text.Left.Set(0, 0f);

			gradientA = new Color(205, 205, 180); // 
			gradientB = new Color(245, 205, 77); // 
			finalColor = new Color(255, 197, 0);


			area.Append(text);
			area.Append(barFrame);
			Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch) {
			// This prevents drawing unless we are using an ExampleDamageItem
			if (!(Main.LocalPlayer.HeldItem.ModItem is HawkmoonRanged || Main.LocalPlayer.HeldItem.ModItem is HawkmoonMagic))
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);
			area.Left.Set(Main.mouseX - 25, 0f); // Place the resource bar to the left of the hearts.
			area.Top.Set(Main.mouseY, 0f); // Placing it just a bit below the top of the screen.
			Recalculate();
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			// Calculate quotient
			float quotient = (float)modPlayer.powderGauge / 100; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
			quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

			// Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 12;
			hitbox.Width -= 24;
			hitbox.Y += 8;
			hitbox.Height -= 16;

			Rectangle indicator = new Rectangle((1025), (580), (34), (34));
			indicator.X += 0;
			indicator.Width -= 0;
			indicator.Y += 0;
			indicator.Height -= 0;


				// Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
			int left = hitbox.Left;
			int right = hitbox.Right;
			int steps = (int)((right - left) * quotient);
			
		}
		public override void Update(GameTime gameTime) {
			//if (!(Main.LocalPlayer.HeldItem.ModItem is HawkmoonRanged) || !(Main.LocalPlayer.HeldItem.ModItem is HawkmoonMagic))
			if (!(Main.LocalPlayer.HeldItem.ModItem is HawkmoonRanged || Main.LocalPlayer.HeldItem.ModItem is HawkmoonMagic))
				return;

			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			// Setting the text per tick to update and show our resource values.
			//text.SetText($"");

			if (modPlayer.hawkmoonRounds > 0)
			{
				text.SetText($"[c/5970cf:{modPlayer.hawkmoonRounds} / 12]");
			}
			else
			{
				text.SetText($"[c/5970cf:Reload]");

			}
			base.Update(gameTime);
		}
	}
}
