
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;

namespace StarsAbove.Projectiles.Bosses.WarriorOfLight
{
    public class WarriorSolarEruption : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Ice Lotus");
			
		}

		public override void SetDefaults() {
			Projectile.width = 280;
			Projectile.height = 280;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 120;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = true;
			Projectile.friendly = false;


		}
		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			//hitbox.Width /= 2;
			//hitbox.Height /= 2;


			base.ModifyDamageHitbox(ref hitbox);
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
			ArmorShaderData data = GameShaders.Armor.GetSecondaryShader((byte)GameShaders.Armor.GetShaderIdFromItemId(ItemID.ShiftingSandsDye), Main.LocalPlayer);
			data.Apply(null);
			Main.EntitySpriteDraw(texture,
				Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
				sourceRectangle, Color.White, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, (Effect)null, Main.GameViewMatrix.TransformationMatrix);

			return false;
		}

		float rotationsPerSecond = 1f;
		bool firstSpawn = true;
		bool rotateClockwise = true;
		public override void AI() {
			Projectile.scale = 1.5f;
			rotationsPerSecond += 0.03f;
			if(firstSpawn)
			{
				rotateClockwise = Main.rand.NextBool();

				firstSpawn = false;
            }
			//The rotation is set here
			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
			Projectile.ai[0] += 1f;
			if(Projectile.ai[0] >= 15)
            {
				SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);

				Projectile.ai[0] = 0;
            }
			Player projOwner = Main.player[Projectile.owner];

			// Fade in
			Projectile.alpha -= 5;
				if (Projectile.alpha < 100)
				{
					Projectile.alpha = 100;
				}

			
			
		}
		public override void OnKill(int timeLeft)
		{
			

		}
	}
}
