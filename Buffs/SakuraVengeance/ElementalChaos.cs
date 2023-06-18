using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SakuraVengeance
{
    public class ElementalChaos : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Elemental Chaos");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.4f;
            player.GetCritChance(DamageClass.Generic) += 0.2f;
            player.lifeRegen += 5;
            player.manaRegen += 5;
            player.statDefense += 10;
        }

    }
}
