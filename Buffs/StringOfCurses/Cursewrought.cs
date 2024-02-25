using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StringOfCurses
{
    public class Cursewrought : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Generic) += 0.2f;
            player.GetCritChance(DamageClass.Generic) += 20;

        }
    }
}
