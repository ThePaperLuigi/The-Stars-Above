using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SubworldModifiers
{
    public class CityTurmoil : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("[c/EFB43E:Environmental Turmoil]");
            /* Description.SetDefault("Exploring a bustling futuristic cityscape" +
                "\n" +
                "\nDefenses increased by 10" +
                "\nAbove 400 HP, gain an extra 10 defense" +
                "\nFall damage is negated"); */
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.noFallDmg = true;
            player.statDefense += 10;
            if(player.statLife > 400)
            {
                player.statDefense += 10;
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
