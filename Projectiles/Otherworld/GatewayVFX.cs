
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SubworldLibrary;
using Terraria;using Terraria.GameContent;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Otherworld
{
	public class GatewayVFX : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("StellarGateway");
			//Main.projFrames[projectile.type] = 14;
		}

		public override void SetDefaults() {
			Projectile.width = 300;
			Projectile.height = 300;
			Projectile.aiStyle = 0;
			Projectile.penetrate = -1;
			Projectile.scale = 1;
			Projectile.alpha = 0;
			Projectile.damage = 0;
			Projectile.hide = false;
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			
		}
		int timer;
		int fadeIn = 0;
		
		float projectileVelocity = 15;

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		float rotationSpeed = 10f;

		bool firstSpawn = true;
		public override void AI() {
			
			Projectile.timeLeft = 10;
			Projectile.scale = 1.7f;
			
			float rotationsPerSecond = rotationSpeed;
			rotationSpeed = 0.4f;
			bool rotateClockwise = false;
			//The rotation is set here
			
			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
		}

       
    }
}
