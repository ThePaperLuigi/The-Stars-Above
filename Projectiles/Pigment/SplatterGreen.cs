using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Pigment
{
    public class SplatterGreen : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Penthesila's Muse");     //The English name of the projectile

			Main.projFrames[Projectile.type] = 5;
		}

		public override void SetDefaults() {
			Projectile.width = 150;               //The width of projectile hitbox
			Projectile.height = 150;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			  
			//Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;

		}
		bool firstSpawn = true;
		float randomRotate;
        public override void AI()
        {
			Projectile.damage = 0;
			if(firstSpawn)
            {
				Projectile.frame = Main.rand.Next(0, 6);
				Projectile.rotation = MathHelper.ToRadians(Main.rand.Next(0, 360));
				firstSpawn = false;
            }
			Projectile.alpha+=2;
			
			if (Projectile.alpha > 250 || Projectile.scale < 0.3f)
            {
				Projectile.Kill();
            }

            base.AI();
        }
        

		public override bool PreDraw(ref Color lightColor) {
			//Redraw the projectile with the color not influenced by light
			lightColor = Color.White;

			return true;
		}

		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			//Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			
			// Play explosion sound
			
			// Large Smoke Gore spawn
			

		}
	}
}
