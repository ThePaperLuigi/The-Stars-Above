using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Other.ArchitectsLuminance
{
    public class ArchitectLuminanceBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Architect's Luminance");
            // Description.SetDefault("Wielding the might of the Architect");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 30;
        }
    }
}
