using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.GoddessOfMemory
{
    public class BossExpungenceHitEffect : ModProjectile
    {
        private bool particlesSpawned;

        public override string Texture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/BossConstellationAttackStar";

        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.hide = true;
            Projectile.timeLeft = 12;
            Projectile.netImportant = true;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override void AI()
        {
            if (!Main.dedServ && !particlesSpawned)
            {
                particlesSpawned = true;
                BossExpungenceExplosion.SpawnParticles(Projectile.Center);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
    }
}
