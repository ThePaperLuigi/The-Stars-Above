using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Summon.CandiedSugarball
{
    public class Sugartle : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			
			//DrawOffsetX = -20;
			// DisplayName.SetDefault("Luka"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 11;
			
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
            Projectile.minion = true;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.CloneDefaults(ProjectileID.MiniMinotaur);
			AIType = ProjectileID.MiniMinotaur;
			DrawOriginOffsetY = -8;
			//AnimationType = ProjectileID.BabyDino;
			Projectile.light = 1f;
			//DrawOffsetX = -20;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.miniMinotaur = false; // Relic from AIType
			return true;
		}
		bool invisible;
		public override void AI()
		{
            DrawOffsetX = -100;
            Player player = Main.player[Projectile.owner];
            WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
            Vector2 position = new Vector2(Projectile.Center.X - 90, Projectile.Center.Y);

            if (modPlayer.sugarballMinionType == 1)
            {

                for (int i2 = 0; i2 < 5; i2++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 30f);
                    offset.Y += (float)(Math.Cos(angle) * 30f);

                    Dust d2 = Dust.NewDustPerfect(position + offset, DustID.Flare, Projectile.velocity, 200, default, 0.7f);
                    d2.fadeIn = 0.0001f;
                    d2.noGravity = true;
                }
            }

            if (player.dead)
            {
                modPlayer.sugarballMinions = false;
            }
            if (modPlayer.sugarballMinions)
            {
                Projectile.timeLeft = 2;
            }

            SearchForTargets(player, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            if(foundTarget && distanceFromTarget < 300)
            {
            }
            if (Projectile.localAI[2] > 0)
            {
                Projectile.frame = 10;
                if (foundTarget)
                {
                    if (targetCenter.X < Projectile.Center.X - 100)
                    {
                        Projectile.direction = -1;
                    }
                    else
                    {
                        Projectile.direction = 1;
                    }
                }
            }


            if (foundTarget)
            {

            }
            Projectile.ai[2]++;
            Projectile.localAI[2]--;

            if (Projectile.ai[2] > 60)
            {

                if (foundTarget)
                {
                    for (int i3 = 0; i3 < 20; i3++)
                    {
                        Dust.NewDust(position, 0, 0, DustID.Water, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default, 0.9f);

                    }
                    Projectile.localAI[2] = 20;
                    Projectile.ai[2] = 0;
                    int type = ModContent.ProjectileType<SugartleBubble>();


                    float rotation = (float)Math.Atan2(position.Y - Main.MouseWorld.Y, position.X - Main.MouseWorld.X);//Aim towards mouse

                    float launchSpeed = 8f;
                    Vector2 mousePosition = player.GetModPlayer<StarsAbovePlayer>().playerMousePos;
                    Vector2 direction = Vector2.Normalize(targetCenter - Projectile.Center);
                    Vector2 velocity = direction * launchSpeed;

                    int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, Projectile.damage, 0f, player.whoAmI);

                    Main.projectile[index].originalDamage = Projectile.damage;

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