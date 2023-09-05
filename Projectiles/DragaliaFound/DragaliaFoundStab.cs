
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.DragaliaFound
{
    public class DragaliaFoundStab : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("DragaliaFound");
			//ProjectileID.Sets.TrailCacheLength[Projectile.type] = 76;    //The length of old position to be recorded
			//ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 132;
			Projectile.height = 132;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.timeLeft = 15;
			Projectile.alpha = 0;
			Projectile.DamageType = DamageClass.SummonMeleeSpeed;
			Projectile.hide = false;
			Projectile.ownerHitCheck = true;
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
			Projectile.timeLeft = projOwner.itemTime;
			Projectile.position.X = projOwner.Center.X - (float)(Projectile.width / 2);
			Projectile.position.Y = projOwner.Center.Y - (float)(Projectile.height / 2);
			// As long as the player isn't frozen, the spear can move
			if (!projOwner.frozen) {
				if (movementFactor == 0f) // When initially thrown out, the ai0 will be 0f
				{
					movementFactor = 50f; // Make sure the spear moves forward when initially thrown out
					Projectile.netUpdate = true; // Make sure to netUpdate this spear
				}
				if (Projectile.timeLeft < projOwner.itemTimeMax/2) // Somewhere along the item animation, make sure the spear moves back
				{
					movementFactor += 0.5f;
					Projectile.alpha += 50;
				}
				else // Otherwise, increase the movement factor
				{
					movementFactor += 1f;
				}
			}
			// Change the spear position based off of the velocity and the movementFactor
			Projectile.position += Projectile.velocity * movementFactor;
			// When we reach the end of the animation, we can kill the spear projectile
			if (Projectile.alpha > 250) {
				Projectile.Kill();
			}
			// Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
			// MathHelper.ToRadians(xx degrees here)
			Projectile.rotation = Projectile.velocity.ToRotation();//+ MathHelper.ToRadians(135f)
			
			Projectile.alpha -= 20;
			Projectile.alpha = (int)MathHelper.Clamp(Projectile.alpha, 0, 255);
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation -= MathHelper.ToRadians(180f);
			}
			//Projectile.spriteDirection = Projectile.direction;

			
			projOwner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (projOwner.Center -
							new Vector2(Projectile.Center.X + (projOwner.velocity.X * 0.05f), Projectile.Center.Y + (projOwner.velocity.Y * 0.05f))
							).ToRotation() + MathHelper.PiOver2);
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			// Vanilla has several particles that can easily be used anywhere.
			// The particles from the Particle Orchestra are predefined by vanilla and most can not be customized that much.
			// Use auto complete to see the other ParticleOrchestraType types there are.
			// Here we are spawning the Excalibur particle randomly inside of the target's hitbox.
			ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Excalibur,
				new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox)  },
				Projectile.owner);

			// You could also spawn dusts at the enemy position. Here is simple an example:
			// Dust.NewDust(Main.rand.NextVector2FromRectangle(target.Hitbox), 0, 0, ModContent.DustType<Content.Dusts.Sparkle>());

			// Set the target's hit direction to away from the player so the knockback is in the correct direction.
			hit.HitDirection = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
		}
		public override bool PreDraw(ref Color lightColor)
		{
			default(Effects.WhiteTrail).Draw(Projectile);

			return true;
		}
	}

	
}
