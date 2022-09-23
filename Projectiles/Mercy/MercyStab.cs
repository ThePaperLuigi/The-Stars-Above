
using Microsoft.Xna.Framework;
using StarsAbove.Buffs;
using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Mercy
{
    public class MercyStab : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Mercy");

		}

		public override void SetDefaults() {
			Projectile.width = 130;
			Projectile.height = 130;
			Projectile.aiStyle = 19;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.timeLeft = 600;
			Projectile.hide = true;
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.friendly = true;

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
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
			// Sadly, Projectile/ModProjectile does not have its own
			Player projOwner = Main.player[Projectile.owner];
			// Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			projOwner.heldProj = Projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2);
			// As long as the player isn't frozen, the spear can move
			if (!projOwner.frozen) {
				if (movementFactor == 0f) // When initially thrown out, the ai0 will be 0f
				{
					movementFactor = 5f; // Make sure the spear moves forward when initially thrown out
					Projectile.netUpdate = true; // Make sure to netUpdate this spear
				}
				
			}
			// Change the spear position based off of the velocity and the movementFactor
			Projectile.position += Projectile.velocity * movementFactor;
			// When we reach the end of the animation, we can kill the spear projectile
			projOwner.itemTime = 10;
			projOwner.itemAnimation = 10;
			if (!projOwner.HasBuff(BuffType<Buffs.Mercy.EdgeOfAnguish>()))
			{
				Projectile.Kill();
			}
			// Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
			// MathHelper.ToRadians(xx degrees here)
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
			// Offset by 90 degrees here
			if (Projectile.spriteDirection == -1) {
				Projectile.rotation -= MathHelper.ToRadians(90f);
			}

			
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if(target.life - Math.Min((-(target.life - target.lifeMax)) / 10, 2000) > 1)
            {
				target.life -= Math.Min((-(target.life - target.lifeMax)) / 10, 2000);
				Rectangle textPos = new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height);
				CombatText.NewText(textPos, new Color(234, 0, 0, 100), $"{Math.Min((-(target.life - target.lifeMax)) / 10, 2000)}", false, false);
			}
			
			target.AddBuff(BuffType<InfernalBleed>(), 180);

		}
    }
}
