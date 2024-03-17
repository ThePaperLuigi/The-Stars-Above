using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Other.Farewells
{
    public class FarewellCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Farewell Cooldown");
            // Description.SetDefault("The Kevesi and Agnian Farewells will fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
