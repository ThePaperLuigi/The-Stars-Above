using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Nanomachina
{
    public class RealizedNanomachinaBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Realized Nanomachina");
            // Description.SetDefault("Nanomachines are strengthening defenses, granting a barrier, damage reduction, and empowered attacks");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.noKnockback = true;
            player.endurance += 0.1f;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            

            base.Update(npc, ref buffIndex);
        }
        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
