
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Melee.Hullwrought
{
    public class HullwroughtSpinDamage : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hullwrought");
            //DrawOriginOffsetY = -120;
            //DrawOffsetX = -125;
        }

        public override void SetDefaults()
        {
            Projectile.width = 250;
            Projectile.height = 250;
            Projectile.aiStyle = 1;
            Projectile.timeLeft = 60;
            Projectile.scale = 1.3f;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false;


        }
        int slowfade;
        int direction;//0 is right 1 is left
        float rotationSpeed = 10f;
        bool firstSpawn = true;
        double deg;
        // In here the AI uses this example, to make the code more organized and readable
        // Also showcased in ExampleJavelinProjectile.cs
        public float movementFactor // Change this value to alter how fast the spear moves
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }


        public override void AI()
        {

            Player player = Main.player[Projectile.owner];
            Player p = Main.player[Projectile.owner];
            Projectile.position.X = player.Center.X - Projectile.width / 2;
            Projectile.position.Y = player.Center.Y - Projectile.height / 2;
            /*if (firstSpawn)
			{
				if (p.direction == 1)
				{
					direction = 0;
					//projectile.rotation += MathHelper.ToRadians(45f);

				}
				if (p.direction == -1)
				{
					direction = 1;
					//projectile.rotation += MathHelper.ToRadians(225f);
				}
				firstSpawn = false;
			}*/
            //projectile.timeLeft = 10;

            //if (player.dead && !player.active || player.GetModPlayer<StarsAbovePlayer>().kroniicHeld < 0)
            if (player.dead && !player.active)
            {
                Projectile.Kill();
            }
            //projectile.spriteDirection = projectile.velocity.X > 0 ? 1 : -1;
            if (Projectile.velocity.Y > 16f)
            {
                //projectile.velocity.Y = 16f;
            }
            float rotationsPerSecond = 0.7f;
            bool rotateClockwise = true;

            //Factors for calculations

            deg = Projectile.ai[1] += 7f; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value


            double rad = deg * (Math.PI / 180); //Convert degrees to radians
            double dist = 0; //Distance away from the player
            Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */


            //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value




            //float rotationsPerSecond = rotationSpeed;
            //rotationSpeed -= 1f;
            //bool rotateClockwise = true;
            //The rotation is set here
            //projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(225f);
            //projectile.rotation = projectile.velocity.ToRotation();




        }
    }
}
