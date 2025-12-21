
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
 
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.StellarNovas.GuardiansLight
{
    public class GoldenGunHeld : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Gloves of the Black Silence");
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults()
		{
			
			AIType = 0;
			
			Projectile.width = 40;
			Projectile.height = 40;
			Projectile.minion = false;
			Projectile.minionSlots = 0f;
			Projectile.timeLeft = 20;
			Projectile.penetrate = -1;
			Projectile.hide = false;
			Projectile.alpha = 0;
			Projectile.netImportant = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;

			//DrawOriginOffsetY = -60;
		}
		bool firstSpawn = true;
		int newOffsetY;
		float spawnProgress;
		bool dustSpawn = true;

		float rotationStrength = 0.1f;
		double deg;

		bool startSound = true;
		bool endSound = false;
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

			ArmorShaderData data = GameShaders.Armor.GetSecondaryShader((byte)GameShaders.Armor.GetShaderIdFromItemId(ItemID.ShiftingSandsDye), Main.LocalPlayer);
			data.Apply(null);
			Main.EntitySpriteDraw(texture,
				Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
				sourceRectangle, Color.White, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, (Effect)null, Main.GameViewMatrix.TransformationMatrix);

			return false;
		}
		public override bool PreAI()
        {
			Player player = Main.player[Projectile.owner];
			return true;
		}
		public override void AI()
		{
			Lighting.AddLight(Projectile.Center, TorchID.Ichor);

			Player projOwner = Main.player[Projectile.owner];
			Projectile.scale = 0.7f;

			if (firstSpawn)
			{
				//When the blade appears, it's rotated a bit.
				Projectile.ai[1] = 240;
				firstSpawn = false;
			}
			projOwner.heldProj = Projectile.whoAmI;//The projectile draws in front of the player.

			if (Projectile.spriteDirection == 1)
			{//Adjust when facing the other direction
				
				Projectile.ai[1] = (int)MathHelper.Lerp(180, 160, spawnProgress);//When the blade is first being drawn, rotate it so it's in front of the player. (Based on spawnProgress)
			}
			else
			{
				
				Projectile.ai[1] = (int)MathHelper.Lerp(359, 379, spawnProgress);//When the blade is first being drawn, rotate it so it's in front of the player. (Based on spawnProgress)
			}


			
			
			spawnProgress += 0.1f;
			



			

			deg = Projectile.ai[1];
			double rad = deg * (Math.PI / 180);
			double dist = 20;

			/*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
			Projectile.position.X = projOwner.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
			Projectile.position.Y = projOwner.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

			
			if (Projectile.spriteDirection == 1)
			{//Adjust when facing the other direction

				Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(180f);
			}
			else
			{

				Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(0f);
			}
			//Projectile.Center += projOwner.gfxOffY * Vector2.UnitY;//Prevent glitchy animation.

			Projectile.ai[0]++;

			if (projOwner.dead && !projOwner.active)
			{//Disappear when player dies
				Projectile.Kill();
			}

			

			
			projOwner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (projOwner.Center -
				new Vector2(Projectile.Center.X + (projOwner.velocity.X * 0.05f), Projectile.Center.Y + (projOwner.velocity.Y * 0.05f))
				).ToRotation() + MathHelper.PiOver2);
				Projectile.alpha -= 90;


			

			//Projectile.timeLeft = 10;//The prjoectile doesn't time out.





			//Orient projectile
			Projectile.direction = projOwner.direction;
			Projectile.spriteDirection = Projectile.direction;


			

			//Cap alpha
			/*if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			if (Projectile.alpha > 255)
			{
				Projectile.alpha = 255;
			}*/
			if (spawnProgress > 1f)
			{
				spawnProgress = 1f;
			}
			if (spawnProgress < 0f)
			{
				spawnProgress = 0f;
			}//Capping variables (there is 100% a better way to do this!)
		}
		private void Visuals()
		{
			

			

			
		}
		public override void OnKill(int timeLeft)
		{
			

		}

	}
}
