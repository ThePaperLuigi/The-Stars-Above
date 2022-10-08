
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.UltimaThule
{
    public class UltimaVFX1 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Ultima Thule");
			//Main.projFrames[projectile.type] = 14;
		}

		public override void SetDefaults() {
			Projectile.width = 500;
			Projectile.height = 500;
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
		bool firstSpawn = true;
		int timer;
		int fadeIn = 0;
		float scaleIn = 0;
		float initialSpeed = 6f;
		float projectileVelocity = 15;

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		// It appears that for this AI, only the ai0 field is used!
		
		public override void AI() {
			if (firstSpawn)
			{
				Projectile.scale = 0f;
				firstSpawn = false;
			}

			Player player = Main.player[Projectile.owner];

			Projectile.timeLeft = 10;

			if (!player.HasBuff(BuffType<Buffs.CosmicConception>()))
			{
				Projectile.scale += 0.003f;
				Projectile.alpha += 25;
			}
			Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;

			Projectile.scale += 0.003f;
			if (Projectile.alpha > 255)
			{
				Projectile.Kill();
			}
			Vector2 ownerMountedCenter = player.Center;
			Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2);
			Projectile.alpha -= 5;


		}

       
    }
}
