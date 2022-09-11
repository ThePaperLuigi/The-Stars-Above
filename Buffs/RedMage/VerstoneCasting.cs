using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.RedMage
{
    public class VerstoneCasting : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Verstone Casting");
            Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
                   
        }
    }
}
