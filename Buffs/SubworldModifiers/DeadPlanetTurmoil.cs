using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SubworldModifiers
{
    public class DeadPlanetTurmoil : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("[c/EFB43E:Environmental Turmoil]");
            /* Description.SetDefault("Exploring a planet long lost" +
                "\n" +
                "\nMax Life is reduced by 80" +
                "\nMana can not regenerate naturally" +
                "\nDefense is increased by 25"); */
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statLifeMax2 -= 80;
            player.manaRegenDelay = 10;
            player.statDefense += 25;
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
