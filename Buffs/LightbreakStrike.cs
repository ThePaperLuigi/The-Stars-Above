using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class LightbreakStrike : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightbreak Strike");
            Description.SetDefault("Melee attacks will always crit");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetCritChance(DamageClass.Melee) = 100;
        }
    }
}
