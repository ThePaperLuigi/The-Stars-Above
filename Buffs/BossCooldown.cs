using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class BossCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Boss Cooldown");
            // Description.SetDefault("The boss of the Dying Citadel will fail to spawn with this debuff");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
