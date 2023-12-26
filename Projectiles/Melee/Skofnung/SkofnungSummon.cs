using StarsAbove.Buffs.Skofnung;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Melee.Skofnung
{
    public class SkofnungSummon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Skofnung");
        }

        public override void SetDefaults()
        {
            // This method right here is the backbone of what we're doing here; by using this method, we copy all of
            // the Meowmere Projectile's SetDefault stats (such as projectile.friendly and projectile.penetrate) on to our projectile,
            // so we don't have to go into the source and copy the stats ourselves. It saves a lot of time and looks much cleaner;
            // if you're going to copy the stats of a projectile, use CloneDefaults().

            Projectile.CloneDefaults(ProjectileID.EmpressBlade);

            // To further the Cloning process, we can also copy the ai of any given projectile using AIType, since we want
            // the projectile to essentially behave the same way as the vanilla projectile.
            AIType = ProjectileID.EmpressBlade;

            // After CloneDefaults has been called, we can now modify the stats to our wishes, or keep them as they are.
            // For the sake of example, lets make our projectile penetrate enemies a few more times than the vanilla projectile.
            // This can be done by modifying projectile.penetrate
            Projectile.width = 146;
            Projectile.height = 146;
            Projectile.minion = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.minionSlots = 0f;
            Projectile.timeLeft = 240;
            Projectile.penetrate = 999;
            Projectile.hide = false;
            Projectile.alpha = 255;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 240;
            DrawOffsetX = -14;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            player.empressBlade = false;
            if (!player.HasBuff(BuffType<SkofnungBuff>()))
            {
                Projectile.Kill();
            }
            Projectile.timeLeft = 10;
            Projectile.alpha -= 10;
            Projectile.damage = player.statDefense / 2;

            base.AI();
        }

        // While there are several different ways to change how our projectile could behave differently, lets make it so
        // when our projectile finally dies, it will explode into 4 regular Meowmere projectiles.
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];


            if (hit.Crit)
            {

                player.AddBuff(BuffType<BloodstainedBelone>(), 480);

            }



        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.CritDamage *= 0.5f;
        }
        public override void OnKill(int timeLeft)
        {


        }

    }
}
