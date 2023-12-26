
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using StarsAbove.Systems;

namespace StarsAbove.Projectiles.Bosses.Vagrant
{
    public class VagrantStarmatter : ModProjectile
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
			
			
			
			if (++Projectile.frameCounter >= 4)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 11 && Projectile.timeLeft < 50)
				{
					SoundEngine.PlaySound(StarsAboveAudio.SFX_HolyStab, Projectile.Center);

					Vector2 vector8 = new Vector2(Projectile.position.X + (Projectile.width / 2), Projectile.position.Y + (Projectile.height / 2));
					Vector2 vel = new Vector2(-1, -1);
					vel *= 4f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, vel.X, vel.Y, Mod.Find<ModProjectile>("VagrantStar").Type, Projectile.damage, 0, Main.myPlayer);
					Vector2 vel2 = new Vector2(1, 1);
					vel2 *= 4f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, vel2.X, vel2.Y, Mod.Find<ModProjectile>("VagrantStar").Type, Projectile.damage, 0, Main.myPlayer);
					Vector2 vel3 = new Vector2(1, -1);
					vel3 *= 4f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, vel3.X, vel3.Y, Mod.Find<ModProjectile>("VagrantStar").Type, Projectile.damage, 0, Main.myPlayer);
					Vector2 vel4 = new Vector2(-1, 1);
					vel4 *= 4f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, vel4.X, vel4.Y, Mod.Find<ModProjectile>("VagrantStar").Type, Projectile.damage, 0, Main.myPlayer);
					Vector2 vel5 = new Vector2(0, -1);
					vel5 *= 4f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, vel5.X, vel5.Y, Mod.Find<ModProjectile>("VagrantStar").Type, Projectile.damage, 0, Main.myPlayer);
					Vector2 vel6 = new Vector2(0, 1);
					vel6 *= 4f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, vel6.X, vel6.Y, Mod.Find<ModProjectile>("VagrantStar").Type, Projectile.damage, 0, Main.myPlayer);
					Vector2 vel7 = new Vector2(1, 0);
					vel7 *= 4f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, vel7.X, vel7.Y, Mod.Find<ModProjectile>("VagrantStar").Type, Projectile.damage, 0, Main.myPlayer);
					Vector2 vel8 = new Vector2(-1, 0);
					vel8 *= 4f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, vel8.X, vel8.Y, Mod.Find<ModProjectile>("VagrantStar").Type, Projectile.damage, 0, Main.myPlayer);
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
