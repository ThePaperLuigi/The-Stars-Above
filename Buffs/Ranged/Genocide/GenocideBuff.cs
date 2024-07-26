using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Ranged.Genocide
{
    public class GenocideBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bloodfest");
            // Description.SetDefault("Speed and crit rate is increased");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

            player.GetCritChance(DamageClass.Magic) += 25;
            player.GetCritChance(DamageClass.Melee) += 25;
            player.GetCritChance(DamageClass.Ranged) += 25;
            player.GetCritChance(DamageClass.Throwing) += 25;


        }
    }
}
