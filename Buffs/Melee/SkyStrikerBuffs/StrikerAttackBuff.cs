using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Melee.SkyStrikerBuffs
{
    public class StrikerAttackBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Aerial Forme: Annihilation");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.5f;
            player.statDefense -= 30;
        }
    }
}
