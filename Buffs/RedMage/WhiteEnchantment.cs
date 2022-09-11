using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.RedMage
{
    public class WhiteEnchantment : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("White Enchantment");
            Description.SetDefault("Attacks are changed, cast instantly, and consume Mana Stacks");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
                   
        }
    }
}
