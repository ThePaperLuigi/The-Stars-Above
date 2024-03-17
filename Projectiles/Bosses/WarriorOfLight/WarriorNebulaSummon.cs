
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
 
using StarsAbove.NPCs.Tsukiyomi;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Bosses.WarriorOfLight
{
	public class WarriorNebulaSummon : ModProjectile
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
			Projectile.hostile = false;
		}
		bool altFire = false;
		public static Texture2D texture;
		public override bool PreDraw(ref Color lightColor)
		{
			if (Projectile.ai[1] > 0)
			{
				return false;
			}
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
				Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY + MathHelper.Lerp(-10, 10, EaseHelper.Pulse((float)(Projectile.localAI[0])))),
				sourceRectangle, Color.White, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, (Effect)null, Main.GameViewMatrix.TransformationMatrix);

			return false;
		}
		bool firstSpawn = true;
		Vector2 bossPosition;

		public override void AI()
		{
			Projectile.localAI[0]++;
			float dustAmount = 20f;

			Projectile.ai[1]--;
			if (firstSpawn && Projectile.ai[1] <= 0)
			{
				Projectile.localAI[0] += Main.rand.Next(0, 50);

				for (int i = 0; i < Main.maxNPCs; i++)//The sprite will always face what the boss is facing.
				{
					NPC other = Main.npc[i];

					if (other.active && other.type == ModContent.NPCType<NPCs.WarriorOfLight.WarriorOfLightBoss>())
					{
						bossPosition = other.Center;
						//return;
					}
				}
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
				for (int ir = 0; ir < 50; ir++)
				{
					Vector2 positionNew = Vector2.Lerp(bossPosition, Projectile.Center, (float)ir / 50);

					Dust da = Dust.NewDustPerfect(positionNew, DustID.FireworkFountain_Yellow, null, 240, default(Color), 0.7f);
					da.fadeIn = 0.3f;
					da.noLight = true;
					da.noGravity = true;

				}
				firstSpawn = false;
			}
			//Projectile.ai[2] == Facing left or right
			//Projectile.ai[0] == Time left (should be more than 120)

			//DrawOriginOffsetY = -90;
			Projectile.timeLeft = 10;
			if(Projectile.ai[1] <= 0)
            {
				Projectile.ai[0]--;
				Projectile.alpha -= 10;

			}
			if (Projectile.ai[2] == 1)
            {
				Projectile.spriteDirection = -1;
				Projectile.direction = -1;
            }
			if (Projectile.ai[0] <= 120)
			{
				//Projectile.velocity.Y -= 0.5f;
				//Projectile.velocity.Y = MathHelper.Clamp(Projectile.velocity.X, -80, 0);

				Projectile.localAI[1]++;
				Dust.NewDust(Projectile.Center, 0, 0, DustID.GemTopaz, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.5f);


			}
			else if (Projectile.ai[1] <= 0)
			{
				for (int i = 0; i < 30; i++)
				{//Circle
					Vector2 offset = new Vector2();
					double angle = Main.rand.NextDouble() * 2d * Math.PI;
					offset.X += (float)(Math.Sin(angle) * (120 - Projectile.ai[0]));
					offset.Y += (float)(Math.Cos(angle) * (120 - Projectile.ai[0]));

					Dust d = Dust.NewDustPerfect(Projectile.Center + offset, DustID.GemTopaz, Projectile.velocity, 20, default(Color), 0.4f);

					d.fadeIn = 0.1f;
					d.noGravity = true;
				}
			}

			
			if (Projectile.localAI[1] == 1)
			{
				//Projectile.ai[1] = -240;
				int type = ModContent.ProjectileType<WarriorNebulaBlast>();
				//SoundEngine.PlaySound(StarsAboveAudio.SFX_WhisperShot, Projectile.Center);
				SoundEngine.PlaySound(SoundID.Item9, Projectile.Center);


				Vector2 position = Projectile.Center;


				Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y - 20, -20, 0, type, Projectile.damage, 0f, Main.myPlayer);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y - 20, 20, 0, type, Projectile.damage, 0f, Main.myPlayer);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y - 20, 0, -20, type, Projectile.damage, 0f, Main.myPlayer);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y - 20, 0, 20, type, Projectile.damage, 0f, Main.myPlayer);

			}

			if (Projectile.ai[0] <= 0)
			{
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
				Projectile.Kill();
			}



		}

	}
}
