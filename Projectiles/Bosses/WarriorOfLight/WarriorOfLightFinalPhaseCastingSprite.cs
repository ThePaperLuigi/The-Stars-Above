using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.WarriorOfLight
{
    public class WarriorOfLightFinalPhaseCastingSprite : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("The Vagrant of Space and Time");
			Main.projFrames[Projectile.type] = 5;
		}

		public override void SetDefaults() {
			Projectile.width = 200;
			Projectile.height = 300;
			Projectile.aiStyle = 0;
			Projectile.penetrate = -1;
			Projectile.scale = 1;
			Projectile.alpha = 0;
			Projectile.damage = 0;
			Projectile.hide = false;
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			
		}
		int timer;
		int fadeIn = 0;
		
		float projectileVelocity = 15;

		public override void AI() {
			DrawOriginOffsetY = 5;
			timer++;
			Vector2 adjustedCenter = new Vector2(Projectile.Center.X, Projectile.Center.Y - 49);
			fadeIn += 5;
			for (int i = 0; i < 2; i++)
			{
				// Charging dust
				Vector2 vector = new Vector2(
					Main.rand.Next(-548, 548) * (0.003f * 500) - 10,
					Main.rand.Next(-548, 548) * (0.003f * 500) - 10);
				Dust d = Main.dust[Dust.NewDust(
					adjustedCenter + vector, 1, 1,
					DustID.GemTopaz, 0, 0, 255,
					new Color(1f, 1f, 1f), 1.5f)];

				d.velocity = -vector / 16;
				d.velocity -= Projectile.velocity / 8;
				d.noGravity = true;
			}
			
			
			Projectile.ai[0]--;
			if(Projectile.ai[0] <= 0)
            {
				float dustAmount = 40f;
				for (int i = 0; (float)i < dustAmount; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
					int dust = Dust.NewDust(adjustedCenter, 0, 0, DustID.GemTopaz);
					Main.dust[dust].scale = 2f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = adjustedCenter + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 13f;
				}
				for (int i = 0; (float)i < dustAmount; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(284f, 4f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
					int dust = Dust.NewDust(adjustedCenter, 0, 0, DustID.GemTopaz);
					Main.dust[dust].scale = 2f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = adjustedCenter + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 23f;
				}
				for (int i = 0; (float)i < dustAmount; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(184f, 4f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
					int dust = Dust.NewDust(adjustedCenter, 0, 0, DustID.GemTopaz);
					Main.dust[dust].scale = 2f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = adjustedCenter + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 48f;
				}
				Projectile.Kill();
            }
			if (timer >= 60)
			{

			}
			else
			{
				Projectile.alpha -= 5;

			}
			if (projectileVelocity < 0)
            {
				projectileVelocity = 0;
            }
			
			
			if (++Projectile.frameCounter >= 8)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 4)
				{
					
					Projectile.frame = 0;

				}

			}
			if(Projectile.frame == 13)
            {
				Projectile.alpha += 46;
			}
			if(Projectile.alpha >= 250)
            {
				Projectile.Kill();
			}
			
			if (Projectile.spriteDirection == -1) {
				//projectile.rotation -= MathHelper.ToRadians(90f);
			}
			for (int i = 0; i < Main.maxNPCs; i++)//The sprite will always face what the boss is facing.
			{
				NPC other = Main.npc[i];

				if (other.active && other.type == ModContent.NPCType<NPCs.WarriorOfLight.WarriorOfLightBossFinalPhase>())
				{
					Projectile.position = other.position;
					Projectile.direction = other.direction;
					Projectile.spriteDirection = Projectile.direction;
					break;
				}
			}
			float dustAmount1 = 12f;

			if (Projectile.direction == 1)
			{
				DrawOffsetX = -5;

				for (int i = 0; (float)i < dustAmount1; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount1)) * new Vector2(4f, 4f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
					int dust = Dust.NewDust(new Vector2(Projectile.Center.X - 50, Projectile.Center.Y - 95), 0, 0, DustID.GemDiamond);
					Main.dust[dust].scale = 2f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = new Vector2(Projectile.Center.X - 50, Projectile.Center.Y - 95) + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 4f;
				}
				for (int i = 0; (float)i < dustAmount1; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount1)) * new Vector2(4f, 4f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() + MathHelper.ToRadians(Projectile.ai[0]));
					int dust = Dust.NewDust(new Vector2(Projectile.Center.X - 50, Projectile.Center.Y - 95), 0, 0, DustID.GemDiamond);
					Main.dust[dust].scale = 2f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = new Vector2(Projectile.Center.X - 50, Projectile.Center.Y - 95) + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 8f;
				}
				for (int i = 0; (float)i < dustAmount1; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount1)) * new Vector2(4f, 4f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() - MathHelper.ToRadians(Projectile.ai[0]));
					int dust = Dust.NewDust(new Vector2(Projectile.Center.X - 50, Projectile.Center.Y - 95), 0, 0, DustID.GemDiamond);
					Main.dust[dust].scale = 2f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = new Vector2(Projectile.Center.X - 50, Projectile.Center.Y - 95) + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 8f;
				}
			}
			else
			{
				DrawOffsetX = 5;

				for (int i = 0; (float)i < dustAmount1; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount1)) * new Vector2(4f, 4f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
					int dust = Dust.NewDust(new Vector2(Projectile.Center.X + 150, Projectile.Center.Y - 95), 0, 0, DustID.GemDiamond);
					Main.dust[dust].scale = 2f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = new Vector2(Projectile.Center.X + 150, Projectile.Center.Y - 95) + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 4f;
				}
				for (int i = 0; (float)i < dustAmount1; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount1)) * new Vector2(4f, 4f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() + MathHelper.ToRadians(Projectile.ai[0]));
					int dust = Dust.NewDust(new Vector2(Projectile.Center.X + 150, Projectile.Center.Y - 95), 0, 0, DustID.GemDiamond);
					Main.dust[dust].scale = 2f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = new Vector2(Projectile.Center.X + 150, Projectile.Center.Y - 95) + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 8f;
				}
				for (int i = 0; (float)i < dustAmount1; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount1)) * new Vector2(4f, 4f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() - MathHelper.ToRadians(Projectile.ai[0]));
					int dust = Dust.NewDust(new Vector2(Projectile.Center.X + 150, Projectile.Center.Y - 95), 0, 0, DustID.GemDiamond);
					Main.dust[dust].scale = 2f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = new Vector2(Projectile.Center.X + 150, Projectile.Center.Y - 95) + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 8f;
				}
			}
		}

       
    }
}
