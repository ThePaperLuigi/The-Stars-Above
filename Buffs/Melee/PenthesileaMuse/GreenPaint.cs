using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Melee.PenthesileaMuse
{
    public class GreenPaint : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Green Paint");
            // Description.SetDefault("You've been covered in green paint");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.GetModPlayer<StarsAbovePlayer>().YellowPaint = true;
        }
    }
}
