using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.Umbra
{
    public class TimelessPotential : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Timeless Potential");
            // Description.SetDefault("Endless possibilities lay before you, granting a variety of bonuses");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //player.GetAttackSpeed(DamageClass.Generic) += 0.4f;
            
        }

    }
}
