using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.SupremeAuthority
{
    public class Mortality : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mortality");
            Description.SetDefault("" +
                "Movement has been slowed by 10% and damage taken has been doubled");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
