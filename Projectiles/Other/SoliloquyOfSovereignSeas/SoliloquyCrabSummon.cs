using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Other.SoliloquyOfSovereignSeas;
using StarsAbove.Systems;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Other.SoliloquyOfSovereignSeas
{
    public class SoliloquyCrabSummon : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			//
			//DrawOffsetX = -20;
			// DisplayName.SetDefault("Mumei"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 14;
			Main.projPet[Projectile.type] = true;
		}
		int idleAnimation;
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.BrainOfCthulhuPet);
			AIType = ProjectileID.BrainOfCthulhuPet;
			Projectile.netImportant = true;
			Projectile.DamageType = ModContent.GetInstance<ArkheDamageClass>();
			Projectile.minion = true;
			
			Projectile.minionSlots = 0f;
			//AnimationType = ProjectileID.BabyDino;
			//Projectile.light = 1f;
			//
			//DrawOffsetX = -20;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			//player.petFlagBrainOfCthulhuPet = false; // Relic from AIType
			DrawOriginOffsetY = -16;
			return true;
		}
		public bool firstSpawn = true;
		public override void AI()
		{
			Lighting.AddLight(Projectile.Center, TorchID.Blue);

			Projectile.velocity.X *= 1.00f;
			Player owner = Main.player[Projectile.owner];
			if(firstSpawn)
            {
				Projectile.ai[2] = 80;//Delay the projectiles coming out!
				firstSpawn = false;
            }
			if (!CheckActive(owner))
			{
				return;
			}
			WeaponPlayer modPlayer = owner.GetModPlayer<WeaponPlayer>();
			SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            if (!modPlayer.ousiaAligned)
            {
                for (int d = 0; d < 20; d++)
                {
                    Dust.NewDust(Projectile.Center, 0, 0, DustID.GemSapphire, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1f);
                }
                Projectile.Kill();
            }
            if (Projectile.ai[2] > 120)
			{

				if (foundTarget)
				{
					Projectile.ai[2] = 0;
					int type = ProjectileType<SoliloquySummonProjectile>();


					Vector2 position = Projectile.Center;
					float rotation = (float)Math.Atan2(position.Y - Main.MouseWorld.Y, position.X - Main.MouseWorld.X);//Aim towards mouse

					float launchSpeed = 66f;
					Vector2 mousePosition = owner.GetModPlayer<StarsAbovePlayer>().playerMousePos;
					Vector2 direction = Vector2.Normalize(targetCenter - Projectile.Center);
					Vector2 velocity = direction * launchSpeed;

					int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, Projectile.damage, 0f, owner.whoAmI);

					Main.projectile[index].originalDamage = Projectile.damage;

				}
			}
			Projectile.ai[2]++;
			
			/*
			if (Projectile.velocity.Length() > 6f)
			{
				if (++Projectile.frameCounter >= 3)
				{
					Projectile.frameCounter = 0;
					if (Projectile.frame < 6)
					{
						Projectile.frame = 6;
					}
					else
					{
						Projectile.frame++;
						if (Projectile.frame > 15)
						{
							Projectile.frame = 10;
						}
					}
				}
			}
			else
			{
				Projectile.rotation = Projectile.velocity.X * 0.125f;
				if (++Projectile.frameCounter >= 5)
				{
					Projectile.frameCounter = 0;
					Projectile.frame++;
					if (Projectile.frame == 6 || Projectile.frame >= Main.projFrames[Projectile.type])
					{
						Projectile.frame = 0;
					}
				}
			}*/
		}
		private bool CheckActive(Player owner)
		{
			

			if (owner.HasBuff(BuffType<SoliloquyMinionBuff>()))
			{
				Projectile.timeLeft = 2;
			}

			return true;
		}
		private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
		{
			// Starting search distance
			distanceFromTarget = 700f;
			targetCenter = Projectile.position;
			foundTarget = false;

			// Projectile code is required if your minion weapon has the targeting feature
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
				// Projectile code is required either way, used for finding a target
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];

					if (npc.CanBeChasedBy())
					{
						float between = Vector2.Distance(npc.Center, Projectile.Center);
						bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
						// Additional check for Projectile specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
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
			// You don't need Projectile assignment if your minion is shooting things instead of dealing contact damage
			//Projectile.friendly = foundTarget;
		}
	}
	
}