using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.BurningDesire
{
    public class ChainsawFollowUp : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Burning Desire");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 100;               //The width of projectile hitbox
			Projectile.height = 100;              //The height of projectile hitbox
			Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 60;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 100;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			//AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}
		bool firstSpawn = true;
		Vector2 savedVelocity;
        public override void AI()
        {
			if(firstSpawn)
            {
				savedVelocity = Projectile.velocity;
				firstSpawn = false;
            }
			if(Projectile.ai[0] > 30)
            {
				Projectile.velocity = savedVelocity;
				Projectile.friendly = true;
				
            }
			else
            {
				Projectile.velocity = new Vector2(savedVelocity.X/10, savedVelocity.Y/10);
            }
			if(Projectile.timeLeft < 10)
            {
				Projectile.alpha += 10;
			}
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
			Projectile.ai[0]++;

            base.AI();
        }
        public override bool OnTileCollide(Vector2 oldVelocity) {
			
			
			return false;
		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center.X, target.Center.Y, 0, 0, ProjectileType<ChainsawFollowUpExplosion>(), damageDone / 4, 0f, Main.player[Projectile.owner].whoAmI, 0);
			Projectile.Kill();

			 
        }


		public override void Kill(int timeLeft)
		{
			
		}
	}
}
