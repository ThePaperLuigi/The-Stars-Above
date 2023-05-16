using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class CoreOfFlames : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Core Of Flames");
            // Description.SetDefault("Liberation Blazing is at the apex of its power");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
           
        }
    }
}
