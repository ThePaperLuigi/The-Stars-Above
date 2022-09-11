using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.RexLapis
{
    public class BulwarkOfJade : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bulwark of Jade");
            Description.SetDefault("Gain 30 defense");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 30;
        }
    }
}
