
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Magic.EyeOfEuthymia
{
    public class EuthymiaFollowUp : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Vision of Euthymia");

            Main.projFrames[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 500;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.alpha = 0;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.light = 1f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;

            DrawOriginOffsetY = -150;
            DrawOffsetX = 20;

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
            if (Projectile.ai[0] == 1)
            {
                float rotationValue = MathHelper.ToRadians(Main.rand.Next(0, 364));
                Projectile.rotation += rotationValue - MathHelper.ToRadians(90);
                for (int d = 0; d < 8; d++)
                {
                    float Speed2 = Main.rand.NextFloat(18, 38);  //projectile speed
                    Vector2 perturbedSpeed = new Vector2((float)(Math.Cos(rotationValue) * Speed2 * -1), (float)(Math.Sin(rotationValue) * Speed2 * -1)).RotatedByRandom(MathHelper.ToRadians(5)); // 30 degree spread.
                    int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 219, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                    Main.dust[dustIndex].noGravity = true;
                }
                for (int d = 0; d < 5; d++)
                {
                    float Speed3 = Main.rand.NextFloat(20, 44);  //projectile speed
                    Vector2 perturbedSpeed = new Vector2((float)(Math.Cos(rotationValue) * Speed3 * -1), (float)(Math.Sin(rotationValue) * Speed3 * -1)).RotatedByRandom(MathHelper.ToRadians(10)); // 30 degree spread.
                    int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 86, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 2f);
                    Main.dust[dustIndex].noGravity = true;
                }
                for (int d = 0; d < 8; d++)
                {
                    float Speed2 = Main.rand.NextFloat(-18, -38);  //projectile speed
                    Vector2 perturbedSpeed = new Vector2((float)(Math.Cos(rotationValue) * Speed2 * -1), (float)(Math.Sin(rotationValue) * Speed2 * -1)).RotatedByRandom(MathHelper.ToRadians(5)); // 30 degree spread.
                    int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 219, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                    Main.dust[dustIndex].noGravity = true;
                }
                for (int d = 0; d < 5; d++)
                {
                    float Speed3 = Main.rand.NextFloat(-20, -44);  //projectile speed
                    Vector2 perturbedSpeed = new Vector2((float)(Math.Cos(rotationValue) * Speed3 * -1), (float)(Math.Sin(rotationValue) * Speed3 * -1)).RotatedByRandom(MathHelper.ToRadians(10)); // 30 degree spread.
                    int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 86, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 2f);
                    Main.dust[dustIndex].noGravity = true;
                }
            }

            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }


        }
    }
}
