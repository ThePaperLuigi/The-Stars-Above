
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace StarsAbove.Projectiles.StellarNovas.FireflyTypeIV
{
    public class FireflySlashFollowUp : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bury The Light");

            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 500;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;



            Projectile.alpha = 0;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.light = 1f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        // In here the AI uses this example, to make the code more organized and readable
        // Also showcased in ExampleJavelinProjectile.cs
        public float movementFactor // 
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        /*public override bool PreDraw(ref Color lightColor)
		{
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
			//drawColor = Color.White;
			
			return true;
		}*/
        public override void AI()
        {
            DrawOriginOffsetY = -200;
            DrawOffsetX = 48;

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

                float rotationValue = MathHelper.ToRadians(Main.rand.Next(0, 364));
                Projectile.rotation += rotationValue - MathHelper.ToRadians(90);
                for (int d = 0; d < 12; d++)
                {
                    float Speed2 = Main.rand.NextFloat(-18, -38);  //projectile speed
                    Vector2 perturbedSpeed = new Vector2((float)(Math.Cos(rotationValue) * Speed2 * -1), (float)(Math.Sin(rotationValue) * Speed2 * -1)).RotatedByRandom(MathHelper.ToRadians(5)); // 30 degree spread.
                    int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemEmerald, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f); ;
                    Main.dust[dustIndex].noGravity = true;
                }
                for (int d = 0; d < 13; d++)
                {
                    float Speed3 = Main.rand.NextFloat(-20, -44);  //projectile speed
                    Vector2 perturbedSpeed = new Vector2((float)(Math.Cos(rotationValue) * Speed3 * -1), (float)(Math.Sin(rotationValue) * Speed3 * -1)).RotatedByRandom(MathHelper.ToRadians(10)); // 30 degree spread.
                    int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemTopaz, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                    Main.dust[dustIndex].noGravity = true;
                }
                /*for (int d = 0; d < 2; d++)
				{
					float Speed2 = Main.rand.NextFloat(-18, -38);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotationValue) * Speed2) * -1), (float)((Math.Sin(rotationValue) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(5)); // 30 degree spread.
					int dustIndex = Dust.NewDust(projectile.Center, 0, 0, 221, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
					Main.dust[dustIndex].noGravity = true;
				}
				for (int d = 0; d < 3; d++)
				{
					float Speed3 = Main.rand.NextFloat(-20, -44);  //projectile speed
					Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotationValue) * Speed3) * -1), (float)((Math.Sin(rotationValue) * Speed3) * -1)).RotatedByRandom(MathHelper.ToRadians(10)); // 30 degree spread.
					int dustIndex = Dust.NewDust(projectile.Center, 0, 0, 20, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 2f);
					Main.dust[dustIndex].noGravity = true;
				}*/

            }

            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }


        }
    }
}
