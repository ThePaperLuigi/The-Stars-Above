using StarsAbove.Buffs;
using StarsAbove.Buffs.Skofnung;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Ranged.DevotedHavoc
{
    public class EnergyStar : ModProjectile
    {
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            // This method right here is the backbone of what we're doing here; by using this method, we copy all of
            // the Meowmere Projectile's SetDefault stats (such as projectile.friendly and projectile.penetrate) on to our projectile,
            // so we don't have to go into the source and copy the stats ourselves. It saves a lot of time and looks much cleaner;
            // if you're going to copy the stats of a projectile, use CloneDefaults().

            Projectile.CloneDefaults(ProjectileID.Shuriken);

            // To further the Cloning process, we can also copy the ai of any given projectile using AIType, since we want
            // the projectile to essentially behave the same way as the vanilla projectile.
            AIType = ProjectileID.Shuriken;

            Projectile.penetrate = 1;
            Projectile.noDropItem = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            
            base.AI();
        }

        // While there are several different ways to change how our projectile could behave differently, lets make it so
        // when our projectile finally dies, it will explode into 4 regular Meowmere projectiles.
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            target.AddBuff(BuffType<Stun>(), 60);

        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
        }
        public override void OnKill(int timeLeft)
        {
            //Shrapnel
            for (int d = 0; d < 8; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.Electric, Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-6, 6), 150, default, 1f);

            }

        }

    }
}
