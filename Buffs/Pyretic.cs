using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class Pyretic : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pyretic");
            Description.SetDefault("If you are currently moving when this buff expires, you will be lit on fire and take damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
