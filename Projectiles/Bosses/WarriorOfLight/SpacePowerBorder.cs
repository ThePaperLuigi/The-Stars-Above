using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Bosses.WarriorOfLight
{
    public class SpacePowerBorder : ModProjectile
	{
		public override void SetStaticDefaults() {
			
		}

		public override void SetDefaults() {
			Projectile.width = 1000;
			Projectile.height = 1000;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 480;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.hide = false;
			Projectile.hostile = false;
			Projectile.friendly = false;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.netUpdate = true;

		}
		
		bool firstSpawn = true;

		public override void AI()
		{
			if(firstSpawn)
			{
				Projectile.timeLeft = (int)(Projectile.ai[2]);

				Projectile.scale = 0.001f;
				firstSpawn = false;
            }
			Projectile.ai[0] = MathHelper.Clamp(Projectile.ai[0], 0f, 1f);
			Projectile.scale = MathHelper.Lerp(0, 1, EaseHelper.InOutQuad(Projectile.ai[0]));
			//Projectile.scale = MathHelper.Clamp(Projectile.scale, 0, 1);

			if (Projectile.timeLeft < 60)
			{
				Projectile.ai[0] -= 0.03f;
			}
			else
			{
				Projectile.ai[0] += 0.01f;//Time alive

			}

		}
        public override void Kill(int timeLeft)
        {
			
		}
        public static Texture2D texture;

        public override bool PreDraw(ref Color lightColor)
        {
            
			Microsoft.Xna.Framework.Color color1 = Lighting.GetColor((int)((double)Projectile.position.X + (double)Projectile.width * 0.5) / 16, (int)(((double)Projectile.position.Y + (double)Projectile.height * 0.5) / 16.0));
			Vector2 drawOrigin = new Vector2(Projectile.width * 0.5f, Projectile.height * 0.5f);
			int r1 = (int)color1.R;
			//drawOrigin.Y += 34f;
			//drawOrigin.Y += 8f;
			--drawOrigin.X;
			Vector2 position1 = Projectile.Bottom - Main.screenPosition;
			Texture2D texture2D2 = (Texture2D)Request<Texture2D>("StarsAbove/Projectiles/StellarNovas/UnlimitedBladeWorksBorder");
			float num11 = (float)((double)Main.GlobalTimeWrappedHourly % 2.0 / 2.0);
			float num12 = num11;
			if ((double)num12 > 0.5)
				num12 = 1f - num11;
			if ((double)num12 < 0.0)
				num12 = 0.0f;
			float num13 = (float)(((double)num11 + 0.5) % 1.0);
			float num14 = num13;
			if ((double)num14 > 0.5)
				num14 = 1f - num13;
			if ((double)num14 < 0.0)
				num14 = 0.0f;
			Microsoft.Xna.Framework.Rectangle r2 = texture2D2.Frame(1, 1, 0, 0);
			drawOrigin = r2.Size() / 2f;
			Vector2 position3 = position1 + new Vector2(0.0f, -500f);
			Microsoft.Xna.Framework.Color color3 = new Color(255, 255, 255);
			Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3, Projectile.rotation, drawOrigin, 0.00f + (Projectile.scale * 1.041f), SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
			float num15 = 1f + num11 * 0.35f;
			Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num12, Projectile.rotation, drawOrigin, 0.00f + Projectile.scale * num15, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
			float num16 = 1f + num13 * 0.35f;
			Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num14, Projectile.rotation, drawOrigin, 0.00f + Projectile.scale * num16, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);


			return false;
        }
        
	}

	
}
