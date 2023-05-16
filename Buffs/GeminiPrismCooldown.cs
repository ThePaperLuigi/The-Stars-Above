using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class GeminiPrismCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gemini Prism Cooldown");
            // Description.SetDefault("The Gemini Prism's Twincast will fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
