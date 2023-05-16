using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Pets
{
    public class CrimsonDragonetPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Crimson Dragonet"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 6;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.ZephyrFish);
			AIType = ProjectileID.ZephyrFish;
			Projectile.light = 1f;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.zephyrfish = false; // Relic from AIType
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (Projectile.frameCounter++ >= 30)
			{

				if (Projectile.frame++ > 6)
				{
					Projectile.frame = 1;
				}
			}
			if (player.dead)
			{
				modPlayer.CrimsonDragonetPet = false;
			}
			if (modPlayer.CrimsonDragonetPet)
			{
				Projectile.timeLeft = 2;
			}
		}
	}
	
}