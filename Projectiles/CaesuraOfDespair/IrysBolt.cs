using Microsoft.Xna.Framework;
using StarsAbove.Effects;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.CaesuraOfDespair
{
    public class IrysBolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Irys Bolt");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;    //The length of old position to be recorded
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
            Projectile.DamageType = DamageClass.Summon;

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            //If collide with tile, reduce the penetrate.
            //So the projectile can reflect at most 5 times

            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            default(PurpleTrail).Draw(Projectile);

            return true;
        }

        public override void OnKill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);

            // Play explosion sound
            //Main.PlaySound(SoundID.Drown, projectile.position);
            // Smoke Dust spawn
            for (int i = 0; i < 3; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            // Fire Dust spawn (CHANGE TO ICE DUST)
            for (int i = 0; i < 1; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 15, 0f, 0f, 100, default, 3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 20, 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 3f;
            }
            // Large Smoke Gore spawn


        }
    }
}
