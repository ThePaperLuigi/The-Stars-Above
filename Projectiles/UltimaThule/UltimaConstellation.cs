
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;

namespace StarsAbove.Projectiles.UltimaThule
{
    public class UltimaConstellation : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Ultima Constellation");
			
		}

		public override void SetDefaults() {
			Projectile.width = 54;
			Projectile.height = 54;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 280;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile = false;
			Projectile.friendly = true;


		}
		int connectTo = Main.rand.Next(0, 30);
		float x = Main.rand.NextFloat(-0.9f, 0.9f);
		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		
		public override void AI() {
			float rotationsPerSecond = x;
			bool rotateClockwise = true;
			//The rotation is set here
			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
			Projectile.ai[0] += 1f;
			Player projOwner = Main.player[Projectile.owner];

			// Fade in
			Projectile.alpha -= 5;
				if (Projectile.alpha < 50)
				{
					Projectile.alpha = 50;
				}

			
			

			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile proj = Main.projectile[i];

				if (proj.type == ProjectileType<UltimaConstellation>() && proj.timeLeft > 0)
                {
					for (int i3 = 0; i3 < connectTo; i3++)
					{
						Vector2 position2 = Vector2.Lerp(Projectile.Center, proj.Center, (float)i3 / connectTo);
						Dust d = Dust.NewDustPerfect(position2, 20, null, 240, default(Color), 0.3f);
						d.fadeIn = 0.4f;
						d.noLight = true;
						d.noGravity = true;
						d.velocity = Vector2.Zero;
					}
				}
			}

			/*if (Main.rand.NextBool(5))``````````````````````````````````````````````
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 20,
					projectile.velocity.X * .2f, projectile.velocity.Y * .2f, 269, Scale: 0.4f);
				dust.velocity += projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}*/
		}
		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			if (Projectile.ai[1] == 0)
			{
				for (int i = 0; i < 8; i++)
				{
					// Random upward vector.
					Vector2 vel = new Vector2(Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-1, -4));
					//Projectile.NewProjectile(Projectile.GetSource_FromThis(),projectile.Center, vel, ProjectileType<UltimaStarProjectile>(), 40, projectile.knockback, projectile.owner, 0, 1);
				}
			}
			// Play explosion sound
			SoundEngine.PlaySound(SoundID.Item30, Projectile.position);
			// Smoke Dust spawn
			/*for (int i = 0; i < 30; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}*/
			// Fire Dust spawn (CHANGE TO ICE DUST)
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 20, 0f, 0f, 100, default(Color), 0.8f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 5f;
				dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 20, 0f, 0f, 100, default(Color), 0.8f);
				Main.dust[dustIndex].velocity *= 3f;
			}
			// Large Smoke Gore spawn
			/*for (int g = 0; g < 2; g++)
			{
				int goreIndex = Gore.NewGore(null,new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(null,new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(null,new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				goreIndex = Gore.NewGore(null,new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
			}*/
			// reset size to normal width and height.
			Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);

		}
	}
}
