using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Magic.DraggedBelow
{
    public class DraggedBelowCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Moonlit Greatblade Cooldown");
            // Description.SetDefault("When this debuff ends, you can use Moonlit Greatblade again");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
