
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Items;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI
{
    internal class DreamersInkwellGauge : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the requiearthInk stuff for most UI's in the Mod class.
		private UIText text;
		private UIElement area;
		private UIImage center;
		private Color gradientA;
		private Color gradientB;
		private Color finalColor;

		private UIImageButton earthInk;
		private UIImageButton windInk;
		private UIImageButton flameInk;
		private UIImageButton oceanInk;

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement();
			//area.Left.Set(-area.Width.Pixels - 1050, 1f); // Place the resource bar to the left of the hearts.
			area.Top.Set(0, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(0, 0f);
			area.Height.Set(0, 0f);
			area.HAlign = area.VAlign = 0.5f; // 1

			center = new UIImage(Request<Texture2D>("StarsAbove/UI/CeruleanFlameGauge"));
			center.Left.Set(22, 0f);
			center.Top.Set(0, 0f);
			center.Width.Set(138, 0f);
			center.Height.Set(34, 0f);

			earthInk = new UIImageButton(Request<Texture2D>("StarsAbove/UI/InkwellGauge/earthInk"));
			earthInk.OnLeftClick += earthInkClick;
			earthInk.Width.Set(64, 0f);
			earthInk.Height.Set(64, 0f);
			earthInk.Left.Set(0, 0f);
			earthInk.Top.Set(0, 0f);//

			windInk = new UIImageButton(Request<Texture2D>("StarsAbove/UI/InkwellGauge/windInk"));
			windInk.OnLeftClick += windInkClick;
			windInk.Width.Set(64, 0f);
			windInk.Height.Set(64, 0f);
			windInk.Left.Set(0, 0f);
			windInk.Top.Set(0, 0f);//

			flameInk = new UIImageButton(Request<Texture2D>("StarsAbove/UI/InkwellGauge/flameInk"));
			flameInk.OnLeftClick += flameInkClick;
			flameInk.Width.Set(64, 0f);
			flameInk.Height.Set(64, 0f);
			flameInk.Left.Set(0, 0f);
			flameInk.Top.Set(0, 0f);//

			oceanInk = new UIImageButton(Request<Texture2D>("StarsAbove/UI/InkwellGauge/oceanInk"));
			oceanInk.OnLeftClick += oceanInkClick;
			oceanInk.Width.Set(64, 0f);
			oceanInk.Height.Set(64, 0f);
			oceanInk.Left.Set(0, 0f);
			oceanInk.Top.Set(0, 0f);//

			text = new UIText("", 1.5f); // text to show stat
			text.Width.Set(138, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(40, 0f);
			text.Left.Set(0, 0f);

			gradientA = new Color(178, 227, 213); // 
			gradientB = new Color(60, 255, 199); //
			finalColor = new Color(0, 224, 255);

			area.Append(earthInk);
			area.Append(windInk);
			area.Append(flameInk);
			area.Append(oceanInk);

			//Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch) {
			if (Main.LocalPlayer.GetModPlayer<WeaponPlayer>().InkwellUIAlpha <= 0f)
				return;

			base.Draw(spriteBatch);
		}
		private void earthInkClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<WeaponPlayer>().InkwellUIAlpha <= 0f)
				return;
			
			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().InkwellInk = 0;
			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().InkwellUIAdjustment = 15;

			SoundEngine.PlaySound(SoundID.Item4, null);




			// We can do stuff in here!
		}
		private void windInkClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<WeaponPlayer>().InkwellUIAlpha <= 0f)
				return;

			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().InkwellInk = 1;
			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().InkwellUIAdjustment = 15;

			SoundEngine.PlaySound(SoundID.Item4, null);




			// We can do stuff in here!
		}
		private void flameInkClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<WeaponPlayer>().InkwellUIAlpha <= 0f)
				return;

			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().InkwellInk = 2;
			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().InkwellUIAdjustment = 15;

			SoundEngine.PlaySound(SoundID.Item4, null);




			// We can do stuff in here!
		}
		private void oceanInkClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (Main.LocalPlayer.GetModPlayer<WeaponPlayer>().InkwellUIAlpha <= 0f)
				return;

			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().InkwellInk = 3;
			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().InkwellUIAdjustment = 15;

			SoundEngine.PlaySound(SoundID.Item4, null);



			// We can do stuff in here!
		}

		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);

			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			
			Rectangle earthInkArea = earthInk.GetInnerDimensions().ToRectangle();
			Rectangle windInkArea = windInk.GetInnerDimensions().ToRectangle();
			Rectangle flameInkArea = flameInk.GetInnerDimensions().ToRectangle();
			Rectangle oceanInkArea = oceanInk.GetInnerDimensions().ToRectangle();

			Texture2D InkwellCenterTexture = (Texture2D)Request<Texture2D>("StarsAbove/UI/InkwellGauge/InkwellCenter");
			Vector2 InkwellCenterPosition = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2 - 150);

			spriteBatch.Draw(
						InkwellCenterTexture, //The texture being drawn.
						InkwellCenterPosition, //The position of the texture.
						new Rectangle(0, 0, InkwellCenterTexture.Width, InkwellCenterTexture.Height), //The source rectangle.
						Color.White * (modPlayer.InkwellUIAlpha), //The color of the texture.
						MathHelper.ToRadians(modPlayer.InkwellUIRotation), // The rotation of the texture.
						InkwellCenterTexture.Size() * 0.5f, //The centerpoint of the texture.
						1f, //The scale of the texture.
						SpriteEffects.None,
						0f);
			area.Top.Set(-140, 0f);
			area.Width.Set(300, 0f);
			area.Height.Set(300, 0f);
			area.Left.Set(0, 0f);

			oceanInk.Top.Set(108, 0f);
			oceanInk.Left.Set(182 + modPlayer.InkwellUIAlpha * 10, 0f);

			flameInk.Top.Set(108, 0f);
			flameInk.Left.Set(56 - modPlayer.InkwellUIAlpha * 10, 0f);

			earthInk.Left.Set(118 , 0f);
			earthInk.Top.Set(45 - modPlayer.InkwellUIAlpha * 10, 0f);

			windInk.Left.Set(118 , 0f);
			windInk.Top.Set(170 + modPlayer.InkwellUIAlpha * 10, 0f);

			if (modPlayer.InkwellUIAlpha > 0f)
            {
				if (modPlayer.InkwellInk == 0)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/InkwellGauge/earthInk"), earthInkArea, Color.White);
				}
				if (modPlayer.InkwellInk == 1)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/InkwellGauge/windInk"), windInkArea, Color.White);
				}
				if (modPlayer.InkwellInk == 2)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/InkwellGauge/flameInk"), flameInkArea, Color.White);
				}
				if (modPlayer.InkwellInk == 3)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/InkwellGauge/oceanInk"), oceanInkArea, Color.White);
				}

			}
			
		}
		public override void Update(GameTime gameTime) {
			if (Main.LocalPlayer.GetModPlayer<WeaponPlayer>().InkwellUIAlpha <= 0f)
			{
				area.Remove();
				return;
			}
			else
			{
				if (ContainsPoint(Main.MouseScreen))
				{
					//Main.LocalPlayer.mouseInterface = true;
				}
				Append(area);
			}
			
			// Setting the text per tick to update and show our resource values.
			text.SetText($"");
			base.Update(gameTime);
		}
	}
}
