using Microsoft.Xna.Framework;
using StarsAbove.Effects;
using StarsAbove.Systems;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Celestial.BuryTheLight
{
    public class MirageBlade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sunset of the Sun God");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 70;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
                                                                        //DrawOffsetX = 40;
                                                                        //DrawOriginOffsetY = 81;
        }

        public override void SetDefaults()
        {
            Projectile.width = 80;               //The width of projectile hitbox
            Projectile.height = 80;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = GetInstance<Systems.CelestialDamageClass>();
            Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 120;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 1f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
        bool firedProjectile = false;
        float rotationStrength = 12f;
        bool firstSpawn = true;
        double deg;
        bool spawnDust = false;
        public override void AI()
        {
            //DrawOffsetX = -110;
            //DrawOriginOffsetY = -60;
            Player player = Main.player[Projectile.owner];

            if (firstSpawn)
            {
                Projectile.scale = 1.5f;

                firstSpawn = false;
            }
            if (player.dead && !player.active)
            {
                Projectile.Kill();
            }
            Projectile.ai[0]++;

            if (Projectile.ai[0] > 60)
            {
                Projectile.friendly = true;
                //Shoot towards the mouse cursor
                if (!firedProjectile)
                {
                    Projectile.velocity = (player.GetModPlayer<StarsAbovePlayer>().playerMousePos - Projectile.Center).SafeNormalize(Vector2.Zero) * 20f;

                    firedProjectile = true;
                }
                float dustAmount = 2f;

                for (int i = 0; i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(2f, 2f);
                    spinningpoint5 = spinningpoint5.RotatedBy(Projectile.rotation);
                    int dust = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y + 120), 0, 0, DustID.Electric);
                    Main.dust[dust].scale = 1f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = Projectile.Center + spinningpoint5;
                    Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 2f;
                }
            }
            else
            {
                float dustAmount = 1f;

                for (int i = 0; i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(2f, 30f);
                    spinningpoint5 = spinningpoint5.RotatedBy(Projectile.rotation);
                    int dust = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y + 120), 0, 0, DustID.Electric);
                    Main.dust[dust].scale = 0.5f;
                    Main.dust[dust].fadeIn = 0.1f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = Projectile.Center + spinningpoint5;
                    Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * -6f;
                }

                Projectile.friendly = false;
                deg = Projectile.ai[1] += rotationStrength;
                rotationStrength *= 0.97f;
                double rad = deg * (Math.PI / 180);
                double dist = 200;

                /*Position the player based on where the player is, the Sin/Cos of the angle times the /
				/distance for the desired distance away from the player minus the projectile's width   /
				/and height divided by two so the center of the projectile is at the right place.     */
                Projectile.position.X = player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
                Projectile.position.Y = player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
                //Projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
                //Projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

                //Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(0f);
                //Projectile.rotation = MathHelper.ToRadians(90f);
                Projectile.rotation = Vector2.Normalize(player.GetModPlayer<StarsAbovePlayer>().playerMousePos - Projectile.Center).ToRotation() + MathHelper.ToRadians(90f);

            }

            if (!spawnDust)
            {
                float dustAmount = 13f;

                for (int i = 0; i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(2f, 30f);
                    spinningpoint5 = spinningpoint5.RotatedBy(Projectile.rotation);
                    int dust = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y + 120), 0, 0, DustID.Electric);
                    Main.dust[dust].scale = 1f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = Projectile.Center + spinningpoint5;
                    Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
                }
                /*for (int ir = 0; ir < 30; ir++)
				{
					Vector2 positionNew = Vector2.Lerp(player.Center, Projectile.Center, (float)ir / 30);

					Dust da = Dust.NewDustPerfect(positionNew, DustID.Electric, null, 240, default(Color), 1f);
					da.fadeIn = 0.3f;
					da.noLight = true;
					da.noGravity = true;

				}*/
                spawnDust = true;
            }

        }

        public override bool PreDraw(ref Color lightColor)
        {


            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            OnHitDust(target);

            Player projOwner = Main.player[Projectile.owner];
            projOwner.GetModPlayer<WeaponPlayer>().gaugeChangeAlpha = 1f;
            projOwner.GetModPlayer<WeaponPlayer>().judgementGauge++;
            projOwner.Heal(5);

        }

        private void OnHitDust(NPC target)
        {

            float dustAmount = 33f;
            float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(25f, 1f);
                spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant);
                int dust = Dust.NewDust(target.Center, 0, 0, DustID.Electric);
                Main.dust[dust].scale = 1f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = target.Center + spinningpoint5;
                Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 3f;
            }
            for (int i = 0; i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(25f, 1f);
                spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant + MathHelper.ToRadians(90));
                int dust = Dust.NewDust(target.Center, 0, 0, DustID.Electric);
                Main.dust[dust].scale = 1f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = target.Center + spinningpoint5;
                Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 3f;
            }
        }
    }
}
