using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace StarsAbove.Projectiles.UltimaThule
{
    public class RandomPlanetProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Ultima Thule");     //The English name of the projectile
			Main.projFrames[Projectile.type] = 5;

		}

		public override void SetDefaults() {
			Projectile.width = 175;               //The width of projectile hitbox
			Projectile.height = 175;              //The height of projectile hitbox
			Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			
			Projectile.penetrate = 99;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 1f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			//AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}
		bool firstSpawn = true;
		
		public override bool OnTileCollide(Vector2 oldVelocity) {
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 5 times
			
			return false;
		}

		public override void AI()
		{
			if(Projectile.ai[1] == 1)
            {

            }
			else
            {
				Projectile.scale = 2f;

			}
			if (firstSpawn)
			{
				Projectile.frame = Main.rand.Next(1, 5);
				
				firstSpawn = false;
			}
			
		}

		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			//Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			
			// Play explosion sound
			//Main.PlaySound(SoundID.Drown, projectile.position);
			// Smoke Dust spawn
			
			// Fire Dust spawn (CHANGE TO ICE DUST)
			
			// Large Smoke Gore spawn
			

		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			
			Player p = Main.player[Projectile.owner];
			p.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
			for (int d = 0; d < 30; d++)
			{
				Dust.NewDust(target.position, target.width, target.height, 0, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
			}

			for (int d = 0; d < 26; d++)
			{
				Dust.NewDust(target.position, target.width, target.height, 222, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 30; d++)
			{
				Dust.NewDust(target.position, target.width, target.height, 7, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 40; d++)
			{
				Dust.NewDust(target.position, target.width, target.height, 269, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 50; d++)
			{
				Dust.NewDust(target.position, target.width, target.height, 78, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
			}

			// Play explosion sound
			SoundEngine.PlaySound(SoundID.Item89, target.position);
			// Smoke Dust spawn
			for (int i = 0; i < 70; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			// Fire Dust spawn
			for (int i = 0; i < 80; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 3f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 5f;
				dustIndex = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 3f;
			}
			// Large Smoke Gore spawn
			for (int g = 0; g < 4; g++)
			{
				int goreIndex = Gore.NewGore(null,new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(null,new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(null,new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				goreIndex = Gore.NewGore(null,new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
			}



			base.OnHitNPC(target, damage, knockback, crit);
		}
	}
}
