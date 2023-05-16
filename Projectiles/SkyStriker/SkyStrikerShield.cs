
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.SkyStrikerBuffs;
using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.SkyStriker
{
    public class SkyStrikerShield : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Armament of the Sky Striker");
			DrawOriginOffsetY = 12;
		}

		public override void SetDefaults() {
			Projectile.width = 70;
			Projectile.height = 130;
			Projectile.aiStyle = 0;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.timeLeft = 60;
			Projectile.hide = true;
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.hostile = false;
		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		// It appears that for this AI, only the ai0 field is used!
		public override void AI() {
			// Since we access the owner player instance so much, it's useful to create a helper local variable for this
			Projectile.timeLeft = 10;
			// Sadly, Projectile/ModProjectile does not have its own
			Player projOwner = Main.player[Projectile.owner];
			if(!projOwner.channel)
            {
				Projectile.Kill();
            }
			projOwner.itemAnimation = 10;
			projOwner.itemTime = 10;
			// Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			projOwner.heldProj = Projectile.whoAmI;
			
			Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2) - 10;
			// As long as the player isn't frozen, the spear can move
			if (!projOwner.frozen) {
				if (movementFactor == 0f) // When initially thrown out, the ai0 will be 0f
				{
					movementFactor = 0.2f; // Make sure the spear moves forward when initially thrown out
					Projectile.netUpdate = true; // Make sure to netUpdate this spear
				}
				if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3) // Somewhere along the item animation, make sure the spear moves back
				{
					//movementFactor -= 2.4f;
				}
				else // Otherwise, increase the movement factor
				{
					//movementFactor += 2.4f;
				}
			}
			// Change the spear position based off of the velocity and the movementFactor
			Projectile.position += Projectile.velocity * movementFactor;
			// When we reach the end of the animation, we can kill the spear projectile
			
			// Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
			// MathHelper.ToRadians(xx degrees here)
			Projectile.rotation = Projectile.velocity.ToRotation();
			// Offset by 90 degrees here
			if (Projectile.spriteDirection == -1) {
				Projectile.rotation -= MathHelper.ToRadians(180f);
			}
			Projectile.spriteDirection = Projectile.direction;
			// These dusts are added later, for the 'ExampleMod' effect
			/*if (Main.rand.NextBool(3)) {
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 20,
					projectile.velocity.X * .2f, projectile.velocity.Y * .2f, 200, Scale: 1.2f);
				dust.velocity += projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
			if (Main.rand.NextBool(4)) {
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width,20,
					0, 0, 254, Scale: 0.3f);
				dust.velocity += projectile.velocity * 0.5f;
				dust.velocity *= 0.5f;
			}*/
			for (int i = 0; i < 30; i++)
			{//Circle
				Vector2 offset = new Vector2();
				double angle = Main.rand.NextDouble() * 2d * Math.PI;
				offset.X += (float)(Math.Sin(angle) * 250);
				offset.Y += (float)(Math.Cos(angle) * 250);

				Dust d = Dust.NewDustPerfect(Projectile.Center + offset, 20, Vector2.Zero, 200, default(Color), 0.7f);
				d.fadeIn = 0.1f;
				d.noGravity = true;
			}
			for (int i = 0; i < Main.maxPlayers; i++)
			{
				Player p = Main.player[i];
				if (p.active && !p.dead && p.Distance(Projectile.Center) < 250f)
				{
					p.AddBuff(BuffType<Shielded>(), 2);
					
				}

			}
			Projectile closest = null;
			float closestDistance = 9999999;
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile projectile = Main.projectile[i];
				float distance = Vector2.Distance(projectile.Center, projOwner.Center);


				if (projectile.hostile && projectile.Distance(projOwner.position) < closestDistance && projectile.damage > 0)
				{
					closest = projectile;
					closestDistance = projectile.Distance(projOwner.position);
				}




			}

			if (closestDistance < 150f)
			{
				if (Projectile.ai[1] > 120)
				{
					closest.Kill();
					Projectile.ai[1] = 0;
				}
			}

			Projectile.ai[1]++;
			
		}
	}
}
