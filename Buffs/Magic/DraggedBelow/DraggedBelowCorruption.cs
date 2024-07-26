using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Magic.DraggedBelow
{
    public class DraggedBelowCorruption : ModBuff
    {
        public override void SetStaticDefaults()
        {

            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 12;

        }
    }

}
