using Microsoft.Xna.Framework;
using StarsAbove.Projectiles.Melee.SoulReaver;
using StarsAbove.Projectiles.Summon.CandiedSugarball;
using StarsAbove.Systems;
using StarsAbove.Systems;
using System;
using System.Runtime.Intrinsics;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Other.DreadmotherDarkIdol
{
    public class DreadmotherMeleeSummon : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 3;
			
			
		}

		public override void SetDefaults()
		{
            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 280;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = true;
			Projectile.tileCollide = true;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false; // Don't fall through platforms
            return true; // Collide with tiles
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			return true;
		}
		bool invisible;
		public override void AI()
		{
			DrawOriginOffsetY = 14;
			Projectile.velocity.Y += 0.26f;
			Projectile.velocity.X *= 1.00f;
			Player player = Main.player[Projectile.owner];
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			
			
			Vector2 position = Vector2.Zero;
			
            if (player.dead)
			{
				modPlayer.dreadmotherMinion = false;
			}
			if (modPlayer.dreadmotherMinion)
			{
				Projectile.timeLeft = 2;
			}
            SearchForTargets(player, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            
            if (Projectile.localAI[2] > 0)
            {
                Projectile.frame = 2;

                if (foundTarget)
                {
                    if (targetCenter.X < Projectile.Center.X)
                    {
                        Projectile.direction = -1;
                    }
                    else
                    {
                        Projectile.direction = 1;
                    }
                }
            }
            else
            {
                if (Projectile.ai[2] > 40)
                {
                    Dust.NewDust(position, 0, 0, DustID.Shadowflame, 0f + Main.rand.Next(-6, -3), 0f + Main.rand.Next(-5, 5), 150, default, 0.9f);
                    Dust.NewDust(position, 0, 0, DustID.Shadowflame, 0f + Main.rand.Next(3, 6), 0f + Main.rand.Next(-5, 5), 150, default, 0.9f);
                    Projectile.frame = 1;

                }
                else
                {
                    Projectile.frame = 0;

                }

            }
            
            if (foundTarget)
            {
                if (targetCenter.X > Projectile.Center.X)
                {
                    Projectile.spriteDirection = 1;

                }
                else
                {
                    Projectile.spriteDirection = -1;


                }
                Projectile.ai[2]++;

            }
            Projectile.localAI[2]--;

            if (Projectile.ai[2] > 45)
            {

                if(distanceFromTarget > 100)
                {
                    if (Projectile.owner == Main.myPlayer)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<SoulReaverPortal>(), 0, Projectile.knockBack, Projectile.owner, 0, 1);

                    }



                    //Appear either left or right of the enemy.
                    if (Main.rand.NextBool())
                    {
                        Projectile.Center = new Vector2(targetCenter.X + 60, targetCenter.Y - 50);
                    }
                    else
                    {
                        Projectile.Center = new Vector2(targetCenter.X - 60, targetCenter.Y - 50);

                    }

                    if (Projectile.owner == Main.myPlayer)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<SoulReaverPortal>(), 0, Projectile.knockBack, Projectile.owner, 0, 1);

                    }
                    return;

                }
                if (foundTarget)
                {
                    for (int i3 = 0; i3 < 20; i3++)
                    {
                        Dust.NewDust(position, 0, 0, DustID.Shadowflame, 0f + Main.rand.Next(-6, -3), 0f + Main.rand.Next(-5, 5), 150, default, 0.9f);

                    }

                    for (int i3 = 0; i3 < 20; i3++)
                    {
                        Dust.NewDust(position, 0, 0, DustID.Shadowflame, 0f + Main.rand.Next(3, 6), 0f + Main.rand.Next(-5, 5), 150, default, 0.9f);

                    }
                    Projectile.localAI[2] = 10;
                    Projectile.ai[2] = 0;
                    int type = ModContent.ProjectileType<DreadmotherMinionClaw>();



                    float rotation = (float)Math.Atan2(position.Y - Main.MouseWorld.Y, position.X - Main.MouseWorld.X);//Aim towards mouse

                    float launchSpeed = 8f;
                    Vector2 mousePosition = player.GetModPlayer<StarsAbovePlayer>().playerMousePos;
                    Vector2 direction = Vector2.Normalize(targetCenter - Projectile.Center);
                    Vector2 velocity = direction * launchSpeed;

                    if(Projectile.owner == Main.myPlayer)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity, type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1);

                    }


                }
            }
        }
        private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
        {
            // Starting search distance
            distanceFromTarget = 300f;
            targetCenter = Projectile.position;
            foundTarget = false;

            // This code is required if your minion weapon has the targeting feature
            if (owner.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[owner.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, Projectile.Center);

                // Reasonable distance away so it doesn't target across multiple screens
                if (between < 800f)
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
        public override void OnKill(int timeLeft)
        {
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Excalibur,
                new ParticleOrchestraSettings { PositionInWorld = Projectile.Center },
                Projectile.owner);
        }
       
    }
	
	
}