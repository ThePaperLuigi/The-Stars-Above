using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Tsukiyomi
{
    public class TsukiBloodshedAttack : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Only Thing I Know For Real");
            //DrawOriginOffsetY = 12;
            Main.projFrames[Projectile.type] = 8;

        }

        public override void SetDefaults()
        {
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.aiStyle = 0;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.timeLeft = 255;
            Projectile.hide = false;
            Projectile.ownerHitCheck = true;
            //Projectile.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>();
            Projectile.tileCollide = false;
            Projectile.friendly = false;
            Projectile.hostile = true;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
        }
        bool firstSpawn = true;
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


            if (firstSpawn)
            {
                //Projectile.rotation = MathHelper.ToRadians(Main.rand.NextFloat(0, 360));
                firstSpawn = false;
            }
            float rotationsPerSecond = 6f;
            bool rotateClockwise = true;
            // Since we access the owner player instance so much, it's useful to create a helper local variable for this
            // Sadly, Projectile/ModProjectile does not have its own

            // Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player


            if (++Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                if (Projectile.frame < 8)
                {
                    if (Projectile.frame < 6)
                    {
                        SoundEngine.PlaySound(SoundID.Item1, Projectile.position);

                    }
                    if (Projectile.frame == 1)
                    {

                    }
                    //Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/electroSmack"));
                    Projectile.frame++;
                }
                else
                {
                    Projectile.Kill();

                }

            }
        }
    }
}
