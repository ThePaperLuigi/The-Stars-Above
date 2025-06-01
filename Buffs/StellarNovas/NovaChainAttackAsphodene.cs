using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarNovas
{
    public class NovaChainAttackAsphodene : ModBuff
    {
        public override void SetStaticDefaults()
        {

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense = 0;
        }
    }
}
