
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.CatalystMemory;
using StarsAbove.Buffs.DragaliaFound;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.DragaliaFound
{
    public class DragonArm : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			
		}

		public override void SetDefaults()
		{
			
			AIType = 0;
			
			Projectile.width = 32;
			Projectile.height = 82;
			Projectile.minion = false;
			Projectile.minionSlots = 0f;
			Projectile.timeLeft = 10;
			Projectile.penetrate = -1;
			Projectile.hide = true;
			Projectile.alpha = 0;
			Projectile.netImportant = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;

		}

		bool firstSpawn = true;
		double deg;
		public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{
			overPlayers.Add(index);
		}
		public override bool PreAI()
        {
			Player player = Main.player[Projectile.owner];
			return true;
		}
		public override void AI()
		{
			DrawOriginOffsetY = -30;

			Player projOwner = Main.player[Projectile.owner];
			Projectile.scale = 1f;

			if (firstSpawn)
			{
				
				firstSpawn = false;
			}
			projOwner.heldProj = Projectile.whoAmI;//The projectile draws in front of the player.

			deg = Projectile.ai[1];
			double rad = deg * (Math.PI / 180);
			double dist = 6;

			/*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
			
			Projectile.rotation = Vector2.Normalize(Main.MouseWorld - Projectile.Center).ToRotation() + MathHelper.ToRadians(90f);

			int frameMod = 0;
			switch (projOwner.mount._frame)
				{
					case 1:
					frameMod = 2;
						break;
					case 2:
					frameMod = 8;
					break;
					case 3:
					frameMod = 6;
					break;
					case 4:
					frameMod = 4;
					break;
					case 5:
					frameMod = 2;
					break;
					case 6:
					frameMod = 0;
					break;
				}

			Projectile.position.Y = projOwner.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2 - 100 - frameMod;

			if (Projectile.spriteDirection == 1)
			{
				Projectile.position.X = projOwner.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2 - 20;
			}
			else
			{
				Projectile.position.X = projOwner.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2 + 30;
			}
			
			Projectile.ai[0]++;

			if ((projOwner.dead && !projOwner.active) || !projOwner.HasBuff(BuffType<DragonshiftActiveBuff>()))
			{//Disappear when player dies
				Projectile.Kill();
			}

			Projectile.timeLeft = 10;//The prjoectile doesn't time out.

			//Orient projectile
			if (Main.MouseWorld.X < projOwner.Center.X)
			{
				Projectile.spriteDirection = 0;
				projOwner.direction = 0;
			}
			else
			{
				Projectile.spriteDirection = 1;
				projOwner.direction = 1;
			}
			Projectile.direction = projOwner.direction;
			Projectile.spriteDirection = Projectile.direction;

		}
		public override bool PreDraw(ref Color lightColor)
		{
			// This is where we specify which way to flip the sprite. If the projectile is moving to the left, then flip it vertically.
			SpriteEffects spriteEffects = ((Projectile.spriteDirection <= 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

			// Getting texture of projectile
			Texture2D texture = TextureAssets.Projectile[Type].Value;

			// Get the currently selected frame on the texture.
			Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Type], frameY: Projectile.frame);

			Vector2 origin = sourceRectangle.Size() / 2f;
			origin.Y += 30;

			if (Projectile.spriteDirection == 1)
			{
				origin.X -= 10;
			}
			else
			{
				origin.X += 10;
			}

			// Applying lighting and draw our projectile
			Color drawColor = Projectile.GetAlpha(lightColor);

			Main.EntitySpriteDraw(texture,
				   Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
				   sourceRectangle, lightColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

			// It's important to return false, otherwise we also draw the original texture.
			return false;
		}
		private void Visuals()
		{
			

		}
		public override void Kill(int timeLeft)
		{
			

		}

	}
}
