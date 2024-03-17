using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Magic.VenerationOfButterflies
{
    public class ButterflyTrance : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Butterfly Trance");
            // Description.SetDefault("Entranced by butterflies, your damage taken is reduced by 50%, you fall slowly, and Veneration of Butterflies has no cost");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

            player.slowFall = true;
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
