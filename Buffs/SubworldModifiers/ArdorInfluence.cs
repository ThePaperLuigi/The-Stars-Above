using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SubworldModifiers
{
    public class ArdorInfluence : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("[c/EFB43E:The Ardor's Influence]");
            /* Description.SetDefault("Your surroundings are permeated with an unnatural cold" +
                "\n" +
                "\nDefense is reduced by 10" +
                "\nEntering combat may have unique effects"); */
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

            player.statDefense -= 10;
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
