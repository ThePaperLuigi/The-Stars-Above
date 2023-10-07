using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Magic.EyeOfEuthymia
{
    public class EyeOfEuthymia : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Eye of Euthymia");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
            Main.projFrames[Projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;               //The width of projectile hitbox
            Projectile.height = 68;              //The height of projectile hitbox
            Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true;         //Can the projectile deal damage to enemies?
            Projectile.hostile = false;         //Can the projectile deal damage to the player?
            Projectile.minion = true;
            Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 10;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f;            //How much light emit around the projectile
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;          //Can the projectile collide with tiles?
            Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
        }
        bool firstSpawn = true;
        public override void AI()
        {
            Projectile.ai[1] = 90;
            if (firstSpawn)
            {
                for (int d1 = 0; d1 < 15; d1++)
                {
                    Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, 100, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default, 1.5f);
                }

                // Smoke Dust spawn
                for (int i = 0; i < 10; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }

                // Large Smoke Gore spawn
                for (int g = 0; g < 2; g++)
                {
                    int goreIndex = Gore.NewGore(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), default, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), default, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), default, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), default, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                }

                firstSpawn = false;
            }
            Projectile.timeLeft = 10;
            Player player = Main.player[Projectile.owner];
            if (player.dead && !player.active || !player.GetModPlayer<WeaponPlayer>().euthymiaActive)
            {
                Projectile.Kill();
            }
            Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            Player p = Main.player[Projectile.owner];
            if (++Projectile.frameCounter >= 7)
            {
                Projectile.frameCounter = 0;
                if (Projectile.frame < 6)
                {
                    Projectile.frame++;
                }
                else
                {
                    Projectile.frame = 0;
                }

            }
            //Factors for calculations
            double deg = Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
            double rad = deg * (Math.PI / 180); //Convert degrees to radians
            double dist = 108; //Distance away from the player

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            Projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
            //projectile.ai[1] += 2f;




            Vector2 vector = new Vector2(
                    Main.rand.Next(-28, 28) * (0.003f * 40 - 10),
                    Main.rand.Next(-28, 28) * (0.003f * 40 - 10));
            Dust d = Main.dust[Dust.NewDust(
                Projectile.Center + vector, 1, 1,
                223, 0, 0, 255,
                Color.White, 0.4f)];
            d.velocity = -vector / 14;
            d.velocity -= player.velocity / 8;
            d.noLight = true;
            d.noGravity = true;
            /*if (Main.rand.NextBool(3))
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 20,
					projectile.velocity.X * .2f, projectile.velocity.Y * .2f, 200, Scale: 1.2f);
				dust.velocity += projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
			if (Main.rand.NextBool(4))
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 20,
					0, 0, 254, Scale: 0.3f);
				dust.velocity += projectile.velocity * 0.5f;
				dust.velocity *= 0.5f;
			}*/
        }



    }
}
