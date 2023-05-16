
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace StarsAbove.Projectiles
{
    public class RexLapisMeteor : ModProjectile
	{
		public override void SetStaticDefaults() {//TODO ADD FADE IN
			// DisplayName.SetDefault("Rex Lapis");
			Main.projFrames[Projectile.type] = 10;

		}

		public override void SetDefaults() {
			Projectile.width = 80;
			Projectile.height = 80;
			Projectile.aiStyle = 1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.timeLeft = 50;
			Projectile.hide = false;
			Projectile.ownerHitCheck = false;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = true;
			Projectile.friendly = true;
			Projectile.netUpdate = true;

		}
		bool inGround;
		int groundFirstTime = 1;
		float keepRotation;
		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		

		// It appears that for this AI, only the ai0 field is used!
		public override void AI() {
			// Since we access the owner player instance so much, it's useful to create a helper local variable for this
			// Sadly, Projectile/ModProjectile does not have its own
			Player projOwner = Main.player[Projectile.owner];
			// Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
			Projectile.netUpdate = true; // Make sure to netUpdate this spear

			if (groundFirstTime == 1 && inGround)
			{
				for (int d = 0; d < 10; d++)
				{
					Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 0, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
				}
				for (int d = 0; d < 5; d++)
				{
					Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 0, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
				}

				for (int d = 0; d < 10; d++)
				{
					Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 7, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default(Color), 1.5f);
				}
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

				groundFirstTime = 0;

			}


			if (++Projectile.frameCounter >= 2)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 9)
				{
					Projectile.frame = 9;
					
				}

			}



			if (projOwner.GetModPlayer<WeaponPlayer>().rexLapisSpear == false) {
				if (inGround == false)
				{

					for (int d = 0; d < 10; d++)
					{
						Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 0, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
					}
					for (int d = 0; d < 5; d++)
					{
						Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 0, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
					}

					for (int d = 0; d < 10; d++)
					{
						Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 7, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default(Color), 1.5f);
					}
				}
				Projectile.Kill();
			}
			// Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
			// MathHelper.ToRadians(xx degrees here)
			if (inGround == false)
			{
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
				if (Projectile.spriteDirection == -1)
				{
					Projectile.rotation -= MathHelper.ToRadians(90f);
				}
				keepRotation = Projectile.rotation;
			}
			else
			{
				Projectile.rotation = keepRotation;
			}
			// Offset by 90 degrees here
			
			// These dusts are added later, for the 'ExampleMod' effect
			if (Main.rand.NextBool(3)) {
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, 269,
					Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 269, Scale: 1.2f);
				dust.velocity += Projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
			if (Main.rand.NextBool(4)) {
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, 269,
					0, 0, 269, Scale: 0.3f);
				dust.velocity += Projectile.velocity * 0.5f;
				dust.velocity *= 0.5f;
			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			inGround = true;
			Projectile.rotation = keepRotation;
				Projectile.velocity *= Vector2.Zero;
			Projectile.rotation = keepRotation;


			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			/**/

			return false;
		}

	}
}
