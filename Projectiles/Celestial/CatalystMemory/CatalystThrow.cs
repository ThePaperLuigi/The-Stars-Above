
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.CatalystMemory;
using StarsAbove.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
namespace StarsAbove.Projectiles.Celestial.CatalystMemory
{
    public class CatalystThrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Catalyst's Memory");

        }

        public override void SetDefaults()
        {
            Projectile.width = 180;
            Projectile.height = 180;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.timeLeft = 240;
            Projectile.alpha = 0;
            Projectile.DamageType = GetInstance<Systems.CelestialDamageClass>();
            Projectile.hide = false;
            Projectile.ownerHitCheck = true;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
            Projectile.extraUpdates = 1;

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
            Projectile.scale = 0.7f;
            Player projOwner = Main.player[Projectile.owner];
            // Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            Projectile.direction = projOwner.direction;
            projOwner.itemTime = 10;
            projOwner.itemAnimation = 10;
            //projOwner.heldProj = Projectile.whoAmI;
            //Projectile.position.X = projOwner.Center.X - (float)(Projectile.width / 2);
            //Projectile.position.Y = projOwner.Center.Y - (float)(Projectile.height / 2);

            // Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
            // MathHelper.ToRadians(xx degrees here)
            Projectile.rotation = Projectile.velocity.ToRotation();//+ MathHelper.ToRadians(135f)
                                                                   // Offset by 90 degrees here
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= MathHelper.ToRadians(180f);
            }
            Projectile.spriteDirection = Projectile.direction;
            // As long as the player isn't frozen, the spear can move
            if (!projOwner.frozen)
            {
                if (movementFactor == 0f) // When initially thrown out, the ai0 will be 0f
                {
                    movementFactor = 6.5f; // Make sure the spear moves forward when initially thrown out
                    Projectile.netUpdate = true; // Make sure to netUpdate this spear
                }
                movementFactor += 1.1f;
            }
            // Change the spear position based off of the velocity and the movementFactor
            Projectile.position += Projectile.velocity * movementFactor;
            // When we reach the end of the animation, we can kill the spear projectile

            float distance = Vector2.Distance(projOwner.GetModPlayer<WeaponPlayer>().CatalystPrismicPosition, Projectile.Center);

            if (distance < 30f)
            {
                projOwner.ClearBuff(BuffType<Bedazzled>());
                projOwner.AddBuff(BuffType<DazzlingBladedance>(), 300);
                projOwner.statLife += 70;
                Rectangle textPos = new Rectangle((int)projOwner.position.X, (int)projOwner.position.Y - 20, projOwner.width, projOwner.height);
                CombatText.NewText(textPos, new Color(49, 234, 63, 240), $"70", false, false);
                for (int d = 0; d < 33; d++)//Visual effects
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(25));
                    float scale = 2f - Main.rand.NextFloat() * 7f;
                    perturbedSpeed = perturbedSpeed * scale;
                    int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Pink, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.6f);
                    Main.dust[dustIndex].noGravity = true;

                }
                for (int d = 0; d < 27; d++)//Visual effects
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(65));
                    float scale = 2f - Main.rand.NextFloat() * 4.6f;
                    perturbedSpeed = -(perturbedSpeed * scale);
                    int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmethyst, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.6f);
                    Main.dust[dustIndex].noGravity = true;

                }
                for (int d = 0; d < 27; d++)//Visual effects
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(15));
                    float scale = 1f - Main.rand.NextFloat() * 6.6f;
                    perturbedSpeed = -(perturbedSpeed * scale);
                    int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.PurpleCrystalShard, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 0.6f);
                    Main.dust[dustIndex].noGravity = true;

                }
                for (int d = 0; d < 63; d++)//Visual effects
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(25));
                    float scale = 28f - Main.rand.NextFloat() * 107f;
                    perturbedSpeed = -perturbedSpeed * scale;
                    int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Pink, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1.6f);
                    Main.dust[dustIndex].noGravity = true;

                }
                Projectile.Kill();
            }

            // These dusts are added later, for the 'ExampleMod' effect

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int d = 0; d < 8; d++)
            {
                Dust.NewDust(target.Center, 0, 0, DustID.GemAmethyst, Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5), 150, default, 0.4f);
                Dust.NewDust(target.Center, 0, 0, DustID.PurpleCrystalShard, Main.rand.NextFloat(-8, 8), Main.rand.NextFloat(-8, 8), 150, default, 0.8f);
            }


        }
    }


}
