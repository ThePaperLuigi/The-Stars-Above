
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Items;
using Terraria;using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI.CelestialCartography
{
	internal class CelestialCompass : UIState
	{
		
		private UIText text;
		private UIElement area;

		private UIElement CompassRegion;
		private UIElement GearRegion;

		private UIImage barFrame;
		private Color gradientA;
		private Color gradientB;

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement();
			area.Top.Set(100, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(1000, 0f);
			area.Height.Set(650, 0f);
			area.HAlign = area.VAlign = 0.5f; // 1

			CompassRegion = new UIElement();//UIImage(Request<Texture2D>("StarsAbove/UI/CelestalCartography/CelestialCompass"));
			CompassRegion.Left.Set(74, 0f);
			CompassRegion.Top.Set(125, 0f);
			CompassRegion.Width.Set(600, 0f);
			CompassRegion.Height.Set(600, 0f);

			GearRegion = new UIElement();//UIImage(Request<Texture2D>("StarsAbove/UI/CelestalCartography/CelestialCompass"));
			GearRegion.Left.Set(190, 0f);
			GearRegion.Top.Set(241, 0f);
			GearRegion.Width.Set(600, 0f);
			GearRegion.Height.Set(600, 0f);

			text = new UIText("", 1.5f); // text to show stat
			text.Width.Set(138, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(40, 0f);
			text.Left.Set(0, 0f);

			area.Append(text);
			area.Append(CompassRegion);
			area.Append(GearRegion);
			Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch) {
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().CelestialCompassVisibility <= 0f)
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			

			Rectangle hitbox = CompassRegion.GetInnerDimensions().ToRectangle();
			Rectangle gearHitbox = GearRegion.GetInnerDimensions().ToRectangle();

			Vector2 compassCenter = new Vector2(CompassRegion.GetInnerDimensions().Width, CompassRegion.GetInnerDimensions().Height)/2;
			Vector2 gearCenter = new Vector2(GearRegion.GetInnerDimensions().Width/2, GearRegion.GetInnerDimensions().Height / 2) ;
			/*spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassCenter"),
				hitbox,
				Color.White * (modPlayer.CelestialCompassVisibility));*/



			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassCenter"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);
			
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/RotationRing"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(modPlayer.CelestialCompassRotation + modPlayer.CelestialCompassInitialVelocity),//Very mechanical animation, ends on neutral
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/Gear"),
				gearHitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(modPlayer.CelestialCompassRotation - modPlayer.CelestialCompassInitialVelocity),//Very mechanical animation, ends on neutral
				gearCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassLowerBase"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),//Static
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/CompassTopBase"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(modPlayer.CelestialCompassRotation - modPlayer.CelestialCompassInitialVelocity),//Very mechanical animation, ends on neutral
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/RotatingPiece1"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(modPlayer.CelestialCompassRotation - modPlayer.CelestialCompassInitialVelocity),//
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/Telescope"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians((modPlayer.CelestialCompassRotation - modPlayer.CelestialCompassInitialVelocity) - 25),//
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/Pointer"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(-modPlayer.CelestialCompassRotation2 + 150),//Very fluid animation. Subtract/add a static number to set position.
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/OrbitingPin"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(modPlayer.CelestialCompassRotation3),//Constant orbit.
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/OrbitingPin"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(modPlayer.CelestialCompassRotation3 + 180),//Constant orbit + offset.
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/LowerPiece1"),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(modPlayer.CelestialCompassRotation - modPlayer.CelestialCompassInitialVelocity),
				compassCenter,
				SpriteEffects.None,
				1f);
			spriteBatch.Draw(
				(Texture2D)Request<Texture2D>("StarsAbove/UI/CelestialCartography/RingFrames/RingFrame" + modPlayer.CelestialCompassFrame),
				hitbox,
				null,
				Color.White * (modPlayer.CelestialCompassVisibility),
				MathHelper.ToRadians(0),
				compassCenter,
				SpriteEffects.None,
				1f);


		}
		

		public override void Update(GameTime gameTime) {
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().CelestialCompassVisibility <= 0f)
				return;


			// Setting the text per tick to update and show our resource values.
			text.SetText($"");
			base.Update(gameTime);
		}
	}
}
