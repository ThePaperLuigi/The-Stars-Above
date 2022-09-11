using StarsAbove.NPCs;
using System.Net.WebSockets;
using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class OceanCulling : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ocean's Culling");
            Description.SetDefault("Weakened");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<StarsAboveGlobalNPC>().OceanCulling = true;
            //npc.wet = true;
            
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {

            return base.ReApply(npc, time, buffIndex);
        }
    }
}
