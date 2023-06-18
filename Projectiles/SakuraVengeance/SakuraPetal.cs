using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.SakuraVengeance
{
    public class SakuraPetal : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Sakura's Vengeance");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 8;								 //The width of projectile hitbox
			Projectile.height = 8;								  //The height of projectile hitbox
			Projectile.aiStyle = 1;								 //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;							 //Can the projectile deal damage to enemies?
			Projectile.hostile = false;							 //Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Melee;           //Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 10;						    //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 80;							  //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;							  //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0f;					 //How much light emit around the projectile
			Projectile.ignoreWater = true;					    //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;					    //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;						    //Set to above 0 if you want the projectile to update multiple time in a frame
			AIType = ProjectileID.Bullet;                      //Act exactly like default Bullet

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}
		bool onSpawn = true;
        public override void AI()
        {
			if (onSpawn)
			{
				Projectile.timeLeft += Main.rand.Next(0, 30);


				onSpawn = false;
			}
			Projectile.velocity.Y = Projectile.velocity.Y + 0.05f; // 0.1f for arrow gravity, 0.4f for knife gravity
			if (Projectile.velocity.Y > 16f) // This check implements "terminal velocity". We don't want the projectile to keep getting faster and faster. Past 16f this projectile will travel through blocks, so this check is useful.
			{
				Projectile.velocity.Y = 16f;
			}

			if (Projectile.timeLeft < 20)
            {
				Projectile.alpha += 25;
            }				
            base.AI();
        }
        public override void Kill(int timeLeft)
		{
			

		}
	}
}
