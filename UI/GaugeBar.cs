using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace StarsAbove.UI
{
    internal static class GaugeBar
    {
        public static void DrawHorizontal(SpriteBatch spriteBatch, Rectangle bounds, Color color)
        {
            Draw(spriteBatch, bounds, color, false);
        }

        public static void DrawVertical(SpriteBatch spriteBatch, Rectangle bounds, Color color)
        {
            Draw(spriteBatch, bounds, color, true);
        }

        private static void Draw(SpriteBatch spriteBatch, Rectangle bounds, Color color, bool vertical)
        {
            if (bounds.Width <= 0 || bounds.Height <= 0)
            {
                return;
            }

            int thickness = vertical ? bounds.Width : bounds.Height;
            Color shadow = Color.Lerp(color, new Color(0, 0, 0, color.A), 0.25f);
            Color shine = new Color(255, 255, 255, color.A);
            Color highlight = Color.Lerp(color, shine, 0.55f);

            DrawBand(spriteBatch, bounds, 0, thickness, shadow, vertical);
            DrawBand(spriteBatch, bounds, 0, thickness * 12 / 18, color, vertical);
            DrawBand(spriteBatch, bounds, thickness * 4 / 18, thickness * 8 / 18, highlight, vertical);
            DrawBand(spriteBatch, bounds, thickness * 8 / 18, thickness * 10 / 18, shine, vertical);
        }

        private static void DrawBand(SpriteBatch spriteBatch, Rectangle bounds, int start, int end, Color color, bool vertical)
        {
            if (end <= start)
            {
                return;
            }

            Rectangle band = vertical
                ? new Rectangle(bounds.X + start, bounds.Y, end - start, bounds.Height)
                : new Rectangle(bounds.X, bounds.Y + start, bounds.Width, end - start);
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, band, color);
        }
    }
}
