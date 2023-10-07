
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Celestial.BuryTheLight
{
    public class BuryTheLightSlash2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bury The Light");

            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 500;
            Projectile.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>();

            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.light = 1f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;
            DrawOriginOffsetY = -200;
            DrawOffsetX = 45;
        }

        // In here the AI uses this example, to make the code more organized and readable
        // Also showcased in ExampleJavelinProjectile.cs
        public float movementFactor // 
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override void AI()
        {


            Projectile.ai[0] += 1f;


            if (++Projectile.frameCounter >= 2)
            {
                Projectile.frameCounter = 0;
                if (Projectile.frame < 3)
                {
                    Projectile.frame++;
                }
                else
                {
                    Projectile.Kill();
                }

            }
            if (Projectile.ai[0] == 1)
            {

                Projectile.rotation += MathHelper.ToRadians(Main.rand.Next(0, 364));

            }
            Projectile.position += Projectile.velocity;
            Projectile.velocity *= 0.9f;
            Projectile.alpha += 10;

            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }


        }
    }
}
