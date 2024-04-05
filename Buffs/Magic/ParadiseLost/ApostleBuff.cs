using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Magic.ParadiseLost
{
    public class ApostleBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense = npc.defDefense * 4;
            base.Update(npc, ref buffIndex);
        }
    }
}
