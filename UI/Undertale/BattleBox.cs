
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Items;
 
using System;
using Terraria;using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.IO;
using StarsAbove;
using Microsoft.Xna.Framework.Audio;


namespace StarsAbove.UI.Undertale
{
	internal class BattleBox : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the Mod class.
		private UIText text;
		private UIElement area;
		private UIImage barFrame;
		private UIImage heart;
		private UIImage pellet;

		private UIImage swordUp;
		private UIImage swordDown;
		private UIImage swordBlue;
		private UIImage swordOrange;

		private Color gradientA;
		private Color gradientB;

		public override void OnInitialize()
		{
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding. You can use a UIPanel if you wish for a background.
			area = new UIElement();
			area.Top.Set(0, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(800, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(500, 0f);
			area.HAlign = area.VAlign = 0.5f; // 1

			barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/Undertale/Placeholder"));
			barFrame.Left.Set(0, 0f);
			barFrame.Top.Set(0, 0f);
			barFrame.Width.Set(800, 0f);
			barFrame.Height.Set(500, 0f);

			heart = new UIImage(Request<Texture2D>("StarsAbove/UI/Undertale/heartEmpty"));
			heart.Left.Set(0, 0f);
			heart.Top.Set(0, 0f);
			heart.Width.Set(18, 0f);
			heart.Height.Set(16, 0f);

			swordUp = new UIImage(Request<Texture2D>("StarsAbove/UI/Undertale/swordEmpty"));
			swordUp.Left.Set(0, 0f);
			swordUp.Top.Set(0, 0f);
			swordUp.Width.Set(38, 0f);
			swordUp.Height.Set(194, 0f);


			swordDown = new UIImage(Request<Texture2D>("StarsAbove/UI/Undertale/swordEmpty"));
			swordDown.Left.Set(0, 0f);
			swordDown.Top.Set(0, 0f);
			swordDown.Width.Set(38, 0f);
			swordDown.Height.Set(194, 0f);

			swordBlue = new UIImage(Request<Texture2D>("StarsAbove/UI/Undertale/swordEmpty"));
			swordBlue.Left.Set(0, 0f);
			swordBlue.Top.Set(0, 0f);
			swordBlue.Width.Set(82, 0f);
			swordBlue.Height.Set(272, 0f);

			pellet = new UIImage(Request<Texture2D>("StarsAbove/UI/Undertale/pelletEmpty"));
			pellet.Left.Set(0, 0f);
			pellet.Top.Set(0, 0f);
			pellet.Width.Set(18, 0f);
			pellet.Height.Set(16, 0f);

			text = new UIText(" ", 2f); // text to show stat
			text.Width.Set(138, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(350, 0f);
			text.Left.Set(270, 0f);

			gradientA = new Color(123, 25, 138); // A dark purple
			gradientB = new Color(187, 91, 201); // A light purple

			area.Append(text);
			area.Append(barFrame);
			area.Append(heart);
			area.Append(pellet);
			area.Append(swordUp);
			area.Append(swordDown);
			area.Append(swordBlue);
			Append(area);
		}
		int damageX1 = 50;
		int damageY1 = 34;
		int damageX2 = 700;
		int damageY2 = 113;

		int coloredSword = 0;//0 is blue, 1 is orange
		int damageXBlue = 50;
		int damageYBlue = 34;

		
		int iFrameFlashing = 0;
		bool heartVisible = true;
		int damageTransparency = 255;
		
		public override void Draw(SpriteBatch spriteBatch)
		{
			// This prevents drawing unless we are using an ExampleDamageItem
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().undertaleActive))
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			base.DrawSelf(spriteBatch);
			
			modPlayer.undertaleiFrames--;

			Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
			Rectangle heartloc = heart.GetInnerDimensions().ToRectangle();
			heartloc.Y += modPlayer.heartY;
			heartloc.X += modPlayer.heartX;
			Rectangle swordUpLoc = swordUp.GetInnerDimensions().ToRectangle();
			swordUpLoc.Y += damageY1;
			swordUpLoc.X += damageX1;
			Rectangle swordDownLoc = swordDown.GetInnerDimensions().ToRectangle();
			swordDownLoc.Y += damageY2;
			swordDownLoc.X += damageX2;
			Rectangle swordBlueLoc = swordBlue.GetInnerDimensions().ToRectangle();
			swordBlueLoc.Y += damageYBlue;
			swordBlueLoc.X += damageXBlue;

			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Undertale/BattleBox"), hitbox, Color.White * 255);
			if (modPlayer.undertaleiFrames > 0)
			{
				
			}
			if(iFrameFlashing>=4)
			{
				if(heartVisible)
				{
					heartVisible = false;
				}
				else
				{
					heartVisible = true;
				}
				iFrameFlashing = 0;
			}
			if (modPlayer.undertaleiFrames > 0)
			{
				iFrameFlashing++;
			}
			else
			{

				heartVisible = true;
			}
			if (heartVisible)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Undertale/heart"), heartloc, Color.White * 255);

			}
			else
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Undertale/heartEmpty"), heartloc, Color.White * 255);
			}
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Undertale/swordUp"), swordUpLoc, Color.White * 255);
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Undertale/swordDown"), swordDownLoc, Color.White * 255);
			if(coloredSword == 0)
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Undertale/swordBlue"), swordBlueLoc, Color.White * 255);

			}
			else
			{
				spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/Undertale/swordOrange"), swordBlueLoc, Color.White * 255);

			}
			if (modPlayer.undertaleiFrames < 0)
			{
				modPlayer.undertaleiFrames = 0;
			}

			if (Main.LocalPlayer.controlUp)
			{
				modPlayer.heartY-=3;
			}
			if (Main.LocalPlayer.controlDown)
			{
				modPlayer.heartY+=3;
			}
			if (Main.LocalPlayer.controlLeft)
			{
				modPlayer.heartX-=3;
			}
			if (Main.LocalPlayer.controlRight)
			{
				modPlayer.heartX+=3;
			}
			if (modPlayer.heartX < 64)
			{
				modPlayer.heartX = 64;
			}
			if (modPlayer.heartX > 719)
			{
				modPlayer.heartX = 719;
			}
			if (modPlayer.heartY < 34)
			{
				modPlayer.heartY = 34;
			}
			if (modPlayer.heartY > 290)
			{
				modPlayer.heartY = 290;
			}
			if(damageX1 < 700)
			{
				damageX1+= 4;
			}
			else
			{
				damageX1 = 50;
			}
			if (damageX2 > 64)
			{
				damageX2 -= 2;
			}
			else
			{
				damageX2 = 700;
			}
			if (damageXBlue < 660)
			{
				damageXBlue += 7;
			}
			else
			{
				coloredSword = Main.rand.Next(0,2);
				damageXBlue = 50;
			}
			
			if (modPlayer.heartX <= damageX1 + 18 && modPlayer.heartX >= damageX1 - 18 && modPlayer.heartY <= damageY1 + 190 && modPlayer.heartY >= damageY1 - 190 && modPlayer.undertaleiFrames <= 0)
			{
				modPlayer.undertaleiFrames += 60;
				modPlayer.Player.statLife -= 100;
				if (modPlayer.Player.statLife < 0)
				{
					modPlayer.Player.statLife = 1;
				}
				modPlayer.damageTakenInUndertale = true;
				if (Main.expertMode == true)
				{
					modPlayer.Player.AddBuff(BuffType<Buffs.Vulnerable>(), 1800);
				}
			}
			if (modPlayer.heartX <= damageXBlue + 3 && modPlayer.heartX >= damageXBlue - 3 && modPlayer.undertaleiFrames <= 0 && coloredSword == 0)
			{
				if (Main.LocalPlayer.controlUp || Main.LocalPlayer.controlDown || Main.LocalPlayer.controlRight || Main.LocalPlayer.controlLeft)
				{
					modPlayer.undertaleiFrames += 60;
					modPlayer.Player.statLife -= 100;
					if (modPlayer.Player.statLife < 0)
					{
						modPlayer.Player.statLife = 1;
					}
					modPlayer.damageTakenInUndertale = true;
					if (Main.expertMode == true)
					{
						modPlayer.Player.AddBuff(BuffType<Buffs.Vulnerable>(), 1800);
					}
				}
			}

			if (modPlayer.heartX <= damageXBlue + 3 && modPlayer.heartX >= damageXBlue - 3 && modPlayer.undertaleiFrames <= 0 && coloredSword == 1)
			{
				if (Main.LocalPlayer.controlUp || Main.LocalPlayer.controlDown || Main.LocalPlayer.controlRight || Main.LocalPlayer.controlLeft)
				{

				}
				else
				{
					modPlayer.Player.statLife -= 100;
					if (modPlayer.Player.statLife < 0)
					{
						modPlayer.Player.statLife = 1;
					}
					modPlayer.undertaleiFrames += 60;
					modPlayer.damageTakenInUndertale = true;
					if (Main.expertMode == true)
					{
						modPlayer.Player.AddBuff(BuffType<Buffs.Vulnerable>(), 1800);
					}
				}
			}
				
			
			if (modPlayer.heartX <= damageX2 + 18 && modPlayer.heartX >= damageX2 - 18 && modPlayer.heartY <= damageY2 + 190 && modPlayer.heartY >= damageY2 - 0 && modPlayer.undertaleiFrames <= 0)
			{
				
				modPlayer.Player.statLife -= 100;
				if (modPlayer.Player.statLife < 0)
				{
					modPlayer.Player.statLife = 1;
				}
				if (Main.expertMode == true)
				{
					modPlayer.Player.AddBuff(BuffType<Buffs.Vulnerable>(), 1800);
				}
				modPlayer.undertaleiFrames += 60;
				modPlayer.damageTakenInUndertale = true;
			}
			modPlayer.oldHeartX = modPlayer.heartX;
			modPlayer.oldHeartY = modPlayer.heartY;
			base.DrawSelf(spriteBatch);
			Recalculate();
		}
		public override void Update(GameTime gameTime)
		{
			if (!(Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().undertaleActive))
				return;

			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			modPlayer.Player.gravity = 0f;
			modPlayer.Player.velocity = Vector2.Zero;
			modPlayer.Player.immune = true;
			modPlayer.Player.immuneTime = 100;
			
			// Setting the text per tick to update and show our resource values.
			text.SetText($"HP {modPlayer.Player.statLife} / {modPlayer.Player.statLifeMax2}");

			base.Update(gameTime);
		}
	}
}
