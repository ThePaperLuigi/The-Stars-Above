using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class RightDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rightward Chaos");
            // Description.SetDefault("When this buff expires, you will be flung right");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
