using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class LifeOfTheDragon : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Life of the Dragon");
            Description.SetDefault("The strengthened gaze of the first brood grants mastery over the skies, resulting in increased damage, critical rate, lifesteal, and movement speed");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed *= 1.30f; //
            player.GetDamage(DamageClass.Generic) *= 1.60f;
            player.noFallDmg = true;
            //player.statDefense += 20;
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
