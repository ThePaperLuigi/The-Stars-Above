using Terraria;using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Buffs
{
    public class Wormhole : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wormhole");
            Description.SetDefault(" ");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.velocity = Microsoft.Xna.Framework.Vector2.Zero;
        }
    }
}
