using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Melee.YunlaiStiletto
{
    public class StilettoMarker : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Yunlai Stiletto");

        }

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 999999;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.hostile = true;


        }

        // In here the AI uses this example, to make the code more organized and readable
        // Also showcased in ExampleJavelinProjectile.cs
        public float movementFactor // Change this value to alter how fast the spear moves
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }


        public override void AI()
        {

            Projectile.ai[0] += 1f;
            Player projOwner = Main.player[Projectile.owner];

            // Fade in
            Projectile.alpha -= 10;
            if (Projectile.alpha < 100)
            {
                Projectile.alpha = 100;
            }

            if (projOwner.GetModPlayer<WeaponPlayer>().yunlaiTeleport == false)
            {
                Projectile.Kill();
            }
            if (Main.rand.NextBool(5))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, 20,
                    Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 269, Scale: 1.2f);
                dust.velocity += Projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
        }
    }
}
