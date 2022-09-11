using IL.Terraria.DataStructures;
using Terraria;using Terraria.ID;
using Terraria.ModLoader;
using StarsAbove.Buffs;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace StarsAbove.Buffs
{
    public class Voidform : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Superimposition");
            Description.SetDefault("Corporeal form has transcended dimensions, granting invincibility and drastic life regen but preventing movement");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statLife += 10;
            player.velocity = Vector2.Zero;
            player.gravity = 0f;
            player.immune = true;
            player.immuneTime = 60;
            
        }

        public override bool RightClick(int buffIndex)
        {

            return false;
        }

    }
}
