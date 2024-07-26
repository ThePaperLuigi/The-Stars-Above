using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Other.Suistrume
{
    public class StellarPerformanceCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Stellar Performance Cooldown");
            // Description.SetDefault("When this debuff ends, you will be able to use Stellar Performance again");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
