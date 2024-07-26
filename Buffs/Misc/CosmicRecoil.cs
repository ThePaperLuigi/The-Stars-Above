using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace StarsAbove.Buffs.Misc
{
    public class CosmicRecoil : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cosmic Recoil");
            // Description.SetDefault("Limitless power has its cost");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {

            player.velocity = Vector2.Zero;
            player.gravity = 0f;


        }

    }
}
