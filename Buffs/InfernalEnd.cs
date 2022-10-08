using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class InfernalEnd : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal End");
            Description.SetDefault("Anger after taking damage has temporarily granted 50% increased damage");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
