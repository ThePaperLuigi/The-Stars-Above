using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.RedMage
{
    public class Dualcast : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dualcast");
            Description.SetDefault("Next attack will be executed without delay");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
                   
        }
    }
}
