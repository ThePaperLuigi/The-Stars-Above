using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Chemtank
{
    public class ChemtankDefenseBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Disdain Defense");
            // Description.SetDefault("Gain 20 defense");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 20;
            //player.manaRegenDelay = 10;
            //player.manaRegen = 0;
        }
    }
}
