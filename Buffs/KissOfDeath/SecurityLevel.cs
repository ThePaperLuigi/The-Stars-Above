using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.KissOfDeath
{
    public class SecurityLevel : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Security Level");
            Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.manaRegenDelay = 10;
            //player.manaRegen = 0;
        }
    }
}
