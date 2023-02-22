
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;

namespace StarsAbove.Projectiles.Bosses.Nalhaun
{
    public class BladeworkSlash : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Burnished Bladework");
			Main.projFrames[Projectile.type] = 4;
			
		}

		public override void SetDefaults() {
			Projectile.width = 120;
			Projectile.height = 120;
			Projectile.aiStyle = -1;
			Projectile.timeLeft = 80;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			
			Projectile.penetrate = -1;
			Projectile.hostile = false;
			Projectile.friendly = false;

		}

		bool slashSound = true;
		bool firstSpawn = true;
		public override void AI() {

			//The projectile is alive for 4 seconds. Each second, a phase of the sword slash will begin.
			//Phase 1: trail projectile as an indicator.
			//Phase 2: attack indicator
			//Phase 3: slash attack
			//Phase 4: disappear

			int damage = Projectile.damage;
			float speed = 30;
			Projectile.rotation = MathHelper.ToRadians(Projectile.ai[0]);
			if (Projectile.ai[1] > 0)
            {
				Projectile.timeLeft = 80;
				Projectile.ai[1]--;
				return;
            }

			if (firstSpawn)
			{

				Projectile.light = 0f;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)((Math.Cos(Projectile.rotation) * speed) * -1), (float)((Math.Sin(Projectile.rotation) * speed) * -1), ModContent.ProjectileType<BladeworkIndicator>(), 0, 0f, 0);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)((Math.Cos(Projectile.rotation) * speed)), (float)((Math.Sin(Projectile.rotation) * speed)), ModContent.ProjectileType<BladeworkIndicator>(), 0, 0f, 0);
				firstSpawn = false;
			}

			if (Projectile.timeLeft == 40)
			{
				if (Main.rand.NextBool())
				{
					SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing0, Projectile.Center);

				}
				else
				{
					SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing1, Projectile.Center);

				}
				Projectile.frame++;
			}
			if(Projectile.timeLeft <= 40)
            {
				Projectile.light = 1f;
			}
			if (Projectile.timeLeft == 35)
			{
				Projectile.frame++;
			}
			if (Projectile.timeLeft == 30)
			{
				Projectile.frame++;
			}
			if (Projectile.timeLeft <= 25)
			{
				Projectile.frame = 0;
			}
			Projectile.localAI[0]++;
			if (Projectile.timeLeft <= 15)
            {
				if(slashSound)
                {
					SoundEngine.PlaySound(StarsAboveAudio.SFX_TruesilverSlash, Projectile.Center);
					slashSound = false;
				}
				if (Projectile.localAI[0] >= 1)
				{
					if(Projectile.timeLeft == 15)
                    {
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)((Math.Cos(Projectile.rotation) * speed / 999)), (float)((Math.Sin(Projectile.rotation) * speed / 999)), ModContent.ProjectileType<BladeworkDamage>(), damage, 0f, 0);
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)((Math.Cos(Projectile.rotation) * speed / 999) * -1), (float)((Math.Sin(Projectile.rotation) * speed / 999) * -1), ModContent.ProjectileType<BladeworkDamage>(), damage, 0f, 0);

					}

					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)((Math.Cos(Projectile.rotation) * speed) * -1), (float)((Math.Sin(Projectile.rotation) * speed) * -1), ModContent.ProjectileType<BladeworkDamage>(), damage, 0f, 0);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, (float)((Math.Cos(Projectile.rotation) * speed)), (float)((Math.Sin(Projectile.rotation) * speed)), ModContent.ProjectileType<BladeworkDamage>(), damage, 0f, 0);
					Projectile.localAI[0] = 0;
				}
			}
			//Keep firing slashes
			if (Projectile.localAI[0] >= 3)
			{

				Projectile.localAI[0] = 0;
			}
		}

		public override bool PreDraw(ref Color lightColor)
		{
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Width() * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Color.White;
				Main.EntitySpriteDraw((Texture2D)TextureAssets.Projectile[Projectile.type], drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}

		public override void Kill(int timeLeft)
		{
			

		}
	}
}
