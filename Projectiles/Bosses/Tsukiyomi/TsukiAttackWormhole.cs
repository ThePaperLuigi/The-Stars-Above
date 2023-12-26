using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Tsukiyomi
{
    public class TsukiAttackWormhole : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Tsukiyomi's Gateway");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.width = 400;               //The width of projectile hitbox
			Projectile.height = 400;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.scale = 1f;
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft =10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 250;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
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

		int fadeAway = 0;
		
		public override void AI()
		{
			Projectile.timeLeft = 10;
			if(firstSpawn)
            {
				Projectile.scale = 0f;
				firstSpawn = false;
            }
			
			Player player = Main.player[Projectile.owner];

			Projectile.alpha -= 5;
			Projectile.scale += 0.005f;
			Projectile.scale = MathHelper.Clamp(Projectile.scale, 0, 3f);

			Projectile.ai[0]++;
			if(Projectile.ai[0] <= 0)
            {
				Projectile.Kill();
            }
		
			//Depending on the AI[1], this fires different attacks?

			float rotationsPerSecond = rotationSpeed;
			rotationSpeed = 0.6f;
			bool rotateClockwise = true;
			//The rotation is set here

			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);

		}

        public override void OnKill(int timeLeft)
        {
			for (int d = 0; d < 26; d++)
			{
				Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 20, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16),0, default(Color), 1f);
			}
			for (int d = 0; d < 22; d++)
			{
				Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 221, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 0, default(Color), 1f);
			}

			base.OnKill(timeLeft);
        }
    }
}
