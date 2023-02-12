using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.Kifrosse;

namespace StarsAbove.Projectiles.Kifrosse
{
    public class Kifrosse5 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			
			DisplayName.SetDefault("Foxfrost Mystic"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 5;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			//ProjectileID.Sets.Homing[Projectile.type] = true;

		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Parrot);
			AIType = ProjectileID.Parrot;
			//projectile.light = 1f;
			Projectile.minion = true;
			Projectile.minionSlots = 5f;
			Projectile.penetrate = -1;
			DrawOriginOffsetY = -58;
			DrawOffsetX = -60;
		}
		int amaterasu;
		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.parrot = false; // Relic from AIType
			return true;
		}
		bool invisible;
		public override void AI()
		{
			Projectile.velocity.X *= 1.00f;
			Player player = Main.player[Projectile.owner];
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			modPlayer.KifrossePosition = Projectile.Center;
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Kifrosse.DancingFoxfire1>()] < 1)
			{
				int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.Kifrosse.DancingFoxfire1>(), Projectile.damage, 4, player.whoAmI, 0f);
				Main.projectile[index].originalDamage = Projectile.damage;

			}
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Kifrosse.DancingFoxfire2>()] < 1)
			{
				int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.position.X, player.position.Y, 0, 0, ProjectileType<Projectiles.Kifrosse.DancingFoxfire2>(), Projectile.damage, 4, player.whoAmI, 0f);
				Main.projectile[index].originalDamage = Projectile.damage;

			}
			if (Projectile.velocity.Y >= 0.3 || Projectile.velocity.X >= 0.3 || Projectile.velocity.Y <= -0.3 || Projectile.velocity.X <= -0.3)
			{
				player.AddBuff(BuffID.Swiftness, 2);
				if (Projectile.frameCounter++ >= 10)
				{

					if (Projectile.frame++ > 5)
					{
						Projectile.frame = 1;
					}
				}
				if (!invisible)
                {
					//poof dust here
					invisible = true; 
					Vector2 position2 = new Vector2(Projectile.Center.X, Projectile.Center.Y - 35);
					for (int d = 0; d < 25; d++)
					{
						Dust dust2 = Main.dust[Terraria.Dust.NewDust(position2, 30, 30, 229, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
					}
					
						for (int i = 0; i < 20; i++)
						{
							int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 20, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 3f);
							Main.dust[dustIndex].noGravity = true;
							Main.dust[dustIndex].velocity *= 5f;
							dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 20, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
							Main.dust[dustIndex].velocity *= 3f;
						}

						for (int d = 0; d < 15; d++)
						{
							Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 91, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
						}

					
					for (int d = 0; d < 15; d++)
					{
						Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 221, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
					}
					// Smoke Dust spawn
					for (int i = 0; i < 10; i++)
					{
						int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
						Main.dust[dustIndex].velocity *= 1.4f;
					}
					// Fire Dust spawn

					// Large Smoke Gore spawn
					for (int g = 0; g < 2; g++)
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
				}
				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = Projectile.Center;
				//dust = Terraria.Dust.NewDustPerfect(position, 71, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 0.4f);

				
			}
			else
			{
				for (int i = 0; i < Main.maxPlayers; i++)
				{
					Player p = Main.player[i];
					if (p.active && !p.dead && p.Distance(Projectile.Center) < 290f)
					{
						p.AddBuff(BuffType<StalwartSnow>(), 2);
					}

				}
				for (int i = 0; i < 30; i++)
				{//Circle
					Vector2 offset = new Vector2();
					double angle = Main.rand.NextDouble() * 2d * Math.PI;
					offset.X += (float)(Math.Sin(angle) * 294);
					offset.Y += (float)(Math.Cos(angle) * 294);

					Dust d = Dust.NewDustPerfect(Projectile.Center + offset, 20, Vector2.Zero, 200, default(Color), 0.7f);
					d.fadeIn = 0.1f;
					d.noGravity = true;
				}

				if (invisible)
				{
					Dust dust2;
					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					Vector2 position2 = new Vector2(Projectile.Center.X, Projectile.Center.Y - 35);
					for (int d = 0; d < 25; d++)
					{
						dust2 = Main.dust[Terraria.Dust.NewDust(position2, 30, 30, 229, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
					}
					
						for (int i = 0; i < 20; i++)
						{
							int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 20, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 3f);
							Main.dust[dustIndex].noGravity = true;
							Main.dust[dustIndex].velocity *= 5f;
							dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 20, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
							Main.dust[dustIndex].velocity *= 3f;
						}
						
						for (int d = 0; d < 15; d++)
						{
							Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 91, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
						}

					
					for (int d = 0; d < 15; d++)
					{
						Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 221, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
					}
					// Smoke Dust spawn
					for (int i = 0; i < 10; i++)
					{
						int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
						Main.dust[dustIndex].velocity *= 1.4f;
					}
					// Fire Dust spawn

					// Large Smoke Gore spawn
					for (int g = 0; g < 2; g++)
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
				}
				Projectile.frame = 0;
				invisible = false;
				
				//projectile.frame = 0;
			}
			
			if (player.dead)
			{
				modPlayer.Kifrosse5 = false;
			}
			if (modPlayer.Kifrosse5)
			{
				Projectile.timeLeft = 2;
			}
			if (amaterasu > 480)
			{
				for (int d = 0; d < 6; d++)
				{
					Dust.NewDust(player.position, Projectile.width, Projectile.height, 221, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default(Color), 1.5f);
				}
				player.AddBuff(BuffType<AmaterasuGrace>(), 180);
				amaterasu = 0;



			}
			amaterasu++;
		}
	}
	
}