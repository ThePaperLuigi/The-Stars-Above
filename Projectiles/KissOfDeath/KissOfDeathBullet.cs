using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.KissOfDeath;

namespace StarsAbove.Projectiles.KissOfDeath
{
    public class KissOfDeathBullet : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("The Kiss of Death");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 40;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 24;               //The width of projectile hitbox
			Projectile.height = 24;              //The height of projectile hitbox
			Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 120;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = true;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.DamageType = DamageClass.Ranged;
			AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}

		
        public override void AI()
        {
			/*
			Dust.NewDust(Projectile.Center, 0, 0, 110, 0f , 0f + Main.rand.Next(-1, 1), 150, default(Color), 1f);
			
			for (int d = 0; d < 20; d++)
			{
				
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = Projectile.Center;
				Dust dust1 = Dust.NewDustPerfect(position, 220, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1f);
				dust1.noGravity = true;
			}*/

			base.AI();
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			if (target.life >= target.lifeMax / 2)
			{
				damage = (int)(damage * 1.2f);
			}
			target.AddBuff(BuffType<SecurityLevel>(), 12 * 60);
			base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        public override bool PreDraw(ref Color lightColor)
		{
			default(Effects.YellowTrail).Draw(Projectile);

			return true;
		}

		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}
	}
}
