using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class InnerAlchemy : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inner Alchemy");
            Description.SetDefault("Inner balance has been found, granting 10% increased damage, 25 defense, knockback resistance, and Health Regeneration");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 25;
            player.lifeRegen++;
            player.noKnockback = true;
            player.GetDamage(DamageClass.Generic) += 0.1f;

        }
    }
}
