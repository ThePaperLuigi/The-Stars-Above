using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Penthesilea
{
    public class BrushStrokeDamage : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Brush Stroke");

        }

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 1;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.damage = 60;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.netUpdate = true;


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
            Projectile.damage = 60;
            // Fade in
            Projectile.alpha--;
            if (Projectile.alpha < 100)
            {
                Projectile.alpha = 100;
            }


        }
    }
}
