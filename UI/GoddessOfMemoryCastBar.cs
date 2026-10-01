using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Systems;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.UI
{
    internal class GoddessOfMemoryCastBar : UIState
    {
        private UIText text;
        private UIElement area;
        private UIImage barFrame;
        private Color gradientA;
        private Color gradientB;

        public override void OnInitialize()
        {
            area = new UIElement();
            area.Top.Set(200, 0f);
            area.Width.Set(198, 0f);
            area.Height.Set(60, 0f);
            area.HAlign = area.VAlign = 0.5f;

            barFrame = new UIImage(Request<Texture2D>("StarsAbove/UI/GoddessOfMemoryCastBarFrame"));
            barFrame.Left.Set(0, 0f);
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(198, 0f);
            barFrame.Height.Set(40, 0f);

            text = new UIText("", 1.5f);
            text.Height.Set(34, 0f);
            text.Top.Set(40, 0f);
            text.HAlign = 0.5f;

            gradientA = new Color(249, 133, 36);
            gradientB = new Color(255, 166, 83);

            barFrame.Append(text);
            area.Append(barFrame);
            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();
            if (!modPlayer.GoddessOfMemoryBarActive)
            {
                return;
            }

            base.Draw(spriteBatch);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();
            float quotient = modPlayer.CastTimeMax > 0 ? (float)modPlayer.CastTime / modPlayer.CastTimeMax : 0f;
            quotient = Utils.Clamp(quotient, 0f, 1f);

            Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
            hitbox.X += 42;
            hitbox.Y += 14;
            hitbox.Width = 112;
            hitbox.Height = 12;

            int steps = (int)(hitbox.Width * quotient);
            for (int i = 0; i < steps; i++)
            {
                float percent = (float)i / hitbox.Width;
                GaugeBar.DrawHorizontal(spriteBatch, new Rectangle(hitbox.X + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
            }
        }

        public override void Update(GameTime gameTime)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();
            if (!modPlayer.GoddessOfMemoryBarActive)
            {
                return;
            }

            text.SetText(string.IsNullOrWhiteSpace(modPlayer.NextAttack) ? "" : $"[c/FF9D4B:{modPlayer.NextAttack}]");
            text.Recalculate();
            base.Update(gameTime);
        }
    }
}
