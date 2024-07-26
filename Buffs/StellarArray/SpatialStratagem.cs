using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarArray
{
    public class SpatialStratagem : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spatial Gambit");
            // Description.SetDefault("Celestial gambit allows for the negation of the next damage taken while additionally granting a damage bonus once triggered");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {




        }
    }
}
