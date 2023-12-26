
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Celestial.BuryTheLight
{
    public class BuryTheLightFollowUp : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ultima Thule Follow-Up");
            //DrawOriginOffsetY = -150;
            //DrawOffsetX = 20;
            Main.projFrames[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.width = 192;
            Projectile.height = 192;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 500;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            //Projectile.DamageType = DamageClass.Melee;
            Projectile.alpha = 0;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.light = 1f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;

        }

        bool firstSpawn = true;

        public override void AI()
        {


            Projectile.ai[0] += 1f;

            if (firstSpawn)
            {
                float rotationValue = MathHelper.ToRadians(Main.rand.Next(0, 364));
                Projectile.rotation += rotationValue - MathHelper.ToRadians(90);
                firstSpawn = false;
            }

            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                if (Projectile.frame < 4)
                {
                    Projectile.frame++;
                }
                else
                {
                    Projectile.Kill();
                }

            }


            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }


        }
    }
}
