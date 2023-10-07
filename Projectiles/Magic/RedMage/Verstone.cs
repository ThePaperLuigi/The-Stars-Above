using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Magic.RedMage
{
    public class Verstone : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Verstone");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 70;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;               //The width of projectile hitbox
            Projectile.height = 24;              //The height of projectile hitbox
            Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.DamageType = DamageClass.Magic;
            AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            //If collide with tile, reduce the penetrate.
            //So the projectile can reflect at most 5 times

            return true;
        }
        public override void AI()
        {
            for (int d = 0; d < 2; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Dirt, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default, 0.4f);
                Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmber, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default, 0.7f);

            }
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
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hit.Crit)
            {
                Player player = Main.player[Projectile.owner];
                if (target.CanBeChasedBy() && !target.SpawnedFromStatue)
                {
                    player.Heal(12);
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {



        }
        public override bool PreDraw(ref Color lightColor)
        {
            default(Effects.YellowTrail).Draw(Projectile);

            return true;
        }

        public override void OnKill(int timeLeft)
        {
            for (int d = 0; d < 14; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, 0, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default, 1.5f);
            }
            for (int d = 0; d < 16; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Firework_Yellow, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default, 1.5f);
            }
            for (int d = 0; d < 10; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, 7, 0f + Main.rand.Next(-4, 4), 0f + Main.rand.Next(-4, 4), 150, default, 1.5f);
            }
            for (int d = 0; d < 10; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, 269, 0f + Main.rand.Next(-4, 4), 0f + Main.rand.Next(-4, 4), 150, default, 1.5f);
            }
            for (int d = 0; d < 10; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.AmberBolt, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default, 1.5f);
            }
            // Smoke Dust spawn
            for (int i = 0; i < 20; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            //SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}
