using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.ManiacalJustice
{
    public class SpecialAttackBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Special Attack");
            Description.SetDefault("Attack speed and damage drastically increased... at a cost");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Generic) += 1f;
            player.GetDamage(DamageClass.Generic) += 1f;
            player.wingTime = 10;
            player.wingTimeMax = 10;

        }
    }
}
