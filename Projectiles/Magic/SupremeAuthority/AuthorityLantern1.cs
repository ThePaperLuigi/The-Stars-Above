using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using StarsAbove.Buffs.Magic.SupremeAuthority;
using StarsAbove.Systems;

namespace StarsAbove.Projectiles.Magic.SupremeAuthority
{
    public class AuthorityLantern1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Supreme Authority");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
            Main.projFrames[Projectile.type] = 2;

        }

        public override void SetDefaults()
        {
            Projectile.width = 26;               //The width of projectile hitbox
            Projectile.height = 48;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.minion = false;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 2f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
            DrawOriginOffsetY = -15;
            DrawOffsetX = -26;
        }
        bool firstSpawn = true;
        float rotationSpeed = 10f;
        int offsetVelocity;
        int idlePause;
        bool floatUpOrDown; //false is Up, true is Down
        public override void AI()
        {
            Projectile.scale = 0.9f;
            Projectile.timeLeft = 10;
            Player projOwner = Main.player[Projectile.owner];
            Player player = Main.player[Projectile.owner];
            if (projOwner.dead && !projOwner.active || !projOwner.HasBuff(BuffType<DeifiedBuff>()))
            {
                Projectile.Kill();
            }
            if (firstSpawn)
            {
                float dustAmount = 33f;
                for (int i = 0; i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(18f, 1f);
                    spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() + MathHelper.ToRadians(90));
                    int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmethyst);
                    Main.dust[dust].scale = 1f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = Projectile.Center + spinningpoint5;
                    Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
                }

                firstSpawn = false;
            }

            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            Projectile.direction = projOwner.direction;
            Projectile.position.X = ownerMountedCenter.X - Projectile.width / 2;
            Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = player.velocity.X * 0.05f;

            if (projOwner.GetModPlayer<WeaponPlayer>().SupremeAuthorityEncroachingStacks >= 1)
            {
                Projectile.frame = 1;
            }
            else
            {
                Projectile.frame = 0;
            }

            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            if (Projectile.alpha > 255)
            {
                Projectile.alpha = 255;
            }


            UpdateMovement();



        }
        public override void OnKill(int timeLeft)
        {

            base.OnKill(timeLeft);
        }
        private void UpdateMovement()
        {
            if (floatUpOrDown)//Up
            {
                if (Projectile.ai[0] > 7)
                {
                    DrawOriginOffsetY++;
                    Projectile.ai[0] = 0;
                }
            }
            else
            {
                if (Projectile.ai[0] > 7)
                {
                    DrawOriginOffsetY--;
                    Projectile.ai[0] = 0;
                }
            }
            if (DrawOriginOffsetY > -10)
            {
                idlePause = 10;
                DrawOriginOffsetY = -10;
                floatUpOrDown = false;

            }
            if (DrawOriginOffsetY < -20)
            {
                idlePause = 10;
                DrawOriginOffsetY = -20;
                floatUpOrDown = true;

            }
            if (idlePause < 0)
            {
                if (DrawOriginOffsetY < -12 && DrawOriginOffsetY > -18)
                {
                    Projectile.ai[0] += 2;
                }
                else
                {
                    Projectile.ai[0]++;
                }
            }

            idlePause--;

        }

    }
}
