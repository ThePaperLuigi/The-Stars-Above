using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.TwinStars
{
    public class TwinStar1 : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Twin Stars");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.width = 200;               //The width of projectile hitbox
			Projectile.height = 200;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.scale = 1f;
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 230;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 2f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.hide = false;
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
			Player player = Main.player[Projectile.owner];
			if(player.HasBuff(BuffType<Buffs.TwinStarsBuff>()))
            {
				Projectile.alpha -= 10;
				if(Projectile.alpha < 0)
                {
					Projectile.alpha = 0;
                }
            }
			if (player.dead && !player.active || !player.HasBuff(BuffType<Buffs.TwinStarsBuff>()))
			{
				Projectile.alpha += 20;
			}
			if (Projectile.alpha >= 255)
            {
				Projectile.Kill();
            }
			player.GetModPlayer<WeaponPlayer>().starPosition1 = Projectile.Center;
			//player.heldProj = projectile.whoAmI;
			Vector2 ownerMountedCenter = player.RotatedRelativePoint(player.MountedCenter, true);
			Projectile.direction = player.direction;
			Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2) - 200;
			float rotationsPerSecond = rotationSpeed;
			rotationSpeed = 0.2f;
			bool rotateClockwise = true;
			//The rotation is set here

			//projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);


			for (int i = 0; i < 30; i++)
			{//Circle
				Vector2 offset = new Vector2();
				double angle = Main.rand.NextDouble() * 2d * Math.PI;
				offset.X += (float)(Math.Sin(angle) * 52);
				offset.Y += (float)(Math.Cos(angle) * 52);

				Dust d = Dust.NewDustPerfect(Projectile.Center + offset, 219, Vector2.Zero, 200, default(Color), 0.2f);
				d.fadeIn = 0.1f;
				d.noGravity = true;
			}


			for (int i = 0; i < 10; i++)
			{//Circle
				Vector2 offset = new Vector2();
				double angle = Main.rand.NextDouble() * 2d * Math.PI;
				offset.X += (float)(Math.Sin(angle) * 108);
				offset.Y += (float)(Math.Cos(angle) * 108);

				Dust d = Dust.NewDustPerfect(Projectile.Center + offset, 226, Vector2.Zero, 200, default(Color), 0.2f);
				d.fadeIn = 0.1f;
				d.noGravity = true;
			}
			for (int i = 0; i < 5; i++)
			{//Circle
				Vector2 offset = new Vector2();
				double angle = Main.rand.NextDouble() * 2d * Math.PI;
				offset.X += (float)(Math.Sin(angle) * 106);
				offset.Y += (float)(Math.Cos(angle) * 106);

				Dust d = Dust.NewDustPerfect(Projectile.Center + offset, 219, Vector2.Zero, 200, default(Color), 0.2f);
				d.fadeIn = 0.1f;
				d.noGravity = true;
			}
			for (int i = 0; i < 5; i++)
			{//Circle
				Vector2 offset = new Vector2();
				double angle = Main.rand.NextDouble() * 2d * Math.PI;
				offset.X += (float)(Math.Sin(angle) * 110);
				offset.Y += (float)(Math.Cos(angle) * 110);

				Dust d = Dust.NewDustPerfect(Projectile.Center + offset, 219, Vector2.Zero, 200, default(Color), 0.2f);
				d.fadeIn = 0.1f;
				d.noGravity = true;
			}

			

		}


	}
}
