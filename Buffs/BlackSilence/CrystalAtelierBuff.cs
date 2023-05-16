using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.BlackSilence
{
    public class CrystalAtelierBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Black Silence: Crystal Atelier");
            // Description.SetDefault("Reflect 80% of damage taken");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //Do this in ModPlayer so Furioso can activate them all.
            
        }
    }
}
