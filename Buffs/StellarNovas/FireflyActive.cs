using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarNovas
{
    public class FireflyActive : ModBuff
    {
        public override void SetStaticDefaults()
        {
           
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        float modifier;
        public override void Update(Player player, ref int buffIndex)
        {
         
            if(player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer != 2)
            {
                player.GetModPlayer<StarsAbovePlayer>().novaGauge = 0;
            }
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
