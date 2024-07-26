using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Summon.MorningStar
{
    public class MorningStarHit : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Morning Star");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {





        }
    }
}
