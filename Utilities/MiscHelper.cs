using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.DataStructures;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StarsAbove.Utilities;

internal static class MiscHelper
{
    // thanks andre the stinki
    /// <inheritdoc cref="DrawLine(Vector2, Vector2, Texture2D, Rectangle, Rectangle, Rectangle, Color, float, float, SpriteEffects, bool)"/>
    /// <param name="sourceRect">The sprite rectangle to be used for the whole laser.</param>
    public static void DrawLine(Vector2 from, Vector2 to, Texture2D texture, Rectangle? sourceRect, Color color, float scale = 1, float initialRotationOffset = 0, SpriteEffects effects = SpriteEffects.None, bool isEntity = true)
    {
        Rectangle rect = sourceRect ?? texture.Bounds;
        DrawLine(from, to, texture, rect, rect, rect, color, scale, initialRotationOffset, effects, isEntity);
    }

    /// <summary>
    /// Draws a line using the specified texture.
    /// </summary>
    /// <param name="from">The start position.</param>
    /// <param name="to">The end position.</param>
    /// <param name="texture">The texture to draw. <br />The laser draws horizontally and assumes the texture<br />is <b>horizontal</b> when rotation = 0.</param>
    /// <param name="begin">The texture rectangle of the start of the line.</param>
    /// <param name="middle">The body of the line, this texture will be repeated until the end of the laser.</param>
    /// <param name="end">The final texture, drawn at the end of the laser.</param>
    /// <param name="color">Color mask for the parts of the laser..</param>
    /// <param name="scale">Scale for the parts of the laser.</param>
    /// <param name="initialRotationOffset">Rotation offset for the parts of the laser..</param>
    /// <param name="effects"><see cref="SpriteEffects"/> for the parts of the laser.</param>
    /// <param name="isEntity">If <see langword="true"/> it will call <see cref="Main.EntitySpriteDraw(DrawData)"/>, otherwise it will call Main.spritebatch.Draw.</param>
    public static void DrawLine(Vector2 from, Vector2 to, Texture2D texture, Rectangle begin, Rectangle middle, Rectangle end,
        Color color, float scale = 1, float initialRotationOffset = 0, SpriteEffects effects = SpriteEffects.None, bool isEntity = true)
    {
        float angle = from.AngleTo(to) + initialRotationOffset;
        // start
        DrawData data = new(texture, from - Main.screenPosition, begin, color, angle, begin.Size() / 2, scale, effects);
        DoDraw(ref data);

        // middle
        float totalDistance = from.Distance(to);
        float elapsed = begin.Height;
        float step = middle.Height;
        Vector2 unit = from.DirectionTo(to);
        for (; elapsed < totalDistance; elapsed += step)
        {
            Vector2 position = from + unit * elapsed - Main.screenPosition;
            data = new(texture, position, middle, color, angle, begin.Size() / 2, scale, effects);
            DoDraw(ref data);
        }

        // end
        Vector2 pos = from + unit * (elapsed - step) - Main.screenPosition;
        data = new(texture, pos, end, color, angle, begin.Size() / 2, scale, effects);
        DoDraw(ref data);

        void DoDraw(ref DrawData data)
        {
            if (isEntity)
                Main.EntitySpriteDraw(data);
            else
                data.Draw(Main.spriteBatch);
        }
    }
}