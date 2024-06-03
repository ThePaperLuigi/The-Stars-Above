using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarArray
{
    public class SwiftstrikeBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Alacrity");
            // Description.SetDefault("Attack speed and movement speed are increased");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Generic) += 0.8f;
            
            
        }
    }
}
