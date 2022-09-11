using StarsAbove.NPCs;
using System.Net.WebSockets;
using Terraria;using Terraria.ID;
using Terraria.ModLoader;

using Terraria;using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs
{
    public class Ultima : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Edge of the Universe");
            Description.SetDefault("Blade in hand, etched in fate");
            Main.buffNoTimeDisplay[Type] = false;
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
