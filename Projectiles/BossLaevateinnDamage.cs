
using Microsoft.Xna.Framework;
using System.Security.Policy;
using Terraria;using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles
{
	public class BossLaevateinnDamage : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Ars Laevateinn");
			
		}

		public override void SetDefaults() {
			Projectile.width = 1020;//1320
			Projectile.height = 1020;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile =  true;
			Projectile.friendly = false;
			Projectile.damage = 300;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.netUpdate = true;


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
			Projectile.damage = 500;
			// Fade in
			Projectile.alpha--;
				if (Projectile.alpha < 100)
				{
					Projectile.alpha = 100;
				}

			
		}
	}
}
