using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Other.Phasmasaber
{
    public class SpiritflameDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }

    }
}
