
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.StellarNovas.GuardiansLight
{
    public class SilenceSquall2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aegis Driver");

		}

		public override void SetDefaults()
		{
			Projectile.width = 38;
			Projectile.height = 38;
			//projectile.aiStyle = 2;//2
			Projectile.penetrate = 1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.timeLeft = 30;
			Projectile.hide = false;
			Projectile.ownerHitCheck = false;
			Projectile.DamageType = DamageClass.Generic;
			Projectile.tileCollide = true;
			Projectile.friendly = true;

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}

		float rotationSpeed = 3.7f;
		public static Texture2D texture;

		public override bool PreDraw(ref Color lightColor)
		{

			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);



			SpriteEffects spriteEffects = SpriteEffects.None;
			if (Projectile.spriteDirection == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}

			if (texture == null || texture.IsDisposed)
			{
				texture = (Texture2D)ModContent.Request<Texture2D>(Projectile.ModProjectile.Texture);
			}

			int frameHeight = texture.Height / Main.projFrames[Projectile.type];
			int startY = frameHeight * Projectile.frame;
			Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
			Vector2 origin = sourceRectangle.Size() / 2f;

			ArmorShaderData data = GameShaders.Armor.GetSecondaryShader((byte)GameShaders.Armor.GetShaderIdFromItemId(ItemID.StardustDye), Main.LocalPlayer);
			data.Apply(null);
			Main.EntitySpriteDraw(texture,
				Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
				sourceRectangle, Color.White, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, (Effect)null, Main.GameViewMatrix.TransformationMatrix);

			return false;
		}
		public override void AI()
		{
			Player projOwner = Main.player[Projectile.owner];

			if (Main.rand.NextBool(2))
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Frost,
					Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
				dust.noGravity = true;
				dust.velocity += Projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
			if (Main.rand.NextBool(4))
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.FireworkFountain_Blue,
					0, 0, 254, Scale: 0.3f);
				dust.noGravity = true;
				dust.velocity += Projectile.velocity * 0.5f;
				dust.velocity *= 0.5f;
			}
			float rotationsPerSecond = rotationSpeed;
			rotationSpeed -= 0.1f;
			bool rotateClockwise = true;
			//The rotation is set here
			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation -= MathHelper.ToRadians(90f);
			}

		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			Player projOwner = Main.player[Projectile.owner];

			
			base.OnHitNPC(target, hit, damageDone);
        }
		public override void Kill(int timeLeft)
		{
			Player projOwner = Main.player[Projectile.owner];
			SoundEngine.PlaySound(StarsAboveAudio.SFX_SilenceSquall1, Projectile.Center);

			projOwner.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -95;
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{

				Projectile.NewProjectile(null, Projectile.Center, Vector2.Zero, ModContent.ProjectileType<SilenceSquallDamageField>(), Projectile.damage, 0, Main.player[Projectile.owner].whoAmI);

			}
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.GemSapphire, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default(Color), 0.5f);
				Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Blue, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default(Color), 0.4f);

			}
			float dustAmount = 33f;
			float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
			for (int i = 0; (float)i < dustAmount; i++)
			{
				Vector2 spinningpoint5 = Vector2.UnitX * 0f;
				spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(25f, 1f);
				spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() + randomConstant);
				int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemSapphire);
				Main.dust[dust].scale = 2f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].position = Projectile.Center + spinningpoint5;
				Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
			}
			for (int i = 0; (float)i < dustAmount; i++)
			{
				Vector2 spinningpoint5 = Vector2.UnitX * 0f;
				spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(25f, 1f);
				spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() + randomConstant + MathHelper.ToRadians(90));
				int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemSapphire);
				Main.dust[dust].scale = 2f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].position = Projectile.Center + spinningpoint5;
				Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * Main.rand.NextFloat(6, 32);
			}
			for (int d = 0; d < 10; d++)
			{
				Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1f);
			}
			base.Kill(timeLeft);
		}
	}

}
