using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Ozma
{
    public class OzmaAttack : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ascendant Array's Attacks");
            // Description.SetDefault("");
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
