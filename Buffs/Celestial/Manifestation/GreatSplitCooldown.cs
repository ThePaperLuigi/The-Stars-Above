using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Celestial.Manifestation
{
    public class GreatSplitCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Great Split Cooldown");
            // Description.SetDefault("Both Great Splits will fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
