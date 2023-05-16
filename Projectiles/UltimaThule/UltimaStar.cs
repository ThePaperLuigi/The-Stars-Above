using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.UltimaThule
{
    public class UltimaStar : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Ultima Thule");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.width = 400;               //The width of projectile hitbox
			Projectile.height = 400;              //The height of projectile hitbox
			Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.scale = 1f;
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
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
			if(firstSpawn)
            {
				Projectile.scale = 0f;
				firstSpawn = false;
            }
			
			Player player = Main.player[Projectile.owner];
			
			Projectile.timeLeft = 10;
			
			if (!player.HasBuff(BuffType<Buffs.CosmicConception>()))
			{
				Projectile.scale -= 0.015f;
				Projectile.alpha += 25;
			}
			Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
			
			Projectile.scale += 0.003f;
			if (Projectile.alpha > 255)
            {
				Projectile.Kill();
            }
			Vector2 ownerMountedCenter = player.Center;
			Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2);
			Projectile.alpha -= 5;

			for (int i = 0; i < 50; i++)
			{
				Vector2 vector = new Vector2(
					Main.rand.Next(-2048, 2048) * (0.003f * 300) - 10,
					Main.rand.Next(-2048, 2048) * (0.003f * 300) - 10);
				Dust d = Main.dust[Dust.NewDust(
					player.Center + vector, 1, 1,
					45, 0, 0, 255,
					new Color(0.8f, 0.4f, 1f), 1.5f)];
				d.velocity = -vector / 16;
				d.velocity -= Projectile.velocity / 8;
				d.noLight = true;
				d.noGravity = true;

			}
			float rotationsPerSecond = rotationSpeed;
			rotationSpeed -= 0.6f;
			bool rotateClockwise = true;
			//The rotation is set here

			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);

		}


	}
}
