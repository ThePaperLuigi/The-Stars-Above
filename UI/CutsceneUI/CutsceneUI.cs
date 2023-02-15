
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Items;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Microsoft.Xna.Framework.Media;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Systems;

namespace StarsAbove.UI.CutsceneUI
{
	internal class CutsceneUI : UIState
	{
		private UIElement area;
		private UIImage edinGenesisQuasarTransition;
		private UIVideo edinGenesisQuasarVideo;
		public override void OnInitialize() {
			
			area = new UIElement();
			area.Top.Set(0, 0f);
			area.Left.Set(0, 0f);
			area.Width.Set(Main.screenWidth, 1f);
			area.Height.Set(Main.screenHeight, 1f);
			area.HAlign = area.VAlign = 0.5f; // 1

			edinGenesisQuasarTransition = new UIImage(Request<Texture2D>("StarsAbove/UI/CutsceneUI/EdinGenesisQuasarTransition"));
			edinGenesisQuasarTransition.Width.Set(Main.screenWidth, 0f);
			edinGenesisQuasarTransition.Height.Set(Main.screenHeight, 0f);

			edinGenesisQuasarVideo = new UIVideo(Request<Video>("StarsAbove/Video/EdinGenesisQuasar"));
			edinGenesisQuasarVideo.ScaleToFit = true;
			edinGenesisQuasarVideo.WaitForStart = true;
			edinGenesisQuasarVideo.DoLoop = false;

			//area.Append(edinGenesisQuasarVideo);
			Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch) {
			base.Draw(spriteBatch);
		}

		float CutsceneIntroAlpha = 0;
		float CutsceneExitAlpha = 0;
		bool CutsceneExit = false;
		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);

			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CutsceneUI/EdinGenesisQuasarTransition"), area.GetInnerDimensions().ToRectangle(), Color.White * CutsceneIntroAlpha);
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CutsceneUI/EdinGenesisQuasarTransition2"), area.GetInnerDimensions().ToRectangle(), Color.White * CutsceneExitAlpha);

		}
		public override void Update(GameTime gameTime) {
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

			CutsceneIntroAlpha = MathHelper.Clamp(CutsceneIntroAlpha, 0f, 1f);
			CutsceneExitAlpha = MathHelper.Clamp(CutsceneExitAlpha, 0f, 1f);

			if (modPlayer.astarteCutsceneProgress <= 60 && modPlayer.astarteCutsceneProgress > 0)
			{
				CutsceneIntroAlpha += 0.05f;
				
			}
			
			CutsceneExitAlpha -= 0.01f;

			if (modPlayer.astarteCutsceneProgress == 1)
            {
				CutsceneIntroAlpha = 0f;
				
				edinGenesisQuasarVideo.FinishedVideo = false;
				edinGenesisQuasarVideo.StartVideo = true;
				CutsceneExit = true;
				area.Append(edinGenesisQuasarVideo);
				
			}		
			if(edinGenesisQuasarVideo.FinishedVideo)
            {
				if(CutsceneExit)
                {
					CutsceneExitAlpha = 1f;
					CutsceneExit = false;
                }
				
				edinGenesisQuasarVideo.Remove();
            }


			base.Update(gameTime);
		}
	}
}
