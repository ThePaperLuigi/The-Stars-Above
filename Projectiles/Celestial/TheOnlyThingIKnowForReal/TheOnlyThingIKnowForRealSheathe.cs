
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Celestial.TheOnlyThingIKnowForReal
{
    public class TheOnlyThingIKnowForRealSheathe : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Only Thing I Know For Real");
            Main.projFrames[Projectile.type] = 13;

            //DrawOriginOffsetY = -40;
        }

        public override void SetDefaults()
        {
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.aiStyle = 0;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 255;
            Projectile.timeLeft = 255;
            Projectile.hide = false;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
        }

        // In here the AI uses this example, to make the code more organized and readable
        // Also showcased in ExampleJavelinProjectile.cs
        public float movementFactor // Change this value to alter how fast the spear moves
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        // It appears that for this AI, only the ai0 field is used!
        public override void AI()
        {
            // Since we access the owner player instance so much, it's useful to create a helper local variable for this
            // Sadly, Projectile/ModProjectile does not have its own
            Player projOwner = Main.player[Projectile.owner];
            // Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            Projectile.direction = projOwner.direction;
            projOwner.heldProj = Projectile.whoAmI;
            Projectile.timeLeft = 10;
            Projectile.position.X = ownerMountedCenter.X - Projectile.width / 2;
            Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2 - 40;
            // As long as the player isn't frozen, the spear can move
            if (!projOwner.frozen)
            {
                if (movementFactor == 0f) // When initially thrown out, the ai0 will be 0f
                {
                    movementFactor = 2f; // Make sure the spear moves forward when initially thrown out
                    Projectile.netUpdate = true; // Make sure to netUpdate this spear
                }
                if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3) // Somewhere along the item animation, make sure the spear moves back
                {
                    //movementFactor -= 2.4f;
                }
                else // Otherwise, increase the movement factor
                {
                    //movementFactor += 2.4f;
                }
            }
            Projectile.alpha -= 20;
            Projectile.position += Projectile.velocity * movementFactor;

            Projectile.rotation = Projectile.velocity.ToRotation();

            if (Projectile.spriteDirection == -1)
            {
                //Projectile.rotation -= MathHelper.ToRadians(180f);
            }
            Projectile.spriteDirection = Projectile.direction;



            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (Projectile.frame < 12)
                {

                    //Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/electroSmack"));
                    Projectile.frame++;
                }
                else
                {


                }

            }

            if (!projOwner.channel)
            {
                Projectile.Kill();
            }
        }
    }
}
