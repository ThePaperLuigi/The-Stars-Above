
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Summon.Chronoclock
{
    public class FragmentOfTimeMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fragment of Time");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
            Main.projFrames[Projectile.type] = 11;
        }

        public override void SetDefaults()
        {

            AIType = 0;
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.timeLeft = 240;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hide = false;
            Projectile.alpha = 0;
            Projectile.netImportant = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;

        }
        int idlePause;
        int idleAnimation;
        bool floatUpOrDown; //false is Up, true is Down
        public override bool PreDraw(ref Color lightColor)
        {
            default(Effects.WhiteTrail).Draw(Projectile);


            return true;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            //player.suspiciouslookingTentacle = false;
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Player projOwner = Main.player[Projectile.owner];

            //When active, immunity to slow
            projOwner.buffImmune[BuffID.Slow] = true;

            Projectile.scale = 1f;
            if (!player.HasBuff(BuffType<Buffs.Summon.Chronoclock.ChronoclockMinionBuff>()))
            {
                Projectile.Kill();
            }
            if (player.dead && !player.active)
            {
                Projectile.Kill();
            }
            Projectile.timeLeft = 60;
            Projectile.alpha -= 10;

            GeneralBehavior(player, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
            SearchForTargets(player, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);
            Visuals();

            if (foundTarget)//if Time Pulse is active, don't do this
            {

                if (projOwner.ownedProjectileCounts[ProjectileType<TimePulse>()] >= 1)
                {

                }
                else
                {
                    Projectile.ai[1]++;
                }


            }


            if (Projectile.ai[1] > 300)//Fire the Time Pulse
            {

                Projectile.ai[1] = 0;
                int type = ProjectileType<TimePulse>();
                SoundEngine.PlaySound(SoundID.Item6, Projectile.Center);


                Vector2 position = Projectile.Center;
                float rotation = (float)Math.Atan2(position.Y - Main.MouseWorld.Y, position.X - Main.MouseWorld.X);//Aim towards mouse

                float launchSpeed = 0f;
                Vector2 direction = Vector2.Normalize(targetCenter - Projectile.Center);
                Vector2 velocity = direction * launchSpeed;

                int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, Projectile.damage, 0f, projOwner.whoAmI);

                Main.projectile[index].originalDamage = Projectile.damage;
            }







            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            if (Projectile.alpha > 255)
            {
                Projectile.alpha = 255;
            }

            Animation(projOwner);

        }

        private void Animation(Player projOwner)
        {
            idleAnimation++;
            if (projOwner.ownedProjectileCounts[ProjectileType<TimePulse>()] >= 1)
            {
                Projectile.frame = 2;
                for (int i = 0; i < 1; i++)
                {
                    // Charging dust
                    Vector2 vector2 = new Vector2(
                        Main.rand.Next(-2048, 2048) * (0.003f * 100) - 10,
                        Main.rand.Next(-2048, 2048) * (0.003f * 100) - 10);
                    Dust d2 = Main.dust[Dust.NewDust(
                        Projectile.Center + vector2, 1, 1,
                        132, 0, 0, 255,
                        new Color(1f, 1f, 1f), 0.5f)];

                    d2.velocity = -vector2 / 16;
                    d2.velocity -= Projectile.velocity / 8;
                    d2.noLight = true;
                    d2.noGravity = true;
                }
                Vector2 vector = new Vector2(
                      Main.rand.Next(-2048, 2048) * (0.003f * 100) - 10,
                      Main.rand.Next(-2048, 2048) * (0.003f * 100) - 10);
                Dust d = Main.dust[Dust.NewDust(
                    Projectile.Center + vector, 1, 1,
                    DustID.TreasureSparkle, 0, 0, 255,
                    new Color(1f, 1f, 1f), 0.5f)];

                d.velocity = -vector / 16;
                d.velocity -= Projectile.velocity / 8;
                d.noLight = true;
                d.noGravity = true;
            }
            else
            {
                if (Math.Abs(Projectile.velocity.X) >= 1 || Math.Abs(Projectile.velocity.Y) >= 1)//If moving
                {
                    if (Projectile.frame < 3)
                    {
                        Projectile.frame = 3;
                    }
                    // This is a simple "loop through all frames from top to bottom" animation
                    int frameSpeed = 5;

                    Projectile.frameCounter++;

                    if (Projectile.frameCounter >= frameSpeed)
                    {
                        Projectile.frameCounter = 0;
                        Projectile.frame++;

                        if (Projectile.frame >= 7)
                        {
                            Projectile.frame = 3;
                        }
                    }
                }
                else
                {//Idle animation (blinking, etc.)
                    if (idleAnimation >= 65 && idleAnimation < 70 || idleAnimation >= 215 && idleAnimation < 220 || idleAnimation >= 225 && idleAnimation < 230)
                    {
                        Projectile.frame = 1;

                    }
                    else
                    {
                        if (Projectile.frame < 7)
                        {
                            Projectile.frame = 7;
                        }
                        // This is a simple "loop through all frames from top to bottom" animation
                        int frameSpeed = 8;

                        Projectile.frameCounter++;

                        if (Projectile.frameCounter >= frameSpeed)
                        {
                            Projectile.frameCounter = 0;
                            Projectile.frame++;

                            if (Projectile.frame >= Main.projFrames[Projectile.type])
                            {
                                Projectile.frame = 7;
                            }
                        }
                    }
                }
            }
            if (idleAnimation > 250)
            {
                idleAnimation = 0;
            }
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
            distanceFromTarget = 700f;
            targetCenter = Projectile.position;
            foundTarget = false;

            // This code is required if your minion weapon has the targeting feature
            if (owner.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[owner.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, Projectile.Center);

                // Reasonable distance away so it doesn't target across multiple screens
                if (between < 2000f)
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
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                        // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                        // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                        bool closeThroughWall = between < 100f;

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
            Player projOwner = Main.player[Projectile.owner];
            // Default movement parameters (here for attacking)
            float speed = 8f;
            float inertia = 20f;
            if (projOwner.ownedProjectileCounts[ProjectileType<TimePulse>()] >= 1)
            {
                HoverAnimation();
                Projectile.velocity = Vector2.Zero;
            }
            else
            {
                DrawOriginOffsetY = 0;
                // Minion doesn't have a target: return to player and idle
                if (distanceToIdlePosition > 600f)
                {
                    // Speed up the minion if it's away from the player
                    speed = 12f;
                    inertia = 60f;
                }
                else
                {
                    // Slow down the minion if closer to the player
                    speed = 4f;
                    inertia = 80f;
                }

                if (distanceToIdlePosition > 20f)
                {
                    // The immediate range around the player (when it passively floats about)

                    // This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
                    vectorToIdlePosition.Normalize();
                    vectorToIdlePosition *= speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
                }
                else if (Projectile.velocity == Vector2.Zero)
                {
                    // If there is a case where it's not moving at all, give it a little "poke"
                    Projectile.velocity.X = -0.15f;
                    Projectile.velocity.Y = -0.05f;
                }
            }

        }

        private void Visuals()
        {
            // So it will lean slightly towards the direction it's moving
            Projectile.rotation = Projectile.velocity.X * 0.05f;



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
        private void HoverAnimation()
        {
            if (floatUpOrDown)//Up
            {
                if (Projectile.ai[0] > 7)
                {
                    DrawOriginOffsetY++;
                    Projectile.ai[0] = 0;
                }
            }
            else
            {
                if (Projectile.ai[0] > 7)
                {
                    DrawOriginOffsetY--;
                    Projectile.ai[0] = 0;
                }
            }
            if (DrawOriginOffsetY > -10)
            {
                idlePause = 10;
                DrawOriginOffsetY = -10;
                floatUpOrDown = false;

            }
            if (DrawOriginOffsetY < -20)
            {
                idlePause = 10;
                DrawOriginOffsetY = -20;
                floatUpOrDown = true;

            }
            if (idlePause < 0)
            {
                if (DrawOriginOffsetY < -12 && DrawOriginOffsetY > -18)
                {
                    Projectile.ai[0] += 2;
                }
                else
                {
                    Projectile.ai[0]++;
                }
            }

            idlePause--;

        }
        public override void OnKill(int timeLeft)
        {


        }

    }
}
