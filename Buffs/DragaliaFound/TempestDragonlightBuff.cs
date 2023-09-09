using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.DragaliaFound
{
    public class TempestDragonlightBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += player.maxMinions*2;
        }
    }
}
