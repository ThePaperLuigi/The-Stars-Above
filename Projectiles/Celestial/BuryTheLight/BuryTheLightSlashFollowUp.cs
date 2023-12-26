using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Celestial.BuryTheLight
{
    //
    public class BuryTheLightSlashFollowUp : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gloves of the Black Silence");
            Main.projFrames[Projectile.type] = 9;
            //DrawOriginOffsetY = 30;
            //DrawOffsetX = -60;
        }
        public override void SetDefaults()
        {
            Projectile.width = 300;
            Projectile.height = 300;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 30;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.DamageType = GetInstance<Systems.CelestialDamageClass>();

            Projectile.hide = false;
            Projectile.ownerHitCheck = false;
            Projectile.tileCollide = false;
            Projectile.friendly = true;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 3;
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
            Lighting.AddLight(Projectile.Center, new Vector3(1f, 0.4f, 0.64f));
            Player projOwner = Main.player[Projectile.owner];
            //Projectile.velocity = Vector2.Zero;
            Projectile.netUpdate = true;

            if (movementFactor == 0f) // When initially thrown out, the ai0 will be 0f
            {
                movementFactor = 20f; // Make sure the spear moves forward when initially thrown out
                Projectile.netUpdate = true; // Make sure to netUpdate this spear
            }
            Projectile.position += Projectile.velocity * movementFactor;

            if (Projectile.frame == 9)
            {

            }

            if (++Projectile.frameCounter >= 2)
            {
                Projectile.frameCounter = 0;
                if (Projectile.frame < 9)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        // Random upward vector.
                        Vector2 vel = new Vector2(Projectile.Center.X + Main.rand.Next(-24, 24), Projectile.Center.Y + Main.rand.Next(-24, 24));
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), vel, Vector2.Zero, ProjectileType<BuryTheLightSlash>(), 0, Projectile.knockBack, Projectile.owner, 0, 1);
                    }
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_YunlaiSwing0, Main.LocalPlayer.Center);

                    Projectile.frame++;
                }
                else
                {


                    Projectile.Kill();
                }

            }
            if (projOwner.itemAnimation == 0)
            {

            }


        }
    }
}