using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Memories
{
    public class PowderCharge : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Burnout");
            // Description.SetDefault("Stats are halved");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
