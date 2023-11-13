using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Bosses.Penthesilea
{
    public class PenthSpin : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Penthesilea's Brush");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 140;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.width = 200;               //The width of projectile hitbox
			Projectile.height = 200;              //The height of projectile hitbox
			Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 150;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			DrawOffsetX = 16;
		}
		float rotationSpeed = 10f;
		bool firstSpawn = true;
		//float initialSpeed = 27f;
		int initialDistance = 250;
		bool isActive = true;
		int savedDamage;
		int respawnTimer;
		bool cosmicConceptionStart;
		public override void AI()
		{
			if (firstSpawn)
			{
				
				Projectile.ai[1] = 312;
				firstSpawn = false;
			}


			if (!NPC.AnyNPCs(NPCType<NPCs.Penthesilea.PenthesileaBoss>()))
			{

				Projectile.Kill();
			}


			if (Projectile.timeLeft < 10)
            {
				initialDistance+=20;
				Projectile.scale -= 0.05f;
            }
			
			Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
			if (Projectile.velocity.Y > 16f)
			{
				Projectile.velocity.Y = 16f;
			}
			
			//Factors for calculations
			double deg = Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
			double rad = deg * (Math.PI / 180); //Convert degrees to radians
			double dist = 250 - initialDistance; //Distance away from the player

			/*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */

			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];



				if (npc.active && npc.type == NPCType<NPCs.Penthesilea.PenthesileaBoss>())
				{
					//Projectile.Center = npc.Center;
					Projectile.position.X = npc.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
					Projectile.position.Y = npc.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
					Projectile.rotation = Vector2.Normalize(npc.Center - Projectile.Center).ToRotation() + MathHelper.ToRadians(-90f);

				}




			}
			

			//Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value

			Projectile.ai[1] += 5f;
			//Projectile.ai[1] += initialSpeed;
			Projectile.alpha -= 5;
			/*
			if(initialSpeed > 0f)
            {
				initialSpeed-= 0.2f;
            }*/
			if (initialDistance > 0)
			{
				initialDistance -= 5;
			}
			float rotationsPerSecond = rotationSpeed;
			rotationSpeed -= 0.1f;
			bool rotateClockwise = true;
			//The rotation is set here


		}

        public override void OnKill(int timeLeft)
        {

			
		}
		public override bool PreDraw(ref Color lightColor)
		{
			default(Effects.RainbowTrail).Draw(Projectile);

			return true;
		}

	}
}
