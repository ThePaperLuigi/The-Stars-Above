using StarsAbove.NPCs;
using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.ManiacalJustice
{
    public class KarmicRetribution : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Karmic Retribution");
            // Description.SetDefault("Sins are causing a drastic loss in health");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<StarsAboveGlobalNPC>().KarmicRetribution = true;


        }
    }
}
