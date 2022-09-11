using StarsAbove.NPCs;
using System.Net.WebSockets;
using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class VoidAtrophy2 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Atrophy2");
            Description.SetDefault("Weakened");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<StarsAboveGlobalNPC>().voidAtrophy2 = true;
            
            
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {

            return base.ReApply(npc, time, buffIndex);
        }
    }
}
