using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.RedMage
{
    public class BlackEnchantment : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Enchantment");
            Description.SetDefault("Attacks are changed, cast instantly, and consume Mana Stacks");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
                   
        }
    }
}
