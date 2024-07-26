
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
using StarsAbove.Buffs.Other.Nanomachina;
using StarsAbove.Systems;

namespace StarsAbove.UI
{
    internal class NanomachinaGauge : UIState
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
		private Color finalColor;

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement();
			//area.Left.Set(-area.Width.Pixels - 1050, 1f); // Place the resource bar to the left of the hearts.
			area.Top.Set(120, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(182, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(60, 0f);
			area.HAlign = area.VAlign = 0.5f; // 1

			barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/NanomachinaGauge"));
			barFrame.Left.Set(22, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(138, 0f);
			barFrame.Height.Set(34, 0f);

			text = new UIText("", 0.7f); // text to show stat
			text.Width.Set(138, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(40, 0f);
			text.Left.Set(0, 0f);

			gradientA = new Color(198, 169, 169); // Gauge
			gradientB = new Color(233, 44, 44); //

			gradientC = new Color(231, 227, 188); // Barrier
			gradientD = new Color(255, 238, 64); //

			finalColor = new Color(0, 224, 255);

			
			area.Append(barFrame);
			barFrame.Append(text);
			Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch) {
			if (!(Main.LocalPlayer.HasBuff(BuffType<RealizedNanomachinaBuff>())))
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);

			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			// Calculate quotient
			float quotientBarrier = (float)modPlayer.nanomachinaShieldHP / modPlayer.nanomachinaShieldHPMax; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
			quotientBarrier = Utils.Clamp(quotientBarrier, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

			float quotient = (float)modPlayer.nanomachinaGauge / 100; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
			quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

			barFrame.Top.Set(-30 + modPlayer.WeaponGaugeOffset, 0f);
			modPlayer.WeaponGaugeOffset += 20;

			// Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
			Rectangle hitboxBarrier = barFrame.GetInnerDimensions().ToRectangle();
			hitboxBarrier.X += 12;
			hitboxBarrier.Width -= 24;
			hitboxBarrier.Y += 8;
			hitboxBarrier.Height -= 24;

			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 12;
			hitbox.Width -= 24;
			hitbox.Y += 18;
			hitbox.Height -= 24;

			int left = hitbox.Left;
			int right = hitbox.Right;
			int steps = (int)((right - left) * quotient);
			for (int i = 0; i < steps; i += 1) {
				//float percent = (float)i / steps; // Alternate Gradient Approach
				float percent = (float)i / (right - left);
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height - 3), Color.Lerp(gradientA, gradientB, percent));
				
			}

			int leftBarrier = hitboxBarrier.Left;
			int rightBarrier = hitboxBarrier.Right;
			int stepsBarrier = (int)((rightBarrier - leftBarrier) * quotientBarrier);
			for (int i = 0; i < stepsBarrier; i += 1)
			{
				//float percent = (float)i / steps; // Alternate Gradient Approach
				float percent = (float)i / (rightBarrier - leftBarrier);
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(leftBarrier + i, hitboxBarrier.Y, 1, hitboxBarrier.Height - 3), Color.Lerp(gradientC, gradientD, percent));
				
			}
		}
		public override void Update(GameTime gameTime) {
			if (!(Main.LocalPlayer.HasBuff(BuffType<RealizedNanomachinaBuff>())))
				return;
			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			text.Left.Set(22f, 0f);
			text.Top.Set(-10f, 0f);
			// Setting the text per tick to update and show our resource values.
			text.SetText($"[c/FAD280:{Main.LocalPlayer.GetModPlayer<WeaponPlayer>().nanomachinaShieldHP}/{Main.LocalPlayer.GetModPlayer<WeaponPlayer>().nanomachinaShieldHPMax}]");

			base.Update(gameTime);
		}
	}
}
