
using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles
{
    public class PhantomMarker : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Phantom In The Mirror");
			
		}

		public override void SetDefaults() {
			Projectile.width = 80;
			Projectile.height = 80;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 999999;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.tileCollide = false;


		}
		float rotationSpeed = 1.5f;
		float throwSpeed = 10f;
		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		
		public override void AI() {

			Projectile.ai[0] += 1f;
			Player projOwner = Main.player[Projectile.owner];

			// Fade in
			Projectile.alpha -= 10;
			
			if(projOwner.GetModPlayer<WeaponPlayer>().phantomKill == true)
            {
				Projectile.Center = projOwner.GetModPlayer<WeaponPlayer>().phantomSavedPosition;
				projOwner.GetModPlayer<WeaponPlayer>().phantomKill = false;
			}
			if (projOwner.GetModPlayer<WeaponPlayer>().phantomTeleport == false || projOwner.statMana <= 0)
			{
				
				Projectile.Kill();
				

			}
			if (Main.rand.NextBool(5))
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, 20,
					Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 269, Scale: 1.2f);
				dust.velocity += Projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}

			float rotationsPerSecond = rotationSpeed;
			//rotationSpeed -= 0.1f;
			bool rotateClockwise = true;
			//The rotation is set here
			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation -= MathHelper.ToRadians(90f);
			}
		}
	}
}
