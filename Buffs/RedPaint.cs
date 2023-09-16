using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class RedPaint : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Red Paint");
            // Description.SetDefault("You've been covered in red paint");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<StarsAbovePlayer>().RedPaint = true;
        }
        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
