using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Magic.RedMage
{
    public class Veraero : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Veraero");     //The English name of the Projectile
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
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 200;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
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
            Dust.NewDust(Projectile.Center, 0, 0, DustID.GemEmerald, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default, 1.5f);


            base.AI();
        }

        public override bool PreDraw(ref Color lightColor)
        {
            default(Effects.VeraeroVFX).Draw(Projectile);

            return true;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Projectile.damage = (int)(Projectile.damage * 0.8);
            // 
        }
        public override void OnKill(int timeLeft)
        {


            for (int d = 0; d < 16; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.GemEmerald, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-12, 12), 150, default, 1.5f);
            }
            for (int d = 0; d < 10; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Firework_Green, 0f + Main.rand.Next(-4, 4), 0f + Main.rand.Next(-14, 14), 150, default, 1.5f);
            }
            for (int d = 0; d < 10; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Green, 0f + Main.rand.Next(-4, 4), 0f + Main.rand.Next(-14, 14), 150, default, 1.5f);
            }
            for (int d = 0; d < 10; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.GreenFairy, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-9, 9), 150, default, 1.5f);
            }

            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            //SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }
    }
}
