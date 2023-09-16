
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
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Systems;

namespace StarsAbove.UI.IrminsulDream
{
    internal class IrminsulCursor : UIState
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
			area.Width.Set(70, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(70, 0f);

			barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/IrminsulDream/Cursor"));
			barFrame.Left.Set(0, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(70, 0f);
			barFrame.Height.Set(70, 0f);


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
			if (!Main.LocalPlayer.GetModPlayer<WeaponPlayer>().IrminsulAttackActive)
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);
			area.Left.Set(Main.mouseX - 20, 0f); // Place the resource bar to the left of the hearts.
			area.Top.Set(Main.mouseY - 20, 0f); // Placing it just a bit below the top of the screen.
			Recalculate();
			
		}
		public override void Update(GameTime gameTime) {
			//if (!(Main.LocalPlayer.HeldItem.ModItem is HawkmoonRanged) || !(Main.LocalPlayer.HeldItem.ModItem is HawkmoonMagic))
			if (!Main.LocalPlayer.GetModPlayer<WeaponPlayer>().IrminsulAttackActive)
				return;

			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			// Setting the text per tick to update and show our resource values.
			//text.SetText($"");

			
			base.Update(gameTime);
		}
	}
}
