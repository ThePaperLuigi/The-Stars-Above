using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.CatalystMemory
{
    public class CatalystMemoryPrismicCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dazzling Prismic Cooldown");
            // Description.SetDefault("Summoning the Dazzling Prismic will fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
