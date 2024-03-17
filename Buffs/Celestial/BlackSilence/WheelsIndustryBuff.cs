using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Celestial.BlackSilence
{
    public class WheelsIndustryBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Black Silence: Wheels Industry");
            // Description.SetDefault("Deal 10% increased damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //Do this in ModPlayer so Furioso can activate them all.

        }
    }
}
