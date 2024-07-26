using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Boss
{
    public class Vulnerable : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Vulnerability Up");
            // Description.SetDefault("Your defenses are drastically reduced");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense /= 2; //

        }
    }
}
