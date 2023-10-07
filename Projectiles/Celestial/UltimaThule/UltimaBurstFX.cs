
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Celestial.UltimaThule
{
    public class UltimaBurstFX : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ultima Thule");
            //Main.projFrames[projectile.type] = 14;
        }

        public override void SetDefaults()
        {
            Projectile.width = 300;
            Projectile.height = 300;
            Projectile.aiStyle = 1;
            Projectile.penetrate = -1;
            Projectile.scale = 1;
            Projectile.alpha = 255;
            Projectile.damage = 0;
            Projectile.hide = false;
            Projectile.ownerHitCheck = true;
            Projectile.tileCollide = false;
            Projectile.friendly = true;

        }
        int timer;
        int fadeIn = 0;

        float projectileVelocity = 15;

        // In here the AI uses this example, to make the code more organized and readable
        // Also showcased in ExampleJavelinProjectile.cs
        public float movementFactor // Change this value to alter how fast the spear moves
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        float rotationSpeed = 10f;

        bool firstSpawn = true;
        public override void AI()
        {
            if (firstSpawn)
            {
                Projectile.scale = 0f;
                firstSpawn = false;
            }

            Player player = Main.player[Projectile.owner];

            Projectile.timeLeft = 10;

            if (!player.HasBuff(BuffType<Buffs.CosmicConception>()))
            {
                Projectile.scale -= 0.02f;
                Projectile.alpha += 25;
            }
            Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;

            Projectile.scale += 0.006f;
            if (Projectile.alpha > 255)
            {
                Projectile.Kill();
            }
            Vector2 ownerMountedCenter = player.Center;
            Projectile.position.X = ownerMountedCenter.X - Projectile.width / 2;
            Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2;
            Projectile.alpha -= 5;


            float rotationsPerSecond = rotationSpeed;
            rotationSpeed -= 0.4f;
            bool rotateClockwise = false;
            //The rotation is set here

            Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
        }


    }
}
