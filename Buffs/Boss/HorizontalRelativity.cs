using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Boss
{
    public class HorizontalRelativity : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Relativity");
            Description.SetDefault("Unable to move up or down");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.velocity.Y = 0;
            player.gravity = 0;
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
