using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class DeepFreeze : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deep Freeze");
            Description.SetDefault("If you are currently standing still when this buff expires, you will be frozen solid and take damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
