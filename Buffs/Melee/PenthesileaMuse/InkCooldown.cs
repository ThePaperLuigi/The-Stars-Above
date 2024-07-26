using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Melee.PenthesileaMuse
{
    public class InkCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Color Swap Cooldown");
            // Description.SetDefault("Swapping Mystic Pigments will fail to activate if this debuff is present");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
