using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Celestial.BlackSilence
{
    public class RangaBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Black Silence: Ranga");
            // Description.SetDefault("Deal 30% increased critical strike damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //Do this in ModPlayer so Furioso can activate them all.

        }
    }
}
