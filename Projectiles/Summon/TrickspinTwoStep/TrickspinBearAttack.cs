using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;
using StarsAbove.Buffs.TrickspinTwoStep;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Summon.TrickspinTwoStep
{
    public class TrickspinBearAttack : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;

        }

        public override void SetDefaults()
        {
            Projectile.width = 32;               //The width of projectile hitbox
            Projectile.height = 32;              //The height of projectile hitbox
            Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 30;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.DamageType = DamageClass.Summon;
            AIType = ProjectileID.Bullet;           //Act exactly like default Bullet

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        public override bool PreDraw(ref Color lightColor)
        {

            return true;
        }
        bool firstSpawn = true;
        public override void AI()
        {

            if (firstSpawn)
            {
                Projectile.frame = Main.rand.Next(0, 2);
                Projectile.scale = 1.3f;
                firstSpawn = false;
            }
            Projectile.rotation += MathHelper.ToRadians(-90);
            Projectile.velocity *= 0.98f;

            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);
            base.AI();

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player owner = Main.player[Projectile.owner];
            owner.AddBuff(BuffType<KickStartBuff>(), 120);

            float dustAmount = 12f;
            float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(15f, 1f);
                spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant);
                int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemTopaz);
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = target.Center + spinningpoint5;
                Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 3f;
            }
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(15f, 1f);
                spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant + MathHelper.ToRadians(90));
                int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemTopaz);
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = target.Center + spinningpoint5;
                Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 3f;
            }


        }


        public override void OnKill(int timeLeft)
        {
            for (int d = 0; d < 23; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(6));
                float scale = 1f + Main.rand.NextFloat() * 0.6f;
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemTopaz, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.6f);
                Main.dust[dustIndex].noGravity = true;

            }

        }
    }
}
