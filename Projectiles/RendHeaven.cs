
using Microsoft.Xna.Framework;
using System.Security.Policy;
using Terraria;using Terraria.GameContent;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles
{
	public class RendHeaven : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Rend Heaven");
			
		}

		public override void SetDefaults() {
			Projectile.width = 420;
			Projectile.height = 2048;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 200;
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
				Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center, Vector2.Zero, ProjectileType<RendHeavenDamage>(), 200, 0f, Projectile.owner, 0f, 0f);
			}
				// Fade in
				Projectile.alpha--;
				if (Projectile.alpha < 100)
				{
					Projectile.alpha = 100;
				}

			
		}
	}
}
