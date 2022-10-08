using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.HunterSymphony
{
    public class ExpertiseSong : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Expertise Song");
            Description.SetDefault("Powerful melodies grant 8% increased critical strike chance");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetCritChance(DamageClass.Generic) += 0.08f;
        }
    }
}
