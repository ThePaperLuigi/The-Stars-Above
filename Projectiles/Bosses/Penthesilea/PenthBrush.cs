
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Penthesilea
{
    public class PenthBrush : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 90;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}

		public override void SetDefaults() {
			Projectile.width = 40;
			Projectile.height = 40;
			Projectile.timeLeft = 680;
			Projectile.penetrate = -1;
			Projectile.aiStyle = 0;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.netUpdate = true;

		}
		public static Texture2D texture;
        public override void PostDraw(Color lightColor)
        {

			base.PostDraw(lightColor);
        }
        public override bool PreDraw(ref Color lightColor)
		{
            default(Effects.GoldTrail).Draw(Projectile);


            return true;
		}
		bool firstSpawn = true;
		Vector2 originalVelocity;
		//Blade is fired, travels for a time, then returns.
		public override void AI() {
			Lighting.AddLight(Projectile.Center, TorchID.Ichor);

			DrawOffsetX = 12;
			if (firstSpawn)
            {
				originalVelocity = Projectile.velocity;
				firstSpawn = false;
            }
			Projectile.ai[2]--;
			if(Projectile.ai[2] > 0)
            {
				Projectile.velocity = Vector2.Zero;
            }
			else
            {
				Projectile.ai[0]++;
			}
			if (Projectile.ai[2] <= 0 && Projectile.ai[1] == 0 && Projectile.localAI[0] == 0)
            {
				Projectile.velocity = originalVelocity;
			}
			//AI 1 is the state, ai 0 is the timer (AI1 0 is the first attack, AI1 1 is the spinning, AI1 2 is the return)
			Projectile.timeLeft = 10;
			
			if(Projectile.ai[1] != 1)
            {
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);

			}
			if (Projectile.ai[0] >= 120 && Projectile.ai[1] == 0)
            {
				Projectile.ai[1] = 1;
				Projectile.ai[0] = 0;
            }
			if(Projectile.ai[1] == 1)
            {
				if(Projectile.ai[0] >= 60)
                {
					float dustAmount = 20f;
					for (int i = 0; (float)i < dustAmount; i++)
					{
						Vector2 spinningpoint5 = Vector2.UnitX * 0f;
						spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
						spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
						int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemTopaz);
						Main.dust[dust].scale = 2f;
						Main.dust[dust].noGravity = true;
						Main.dust[dust].position = Projectile.Center + spinningpoint5;
						Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 3f;
					}
					Projectile.ai[1] = 2;
					Projectile.ai[0] = 0;
				}
				else
				{
					Projectile.velocity *= 0.97f;
					Projectile.rotation += MathHelper.ToRadians(3);
				}
				
			}
			if(Projectile.ai[1] == 2)
            {
				if (Projectile.localAI[0] == 0)
				{
                    Projectile.velocity = originalVelocity * -2f;
					Projectile.localAI[0]++;
                    Projectile.ai[1] = 0;
                    Projectile.ai[0] = 0;
					return;
                }
                else if (Projectile.localAI[0] == 1)
				{
                    Projectile.velocity = originalVelocity * 2f;
                    Projectile.localAI[0]++;
                    Projectile.ai[1] = 0;
                    Projectile.ai[0] = 0;
                    return;
                }
                else if (Projectile.localAI[0] == 2)
				{
                    Projectile.velocity = originalVelocity * -2f;
                    Projectile.localAI[0]++;
                    Projectile.ai[1] = 0;
                    Projectile.ai[0] = 0;
                    return;
                }
                else
				{
                    Projectile.Kill();
                    float dustAmount = 20f;
                    for (int i = 0; (float)i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                        int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemTopaz);
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = Projectile.Center + spinningpoint5;
                        Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 3f;
                    }
                }
				
			}

			//Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);

		}
	}
}
