using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class TheofaniaDualcast : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Theofania Dualcast");
            Description.SetDefault("You are able to cast Theofania Inanis again without cost");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
        }
    }
}
