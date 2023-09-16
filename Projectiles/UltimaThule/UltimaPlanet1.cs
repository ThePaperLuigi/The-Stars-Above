using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;
using StarsAbove.Systems;

namespace StarsAbove.Projectiles.UltimaThule
{
    public class UltimaPlanet1 : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Ultima Thule");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 140;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.width = 200;               //The width of projectile hitbox
			Projectile.height = 200;              //The height of projectile hitbox
			Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
		}
		float rotationSpeed = 10f;
		bool firstSpawn = true;
		float initialSpeed = 10f;
		int initialDistance = 200;
		bool isActive = true;
		int savedDamage;
		int respawnTimer;
		bool cosmicConceptionStart;
		public override void AI()
		{
			if (firstSpawn)
			{
				savedDamage = Projectile.damage;
				Projectile.ai[1] = 320;
				firstSpawn = false;
			}
			Player player = Main.player[Projectile.owner];
			if (isActive)
			{
				
				respawnTimer = 0;
				Projectile.light = 1f;
				Projectile.alpha -= 5;
			}
			else
			{
				respawnTimer++;
				Projectile.light = 0;
				Projectile.damage = 0;
				Projectile.alpha = 255;
			}
			if (Projectile.alpha < 30)
			{
				Projectile.damage = savedDamage;
			}
			if (respawnTimer >= 120)
			{
				initialSpeed = 10f;
				initialDistance = 200;
				Projectile.ai[1] = Main.rand.Next(0,361);
				isActive = true;
			}
			Projectile.timeLeft = 10;
			
			if (player.dead && !player.active || !player.HasBuff(BuffType<Buffs.Ultima>()))
			{
				Projectile.Kill();
			}
			Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
			if (Projectile.velocity.Y > 16f)
			{
				Projectile.velocity.Y = 16f;
			}
			if (player.HasBuff(BuffType<Buffs.CosmicConception>()))
			{
				if (initialSpeed < 20f)
				{
					initialSpeed = 0.3f;
				}
				respawnTimer = 0;
				if (Projectile.scale > 0f)
				{
					Projectile.scale -= 0.005f;
				}
				if (initialDistance > -150)
                {
					initialDistance--;
				}
				else
				{
					isActive = false;
				}
				Projectile.frame = 0;
				if (cosmicConceptionStart)
				{

					cosmicConceptionStart = false;
				}
			}
			else
			{
				Projectile.scale = 1f;
				cosmicConceptionStart = true;
				Projectile.frame = 0;
			}

			//Factors for calculations
			double deg = Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
			double rad = deg * (Math.PI / 180); //Convert degrees to radians
			double dist = 150 + initialDistance; //Distance away from the player

			/*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
			Projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
			Projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

			//Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
			
				Projectile.ai[1] += 2f + initialSpeed;
			Projectile.alpha -= 5;
			if(initialSpeed > 0f)
            {
				initialSpeed-= 0.2f;
            }
			if (initialDistance > 0)
			{
				initialDistance -= 5;
			}
			float rotationsPerSecond = rotationSpeed;
			rotationSpeed -= 0.1f;
			bool rotateClockwise = true;
			//The rotation is set here
			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);


		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			Projectile.scale = 2f;
			isActive = false;
			Projectile.scale = 1f;
			Player p = Main.player[Projectile.owner];
			p.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
			for (int d = 0; d < 30; d++)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 0, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
			}

			for (int d = 0; d < 26; d++)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 222, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 30; d++)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 7, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 40; d++)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 269, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 50; d++)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 78, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
			}

			// Play explosion sound
			SoundEngine.PlaySound(SoundID.Item89, Projectile.position);
			// Smoke Dust spawn
			for (int i = 0; i < 70; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			// Fire Dust spawn
			for (int i = 0; i < 80; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 3f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 5f;
				dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 3f;
			}
			// Large Smoke Gore spawn
			for (int g = 0; g < 4; g++)
			{
				int goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
			}



			 
		}

		public override bool PreDraw(ref Color lightColor)
		{
			
			return true;
		}

	}
}
