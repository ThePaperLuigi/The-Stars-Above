using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.InheritedCaseM4A1
{
    public class M16A1Counterfire : ModBuff
    {
        public override void SetStaticDefaults()
        {
            
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
                        
           
        }
    }
}
