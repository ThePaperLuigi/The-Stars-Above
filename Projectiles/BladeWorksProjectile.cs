
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles
{
    public class BladeWorksProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Unlimited Blade Works");
			Main.projFrames[Projectile.type] = 10;
		}

		public override void SetDefaults() {
			Projectile.width = 68;
			Projectile.height = 78;
			Projectile.timeLeft = 500;
			Projectile.penetrate = -1;
			Projectile.aiStyle = 1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.localNPCHitCooldown = -1;
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.netUpdate = true;
			AIType = ProjectileID.Bullet;

		}
		bool finished;
		
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
			
			
			
			if (++Projectile.frameCounter >= 11)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 9)
				{
					Projectile.frame = 9;
					
				}
				
			}
			
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
			// Offset by 90 degrees here
			if (Projectile.spriteDirection == -1) {
				Projectile.rotation -= MathHelper.ToRadians(90f);
			}


			// These dusts are added later, for the 'ExampleMod' effect
			if (Main.rand.NextBool(3))
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, 204,
					Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
				dust.shader = GameShaders.Armor.GetSecondaryShader(122, Main.LocalPlayer);

				dust.velocity += Projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
			if (Main.rand.NextBool(4))
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, 204,
					0, 0, 254, Scale: 0.3f);
				dust.shader = GameShaders.Armor.GetSecondaryShader(122, Main.LocalPlayer);
				dust.velocity += Projectile.velocity * 0.5f;
				dust.velocity *= 0.5f;
			}
			
			
			

		}
	}
}
