
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using System;
using Terraria.ModLoader;
using StarsAbove.Buffs;
using StarsAbove.Systems;

namespace StarsAbove.Projectiles.ElCapitansHardware
{

    public class GyroDisk : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 3;
			Main.projPet[Projectile.type] = true;
			//ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			//ProjectileID.Sets.Homing[Projectile.type] = true;
			//ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true; //This is necessary for right-click targeting
		}

		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 24;
			Projectile.height = 32;
			Projectile.friendly = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.minion = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 240;
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
			return true;
		}

		// The AI of this minion is split into multiple methods to avoid bloat. This method just passes values between calls actual parts of the AI.
		public override void AI()
		{
			Player owner = Main.player[Projectile.owner];

			if (!CheckActive(owner))
			{
				return;
			}

			//GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
			SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
			//Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);
			Visuals();
			Projectile.velocity *= 0.98f;
			if (Projectile.ai[0] > 12)
			{

				if (foundTarget)
				{
					Projectile.ai[0] = 0;
					int type = ProjectileType<DiskShot>();


					Vector2 position = Projectile.Center;
					float rotation = (float)Math.Atan2(position.Y - (Main.MouseWorld.Y), position.X - (Main.MouseWorld.X));//Aim towards mouse

					float launchSpeed = 66f;
					Vector2 mousePosition = owner.GetModPlayer<StarsAbovePlayer>().playerMousePos;
					Vector2 direction = Vector2.Normalize(targetCenter - Projectile.Center);
					Vector2 velocity = direction * launchSpeed;

					int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, Projectile.damage, 0f, owner.whoAmI);

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
				

				return false;
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
						bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
						// Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
						// The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
						bool closeThroughWall = between < 100f;

						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
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
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.AmberBolt, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default(Color), 0.5f);
				Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Yellow, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default(Color), 0.4f);

			}
			
		}
		private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
		{
			// Default movement parameters (here for attacking)
			float speed = 8f;
			float inertia = 20f;

			if (foundTarget)
			{
				// Minion has a target: attack (here, fly towards the enemy)
				if (distanceFromTarget > 40f)
				{
					// The immediate range around the target (so it doesn't latch onto it when close)
					Vector2 direction = targetCenter - Projectile.Center;
					direction.Normalize();
					direction *= speed;

					Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
				}
			}
			else
			{
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
					
				}
			}
		}

		private void Visuals()
		{
			// So it will lean slightly towards the direction it's moving
			Projectile.rotation = Projectile.velocity.X * 0.05f;

			// This is a simple "loop through all frames from top to bottom" animation
			int frameSpeed = 5;

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

