using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SubworldModifiers
{
    public class BleachedWorldTurmoil : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[c/EFB43E:Environmental Turmoil]");
            Description.SetDefault("Exploring a planet bleached clean of ambient energy" +
                "\n" +
                "\nNatural life regeneration is disabled" +
                "\nBelow 100 HP, you become Slowed" +
                "\n");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.bleed = true;
            if(player.statLife < 100)
            {
                player.AddBuff(BuffID.Slow, 2);
            }
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
