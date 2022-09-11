
using Microsoft.Xna.Framework;
using System.Security.Policy;
using Terraria;using Terraria.GameContent;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.AshenAmbition
{
	public class AshenAmbitionMarker : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Ashen Ambition");
			
		}

		public override void SetDefaults() {
			Projectile.width = 160;
			Projectile.height = 160;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 30;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			//projectile.penetrate = -1;
			Projectile.hostile = false;
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

			if(Projectile.ai[0] == 0)
            {
				Projectile.scale = 1.5f;
            }
			Projectile.ai[0] += 1f;
			Player projOwner = Main.player[Projectile.owner];

			// Fade in
			Projectile.alpha -= 6;
			Projectile.scale -= 0.01f;


				if (Projectile.alpha < 100)
				{
					Projectile.alpha = 100;
				}

			
			
		}
	}
}
