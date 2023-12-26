using StarsAbove.NPCs;
using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class Starblight : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Starblight");
            // Description.SetDefault("Taking powerful damage over time");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<StarsAboveGlobalNPC>().Starblight = true;
            
            
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {

            return base.ReApply(npc, time, buffIndex);
        }
    }
}
