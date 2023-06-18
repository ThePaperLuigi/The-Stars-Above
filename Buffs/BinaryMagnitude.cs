using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class BinaryMagnitude : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Binary Magnitude");
            // Description.SetDefault("The radiance of the twin stars is empowered");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
