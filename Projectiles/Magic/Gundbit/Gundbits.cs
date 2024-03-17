
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using System;
using Terraria.ModLoader;
using Terraria.Audio;
using StarsAbove.Buffs.Magic.Gundbits;

namespace StarsAbove.Projectiles.Magic.Gundbit
{

    public class Gundbits : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 11;
            //ProjectileID.Sets.Homing[Projectile.type] = true;

            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 40;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 44;
            Projectile.height = 44;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            //ProjectileType<TakodachiRound>();
        }
        public ref float GundbitID => ref Projectile.ai[2];//As there are 11 gundbits, some act differently to keep things unique

        float damageBonus = 1f;
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            default(Effects.SmallBlueTrail).Draw(Projectile);
            return true;
        }
        // This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
        public override bool MinionContactDamage()
        {
            return false;
        }

        // The AI of this minion is split into multiple methods to avoid bloat. This method just passes values between calls actual parts of the AI.
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 40;
            if (!CheckActive(owner))
            {
                return;
            }

            Projectile.frame = (int)GundbitID;
            GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
            SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);
            Visuals();
            Projectile.alpha -= 3;
            if (Projectile.scale < 0)
            {
                Projectile.alpha = 255;
            }
            Projectile.scale += 0.02f;
            Projectile.scale = Math.Clamp(Projectile.scale, -1, 1);
            if (Projectile.ai[0] > 90)
            {

                if (foundTarget && distanceFromTarget < 300)
                {
                    Projectile.ai[0] = Main.rand.Next(-20, 20);
                    int type = ProjectileType<GundbitMinionLaser>();


                    for (int d = 0; d < 5; d++)
                    {
                        Dust.NewDust(Projectile.Center, 0, 0, DustID.Electric, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default, 0.5f);
                    }

                    Vector2 position = Projectile.Center;

                    float launchSpeed = 15f;
                    Vector2 direction = Vector2.Normalize(targetCenter - Projectile.Center);
                    Vector2 velocity = direction * launchSpeed;

                    int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, (int)(Projectile.damage * damageBonus), 0f, owner.whoAmI);
                    //Projectile.alpha = 255;
                    //Projectile.scale = 0;
                    //Projectile.ai[1] = Main.rand.Next(0, 360);

                    Main.projectile[index].originalDamage = Projectile.damage;
                }
            }
            if (Projectile.scale >= 1f)
            {
                Projectile.ai[0]++;
            }
            else
            {
                Projectile.ai[0] = 0;

            }
            if (owner.HasBuff(BuffType<GundbitBeamAttack>()) || owner.HasBuff(BuffType<GundbitShieldBuff>()))
            {
                if (Projectile.alpha <= 200)
                {
                    for (int d = 0; d < 5; d++)
                    {
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemSapphire, 0f + Main.rand.Next(-1, 1), 0f + Main.rand.Next(-1, 1), 0, default, 2f);
                        Main.dust[dustIndex].noGravity = true;
                    }
                }
                Projectile.alpha = 255;
                Projectile.ai[0] = Main.rand.Next(-20, 20);
                Projectile.scale = -0.5f;
                Projectile.ai[1] = Main.rand.Next(0, 360);
            }
            else
            {

            }
        }


        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(BuffType<AerialDefensiveArray>());

                return false;
            }

            if (owner.HasBuff(BuffType<AerialDefensiveArray>()))
            {
                Projectile.timeLeft = 2;
            }

            return true;
        }

        private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
        {
            Vector2 idlePosition = owner.Center;
            idlePosition.Y -= 48f; // Go up 48 coordinates (three tiles from the center of the player)

            // If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
            // The index is projectile.minionPos
            float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -owner.direction;
            idlePosition.X += minionPositionOffsetX; // Go behind the player

            // All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

            // Teleport to player if distance is too big
            vectorToIdlePosition = idlePosition - Projectile.Center;
            distanceToIdlePosition = vectorToIdlePosition.Length();

            if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f)
            {
                // Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
                // and then set netUpdate to true
                Projectile.position = idlePosition;
                Projectile.velocity *= 0.1f;
                Projectile.netUpdate = true;
            }

            // If your minion is flying, you want to do this independently of any conditions
            float overlapVelocity = 0.04f;

            // Fix overlap with other minions
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile other = Main.projectile[i];

                if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner && Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width)
                {
                    if (Projectile.position.X < other.position.X)
                    {
                        Projectile.velocity.X -= overlapVelocity;
                    }
                    else
                    {
                        Projectile.velocity.X += overlapVelocity;
                    }

                    if (Projectile.position.Y < other.position.Y)
                    {
                        Projectile.velocity.Y -= overlapVelocity;
                    }
                    else
                    {
                        Projectile.velocity.Y += overlapVelocity;
                    }
                }
            }
        }

        private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
        {
            // Starting search distance
            distanceFromTarget = 200f;
            targetCenter = Projectile.position;
            foundTarget = false;

            if (!foundTarget)
            {
                // This code is required either way, used for finding a target
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (npc.CanBeChasedBy() && npc.HasBuff(BuffType<GundbitMarked>()))
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;

                        if (npc.buffTime[npc.FindBuffIndex(BuffType<GundbitMarked>())] <= 1)
                        {
                            if (Projectile.scale >= 0.1f)
                            {
                                for (int d = 0; d < 5; d++)
                                {
                                    int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemSapphire, 0f + Main.rand.Next(-1, 1), 0f + Main.rand.Next(-1, 1), 0, default, 2f);
                                    Main.dust[dustIndex].noGravity = true;
                                }
                            }

                            Projectile.scale = -0.5f;
                        }

                        if (closest && inRange || !foundTarget)
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }

            // friendly needs to be set to true so the minion can deal contact damage
            // friendly needs to be set to false so it doesn't damage things like target dummies while idling
            // Both things depend on if it has a target or not, so it's just one assignment here
            // You don't need this assignment if your minion is shooting things instead of dealing contact damage
            //Projectile.friendly = foundTarget;
        }

        private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
        {
            // Default movement parameters (here for attacking)
            float speed = 200f;
            float inertia = 20f;

            if (foundTarget)
            {
                // Minion has a target: attack (here, fly towards the enemy)
                if (distanceFromTarget > 300f)
                {
                    for (int d = 0; d < 5; d++)
                    {
                        int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemSapphire, 0f + Main.rand.Next(-1, 1), 0f + Main.rand.Next(-1, 1), 0, default, 2f);
                        Main.dust[dustIndex].noGravity = true;
                    }
                    Projectile.scale = -0.5f;
                    Projectile.position = targetCenter;
                    Projectile.netUpdate = true;
                }
                else
                {
                    //Orbit the enemy
                    if (GundbitID <= 6)
                    {
                        //Factors for calculations
                        double deg = Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
                        double rad = deg * (Math.PI / 180); //Convert degrees to radians
                        double dist = 100 + GundbitID * 15; //Distance away from the target
                        Vector2 adjustedPosition = targetCenter;

                        /*Position the player based on where the player is, the Sin/Cos of the angle times the /
						/distance for the desired distance away from the player minus the projectile's width   /
						/and height divided by two so the center of the projectile is at the right place.     */
                        if (GundbitID <= 3)
                        {
                            Projectile.position.Y = adjustedPosition.Y - (int)(Math.Sin(rad) * dist / 2) - Projectile.height / 2;

                        }
                        else
                        {
                            Projectile.position.Y = adjustedPosition.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

                        }
                        Projectile.position.X = adjustedPosition.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;

                        //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
                        if (GundbitID <= 2)
                        {
                            Projectile.ai[1] -= 0.9f;

                        }
                        else
                        {
                            Projectile.ai[1] += 0.9f;

                        }

                        Projectile.rotation = Vector2.Normalize(targetCenter - Projectile.Center).ToRotation() + MathHelper.ToRadians(90f);
                    }
                    else
                    {
                        //Factors for calculations
                        double deg = Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
                        double rad = deg * (Math.PI / 180); //Convert degrees to radians
                        double dist = 100 + GundbitID * 10; //Distance away from the target
                        Vector2 adjustedPosition = targetCenter;

                        /*Position the player based on where the player is, the Sin/Cos of the angle times the /
						/distance for the desired distance away from the player minus the projectile's width   /
						/and height divided by two so the center of the projectile is at the right place.     */
                        if (GundbitID >= 9)
                        {
                            Projectile.position.X = adjustedPosition.X - (int)(Math.Cos(rad) * dist / 2) - Projectile.width / 2;

                        }
                        else
                        {
                            Projectile.position.X = adjustedPosition.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;

                        }
                        Projectile.position.Y = adjustedPosition.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

                        //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
                        if (GundbitID >= 10)
                        {
                            Projectile.ai[1] += 0.9f;

                        }
                        else
                        {
                            Projectile.ai[1] -= 0.9f;

                        }

                        Projectile.rotation = Vector2.Normalize(targetCenter - Projectile.Center).ToRotation() + MathHelper.ToRadians(90f);
                    }


                }
            }
            else
            {

                //Projectile.alpha = 170;
                //Factors for calculations
                double deg = Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
                double rad = deg * (Math.PI / 180); //Convert degrees to radians
                double dist = 100; //Distance away from the target
                Vector2 adjustedPosition = Main.player[Projectile.owner].Center;

                /*Position the player based on where the player is, the Sin/Cos of the angle times the /
				/distance for the desired distance away from the player minus the projectile's width   /
				/and height divided by two so the center of the projectile is at the right place.     */
                if (GundbitID >= 9)
                {
                    Projectile.position.X = adjustedPosition.X - (int)(Math.Cos(rad) * dist / 2) - Projectile.width / 2;

                }
                else
                {
                    Projectile.position.X = adjustedPosition.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;

                }
                if (GundbitID <= 3)
                {
                    Projectile.position.Y = adjustedPosition.Y - (int)(Math.Sin(rad) * dist / 2) - Projectile.height / 2;

                }
                else
                {
                    Projectile.position.Y = adjustedPosition.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

                }

                //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
                if (GundbitID % 2 == 0)
                {
                    Projectile.ai[1] += 0.9f;
                }
                else
                {
                    Projectile.ai[1] -= 0.9f;

                }

                Projectile.rotation = Vector2.Normalize(Main.MouseWorld - Projectile.Center).ToRotation() + MathHelper.ToRadians(90f);
            }
        }

        private void Visuals()
        {
            //Projectile.rotation = Projectile.velocity.X * 0.05f;

            //Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            // This is a simple "loop through all frames from top to bottom" animation
            int frameSpeed = 5;


            if (Projectile.velocity.X > 0f)
            {
                Projectile.spriteDirection = Projectile.direction = -1;
            }
            else if (Projectile.velocity.X < 0f)
            {
                Projectile.spriteDirection = Projectile.direction = 1;
            }

            // Some visuals here
            //Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);
        }
    }
}

