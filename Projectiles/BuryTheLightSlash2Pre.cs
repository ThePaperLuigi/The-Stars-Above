
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles
{
    public class BuryTheLightSlash2Pre : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Bury The Light");
			
			Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults() {
			Projectile.width = 200;
			Projectile.height = 200;
			Projectile.aiStyle = -1;
			Projectile.timeLeft = 9900;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.light = 1f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			DrawOriginOffsetY = -200;
			DrawOffsetX = 80;
		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // 
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
        
        public override bool PreDraw(ref Color lightColor)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

			return true;
		}
		public override void AI() {

			Player projOwner = Main.player[Projectile.owner];
			Projectile.ai[0] += 1f;

			if (projOwner.GetModPlayer<WeaponPlayer>().judgementCutTimer < 0)
			{
				//if (projectile.ai[0] > 100)
				//{
				if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
				{
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center, Vector2.Zero, ProjectileType<BuryTheLightSlash2>(), 55000, Projectile.knockBack, Projectile.owner, 0, 1);
				}
				else
				{
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileType<BuryTheLightSlash2>(), 1000, Projectile.knockBack, Projectile.owner, 0, 1);

				}

				// Play explosion sound
				// Smoke Dust spawn
				for (int i = 0; i < 6; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex].velocity *= 19f;
				}
				// Fire Dust spawn (CHANGE TO ICE DUST)
				for (int i = 0; i < 2; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 20, 0f, 0f, 100, default(Color), 3f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity *= 15f;
					dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 20, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex].velocity *= 15f;
				}
				// Large Smoke Gore spawn
				for (int g = 0; g < 1; g++)
				{
					int goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 9.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 9.5f;
					goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 9.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 9.5f;
					goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 9.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 9.5f;
					goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 9.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 9.5f;
				}
				Projectile.Kill();
				//}
			}



			if (Projectile.ai[0] == 1)
			{
				
				Projectile.rotation += MathHelper.ToRadians(Main.rand.Next(0, 364));
				
			}
			Projectile.position += Projectile.velocity;
			Projectile.velocity *= 0.9f;
			Projectile.alpha+=0;

			if (Projectile.alpha >= 255)
			{
				
			}
			


		}
	}
}
