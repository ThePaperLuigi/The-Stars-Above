using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.VermillionDaemon
{
    public class Retribution : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Retribution");
            Description.SetDefault("Firing a barrage of spectral weapons" +
                "");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }

        public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {
            

            Vector2 shake = new Vector2(Main.rand.Next(-1, 2), Main.rand.Next(-1, 2));

            drawParams.Position += shake;
            drawParams.TextPosition += shake;
            

            return true;
        }
    }
}
