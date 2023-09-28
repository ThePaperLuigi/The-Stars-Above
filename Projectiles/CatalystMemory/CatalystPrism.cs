using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace StarsAbove.Projectiles.CatalystMemory
{
    public class CatalystPrism : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Catalyst's Memory");     //The English name of the projectile
			Main.projFrames[Projectile.type] = 1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}

		public override void SetDefaults() {
			Projectile.width = 20;               //The width of projectile hitbox
			Projectile.height = 20;              //The height of projectile hitbox
			Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.penetrate = 3;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 80;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>();
			AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}
		public override bool PreDraw(ref Color lightColor)
		{
			//default(Effects.SmallPurpleTrail).Draw(Projectile);

			return true;
		}
		public override void AI()
        {
			Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmethyst, Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1), 150, default(Color), 0.5f);
			Dust.NewDust(Projectile.Center, 0, 0, DustID.PurpleCrystalShard, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default(Color), 0.7f);

			Color projcolor = new Color(174, 0, 255);
			Lighting.AddLight(Projectile.Center, projcolor.ToVector3());

			base.AI();

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			

             
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
			if(target.life <= target.lifeMax/2)
            {
				modifiers.SourceDamage += 0.3f;
            }
             
        }
        


		
		public override void OnKill(int timeLeft)
		{
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmethyst, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default(Color), 0.4f);
				Dust.NewDust(Projectile.Center, 0, 0, DustID.PurpleCrystalShard, Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8), 150, default(Color), 0.8f);
			}
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			//SoundEngine.PlaySound(SoundID., Projectile.position);
		}
	}
}
