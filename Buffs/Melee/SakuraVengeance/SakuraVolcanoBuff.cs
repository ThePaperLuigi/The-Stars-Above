using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Melee.SakuraVengeance
{
    public class SakuraVolcanoBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Volcanic Wrath");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.2f;
            player.GetCritChance(DamageClass.Generic) += 0.2f;
        }

    }
}
