using StarsAbove.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Pets
{
    public class BloopPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{

			// DisplayName.SetDefault("Bloop"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 1;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
			DrawOffsetX = -20;
			DrawOriginOffsetY = -16;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.DD2PetGhost);
			AIType = ProjectileID.DD2PetGhost;
			Projectile.light = 1f;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.petFlagDD2Ghost = false; // Relic from AIType
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (player.dead)
			{
				modPlayer.BloopPet = false;
			}
			if (modPlayer.BloopPet)
			{
				Projectile.timeLeft = 2;
			}
		}
	}
	
}