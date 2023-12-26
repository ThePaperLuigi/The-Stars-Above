using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Magic.SupremeAuthority
{
    public class SwordstormEffect : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Supreme Authority");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;                               //The width of projectile hitbox
            Projectile.height = 20;                               //The height of projectile hitbox
            Projectile.aiStyle = 0;                              //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;                             //Can the projectile deal damage to enemies?
            Projectile.hostile = false;                          //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Magic;           //Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = 10;                          //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 160;                            //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 255;                           //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 1f;                   //How much light emit around the projectile
            Projectile.ignoreWater = true;                      //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;                     //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;                            //Set to above 0 if you want the projectile to update multiple time in a frame

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        bool onSpawn = true;
        float randomRotationAmount;
        float projSpeed = 1f; // The speed at which the projectile moves towards the target
        Vector2 pos;
        public override void AI()
        {
            if (onSpawn)
            {
                Projectile.timeLeft += Main.rand.Next(0, 30);
                randomRotationAmount = Main.rand.Next(0, 129);
                Projectile.scale = Math.Max(Main.rand.NextFloat(), 0.3f);
                onSpawn = false;
            }
            Projectile.velocity *= 0.96f;
            if (randomRotationAmount > 0)
            {
                Projectile.rotation += MathHelper.ToRadians(randomRotationAmount);
                randomRotationAmount *= 0.9f;
            }
            //Projectile.velocity.Y = Projectile.velocity.Y + 0.05f; // 0.1f for arrow gravity, 0.4f for knife gravity

            if (Projectile.timeLeft == 20)
            {
                //Projectile.velocity
            }
            if (Projectile.timeLeft < 30)
            {



            }
            else
            {
                Projectile.alpha -= 15;

            }
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile other = Main.projectile[i];

                if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner &&
                    other.type == ProjectileType<AuthoritySwordstorm>() && other.frame > 2)
                {
                    pos = other.Center;
                    Projectile.velocity = (other.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
                    projSpeed *= 1.6f;

                }

                if (Projectile.Distance(pos) < 20)
                {
                    Projectile.Kill();
                }
            }
            base.AI();
        }
        public override void OnKill(int timeLeft)
        {


        }
    }
}
