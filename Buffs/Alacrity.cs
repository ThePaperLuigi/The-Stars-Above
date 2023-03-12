using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class Alacrity : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Alacrity");
            Description.SetDefault("Attack speed and movement speed are increased");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Generic) += 0.2f;
            
            
        }
    }
}
