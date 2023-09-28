using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.VermillionDaemon
{
    public class RetributionAttacks : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Vermillion Daemon");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 60;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
			Main.projFrames[Projectile.type] = 12;

		}

		public override void SetDefaults() {
			Projectile.width = 30;               //The width of projectile hitbox
			Projectile.height = 30;              //The height of projectile hitbox
			Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Magic;           //Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 120;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
			

		}
		bool onSpawn = true;
        public override void AI()
        {

			if (onSpawn)
			{
				Projectile.frame = Main.rand.Next(1, 13);
				Projectile.timeLeft += Main.rand.Next(0, 30);
				
				onSpawn = false;
			}
			base.AI();
        }


		

		public override void OnKill(int timeLeft)
		{
			for (int i = 0; i < 12; i++)
			{
				int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 223, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 0.8f);
				//Main.dust[dustIndex].velocity *= 0.6f;
			}
		}
	}
}
