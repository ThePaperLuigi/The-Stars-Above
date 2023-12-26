
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
using Microsoft.Xna.Framework.Media;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Systems;
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
        private UIVideo tsukiCutsceneVideo2;
        private UIVideo warriorCutsceneVideo;
        private UIVideo warriorCutsceneVideo2;
        private UIVideo starfarerIntroVideo;

        public override void OnInitialize() {
			
			area = new UIElement();
			area.Top.Set(0, 0f);
			area.Left.Set(0, 0f);
			area.Width.Set(Main.screenWidth, 1f);
			area.Height.Set(Main.screenHeight, 1f);
			area.HAlign = area.VAlign = 0.5f; // 1

			edinGenesisQuasarTransition = new UIImage(Request<Texture2D>("StarsAbove/UI/CutsceneUI/EdinGenesisQuasarTransition"));
			edinGenesisQuasarTransition.Width.Set(Main.screenWidth, 1f);
			edinGenesisQuasarTransition.Height.Set(Main.screenHeight, 1f);

            edinGenesisQuasarVideo = new UIVideo(Request<Video>("StarsAbove/Video/EdinGenesisQuasar"))
            {
                ScaleToFit = true,
                WaitForStart = true,
                DoLoop = false
            };
            edinGenesisQuasarVideo.Width.Set(Main.screenWidth, 1f);
            edinGenesisQuasarVideo.Height.Set(Main.screenHeight, 1f);

            nalhaunCutsceneVideo = new UIVideo(Request<Video>("StarsAbove/Video/NalhaunBossCutscene"))
            {
                ScaleToFit = true,
                WaitForStart = true,
                DoLoop = false
            };
            nalhaunCutsceneVideo.Width.Set(Main.screenWidth, 1f);
            nalhaunCutsceneVideo.Height.Set(Main.screenHeight, 1f);

            tsukiCutsceneVideo = new UIVideo(Request<Video>("StarsAbove/Video/TsukiyomiBossCutscene"))
            {
                ScaleToFit = true,
                WaitForStart = true,
                DoLoop = false
            };
            tsukiCutsceneVideo.Width.Set(Main.screenWidth, 1f);
            tsukiCutsceneVideo.Height.Set(Main.screenHeight, 1f);

            tsukiCutsceneVideo2 = new UIVideo(Request<Video>("StarsAbove/Video/TsukiyomiNovaCutscene"))
            {
                ScaleToFit = true,
                WaitForStart = true,
                DoLoop = false
            };
            tsukiCutsceneVideo2.Width.Set(Main.screenWidth, 1f);
            tsukiCutsceneVideo2.Height.Set(Main.screenHeight, 1f);

            warriorCutsceneVideo = new UIVideo(Request<Video>("StarsAbove/Video/WarriorIntroCutscene"))
            {
                ScaleToFit = true,
                WaitForStart = true,
                DoLoop = false
            };
            warriorCutsceneVideo.Width.Set(Main.screenWidth, 1f);
            warriorCutsceneVideo.Height.Set(Main.screenHeight, 1f);

            warriorCutsceneVideo2 = new UIVideo(Request<Video>("StarsAbove/Video/WarriorFinalPhaseCutscene"))
            {
                ScaleToFit = true,
                WaitForStart = true,
                DoLoop = false
            };
            warriorCutsceneVideo2.Width.Set(Main.screenWidth, 1f);
            warriorCutsceneVideo2.Height.Set(Main.screenHeight, 1f);

            starfarerIntroVideo = new UIVideo(Request<Video>("StarsAbove/Video/StarfarerIntroCutscene"))
            {
                ScaleToFit = true,
                WaitForStart = true,
                DoLoop = false
            };
            starfarerIntroVideo.Width.Set(Main.screenWidth, 1f);
            starfarerIntroVideo.Height.Set(Main.screenHeight, 1f);

            //area.Append(edinGenesisQuasarVideo);
            Append(area);
		}

		public override void Draw(SpriteBatch spriteBatch) {
			base.Draw(spriteBatch);
		}

		float CutsceneIntroAlpha = 0;
		float CutsceneExitAlpha = 0;
		//bool CutsceneExit = false;
		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);
            var bossPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

            var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CutsceneUI/EdinGenesisQuasarTransition"), area.GetInnerDimensions().ToRectangle(), Color.White * CutsceneIntroAlpha);
			spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CutsceneUI/WhiteScreen"), area.GetInnerDimensions().ToRectangle(), Color.White * CutsceneExitAlpha);

            spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CutsceneUI/BlackScreen"), area.GetInnerDimensions().ToRectangle(), Color.White * bossPlayer.BlackAlpha);

            spriteBatch.Draw((Texture2D)Request<Texture2D>("StarsAbove/UI/CutsceneUI/WhiteScreen"), area.GetInnerDimensions().ToRectangle(), Color.White * bossPlayer.WhiteAlpha);
        }
        bool alphaFade;

        bool anyCutsceneActive;
		public override void Update(GameTime gameTime)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
            var bossPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

            CutsceneIntroAlpha = MathHelper.Clamp(CutsceneIntroAlpha, 0f, 1f);
            CutsceneExitAlpha = MathHelper.Clamp(CutsceneExitAlpha, 0f, 1f);

            bool introWhite = false; //If true, intro is white. If false, intro is black.
            bool outroWhite = false; //If true, outro is white. If false, outro is black.

            AstarteDriver(modPlayer);
            NalhaunCutscene(bossPlayer);
            TsukiCutscene(bossPlayer, ref introWhite, ref outroWhite);
            TsukiCutscene2(bossPlayer, ref introWhite, ref outroWhite);
            WarriorCutscene1(bossPlayer, ref introWhite, ref outroWhite);
            WarriorCutscene2(bossPlayer, ref introWhite, ref outroWhite);
            IntroCutscene(bossPlayer, ref introWhite, ref outroWhite);

            if (bossPlayer.VideoDuration == 0)
            {
                if (outroWhite)
                {
                    bossPlayer.BlackAlpha = 0f;
                    bossPlayer.WhiteAlpha = 1f;
                }
                else
                {
                    bossPlayer.WhiteAlpha = 0f;
                    bossPlayer.BlackAlpha = 1f;

                }


            }
            if(anyCutsceneActive)
            {
                //Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().gaussianBlurProgress = 1f;
            }


            bossPlayer.WhiteAlpha -= 0.01f;
            bossPlayer.BlackAlpha -= 0.01f;
            
            

            base.Update(gameTime);
        }

        private void AstarteDriver(StarsAbovePlayer modPlayer)
        {
            UIVideo Video = edinGenesisQuasarVideo;
            bool CutsceneExit = false;
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
                anyCutsceneActive = true;
                edinGenesisQuasarVideo.FinishedVideo = false;
                edinGenesisQuasarVideo.StartVideo = true;
                area.Append(edinGenesisQuasarVideo);


            }
            if (Video.FinishedVideo)
            {
                Video.FinishedVideo = false;
                anyCutsceneActive = false;
                Main.LocalPlayer.GetModPlayer<BossPlayer>().WhiteAlpha = 1f;

                edinGenesisQuasarVideo.Remove();
            }
        }
        
        private void NalhaunCutscene(BossPlayer modPlayer)
        {
            UIVideo Video = nalhaunCutsceneVideo;
            var cutsceneProgress = modPlayer.nalhaunCutsceneProgress;
            if (cutsceneProgress <= 60 && cutsceneProgress > 0)
            {
                modPlayer.BlackAlpha += 0.1f;
            }
            else
            {
                //modPlayer.BlackAlpha -= 0.1f;
            }
            
            if (cutsceneProgress == 1)
            {
                anyCutsceneActive = true;

                Video.FinishedVideo = false;
                Video.StartVideo = true;
                area.Append(Video);

            }
            if (Video.FinishedVideo)
            {
                Video.FinishedVideo = false;
                anyCutsceneActive = false;

                modPlayer.BlackAlpha = 1f;

                Video.Remove();
            }
        }
        private void TsukiCutscene(BossPlayer modPlayer, ref bool introWhite, ref bool outroWhite)
        {
            UIVideo Video = tsukiCutsceneVideo;
            var cutsceneProgress = modPlayer.tsukiCutsceneProgress;

            introWhite = true;
            outroWhite = false;

            if (cutsceneProgress <= 60 && cutsceneProgress > 0)
            {//If the cutscene hasn't started yet, give time for the screen to fade.

                if (introWhite)
                {
                    modPlayer.WhiteAlpha += 0.1f;
                }
                else
                {
                    modPlayer.BlackAlpha += 0.1f;
                }


            }
            else
            {

            }
            if (cutsceneProgress == 1)
            {
                anyCutsceneActive = true;

                Video.FinishedVideo = false;
                Video.StartVideo = true;

                area.Append(Video);

            }
            if (Video.FinishedVideo)
            {
                Video.FinishedVideo = false;
                anyCutsceneActive = false;

                modPlayer.BlackAlpha = 1f;
                Video.Remove();
            }
        }
        private void TsukiCutscene2(BossPlayer modPlayer, ref bool introWhite, ref bool outroWhite)
        {
            UIVideo Video = tsukiCutsceneVideo2;
            var cutsceneProgress = modPlayer.tsukiCutscene2Progress;

            introWhite = false;
            outroWhite = true;

            if (cutsceneProgress <= 60 && cutsceneProgress > 0)
            {//If the cutscene hasn't started yet, give time for the screen to fade.

                if (introWhite)
                {
                    modPlayer.WhiteAlpha += 0.1f;
                }
                else
                {
                    modPlayer.BlackAlpha += 0.1f;
                }


            }
            else
            {

            }
            if (cutsceneProgress == 1)
            {
                anyCutsceneActive = true;

                Video.FinishedVideo = false;
                Video.StartVideo = true;

                area.Append(Video);

            }
            if(Video.FinishedVideo)
            {
                Video.FinishedVideo = false;
                anyCutsceneActive = false;

                modPlayer.WhiteAlpha = 1f;
                Video.Remove();
            }
            
            
        }
        private void WarriorCutscene1(BossPlayer modPlayer, ref bool introWhite, ref bool outroWhite)
        {
            UIVideo Video = warriorCutsceneVideo;
            var cutsceneProgress = modPlayer.warriorCutsceneProgress;

            introWhite = true;
            outroWhite = true;


            if (cutsceneProgress <= 60 && cutsceneProgress > 0)
            {//If the cutscene hasn't started yet, give time for the screen to fade.

                if (introWhite)
                {
                    modPlayer.WhiteAlpha += 0.1f;
                }
                else
                {
                    modPlayer.BlackAlpha += 0.1f;
                }


            }
            else
            {

            }
            if (cutsceneProgress == 1)
            {
                anyCutsceneActive = true;

                Video.FinishedVideo = false;
                Video.StartVideo = true;

                area.Append(Video);

            }
            if (Video.FinishedVideo)
            {
                Video.FinishedVideo = false;
                anyCutsceneActive = false;

                modPlayer.WhiteAlpha = 1f;
                Video.Remove();
            }


        }
        private void WarriorCutscene2(BossPlayer modPlayer, ref bool introWhite, ref bool outroWhite)
        {
            UIVideo Video = warriorCutsceneVideo2;
            var cutsceneProgress = modPlayer.warriorCutsceneProgress2;

            introWhite = true;
            outroWhite = true;


            if (cutsceneProgress <= 60 && cutsceneProgress > 0)
            {//If the cutscene hasn't started yet, give time for the screen to fade.

                if (introWhite)
                {
                    modPlayer.WhiteAlpha += 0.1f;
                }
                else
                {
                    modPlayer.BlackAlpha += 0.1f;
                }


            }
            else
            {

            }
            if (cutsceneProgress == 1)
            {
                anyCutsceneActive = true;

                Video.FinishedVideo = false;
                Video.StartVideo = true;

                area.Append(Video);

            }
            if (Video.FinishedVideo)
            {
                Video.FinishedVideo = false;
                anyCutsceneActive = false;

                modPlayer.WhiteAlpha = 1f;
                Video.Remove();
            }


        }
        private void IntroCutscene(BossPlayer modPlayer, ref bool introWhite, ref bool outroWhite)
        {
            UIVideo Video = starfarerIntroVideo;
            var cutsceneProgress = modPlayer.introCutsceneProgress;

            introWhite = true;
            outroWhite = true;


            if (cutsceneProgress <= 60 && cutsceneProgress > 0)
            {//If the cutscene hasn't started yet, give time for the screen to fade.

                if (introWhite)
                {
                    modPlayer.WhiteAlpha += 0.1f;
                }
                else
                {
                    modPlayer.BlackAlpha += 0.1f;
                }


            }
            else
            {

            }
            if (cutsceneProgress == 1)
            {
                anyCutsceneActive = true;

                Video.FinishedVideo = false;
                Video.StartVideo = true;
                Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenIntroCutscene = true;
                area.Append(Video);

            }
            if (Video.FinishedVideo)
            {
                Video.FinishedVideo = false;
                anyCutsceneActive = false;

                modPlayer.WhiteAlpha = 1f;
                Video.Remove();
            }


        }
    }
}
