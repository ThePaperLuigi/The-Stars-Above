using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.GoddessOfMemory
{
    public class BossExpungenceExplosion : ModProjectile
    {
        public const int ExplosionDamage = 200;
        public const float ExplosionRadius = BossConstellationAttackStar.ExplosionRadius;

        private readonly bool[] hitPlayers = new bool[Main.maxPlayers];
        private bool particlesSpawned;

        public override string Texture => "StarsAbove/Projectiles/Bosses/GoddessOfMemory/BossConstellationAttackStar";

        public override void SetDefaults()
        {
            Projectile.width = (int)(ExplosionRadius * 2f);
            Projectile.height = (int)(ExplosionRadius * 2f);
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
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
            return Projectile.localAI[0] == 1f;
        }

        public override bool CanHitPlayer(Player target)
        {
            return target.active && !target.dead && !target.ghost && target.whoAmI != (int)Projectile.ai[0] && !hitPlayers[target.whoAmI];
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 closestPoint = targetHitbox.ClosestPointInRect(Projectile.Center);
            return Vector2.DistanceSquared(Projectile.Center, closestPoint) <= ExplosionRadius * ExplosionRadius;
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            modifiers.ModifyHurtInfo += SetExplosionDamage;
        }

        private static void SetExplosionDamage(ref Player.HurtInfo info)
        {
            info.Damage = ExplosionDamage;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            hitPlayers[target.whoAmI] = true;
            if (info.Damage > 0 && target.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center, Vector2.Zero, ModContent.ProjectileType<BossExpungenceHitEffect>(), 0, 0f, target.whoAmI);
            }
        }

        public override void AI()
        {
            Projectile.localAI[0]++;
            if (Main.dedServ || particlesSpawned)
            {
                return;
            }

            particlesSpawned = true;
            SpawnParticles(Projectile.Center);
        }

        internal static void SpawnParticles(Vector2 center)
        {
            SoundEngine.PlaySound(SoundID.Item14, center);

            for (int i = 0; i < 60; i++)
            {
                Vector2 velocity = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(4f, 13f);
                Dust ruby = Dust.NewDustPerfect(center, DustID.GemRuby, velocity, 0, default, Main.rand.NextFloat(1.5f, 2.5f));
                ruby.noGravity = true;
                ruby.noLight = false;
            }

            for (int i = 0; i < 36; i++)
            {
                Vector2 velocity = Main.rand.NextVector2Circular(10f, 10f);
                Dust flame = Dust.NewDustPerfect(center, DustID.Torch, velocity, 0, default, Main.rand.NextFloat(2f, 3.5f));
                flame.noGravity = true;
            }

            for (int i = 0; i < 24; i++)
            {
                Vector2 velocity = Main.rand.NextVector2Circular(6f, 6f);
                Dust smoke = Dust.NewDustPerfect(center, DustID.Smoke, velocity, 100, default, Main.rand.NextFloat(1.5f, 2.5f));
                smoke.noGravity = true;
            }

            for (int i = 0; i < 24; i++)
            {
                Vector2 direction = (MathHelper.TwoPi * i / 24f).ToRotationVector2();
                Dust spark = Dust.NewDustPerfect(center, DustID.GemDiamond, direction * 9f, 0, Color.White, 1.4f);
                spark.noGravity = true;
                spark.noLight = false;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }
    }
}
