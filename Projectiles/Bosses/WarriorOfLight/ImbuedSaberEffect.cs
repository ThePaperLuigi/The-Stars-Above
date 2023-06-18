using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.WarriorOfLight
{
    public class ImbuedSaberEffect : ModProjectile
	{
		public override void SetStaticDefaults() {
			
		}

		public override void SetDefaults() {
			Projectile.width = 1000;
			Projectile.height = 1000;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 10;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = false;
			Projectile.tileCollide = false;

		}
		bool onSpawn = true;
        public override bool PreAI()
        {
			

			return true;
        }
        public override bool CanHitPlayer(Player target)
        {
			
			return false;
        }
        public override void AI() {

			Projectile.timeLeft = 10;
			if(Projectile.ai[1] == 0)
            {
				for (int i = 0; i < 15; i++)
				{
					// Charging dust
					Vector2 vector = new Vector2(
						Main.rand.Next(-548, 548) * (0.003f * 500) - 10,
						Main.rand.Next(-548, 548) * (0.003f * 500) - 10);
					Dust d = Main.dust[Dust.NewDust(
						Projectile.Center + vector, 1, 1,
						DustID.Flare, 0, 0, 255,
						new Color(1f, 1f, 1f), 1.5f)];

					d.velocity = -vector / 16;
					d.velocity -= Projectile.velocity / 8;
					d.noGravity = true;
				}
			}
			else if (Projectile.ai[1] == 1)//ice
            {
				for (int i = 0; i < 15; i++)
				{
					// Charging dust
					Vector2 vector = new Vector2(
						Main.rand.Next(-548, 548) * (0.003f * 500) - 10,
						Main.rand.Next(-548, 548) * (0.003f * 500) - 10);
					Dust d = Main.dust[Dust.NewDust(
						Projectile.Center + vector, 1, 1,
						DustID.IceTorch, 0, 0, 255,
						new Color(1f, 1f, 1f), 1.5f)];

					d.velocity = -vector / 16;
					d.velocity -= Projectile.velocity / 8;
					d.noGravity = true;
				}
			}
				
			Projectile.ai[0]--;
			if(Projectile.ai[0] <= 0)
			{
				SoundEngine.PlaySound(SoundID.Item43, Projectile.Center);

				float dustAmount = 80f;
				if (Projectile.ai[1] == 0)
				{
					for (int i = 0; (float)i < dustAmount; i++)
					{
						Vector2 spinningpoint5 = Vector2.UnitX * 0f;
						spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
						spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
						int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.Flare);
						Main.dust[dust].scale = 4f;
						Main.dust[dust].noGravity = true;
						Main.dust[dust].position = Projectile.Center + spinningpoint5;
						Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 13f;
					}
					for (int i = 0; (float)i < dustAmount; i++)
					{
						Vector2 spinningpoint5 = Vector2.UnitX * 0f;
						spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(84f, 4f);
						spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
						int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.Flare);
						Main.dust[dust].scale = 4f;
						Main.dust[dust].noGravity = true;
						Main.dust[dust].position = Projectile.Center + spinningpoint5;
						Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 23f;
					}
					for (int i = 0; (float)i < dustAmount; i++)
					{
						Vector2 spinningpoint5 = Vector2.UnitX * 0f;
						spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(184f, 4f);
						spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
						int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.Flare);
						Main.dust[dust].scale = 4f;
						Main.dust[dust].noGravity = true;
						Main.dust[dust].position = Projectile.Center + spinningpoint5;
						Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 13f;
					}
				}
				else if (Projectile.ai[1] == 1)//ice
				{
					for (int i = 0; (float)i < dustAmount; i++)
					{
						Vector2 spinningpoint5 = Vector2.UnitX * 0f;
						spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
						spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
						int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.IceTorch);
						Main.dust[dust].scale = 4f;
						Main.dust[dust].noGravity = true;
						Main.dust[dust].position = Projectile.Center + spinningpoint5;
						Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 13f;
					}
					for (int i = 0; (float)i < dustAmount; i++)
					{
						Vector2 spinningpoint5 = Vector2.UnitX * 0f;
						spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(84f, 4f);
						spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
						int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.IceTorch);
						Main.dust[dust].scale = 4f;
						Main.dust[dust].noGravity = true;
						Main.dust[dust].position = Projectile.Center + spinningpoint5;
						Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 23f;
					}
					for (int i = 0; (float)i < dustAmount; i++)
					{
						Vector2 spinningpoint5 = Vector2.UnitX * 0f;
						spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(184f, 4f);
						spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
						int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.IceTorch);
						Main.dust[dust].scale = 4f;
						Main.dust[dust].noGravity = true;
						Main.dust[dust].position = Projectile.Center + spinningpoint5;
						Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 13f;
					}
				}
				
				Projectile.Kill();
			}
		}
	}
}
