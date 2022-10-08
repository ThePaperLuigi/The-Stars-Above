using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.AshenAmbition
{
    public class AshenAmbitionPrep : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ashen Ambition");
            Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.velocity = Vector2.Zero;
            //player.manaRegen = 0;
        }
    }
}
