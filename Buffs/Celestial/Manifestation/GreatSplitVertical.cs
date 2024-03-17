using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace StarsAbove.Buffs.Celestial.Manifestation
{
    public class GreatSplitVertical : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Great Split: Vertical");
            // Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {


        }


    }
}
