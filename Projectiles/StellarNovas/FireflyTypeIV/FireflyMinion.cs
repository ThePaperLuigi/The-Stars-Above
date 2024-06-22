
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using System;
using Terraria.ModLoader;
using StarsAbove.Systems;
using StarsAbove.Buffs.Summon.Takonomicon;
using StarsAbove.Buffs.StellarNovas;
using StarsAbove.Projectiles.Summon.Takodachi;
using Terraria.GameContent;
using StarsAbove.Utilities;
using System.Collections.Generic;
using StarsAbove.Projectiles.Bosses.Tsukiyomi;
using Terraria.GameContent.Tile_Entities;
using Terraria.Audio;
using rail;

namespace StarsAbove.Projectiles.StellarNovas.FireflyTypeIV
{

    public class FireflyMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 70;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
            Main.projFrames[Projectile.type] = 7;
            //Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            //ProjectileID.Sets.Homing[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; //This is necessary for right-click targeting
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.friendly = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            //ProjectileType<TakodachiRound>();


        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        // This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
        public override bool MinionContactDamage()
        {
            return false;
        }
        double dist;
        bool firstSpawn = true;
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (!CheckActive(owner))
            {
                return;
            }
            Visuals(owner);
            
            
            
            if (firstSpawn)
            {
                if (Projectile.ai[0] < 180)
                {
                    
                }
                else
                {
                    Projectile.localAI[0] += 0.01f;
                    double deg = 90; //The degrees, you can multiply projectile.ai[1] to make it orbit faster, may be choppy depending on the value
                    double rad = deg * (Math.PI / 180); //Convert degrees to radians
                    Projectile.localAI[0] = MathHelper.Clamp(Projectile.localAI[0], 0f, 1f);
                    dist = MathHelper.Lerp(600, 140, EaseHelper.OutQuad(Projectile.localAI[0]));

                    Projectile.position.X = owner.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
                    Projectile.position.Y = owner.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
                }

                if (Projectile.localAI[0] == 1f)
                {
                    firstSpawn = false;
                }
            }

            GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
            SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);
            
            if (Projectile.ai[0] > 70 && !firstSpawn)
            {
                if (foundTarget)
                {
                    
                    Projectile.alpha = 255;

                    if(Main.rand.NextBool(3))
                    {
                        int type = ProjectileType<FireflyKick>();
                        Vector2 position = new Vector2(targetCenter.X, targetCenter.Y);



                        if (targetCenter.X > Projectile.Center.X)
                        {
                            position = new Vector2(targetCenter.X - 500, targetCenter.Y);
                        }
                        else if (targetCenter.X < Projectile.Center.X)
                        {
                            position = new Vector2(targetCenter.X + 500, targetCenter.Y);
                        }


                        float rotation = (float)Math.Atan2(position.Y - Main.MouseWorld.Y, position.X - Main.MouseWorld.X);//Aim towards mouse

                        float launchSpeed = 26f;
                        Vector2 mousePosition = owner.GetModPlayer<StarsAbovePlayer>().playerMousePos;
                        Vector2 direction = Vector2.Normalize(targetCenter - position);
                        Vector2 velocity = direction * launchSpeed;
                        Vector2 adjustedVelocity = velocity * 0.04f;

                        if (Main.myPlayer == owner.whoAmI)
                        {
                            int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y - 40, type, Projectile.damage, 0f, owner.whoAmI);

                        }
                        Projectile.ai[0] = 0;
                        Projectile.ai[2] = 60;

                    }
                    else
                    {
                        int type = ProjectileType<FireflySlash>();
                        Vector2 position = new Vector2(targetCenter.X, targetCenter.Y);



                        if (targetCenter.X > Projectile.Center.X)
                        {
                            position = new Vector2(targetCenter.X - 600, targetCenter.Y + Main.rand.Next(-100, 100));
                        }
                        else if (targetCenter.X < Projectile.Center.X)
                        {
                            position = new Vector2(targetCenter.X + 600, targetCenter.Y + Main.rand.Next(-100, 100));
                        }


                        float rotation = (float)Math.Atan2(position.Y - Main.MouseWorld.Y, position.X - Main.MouseWorld.X);//Aim towards mouse

                        float launchSpeed = 66f;
                        Vector2 mousePosition = owner.GetModPlayer<StarsAbovePlayer>().playerMousePos;
                        Vector2 direction = Vector2.Normalize(targetCenter - position);
                        Vector2 velocity = direction * launchSpeed;
                        Vector2 adjustedVelocity = velocity * 0.04f;

                        if (Main.myPlayer == owner.whoAmI)
                        {
                            int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, Projectile.damage, 0f, owner.whoAmI);

                        }
                        Projectile.ai[2] = 40;

                        Projectile.ai[0] = 30;
                    }
                    
                   
                }
            }
            if(foundTarget)
            {
                if (targetCenter.X > Projectile.Center.X)
                {
                    Projectile.spriteDirection = Projectile.direction = 1;
                }
                else if (targetCenter.X < Projectile.Center.X)
                {
                    Projectile.spriteDirection = Projectile.direction = -1;
                }
            }
            else
            {
                Projectile.spriteDirection = owner.direction;

            }
            if (owner.HasBuff(BuffType<FireflyActive>()))
            {
                if (owner.buffTime[owner.FindBuffIndex(BuffType<FireflyActive>())] > 180)
                {
                    Projectile.ai[0]++;
                }
                else
                {
                    Projectile.ai[2]--;
                    if (Projectile.ai[2] == 0)
                    {
                        int type = ProjectileType<FireflySkill>();
                        Vector2 position = new Vector2(targetCenter.X, targetCenter.Y);

                        SoundEngine.PlaySound(StarsAboveAudio.SFX_CounterImpact, Projectile.Center);

                        if (targetCenter.X > Projectile.Center.X)
                        {
                            position = new Vector2(targetCenter.X - 800, targetCenter.Y);
                        }
                        else if (targetCenter.X < Projectile.Center.X)
                        {
                            position = new Vector2(targetCenter.X + 800, targetCenter.Y);
                        }


                        float rotation = (float)Math.Atan2(position.Y - Main.MouseWorld.Y, position.X - Main.MouseWorld.X);//Aim towards mouse

                        float launchSpeed = 66f;
                        Vector2 mousePosition = owner.GetModPlayer<StarsAbovePlayer>().playerMousePos;
                        Vector2 direction = Vector2.Normalize(targetCenter - position);
                        Vector2 velocity = direction * launchSpeed;
                        Vector2 adjustedVelocity = velocity * 0.04f;

                        if (Main.myPlayer == owner.whoAmI)
                        {
                            int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, Projectile.damage, 0f, owner.whoAmI);

                        }
                    }
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            Player owner = Main.player[Projectile.owner];

            for (int i = 0; i < 70; i++)
            {
                int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 220, Main.rand.NextFloat(-25, 25), Main.rand.NextFloat(-25, 25), 100, default, 1f);
                Main.dust[dustIndex].noGravity = true;

                dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 220, Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(-20, 20), 100, default, 2f);
                Main.dust[dustIndex].velocity *= 3f;
            }

            if (owner.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1 && Main.myPlayer == owner.whoAmI)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), new Vector2(Projectile.Center.X, Projectile.Center.Y), Vector2.Zero, ModContent.ProjectileType<AsphodeneFFEnd>(), 0, 0f, Main.myPlayer);

            }
            else if (owner.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2 && Main.myPlayer == owner.whoAmI)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), new Vector2(Projectile.Center.X, Projectile.Center.Y), Vector2.Zero, ModContent.ProjectileType<EridaniFFEnd>(), 0, 0f, Main.myPlayer);

            }

            base.OnKill(timeLeft);
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(BuffType<FireflyActive>());

                return false;
            }

            if (owner.HasBuff(BuffType<FireflyActive>()))
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
                        bool lineOfSight = true;
                        // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                        // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                        bool closeThroughWall = between < 100f;

                        if ((closest && inRange || !foundTarget) && (lineOfSight || closeThroughWall))
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
            float speed = 1f;
            float inertia = 1f;

            
            if (foundTarget)
            {
                Projectile.velocity = Projectile.velocity *= 0.9f;
                /*
                // Minion has a target: attack (here, fly towards the enemy)
                if (distanceFromTarget > 400f)
                {
                    // The immediate range around the target (so it doesn't latch onto it when close)
                    Vector2 direction = targetCenter - Projectile.Center;
                    direction.Normalize();
                    direction *= speed;

                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
                }*/
            }
            else
            {
                Projectile.Center = Vector2.Lerp(Projectile.Center, new Vector2(Main.player[Projectile.owner].Center.X, Main.player[Projectile.owner].Center.Y - 120), 0.1f);
                Projectile.velocity = Vector2.Zero;
            }
        }
        bool activeAttack = false;
        private void Visuals(Player owner)
        {
            Projectile.alpha = Math.Clamp(Projectile.alpha, 0, 255);
            if (owner.ownedProjectileCounts[ProjectileType<FireflySlash>()] > 0 || owner.ownedProjectileCounts[ProjectileType<FireflyKick>()] > 0 || owner.ownedProjectileCounts[ProjectileType<FireflySkill>()] > 0)
            {
                activeAttack = true;
                Projectile.alpha = 255;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile projTarget = Main.projectile[i];

                    if (projTarget.active && (projTarget.type == ProjectileType<FireflySlash>() || projTarget.type == ProjectileType<FireflyKick>() || projTarget.type == ProjectileType<FireflySkill>()) && projTarget.owner == owner.whoAmI)
                    {
                        Projectile.Center = new Vector2(projTarget.Center.X, projTarget.Center.Y);
                        Projectile.velocity = projTarget.velocity;
                    }
                }
            }
            else
            {
                if(activeAttack)
                {
                    activeAttack = false;
                }
                Lighting.AddLight(Projectile.Center, TorchID.Green);

                Projectile.alpha = 0;
            }
            // So it will lean slightly towards the direction it's moving
            Projectile.rotation = Projectile.velocity.X * 0.02f;

            // This is a simple "loop through all frames from top to bottom" animation
            int frameSpeed = 8;

            Projectile.frameCounter++;

            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }

            

            // Some visuals here
        }
        public override bool PreDraw(ref Color lightColor)
        {
            //default(Effects.FireflyVFX).Draw(Projectile);
            // This is where we specify which way to flip the sprite. If the projectile is moving to the left, then flip it vertically.
            SpriteEffects spriteEffects = Projectile.spriteDirection <= 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            // Getting texture of projectile
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Rectangle sourceRectangle;
            Vector2 origin;
            sourceRectangle = texture.Frame(1, Main.projFrames[Type], frameY: Projectile.frame);
            origin = sourceRectangle.Size() / 2f;
            DrawGlowEffects(texture, out sourceRectangle, out origin);

            return base.PreDraw(ref lightColor);
        }
        public float quadraticFloatTimer;
        public float quadraticFloat;
        private void DrawGlowEffects(Texture2D texture, out Rectangle sourceRectangle, out Vector2 origin)
        {
            quadraticFloatTimer += 0.003f;
            quadraticFloat = EaseHelper.Pulse(quadraticFloatTimer);

            // Get the currently selected frame on the texture.
            sourceRectangle = texture.Frame(1, Main.projFrames[Type], frameY: Projectile.frame);
            origin = sourceRectangle.Size() / 2f;
            var drawPlayer = Main.player[Projectile.owner];
            Microsoft.Xna.Framework.Color color1 = Lighting.GetColor((int)((double)drawPlayer.position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawPlayer.position.Y + (double)drawPlayer.height * 0.5) / 16.0));
            int r1 = (int)color1.R;
            //drawOrigin.Y += 34f;
            //drawOrigin.Y += 8f;
            //--drawOrigin.X;
            Vector2 position1 = Projectile.Center - Main.screenPosition;
            Texture2D texture2D2 = (Texture2D)ModContent.Request<Texture2D>("StarsAbove/Effects/FireflyVFX");

            float num11 = (float)((double)Main.GlobalTimeWrappedHourly / 7.0);
            float timeFloatAlt = (float)((double)Main.GlobalTimeWrappedHourly / 5.0);

            //These control fade out (unused)
            float num12 = num11;
            if ((double)num12 > 0.5)
                num12 = 1f - num11;
            if ((double)num12 < 0.0)
                num12 = 0.0f;
            float num13 = (float)(((double)num11 + 0.5) % 1.0);
            float num14 = num13;
            if ((double)num14 > 0.5)
                num14 = 1f - num13;
            if ((double)num14 < 0.0)
                num14 = 0.0f;
            Microsoft.Xna.Framework.Rectangle r2 = texture2D2.Frame(1, 1, 0, 0);
            //drawOrigin = r2.Size() / 2f;
            Vector2 position3 = position1 + new Vector2(0f, 0f);

            /*if (drawPlayer.direction == 1)
            {
                position3 = position1 + new Vector2(-10f, -100f);
            }
            else
            {
                position3 = position1 + new Vector2(10f, -100f);
            }*/
            Microsoft.Xna.Framework.Color color3 = new Microsoft.Xna.Framework.Color(117, 235, 175, 220) * MathHelper.Lerp(1, 0, Projectile.alpha / 254); //This is the color of the pulse!
                                                                                                     //Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3, Projectile.rotation, drawOrigin, Projectile.scale * 0.5f, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
            float num15 = 1.3f; //+ num11 * 2.75f; //Scale?
            Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 , 0 + num11, origin, 1 * 0.8f * num15 + MathHelper.Lerp(-0.2f, 0.2f, quadraticFloat), SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
            float num16 = 1.3f; //+ num13 * 2.75f; //Scale?
            Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 , 0 - timeFloatAlt, origin, 1 * 0.8f * num16, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);


            color3 = new Color(255, 0, 0);
        }
    }
}

