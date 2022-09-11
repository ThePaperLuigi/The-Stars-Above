using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.TwinStars
{
	public class TwinStarShine1 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Twin Stars");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.width = 350;               //The width of projectile hitbox
			Projectile.height = 350;              //The height of projectile hitbox
			Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 60;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
		}
		float rotationSpeed = 0f;
		bool firstSpawn = true;
		float initialSpeed = 10f;
		int initialDistance = 200;
		bool isActive = true;
		
		int respawnTimer;
		bool cosmicConceptionStart;
		public override void AI()
		{
			if (firstSpawn)
			{
				
				Projectile.ai[1] = 160;
				firstSpawn = false;
			}
			Player player = Main.player[Projectile.owner];
			
			if (Projectile.timeLeft < 10)
			{
				Projectile.alpha += 30;
			}
			else
            {
				Projectile.alpha -= 5;

			}
			if(Projectile.timeLeft > 40)
            {
				Projectile.scale += 0.08f;
            }
			else
            {
				Projectile.scale -= 0.04f;
				if (Projectile.scale < 0.1f)
                {
					Projectile.scale = 0.1f;
                }
            }


			//projectile.spriteDirection = projectile.velocity.X > 0 ? 1 : -1;
			

			//Factors for calculations
			double deg = Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
			double rad = deg * (Math.PI / 180); //Convert degrees to radians
			double dist = 54; //Distance away from the player

			/*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
			Projectile.position.X = player.GetModPlayer<StarsAbovePlayer>().starPosition1.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
			Projectile.position.Y = player.GetModPlayer<StarsAbovePlayer>().starPosition1.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

			//Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
			
				Projectile.ai[1] += 0f + initialSpeed;
			if(initialSpeed > 0f)
            {
				initialSpeed-= 0.2f;
            }
			
			float rotationsPerSecond = rotationSpeed;
			rotationSpeed -= 0.1f;
			bool rotateClockwise = true;
			//The rotation is set here
			//projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);


		}
		


	}
}
