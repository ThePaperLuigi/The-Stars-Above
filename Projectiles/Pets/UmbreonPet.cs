using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Pets
{
    public class UmbreonPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			
			// DisplayName.SetDefault("Umbreon"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 2;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Parrot);
			AIType = ProjectileID.Parrot;
			Projectile.light = 1f;
			DrawOriginOffsetY = -20;
			DrawOffsetX = -20;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.parrot = false; // Relic from AIType
			return true;
		}

		public override void AI()
		{
			Projectile.velocity.X *= 0.97f;
			Player player = Main.player[Projectile.owner];
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			if(Projectile.velocity.Y >= 0.3 || Projectile.velocity.X >= 0.3 || Projectile.velocity.Y <= -0.3 || Projectile.velocity.X <= -0.3)
            {
				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = Projectile.Center;
				dust = Terraria.Dust.NewDustPerfect(position, 124, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 0.4f);

				Projectile.frame = 1;
            }
			else
            {
				Projectile.frame = 0;
            }
			if (player.dead)
			{
				modPlayer.UmbreonPet = false;
			}
			if (modPlayer.UmbreonPet)
			{
				Projectile.timeLeft = 2;
			}
		}
	}
	
}