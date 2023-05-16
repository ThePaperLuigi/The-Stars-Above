using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Pets
{
    public class DuckHuntBirdPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DrawOriginOffsetY = -40;
			//DrawOffsetX = -20;
			// DisplayName.SetDefault("Duck Hunt Bird"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 5;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Parrot);
			AIType = ProjectileID.Parrot;
			Projectile.light = 1f;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.parrot = false; // Relic from AIType
			return true;
		}
		bool invisible;
		int frame;
		int frameCounter;
		public override void AI()
		{
			frameCounter++;
			Projectile.velocity.X *= 1.00f;
			Player player = Main.player[Projectile.owner];
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();

			
			if (Projectile.velocity.Y >= 0.3 || Projectile.velocity.X >= 0.3 || Projectile.velocity.Y <= -0.3 || Projectile.velocity.X <= -0.3)
			{

				if(!invisible)
                {
					Dust dust2;
					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					Vector2 position2 = new Vector2(Projectile.Center.X, Projectile.Center.Y - 35);
					for (int d = 0; d < 25; d++)
					{
						dust2 = Main.dust[Terraria.Dust.NewDust(position2, 30, 30, 229, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
					}

					invisible = true;
                }
				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = Projectile.Center;
				//dust = Terraria.Dust.NewDustPerfect(position, 71, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 0.4f);

				Projectile.frame = 5;
			}
			else
			{
				if(invisible)
                {
					Dust dust2;
					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					Vector2 position2 = new Vector2(Projectile.Center.X, Projectile.Center.Y - 35);
					for (int d = 0; d < 25; d++)
					{
						dust2 = Main.dust[Terraria.Dust.NewDust(position2, 30, 30, 229, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
					}
				}
				invisible = false;

				//projectile.frame = 0;
				if (frameCounter >= 0 && frameCounter < 10)//Because of parrot code, this has to be done.. if there's another way, I don't know it!
				{
					Projectile.frame = 0;


				}
				if (frameCounter >= 10 && frameCounter < 20)//Because of parrot code, this has to be done.. if there's another way, I don't know it!
				{
					Projectile.frame = 1;
					
					
				}
				if (frameCounter >= 20 && frameCounter < 30)
				{
					Projectile.frame = 2;




				}
				if (frameCounter >= 30 && frameCounter < 40)
				{
					Projectile.frame = 3;


				}
				
				if (frameCounter >= 40)
				{
					Projectile.frame = 0;
					frameCounter = 0;

				}
			}
			
			if (player.dead)
			{
				modPlayer.DuckHuntBirdPet = false;
			}
			if (modPlayer.DuckHuntBirdPet)
			{
				Projectile.timeLeft = 2;
			}
		}
	}
	
}