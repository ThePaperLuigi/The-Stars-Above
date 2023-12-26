using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Dusts
{
	public class Shine : ModDust
	{
		
		public override void OnSpawn(Dust dust) {
			//dust.velocity *= 0.4f;
			dust.noGravity = true;
			dust.noLight = true;
			dust.scale *= 1f;
			dust.frame = new Rectangle(0, 0, 30, 30);
			dust.alpha = 0;
		}
		bool flashy;
		int flashySwap;
		int flashyInt;
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
			return Color.White * ((float)dust.alpha/255);
        }

        public override bool Update(Dust dust) {
			dust.position += dust.velocity;
			dust.scale -= 0.015f;
			if (flashy)
            {
				dust.alpha-=20;
            }
			if(!flashy)
            {
				dust.alpha+=20;
			}
			//dust.alpha = (int)MathHelper.Clamp(dust.alpha,0, 255);
			if(dust.alpha > 254)
            {
				flashy = true;
            }
			if(dust.alpha < 0)
            {
				flashy = false;
            }

			float light = 0.35f * dust.scale;
			Lighting.AddLight(dust.position, light, light, light);
			if (dust.scale < 0f) {
				dust.active = false;
			}
			return false;
		}
	}
}