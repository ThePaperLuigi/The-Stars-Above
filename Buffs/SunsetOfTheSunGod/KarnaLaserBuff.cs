using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SunsetOfTheSunGod
{
    public class KarnaLaserBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vasavi Shakti");
            Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.statDefense += 30;
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
