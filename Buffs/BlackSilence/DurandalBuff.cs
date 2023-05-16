using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.BlackSilence
{
    public class DurandalBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Black Silence: Durandal");
            // Description.SetDefault("Critical strikes deal 50% increased damage to foes below 50% HP");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //Do this in ModPlayer so Furioso can activate them all.
            
        }
    }
}
