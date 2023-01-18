using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Subworlds
{
    public class StellarFocusOverlimit : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[c/FF0046:Stellaglyph Overlimit]");
            Description.SetDefault("Stellar Foci buffs will not apply, as there are too many Stellar Foci nearby the Stellaglyph" +
                "\nUpgrade the Stellaglyph to support more Stellar Foci");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
           
            
            
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            
            return base.ReApply(npc, time, buffIndex);
        }
    }
}
