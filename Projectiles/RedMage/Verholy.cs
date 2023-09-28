using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using Terraria.GameContent.Drawing;
using StarsAbove.Systems;

namespace StarsAbove.Projectiles.RedMage
{
    public class Verholy : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Verholy");     //The English name of the Projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 128;               //The width of Projectile hitbox
			Projectile.height = 128;              //The height of Projectile hitbox
			//Projectile.aiStyle = 1;             //The ai style of the Projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the Projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the Projectile deal damage to the player?
			Projectile.penetrate = -1;           //How many monsters the Projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 200;          //The live time for the Projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the Projectile, 255 for completely transparent. (aiStyle 1 quickly fades the Projectile in) Make sure to delete Projectile if you aren't using an aiStyle that fades in. You'll wonder why your Projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the Projectile
			Projectile.ignoreWater = true;          //Does the Projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the Projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the Projectile to update multiple time in a frame
			Projectile.DamageType = DamageClass.Magic;
			
			//AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			
			return true; // return false because we are handling collision
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
		}

		public override void AI()
		{
			float num = 60f;
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] >= num)
			{
				Projectile.Kill();
				return;
			}
			Projectile.Opacity = Utils.Remap(Projectile.ai[0], 0f, num, 1f, 0f);
			float num10 = Projectile.ai[0] / num;
			float num11 = 1f - (1f - num10) * (1f - num10);
			float num12 = 1f - (1f - num11) * (1f - num11);
			float num17 = Utils.Remap(Projectile.ai[0], num - 15f, num, 0f, 1f);
			float num13 = num17 * num17;
			float num14 = 1f - num13;
			Projectile.scale = ((0.4f + 0.6f * num12) * num14)*3;
			float num15 = Utils.Remap(Projectile.ai[0], 20f, num, 0f, 1f);
			float num16 = 1f - (1f - num15) * (1f - num15);
			float num2 = 1f - (1f - num16) * (1f - num16);
			Projectile.localAI[0] = (0.4f + 0.6f * num2) * num14;
			int num3 = Projectile.width / 2;
			Color newColor = Main.hslToRgb(0.69f, 1f, 0.93f) * Projectile.Opacity;
			float num4 = 6f;
			float num5 = 2f;
			if (num10 < 0.9f)
			{
				for (int i = 0; i < 3; i++)
				{
					if (Main.rand.Next(2) == 0)
					{
						Vector2 vector = Vector2.UnitX.RotatedBy(Main.rand.NextFloat() * ((float)Math.PI * 2f));
						Vector2 value = vector * ((float)num3 * Projectile.scale);
						Vector2 position = Projectile.Center + value;
						Vector2 value2 = vector.RotatedBy(0.78539818525314331);
						position += value2 * num4;
						int num6 = Dust.NewDust(position, 0, 0, 267, 0f, 0f, 0, newColor);
						Main.dust[num6].position = position;
						Main.dust[num6].noGravity = true;
						Main.dust[num6].scale = 0.3f;
						Main.dust[num6].fadeIn = Main.rand.NextFloat() * 1.2f * Projectile.scale;
						Main.dust[num6].velocity = value2 * Projectile.scale * (0f - num5);
						Main.dust[num6].scale *= Projectile.scale;
						Main.dust[num6].velocity += Projectile.velocity * 0.5f;
						Main.dust[num6].position += Main.dust[num6].velocity * -5f;
						if (num6 != 6000)
						{
							Dust dust = Dust.CloneDust(num6);
							dust.scale /= 2f;
							dust.fadeIn *= 0.85f;
							dust.color = new Color(255, 255, 255, 255);
						}
					}
				}
			}
			if (num10 < 0.9f)
			{
				for (int j = 0; j < 3; j++)
				{
					if (Main.rand.Next(2) == 0)
					{
						Vector2 vector2 = Vector2.UnitX.RotatedBy(Main.rand.NextFloat() * ((float)Math.PI * 2f));
						Vector2 value3 = vector2 * ((float)num3 * Projectile.scale);
						Vector2 position2 = Projectile.Center + value3;
						Vector2 value4 = vector2.RotatedBy(0.78539818525314331);
						position2 += value4 * (0f - num4);
						int num7 = Dust.NewDust(position2, 0, 0, 267, 0f, 0f, 0, newColor);
						Main.dust[num7].position = position2;
						Main.dust[num7].noGravity = true;
						Main.dust[num7].scale = 0.3f;
						Main.dust[num7].fadeIn = Main.rand.NextFloat() * 1.2f * Projectile.scale;
						Main.dust[num7].velocity = value4 * Projectile.scale * num5;
						Main.dust[num7].scale *= Projectile.scale;
						Main.dust[num7].velocity = Projectile.velocity * 0.5f;
						if (num7 != 6000)
						{
							Dust dust2 = Dust.CloneDust(num7);
							dust2.scale /= 2f;
							dust2.fadeIn *= 0.85f;
							dust2.color = new Color(255, 255, 255, 255);
						}
					}
				}
			}
			if (num10 < 0.95f)
			{
				for (float num8 = 0f; num8 < 0.8f; num8 += 1f)
				{
					if (Main.rand.Next(4) == 0)
					{
						Vector2 value5 = Vector2.UnitX.RotatedBy(Main.rand.NextFloat() * ((float)Math.PI * 2f) + (float)Math.PI / 2f) * ((float)num3 * Projectile.scale);
						Vector2 positionInWorld = Projectile.Center + value5;
						ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
						particleOrchestraSettings.PositionInWorld = positionInWorld;
						particleOrchestraSettings.MovementVector = Projectile.velocity;
						ParticleOrchestraSettings settings = particleOrchestraSettings;
						ParticleOrchestrator.RequestParticleSpawn(clientOnly: true, ParticleOrchestraType.PrincessWeapon, settings, Projectile.owner);
					}
				}
			}
			if (Projectile.ai[0] == num - 10f)
			{
				
			}
		}
		
		

		public override void OnKill(int timeLeft)
		{
			Player player = Main.player[Projectile.owner];
			player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ProjectileType<VerholyExplosion>(), Projectile.damage * 3, 0f, 0, 0);
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ProjectileType<fastRadiate>(), 0, 0f, 0, 0);


		}
	}
}
