using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class ButchersDozen : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Butcher's Dozen");
            Description.SetDefault("Attack damage and swing speed has been increased");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Generic) *= (float)(1.3);
            player.GetDamage(DamageClass.Generic) *= (float)1.05;
            
        }
    }
}
