using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.CatalystMemory
{
    public class CatalyzedBlade : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Catalyzed Blade");
            Description.SetDefault("The empowered blade before you grants 10% increased movement speed");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.GetDamage(DamageClass.Generic) += 0.1f;

            
            
        }
    }
}
