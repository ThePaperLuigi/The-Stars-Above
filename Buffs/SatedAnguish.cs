using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class SatedAnguish : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sated Anguish");
            Description.SetDefault("The detriments of Lucifer's Bargain have been negated... for now");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
           

            
            
        }
    }
}
