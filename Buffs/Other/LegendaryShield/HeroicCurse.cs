using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Other.LegendaryShield
{
    public class HeroicCurse : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 10;
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
