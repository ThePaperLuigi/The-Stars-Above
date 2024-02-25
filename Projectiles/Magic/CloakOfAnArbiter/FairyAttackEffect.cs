
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Magic.CloakOfAnArbiter
{
    public class FairyAttackEffect : ModProjectile
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
            Projectile.timeLeft = 100;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.hostile = false;
            Projectile.friendly = false;
            Projectile.light = 0f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            DrawOriginOffsetY = -200;
            DrawOffsetX = 45;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public static Texture2D texture;
        public override bool PreDraw(ref Color lightColor)
        {
            
            return true;
        }
        float projectileScaleExtra = 0.1f;
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, TorchID.Ichor);


            if (Projectile.ai[0] == 0)
            {
                if (Projectile.ai[2] == 1)
                {
                    Projectile.scale = Main.rand.NextFloat(0.4f, 0.7f);

                }
                else
                {
                    Projectile.scale = Main.rand.NextFloat(0.2f, 0.3f);

                }

                Projectile.rotation += MathHelper.ToRadians(Main.rand.Next(0, 364));

            }
            Projectile.ai[0] += 1f;

            Projectile.position += Projectile.velocity;
            Projectile.velocity *= 0.9f;
            Projectile.alpha += 6;
            if(projectileScaleExtra > 0f)
            {
                projectileScaleExtra -= 0.003f;
                Projectile.scale -= 0.003f;
            }

            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }


        }
    }
}
