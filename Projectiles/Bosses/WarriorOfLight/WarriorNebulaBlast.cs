
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace StarsAbove.Projectiles.Bosses.WarriorOfLight
{
    public class WarriorNebulaBlast : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Starmatter");
			Main.projFrames[Projectile.type] = 11;
		}

		public override void SetDefaults() {
			Projectile.width = 90;
			Projectile.height = 90;
			Projectile.timeLeft = 150;
			Projectile.penetrate = -1;
			Projectile.aiStyle = 1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.localNPCHitCooldown = -1;
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.netUpdate = true;
			AIType = ProjectileID.Bullet;

		}
		bool finished;
		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			hitbox.Width /= 2;
			hitbox.Height /= 2;


			base.ModifyDamageHitbox(ref hitbox);
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
			Lighting.AddLight(Projectile.Center, new Vector3(0.99f, 0.6f, 0.3f));
			
			if(Projectile.timeLeft > 50)
            {
				Projectile.damage = 0;
            }
			if(Projectile.timeLeft < 50)
            {
				Projectile.damage = 100;
            }
			
			
			if (++Projectile.frameCounter >= 4)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 11 && Projectile.timeLeft < 50)
				{
					SoundEngine.PlaySound(StarsAboveAudio.SFX_HolyStab, Projectile.Center);

					Projectile.Kill();

				}
				if (++Projectile.frame >= 4 && Projectile.timeLeft > 50)
				{
					Projectile.frame = 0;

				}
				
				
			}
			
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
			// Offset by 90 degrees here
			if (Projectile.spriteDirection == -1) {
				Projectile.rotation -= MathHelper.ToRadians(90f);
			}


			// These dusts are added later, for the 'ExampleMod' effect
			
			
			
			

		}
	}
}
