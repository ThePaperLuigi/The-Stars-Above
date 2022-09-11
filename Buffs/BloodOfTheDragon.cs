using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class BloodOfTheDragon : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood of the Dragon");
            Description.SetDefault("The gaze of the first brood grants mastery over the skies, resulting in increased damage and movement speed");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed *= 1.10f; //
            player.GetDamage(DamageClass.Generic) *= 1.40f;
            player.noFallDmg = true;
        }
    }
}
