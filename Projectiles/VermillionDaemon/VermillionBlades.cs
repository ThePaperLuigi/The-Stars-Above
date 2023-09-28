
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.VermillionDaemon
{
    public class VermillionBlades : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Vermillion Daemon");
			Main.projFrames[Projectile.type] = 12; 
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 60;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
		}

		public override void SetDefaults()
		{
			// This method right here is the backbone of what we're doing here; by using this method, we copy all of
			// the Meowmere Projectile's SetDefault stats (such as projectile.friendly and projectile.penetrate) on to our projectile,
			// so we don't have to go into the source and copy the stats ourselves. It saves a lot of time and looks much cleaner;
			// if you're going to copy the stats of a projectile, use CloneDefaults().

			Projectile.CloneDefaults(ProjectileID.EmpressBlade);

			// To further the Cloning process, we can also copy the ai of any given projectile using AIType, since we want
			// the projectile to essentially behave the same way as the vanilla projectile.
			AIType = ProjectileID.EmpressBlade;

			// Replacing the blade's Summon-type attributes
			Projectile.minion = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.minionSlots = 0f;
			Projectile.timeLeft = 120;
			Projectile.penetrate = -1;
			Projectile.hide = false;
			Projectile.alpha = 255;
			Projectile.width = 160;
			Projectile.height = 160;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 30;
		}
		bool onSpawn = true;
		bool finishedAttacking = false;

		public override void AI()
		{
			Player projOwner = Main.player[Projectile.owner];
			projOwner.empressBlade = false;//Holdout from CloneDefaults

			if(onSpawn)
            {
				Projectile.frame = Main.rand.Next(1, 13);
				Projectile.timeLeft += Main.rand.Next(0, 30);

				onSpawn = false;
            }

			if(Vector2.Distance(projOwner.Center, Projectile.Center) < 60)
            {
				Projectile.alpha = 255;
				Projectile.friendly = false;
            }
			else
            {


				Projectile.alpha -= 30;
				Projectile.friendly = true;
            }
			if(Projectile.alpha < 50)//A little transparent
            {
				Projectile.alpha = 50;
            }
			if (Projectile.alpha > 255)
			{
				Projectile.alpha = 255;
			}

			

			base.AI();
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

			
		}
		private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
		{
			// Default movement parameters (here for attacking)
			float speed = 8f;
			float inertia = 20f;

			if (foundTarget)
			{
				
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
					// If there is a case where it's not moving at all, give it a little "poke"
					Projectile.velocity.X = -0.15f;
					Projectile.velocity.Y = -0.05f;
				}
			}
		}
		public override void OnKill(int timeLeft)
		{
			if(Projectile.alpha < 100)
            {
				for (int i = 0; i < 12; i++)
				{
					int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 223, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 0.6f);
					//Main.dust[dustIndex].velocity *= 0.6f;
				}
			}
			

		}
		
	}
}
