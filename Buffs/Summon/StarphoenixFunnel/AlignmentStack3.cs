using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Summon.StarphoenixFunnel
{
    public class AlignmentStack3 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Call Of The Void");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.velocity = Vector2.Zero;
            //player.manaRegen = 0;
        }
    }
}
