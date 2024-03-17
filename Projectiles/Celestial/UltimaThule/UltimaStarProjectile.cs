using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Celestial.UltimaThule
{
    public class UltimaStarProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ultima Thule");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 70;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 54;               //The width of projectile hitbox
            Projectile.height = 54;              //The height of projectile hitbox
            Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.minion = true;           //Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = 99;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
            AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            //If collide with tile, reduce the penetrate.
            //So the projectile can reflect at most 5 times

            return false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            float distance = Vector2.Distance(player.Center, Projectile.Center);
            if (distance < 250f)
            {
                Projectile.scale -= 0.015f;
                Projectile.alpha += 3;
            }
            if (distance < 50f)
            {
                Projectile.Kill();
            }
            if (!player.HasBuff(BuffType<Buffs.Celestial.UltimaThule.CosmicConception>()))
            {
                Projectile.alpha += 15;
            }
            if (Projectile.alpha > 255)
            {
                Projectile.Kill();
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            default(Effects.UltimaBlueTrail).Draw(Projectile);

            return true;
        }
        public override void OnKill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            //Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);

            // Play explosion sound
            //Main.PlaySound(SoundID.Drown, projectile.position);
            // Smoke Dust spawn

            // Fire Dust spawn (CHANGE TO ICE DUST)

            // Large Smoke Gore spawn


        }
    }
}
