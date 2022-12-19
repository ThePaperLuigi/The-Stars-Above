
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI
{
    internal class EmotionGauge : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
		private UIText text;
		private UIElement area;
		private UIImage barFrame;
		private Color TopGradient1;
		private Color TopGradient2;

		private Color TopMiddleGradient1;
		private Color TopMiddleGradient2;

		private Color BottomGradient1;
		private Color BottomGradient2;

		private Color finalColor;

		private UIElement swordArea;

		private Vector2 offset;
		public bool dragging = false;
		bool prep = true;

		public static Vector2 NovaGaugePos;

		public static bool AnimationDisabled = false;

		int returnTimer;
		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement();
			area.Top.Set(120, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(182, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(60, 0f);
			area.HAlign = area.VAlign = 0.5f; // 1
											  //area.IgnoresMouseInteraction = true;

			swordArea = new UIElement();
			//swordArea.Top.Set(120, 0f);
			swordArea.Height.Set(1920, 0f);
			swordArea.Width.Set(1080, 0f);

			barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/blank"));
			barFrame.Left.Set(0, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(138, 0f);
			barFrame.Height.Set(40, 0f);
			

			text = new UIText(" ", 0.8f); // text to show stat
			text.Width.Set(138, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(40, 0f);
			text.Left.Set(0, 0f);

			TopGradient1 = new Color(255, 189, 189); // Start
			TopGradient2 = new Color(255, 53, 53); // Finish

			TopMiddleGradient1 = new Color(205, 160, 160); // 
			TopMiddleGradient2 = new Color(203, 8, 8); // 

			BottomGradient1 = new Color(165, 108, 108); // 
			BottomGradient2 = new Color(162, 0, 0); // 

			finalColor = new Color(255, 0, 0);



			//area.Append(text);
			area.Append(swordArea);
			area.Append(barFrame);
			Append(area);
		}
		
		public override void Draw(SpriteBatch spriteBatch) {
			if (!(Main.LocalPlayer.GetModPlayer<ManifestationPlayer>().manifestationHeld == true))
				return;

			base.Draw(spriteBatch);
		}
		private void GaugeHover(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<ManifestationPlayer>().manifestationHeld == true))
				return;
			
			//Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaGaugeDescription = "Hold the Stellar Nova Key for 2 seconds to fix position";

			
	


			// We can do stuff in here!
		}
		private void HoverOff(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<ManifestationPlayer>().manifestationHeld == true))
				return;

			//Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaGaugeDescription = $" ";





			// We can do stuff in here!
		}
		int animationTimer;
		int animationFrame = 1;
		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);

			var modPlayer = Main.LocalPlayer.GetModPlayer<ManifestationPlayer>();
			// Calculate quotient
			float quotient = (float)modPlayer.emotionGauge / (float)modPlayer.emotionGaugeMax; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
			quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

			barFrame.Left.Set(20, 0);

			// Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 12;
			hitbox.Width -= 24;
			hitbox.Y += 12;
			hitbox.Height -= 16;
			
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/EmotionGaugeBack"), barFrame.GetInnerDimensions().ToRectangle(), Color.White);

			if (quotient == 1f)
			{
				//spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNovaGaugeReady"), barFrame.GetInnerDimensions().ToRectangle(), Color.White);
				animationTimer++;
				if (animationTimer > 4)
				{
					animationFrame++;
					if (animationFrame > 11)
					{
						animationFrame = 1;
					}
					animationTimer = 0;
				}
				//spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/StellarNovaGaugeAnimation/NovaGaugeAnimation" + animationFrame), animationHitbox, Color.White);

			}
			// Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
			int left = hitbox.Left;
			int right = hitbox.Right;
			int steps = (int)((right - left) * quotient);
			for (int i = 0; i < steps; i += 1) {
				//float percent = (float)i / steps; // Alternate Gradient Approach
				float percent = (float)i / (right - left);
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, 18), Color.Lerp(BottomGradient1, BottomGradient2, percent));//Bottom layer.
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, 12), Color.Lerp(TopMiddleGradient1, TopMiddleGradient2, percent));//1 above the bottom.
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y + 4, 1, 4), Color.Lerp(TopGradient1, TopGradient2, percent));
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y + 8, 1, 2), Color.White);
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, 18), Color.White * modPlayer.gaugeChangeAlpha);


			}
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/EmotionGauge"), barFrame.GetInnerDimensions().ToRectangle(), Color.White);

			if(AnimationDisabled)
            {
				return;
            }

			Texture2D Blackscreen = (Texture2D)Request<Texture2D>("StarsAbove/UI/Blackscreen");
			Texture2D HorizontalBG = (Texture2D)Request<Texture2D>("StarsAbove/UI/Manifestation/HorizontalBG");
			Texture2D HorizontalSlash = (Texture2D)Request<Texture2D>("StarsAbove/UI/Manifestation/HorizontalSlash");

			Texture2D VerticalSlash1 = (Texture2D)Request<Texture2D>("StarsAbove/UI/Manifestation/Vertical1");
			Texture2D VerticalSlash2 = (Texture2D)Request<Texture2D>("StarsAbove/UI/Manifestation/Vertical2");
			Texture2D VerticalSlash3 = (Texture2D)Request<Texture2D>("StarsAbove/UI/Manifestation/Vertical3");
			Texture2D VerticalSlash4 = (Texture2D)Request<Texture2D>("StarsAbove/UI/Manifestation/Vertical4");
			Texture2D VerticalSlash5 = (Texture2D)Request<Texture2D>("StarsAbove/UI/Manifestation/Vertical5");

			Texture2D HorizontalSlash1 = (Texture2D)Request<Texture2D>("StarsAbove/UI/Manifestation/Horizontal1");
			Texture2D HorizontalSlash2 = (Texture2D)Request<Texture2D>("StarsAbove/UI/Manifestation/Horizontal2");
			Texture2D HorizontalSlash3 = (Texture2D)Request<Texture2D>("StarsAbove/UI/Manifestation/Horizontal3");


			float width = (float)Main.screenWidth / (float)Blackscreen.Width;
			float height = (float)Main.screenHeight / (float)Blackscreen.Height;

			//For the slices

			Vector2 zero = Vector2.Zero;
			if (width != height)
			{
				if (height > width)
				{
					width = height;
					zero.X -= ((float)Blackscreen.Width * width - (float)Main.screenWidth) * 0.5f;
				}
				else
				{
					zero.Y -= ((float)Blackscreen.Height * width - (float)Main.screenHeight) * 0.5f;
				}
			}
			spriteBatch.Draw(Blackscreen, Vector2.Zero, (Rectangle?)null, Color.White * modPlayer.greaterSplitAlpha, 0f, Vector2.Zero, width, (SpriteEffects)0, 0f);
			if(modPlayer.greaterSplitTimer > 0 && modPlayer.greaterSplitTimer <= 5)
            {
				spriteBatch.Draw(VerticalSlash5, Vector2.Zero, (Rectangle?)null, Color.White, 0f, Vector2.Zero, width, (SpriteEffects)0, 0f);

			}
			if (modPlayer.greaterSplitTimer > 5 && modPlayer.greaterSplitTimer < 10)
			{
				spriteBatch.Draw(VerticalSlash4, Vector2.Zero, (Rectangle?)null, Color.White, 0f, Vector2.Zero, width, (SpriteEffects)0, 0f);

			}
			if (modPlayer.greaterSplitTimer > 10 && modPlayer.greaterSplitTimer <= 15)
			{
				spriteBatch.Draw(VerticalSlash3, Vector2.Zero, (Rectangle?)null, Color.White, 0f, Vector2.Zero, width, (SpriteEffects)0, 0f);

			}
			if (modPlayer.greaterSplitTimer > 15 && modPlayer.greaterSplitTimer <= 20)
			{
				spriteBatch.Draw(VerticalSlash2, Vector2.Zero, (Rectangle?)null, Color.White, 0f, Vector2.Zero, width, (SpriteEffects)0, 0f);

			}
			if (modPlayer.greaterSplitTimer > 20 && modPlayer.greaterSplitTimer <= 25)
			{
				spriteBatch.Draw(VerticalSlash1, Vector2.Zero, (Rectangle?)null, Color.White, 0f, Vector2.Zero, width, (SpriteEffects)0, 0f);

			}

			spriteBatch.Draw(Blackscreen, Vector2.Zero, (Rectangle?)null, Color.White * modPlayer.greatSplitHorizontalAlpha, 0f, Vector2.Zero, width, (SpriteEffects)0, 0f);
			Rectangle swordBox = swordArea.GetInnerDimensions().ToRectangle();
			Vector2 swordCenter = new Vector2(swordArea.GetInnerDimensions().Width, swordArea.GetInnerDimensions().Height) / 2;

			if (modPlayer.greatSplitHorizontalTimer > 20)
            {
				spriteBatch.Draw(HorizontalBG, Vector2.Zero, (Rectangle?)null, Color.White * (1 - modPlayer.greatSplitAnimationRotationTimer), 0f, Vector2.Zero, width, (SpriteEffects)0, 0f);

				spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/Manifestation/HorizontalSlash"), //The texture being drawn.
				new Vector2(2280,-300),
				null,
				Color.White,
				MathHelper.ToRadians(modPlayer.greatSplitAnimationRotation),
				new Vector2(swordCenter.X + 1780,swordCenter.Y),
				1.4f,
				SpriteEffects.None,
				1f);

				
			}

			//Horziontal
			if (modPlayer.greatSplitHorizontalTimer > 0 && modPlayer.greatSplitHorizontalTimer <= 2)
			{
				spriteBatch.Draw(HorizontalSlash3, Vector2.Zero, (Rectangle?)null, Color.White, 0f, Vector2.Zero, width, (SpriteEffects)0, 0f);

			}
			if (modPlayer.greatSplitHorizontalTimer > 2 && modPlayer.greatSplitHorizontalTimer < 4)
			{
				//Flash to black
			}
			if (modPlayer.greatSplitHorizontalTimer > 4 && modPlayer.greatSplitHorizontalTimer <= 6)
			{
				spriteBatch.Draw(HorizontalSlash2, Vector2.Zero, (Rectangle?)null, Color.White, 0f, Vector2.Zero, width, (SpriteEffects)0, 0f);

			}
			if (modPlayer.greatSplitHorizontalTimer > 6 && modPlayer.greatSplitHorizontalTimer <= 8)
			{
				//Flash to black

			}
			if (modPlayer.greatSplitHorizontalTimer > 8 && modPlayer.greatSplitHorizontalTimer <= 10)
			{
				spriteBatch.Draw(HorizontalSlash1, Vector2.Zero, (Rectangle?)null, Color.White, 0f, Vector2.Zero, width, (SpriteEffects)0, 0f);

			}
			//Above 10 play the sword swing animation

		}
		public override void Update(GameTime gameTime) {
			if (!(Main.LocalPlayer.GetModPlayer<ManifestationPlayer>().manifestationHeld == true))
				return;


			


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
