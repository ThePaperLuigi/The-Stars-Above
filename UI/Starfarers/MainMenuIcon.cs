
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Systems;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI.Starfarers
{
    internal class MainMenuIcon : UIState
	{

		private UIText text;
		private UIText warning;

		public static UIImage astral;
		public static UIImage umbral;

		private UIElement area;
		private UIElement area2;
		private UIElement area3;
		private UIImage barFrame;
		private UIImage bg;
		private UIImageButton imageButton;
		private UIImage bulletIndicatorOn;
		private Color gradientA;
		private Color gradientB;
		private Color finalColor;
		private Vector2 offset;
		public bool dragging = false;
		public static bool Draggable;


		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement(); 
			area.Left.Set(-25, 0f); // Place the resource bar to the left of the hearts.
			area.Top.Set(160, 0f); 
			area.Width.Set(700, 0f); 
			area.Height.Set(300, 0f);
			area.HAlign = area.VAlign = 0.5f; // 1

			astral = new UIImage(Request<Texture2D>("StarsAbove/Items/Astral"));
			astral.Left.Set(0, 0f);
			astral.Top.Set(0, 0f);
			astral.Width.Set(32, 0f);
			astral.Height.Set(32, 0f);

			umbral = new UIImage(Request<Texture2D>("StarsAbove/Items/Umbral"));
			umbral.Left.Set(0, 0f);
			umbral.Top.Set(0, 0f);
			umbral.Width.Set(32, 0f);
			umbral.Height.Set(32, 0f);
			Append(area);

			
		}

		public override void Draw(SpriteBatch spriteBatch) {
			// This prevents drawing unless we are using an ExampleDamageItem
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			// Calculate quotient
			
			Rectangle hitbox = area.GetInnerDimensions().ToRectangle();


			Recalculate();


		}
			
		
		public override void Update(GameTime gameTime) {
			//area.Remove();
			//return; 

			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

			//text.SetText($"[c/5970cf:{modPlayer.judgementGauge} / 100]");
			base.Update(gameTime);
		}
	}
}
