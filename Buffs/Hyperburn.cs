using StarsAbove.NPCs;
using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class Hyperburn : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hyperburn");
            // Description.SetDefault("Intense flames are causing a drastic loss in health");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<StarsAboveGlobalNPC>().Hyperburn = true;


        }
    }
}
