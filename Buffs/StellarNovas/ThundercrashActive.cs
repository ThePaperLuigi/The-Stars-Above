using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarNovas
{
    public class ThundercrashActive : ModBuff
    {
        public override void SetStaticDefaults()
        {
           
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        float modifier;
        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
