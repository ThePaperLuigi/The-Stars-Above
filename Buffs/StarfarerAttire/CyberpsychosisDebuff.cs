using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StarfarerAttire
{
    public class CyberpsychosisDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 10;
            player.statDefense *= 0.4f;
        }
    }
}
