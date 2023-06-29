using Microsoft.Xna.Framework;
using StarsAbove.Projectiles.BuryTheLight;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.Bosses.Tsukiyomi
{
    //
    public class TsukiCeruleanSlash : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shadowless Cerulean");
			Main.projFrames[Projectile.type] = 9;
			//DrawOriginOffsetY = 30;
			//DrawOffsetX = -60;
		}
		public override void SetDefaults()
		{
			Projectile.width = 200;
			Projectile.height = 200;
			Projectile.aiStyle = -1;
			Projectile.timeLeft = 60;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			
			Projectile.hide = false;
			Projectile.ownerHitCheck = false;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.hostile = true;
			//projectile.extraUpdates = 2;
		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		
		// It appears that for this AI, only the ai0 field is used!
		public override void AI()
		{
			Lighting.AddLight(Projectile.Center, new Vector3(1f, 0.4f, 0.64f));
			 
			 
			
			Player projOwner = Main.player[Projectile.owner];
			
			Projectile.netUpdate = true; // Make sure to netUpdate this spear
			
			if (Projectile.frame == 9)
			{
				
			}
			
			if (++Projectile.frameCounter >= 5)
			{
				Projectile.frameCounter = 0;
				if (Projectile.frame < 9)
				{
					for (int i = 0; i < 3; i++)
					{
						// Random upward vector.
						Vector2 vel = new Vector2(Projectile.Center.X + Main.rand.Next(-24, 24), Projectile.Center.Y + Main.rand.Next(-24, 24));
						Projectile.NewProjectile(Projectile.GetSource_FromThis(),vel, Vector2.Zero, ProjectileType<BuryTheLightSlash>(), 0, Projectile.knockBack, Projectile.owner, 0, 1);
					}
					//Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/electroSmack"));
					Projectile.frame++;
				}
				else
				{
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center, Vector2.Zero, ProjectileType<TsukiCeruleanBurst>(), Projectile.damage*2, Projectile.knockBack, Projectile.owner, 0, 1);
					for (int d = 0; d < 24; d++)
					{
						Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 20, 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default(Color), 1.5f);
					}
					for (int d = 0; d < 46; d++)
					{
						Dust.NewDust(new Vector2(Projectile.position.X+ 150,Projectile.position.Y + 150), 0, 0, 221, 0f + Main.rand.Next(-36, 36), 0f, 150, default(Color), 1.5f);
					}
					for (int d = 0; d < 46; d++)
					{
						Dust.NewDust(new Vector2(Projectile.position.X+150, Projectile.position.Y + 150),0, 0, 20, 0f + Main.rand.Next(-36, 36), 0f, 150, default(Color), 1.5f);
					}
					for (int d = 0; d < 20; d++)
					{
						Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 221, 0f + Main.rand.Next(-33, 33), 0f + Main.rand.Next(-33, 33), 150, default(Color), 1.5f);
					}
					

					// Play explosion sound
					
					// Smoke Dust spawn
					for (int i = 0; i < 40; i++)
					{
						int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f + Main.rand.Next(-26, 26), 0f + Main.rand.Next(-26, 26), 100, default(Color), 2f);
						Main.dust[dustIndex].velocity *= 1.4f;
					}
					
					// Large Smoke Gore spawn
					for (int g = 0; g < 4; g++)
					{
						int goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex].scale = 1.5f;
						Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
						Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
						goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex].scale = 1.5f;
						Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
						Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
						goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex].scale = 1.5f;
						Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
						Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
						goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex].scale = 1.5f;
						Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
						Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
					}
					Projectile.Kill();
				}

			}
			
		}
	}
}