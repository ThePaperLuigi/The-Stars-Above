using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Melee.Hullwrought
{
    public class HullwroughtSpin : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hullwrought");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
                                                                        //DrawOffsetX = 40;
                                                                        //DrawOriginOffsetY = 81;
        }

        public override void SetDefaults()
        {
            Projectile.width = 162;               //The width of projectile hitbox
            Projectile.height = 162;              //The height of projectile hitbox
            Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 60;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }
        int direction;//0 is right 1 is left
        float rotationStrength = 0.1f;
        bool firstSpawn = true;
        double deg;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Player p = Main.player[Projectile.owner];
            if (firstSpawn)
            {
                if (p.direction == 1)
                {
                    direction = 0;
                    Projectile.rotation += MathHelper.ToRadians(45f);

                }
                if (p.direction == -1)
                {
                    direction = 1;
                    Projectile.rotation += MathHelper.ToRadians(225f);
                }
                firstSpawn = false;
            }
            //projectile.timeLeft = 10;

            //if (player.dead && !player.active || player.GetModPlayer<StarsAbovePlayer>().kroniicHeld < 0)
            if (player.dead && !player.active)
            {
                Projectile.Kill();
            }
            //projectile.spriteDirection = projectile.velocity.X > 0 ? 1 : -1;
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            if (Projectile.timeLeft > 30)
            {
                rotationStrength += 0.3f;
            }
            else
            {
                rotationStrength -= 0.3f;
                Projectile.alpha += 6;
            }

            //Factors for calculations
            if (direction == 0)
            {
                deg = Projectile.ai[1] += 6f + rotationStrength; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value

            }
            if (direction == 1)
            {
                deg = Projectile.ai[1] -= 6f + rotationStrength; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value

            }

            double rad = deg * (Math.PI / 180); //Convert degrees to radians
            double dist = 108; //Distance away from the player

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            Projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value




            //float rotationsPerSecond = rotationSpeed;
            //rotationSpeed -= 1f;
            //bool rotateClockwise = true;
            //The rotation is set here
            Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(225f);
            //projectile.rotation = projectile.velocity.ToRotation();

            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height - 51, Projectile.width, 20,
                    Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
                dust.velocity += Projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
            if (Main.rand.NextBool(4))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height - 51, Projectile.width, 20,
                    0, 0, 254, Scale: 0.3f);
                dust.velocity += Projectile.velocity * 0.5f;
                dust.velocity *= 0.5f;
            }
        }



    }
}
