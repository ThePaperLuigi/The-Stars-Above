
using Microsoft.Xna.Framework;
 
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Other.GoldenKatana
{
    public class GoldenKatanaHeld : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Aurum Edge");
            Main.projFrames[Projectile.type] = 16;
        }

        public override void SetDefaults()
        {

            AIType = 0;

            Projectile.width = 70;
            Projectile.height = 170;
            Projectile.minion = false;
            Projectile.minionSlots = 0f;
            Projectile.timeLeft = 240;
            Projectile.penetrate = -1;
            Projectile.light = 0.3f;
            Projectile.hide = false;
            Projectile.alpha = 0;
            Projectile.netImportant = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;

            DrawOriginOffsetY = -60;
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
            Player player = Main.player[Projectile.owner];
            return true;
        }
        public override void AI()
        {


            Player projOwner = Main.player[Projectile.owner];
            Projectile.scale = 0.7f;

            if (firstSpawn)
            {
                //When the blade appears, it's rotated a bit.
                Projectile.ai[1] = 240;
                firstSpawn = false;
            }
            projOwner.heldProj = Projectile.whoAmI;//The projectile draws in front of the player.

            if (Projectile.spriteDirection == 1)
            {//Adjust when facing the other direction

                Projectile.ai[1] = (int)MathHelper.Lerp(280, 180, spawnProgress);//When the blade is first being drawn, rotate it so it's in front of the player. (Based on spawnProgress)
            }
            else
            {

                Projectile.ai[1] = (int)MathHelper.Lerp(260, 359, spawnProgress);//When the blade is first being drawn, rotate it so it's in front of the player. (Based on spawnProgress)
            }


            if (projOwner.GetModPlayer<WeaponPlayer>().bowCharge > 5)
            {
                projOwner.itemTime = 10;
                projOwner.itemAnimation = 10;
                spawnProgress += 0.1f;

            }
            else
            {
                spawnProgress -= 0.1f;
            }


            if (projOwner.GetModPlayer<WeaponPlayer>().overCharge2 >= 100)//2nd stage Overcharge fully charged
            {
                int frameSpeed = 6;
                Projectile.frameCounter++;


                if (Projectile.frameCounter >= frameSpeed && Projectile.frame <= 16)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                    if (Projectile.frame >= 16 || Projectile.frame < 12)
                    {
                        Projectile.frame = 12;
                    }
                }

                if (startSound)
                {
                    //play ignition sound.
                    //SoundEngine.PlaySound(StarsAboveAudio.SFX_CatalystIgnition, projOwner.Center);
                    endSound = true;
                    startSound = false;
                }
            }
            else
            {
                if (projOwner.GetModPlayer<WeaponPlayer>().overCharge1 >= 100)//1st stage Overcharge fully charged
                {
                    int frameSpeed = 6;
                    Projectile.frameCounter++;


                    if (Projectile.frameCounter >= frameSpeed && Projectile.frame <= 11)
                    {
                        Projectile.frameCounter = 0;
                        Projectile.frame++;
                        if (Projectile.frame >= 11 || Projectile.frame < 7)
                        {
                            Projectile.frame = 7;
                        }
                    }

                    if (startSound)
                    {
                        //play ignition sound.
                        //SoundEngine.PlaySound(StarsAboveAudio.SFX_CatalystIgnition, projOwner.Center);
                        endSound = true;
                        startSound = false;
                    }

                }
                else
                {
                    if (projOwner.GetModPlayer<WeaponPlayer>().bowCharge >= 100)//Normal fully charged
                    {
                        int frameSpeed = 6;
                        Projectile.frameCounter++;


                        if (Projectile.frameCounter >= frameSpeed && Projectile.frame <= 6)
                        {
                            Projectile.frameCounter = 0;
                            Projectile.frame++;
                            if (Projectile.frame >= 6)
                            {
                                Projectile.frame = 1;
                            }
                        }

                        if (startSound)
                        {
                            //play ignition sound.
                            //SoundEngine.PlaySound(StarsAboveAudio.SFX_CatalystIgnition, projOwner.Center);
                            endSound = true;
                            startSound = false;
                        }

                    }
                    else //No charge
                    {

                        Projectile.frame = 0;

                    }
                }
            }



            deg = Projectile.ai[1];
            double rad = deg * (Math.PI / 180);
            double dist = 10;

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */
            Projectile.position.X = projOwner.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = projOwner.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;


            if (Projectile.spriteDirection == 1)
            {//Adjust when facing the other direction

                Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(180f);
            }
            else
            {

                Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(0f);
            }
            //Projectile.Center += projOwner.gfxOffY * Vector2.UnitY;//Prevent glitchy animation.

            Projectile.ai[0]++;

            if (projOwner.dead && !projOwner.active)
            {//Disappear when player dies
                Projectile.Kill();
            }



            /*if(projOwner.ownedProjectileCounts[ProjectileType<CatalystStab>()] >= 1 || projOwner.ownedProjectileCounts[ProjectileType<CatalystThrow>()] >= 1)//|| projOwner.ownedProjectileCounts[ProjectileType<BurningDesireSlash1>()] >= 1 || projOwner.ownedProjectileCounts[ProjectileType<BurningDesireSlash2>()] >= 1)
			{//If an attack is active
				Projectile.alpha = 255;
			}
			else
			{*/
            //Arms will hold the weapon.
            projOwner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (projOwner.Center -
                new Vector2(Projectile.Center.X + projOwner.velocity.X * 0.05f, Projectile.Center.Y + projOwner.velocity.Y * 0.05f)
                ).ToRotation() + MathHelper.PiOver2);
            Projectile.alpha -= 90;



            if (projOwner.GetModPlayer<WeaponPlayer>().bowCharge <= 0)
            {
                Projectile.Kill();
            }

            Projectile.timeLeft = 10;//The prjoectile doesn't time out.

            //Orient projectile
            Projectile.direction = projOwner.direction;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation += projOwner.velocity.X * 0.05f; //Rotate in the direction of the user when moving




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
