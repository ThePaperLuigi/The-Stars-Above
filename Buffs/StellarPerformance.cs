using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class StellarPerformance : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Stellar Performance");
            // Description.SetDefault("Your performance invigorates you, granting substantial increases to damage and movement speed");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) *= 1.15f;
            player.moveSpeed *= 1.40f;
        }
    }
}
