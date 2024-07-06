using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;

namespace StarsAbove.Projectiles.Ranged.QuisUtDeus
{
    public class QuisUtDeusRound : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 60;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;               //The width of projectile hitbox
            Projectile.height = 8;              //The height of projectile hitbox
            Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Ranged;           //Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 180;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
            AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
        }
        public override void AI()
        {
            Projectile.scale = 0.8f;

            base.AI();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            default(Effects.SmallPinkTrail).Draw(Projectile);

            return true;
        }

        public override void OnKill(int timeLeft)
        {
            float dustAmount = 25f;
            float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() + randomConstant);
                int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmethyst);
                Main.dust[dust].scale = 1f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = Projectile.Center + spinningpoint5;
                Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 2f;
            }

        }
    }
}
