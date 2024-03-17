using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Summon.CandiedSugarball
{
    public class Bulbasugar : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 2;
			
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.width = 36;
            Projectile.height = 32;
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

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			return true;
		}
		bool invisible;
		public override void AI()
		{
			DrawOriginOffsetY = 2;
			Projectile.velocity.Y += 0.26f;
			Projectile.velocity.X *= 1.00f;
			Player player = Main.player[Projectile.owner];
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			
			if(modPlayer.sugarballMinionType == 2)
			{
                for (int i2 = 0; i2 < 5; i2++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 30f);
                    offset.Y += (float)(Math.Cos(angle) * 30f);

                    Dust d2 = Dust.NewDustPerfect(Projectile.Center + offset, DustID.Flare, Projectile.velocity, 200, default, 0.7f);
                    d2.fadeIn = 0.0001f;
                    d2.noGravity = true;
                }
            }
			Vector2 position = Vector2.Zero;
			
            if (player.dead)
			{
				modPlayer.sugarballMinions = false;
			}
			if (modPlayer.sugarballMinions)
			{
				Projectile.timeLeft = 2;
			}
            SearchForTargets(player, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);

            if (Projectile.localAI[2] > 0)
            {
                Projectile.frame = 1;
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
                Projectile.frame = 0;

            }
            if (Projectile.direction == 1)
            {
                position = new Vector2(Projectile.Center.X, Projectile.Center.Y);
            }
            else
            {
                position = new Vector2(Projectile.Center.X, Projectile.Center.Y);

            }

            if (foundTarget)
            {

            }
            Projectile.ai[2]++;
            Projectile.localAI[2]--;

            if (Projectile.ai[2] > 40)
            {

                if (foundTarget)
                {
                    for (int i3 = 0; i3 < 20; i3++)
                    {
                        Dust.NewDust(position, 0, 0, DustID.Grass, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-5, -3), 150, default, 0.9f);

                    }
                    Projectile.localAI[2] = 20;
                    Projectile.ai[2] = 0;
                    int type = ModContent.ProjectileType<SugartleBubble>();
                    if (modPlayer.sugarballMinionType == 2)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            // Random upward vector.
                            Vector2 vel = new Vector2(Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-5, -3));
                            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel, ModContent.ProjectileType<BulbasugarSeeds>(), Projectile.damage / 5, Projectile.knockBack, Projectile.owner, 0, 1);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            // Random upward vector.
                            Vector2 vel = new Vector2(Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-5, -3));
                            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel, ModContent.ProjectileType<BulbasugarSeeds>(), Projectile.damage / 5, Projectile.knockBack, Projectile.owner, 0, 1);
                        }
                    }
                    

                    float rotation = (float)Math.Atan2(position.Y - Main.MouseWorld.Y, position.X - Main.MouseWorld.X);//Aim towards mouse

                    float launchSpeed = 8f;
                    Vector2 mousePosition = player.GetModPlayer<StarsAbovePlayer>().playerMousePos;
                    Vector2 direction = Vector2.Normalize(targetCenter - Projectile.Center);
                    Vector2 velocity = direction * launchSpeed;



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
                if (between < 300f)
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