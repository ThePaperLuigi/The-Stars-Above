
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.WarriorOfLight
{
    public class WarriorBladeOfLightReturning : ModProjectile
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
			//default(Effects.GoldTrail).Draw(Projectile);

			base.PostDraw(lightColor);
        }
        public override bool PreDraw(ref Color lightColor)
		{
			if(Projectile.ai[2] > 0)
            {

            }
			else
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
				Main.EntitySpriteDraw(texture,
					Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
					sourceRectangle, Color.Black, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
				ArmorShaderData data = GameShaders.Armor.GetSecondaryShader((byte)GameShaders.Armor.GetShaderIdFromItemId(ItemID.ShiftingSandsDye), Main.LocalPlayer);
				data.Apply(null);
				Main.EntitySpriteDraw(texture,
					Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
					sourceRectangle, Color.White, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, (Effect)null, Main.GameViewMatrix.TransformationMatrix);

			}

			return false;
		}
		bool firstSpawn = true;
		Vector2 originalVelocity;
		//Blade is fired, travels for a time, then returns.
		public override void AI() {
			Lighting.AddLight(Projectile.Center, TorchID.Ichor);
			for (int d = 0; d < 2; d++)
			{

				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = Projectile.Center;
				Dust dust1 = Dust.NewDustPerfect(position, DustID.GemTopaz, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1f);
				dust1.noGravity = true;
			}
			
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
			if (Projectile.ai[2] <= 0 && Projectile.ai[1] == 0)
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
				Projectile.velocity = originalVelocity * -4f;
				if (Projectile.ai[0] >= 120)
				{
					Projectile.Kill();
				}
			}

			//Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);

		}
	}
}
