using Microsoft.Xna.Framework;
using System;

using Terraria.ID;

using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;

namespace StarsAbove.Projectiles.Bosses.Dioskouroi
{
    public class CastorFireCircle : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Blazing Circle");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			//Main.projFrames[projectile.type] = 2;
			//DrawOriginOffsetY = -15;
			//DrawOffsetX = -26;
		}

		public override void SetDefaults() {
			Projectile.width = 40;               //The width of projectile hitbox
			Projectile.height = 106;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.minion = false;
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 60;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 200;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 2f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
		}
		float rotationSpeed = 10f;
		bool attack = false;
		bool firstSpawn = true;
		public override void AI()
		{
			Projectile.velocity = Vector2.Zero;
			Player projOwner = Main.player[Projectile.owner];
            Player player = Main.player[Projectile.owner];
			Projectile.ai[0]--;
            Projectile.timeLeft = 10;
			if (firstSpawn)
			{
				Projectile.rotation = MathHelper.ToRadians(Projectile.ai[1]);//+ MathHelper.ToRadians(-135f)

				firstSpawn = false;

			}
			if (Projectile.ai[0] < 20)
			{
				Projectile.alpha += 5;
				Projectile.scale -= 0.03f;
			}
			else
            {
				
				Projectile.alpha -= 10;
				if (Projectile.alpha < 0)
				{
					Projectile.alpha = 0;
				}
			}
			if(Projectile.ai[0] < 20 && !attack)
			{
				SoundEngine.PlaySound(SoundID.Item15, Projectile.Center);

				float Speed = 3f;
				int type = ProjectileType<CastorBolt>();
				attack = true;
				
				int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)((Math.Cos(Projectile.rotation) * Speed)), (float)((Math.Sin(Projectile.rotation) * Speed)), type, (Projectile.damage), 2f, player.whoAmI, Projectile.rotation + MathHelper.ToRadians(90f));

				Main.projectile[index].originalDamage = Projectile.damage;
				for (int d = 0; d < 16; d++)
				{
					float Speed2 = Main.rand.NextFloat(2, 5);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(Projectile.rotation) * Speed2)), (float)((Math.Sin(Projectile.rotation) * Speed2))).RotatedByRandom(MathHelper.ToRadians(140)); // 30 degree spread.
					int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.Flare, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;
				}
			}
			if (Projectile.alpha >= 255)
            {
				Projectile.Kill();
            }
			Projectile.spriteDirection = Projectile.direction;
			



		}



	}
}
