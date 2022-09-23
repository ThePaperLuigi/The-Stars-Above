using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class AncientBulwark : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Bulwark");
            Description.SetDefault("Gain 80 defense, but damage is reduced by 40%");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) -= 0.4f;
            player.statDefense += 80;

            
            
        }
    }
}
