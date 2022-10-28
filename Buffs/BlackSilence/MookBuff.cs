using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.BlackSilence
{
    public class MookBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Silence: Mook");
            Description.SetDefault("Inflict Bleed for 4 seconds on hit");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //Do this in ModPlayer so Furioso can activate them all.
            
        }
    }
}
