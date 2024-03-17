using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Melee.Skofnung
{
    public class BloodstainedBelone : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bloodstained Belone");
            // Description.SetDefault("Damage has been increased by 10%");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.1f;
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
