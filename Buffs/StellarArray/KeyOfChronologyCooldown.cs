using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarArray
{
    public class KeyOfChronologyCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Key of Chronology Cooldown");
            // Description.SetDefault("Key of Chronology will fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
