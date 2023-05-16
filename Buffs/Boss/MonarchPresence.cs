using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Boss
{
    public class MonarchPresence : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Monarch's Presence");
            // Description.SetDefault("Proximity to a powerful foe is lowering defense by 20");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 20;
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
