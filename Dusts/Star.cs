using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Dusts
{
    public class Star : ModDust
	{
		public override void OnSpawn(Dust dust) {
			//dust.velocity *= 0.4f;
			dust.noGravity = true;
			dust.noLight = true;
			dust.scale *= 1f;
			dust.frame = new Rectangle(0, 0, 50, 50);
			dust.alpha = 0;
		}
		bool flashy;
		int flashySwap;
		int flashyInt;
		double sinMagic;

		public override bool Update(Dust dust) {
			dust.position += dust.velocity;
			dust.scale -= 0.01f;
			//dust.rotation = (float)Math.Sin(sinMagic);
			//sinMagic += 0.3;
			dust.alpha = flashyInt;
			float light = 0.35f * dust.scale;
			Lighting.AddLight(dust.position, light, light, light);
			if (dust.scale < 0.3f) {
				dust.active = false;
			}
			return false;
		}
	}
}