
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Vagrant
{
    public class VagrantBulletSwarm : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Wormhole");
			
		}

		public override void SetDefaults() {
			Projectile.width = 400;
			Projectile.height = 400;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 190;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.penetrate = -1;
			Projectile.hide = false;
			Projectile.hostile = false;
			Projectile.friendly = false;
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?

		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		
		public override void AI() {
			if(Projectile.ai[1] == 0)
            {
				Projectile.rotation = MathHelper.ToRadians(180f);
				Projectile.ai[1]++;
            }
			float rotationsPerSecond = 0.3f;
			bool rotateClockwise = false;
			//The rotation is set here
			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
			Projectile.ai[0]++;
			Player projOwner = Main.player[Projectile.owner];
			float Speed = 1f;
			float Speed2 = -1f;
			if (Projectile.ai[0] >= 8)
            {
				Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, (float)((Math.Cos(Projectile.rotation) * Speed) * -1), (float)((Math.Sin(Projectile.rotation) * Speed) * -1), ProjectileID.BrainScramblerBolt, Projectile.damage, 0f, 0);
				Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, (float)((Math.Cos(Projectile.rotation) * Speed2) * -1), (float)((Math.Sin(Projectile.rotation) * Speed2) * -1), ProjectileID.BrainScramblerBolt, Projectile.damage, 0f, 0);

				Projectile.ai[0] = 0;
            }
			
		}
		public override void OnKill(int timeLeft)
		{
			
			

		}
	}
}
