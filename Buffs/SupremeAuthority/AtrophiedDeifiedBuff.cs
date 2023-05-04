using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SupremeAuthority
{
    public class AtrophiedDeifiedBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Atrophied Deification");
            Description.SetDefault("" +
                "Natural life regeneration has been disabled" +
                "\nDeath will have magnified consequences" +
                "\nCan not be removed by right-click");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegenTime = 10;
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
