using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Dusts
{
	public class MusicNote : ModDust
	{
		public override void OnSpawn(Dust dust) {
			dust.noGravity = true;
			dust.frame = new Rectangle(0, 0, 18, 24);
			//If our texture had 2 different dust on top of each other (a 30x60 pixel image), we might do this:
			//dust.frame = new Rectangle(0, Main.rand.Next(2) * 30, 30, 30);
		}

		public override bool Update(Dust dust) {
			dust.scale -= 0.01f;
			if (dust.scale < 1) {
				dust.active = false;
			}
			return false;
		}
	}
}