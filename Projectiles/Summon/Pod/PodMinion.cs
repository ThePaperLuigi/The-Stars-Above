
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.RedMage;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Summon.Pod
{
    public class PodMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pod Zero-42");
        }

        public override void SetDefaults()
        {

            AIType = 0;
            Projectile.width = 62;
            Projectile.height = 62;
            Projectile.minion = true;
            Projectile.minionSlots = 2f;
            Projectile.timeLeft = 240;
            Projectile.penetrate = 999;
            Projectile.friendly = false;
            Projectile.hide = false;
            Projectile.alpha = 255;
            Projectile.netImportant = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;

        }
        int idlePause;
        bool floatUpOrDown; //false is Up, true is Down
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

            Projectile.scale = 0.9f;
            if (!player.HasBuff(BuffType<Buffs.Pod.PodBuff>()))
            {
                Projectile.Kill();
            }
            if (player.dead && !player.active)
            {
                Projectile.Kill();
            }
            Projectile.timeLeft = 60;
            Projectile.alpha -= 10;

            SearchForTargets(projOwner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);


            if (player.whoAmI == Main.myPlayer && StarsAbove.weaponActionKey.Old)
            {
                if (player.statMana > 2)
                {
                    Projectile.ai[1]++;

                    if (Projectile.ai[1] > 6)
                    {
                        player.manaRegenDelay = 60;
                        player.statMana -= 2;
                        Projectile.ai[1] = 0;
                        int type = ProjectileType<PodShot>();
                        SoundEngine.PlaySound(SoundID.Item11, Projectile.Center);


                        Vector2 position = Projectile.Center;
                        float rotation = (float)Math.Atan2(position.Y - Main.MouseWorld.Y, position.X - Main.MouseWorld.X);//Aim towards mouse

                        float launchSpeed = 60f;
                        Vector2 mousePosition = projOwner.GetModPlayer<StarsAbovePlayer>().playerMousePos;
                        Vector2 direction = Vector2.Normalize(mousePosition - Projectile.Center);
                        Vector2 velocity = direction * launchSpeed;

                        int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, (int)(Projectile.damage * 1.1), 0f, projOwner.whoAmI);

                    }


                }
                else
                {
                    Projectile.ai[1] = 0;

                }
            }
            else
            {



                if (foundTarget)
                {
                    Projectile.ai[1]++;
                }

                if (Projectile.ai[1] > 24)
                {

                    Projectile.ai[1] = 0;
                    int type = ProjectileType<PodShot>();
                    SoundEngine.PlaySound(SoundID.Item11, Projectile.Center);


                    Vector2 position = Projectile.Center;
                    float rotation = (float)Math.Atan2(position.Y - Main.MouseWorld.Y, position.X - Main.MouseWorld.X);//Aim towards mouse

                    float launchSpeed = 40f;
                    Vector2 direction = Vector2.Normalize(targetCenter - Projectile.Center);
                    Vector2 velocity = direction * launchSpeed;

                    int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, Projectile.damage, 0f, projOwner.whoAmI);

                    Main.projectile[index].originalDamage = Projectile.damage;
                }
            }

            if (player.GetModPlayer<StarsAbovePlayer>().inCombat > 0)
            {
                player.AddBuff(BuffID.Hunter, 10);

            }
            else
            {
                player.AddBuff(BuffID.NightOwl, 10);

            }

            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
            Projectile.direction = projOwner.direction;


            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = player.velocity.X * 0.05f;

            //Projectile.position.X = player.Center.X;
            if (Projectile.spriteDirection == 1)
            {
                Projectile.position.X = player.Center.X;

            }
            else
            {
                Projectile.position.X = player.Center.X - 62;

            }
            Projectile.position.Y = ownerMountedCenter.Y - Projectile.height / 2 - 12;

            //This is 0 unless a auto attack has been initated, in which it'll tick up.


            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            if (Projectile.alpha > 255)
            {
                Projectile.alpha = 255;
            }



            UpdateMovement();




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

        private void UpdateMovement()
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
