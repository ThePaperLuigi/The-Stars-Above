using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Chemtank
{
    public class Chemtank2 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dreadnought Chemtank Active");
            Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.manaRegenDelay = 10;
            //player.manaRegen = 0;
        }
    }
}
