using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Ozma
{
    public class OzmaOrbAttack : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Ozma Ascendant");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults() {
			Projectile.width = 200;               //The width of projectile hitbox
			Projectile.height = 200;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.scale = 1f;
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 30;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 2f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.hide = false;
			Projectile.DamageType = DamageClass.Magic;
		}
		float rotationSpeed = 10f;
		float expandStrength = 0f;

		bool firstSpawn = true;
		public override void AI()
		{
			if(firstSpawn)
            {
				Projectile.scale = Main.rand.NextFloat(0.1f, 1f);
				
				firstSpawn = false;
            }

			Player owner = Main.player[Projectile.owner];

			if (Projectile.ai[0] > 20)
			{

				for (int i = 0; i < Main.maxProjectiles; i++)
				{
					Projectile other = Main.projectile[i];

					if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner &&
						(other.type == ProjectileType<OzmaOrbAttack>()))
					{
						for (int ir = 0; ir < 50; ir++)
						{
							Vector2 position = Vector2.Lerp(Projectile.Center, other.Center, (float)ir / 50);
							Dust d = Dust.NewDustPerfect(position, DustID.LifeDrain, null, 240, default(Color), 0.7f);
							d.fadeIn = 0.3f;
							d.noLight = true;
							d.noGravity = true;

						}
						for (int ix = 0; ix < 8; ix++)
						{
							Vector2 position = Vector2.Lerp(Projectile.Center, other.Center, (float)ix / 8);
							int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, 0, 0, ProjectileType<OzmaDamage>(), Projectile.damage, 0f, owner.whoAmI);

							Main.projectile[index].originalDamage = Projectile.damage;

						}
					}
				}
				Projectile.ai[0] = 0;
			}
			Projectile.ai[0]++;

			Projectile.alpha -= 20;
			expandStrength += 0.001f;
			Projectile.scale += 0.01f;
			


			float rotationsPerSecond = rotationSpeed;
			rotationSpeed = 0.4f;
			bool rotateClockwise = true;
			//The rotation is set here
			
			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
			if(Projectile.timeLeft < 5)
            {
				Projectile.scale -= 0.05f;
            }
		}
		
		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			//Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			Player projOwner = Main.player[Projectile.owner];
			



		}
		public override bool? CanCutTiles()
		{
			return false;
		}
	}
}
