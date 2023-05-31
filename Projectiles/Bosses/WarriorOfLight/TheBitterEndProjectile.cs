using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;

namespace StarsAbove.Projectiles.Bosses.WarriorOfLight
{
    
    internal class TheBitterEndProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults() {
			Projectile.width = 400;
			Projectile.height = 400;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.scale = 1f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;

			//1: projectile.penetrate = 1; // Will hit even if npc is currently immune to player
			//2a: projectile.penetrate = -1; // Will hit and unless 3 is use, set 10 ticks of immunity
			//2b: projectile.penetrate = 3; // Same, but max 3 hits before dying
			//5: projectile.usesLocalNPCImmunity = true;
			//5a: projectile.localNPCHitCooldown = -1; // 1 hit per npc max
			//5b: projectile.localNPCHitCooldown = 20; // o
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			//3a: target.immune[projectile.owner] = 20;
			//3b: target.immune[projectile.owner] = 5;
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
		public override Color? GetAlpha(Color lightColor) {
			//return Color.White;
			return new Color(255, 255, 255, 0) * (1f - (float)Projectile.alpha / 255f);
		}
		public override void AI() {

			DrawOriginOffsetY = -50;
			DrawOffsetX = -50;

			Projectile.velocity *= 0.95f;
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] > 40f) {
				// Fade out
				Projectile.alpha += 10;
				if (Projectile.alpha > 255) {
					Projectile.alpha = 255;
				}
			}
			else {
				Projectile.alpha -= 25;
				
			}
			// Loop through the 4 animation frames, spending 5 ticks on each.
			if (++Projectile.frameCounter >= 4) {
				Projectile.frameCounter = 0;
				Projectile.frame++;
			}
			// Kill this projectile after 1 second
			if (Projectile.frame > 4) {
				Projectile.Kill();
			}
			Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;
			Projectile.rotation = Projectile.velocity.ToRotation();
			if (Projectile.velocity.Y > 16f) {
				Projectile.velocity.Y = 16f;
			}
			// Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.
			if (Projectile.spriteDirection == -1) {
				Projectile.rotation += MathHelper.Pi;
			}
			
		}

		
	}
}
