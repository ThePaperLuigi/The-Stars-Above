
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Systems;
using StarsAbove.Utilities;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI.CelestialCartography
{
    internal class AreaNameUI : UIState
	{
		
		private UIText text;
		private UIElement area;


		private UIImage barFrame;
		private Color gradientA;
		private Color gradientB;

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement();
			area.Top.Set(000, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(1000, 0f);
			area.Height.Set(700, 0f);
			area.HAlign = 0.5f; // 1
			area.VAlign = 0.5f;

			text = new UIText("", 2.5f); // text to show stat
			//text.Width.Set(138, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(0, 0f);
			text.Left.Set(0, 0f);

			area.Append(text);
			Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch) {
			//if (Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().locationPopUpTimer > 0f)
			//	return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);
			var modPlayer = Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>();
			

			Rectangle hitbox = area.GetInnerDimensions().ToRectangle();
			Texture2D vignette = (Texture2D)Request<Texture2D>("StarsAbove/UI/StarfarerMenu/vignette");
			Texture2D screen = (Texture2D)Request<Texture2D>("StarsAbove/Subworlds/LoadingScreens/DefaultLS");

			float width = (float)Main.screenWidth / (float)vignette.Width;
			float height = (float)Main.screenHeight / (float)vignette.Height;

			float menuWidth = (float)Main.screenWidth / (float)screen.Width;
			float menuHeight = (float)Main.screenHeight / (float)screen.Height;

			Vector2 zero = Vector2.Zero;
			if (width != height)
			{
				if (height > width)
				{
					width = height;
					zero.X -= ((float)vignette.Width * width - (float)Main.screenWidth) * 0.5f;
				}
				else
				{
					zero.Y -= ((float)vignette.Height * width - (float)Main.screenHeight) * 0.5f;
				}
			}
			if (menuWidth != menuHeight)
			{
				if (menuHeight > menuWidth)
				{
					menuWidth = menuHeight;
					zero.X -= ((float)screen.Width * menuWidth - (float)Main.screenWidth) * 0.5f;
				}
				else
				{
					zero.Y -= ((float)screen.Height * menuWidth - (float)Main.screenHeight) * 0.5f;
				}
			}
			

			
			spriteBatch.Draw(vignette, Vector2.Zero, (Rectangle?)null, Color.White * (modPlayer.locationPopUpAlpha), 0f, Vector2.Zero, width, (SpriteEffects)0, 0f);
			if (modPlayer.locationName != "")
			{
				if(modPlayer.locationName == "WarriorOfLight" ||
					modPlayer.locationName == "FirstStarfarer" ||
                    modPlayer.locationName == "Vagrant" ||
                    modPlayer.locationName == "Thespian" ||
                    modPlayer.locationName == "Dioskouroi" ||
                    modPlayer.locationName == "Penthesilea" ||
                    modPlayer.locationName == "Nalhaun" ||
                    modPlayer.locationName == "Arbitration" ||
                    modPlayer.locationName == "Starfarers")
				{
 
					if(modPlayer.locationPopUpAlpha == 0.1f)
					{
                        Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
                    }
                    spriteBatch.Draw(
                        (Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationNames/" + modPlayer.locationName),
                        new Vector2(Main.screenWidth/2, Main.screenHeight/2 - 40),
						(Rectangle?)null,
						Color.White * (modPlayer.locationPopUpAlpha),
						0f,
                        new Vector2(hitbox.Width / 2, hitbox.Height / 2),
						MathHelper.Lerp(2,1,EaseHelper.InOutQuad(modPlayer.locationPopUpAlpha)),//scale
						(SpriteEffects)0,
						0f);

                }
				else
				{
                    spriteBatch.Draw(
					(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/LocationNames/" + modPlayer.locationName),
					hitbox,
					Color.White * (modPlayer.locationPopUpAlpha)
					);
                }
				
			}
            if (modPlayer.locationName != "WarriorOfLight" && modPlayer.locationName != "FirstStarfarer")
            {
                //spriteBatch.Draw(screen, new Vector2(0, -30), (Rectangle?)null, Color.White * (modPlayer.loadingScreenOpacity), 0f, Vector2.Zero, menuWidth, (SpriteEffects)0, 0f);

            }





            //text.Top.Set(modPlayer.locationPopUpPlacement, 0f);



        }


		public override void Update(GameTime gameTime) {
			//if (Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().locationPopUpTimer > 0f)
			//	return;


			// Setting the text per tick to update and show our resource values.
			//text.SetText($"{Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().locationName}");
			base.Update(gameTime);
		}
	}
}
