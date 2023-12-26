
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using System;
using Terraria.ModLoader;
using StarsAbove.Buffs.SparkblossomBeacon;
using Terraria.Audio;

namespace StarsAbove.Projectiles.Summon.SparkblossomBeacon
{

    public class FleetingSparkMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            //ProjectileID.Sets.Homing[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; //This is necessary for right-click targeting

            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 40;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 38;
            Projectile.height = 40;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            //ProjectileType<TakodachiRound>();


        }
        float damageBonus = 1f;
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.alpha < 160)
            {
                default(Effects.SmallBlueTrail).Draw(Projectile);
                return true;
            }


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

            GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
            SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);
            Visuals();
            Projectile.alpha -= 3;
            Projectile.scale += 0.02f;
            Projectile.scale = Math.Clamp(Projectile.scale, 0, 1);
            if (Projectile.ai[0] > 90)
            {

                if (foundTarget && distanceFromTarget < 110)
                {
                    Projectile.ai[0] = 0;
                    int type = ProjectileType<FleetingSparkBullet>();


                    Vector2 position = Projectile.Center;

                    float launchSpeed = 55f;
                    Vector2 direction = Vector2.Normalize(targetCenter - Projectile.Center);
                    Vector2 velocity = direction * launchSpeed;

                    int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, (int)(Projectile.damage * damageBonus), 0f, owner.whoAmI);
                    Projectile.alpha = 255;
                    Projectile.scale = 0;
                    Projectile.ai[1] = Main.rand.Next(0, 360);
                    damageBonus += 0.02f;
                    if (damageBonus > 1.1f)
                    {
                        damageBonus = 1.1f;
                    }
                    if (Main.rand.Next(0, 100) < 10)
                    {
                        //Explode
                        Projectile.Center = targetCenter;
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, 0, Mod.Find<ModProjectile>("SparkExplosion").Type, Projectile.damage * 2, 0f, owner.whoAmI, 0);
                        Projectile.alpha = 255;

                        if (Main.rand.Next(0, 100) < 50)
                        {
                            int k = Item.NewItem(Projectile.GetSource_FromThis(), (int)Projectile.position.X, (int)Projectile.position.Y, 0, 0, ItemID.Star, 1, false);
                            if (Main.netMode == NetmodeID.MultiplayerClient)
                            {
                                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, k, 1f);
                            }
                        }

                        SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
                        //Return to summoner
                        Projectile.position = owner.Center;

                        damageBonus = 1;

                    }

                    Main.projectile[index].originalDamage = Projectile.damage;

                }
            }
            Projectile.ai[0]++;

        }


        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(BuffType<FleetingSparkBuff>());

                return false;
            }

            if (owner.HasBuff(BuffType<FleetingSparkBuff>()))
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

            // This code is required if your minion weapon has the targeting feature
            if (owner.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[owner.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, Projectile.Center);

                // Reasonable distance away so it doesn't target across multiple screens
                if (between < 600f)
                {
                    distanceFromTarget = between;
                    targetCenter = npc.Center;
                    foundTarget = true;
                }
            }

            if (!foundTarget)
            {
                // This code is required either way, used for finding a target
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;


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
            float speed = 12f;
            float inertia = 20f;

            if (Projectile.alpha > 200)
            {
                Projectile.velocity = Vector2.Zero;
            }

            if (foundTarget)
            {
                // Minion has a target: attack (here, fly towards the enemy)
                if (distanceFromTarget > 140f)
                {
                    // The immediate range around the target (so it doesn't latch onto it when close)
                    Vector2 direction = targetCenter - Projectile.Center;
                    direction.Normalize();
                    direction *= speed;

                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
                }
                else
                {
                    //Orbit the enemy

                    //Factors for calculations
                    double deg = Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
                    double rad = deg * (Math.PI / 180); //Convert degrees to radians
                    double dist = 100; //Distance away from the target
                    Vector2 adjustedPosition = targetCenter;

                    /*Position the player based on where the player is, the Sin/Cos of the angle times the /
					/distance for the desired distance away from the player minus the projectile's width   /
					/and height divided by two so the center of the projectile is at the right place.     */
                    Projectile.position.X = adjustedPosition.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
                    Projectile.position.Y = adjustedPosition.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

                    //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
                    //Projectile.ai[1] += 0.9f;

                    Projectile.rotation = Vector2.Normalize(targetCenter - Projectile.Center).ToRotation() + MathHelper.ToRadians(90f);

                }
            }
            else
            {
                Projectile.alpha = 170;
                //Factors for calculations
                double deg = Projectile.ai[1]; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
                double rad = deg * (Math.PI / 180); //Convert degrees to radians
                double dist = 100; //Distance away from the target
                Vector2 adjustedPosition = Main.player[Projectile.owner].Center;

                /*Position the player based on where the player is, the Sin/Cos of the angle times the /
				/distance for the desired distance away from the player minus the projectile's width   /
				/and height divided by two so the center of the projectile is at the right place.     */
                Projectile.position.X = adjustedPosition.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
                Projectile.position.Y = adjustedPosition.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

                //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
                Projectile.ai[1] += 0.9f;

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
            Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);
        }
    }
}

