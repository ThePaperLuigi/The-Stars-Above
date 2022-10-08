using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles
{
    public class AmiyaSlashBurst : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Shadowless Cerulean Burst");
			
		}

		public override void SetDefaults() {
			Projectile.width = 300;
			Projectile.height = 300;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 1;
			Projectile.penetrate = 1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.hostile = false;
			Projectile.friendly = true;
			//projectile.usesLocalNPCImmunity = true;

		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		
		public override void AI() {
			//Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/GunbladeImpact"));

			Projectile.ai[0] += 1f;
			
			
			// Fade in
			Projectile.alpha--;
				if (Projectile.alpha < 100)
				{
					Projectile.alpha = 100;
				}

			
		}
	}
}
