using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class TheofaniaTricast : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Theofania Tricast");
            // Description.SetDefault("You are able to cast Theofania Inanis yet again without cost at limited power");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
