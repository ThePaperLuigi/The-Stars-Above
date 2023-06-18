using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class EverlastingLight : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Light Everlasting");
            /* Description.SetDefault("Endless light blankets the sky... Foes are empowered and will periodically overflow with Light" +
                "\nAsk your Starfarer for a means to combat this threat"); */
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            
        }

    }
}
