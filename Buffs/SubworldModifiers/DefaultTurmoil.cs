using StarsAbove.NPCs;
using System.Net.WebSockets;
using Terraria;using Terraria.ID;
using Terraria.ModLoader;

using Terraria;using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.SubworldModifiers
{
    public class DefaultTurmoil : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[c/EFB43E:Environmental Turmoil]");
            Description.SetDefault("Exploring ruins adrift in the great unknown" +
                "\n" +
                "\nNo special modifiers are active" +
                "\n");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
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
