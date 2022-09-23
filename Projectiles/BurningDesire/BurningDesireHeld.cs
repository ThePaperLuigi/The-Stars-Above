
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
			Player projOwner = Main.player[Projectile.owner];
			Projectile.scale = 0.7f;
			if (firstSpawn)
			{//Place it high up when it first spawns (can be done in setDefaults too)
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
			//Lerp towards the player
			newOffsetY = (int)MathHelper.Lerp(DrawOriginOffsetY, 20, spawnProgress);
			DrawOriginOffsetY = newOffsetY;

			Projectile.ai[0]++;

			//Spawn dust after some time has passed.
			if(Projectile.ai[0] > 8 && dustSpawn)
            {
				projOwner.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
				SoundEngine.PlaySound(StarsAboveAudio.SFX_BlazeEquip, projOwner.Center);

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
			{
				DrawOriginOffsetX = Main.rand.NextFloat(-1.1f, 1.1f);
				Projectile.alpha -= 50;
				
			}
			else
            {
				Projectile.alpha -= 10;
			}

			if (!projOwner.GetModPlayer<StarsAbovePlayer>().BurningDesireHeld)
            {
				Projectile.Kill();
            }

			Projectile.timeLeft = 10;

			if(projOwner.ownedProjectileCounts[ProjectileType<BurningDesireStab>()] >= 1 || projOwner.ownedProjectileCounts[ProjectileType<BurningDesireSlash1>()] >= 1 || projOwner.ownedProjectileCounts[ProjectileType<BurningDesireSlash2>()] >= 1)
            {//If an attack is active
				Projectile.alpha = 255;
            }
			else
            {
				//Arms will hold the weapon.
				projOwner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (projOwner.Center -
					new Vector2(Projectile.Center.X - 10, (Projectile.Center.Y + DrawOriginOffsetY))
					).ToRotation() + MathHelper.PiOver2);
				projOwner.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, (projOwner.Center -
					new Vector2(Projectile.Center.X + 10, (Projectile.Center.Y + DrawOriginOffsetY))
					).ToRotation() + MathHelper.PiOver2);
				
			}
			

			projOwner.heldProj = Projectile.whoAmI;
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2);
			Projectile.direction = projOwner.direction;
			Projectile.spriteDirection = Projectile.direction;
			Projectile.rotation = projOwner.velocity.X * 0.05f;

			if (Projectile.spriteDirection == 1)
			{//Adjust when facing the other direction
				Projectile.position.X = projOwner.Center.X - 104;

			}
			else
			{
				Projectile.position.X = projOwner.Center.X - 144;

			}


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
