
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace StarsAbove.Projectiles.Tsukiyomi
{
    public class PlusPlanet : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Unstable Planetoid");
			Main.projFrames[Projectile.type] = 2;
		}

		public override void SetDefaults() {
			Projectile.width = 200;
			Projectile.height = 200;
			Projectile.timeLeft = 350;
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
		int damage;
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

			Projectile.velocity *= 0.96f;
			
			if (Projectile.timeLeft > 50)
            {
				Projectile.damage = 0;
            }
			if(Projectile.timeLeft == 110)
            {
				Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, Vector2.Zero.X,Vector2.Zero.Y, Mod.Find<ModProjectile>("PlanetSlashing").Type, damage*2, 0, Main.myPlayer);
				SoundEngine.PlaySound(StarsAboveAudio.SFX_AmiyaSlash, Projectile.Center);

			}
			if (Projectile.timeLeft < 50)
            {
				Projectile.frame = 1;
				Vector2 vector8 = new Vector2(Projectile.position.X + (Projectile.width / 2), Projectile.position.Y + (Projectile.height / 2));

				Vector2 vel7 = new Vector2(0.5f, 0);
				
					vel7 *= 7f;
				
				
				
				Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, vel7.X, vel7.Y, Mod.Find<ModProjectile>("Starmatter").Type, damage, 0, Main.myPlayer);
				Vector2 vel8 = new Vector2(-0.5f, 0);
				
					vel8 *= 7f;
				
				
				Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, vel8.X, vel8.Y, Mod.Find<ModProjectile>("Starmatter").Type, damage, 0, Main.myPlayer);
				Vector2 vel9 = new Vector2(0f, 0.5f);

				vel9 *= 7f;



				Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, vel9.X, vel9.Y, Mod.Find<ModProjectile>("Starmatter").Type, damage, 0, Main.myPlayer);
				Vector2 vel10 = new Vector2(0f, -0.5f);

				vel10 *= 7f;


				Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, vel10.X, vel10.Y, Mod.Find<ModProjectile>("Starmatter").Type, damage, 0, Main.myPlayer);
				Projectile.Kill();
			}
			
			
			
			
			Projectile.rotation = MathHelper.ToRadians(0f);
			


			// These dusts are added later, for the 'ExampleMod' effect
			
			
			
			

		}
	}
}
