
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
        private UIVideo nalhaunCutsceneVideo;
        private UIVideo tsukiCutsceneVideo;
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

            edinGenesisQuasarVideo = new UIVideo(Request<Video>("StarsAbove/Video/EdinGenesisQuasar"))
            {
                ScaleToFit = true,
                WaitForStart = true,
                DoLoop = false
            };

            nalhaunCutsceneVideo = new UIVideo(Request<Video>("StarsAbove/Video/NalhaunBossCutscene"))
            {
                ScaleToFit = true,
                WaitForStart = true,
                DoLoop = false
            };
            tsukiCutsceneVideo = new UIVideo(Request<Video>("StarsAbove/Video/TsukiyomiBossCutscene"))
            {
                ScaleToFit = true,
                WaitForStart = true,
                DoLoop = false
            };

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
            var bossPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

            var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CutsceneUI/EdinGenesisQuasarTransition"), area.GetInnerDimensions().ToRectangle(), Color.White * CutsceneIntroAlpha);
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CutsceneUI/WhiteScreen"), area.GetInnerDimensions().ToRectangle(), Color.White * CutsceneExitAlpha);

            spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CutsceneUI/BlackScreen"), area.GetInnerDimensions().ToRectangle(), Color.White * bossPlayer.BlackAlpha);

            spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CutsceneUI/WhiteScreen"), area.GetInnerDimensions().ToRectangle(), Color.White * bossPlayer.WhiteAlpha);
        }
		public override void Update(GameTime gameTime)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
            var bossPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

            CutsceneIntroAlpha = MathHelper.Clamp(CutsceneIntroAlpha, 0f, 1f);
            CutsceneExitAlpha = MathHelper.Clamp(CutsceneExitAlpha, 0f, 1f);

            AstarteDriver(modPlayer);
            NalhaunCutscene(bossPlayer);
            TsukiCutscene(bossPlayer);

            base.Update(gameTime);
        }

        private void AstarteDriver(StarsAbovePlayer modPlayer)
        {
            if (modPlayer.astarteCutsceneProgress <= 60 && modPlayer.astarteCutsceneProgress > 0)
            {
                CutsceneIntroAlpha += 0.05f;

            }

            CutsceneExitAlpha -= 0.02f;
            if (modPlayer.WhiteFade >= 20)
            {
                CutsceneExitAlpha = 1f;

            }


            if (modPlayer.astarteCutsceneProgress == 1)
            {
                CutsceneIntroAlpha = 0f;

                edinGenesisQuasarVideo.FinishedVideo = false;
                edinGenesisQuasarVideo.StartVideo = true;
                CutsceneExit = true;
                area.Append(edinGenesisQuasarVideo);

            }
            if (edinGenesisQuasarVideo.FinishedVideo)
            {
                if (CutsceneExit)
                {
                    CutsceneExitAlpha = 1f;
                    CutsceneExit = false;
                }

                edinGenesisQuasarVideo.Remove();
            }
        }
        
        private void NalhaunCutscene(BossPlayer modPlayer)
        {
            UIVideo Video = nalhaunCutsceneVideo;
            var cutsceneProgress = modPlayer.nalhaunCutsceneProgress;

            if (cutsceneProgress <= 60 && cutsceneProgress > 0)
            {
                modPlayer.BlackAlpha += 0.05f;
            }
            else
            {
                modPlayer.BlackAlpha -= 0.1f;
            }
            
            if (cutsceneProgress == 1)
            {

                Video.FinishedVideo = false;
                Video.StartVideo = true;
                CutsceneExit = true;
                area.Append(Video);

            }
            if (Video.FinishedVideo)
            {
                if (CutsceneExit)
                {
                    modPlayer.BlackAlpha = 1f;
                    CutsceneExit = false;
                }

                Video.Remove();
            }
        }
        private void TsukiCutscene(BossPlayer modPlayer)
        {
            UIVideo Video = tsukiCutsceneVideo;
            var cutsceneProgress = modPlayer.tsukiCutsceneProgress;
            bool introWhite = true; //If true, intro is white. If false, intro is black.
            bool outroWhite = false; //If true, outro is white. If false, outro is black.

            if (cutsceneProgress <= 60 && cutsceneProgress > 0)
            {//If the cutscene hasn't started yet, give time for the screen to fade.

                if(introWhite)
                {
                    modPlayer.WhiteAlpha += 0.05f;
                }
                else
                {
                    modPlayer.BlackAlpha += 0.05f;
                }
                
                
            }
            else
            {
                modPlayer.WhiteAlpha -= 0.05f;
                modPlayer.BlackAlpha -= 0.05f;
            }





            if (cutsceneProgress == 1)
            {

                Video.FinishedVideo = false;
                Video.StartVideo = true;
                CutsceneExit = true;
                area.Append(Video);

            }
            if (Video.FinishedVideo)
            {
                if (CutsceneExit)
                {
                    if(outroWhite)
                    {
                        modPlayer.WhiteAlpha = 1f;
                    }
                    else
                    {
                        modPlayer.BlackAlpha = 1f;

                    }
                    CutsceneExit = false;
                }

                Video.Remove();
            }
        }
    }
}
