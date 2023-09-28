
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.TagDamage;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Bosses.Tsukiyomi
{
    public class TsukiKingsLawProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Key Of The King's Law");
			Main.projFrames[Projectile.type] = 14;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			// This method right here is the backbone of what we're doing here; by using Projectile method, we copy all of
			// the Meowmere Projectile's SetDefault stats (such as projectile.friendly and projectile.penetrate) on to our projectile,
			// so we don't have to go into the source and copy the stats ourselves. It saves a lot of time and looks much cleaner;
			// if you're going to copy the stats of a projectile, use CloneDefaults().

			Projectile.CloneDefaults(ProjectileID.SkyFracture);
			Projectile.width = 32;
			Projectile.height = 32;
			// To further the Cloning process, we can also copy the ai of any given projectile using AIType, since we want
			// the projectile to essentially behave the same way as the vanilla projectile.
			//AIType = ProjectileID.SkyFracture;
			Projectile.friendly = false;
			Projectile.hostile = true;

		}
		public override bool PreDraw(ref Color lightColor)
		{
			default(Effects.YellowTrail).Draw(Projectile);

			return true;
		}
		public override void AI()
		{
			DelegateMethods.v3_1 = new Vector3(0.6f, 1f, 1f) * 0.2f;
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * 10f, 8f, DelegateMethods.CastLightOpen);
			if (Projectile.alpha > 0)
			{
				SoundEngine.PlaySound(SoundID.Item9, Projectile.Center);
				Projectile.alpha = 0;
				Projectile.scale = 1.1f;
				Projectile.frame = Main.rand.Next(14);
				float num10 = 16f;
				for (int num11 = 0; (float)num11 < num10; num11++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)num11 * ((float)Math.PI * 2f / num10)) * new Vector2(1f, 4f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
					int num13 = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemTopaz);
					Main.dust[num13].scale = 1f;
					Main.dust[num13].noGravity = true;
					Main.dust[num13].position = Projectile.Center + spinningpoint5;
					Main.dust[num13].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 1f;
				}
			}
			Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		}

        // While there are several different ways to change how our projectile could behave differently, lets make it so
        // when our projectile finally dies, it will explode into 4 regular Meowmere projectiles.
        
       
        public override void OnKill(int timeLeft)
		{
			for (int d = 0; d < 18; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.AmberBolt, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.7f);
				Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.3f);

			}

		}

	}
}
