using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace StarsAbove.Projectiles.CosmicDestroyer
{
    public class CosmicDestroyerRound : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Cosmic Destroyer");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 70;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode

		}

		public override void SetDefaults() {
			Projectile.width = 54;               //The width of projectile hitbox
			Projectile.height = 54;              //The height of projectile hitbox
			Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 2;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.DamageType = DamageClass.Ranged;
			AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}

		public override bool OnTileCollide(Vector2 oldVelocity) {
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 5 times
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0) {
				for (int i = 0; i < 20; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 0.2f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}
				// Fire Dust spawn
				
				// Large Smoke Gore spawn
				
				Projectile.Kill();
			}
			else {
				Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
				if (Projectile.velocity.X != oldVelocity.X) {
					Projectile.velocity.X = -oldVelocity.X;
				}
				if (Projectile.velocity.Y != oldVelocity.Y) {
					Projectile.velocity.Y = -oldVelocity.Y;
				}
			}
			return false;
		}
        public override void AI()
        {

			
			
			
			base.AI();
        }

		public override bool PreDraw(ref Color lightColor)
		{
			default(Effects.BlueTrail).Draw(Projectile);

			return true;
		}

		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			for (int i = 0; i < 20; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 226, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 0.5f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			for (int i = 0; i < 20; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 226, Projectile.velocity.X/2 + Main.rand.Next(-2, 2), Projectile.velocity.Y*2 + Main.rand.Next(-2, 2), 100, default(Color), 0.8f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			for (int i = 0; i < 20; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 92, Projectile.velocity.X / 2 + Main.rand.Next(-2, 2), Projectile.velocity.Y + Main.rand.Next(-2, 2), 100, default(Color), 0.8f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			// Fire Dust spawn

			// Large Smoke Gore spawn

		}
	}
}
