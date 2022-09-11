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
using StarsAbove.Buffs.Ozma;

namespace StarsAbove.Projectiles.Ozma
{
	public class OzmaBack4 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Ozma Ascendant");     //The English name of the projectile
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
		int offsetVelocity;
		int idlePause;
		bool floatUpOrDown; //false is Up, true is Down
		public override void AI()
		{
			Projectile.scale = 0.9f;
			Projectile.timeLeft = 10;
			Player projOwner = Main.player[Projectile.owner];
			Player player = Main.player[Projectile.owner];
			if (projOwner.dead && !projOwner.active || !projOwner.HasBuff(BuffType<OzmaBuff>()))
			{
				Projectile.Kill();
			}
			
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2) - 6;
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2) - 5;
			Projectile.spriteDirection = Projectile.direction;
			Projectile.rotation = player.velocity.X * 0.05f;

			//This is 0 unless a auto attack has been initated, in which it'll tick up.
			

			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			if (Projectile.alpha > 255)
			{
				Projectile.alpha = 255;
			}


			if(player.HasBuff(BuffType<AnnihilationState>()))
            {
				Projectile.frame = 1;

            }
			else
            {
				Projectile.frame = 0;
            }

			UpdateMovement();
			DrawOriginOffsetY += -projOwner.GetModPlayer<StarsAbovePlayer>().OzmaSpikeVFXProgression*3;
			Projectile.alpha = projOwner.GetModPlayer<StarsAbovePlayer>().OzmaSpikeVFXProgression * 10;





		}

		private void UpdateMovement()
        {
			if (floatUpOrDown)//Up
			{
				if (Projectile.ai[0] > 6)
				{
					DrawOriginOffsetY++;
					Projectile.ai[0] = 0;
				}
			}
			else
			{
				if (Projectile.ai[0] > 6)
				{
					DrawOriginOffsetY--;
					Projectile.ai[0] = 0;
				}
			}
			if (DrawOriginOffsetY > -10)
			{
				idlePause = 10;
				DrawOriginOffsetY = -10;
				floatUpOrDown = false;

			}
			if (DrawOriginOffsetY < -20)
			{
				idlePause = 10;
				DrawOriginOffsetY = -20;
				floatUpOrDown = true;

			}
			if (idlePause < 0)
			{
				if (DrawOriginOffsetY < -12 && DrawOriginOffsetY > -18)
				{
					Projectile.ai[0] += 2;
				}
				else
				{
					Projectile.ai[0]++;
				}
			}

			idlePause--;

		}

	}
}
