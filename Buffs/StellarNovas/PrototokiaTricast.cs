using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarNovas
{
    public class PrototokiaTricast : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("prototokia Tricast");
            // Description.SetDefault("You are able to cast Prototokia Aster yet again without cost at limited power");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
