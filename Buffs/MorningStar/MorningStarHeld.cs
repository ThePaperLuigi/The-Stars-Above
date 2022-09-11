using Microsoft.Xna.Framework;
using StarsAbove.Projectiles.MorningStar;
using Terraria;using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Buffs.MorningStar
{
    public class MorningStarHeld : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Morning Star");
            Description.SetDefault("");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }
		Vector2 position;
		public override void Update(Player player, ref int buffIndex)
		{
			





		}
	}
}
