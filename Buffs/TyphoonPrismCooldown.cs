using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class TyphoonPrismCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Typhoon Prism Cooldown");
            // Description.SetDefault("Typhoon Prism's ability will not activate when this debuff is active");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
