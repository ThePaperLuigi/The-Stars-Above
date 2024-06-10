
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Items;
using StarsAbove.Items.Weapons;
using StarsAbove.Items.Weapons.Summon;
using StarsAbove.Items.Weapons.Ranged;
using StarsAbove.Items.Weapons.Other;
using StarsAbove.Items.Weapons.Celestial;
using StarsAbove.Items.Weapons.Melee;
using StarsAbove.Items.Weapons.Magic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Systems;

namespace StarsAbove.UI
{
    internal class WolvesbaneGauge : UIState
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
			barFrame.Width.Set(182, 0f);
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
            if (Main.LocalPlayer.GetModPlayer<WeaponPlayer>().wolvesbaneHeld)
			{

			}
			else
			{
                return;

            }

            base.Draw(spriteBatch);
		}
		
		int animationTimer;
		int animationFrame = 1;
		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);

			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			// Calculate quotient
			float quotient = (float)modPlayer.wolvesbaneGauge / (float)100; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
			quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

			barFrame.Left.Set(0, 0);

			// Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 16;
			hitbox.Width -= 32;
			hitbox.Y += 12;
			hitbox.Height -= 16;
			
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/DefaultGaugeBack"), barFrame.GetInnerDimensions().ToRectangle(), Color.White);

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
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/WolvesbaneGauge"), barFrame.GetInnerDimensions().ToRectangle(), Color.White);

		}
		public override void Update(GameTime gameTime) {
            if (Main.LocalPlayer.GetModPlayer<WeaponPlayer>().wolvesbaneHeld)
            {

            }
            else
            {
                return;

            }



            var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			
			
			base.Update(gameTime);

		}
	}
}
