using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Dusts
{
	public class Butterfly : ModDust
	{
		public override void OnSpawn(Dust dust) {
			//dust.velocity *= 0.4f;
			dust.noGravity = true;
			dust.noLight = true;
			dust.scale *= 1f;
			dust.frame = new Rectangle(0, 0, 50, 50);
			dust.alpha = 0;
		}
		

		
		int dustTimer;
		int dustFrame;
		
		public override bool Update(Dust dust) {
			
			float light = 0.35f * dust.scale;
			Lighting.AddLight(dust.position, light, light, light);
			
			dust.rotation += 0.03f * (dust.dustIndex % 2 == 0 ? -1 : 1);
			dust.scale -= 0.02f;
			if(++dustTimer > 7)
            {
				dustFrame++;
				if(dustFrame > 1)
                {
					dustFrame = 0;
                }

				
				

			}
			if(dustFrame == 1)
            {
				dust.frame = new Rectangle(0, 50, 50, 50);

			}
			else
            {
				dust.frame = new Rectangle(0, 0, 50, 50);
			}
			
				// Here we assign position to some offset from the player that was assigned. This offset scales with dust.scale. The scale and rotation cause the spiral movement we desired.
				//dust.position += Vector2.UnitX.RotatedBy(dust.rotation, Vector2.Zero) * dust.scale * 50;
			
			// Here we make sure to kill any dust that get really small.
			if (dust.scale < 0.25f)
			{
				dust.active = false;
			}
			return false;
		}
	}
}