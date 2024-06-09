using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Gores
{
    public class NeonVeilLeaves : ModGore
    {
        public override string Texture => "StarsAbove/Gores/NeonVeilLeaves";

        public override void SetStaticDefaults()
        {
            ChildSafety.SafeGore[Type] = true; // Leaf gore should appear regardless of the "Blood and Gore" setting
            GoreID.Sets.SpecialAI[Type] = 3; // Falling leaf behavior
            GoreID.Sets.PaintedFallingLeaf[Type] = true; // This is used for all vanilla tree leaves, related to the bigger spritesheet for tile paints
            
        }
        public override Color? GetAlpha(Gore gore, Color lightColor)
        {
            return Color.LightYellow;
        }
    }
}