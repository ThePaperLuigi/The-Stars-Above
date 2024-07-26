using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.StellarNovas
{
    public class KiwamiRyukenConfirm : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Kiwami Ryuken Activated");
            // Description.SetDefault("Get dunked on!");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.velocity = Vector2.Zero;
            player.immune = true;
            player.immuneTime = 120;
        }
    }
}
