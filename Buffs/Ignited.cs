using Microsoft.Xna.Framework;
using Terraria;using Terraria.ID;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs
{
    public class Ignited : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ignited");
            Description.SetDefault("Shadowless Cerulean surges forth");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.invis = true;
            player.gravity = 0f;
            player.velocity = Vector2.Zero;
            
        }
    }
}
