
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.CarianDarkMoon;
using StarsAbove.NPCs.Tsukiyomi;
using StarsAbove.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Bosses.WarriorOfLight
{
	public class WarriorVortexSummon : ModProjectile
	{
		public override void SetStaticDefaults() {
			Main.projFrames[Projectile.type] = 1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 60;
			Projectile.height = 60;
			Projectile.aiStyle = 0;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.timeLeft = 160;
			Projectile.hide = false;
			Projectile.light = 1f;
			Projectile.ownerHitCheck = false;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.hostile = true;
		}
		bool altFire = false;
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
		public override void AI() {
			//Projectile.ai[2] == Facing left or right
			//Projectile.ai[0] == Time left (should be more than 120)

			//DrawOriginOffsetY = -90;
			Projectile.timeLeft = 10;
			Projectile.alpha -= 10;
			Projectile.ai[0]--;
			if(Projectile.ai[2] == 1)
            {
				Projectile.spriteDirection = 1;
				Projectile.direction = 1;
            }
			if (Projectile.ai[0] <= 120)
			{
				//Projectile.velocity.Y -= 0.5f;
				//Projectile.velocity.Y = MathHelper.Clamp(Projectile.velocity.X, -80, 0);

				Projectile.ai[1]++;
				Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1f);

				
			}
			else
            {
				for (int i = 0; i < 5; i++)
				{
					// Charging dust
					Vector2 vector = new Vector2(
						Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10,
						Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10);
					Dust d = Main.dust[Dust.NewDust(
						Projectile.Center + vector, 1, 1,
						DustID.FireworkFountain_Blue, 0, 0, 255,
						new Color(1f, 1f, 1f), 0.5f)];
					d.velocity = -vector / 16;
					d.velocity -= Projectile.velocity / 8;
					d.noLight = true;
					d.noGravity = true;
				}


			}

			
			if (Projectile.ai[1] >= 15)
			{
				Projectile.ai[1] = 0;
				int type = ModContent.ProjectileType<WarriorVortexArrow>();
				//SoundEngine.PlaySound(StarsAboveAudio.SFX_WhisperShot, Projectile.Center);


				Vector2 position = Projectile.Center;


				if (Projectile.ai[2] == 1)
				{
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y - 20, -40, 0, type, Projectile.damage, 0f, Main.myPlayer);

				}
				else
                {
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y - 20, 40, 0, type, Projectile.damage, 0f, Main.myPlayer);

				}

				for (int d = 0; d < 10; d++)
				{
					Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(0, 20), 0f + Main.rand.Next(0, 20), 150, default(Color), 1.5f);
				}
				for (int d = 0; d < 10; d++)
				{
					Dust.NewDust(Projectile.Center, 0, 0, DustID.WhiteTorch, 0f + Main.rand.Next(0, 20), 0f + Main.rand.Next(0, 20), 150, default(Color), 1.5f);
				}

			}

			if (Projectile.ai[0] <= 0)
            {
				Projectile.Kill();
            }

			
		}
		
	}
}
