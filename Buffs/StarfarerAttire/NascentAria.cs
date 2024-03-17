using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StarfarerAttire
{
    public class NascentAria : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nascent Aria");
            // Description.SetDefault("Inner resolve is increasing defense and Health Regeneration");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 20;
            player.lifeRegen += 5;
        }
    }
}
