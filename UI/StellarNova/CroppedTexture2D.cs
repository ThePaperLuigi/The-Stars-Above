using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarsAbove.UI.StellarNova
{
    public struct CroppedTexture2D
    {
        public Texture2D Texture { get; }
        public Rectangle Rectangle { get; set; }
        public Color Color { get; set; }

        public static readonly CroppedTexture2D Empty = new CroppedTexture2D();

        public CroppedTexture2D(Texture2D texture) : this(texture, Color.White, texture.Bounds) { }

        public CroppedTexture2D(Texture2D texture, Color color) : this(texture, color, texture.Bounds) { }

        public CroppedTexture2D(Texture2D texture, Rectangle rectangle) : this(texture, Color.White, rectangle) { }

        public CroppedTexture2D(Texture2D texture, Color color, Rectangle rectangle)
        {
            Texture = texture;
            Color = color;
            Rectangle = rectangle;
        }

        public static bool operator ==(CroppedTexture2D ct1, CroppedTexture2D ct2)
        {
            return ct1.Texture == ct2.Texture &&
                   ct1.Rectangle == ct2.Rectangle &&
                   ct1.Color == ct2.Color;
        }

        public static bool operator !=(CroppedTexture2D ct1, CroppedTexture2D ct2)
        {
            return ct1.Texture != ct2.Texture ||
                   ct1.Rectangle != ct2.Rectangle ||
                   ct1.Color != ct2.Color;
        }

        public override bool Equals(object obj)
        {
            return obj is CroppedTexture2D ct &&
                   EqualityComparer<Texture2D>.Default.Equals(Texture, ct.Texture) &&
                   Rectangle.Equals(ct.Rectangle) &&
                   Color.Equals(ct.Color);
        }

        public override int GetHashCode()
        {
            var hashCode = -893046046;
            hashCode = hashCode * -1521134295 + EqualityComparer<Texture2D>.Default.GetHashCode(Texture);
            hashCode = hashCode * -1521134295 + EqualityComparer<Rectangle>.Default.GetHashCode(Rectangle);
            return hashCode;
        }
    }
}
