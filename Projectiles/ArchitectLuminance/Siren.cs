
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;using Terraria.GameContent;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.ArchitectLuminance
{
	public class Siren : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Artifice Siren");
			Main.projFrames[Projectile.type] = 5;
		}

		public override void SetDefaults() {
			Projectile.width = 340;
			Projectile.height = 340;
			Projectile.aiStyle = 0;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 900;
			Projectile.scale = 1;
			Projectile.alpha = 255;
			//projectile.damage = 0;
			Projectile.hide = false;
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
		}
		int timer;
		bool firstTick = true;
		float escapeVelocity = 0f;
		float projectileVelocity = 15;

		
		
		// It appears that for this AI, only the ai0 field is used!
		public override void AI() {
			timer++;
			// Since we access the owner player instance so much, it's useful to create a helper local variable for this
			// Sadly, Projectile/ModProjectile does not have its own
			Player projOwner = Main.player[Projectile.owner];
			// Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			//projOwner.heldProj = projectile.whoAmI;

			//projectile.scale += 0.001f;
			projOwner.GetModPlayer<StarsAbovePlayer>().sirenCenter = Projectile.Center;
			projOwner.GetModPlayer<StarsAbovePlayer>().sirenCenterAdjusted = new Vector2(Projectile.Center.X, Projectile.Center.Y - 200);
			if (Projectile.frame == 1)
            {
				if(firstTick)
                {
					Vector2 placement2 = new Vector2((Projectile.Center.X), Projectile.Center.Y);
					//Projectile.NewProjectile(Projectile.GetSource_FromThis(),placement2.X, placement2.Y, 0, 0, mod.ProjectileType("ClaimhBurst"), (int)projectile.ai[0], 0f, 0, 0);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),placement2.X, placement2.Y, 0, 0, Mod.Find<ModProjectile>("radiateDamage").Type, 0, 0f, 0, 0);
					//Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/HolyStab"));
					for (int d = 0; d < 26; d++)
					{
						Dust.NewDust(new Vector2(Projectile.Center.X - 15, Projectile.Center.Y), 0, 0, DustType<Dusts.Star>(), 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
					}
					firstTick = false;
                }
				
				

				
				
			}

			
			if (++Projectile.frameCounter >= 9)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 5)
				{
					
					Projectile.frame = 0;

				}

			}
			if(Projectile.timeLeft <= 60)
            {
				
				Projectile.alpha += 2;
				Projectile.velocity = new Vector2(0, -(1f + escapeVelocity));
				escapeVelocity += 0.5f;
			}
			else
            {
				Projectile.alpha -= 8;
            }
			if(Projectile.alpha >= 250)
            {
				Projectile.Kill();
			}




			NPC closest = null;
			float closestDistance = 9999999;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				float distance = Vector2.Distance(npc.Center, Projectile.Center);


				if (npc.active && npc.Distance(Projectile.position) < closestDistance)
				{
					closest = npc;
					closestDistance = npc.Distance(Projectile.position);
				}




			}

			if (closest.CanBeChasedBy() && closestDistance < 1200f)
			{
				projOwner.GetModPlayer<StarsAbovePlayer>().sirenEnemy = closest.Center;
			}
			
			
			
			Projectile.ai[0]++;
			
			if (closest.Center.X > Projectile.Center.X)
            {
				Projectile.direction = -1;
            }
			else
            {
				Projectile.direction = 1;
            }
			//projectile.direction = projOwner.GetModPlayer<StarsAbovePlayer>().sirenTarget.X > projectile.direction ? 1 : -1;
			Projectile.spriteDirection = Projectile.direction;

		}
	}
}
