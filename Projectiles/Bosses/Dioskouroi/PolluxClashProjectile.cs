
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.CarianDarkMoon;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Bosses.Dioskouroi
{
	public class PolluxClashProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Pollux, the Baleborn of Ice");
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
			Projectile.alpha = 255;
			Projectile.timeLeft = 10;
			Projectile.hide = false;
			Projectile.ownerHitCheck = false;
			Projectile.tileCollide = false;
			Projectile.friendly = false;

		}
		bool animationStart = true;
		
		public override void AI() {

			DrawOriginOffsetY = 54;
			DrawOffsetX = 15;
			Projectile.timeLeft = 10;

			Projectile.ai[0]--;
			Projectile.ai[1]++;

			Projectile.velocity *= 1.13f;

			if (Projectile.ai[1] >= 26)//After 30 frames, stop the animation and clash with the other Baleborn.
			{
				//SoundEngine.PlaySound(StarsAboveAudio.SFX_BlackSilenceGreatsword, Projectile.Center);
				//Draw dust and stuff
				Projectile.velocity = Vector2.Zero;
				
				
            }
			if (Projectile.ai[0] <= 10)
			{
				Projectile.alpha += 20;
			}
			else
			{
				Projectile.alpha -= 20;
			}
			Projectile.alpha = (int)MathHelper.Clamp(Projectile.alpha, 0, 255);

			if (Projectile.ai[0] <= 0)
			{
				//Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileType<TsukiCeruleanSlash>(), 20, Projectile.knockBack, Projectile.owner, 0, 1);

				Projectile.Kill();
            }
		}

	}
}
