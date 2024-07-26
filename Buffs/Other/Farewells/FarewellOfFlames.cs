using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Other.Farewells
{
    public class FarewellOfFlames : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Farewell of Flames");
            // Description.SetDefault("Solidarity with the defeated is increasing Luck");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

            player.luck += 1f;

        }
    }
}
