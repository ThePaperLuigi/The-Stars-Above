using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Utilities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Events;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.Graphics.Capture;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ObjectData;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.UI.Gamepad;
using Terraria.Utilities;
using Terraria.WorldBuilding;
using Terraria;using Terraria.GameContent;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.IO;
using StarsAbove;
using StarsAbove.Items;
using StarsAbove.Projectiles;
using StarsAbove.Buffs;
using StarsAbove.NPCs;
using Microsoft.Xna.Framework.Audio;

using StarsAbove.Dusts;
using StarsAbove.Items.Consumables;
using System.Diagnostics.Contracts;
using StarsAbove.Items.Prisms;
using StarsAbove.UI.StellarNova;

namespace StarsAbove.Projectiles
{
	public class Irys : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Irys");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			Main.projFrames[Projectile.type] = 2;
			
		}

		public override void SetDefaults() {
			Projectile.width = 200;               //The width of projectile hitbox
			Projectile.height = 200;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.minion = true;
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 2f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			DrawOriginOffsetY = -15;
			DrawOffsetX = -26;
		}
		float rotationSpeed = 10f;
		public override void AI()
		{
			Projectile.timeLeft = 10;
			Player projOwner = Main.player[Projectile.owner];
			if (projOwner.dead && !projOwner.active || !projOwner.HasBuff(BuffType<Buffs.IrysBuff>()))
			{
				Projectile.Kill();
			}
			//projectile.spriteDirection = player.direction;
			projOwner.heldProj = Projectile.whoAmI;
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2);
			Projectile.spriteDirection = Projectile.direction;
			Projectile.rotation += Projectile.velocity.X / 20f;

			
			// Adding Pi to rotation if facing left corrects the drawing
			//projectile.rotation = projectile.velocity.ToRotation() + (projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);
			if (++Projectile.frameCounter >= 3)
			{
				Projectile.frameCounter = 0;
				if (Projectile.frame < 1)
				{
					Projectile.frame++;
				}
				else
				{
					Projectile.frame--;
				}

			}
			if(projOwner.velocity == Vector2.Zero)
            {
				//same for now
				for (int i = 0; i < 15; i++)
				{//Circle
					Vector2 offset = new Vector2();
					double angle = Main.rand.NextDouble() * 2d * Math.PI;
					offset.X += (float)(Math.Sin(angle) * 24);
					offset.Y += (float)(Math.Cos(angle) * 24);

					Dust d = Dust.NewDustPerfect(new Vector2(Projectile.Center.X - 2, Projectile.Center.Y - 7) + offset, 183, Vector2.Zero, 200, default(Color), 0.7f);
					d.fadeIn = 0.0001f;
					d.noGravity = true;
				}
			}
			else
            {
				for (int i = 0; i < 15; i++)
				{//Circle
					Vector2 offset = new Vector2();
					double angle = Main.rand.NextDouble() * 2d * Math.PI;
					offset.X += (float)(Math.Sin(angle) * 24);
					offset.Y += (float)(Math.Cos(angle) * 24);

					Dust d = Dust.NewDustPerfect(new Vector2(Projectile.Center.X - 2, Projectile.Center.Y - 7) + offset, 183, Vector2.Zero, 200, default(Color), 0.7f);
					d.fadeIn = 0.0001f;
					d.noGravity = true;
				}
			}
			
		}
		


	}
}
