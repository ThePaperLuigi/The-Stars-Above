
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.CatalystMemory;
using StarsAbove.Buffs.Manifestation;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Manifestation
{
    public class ManifestationHeadVFX2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Manifestation");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
		}

		public override void SetDefaults()
		{
			
			AIType = 0;
			
			Projectile.width = 1;
			Projectile.height = 1;
			Projectile.minion = false;
			Projectile.minionSlots = 0f;
			Projectile.timeLeft = 240;
			Projectile.penetrate = -1;
			Projectile.hide = false;
			Projectile.alpha = 0;
			Projectile.netImportant = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;

			DrawOriginOffsetY = -60;
		}
		bool firstSpawn = true;
		int newOffsetY;
		float spawnProgress;
		bool dustSpawn = true;

		float rotationStrength = 0.1f;
		double deg;

		bool startSound = true;
		bool endSound = false;
		public override bool PreDraw(ref Color lightColor)
		{
			default(Effects.SmallRedTrail).Draw(Projectile);

			return true;
		}
		public override bool PreAI()
        {
			Player player = Main.player[Projectile.owner];
			return true;
		}
		public override void AI()
		{
			Player projOwner = Main.player[Projectile.owner];

			projOwner.heldProj = Projectile.whoAmI;

			deg = Projectile.ai[1];
			double rad = deg * (Math.PI / 180);
			double dist = 1;

			/*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */

			if(projOwner.direction == 1)
            {
				Projectile.position.X = projOwner.Center.X + 5;

			}
			else
            {
				Projectile.position.X = projOwner.Center.X - 5;

			}

			Projectile.position.Y = projOwner.Center.Y - 12;

			
			Projectile.ai[0]++;

			if ((projOwner.dead && !projOwner.active) || !projOwner.HasBuff(BuffType<EGOManifestedBuff>()))
			{//Disappear when player dies
				Projectile.Kill();
			}
			
			Projectile.timeLeft = 10;//The prjoectile doesn't time out.

			//Orient projectile
			Projectile.direction = projOwner.direction;
			Projectile.spriteDirection = Projectile.direction;
			Projectile.rotation += projOwner.velocity.X * 0.05f; //Rotate in the direction of the user when moving


			

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
		public override void Kill(int timeLeft)
		{
			

		}

	}
}
