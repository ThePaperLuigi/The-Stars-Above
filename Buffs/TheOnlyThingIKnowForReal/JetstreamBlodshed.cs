using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.TheOnlyThingIKnowForReal
{
    public class JetstreamBloodshed : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jetstream Bloodshed");
            Description.SetDefault("Damage is increased by 30% until damage is taken" +
                "\nIncreased to 50% at 100 HP or below");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.thorns = 1f;
            
            player.GetDamage(DamageClass.Generic) += 0.3f;
            if(player.statLife<=100)
            {
                player.GetDamage(DamageClass.Generic) += 0.2f;

            }
            //player.statDefense -= 30;
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
