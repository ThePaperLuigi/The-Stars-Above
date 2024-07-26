using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Celestial.Manifestation
{
    public class EGOManifestedBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("E.G.O. Manifestation");
            /* Description.SetDefault("Mastery over emotional turbulence has resulted in the physical manifestation of inner emotion" +
                "\nDamage increased by 30%" +
                "\nAttack speed increased by 40%" +
                "\nMovement speed increased by 90%" +
                "\nDefense reduced by 30"); */
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.3f;
            player.statDefense -= 30;
            player.GetAttackSpeed(DamageClass.Generic) += 0.4f;

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
