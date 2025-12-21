using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.Magic.Ozma;

namespace StarsAbove.Projectiles.Magic.Ozma
{
    public class OzmaAttack3 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ozma Ascendant");     //The English name of the projectile
            Main.projFrames[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;               //The width of projectile hitbox
            Projectile.height = 70;              //The height of projectile hitbox
            Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.penetrate = 99;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 300;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.DamageType = DamageClass.Magic;
            AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
        }
        int timeToDeath;
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            //If collide with tile, reduce the penetrate.
            //So the projectile can reflect at most 5 times
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {

                Projectile.Kill();
            }
            else
            {
                Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            return false;
        }
        public override void AI()
        {
            if (Projectile.penetrate < 99)
            {
                Projectile.velocity *= 0.9f;
                Projectile.alpha += 10;
                timeToDeath++;

            }
            if (timeToDeath >= 20)
            {
                Projectile.Kill();
            }

            Dust.NewDust(Projectile.Center, 0, 0, DustID.PlatinumCoin, 0f, 0f + Main.rand.Next(-1, 1), 150, default, 1f);
            for (int d = 0; d < 10; d++)
            {

                Vector2 position = Projectile.Center;
                Dust dust1 = Dust.NewDustPerfect(position, DustID.PlatinumCoin, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1f);
                dust1.noGravity = true;
            }
            Player projOwner = Main.player[Projectile.owner];
            if (projOwner.HasBuff(BuffType<AnnihilationState>()))
            {
                for (int d = 0; d < 20; d++)
                {

                    Vector2 position = Projectile.Center;
                    Dust dust1 = Dust.NewDustPerfect(position, DustID.LifeDrain, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1f);
                    dust1.noGravity = true;
                }

            }
            if (projOwner.HasBuff(BuffType<AnnihilationState>()))
            {
                Projectile.frame = 1;

            }
            else
            {
                Projectile.frame = 0;
            }
            base.AI();
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int d = 0; d < 8; d++)
            {
                Dust.NewDust(target.Center, 0, 0, 219, Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-7, 7), 150, default, 0.9f);

            }
            Player projOwner = Main.player[Projectile.owner];
            if (hit.Crit)
            {
                projOwner.AddBuff(BuffType<AnnihilationState>(), 180);
            }

        }
        public override void OnKill(int timeLeft)
        {
            //for (int d = 0; d < 8; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.GemDiamond, Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-7, 7), 150, default, 0.9f);

            }
        }
    }
}
