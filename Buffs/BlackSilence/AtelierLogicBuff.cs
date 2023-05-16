using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.BlackSilence
{
    public class AtelierLogicBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Black Silence: Atelier Logic");
            // Description.SetDefault("Gain 20% increased Armor Penetration");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //Do this in ModPlayer so Furioso can activate them all.
            
        }
    }
}
