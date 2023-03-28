using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Nanomachina
{
    public class RealizedNanomachinaActivation : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Realized Nanomachina Activation");
            Description.SetDefault("Nanomachines have been recently deployed, granting powerful damage reduction");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance += 0.4f;
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
