using StarsAbove.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class NanitePlague : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nanite Plague");
            // Description.SetDefault("Losing life");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.GetModPlayer<StarsAbovePlayer>().NanitePlague = true;
        }

        
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<StarsAboveGlobalNPC>().NanitePlague = true;
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            
            return base.ReApply(npc, time, buffIndex);
        }
    }
}
