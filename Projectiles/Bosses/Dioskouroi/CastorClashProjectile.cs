
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
	public class CastorClashProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Castor, the Baleborn of Fire");
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
			Projectile.timeLeft = 10;
			DrawOffsetX = 15;

			Projectile.ai[0]--;
			Projectile.ai[1]++;

			Projectile.velocity *= 1.149f;
			if(Projectile.ai[1] >= 26)//After 30 frames, stop the animation and clash with the other Baleborn.
            {
				if(animationStart)
				{
					//Logic for lowering boss HP goes here too.
					Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
					SoundEngine.PlaySound(StarsAboveAudio.SFX_BlackSilenceGreatsword, Projectile.Center);
					//Draw dust and stuff
					Projectile.velocity = Vector2.Zero;

					for (int d = 0; d < 40; d++)
					{
						Dust.NewDust(Projectile.Center, 0, 0, DustID.SparksMech, 0f + Main.rand.Next(-36, 36), 0f + Main.rand.Next(-30, 30), 0, default(Color), 2f);
					}
					for (int d = 0; d < 70; d++)
					{
						Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-20, 20), 0, default(Color), 1.5f);
					}
					for (int d = 0; d < 20; d++)
					{
						Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(0, 45), 0f + Main.rand.Next(-10, 10), 0, default(Color), 1.8f);
					}
					for (int d = 0; d < 20; d++)
					{
						Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-45, 0), 0f + Main.rand.Next(-10, 10), 0, default(Color), 1.8f);
					}
					for (int d = 0; d < 40; d++)
					{
						Dust.NewDust(Projectile.Center, 0, 0, DustID.TreasureSparkle, 0f + Main.rand.Next(-8, 8), 0f + Main.rand.Next(-30, 30), 0, default(Color), 1.5f);
					}
					for (int d = 0; d < 30; d++)
					{
						Dust.NewDust(Projectile.Center, 0, 0, DustID.Flare, 0f + Main.rand.Next(-34, 34), 0f + Main.rand.Next(-30, 30), 0, default(Color), 1.5f);
					}
					for (int d = 0; d < 30; d++)
					{
						Dust.NewDust(Projectile.Center, 0, 0, DustID.Flare, 0f + Main.rand.Next(-4, 4), 0f + Main.rand.Next(-30, 30), 0, default(Color), 1.5f);
					}
					animationStart = false;
                }
				
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
