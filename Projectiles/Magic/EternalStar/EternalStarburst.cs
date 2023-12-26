
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Magic.EternalStar
{
    public class EternalStarburst : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eternal Star");

        }

        public override void SetDefaults()
        {
            Projectile.width = 1750;
            Projectile.height = 1750;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 1;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.tileCollide = false;

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
            //Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/GunbladeImpact"));

            Projectile.ai[0] += 1f;
            for (int d = 0; d < 30; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, 226, 0f + Main.rand.Next(-50, 50), 0f + Main.rand.Next(-50, 50), 150, default, 1.5f);
            }
            for (int d = 0; d < 44; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, 91, 0f + Main.rand.Next(-35, 35), 0f + Main.rand.Next(-15, 15), 150, default, 1.5f);
            }
            for (int d = 0; d < 26; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, 20, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 150, default, 1.5f);
            }
            for (int d = 0; d < 30; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, 91, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default, 1.5f);
            }


            // Fire Dust spawn
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 91, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 91, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
                Main.dust[dustIndex].velocity *= 3f;
            }

            // Fade in
            Projectile.alpha--;
            if (Projectile.alpha < 100)
            {
                Projectile.alpha = 100;
            }


        }
    }
}
