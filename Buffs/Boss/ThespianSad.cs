using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Boss
{
    public class ThespianSad : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Manifest Laevateinn");
            // Description.SetDefault(" ");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

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
