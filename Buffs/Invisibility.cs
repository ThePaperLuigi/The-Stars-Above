using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class Invisibility : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Invisible");
            Description.SetDefault("Can't see this");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
