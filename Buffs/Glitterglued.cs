using StarsAbove.NPCs;
using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class Glitterglued : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glitterglued");
            Description.SetDefault("Doused in glitterglue");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            
            
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<StarsAboveGlobalNPC>().Glitterglue = true;


        }
    }
}
