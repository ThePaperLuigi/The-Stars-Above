
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs.MorningStar;
using StarsAbove.Buffs.RedMage;
using StarsAbove.Buffs.Skofnung;
using System;
using System.Security.Policy;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.BurningDesire
{
	public class BurningDesireHeld : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Burning Desire");
		}

		public override void SetDefaults()
		{
			
			AIType = 0;
			
			Projectile.width = 248;
			Projectile.height = 50;
			Projectile.minion = false;
			Projectile.minionSlots = 0f;
			Projectile.timeLeft = 240;
			Projectile.penetrate = 999;
			Projectile.hide = false;
			Projectile.alpha = 255;
			Projectile.netImportant = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			

		}
		bool firstSpawn = true;
		int newOffsetY;
		float spawnProgress;
		bool dustSpawn = true;
		public override bool PreAI()
        {
			Player player = Main.player[Projectile.owner];
			return true;
		}
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			Player projOwner = Main.player[Projectile.owner];
			if (firstSpawn)
			{
				DrawOriginOffsetY = -400;
				firstSpawn = false;
			}
			if (DrawOriginOffsetY < 0)
			{
				spawnProgress += 0.05f;
			}
			if(spawnProgress >= 1f)
            {
				spawnProgress = 1;
            }
			//if (Projectile.ai[0] < 10)
            //{
			newOffsetY = (int)MathHelper.Lerp(DrawOriginOffsetY, 20, spawnProgress);
			DrawOriginOffsetY = newOffsetY;
			
			//}
			Projectile.ai[0]++;
			if(Projectile.ai[0] > 8 && dustSpawn)
            {
				player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
				SoundEngine.PlaySound(StarsAboveAudio.SFX_BlazeEquip, player.Center);

				for (int i = 0; i < 40; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-11, 11), 0f + Main.rand.Next(-1, 1), 100, default(Color), 2f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}
				// Fire Dust spawn
				for (int i = 0; i < 20; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-1, 1), 100, default(Color), 3f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity *= 5f;
					dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-1, 1), 100, default(Color), 2f);
					Main.dust[dustIndex].velocity *= 3f;
				}
				// Large Smoke Gore spawn
				for (int g = 0; g < 4; g++)
				{
					int goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
					goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
					goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
					goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				}
				dustSpawn = false;
			}
			if(Projectile.ai[0] > 8)
			{//Intro animation finished...
				DrawOriginOffsetX = Main.rand.NextFloat(-1.1f, 1.1f);
				Projectile.alpha -= 50;
				
			}
			else
            {
				Projectile.alpha -= 10;
			}
			Projectile.scale = 0.7f;
			if (!player.GetModPlayer<StarsAbovePlayer>().BurningDesireHeld)
            {
				Projectile.Kill();
            }
			Projectile.timeLeft = 10;
			if(player.ownedProjectileCounts[ProjectileType<BurningDesireStab>()] >= 1 || player.ownedProjectileCounts[ProjectileType<BurningDesireSlash1>()] >= 1 || player.ownedProjectileCounts[ProjectileType<BurningDesireSlash2>()] >= 1)
            {
				Projectile.alpha = 255;
            }
			else
            {
				//Arms will hold the weapon.
				player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (player.Center -
					new Vector2(Projectile.Center.X - 10, (Projectile.Center.Y + DrawOriginOffsetY))
					).ToRotation() + MathHelper.PiOver2);
				player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, (player.Center -
					new Vector2(Projectile.Center.X + 10, (Projectile.Center.Y + DrawOriginOffsetY))
					).ToRotation() + MathHelper.PiOver2);
				
			}
			

			player.heldProj = Projectile.whoAmI;

			

			//Smoke vfx.
			//Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 60, 0, 31, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-1, 5), 100, default(Color), 0.6f);
			

			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			
			
			Projectile.spriteDirection = Projectile.direction;
			Projectile.rotation = player.velocity.X * 0.05f;

			//Projectile.position.X = player.Center.X;
			if (Projectile.spriteDirection == 1)
			{
				Projectile.position.X = player.Center.X - 104;

			}
			else
			{
				Projectile.position.X = player.Center.X - 144;

			}
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2);

			//This is 0 unless a auto attack has been initated, in which it'll tick up.


			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			if (Projectile.alpha > 255)
			{
				Projectile.alpha = 255;
			}



			
			


			
		}
		
		public override void Kill(int timeLeft)
		{
			

		}

	}
}
