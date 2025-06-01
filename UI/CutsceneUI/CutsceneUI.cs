using System;
using Microsoft.Xna.Framework.Media;
using Terraria.UI;
using Terraria.GameContent;
using ReLogic.Content;
using StarsAbove.Systems;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI.CutsceneUI
{
    internal class CutsceneUI : UIState
	{
        // ----- Variables -----
        private float _cutsceneIntroAlpha = 0;
        private float _cutsceneExitAlpha = 0;
        private bool _anyCutsceneActive;

        // ----- UI Elements -----
        private UIElement _area;
		private UIVideo _edinGenesisQuasarVideo;
        private UIVideo _nalhaunCutsceneVideo;
        private UIVideo _tsukiCutsceneVideo;
        private UIVideo _tsukiCutsceneVideo2;
        private UIVideo _warriorCutsceneVideo;
        private UIVideo _warriorCutsceneVideo2;
        private UIVideo _starfarerIntroVideo;
        private UIVideo _asphodeneFFVideo;
        private UIVideo _eridaniFFVideo;

        // ----- Asset cache -----
        private readonly static Asset<Texture2D> _edinGenesisQuasarTransitionTexture = Request<Texture2D>("StarsAbove/UI/CutsceneUI/EdinGenesisQuasarTransition");

        // ----- Shorthands -----
        private static BossPlayer BossPlayer => Main.LocalPlayer.GetModPlayer<BossPlayer>();
        private static StarsAbovePlayer StarsAbovePlayer => Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();

        public override void OnInitialize() 
        {
            _area = new();
            _area.Top.Set(0f, 0f);
            _area.Left.Set(0f, 0f);
            _area.Width.Set(0f, 1f);
            _area.Height.Set(0f, 1f);
            Append(_area);

            _edinGenesisQuasarVideo = new UIVideo(Request<Video>("StarsAbove/Video/EdinGenesisQuasar"));
            _edinGenesisQuasarVideo.Top.Set(0f, 0f);
            _edinGenesisQuasarVideo.Left.Set(0f, 0f);
            _edinGenesisQuasarVideo.Width.Set(0f, 1f);
            _edinGenesisQuasarVideo.Height.Set(0f, 1f);

            _nalhaunCutsceneVideo = new UIVideo(Request<Video>("StarsAbove/Video/NalhaunBossCutscene"));
            _nalhaunCutsceneVideo.Top.Set(0f, 0f);
            _nalhaunCutsceneVideo.Left.Set(0f, 0f);
            _nalhaunCutsceneVideo.Width.Set(0f, 1f);
            _nalhaunCutsceneVideo.Height.Set(0f, 1f);

            _tsukiCutsceneVideo = new UIVideo(Request<Video>("StarsAbove/Video/TsukiyomiBossCutscene"));
            _tsukiCutsceneVideo.Top.Set(0f, 0f);
            _tsukiCutsceneVideo.Left.Set(0f, 0f);
            _tsukiCutsceneVideo.Width.Set(0f, 1f);
            _tsukiCutsceneVideo.Height.Set(0f, 1f);

            _tsukiCutsceneVideo2 = new UIVideo(Request<Video>("StarsAbove/Video/TsukiyomiNovaCutscene"));
            _tsukiCutsceneVideo2.Top.Set(0f, 0f);
            _tsukiCutsceneVideo2.Left.Set(0f, 0f);
            _tsukiCutsceneVideo2.Width.Set(0f, 1f);
            _tsukiCutsceneVideo2.Height.Set(0f, 1f);

            _warriorCutsceneVideo = new UIVideo(Request<Video>("StarsAbove/Video/WarriorIntroCutscene"));
            _warriorCutsceneVideo.Top.Set(0f, 0f);
            _warriorCutsceneVideo.Left.Set(0f, 0f);
            _warriorCutsceneVideo.Width.Set(0f, 1f);
            _warriorCutsceneVideo.Height.Set(0f, 1f);

            _warriorCutsceneVideo2 = new UIVideo(Request<Video>("StarsAbove/Video/WarriorFinalPhaseCutscene"));
            _warriorCutsceneVideo2.Top.Set(0f, 0f);
            _warriorCutsceneVideo2.Left.Set(0f, 0f);
            _warriorCutsceneVideo2.Width.Set(0f, 1f);
            _warriorCutsceneVideo2.Height.Set(0f, 1f);

            _starfarerIntroVideo = new UIVideo(Request<Video>("StarsAbove/Video/StarfarerIntroCutscene"));
            _starfarerIntroVideo.Top.Set(0f, 0f);
            _starfarerIntroVideo.Left.Set(0f, 0f);
            _starfarerIntroVideo.Width.Set(0f, 1f);
            _starfarerIntroVideo.Height.Set(0f, 1f);

            _asphodeneFFVideo = new UIVideo(Request<Video>("StarsAbove/Video/AsphodeneFFTransformation"));
            _asphodeneFFVideo.Top.Set(0f, 0f);
            _asphodeneFFVideo.Left.Set(0f, 0f);
            _asphodeneFFVideo.Width.Set(0f, 1f);
            _asphodeneFFVideo.Height.Set(0f, 1f);

            _eridaniFFVideo = new UIVideo(Request<Video>("StarsAbove/Video/EridaniFFTransformation"));
            _eridaniFFVideo.Top.Set(0f, 0f);
            _eridaniFFVideo.Left.Set(0f, 0f);
            _eridaniFFVideo.Width.Set(0f, 1f);
            _eridaniFFVideo.Height.Set(0f, 1f);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch) 
        {
            // Draw all the other UI Elements first.
			base.DrawSelf(spriteBatch);

            // Get the dimensions of the area
            CalculatedStyle dimensions = _area.GetInnerDimensions();

            // Draw Edin Genesis Quasar
            if (_cutsceneIntroAlpha > 0f)
            {
                // Draw a complete black screen
                spriteBatch.Draw(
                    TextureAssets.MagicPixel.Value,
                    dimensions.Position(),
                    new Rectangle(0, 0, (int)Math.Ceiling(dimensions.Width), (int)Math.Ceiling(dimensions.Height)),
                    Color.Black * _cutsceneIntroAlpha,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0);

                // Draw the texture
                spriteBatch.Draw(
                    _edinGenesisQuasarTransitionTexture.Value,
                    dimensions.Center(),
                    _edinGenesisQuasarTransitionTexture.Value.Bounds,
                    Color.White * _cutsceneIntroAlpha,
                    0f,
                    new Vector2(_edinGenesisQuasarTransitionTexture.Width() * 0.5f, _edinGenesisQuasarTransitionTexture.Height()),
                    1f / Main.UIScale,
                    SpriteEffects.None,
                    0);
            }

            // Draw White screen
            if (_cutsceneExitAlpha > 0f)
            {
                // Draw a complete white screen
                spriteBatch.Draw(
                    TextureAssets.MagicPixel.Value,
                    dimensions.Position(),
                    new Rectangle(0, 0, (int)Math.Ceiling(dimensions.Width), (int)Math.Ceiling(dimensions.Height)),
                    Color.White * _cutsceneExitAlpha,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0);
            }

            // Draw Black screen
            if (BossPlayer.BlackAlpha > 0f)
            {
                // Draw a complete black screen
                spriteBatch.Draw(
                    TextureAssets.MagicPixel.Value,
                    dimensions.Position(),
                    new Rectangle(0, 0, (int)Math.Ceiling(dimensions.Width), (int)Math.Ceiling(dimensions.Height)),
                    Color.Black * BossPlayer.BlackAlpha,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0);
            }

            // Draw White screen
            if (BossPlayer.WhiteAlpha > 0f)
            {
                // Draw a complete white screen
                spriteBatch.Draw(
                    TextureAssets.MagicPixel.Value,
                    dimensions.Position(),
                    new Rectangle(0, 0, (int)Math.Ceiling(dimensions.Width), (int)Math.Ceiling(dimensions.Height)),
                    Color.White * BossPlayer.WhiteAlpha,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0);
            }
        }

		public override void Update(GameTime gameTime)
        {
            _cutsceneIntroAlpha = MathHelper.Clamp(_cutsceneIntroAlpha, 0f, 1f);
            _cutsceneExitAlpha = MathHelper.Clamp(_cutsceneExitAlpha, 0f, 1f);

            bool introWhite = false; //If true, intro is white. If false, intro is black.
            bool outroWhite = false; //If true, outro is white. If false, outro is black.

            AstarteDriver();
            NalhaunCutscene();
            TsukiCutscene(ref introWhite, ref outroWhite);
            TsukiCutscene2(ref introWhite, ref outroWhite);
            WarriorCutscene1(ref introWhite, ref outroWhite);
            WarriorCutscene2(ref introWhite, ref outroWhite);
            IntroCutscene(ref introWhite, ref outroWhite);

            if (StarsAbovePlayer.chosenStarfarer == 1)
            {
                AsphodeneFF(ref introWhite, ref outroWhite);
            }
            else if (StarsAbovePlayer.chosenStarfarer == 2)
            {
                EridaniFF(ref introWhite, ref outroWhite);
            }

            if (BossPlayer.VideoDuration == 0)
            {
                if (outroWhite)
                {
                    BossPlayer.BlackAlpha = 0f;
                    BossPlayer.WhiteAlpha = 1f;
                }
                else
                {
                    BossPlayer.WhiteAlpha = 0f;
                    BossPlayer.BlackAlpha = 1f;
                }
            }

            BossPlayer.WhiteAlpha -= 0.01f;
            BossPlayer.BlackAlpha -= 0.01f;

            base.Update(gameTime);
        }

        private void AstarteDriver()
        {
            UIVideo Video = _edinGenesisQuasarVideo;
            if (StarsAbovePlayer.astarteCutsceneProgress <= 60 && StarsAbovePlayer.astarteCutsceneProgress > 0)
            {
                _cutsceneIntroAlpha += 0.05f;

            }

            _cutsceneExitAlpha -= 0.02f;
            if (StarsAbovePlayer.WhiteFade >= 20)
            {
                _cutsceneExitAlpha = 1f;

            }

            if (StarsAbovePlayer.astarteCutsceneProgress == 1)
            {
                _cutsceneIntroAlpha = 0f;
                _anyCutsceneActive = true;
                _edinGenesisQuasarVideo.FinishedVideo = false;
                _edinGenesisQuasarVideo.StartVideo = true;
                _area.Append(_edinGenesisQuasarVideo);
            }

            if (Video.FinishedVideo)
            {
                Video.FinishedVideo = false;
                _anyCutsceneActive = false;
                BossPlayer.WhiteAlpha = 1f;

                _edinGenesisQuasarVideo.Remove();
            }
        }
        
        private void NalhaunCutscene()
        {
            UIVideo Video = _nalhaunCutsceneVideo;
            var cutsceneProgress = BossPlayer.nalhaunCutsceneProgress;

            if (cutsceneProgress <= 60 && cutsceneProgress > 0)
            {
                BossPlayer.BlackAlpha += 0.1f;
            }
            
            if (cutsceneProgress == 1)
            {
                _anyCutsceneActive = true;

                Video.FinishedVideo = false;
                Video.StartVideo = true;
                _area.Append(Video);
            }

            if (Video.FinishedVideo)
            {
                Video.FinishedVideo = false;
                _anyCutsceneActive = false;

                BossPlayer.BlackAlpha = 1f;

                Video.Remove();
            }
        }

        private void TsukiCutscene(ref bool introWhite, ref bool outroWhite)
        {
            UIVideo Video = _tsukiCutsceneVideo;
            var cutsceneProgress = BossPlayer.tsukiCutsceneProgress;

            introWhite = true;
            outroWhite = false;

            if (cutsceneProgress <= 60 && cutsceneProgress > 0)
            {
                //If the cutscene hasn't started yet, give time for the screen to fade.
                if (introWhite)
                {
                    BossPlayer.WhiteAlpha += 0.1f;
                }
                else
                {
                    BossPlayer.BlackAlpha += 0.1f;
                }
            }

            if (cutsceneProgress == 1)
            {
                _anyCutsceneActive = true;

                Video.FinishedVideo = false;
                Video.StartVideo = true;

                _area.Append(Video);
            }

            if (Video.FinishedVideo)
            {
                Video.FinishedVideo = false;
                _anyCutsceneActive = false;

                BossPlayer.BlackAlpha = 1f;
                Video.Remove();
            }
        }

        private void TsukiCutscene2(ref bool introWhite, ref bool outroWhite)
        {
            UIVideo Video = _tsukiCutsceneVideo2;
            var cutsceneProgress = BossPlayer.tsukiCutscene2Progress;

            introWhite = false;
            outroWhite = true;

            if (cutsceneProgress <= 60 && cutsceneProgress > 0)
            {
                //If the cutscene hasn't started yet, give time for the screen to fade.
                if (introWhite)
                {
                    BossPlayer.WhiteAlpha += 0.1f;
                }
                else
                {
                    BossPlayer.BlackAlpha += 0.1f;
                }
            }

            if (cutsceneProgress == 1)
            {
                _anyCutsceneActive = true;

                Video.FinishedVideo = false;
                Video.StartVideo = true;

                _area.Append(Video);

            }

            if(Video.FinishedVideo)
            {
                Video.FinishedVideo = false;
                _anyCutsceneActive = false;

                BossPlayer.WhiteAlpha = 1f;
                Video.Remove();
            }
        }

        private void WarriorCutscene1(ref bool introWhite, ref bool outroWhite)
        {
            UIVideo Video = _warriorCutsceneVideo;
            var cutsceneProgress = BossPlayer.warriorCutsceneProgress;

            introWhite = true;
            outroWhite = true;

            if (cutsceneProgress <= 60 && cutsceneProgress > 0)
            {
                //If the cutscene hasn't started yet, give time for the screen to fade.
                if (introWhite)
                {
                    BossPlayer.WhiteAlpha += 0.1f;
                }
                else
                {
                    BossPlayer.BlackAlpha += 0.1f;
                }


            }

            if (cutsceneProgress == 1)
            {
                _anyCutsceneActive = true;

                Video.FinishedVideo = false;
                Video.StartVideo = true;

                _area.Append(Video);
            }

            if (Video.FinishedVideo)
            {
                Video.FinishedVideo = false;
                _anyCutsceneActive = false;

                BossPlayer.WhiteAlpha = 1f;
                Video.Remove();
            }
        }

        private void WarriorCutscene2(ref bool introWhite, ref bool outroWhite)
        {
            UIVideo Video = _warriorCutsceneVideo2;
            var cutsceneProgress = BossPlayer.warriorCutsceneProgress2;

            introWhite = true;
            outroWhite = true;

            if (cutsceneProgress <= 60 && cutsceneProgress > 0)
            {
                //If the cutscene hasn't started yet, give time for the screen to fade.
                if (introWhite)
                {
                    BossPlayer.WhiteAlpha += 0.1f;
                }
                else
                {
                    BossPlayer.BlackAlpha += 0.1f;
                }
            }

            if (cutsceneProgress == 1)
            {
                _anyCutsceneActive = true;

                Video.FinishedVideo = false;
                Video.StartVideo = true;

                _area.Append(Video);
            }

            if (Video.FinishedVideo)
            {
                Video.FinishedVideo = false;
                _anyCutsceneActive = false;

                BossPlayer.WhiteAlpha = 1f;
                Video.Remove();
            }
        }

        private void IntroCutscene(ref bool introWhite, ref bool outroWhite)
        {
            UIVideo Video = _starfarerIntroVideo;
            var cutsceneProgress = BossPlayer.introCutsceneProgress;

            introWhite = true;
            outroWhite = true;

            if (cutsceneProgress <= 60 && cutsceneProgress > 0)
            {
                //If the cutscene hasn't started yet, give time for the screen to fade.
                if (introWhite)
                {
                    BossPlayer.WhiteAlpha += 0.1f;
                }
                else
                {
                    BossPlayer.BlackAlpha += 0.1f;
                }
            }

            if (cutsceneProgress == 1)
            {
                _anyCutsceneActive = true;

                Video.FinishedVideo = false;
                Video.StartVideo = true;
                StarsAbovePlayer.seenIntroCutscene = true;
                _area.Append(Video);
            }

            if (Video.FinishedVideo)
            {
                Video.FinishedVideo = false;
                _anyCutsceneActive = false;

                BossPlayer.WhiteAlpha = 1f;
                Video.Remove();
            }
        }

        private void AsphodeneFF(ref bool introWhite, ref bool outroWhite)
        {
            UIVideo Video = _asphodeneFFVideo;
            var cutsceneProgress = BossPlayer.ffCutsceneProgress;

            introWhite = false;
            outroWhite = true;

            if (cutsceneProgress <= 60 && cutsceneProgress > 0)
            {
                //If the cutscene hasn't started yet, give time for the screen to fade.
                if (introWhite)
                {
                    BossPlayer.WhiteAlpha += 0.1f;
                }
                else
                {
                    BossPlayer.BlackAlpha += 0.1f;
                }
            }

            if (cutsceneProgress == 1)
            {
                _anyCutsceneActive = true;

                Video.FinishedVideo = false;
                Video.StartVideo = true;
                _area.Append(Video);
            }

            if (Video.FinishedVideo)
            {
                Video.FinishedVideo = false;
                _anyCutsceneActive = false;

                BossPlayer.WhiteAlpha = 1f;
                Video.Remove();
            }
        }

        private void EridaniFF(ref bool introWhite, ref bool outroWhite)
        {
            UIVideo Video = _eridaniFFVideo;
            var cutsceneProgress = BossPlayer.ffCutsceneProgress;

            introWhite = false;
            outroWhite = true;

            if (cutsceneProgress <= 60 && cutsceneProgress > 0)
            {//If the cutscene hasn't started yet, give time for the screen to fade.

                if (introWhite)
                {
                    BossPlayer.WhiteAlpha += 0.1f;
                }
                else
                {
                    BossPlayer.BlackAlpha += 0.1f;
                }
            }

            if (cutsceneProgress == 1)
            {
                _anyCutsceneActive = true;

                Video.FinishedVideo = false;
                Video.StartVideo = true;
                _area.Append(Video);
            }

            if (Video.FinishedVideo)
            {
                Video.FinishedVideo = false;
                _anyCutsceneActive = false;

                BossPlayer.WhiteAlpha = 1f;
                Video.Remove();
            }
        }
    }
}
