using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.BloodBlade
{
    public class BloodBladeBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bloodthirsting Blade");
            // Description.SetDefault("Gain powerful buffs based on missing HP, but life regeneration is disabled");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }
        float missingHPPercent;

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegenTime = 10;

            player.GetDamage(DamageClass.Generic) += (float)(player.statLifeMax2 - player.statLife) / player.statLifeMax2;

            player.GetCritChance(DamageClass.Generic) += (((player.statLifeMax2 / player.statLife)) - 1);

        }
    }
}