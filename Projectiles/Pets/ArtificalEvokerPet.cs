using Microsoft.Xna.Framework;
using StarsAbove.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Pets
{
    public class ArtificalEvokerPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Astrogirl"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 1;
			Main.projPet[Projectile.type] = true; 
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 70;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.ZephyrFish);
			AIType = ProjectileID.ZephyrFish;
			Projectile.light = 1f;
			Projectile.width = 44;
			Projectile.height = 44;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.zephyrfish = false; // Relic from AIType
			return true;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			if (Projectile.velocity.Y != 0)
			{
				default(Effects.PinkTrail).Draw(Projectile);
			}


			return true;
		}
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			WeaponPlayer modPlayer = player.GetModPlayer<WeaponPlayer>();
			if (player.dead)
			{
				modPlayer.AigisPet = false;
			}
			if (modPlayer.AigisPet)
			{
				Projectile.timeLeft = 2;
			}
		}
	}
	
}