using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.CloakOfAnArbiter
{
    public class Ensnared : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Stun");
            // Description.SetDefault("Stuck");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.GetModPlayer<StarsAbovePlayer>().NanitePlague = true;
        }

        
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.velocity *= 0;
            if(!npc.boss)
            {
                
                

            }
            //npc.GetGlobalNPC<StarsAboveGlobalNPC>().Petrified = true;
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            
            return base.ReApply(npc, time, buffIndex);
        }
    }
}
