using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Boss
{
    public class VisionsBeyondBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
           
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
                player.CanSeeInvisibleBlocks = true;
            
            

        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
