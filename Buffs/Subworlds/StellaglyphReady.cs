using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Subworlds
{
    public class StellaglyphReady : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("[c/DE5D5D:Stellaglyph Proximity]");
            // Description.SetDefault("The Celestial Cartography menu will allow you to explore the cosmos");
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
