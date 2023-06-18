using StarsAbove.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SupremeAuthority
{
    public class AuthoritySacrificeMark : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Supreme Authority Mark");
            // Description.SetDefault("Marked for death");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            
            
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<StarsAboveGlobalNPC>().AuthoritySacrificeMark = true;


        }
    }
}
