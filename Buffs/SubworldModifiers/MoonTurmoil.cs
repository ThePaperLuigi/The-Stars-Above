using StarsAbove.NPCs;
using System.Net.WebSockets;
using Terraria;using Terraria.ID;
using Terraria.ModLoader;

using Terraria;using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using SubworldLibrary;

namespace StarsAbove.Buffs.SubworldModifiers
{
    public class MoonTurmoil : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[c/EFB43E:Environmental Turmoil]");
            Description.SetDefault("Temporarily transposed to an orbiting celestial body" +
                "\n" +
                "\nDefenses have been reduced by 50" +
                "\nOutgoing damage is reduced by 20%" +
                "\n");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            
            player.statDefense -= 50;
            player.GetDamage(DamageClass.Generic) -= 0.2f;
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
