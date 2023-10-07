
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Ozma;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Magic.Ozma
{
    public class OzmaAttack5Slash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ozma Ascendant");
            //DrawOriginOffsetY = 12;
            Main.projFrames[Projectile.type] = 8;

        }

        public override void SetDefaults()
        {
            Projectile.width = 300;
            Projectile.height = 300;
            Projectile.aiStyle = 0;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.timeLeft = 255;
            Projectile.hide = false;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.friendly = true;

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
            Player projOwner = Main.player[Projectile.owner];
            // Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            Projectile.direction = projOwner.direction;
            projOwner.heldProj = Projectile.whoAmI;


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
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ProjectileType<OzmaAttack5Slash2>(), 0, 3, projOwner.whoAmI, 0f);

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

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int d = 0; d < 8; d++)
            {
                Dust.NewDust(target.Center, 0, 0, 219, Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-7, 7), 150, default, 0.9f);

            }
            Player projOwner = Main.player[Projectile.owner];
            if (hit.Crit)
            {
                projOwner.AddBuff(BuffType<AnnihilationState>(), 180);
            }

        }
    }
}
