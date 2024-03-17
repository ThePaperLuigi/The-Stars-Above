
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
 
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Bosses.Tsukiyomi
{
	public class TsukiShadowlessCeruleanAfterimage : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Tsukiyomi, the First Starfarer");
			Main.projFrames[Projectile.type] = 1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 110;
			Projectile.height = 110;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.timeLeft = 10;
			Projectile.hide = false;
			Projectile.ownerHitCheck = false;
			Projectile.tileCollide = false;
			Projectile.friendly = false;

		}
		bool animationStart = true;
		
		public override void AI() {

			DrawOriginOffsetY = -50;
			Projectile.timeLeft = 10;

			Projectile.ai[0]--;

			Projectile.velocity *= 0.96f;
			
			if(Projectile.ai[0] <= 30)
            {
				if (animationStart)
				{
					SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
					if (Main.rand.NextBool())
                    {
						Projectile.spriteDirection = -1;
						Projectile.velocity = new Vector2(20, 0);

					}
					else
                    {
						//Projectile.direction = 1;
						
						Projectile.velocity = new Vector2(-20, 0);

					}

					animationStart = false;
				}
				if(Projectile.ai[0] <= 10)
                {
					Projectile.alpha += 20;
				}
				else
                {
					Projectile.alpha -= 20;
				}
				Projectile.alpha = (int)MathHelper.Clamp(Projectile.alpha, 0, 255);
				
            }
			
			if(Projectile.ai[0] <= 0)
			{
				//Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileType<TsukiCeruleanSlash>(), 20, Projectile.knockBack, Projectile.owner, 0, 1);

				Projectile.Kill();
            }
		}

	}
}
