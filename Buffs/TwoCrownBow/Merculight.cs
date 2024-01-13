using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.TwoCrownBow
{
    public class Merculight : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Guntrigger Parry");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.thorns = 1f;
            //player.GetDamage(DamageClass.Generic) += 0.5f;
            //player.statDefense -= 30;
        }
    }
}
