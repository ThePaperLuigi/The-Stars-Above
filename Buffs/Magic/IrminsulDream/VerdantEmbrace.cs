using StarsAbove.NPCs;
using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Magic.IrminsulDream
{
    public class VerdantEmbrace : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Verdant Embrace (Enemy Only)");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<StarsAboveGlobalNPC>().VerdantEmbrace = true;


        }
    }
}
