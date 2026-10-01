using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarNovas
{
    public class TheThirteenthArcana : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }
    }
}
