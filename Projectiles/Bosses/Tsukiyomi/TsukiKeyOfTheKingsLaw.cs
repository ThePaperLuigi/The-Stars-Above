
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
 
using Terraria;using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Bosses.Tsukiyomi
{
	public class TsukiKeyOfTheKingsLaw : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Tsukiyomi, the First Starfarer");
			Main.projFrames[Projectile.type] = 1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 200;
			Projectile.height = 200;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.timeLeft = 160;
			Projectile.hide = false;
			Projectile.ownerHitCheck = false;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
		}

		
		public override void AI() {

			DrawOffsetX = -34;
			DrawOriginOffsetY = -16;

			Projectile.timeLeft = 10;
			Projectile.ai[0]--;
			if(Projectile.ai[0] <= 0)
            {
				Projectile.Kill();
            }
			
		}

	}
}
