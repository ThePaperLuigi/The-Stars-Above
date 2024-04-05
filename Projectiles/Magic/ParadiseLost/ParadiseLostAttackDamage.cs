
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Magic.ParadiseLost
{
    public class ParadiseLostAttackDamage : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Vermilion Riposte");

        }

        public override void SetDefaults()
        {
            Projectile.width = 250;
            Projectile.height = 250;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 3;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
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
            float dustAmount = 120f;
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(5f, 5f);
                //spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain);
                Main.dust[dust].scale = 2f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = Projectile.Center + spinningpoint5;
                Main.dust[dust].velocity = spinningpoint5.SafeNormalize(Vector2.UnitY) * 30;
            }
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(5f, 5f);
                //spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain);
                Main.dust[dust].scale = 1f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = Projectile.Center + spinningpoint5;
                Main.dust[dust].velocity = spinningpoint5.SafeNormalize(Vector2.UnitY) * 20;
            }

            Projectile.ai[0] += 1f;
            for (int d = 0; d < 10; d++)
            {
                Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default, 1.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {



        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {



        }
    }
}
