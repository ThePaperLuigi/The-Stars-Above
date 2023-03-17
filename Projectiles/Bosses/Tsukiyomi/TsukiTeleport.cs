using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Tsukiyomi
{
    //
    public class TsukiTeleport : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tsukiyomi");
			Main.projFrames[Projectile.type] = 9;
			//DrawOriginOffsetY = 30;
			//DrawOffsetX = -60;
		}
		public override void SetDefaults()
		{
			Projectile.width = 300;
			Projectile.height = 300;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 120;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			
			Projectile.hide = false;
			Projectile.ownerHitCheck = false;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.hostile = true;

		}

		public override void AI()
		{
			Lighting.AddLight(Projectile.Center, new Vector3(1f, 0.4f, 0.64f));
			
			Projectile.netUpdate = true;
			
			if(Projectile.ai[0] == 0)
            {

            }
			Projectile.ai[0]++;


			if (Projectile.frame >= 5)
			{
				Projectile.alpha += 10;
			}
			
			if (++Projectile.frameCounter >= 10)
			{
				Projectile.frameCounter = 0;
				if (Projectile.frame < 9)
				{
					
					Projectile.frame++;
				}
				else
				{
					
					Projectile.Kill();
				}

			}
			
		}
	}
}