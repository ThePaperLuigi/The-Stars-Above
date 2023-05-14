
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI
{
    internal class SkyStrikerGauge : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
		private UIText text;
		private UIElement area;
		private UIImage barFrame;
		private Color gradientA;
		private Color gradientB;
		private Color finalColor;

		private UIImageButton red;
		private UIImageButton orange;
		private UIImageButton yellow;
		private UIImageButton green;
		private UIImageButton blue;
		private UIImageButton purple;

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement();
			//area.Left.Set(-area.Width.Pixels - 1050, 1f); // Place the resource bar to the left of the hearts.
			area.Top.Set(0, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(200, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(200, 0f);
			area.HAlign = area.VAlign = 0.5f; // 1

			/*barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/CeruleanFlameGauge"));
			barFrame.Left.Set(22, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(138, 0f);
			barFrame.Height.Set(34, 0f);*/

			red = new UIImageButton(Request<Texture2D>("StarsAbove/UI/SkyStrikerGauge/Red"));//Red
			red.OnClick += redClick;
			red.Width.Set(30, 0f);
			red.Height.Set(30, 0f);
			red.Left.Set(40, 0f);
			red.Top.Set(52, 0f);//

			orange = new UIImageButton(Request<Texture2D>("StarsAbove/UI/InkGauge/Orange"));//Unused
			//orange.OnClick += orangeClick;
			orange.Width.Set(22, 0f);
			orange.Height.Set(46, 0f);
			orange.Left.Set(89, 0f);
			orange.Top.Set(10, 0f);//

			yellow = new UIImageButton(Request<Texture2D>("StarsAbove/UI/SkyStrikerGauge/Yellow"));//Blue
			yellow.OnClick += yellowClick;
			yellow.Width.Set(30, 0f);
			yellow.Height.Set(30, 0f);
			yellow.Left.Set(128, 0f);
			yellow.Top.Set(52, 0f);//

			green = new UIImageButton(Request<Texture2D>("StarsAbove/UI/SkyStrikerGauge/Green"));//Yellow
			green.OnClick += greenClick;
			green.Width.Set(30, 0f);
			green.Height.Set(30, 0f);
			green.Left.Set(40, 0f);
			green.Top.Set(112, 0f);//

			blue = new UIImageButton(Request<Texture2D>("StarsAbove/UI/InkGauge/Blue"));//Unused
			//blue.OnClick += blueClick;
			blue.Width.Set(30, 0f);
			blue.Height.Set(30, 0f);
			blue.Left.Set(89, 0f);
			blue.Top.Set(135, 0f);//

			purple = new UIImageButton(Request<Texture2D>("StarsAbove/UI/SkyStrikerGauge/Purple"));//Green
			purple.OnClick += purpleClick;
			purple.Width.Set(30, 0f);
			purple.Height.Set(30, 0f);
			purple.Left.Set(128, 0f);
			purple.Top.Set(112, 0f);//

			text = new UIText("", 1.5f); // text to show stat
			text.Width.Set(138, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(40, 0f);
			text.Left.Set(0, 0f);

			gradientA = new Color(178, 227, 213); // 
			gradientB = new Color(60, 255, 199); //
			finalColor = new Color(0, 224, 255);

			area.Append(red);
			//area.Append(orange);
			area.Append(yellow);
			area.Append(green);
			//area.Append(blue);
			area.Append(purple);


			//Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch) {
			if (!(Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerHeld) && Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerMenuVisible == true)
				return;

			base.Draw(spriteBatch);
		}
		private void redClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerHeld && Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerMenuVisible == true))
				return;
			
			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerForm = 1;
			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerTransformPrep = true;

			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerMenuVisible = false;
			




			// We can do stuff in here!
		}
		
		private void yellowClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerHeld && Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerMenuVisible == true))
				return;

			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerForm = 2;
			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerTransformPrep = true;

			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerMenuVisible = false;





			// We can do stuff in here!
		}
		private void greenClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerHeld && Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerMenuVisible == true))
				return;

			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerForm = 3;
			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerTransformPrep = true;
			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerMenuVisible = false;





			// We can do stuff in here!
		}
		

			// We can do stuff in here!
		
		private void purpleClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerHeld && Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerMenuVisible == true))
				return;

			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerForm = 4;
			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerTransformPrep = true;

			Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerMenuVisible = false;





			// We can do stuff in here!
		}
		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);

			var modPlayer = Main.LocalPlayer.GetModPlayer<WeaponPlayer>();
			
			Rectangle redArea = red.GetInnerDimensions().ToRectangle();
			Rectangle orangeArea = orange.GetInnerDimensions().ToRectangle();
			Rectangle yellowArea = yellow.GetInnerDimensions().ToRectangle();
			Rectangle greenArea = green.GetInnerDimensions().ToRectangle();
			Rectangle blueArea = blue.GetInnerDimensions().ToRectangle();
			Rectangle purpleArea = purple.GetInnerDimensions().ToRectangle();
			if(modPlayer.SkyStrikerMenuVisible)
            {
				if (modPlayer.SkyStrikerForm == 1)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/SkyStrikerGauge/Red"), redArea, Color.White);
				}
				if (modPlayer.SkyStrikerForm == 2)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/SkyStrikerGauge/Yellow"), yellowArea, Color.White);
				}
				if (modPlayer.SkyStrikerForm == 3)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/SkyStrikerGauge/Green"), greenArea, Color.White);
				}
				if (modPlayer.SkyStrikerForm == 4)
				{
					spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/SkyStrikerGauge/Purple"), purpleArea, Color.White);
				}
				
			}
			
		}
		public override void Update(GameTime gameTime) {
			if (!(Main.LocalPlayer.GetModPlayer<WeaponPlayer>().SkyStrikerMenuVisible == true))
			{
				area.Remove();
				return;
			}
			else
			{
				Append(area);
			}
			if (ContainsPoint(Main.MouseScreen))
			{
				Main.LocalPlayer.mouseInterface = true;
			}
			// Setting the text per tick to update and show our resource values.
			text.SetText($"");
			base.Update(gameTime);
		}
	}
}
