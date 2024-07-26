using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Ranged.Tartaglia
{
    public class RagingTidesStance : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Raging Tides Stance");
            // Description.SetDefault("Tartaglia's attacks have been changed to melee");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed *= 1.40f;
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }
    }
}
