
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;

namespace StarsAbove.Projectiles
{
    public class RadiantBraver : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Radiant Braver");
			
		}

		public override void SetDefaults() {
			Projectile.width = 420;
			Projectile.height = 2048;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 171;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = true;
			Projectile.tileCollide = false;


		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		
		public override void AI() {

			Projectile.ai[0] += 1f;
			
			if(Projectile.timeLeft == 1)
            {
				SoundEngine.PlaySound(StarsAboveAudio.SFX_GunbladeImpact, Projectile.Center);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center, Vector2.Zero, ProjectileType<AbsoluteRendHeavenDamage>(), 200, 0f, Projectile.owner, 0f, 0f);
			}
				// Fade in
				Projectile.alpha--;

			if(Projectile.timeLeft > 150)
            {
				if (Projectile.alpha < 230)
				{
					Projectile.alpha = 230;
				}
			}
			else
            {
				if (Projectile.alpha < 100)
				{
					Projectile.alpha = 100;
				}
			}
				

			
		}
	}
}
