using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SkyStrikerBuffs
{
    public class Shielded : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shielded");
            // Description.SetDefault("An ally is granting you increased defenses");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 10;
        }
    }
}
