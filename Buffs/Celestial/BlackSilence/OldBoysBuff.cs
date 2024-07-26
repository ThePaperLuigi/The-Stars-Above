using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Celestial.BlackSilence
{
    public class OldBoysBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Black Silence: Old Boys");
            // Description.SetDefault("Gain 20 defense");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //Do this in ModPlayer so Furioso can activate them all.

        }
    }
}
