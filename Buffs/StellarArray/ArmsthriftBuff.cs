using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarArray
{
    public class ArmsthriftBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Combo Cooldown");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.15f;
        }
    }
}
