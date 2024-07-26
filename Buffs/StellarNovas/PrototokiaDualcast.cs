using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarNovas
{
    public class PrototokiaDualcast : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("prototokia Dualcast");
            // Description.SetDefault("You are able to cast Prototokia Aster again without cost");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
