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

namespace StarsAbove.Projectiles.ArchitectLuminance
{
	public class Armament : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Armament");     //The English name of the projectile
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
			Player player = Main.player[Projectile.owner];
			if (projOwner.dead && !projOwner.active || !projOwner.HasBuff(BuffType<Buffs.ArchitectLuminanceBuff>()))
			{
				Projectile.Kill();
			}
			//projectile.spriteDirection = player.direction;
			//projOwner.heldProj = projectile.whoAmI;
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2);
			Projectile.spriteDirection = Projectile.direction;
			Projectile.rotation += Projectile.velocity.X / 20f;

			
			// Adding Pi to rotation if facing left corrects the drawing
			//projectile.rotation = projectile.velocity.ToRotation() + (projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);
			
			if(projOwner.velocity == Vector2.Zero)
            {
				
			}
			else
            {
				
			}
			NPC closest = null;
			float closestDistance = 9999999;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				float distance = Vector2.Distance(npc.Center, Projectile.Center);


				if (npc.active && npc.Distance(Projectile.position) < closestDistance)
				{
					closest = npc;
					closestDistance = npc.Distance(Projectile.position);
				}




			}

			if (closest.CanBeChasedBy() && closestDistance < 1200f)
			{
				/*for (int i3 = 0; i3 < 50; i3++)
				{
					Vector2 position2 = Vector2.Lerp(projectile.Center, closest.Center, (float)i3 / 50);
					Dust d = Dust.NewDustPerfect(position2, 183, null, 240, default(Color), 0.3f);
					d.fadeIn = 0.4f;
					d.noLight = true;
					d.noGravity = true;
				}*/
			}

			if (Projectile.ai[0] > 40)
			{

				if (closest.CanBeChasedBy() && closestDistance < 1200f)
				{
					Projectile.ai[0] = 0;
					int type = ProjectileID.LunarFlare;


					Vector2 position = Projectile.Center;
					float rotation = (float)Math.Atan2(position.Y - (Main.MouseWorld.Y), position.X - (Main.MouseWorld.X));//Aim towards mouse

					float launchSpeed = 36f;
					Vector2 mousePosition = player.GetModPlayer<StarsAbovePlayer>().playerMousePos;
					Vector2 direction = Vector2.Normalize(closest.position - Projectile.Center);
					Vector2 velocity = direction * launchSpeed;

					Projectile.NewProjectile(Projectile.GetSource_FromThis(),position.X, position.Y, velocity.X, velocity.Y, type, projOwner.statLife/2, 0f, player.whoAmI);

				}
			}
			if(projOwner.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
            {
				Projectile.ai[0]++;
			}
			else
            {
				Projectile.ai[0] = 0;
            }
			
		}
		


	}
}
