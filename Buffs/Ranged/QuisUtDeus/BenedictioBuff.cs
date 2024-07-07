using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Ranged.QuisUtDeus
{
    public class BenedictioBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
