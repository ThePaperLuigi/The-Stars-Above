using Microsoft.Xna.Framework;
using StarsAbove;
using System;
using Terraria;using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Pets
{
	public class PhymPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Phym"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 1;
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
			StarsAbovePlayer modPlayer = player.GetModPlayer<StarsAbovePlayer>();
			if (player.dead)
			{
				modPlayer.PhymPet = false;
			}
			if (modPlayer.PhymPet)
			{
				Projectile.timeLeft = 2;
			}
		}
	}
	
}