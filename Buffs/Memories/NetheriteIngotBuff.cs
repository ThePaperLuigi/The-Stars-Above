using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Memories
{
    public class NetheriteIngotBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 20;
        }
    }
}
