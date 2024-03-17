using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Magic.StygianNymph
{
    public class ClawsOfNyx : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Claws of Nyx");
            // Description.SetDefault("Damage and crit rate is increased");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) *= 1.40f;
            player.GetCritChance(DamageClass.Magic) += 20;
            player.GetCritChance(DamageClass.Melee) += 20;
            player.GetCritChance(DamageClass.Ranged) += 20;
            player.GetCritChance(DamageClass.Throwing) += 20;


        }
    }
}
