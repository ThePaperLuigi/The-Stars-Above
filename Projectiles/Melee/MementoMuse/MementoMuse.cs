
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Melee.MementoMuse
{
    public class MementoMuseProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Memento Muse");

        }

        public override void SetDefaults()
        {
            Projectile.width = 108;
            Projectile.height = 108;
            //projectile.aiStyle = 2;//2
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.timeLeft = 40;
            Projectile.hide = false;
            Projectile.ownerHitCheck = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
        }

        float rotationSpeed = 4.7f;
        float throwSpeed = 10f;
        // In here the AI uses this example, to make the code more organized and readable
        // Also showcased in ExampleJavelinProjectile.cs
        public float movementFactor // Change this value to alter how fast the spear moves
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }


        public override void AI()
        {



            // Since we access the owner player instance so much, it's useful to create a helper local variable for this
            // Sadly, Projectile/ModProjectile does not have its own
            Player projOwner = Main.player[Projectile.owner];
            // Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);

            projOwner.heldProj = Projectile.whoAmI;
            projOwner.itemTime = projOwner.itemAnimation;
            Projectile.position.X = ownerMountedCenter.X - Projectile.width / 2;
            Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2;
            // As long as the player isn't frozen, the spear can move
            if (!projOwner.frozen)
            {
                if (movementFactor == 0f) // When initially thrown out, the ai0 will be 0f
                {
                    movementFactor = 1f; // Make sure the spear moves forward when initially thrown out
                    Projectile.netUpdate = true; // Make sure to netUpdate this spear	
                                                 //projectile.scale += projOwner.GetModPlayer<StarsAbovePlayer>().RhythmCombo / 4;
                }

                if (Projectile.timeLeft <= 20) // Somewhere along the item animation, make sure the spear moves back
                {
                    Projectile.alpha += 14;
                    Projectile.scale -= 0.05f;
                }
                movementFactor += throwSpeed;
                throwSpeed -= 0.2f;

                Projectile.scale += 0.05f;
                if (movementFactor < 0)
                {
                    movementFactor = 0;
                }
            }
            /*if (++projectile.frameCounter >= 2)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= Main.projFrames[projectile.type])
				{
					projectile.frame = 10;
				}
			}*/
            // Change the spear position based off of the velocity and the movementFactor
            Projectile.position += Projectile.velocity * movementFactor;
            // When we reach the end of the animation, we can kill the spear projectile
            if (Projectile.alpha >= 255 || movementFactor <= 0)
            {
                Projectile.Kill();
            }
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= MathHelper.ToRadians(90f);
            }


            // These dusts are added later, for the 'ExampleMod' effect
            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, 179,
                    Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
                dust.shader = GameShaders.Armor.GetSecondaryShader(122, Main.LocalPlayer);

                dust.velocity += Projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
            if (Main.rand.NextBool(4))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, 179,
                    0, 0, 254, Scale: 0.3f);
                dust.shader = GameShaders.Armor.GetSecondaryShader(122, Main.LocalPlayer);
                dust.velocity += Projectile.velocity * 0.5f;
                dust.velocity *= 0.5f;
            }
            float rotationsPerSecond = rotationSpeed;
            rotationSpeed -= 0.1f;
            bool rotateClockwise = true;
            //The rotation is set here
            Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= MathHelper.ToRadians(90f);
            }

        }
    }

}
