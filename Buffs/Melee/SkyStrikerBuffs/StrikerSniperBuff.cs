using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Melee.SkyStrikerBuffs
{
    public class StrikerSniperBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Aerial Forme: Railgunner");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetCritChance(DamageClass.Magic) += 20;
            player.GetCritChance(DamageClass.Melee) += 20;
            player.GetCritChance(DamageClass.Ranged) += 20;
            player.GetCritChance(DamageClass.Throwing) += 20;


        }
    }
}
