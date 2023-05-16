using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class StellarOverload : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Stellar Overload");
            // Description.SetDefault("The Stellar Array is above capacity, reducing damage by half and defense to 0");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense = 0;
            player.witheredWeapon = true;
        }
    }
}
