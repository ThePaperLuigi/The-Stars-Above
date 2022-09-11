
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Items;
using Terraria;using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI
{
	internal class StellarNovaGauge : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
		private UIText text;
		private UIElement area;
		private UIImage barFrame;
		private Color gradientA;
		private Color gradientB;
		private Color finalColor;
		private Vector2 offset;
		public bool dragging = false;
		bool prep = true;

		public static Vector2 NovaGaugePos;

		public static bool Draggable;

		int returnTimer;
		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new DraggableUIElement();
			area.Left.Set(1200, 0f); //
			area.Top.Set(30, 0f); // 
			area.Width.Set(182, 0f); //
			area.Height.Set(60, 0f);
			area.OnMouseOver += GaugeHover;
			area.OnMouseOut += HoverOff;	
			//area.IgnoresMouseInteraction = true;



			barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/StellarNovaGauge"));
			barFrame.Left.Set(0, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(138, 0f);
			barFrame.Height.Set(34, 0f);
			

			text = new UIText(" ", 0.8f); // text to show stat
			text.Width.Set(138, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(40, 0f);
			text.Left.Set(0, 0f);

			gradientA = new Color(205, 205, 180); // 
			gradientB = new Color(245, 205, 77); // 
			finalColor = new Color(255, 197, 0);

			

			//area.Append(text);
			area.Append(barFrame);
			Append(area);
		}
		
		public override void Draw(SpriteBatch spriteBatch) {
			// This prevents drawing unless we are using an ExampleDamageItem
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaGaugeUnlocked == true))
				return;

			base.Draw(spriteBatch);
		}
		private void GaugeHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaGaugeUnlocked == true))
				return;
			
			//Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaGaugeDescription = "Hold the Stellar Nova Key for 2 seconds to fix position";

			
	


			// We can do stuff in here!
		}
		private void HoverOff(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaGaugeUnlocked == true))
				return;

			//Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaGaugeDescription = $" ";





			// We can do stuff in here!
		}
		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);

			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			// Calculate quotient
			float quotient = (float)modPlayer.novaGauge / (float)modPlayer.trueNovaGaugeMax; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
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
			for (int i = 0; i < steps; i += 1) {
				//float percent = (float)i / steps; // Alternate Gradient Approach
				float percent = (float)i / (right - left);
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
				if (i >= 113)
				{
					spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left, hitbox.Y, 113, hitbox.Height), finalColor);

				}
			}
		}
		public override void Update(GameTime gameTime) {
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaGaugeUnlocked == true))
				return;


			if (StarsAbove.novaKey.Current)
			{
				returnTimer++;
			}
			else
            {
				returnTimer--;
            }
			if(returnTimer < 0)
            {
				returnTimer = 0;
            }
			if(returnTimer >= 120)
            {
				area.Left.Set(1200, 0f);
				area.Top.Set(30, 0f);
				returnTimer = 120;
            }


				// Here we check if the DragableUIPanel is outside the Parent UIElement rectangle. 
				// (In our example, the parent would be ExampleUI, a UIState. This means that we are checking that the DragableUIPanel is outside the whole screen)
				// By doing this and some simple math, we can snap the panel back on screen if the user resizes his window or otherwise changes resolution.

				//Vector2 configVec = NovaGaugePos;
				//Left.Set(configVec.X, 0f);
				//Top.Set(configVec.Y, 0f);


				var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			// Setting the text per tick to update and show our resource values.
			//text.SetText($"{modPlayer.novaGauge} / {modPlayer.trueNovaGaugeMax}");
			//Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaGaugeDescription = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaGaugeDescriptionActive;
			//modPlayer.novaGaugeDescriptionActive = $"{Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaGauge} / {Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().trueNovaGaugeMax}";
			//text.SetText($"[c/5970cf:{modPlayer.novaGaugeDescription}]");
			
			base.Update(gameTime);

		}
	}
}
