using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Pets
{
    public class LukaPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			
			//DrawOffsetX = -20;
			DisplayName.SetDefault("Luka"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 10;
			
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.MiniMinotaur);
			AIType = ProjectileID.MiniMinotaur;
			DrawOriginOffsetY = -8;
			//AnimationType = ProjectileID.BabyDino;
			Projectile.light = 1f;
			DrawOffsetX = -20;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.miniMinotaur = false; // Relic from AIType
			return true;
		}
		bool invisible;
		public override void AI()
		{
			Projectile.velocity.X *= 1.00f;
			Player player = Main.player[Projectile.owner];
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			/*if (projectile.velocity.Y >= 0.3 || projectile.velocity.X >= 0.3 || projectile.velocity.Y <= -0.3 || projectile.velocity.X <= -0.3)
			{
				if (projectile.frameCounter++ >= 10)
				{

					if (projectile.frame++ > 5)
					{
						projectile.frame = 1;
					}
				}
				if (!invisible)
                {
					//poof dust here
					invisible = true;
                }
				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = projectile.Center;
				//dust = Terraria.Dust.NewDustPerfect(position, 71, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 0.4f);

				
			}
			else
			{
				projectile.frame = 0;
				invisible = false;
				
				//projectile.frame = 0;
			}*/
			
			if (player.dead)
			{
				modPlayer.LukaPet = false;
			}
			if (modPlayer.LukaPet)
			{
				Projectile.timeLeft = 2;
			}
		}
	}
	
}