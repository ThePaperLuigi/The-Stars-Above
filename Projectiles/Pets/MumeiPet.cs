using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Pets
{
    public class MumeiPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			//
			//DrawOffsetX = -20;
			DisplayName.SetDefault("Mumei"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 13;
			
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}
		int idleAnimation;
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.MiniMinotaur);
			AIType = ProjectileID.MiniMinotaur;
			//AnimationType = ProjectileID.BabyDino;
			Projectile.light = 1f;
			DrawOriginOffsetY = -8;
			DrawOffsetX = -20;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.miniMinotaur = false; // Relic from AIType
			return true;
		}
		
		public override void AI()
		{
			Projectile.velocity.X *= 1.00f;
			Player player = Main.player[Projectile.owner];
			StarsAbovePlayer modPlayer = player.GetModPlayer<StarsAbovePlayer>();

			

			if(Projectile.velocity.X == 0)
            {
				idleAnimation++;
				if(idleAnimation >= 60 && idleAnimation < 70)
                {
					Projectile.frame = 10;
                }
				if (idleAnimation >= 70 && idleAnimation < 80)
				{
					Projectile.frame = 11;
				}
				if (idleAnimation >= 80 && idleAnimation < 90)
				{
					Projectile.frame = 12;
				}
				

				if (idleAnimation > 700)
                {
					idleAnimation = 0;
                }
			}
			else
            {
				idleAnimation = 0;
            }

			if (player.dead)
			{
				modPlayer.MumeiPet = false;
			}
			if (modPlayer.MumeiPet)
			{
				Projectile.timeLeft = 2;
			}
		}

		
	}
	
}