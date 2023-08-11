
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.SupremeAuthority
{
    public class AuthoritySwordstorm : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Supreme Authority");
			
			Main.projFrames[Projectile.type] = 5;
		}

		public override void SetDefaults() {
			Projectile.width = 100;
			Projectile.height = 100;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 150;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.alpha = 0;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.light = 1f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;

			DrawOriginOffsetY = -150;
			DrawOffsetX = 20;

		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // 
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
		int frameWait = 120;
		float rotationValue;

		public override void AI() {
			Player projOwner = Main.player[Projectile.owner];

			Projectile.scale += 0.005f;
			if (Projectile.ai[0] == 0)
			{
				Projectile.scale += MathHelper.Min(projOwner.GetModPlayer<WeaponPlayer>().SupremeAuthorityConsumedNPCs/5, 2f);
				rotationValue = Projectile.ai[1];
				Projectile.rotation += rotationValue - MathHelper.ToRadians(90);
				for (int d = 0; d < 8; d++)
				{
					float Speed2 = Main.rand.NextFloat(18, 38);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotationValue) * Speed2) * -1), (float)((Math.Sin(rotationValue) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(5)); // 30 degree spread.
					int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 219, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;
				}
				for (int d = 0; d < 5; d++)
				{
					float Speed3 = Main.rand.NextFloat(20, 44);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotationValue) * Speed3) * -1), (float)((Math.Sin(rotationValue) * Speed3) * -1)).RotatedByRandom(MathHelper.ToRadians(10)); // 30 degree spread.
					int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 86, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
					Main.dust[dustIndex].noGravity = true;
				}
				for (int d = 0; d < 8; d++)
				{
					float Speed2 = Main.rand.NextFloat(-18, -38);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotationValue) * Speed2) * -1), (float)((Math.Sin(rotationValue) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(5)); // 30 degree spread.
					int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 219, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;
				}
				for (int d = 0; d < 5; d++)
				{
					float Speed3 = Main.rand.NextFloat(-20, -44);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotationValue) * Speed3) * -1), (float)((Math.Sin(rotationValue) * Speed3) * -1)).RotatedByRandom(MathHelper.ToRadians(10)); // 30 degree spread.
					int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 86, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
					Main.dust[dustIndex].noGravity = true;
				}
			}
			Projectile.ai[0] += 1f;
			if(Projectile.frame < 3)
            {
				float dustAmount = 300f;
				for (int i = 0; (float)i < dustAmount; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(1f + Projectile.scale * 195, 6f + Projectile.scale * 8);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() + rotationValue);
					int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmethyst);
					Main.dust[dust].scale = 1f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = Projectile.Center + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 1f;
				}

				float dustAmount2 = 3f;
				for (int i = 0; (float)i < dustAmount2; i++)
				{
					Vector2 vector = new Vector2(
					Main.rand.Next(-44, 44) * (0.003f * 4 - 10),
					Main.rand.Next(-44, 44) * (0.003f * 4 - 10));
					Dust d = Main.dust[Dust.NewDust(
						Projectile.Center + vector, 1, 1,
						DustID.GemAmethyst, 0, 0, 255,
						Color.White, 0.8f)];
					d.velocity = -vector / 14;
					d.velocity -= Projectile.velocity / 8;
					d.noLight = false;
					d.noGravity = true;
				}
			}
			else
            {
				
				
			}

			if (Projectile.frame == 1 && frameWait > 0)
            {
				
				if(frameWait == 60)
                {
					SoundEngine.PlaySound(StarsAboveAudio.SFX_Disappear, Projectile.Center);

				}
				frameWait--;
            }
			else
            {
				if (++Projectile.frameCounter >= 4)
				{
					Projectile.frameCounter = 0;
					if (Projectile.frame < 4)
					{
						Projectile.frame++;
					}
					else
					{
						Projectile.Kill();
					}

				}
			}
			
			
			
			if (Projectile.alpha >= 255)
			{
				Projectile.Kill();
			}
			

		}
		public static Texture2D texture;
		
		public override void Kill(int timeLeft)
		{
			Player projOwner = Main.player[Projectile.owner];

			for (int d1 = 0; d1 < 15; d1++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmethyst, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default(Color), 1f);


			}
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ProjectileType<SwordstormFinish>(), Projectile.damage, 0, projOwner.whoAmI, 0f);

			base.Kill(timeLeft);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			Player projOwner = Main.player[Projectile.owner];

			modifiers.SourceDamage *= (target.lifeMax * 0.01f + (MathHelper.Min(projOwner.GetModPlayer<WeaponPlayer>().SupremeAuthorityConsumedNPCs, 5))/100);
			if(projOwner.GetModPlayer<WeaponPlayer>().SupremeAuthorityConsumedNPCs > 5)
            {
				modifiers.SourceDamage *= (1 + ((projOwner.GetModPlayer<WeaponPlayer>().SupremeAuthorityConsumedNPCs - 5) * 0.01f));
            }
			modifiers.SourceDamage /= 10; //The attack hits multiple times so damage should be lowered as a result.

			 
        }
    }
}
