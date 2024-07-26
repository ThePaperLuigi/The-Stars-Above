
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Summon.PhantomInTheMirror
{
    public class BloodstainedCrescent : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bloodstained Crescent");
            //DrawOriginOffsetY = 50;
            //DrawOffsetX = 50;
        }

        public override void SetDefaults()
        {
            Projectile.width = 400;
            Projectile.height = 400;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 5;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.minion = true;
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
            float dustAmount = 30f;
            float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(1f, 1f);
                spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() + randomConstant);
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain);
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = new Vector2(Projectile.Center.X - 50, Projectile.Center.Y) + spinningpoint5;
                Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 12f;
            }
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(1f, 1f);
                spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() + randomConstant);
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain);
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = new Vector2(Projectile.Center.X + 50, Projectile.Center.Y) + spinningpoint5;
                Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 12f;
            }
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(1f, 1f);
                spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() + randomConstant);
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain);
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = new Vector2(Projectile.Center.X, Projectile.Center.Y - 50) + spinningpoint5;
                Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 12f;
            }
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(1f, 1f);
                spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() + randomConstant);
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain);
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = new Vector2(Projectile.Center.X, Projectile.Center.Y + 50) + spinningpoint5;
                Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 12f;
            }

            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(115f, 4f);
                spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() + randomConstant);
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain);
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = Projectile.Center + spinningpoint5;
                Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 13f;
            }
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(115f, 4f);
                spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() + randomConstant + MathHelper.ToRadians(90));
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain);
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = Projectile.Center + spinningpoint5;
                Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 13f;
            }
            


        }
    }
}
