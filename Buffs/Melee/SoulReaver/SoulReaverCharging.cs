using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Melee.SoulReaver
{
    public class SoulReaverCharging : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul Harvest");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}