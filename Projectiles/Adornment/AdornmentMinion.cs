
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.Adornment;
using StarsAbove.Buffs.RedMage;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Adornment
{
    public class AdornmentMinion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Adornment of the Chaotic God");
		}

		public override void SetDefaults()
		{
			
			AIType = 0;
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.minion = true;
			Projectile.minionSlots = 1f;
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
			if (!player.HasBuff(BuffType<Buffs.Adornment.AdornmentMinionBuff>()))
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


			if (foundTarget)
			{
				if(player.HasBuff(BuffType<AdornmentAttackSpeedBuff>()))
                {
					Projectile.ai[1]++;

				}
				Projectile.ai[1] += 1 + (player.GetModPlayer<WeaponPlayer>().activeMinions * 0.05f);//Increase attack speed by 5% per each minion summoned.
				float rotation = (float)Math.Atan2(Projectile.Center.Y - (targetCenter.Y), Projectile.Center.X - (targetCenter.X)) + MathHelper.ToRadians(-90);
				Projectile.rotation = rotation;
			}
			else
            {
				Projectile.rotation = player.velocity.X * 0.05f;
			}
			
			if (Projectile.ai[1] > 30)
			{

				Projectile.ai[1] = 0;
				int type = ProjectileType<ChaosMagic>();
				Vector2 position = Projectile.Center;
				float launchSpeed = 30f;
				Vector2 direction = Vector2.Normalize(targetCenter - Projectile.Center);
				Vector2 velocity = direction * launchSpeed;

				if(player.HasBuff(BuffType<AdornmentRandomObjectsBuff>()))
                {
					SoundEngine.PlaySound(SoundID.DD2_DarkMageAttack, Projectile.Center);

					type = ProjectileID.PewMaticHornShot;
					launchSpeed = Main.rand.NextFloat(5, 20);
					velocity = direction * launchSpeed;

					Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, Projectile.damage/2, 0f, projOwner.whoAmI, Main.rand.Next(Main.projFrames[ProjectileID.PewMaticHornShot]), Main.rand.Next(Main.projFrames[ProjectileID.PewMaticHornShot]));

					Projectile.ai[1] = 20;
					

				}
				else
                {
					if (Main.rand.NextBool(2))
					{

						
						if (Main.rand.NextBool(2))
						{
							SoundEngine.PlaySound(SoundID.DD2_DarkMageAttack, Projectile.Center);

							type = ProjectileID.PewMaticHornShot;
							launchSpeed = Main.rand.NextFloat(5, 20);
							velocity = direction * launchSpeed;

							Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, Projectile.damage, 0f, projOwner.whoAmI, Main.rand.Next(Main.projFrames[ProjectileID.PewMaticHornShot]), Main.rand.Next(Main.projFrames[ProjectileID.PewMaticHornShot]));

							Projectile.ai[1] = 25;

						}
						else
						{
							SoundEngine.PlaySound(SoundID.DD2_DarkMageAttack, Projectile.Center);

							type = ProjectileType<ChaosObject>();
							launchSpeed = Main.rand.NextFloat(7, 15);
							velocity = direction * launchSpeed;

							Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, Projectile.damage, 0f, projOwner.whoAmI, Main.rand.Next(Main.projFrames[ProjectileID.PewMaticHornShot]), Main.rand.Next(Main.projFrames[ProjectileID.PewMaticHornShot]));

							Projectile.ai[1] = 25;
						}


					}
					else
					{
						SoundEngine.PlaySound(SoundID.DD2_DarkMageAttack, Projectile.Center);

						Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, Projectile.damage, 0f, projOwner.whoAmI, Main.rand.Next(Main.projFrames[ProjectileID.PewMaticHornShot]), Main.rand.Next(Main.projFrames[ProjectileID.PewMaticHornShot]));

						float rotation = (float)Math.Atan2(Projectile.Center.Y - (targetCenter.Y), Projectile.Center.X - (targetCenter.X));

						for (int d = 0; d < 25; d++)
						{
							float Speed2 = Main.rand.NextFloat(10, 18);  //projectile speed
							Vector2 perturbedSpeed = new Vector2((float)((Math.Cos(rotation) * Speed2) * -1), (float)((Math.Sin(rotation) * Speed2) * -1)).RotatedByRandom(MathHelper.ToRadians(15)); // 30 degree spread.
							int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.GreenFairy, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
							Main.dust[dustIndex].noGravity = true;
						}


					}
				}

				





				//Main.projectile[index].originalDamage = Projectile.damage;
			}

			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			
			
			Projectile.spriteDirection = Projectile.direction;
			//

			//Projectile.position.X = player.Center.X;
			if (Projectile.spriteDirection == 1)
			{
				Projectile.position.X = player.Center.X - 15;

			}
			else
			{
				Projectile.position.X = player.Center.X - 15;

			}
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2) - 52;

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
						//bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
						bool lineOfSight = true;
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
