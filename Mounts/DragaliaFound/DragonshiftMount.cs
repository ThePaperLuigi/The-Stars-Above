using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using StarsAbove.Buffs.DragaliaFound;
using Terraria.GameContent;

namespace StarsAbove.Mounts.DragaliaFound
{
	// This mount is a car with wheels which behaves simillarly to the unicorn mount. The car has 3 baloons attached to the back.
	public class DragonshiftMount : ModMount
	{
		// Since only a single instance of ModMountData ever exists, we can use player.mount._mountSpecificData to store additional data related to a specific mount.
		// Using something like this for gameplay effects would require ModPlayer syncing, but this example is purely visual.
		protected class DragonSpecificData
		{
			/*
			internal static float[] offsets = new float[] { 0, 14, -14 };

			internal int count; // Tracks how many balloons are still left.
			internal float[] rotations;

			public CarSpecificData()
			{
				count = 3;
				rotations = new float[count];
			}*/
		}

		public override void SetStaticDefaults()
		{
			// Movement
			MountData.jumpHeight = 15; // How high the mount can jump.
			MountData.acceleration = 0.19f; // The rate at which the mount speeds up.
			MountData.jumpSpeed = 4f; // The rate at which the player and mount ascend towards (negative y velocity) the jump height when the jump button is presssed.
			MountData.blockExtraJumps = true; // Determines whether or not you can use a double jump (like cloud in a bottle) while in the mount.
			MountData.constantJump = true; // Allows you to hold the jump button down.
			MountData.heightBoost = 20; // Height between the mount and the ground
			MountData.fallDamage = 0f; // Fall damage multiplier.
			MountData.runSpeed = 8f; // The speed of the mount
			MountData.dashSpeed = 3f; // The speed the mount moves when in the state of dashing.
			MountData.flightTimeMax = 240; // The amount of time in frames a mount can be in the state of flying.

			// Misc
			MountData.fatigueMax = 0;
			MountData.buff = ModContent.BuffType<DragonshiftActiveBuff>(); // The ID number of the buff assigned to the mount.

			// Effects
			MountData.spawnDust = DustID.GemEmerald;//ModContent.DustType<Dusts.Sparkle>(); // The ID of the dust spawned when mounted or dismounted.

			// Frame data and player offsets
			MountData.totalFrames = 8; // Amount of animation frames for the mount
			MountData.playerYOffsets = Enumerable.Repeat(90, MountData.totalFrames).ToArray(); // Fills an array with values for less repeating code
			MountData.xOffset = -70;
			MountData.yOffset = -90;
			MountData.playerHeadOffset = 22;
			MountData.bodyFrame = 7;
			// Standing
			MountData.standingFrameCount = 7;
			MountData.standingFrameDelay = 12;
			MountData.standingFrameStart = 0;
			// Running
			MountData.runningFrameCount = 7;
			MountData.runningFrameDelay = 30;
			MountData.runningFrameStart = 0;
			// Flying
			MountData.flyingFrameCount = 7;
			MountData.flyingFrameDelay = 8;
			MountData.flyingFrameStart = 0;
			// In-air
			MountData.inAirFrameCount = 7;
			MountData.inAirFrameDelay = 25;
			MountData.inAirFrameStart = 0;
			// Idle
			MountData.idleFrameCount = 7;
			MountData.idleFrameDelay = 12;
			MountData.idleFrameStart = 0;
			MountData.idleFrameLoop = true;
			// Swim
			MountData.swimFrameCount = MountData.inAirFrameCount;
			MountData.swimFrameDelay = MountData.inAirFrameDelay;
			MountData.swimFrameStart = MountData.inAirFrameStart;

			if (!Main.dedServ)
			{
				MountData.textureWidth = MountData.backTexture.Width() + 20;
				MountData.textureHeight = MountData.backTexture.Height();
			}
		}

		public override void UpdateEffects(Player player)
		{
			Dust.NewDust(player.MountedCenter, 0, 0, DustID.GreenFairy, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 0.5f);

			// This code spawns some dust if we are moving fast enough.
			if (Math.Abs(player.velocity.X) > 4f)
			{
				//Rectangle rect = player.getRect();

				//Dust.NewDust(new Vector2(rect.X, rect.Y), rect.Width, rect.Height, DustID.GemEmerald);
			}
		}

		public override void SetMount(Player player, ref bool skipDust)
		{
			// When this mount is mounted, we initialize _mountSpecificData with a new CarSpecificData object which will track some extra visuals for the mount.
			//player.mount._mountSpecificData = new CarSpecificData();

			// This code bypasses the normal mount spawning dust and replaces it with our own visual.
			if (!Main.dedServ)
			{
				for (int i = 0; i < 16; i++)
				{
					Dust.NewDustPerfect(player.Center + new Vector2(80, 0).RotatedBy(i * Math.PI * 2 / 16f), MountData.spawnDust);
				}

				skipDust = true;
			}
		}

		public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
		{
			// Draw is called for each mount texture we provide, so we check drawType to avoid duplicate draws.
			if (drawType == 0)
			{
				Microsoft.Xna.Framework.Color color1 = Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0));
				int r1 = (int)color1.R;
				//drawOrigin.Y += 34f;
				//drawOrigin.Y += 8f;
				//--drawOrigin.X;
				Vector2 position1 = drawPlayer.Bottom - Main.screenPosition;
				Texture2D texture2D2 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Effects/WarriorVFX");

				float num11 = (float)((double)Main.GlobalTimeWrappedHourly / 7.0);
				float timeFloatAlt = (float)((double)Main.GlobalTimeWrappedHourly / 5.0);

				//These control fade out (unused)
				float num12 = num11;
				if ((double)num12 > 0.5)
					num12 = 1f - num11;
				if ((double)num12 < 0.0)
					num12 = 0.0f;
				float num13 = (float)(((double)num11 + 0.5) % 1.0);
				float num14 = num13;
				if ((double)num14 > 0.5)
					num14 = 1f - num13;
				if ((double)num14 < 0.0)
					num14 = 0.0f;
				Microsoft.Xna.Framework.Rectangle r2 = texture2D2.Frame(1, 1, 0, 0);
				//drawOrigin = r2.Size() / 2f;
				Vector2 position3 = position1 + new Vector2(0f, -100f);

				if (drawPlayer.direction == 1)
				{
					position3 = position1 + new Vector2(-10f, -100f);
				}
				else
				{
					position3 = position1 + new Vector2(10f, -100f);
				}
				Microsoft.Xna.Framework.Color color3 = new Microsoft.Xna.Framework.Color(178, 255, 190); //This is the color of the pulse!
																												//Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3, NPC.rotation, drawOrigin, NPC.scale * 0.5f, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
				float num15 = 2f; //+ num11 * 2.75f; //Scale?
				Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3, drawPlayer.fullRotation + num11, drawOrigin, 1 * 0.5f * num15, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
				float num16 = 2f; //+ num13 * 2.75f; //Scale?
				Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * 1.3f, drawPlayer.fullRotation - timeFloatAlt, drawOrigin, 1 * 0.5f * num16, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
			
			}

			// by returning true, the regular drawing will still happen.
			return true;
		}
	}
}