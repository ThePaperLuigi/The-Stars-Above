using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs;
using SubworldLibrary;
using System;
using Terraria;using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.TwinStars
{
	public class TwinStar2 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Twin Stars");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.width = 125;               //The width of projectile hitbox
			Projectile.height = 125;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.scale = 1f;
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 230;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 2f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.hide = false;
		}
		float rotationSpeed = 10f;
		bool firstSpawn = true;
		float initialSpeed = 10f;
		int initialDistance = 200;
		bool isActive = true;
		int savedDamage;
		int respawnTimer;
		bool cosmicConceptionStart;

		int fadeAway = 0;

		public override void AI()
		{

			Projectile.timeLeft = 10;
			Player player = Main.player[Projectile.owner];
			if(player.HasBuff(BuffType<Buffs.TwinStarsBuff>()))
            {
				Projectile.alpha -= 10;
				if(Projectile.alpha < 0)
                {
					Projectile.alpha = 0;
                }
            }
			if (player.dead && !player.active || !player.HasBuff(BuffType<Buffs.TwinStarsBuff>()))
			{
				Projectile.alpha += 20;
			}
			if (Projectile.alpha >= 255)
            {
				Projectile.Kill();
            }
			player.GetModPlayer<StarsAbovePlayer>().starPosition2 = Projectile.Center;
			//player.heldProj = projectile.whoAmI;
			Vector2 ownerMountedCenter = player.RotatedRelativePoint(player.MountedCenter, true);
			Projectile.direction = player.direction;
			
			float rotationsPerSecond = rotationSpeed;
			rotationSpeed = 0.2f;
			bool rotateClockwise = true;
			//The rotation is set here

			//Factors for calculations
			double deg = Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
			double rad = deg * (Math.PI / 180); //Convert degrees to radians
			double dist = 108; //Distance away from the player
			Vector2 adjustedPosition = new Vector2(player.Center.X, player.Center.Y - 200);
			/*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
			Projectile.position.X = adjustedPosition.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
			Projectile.position.Y = adjustedPosition.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

			//Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
			Projectile.ai[1] += 0.5f;
			Projectile.rotation = Vector2.Normalize(player.GetModPlayer<StarsAbovePlayer>().starPosition1 - Projectile.Center).ToRotation();


			for (int i = 0; i < 30; i++)
			{//Circle
				Vector2 offset = new Vector2();
				double angle = Main.rand.NextDouble() * 2d * Math.PI;
				offset.X += (float)(Math.Sin(angle) * 32);
				offset.Y += (float)(Math.Cos(angle) * 32);

				Dust d = Dust.NewDustPerfect(Projectile.Center + offset, 226, Vector2.Zero, 200, default(Color), 0.2f);
				d.fadeIn = 0.1f;
				d.noGravity = true;
			}



		}


	}
}
