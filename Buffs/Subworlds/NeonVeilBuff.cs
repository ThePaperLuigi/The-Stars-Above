using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Subworlds
{
    public class NeonVeilBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if(player.HeldItem.pick > 0)
            {
                player.GetAttackSpeed(DamageClass.Generic) -= 0.6f;
            }
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
