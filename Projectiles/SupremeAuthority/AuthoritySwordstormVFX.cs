
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.SupremeAuthority
{
    public class AuthoritySwordstormVFX : ModProjectile
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

			Projectile.scale += 0.008f;
			if (Projectile.ai[0] == 0)
			{
				Projectile.scale += MathHelper.Min(projOwner.GetModPlayer<WeaponPlayer>().SupremeAuthorityConsumedNPCs/5, 2f);
				rotationValue = Projectile.ai[1];
				Projectile.rotation += rotationValue - MathHelper.ToRadians(90);
				
			}
			Projectile.ai[0] += 1f;
			if(Projectile.frame < 3)
            {
				
			}
			else
            {
				
				
			}

			if (Projectile.frame == 1 && frameWait > 0)
            {
				
				
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
			Main.EntitySpriteDraw(texture,
				Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
				sourceRectangle, Color.Black, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
			ArmorShaderData data = GameShaders.Armor.GetSecondaryShader((byte)GameShaders.Armor.GetShaderIdFromItemId(ItemID.ShiftingPearlSandsDye), Main.LocalPlayer);
			data.Apply(null);
			Main.EntitySpriteDraw(texture,
				Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
				sourceRectangle, Color.White, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, (Effect)null, Main.GameViewMatrix.TransformationMatrix);

			return false;
		}
		public override void OnKill(int timeLeft)
        {
			
			base.OnKill(timeLeft);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			
        }
    }
}
