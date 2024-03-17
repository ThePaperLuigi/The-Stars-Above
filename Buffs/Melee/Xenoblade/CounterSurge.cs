using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Melee.Xenoblade
{
    public class CounterSurge : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Counter Surge");
            // Description.SetDefault("The Xenoblade's charged attack has been drastically empowered");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
