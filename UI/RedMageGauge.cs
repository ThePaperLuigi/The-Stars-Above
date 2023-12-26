
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
    internal class RedMageGauge : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
		private UIText blackMana;
		private UIText whiteMana;

		private UIElement area;
		private UIElement blackManaGauge;
		private UIElement whiteManaGauge;
		private UIImage barFrame;
		private UIImage gemIcon;
		private UIImage manaStack;


		private Color colorWhite;
		private Color colorBlack;
		private Color finalColor;

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement();
			//area.Left.Set(-area.Width.Pixels - 1050, 1f); // Place the resource bar to the left of the hearts.
			area.Top.Set(0, 0f); // Placing it just a bit below the top of the screen.
			area.Left.Set(280, 0f);
			area.Width.Set(78, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(146, 0f);
			area.HAlign = area.VAlign = 0.5f; // 1

			blackManaGauge = new UIElement();
			blackManaGauge.Left.Set(26, 0f);
			blackManaGauge.Top.Set(32, 0f);
			blackManaGauge.Width.Set(10, 0f);
			blackManaGauge.Height.Set(70, 0f);

			whiteManaGauge = new UIElement();
			whiteManaGauge.Left.Set(42, 0f);
			whiteManaGauge.Top.Set(32, 0f);
			whiteManaGauge.Width.Set(10, 0f);
			whiteManaGauge.Height.Set(70, 0f);

			barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/RedMage/RedMageGauge"));
			barFrame.Left.Set(0, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(78, 0f);
			barFrame.Height.Set(146, 0f);

			/*gemIcon = new UIImage(Request<Texture2D>("StarsAbove/UI/RedMage/gemEmpty"));
			gemIcon.Left.Set(30, 0f);
			gemIcon.Top.Set(4, 0f);
			gemIcon.Width.Set(18, 0f);
			gemIcon.Height.Set(32, 0f);*/

			manaStack = new UIImage(Request<Texture2D>("StarsAbove/UI/RedMage/manaStackEmpty"));
			manaStack.Left.Set(0, 0f);
			manaStack.Top.Set(0, 0f);
			manaStack.Width.Set(70, 0f);
			manaStack.Height.Set(26, 0f);

			blackMana = new UIText("", 0.7f); // text to show stat
			blackMana.Width.Set(0, 0f);
			blackMana.Height.Set(0, 0f);
			blackMana.Top.Set(106, 0f);
			blackMana.Left.Set(16, 0f);

			whiteMana = new UIText("", 0.7f); // text to show stat
			whiteMana.Width.Set(0, 0f);
			whiteMana.Height.Set(0, 0f);
			whiteMana.Top.Set(106, 0f);
			whiteMana.Left.Set(42, 0f);

			colorBlack = new Color(78, 1, 255); // 

			colorWhite = new Color(255, 231, 186); // 

			finalColor = new Color(0, 224, 255);


			area.Append(barFrame);
			area.Append(blackMana);
			area.Append(whiteMana);
			area.Append(blackManaGauge);
			area.Append(whiteManaGauge);
			//area.Append(manaStack);
			//area.Append(gemIcon);
			Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch) {
			if (!(Main.LocalPlayer.HeldItem.ModItem is RedMage))
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);

			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			// Calculate quotient
			float quotient1 = (float)modPlayer.blackMana / 100; // Creating a quotient1 that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
			quotient1 = Utils.Clamp(quotient1, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

			// Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
			Rectangle blackManaHitbox = blackManaGauge.GetInnerDimensions().ToRectangle();

			// Now, using this blackManaHitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
			int bottom = blackManaHitbox.Bottom;
			int top = blackManaHitbox.Top;
			int steps = (int)((bottom - top) * quotient1);
			for (int i = 0; i < steps; i += 1) {
				//float percent = (float)i / steps; // Alternate Gradient Approach
				float percent = (float)i / (top - bottom);
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(blackManaHitbox.X, bottom - i, blackManaHitbox.Width, 1), colorBlack);
				
			}

			float quotient2 = (float)modPlayer.whiteMana / 100; // Creating a quotient2 that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
			quotient2 = Utils.Clamp(quotient2, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

			// Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
			Rectangle whiteManaHitbox = whiteManaGauge.GetInnerDimensions().ToRectangle();

			// Now, using this whiteManaHitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
			int bottom1 = whiteManaHitbox.Bottom;
			int top1 = whiteManaHitbox.Top;
			int steps1 = (int)((bottom1 - top1) * quotient2);
			for (int i = 0; i < steps1; i += 1)
			{
				//float percent = (float)i / steps1; // Alternate Gradient Approach
				float percent = (float)i / (top1 - bottom1);
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(whiteManaHitbox.X, bottom - i, whiteManaHitbox.Width, 1), colorWhite);

			}
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/RedMage/emptyGem"), area.GetInnerDimensions().ToRectangle(), Color.White);
			
			if (Main.LocalPlayer.GetModPlayer<WeaponPlayer>().blackMana > Main.LocalPlayer.GetModPlayer<WeaponPlayer>().whiteMana)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/RedMage/blackGem"), area.GetInnerDimensions().ToRectangle(), Color.White);
			}
			if (Main.LocalPlayer.GetModPlayer<WeaponPlayer>().whiteMana > Main.LocalPlayer.GetModPlayer<WeaponPlayer>().blackMana)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/RedMage/whiteGem"), area.GetInnerDimensions().ToRectangle(), Color.White);
			}
			if ((Main.LocalPlayer.GetModPlayer<WeaponPlayer>().whiteMana == Main.LocalPlayer.GetModPlayer<WeaponPlayer>().blackMana
				&& Main.LocalPlayer.GetModPlayer<WeaponPlayer>().blackMana + Main.LocalPlayer.GetModPlayer<WeaponPlayer>().whiteMana != 0)
				&& (Main.LocalPlayer.GetModPlayer<WeaponPlayer>().blackMana >= 50
				&& Main.LocalPlayer.GetModPlayer<WeaponPlayer>().whiteMana >= 50)
				|| (Main.LocalPlayer.GetModPlayer<WeaponPlayer>().blackMana >= 50
				&& Main.LocalPlayer.GetModPlayer<WeaponPlayer>().whiteMana >= 50))
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/RedMage/redGem"), area.GetInnerDimensions().ToRectangle(), Color.White);
			}
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/RedMage/manaStackEmpty"), area.GetInnerDimensions().ToRectangle(), Color.White);

			if (Main.LocalPlayer.GetModPlayer<WeaponPlayer>().manaStack == 3)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/RedMage/manaStack3"), area.GetInnerDimensions().ToRectangle(), Color.White);
			}
			if (Main.LocalPlayer.GetModPlayer<WeaponPlayer>().manaStack == 2)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/RedMage/manaStack2"), area.GetInnerDimensions().ToRectangle(), Color.White);
			}
			if (Main.LocalPlayer.GetModPlayer<WeaponPlayer>().manaStack == 1)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/RedMage/manaStack1"), area.GetInnerDimensions().ToRectangle(), Color.White);
			}
		}
		public override void Update(GameTime gameTime) {
			if (!(Main.LocalPlayer.HeldItem.ModItem is RedMage))
				return;
			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();

			blackMana.SetText($"{modPlayer.blackMana}");
			whiteMana.SetText($"{modPlayer.whiteMana}");

			// Setting the text per tick to update and show our resource values.

			base.Update(gameTime);
		}
	}
}
