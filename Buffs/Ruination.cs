using StarsAbove.NPCs;
using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class Ruination : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ruination");
            // Description.SetDefault("You matter so little!");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            
            
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<StarsAboveGlobalNPC>().ruination = true;


        }
    }
}
