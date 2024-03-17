using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarSpoils.EmberFlask
{
    public class EmberFlaskUsed : ModBuff
    {
        public override void SetStaticDefaults()
        {

            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true; //
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
