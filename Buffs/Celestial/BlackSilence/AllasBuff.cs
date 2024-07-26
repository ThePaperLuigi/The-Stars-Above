using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Celestial.BlackSilence
{
    public class AllasBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Black Silence: Allas");
            // Description.SetDefault("Gain 30% increased Movement Speed");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //Do this in ModPlayer so Furioso can activate them all.

        }
    }
}
