
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Ranged.KissOfDeath
{
    public class KissOfDeathMinigun : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Kiss of Death");
            DrawOriginOffsetY = 20;
        }

        public override void SetDefaults()
        {

            AIType = 0;

            Projectile.width = 162;
            Projectile.height = 72;
            Projectile.minion = false;
            Projectile.minionSlots = 0f;
            Projectile.timeLeft = 10;
            Projectile.penetrate = -1;
            Projectile.hide = false;
            Projectile.alpha = 0;
            Projectile.netImportant = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;


        }
        bool firstSpawn = true;
        int newOffsetY;
        float spawnProgress;
        bool dustSpawn = true;

        float rotationStrength = 0.1f;
        double deg;

        bool startSound = true;
        bool endSound = false;
        public override bool PreAI()
        {
            Player projOwner = Main.player[Projectile.owner];
            Projectile.ai[1] = MathHelper.ToDegrees((float)Math.Atan2(Main.MouseWorld.Y - projOwner.Center.Y, Main.MouseWorld.X - projOwner.Center.X)) - 180;
            DrawOriginOffsetY = 20;
            deg = Projectile.ai[1];
            double rad = deg * (Math.PI / 180);
            double dist = 43;

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            Projectile.position.X = projOwner.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2 + Main.rand.Next(0, 3);
            Projectile.position.Y = projOwner.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            Projectile.direction = projOwner.direction;
            Projectile.spriteDirection = Projectile.direction;
            if (Projectile.spriteDirection == 1)
            {//Adjust when facing the other direction

                Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(180f);
            }
            else
            {

                Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(0f);
            }

            return base.PreAI();
        }

        public override void AI()
        {
            Player projOwner = Main.player[Projectile.owner];
            Projectile.scale = 0.8f;

            if (firstSpawn)
            {
                //When the blade appears, it's rotated a bit.
                Projectile.ai[1] = 240;
                firstSpawn = false;
            }
            projOwner.heldProj = Projectile.whoAmI;//The projectile draws in front of the player.
            spawnProgress += 0.1f;

            //Projectile.Center += projOwner.gfxOffY * Vector2.UnitY;//Prevent glitchy animation.

            Projectile.ai[0]++;

            if (projOwner.dead && !projOwner.active)
            {//Disappear when player dies
                Projectile.Kill();
            }


            projOwner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (projOwner.Center -
                new Vector2(Projectile.Center.X + projOwner.velocity.X * 0.05f, Projectile.Center.Y + projOwner.velocity.Y * 0.05f)
                ).ToRotation() + MathHelper.PiOver2);
            Projectile.alpha -= 90;

            projOwner.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, (projOwner.Center -
                new Vector2(Projectile.Center.X + projOwner.velocity.X * 0.05f, Projectile.Center.Y + projOwner.velocity.Y * 0.05f)
                ).ToRotation() + MathHelper.PiOver2);
            Projectile.alpha -= 90;


            if (projOwner.itemTime <= 1)
            {
                Projectile.Kill();
            }

            Projectile.timeLeft = 10;//The prjoectile doesn't time out.





            //Orient projectile





            //Cap alpha
            /*if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			if (Projectile.alpha > 255)
			{
				Projectile.alpha = 255;
			}*/
            if (spawnProgress > 1f)
            {
                spawnProgress = 1f;
            }
            if (spawnProgress < 0f)
            {
                spawnProgress = 0f;
            }//Capping variables (there is 100% a better way to do this!)
        }
        private void Visuals()
        {





        }
        public override void OnKill(int timeLeft)
        {


        }
    }
}
