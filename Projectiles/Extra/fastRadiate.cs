
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Extra
{
    public class fastRadiate : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Radiate");

        }

        public override void SetDefaults()
        {
            Projectile.width = 250;
            Projectile.height = 250;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 300;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;


        }
        int slowfade;
        bool start = true;
        // In here the AI uses this example, to make the code more organized and readable
        // Also showcased in ExampleJavelinProjectile.cs



        public override void AI()
        {
            Projectile.damage = 0;
            if (start)
            {
                Projectile.scale = 0.1f;
                start = false;
            }
            Projectile.ai[0] += 1f;
            Projectile.scale += 0.1f;
            //projectile.rotation++;
            float rotationsPerSecond = 0.7f;
            bool rotateClockwise = true;
            //The rotation is set here
            Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
            // Fade in
            if (Projectile.ai[0] <= 30)
            {
                Projectile.alpha -= 4;
                if (Projectile.alpha < 0)
                {
                    Projectile.alpha = 0;
                }
            }
            else
            {
                Projectile.alpha += 8;
            }



        }
    }
}
