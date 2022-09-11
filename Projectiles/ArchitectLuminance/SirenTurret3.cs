using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.ArchitectLuminance
{
	public class SirenTurret3 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("SirenTurret3");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 60;               //The width of projectile hitbox
			Projectile.height = 60;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.minion = true;
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 900;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			
		}
		float rotationSpeed = 0f;
		NPC chosenTarget;
		bool firstSpawn = true;
		float chosenTargetDistance;
		public override void AI()
		{
			if (firstSpawn)
			{
				Projectile.ai[1] = 120;
				firstSpawn = false;
			}
			//projectile.timeLeft = 999;
			Player player = Main.player[Projectile.owner];
			if (player.dead && !player.active || !player.HasBuff(BuffType<Buffs.ArtificeSirenBuff>()))
			{
				Projectile.Kill();
			}
			
			if (Projectile.velocity.Y > 16f)
			{
				Projectile.velocity.Y = 16f;
			}
			Player p = Main.player[Projectile.owner];
			player.GetModPlayer<StarsAbovePlayer>().sirenTurretCenter3 = Projectile.Center;

			//Factors for calculations
			double deg = Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
			double rad = deg * (Math.PI / 180); //Convert degrees to radians
			double dist = 128; //Distance away from the player
			Vector2 adjustedPosition = new Vector2(player.GetModPlayer<StarsAbovePlayer>().sirenCenter.X, player.GetModPlayer<StarsAbovePlayer>().sirenCenter.Y - 20);

			/*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
			Projectile.position.X = adjustedPosition.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
			Projectile.position.Y = adjustedPosition.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

			//Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
			Projectile.ai[1] += 0.9f;


			float rotationsPerSecond = rotationSpeed;
			rotationSpeed -= 0.4f;
			bool rotateClockwise = true;

			
			Projectile.rotation = Vector2.Normalize(player.GetModPlayer<StarsAbovePlayer>().sirenTarget - Projectile.Center).ToRotation() + MathHelper.ToRadians(-135f);
			Projectile.ai[0]++;
			
			
			
			



		}



	}
}
