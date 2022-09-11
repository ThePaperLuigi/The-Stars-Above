using Microsoft.Xna.Framework;
using StarsAbove;
using System;
using Terraria;using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Pets
{
	public class BubbaPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			
			//DrawOffsetX = -20;
			DisplayName.SetDefault("Bubba"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 3;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Parrot);
			AIType = ProjectileID.Parrot;
			Projectile.light = 1f;
			DrawOriginOffsetY = -20;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.parrot = false; // Relic from AIType
			return true;
		}
		int idleTimer;
		public override void AI()
		{
			idleTimer++;
			Projectile.velocity.X *= 1f;
			Player player = Main.player[Projectile.owner];
			StarsAbovePlayer modPlayer = player.GetModPlayer<StarsAbovePlayer>();
			if(Projectile.velocity.Y >= 0.3 || Projectile.velocity.X >= 0.3 || Projectile.velocity.Y <= -0.3 || Projectile.velocity.X <= -0.3)
            {

				idleTimer = 0;
				Projectile.frame = 2;
            }
			else
            {
				if(idleTimer > 120)
                {
					Projectile.frame = 0;

				}
				else
                {
					Projectile.frame = 1;

				}
			}
			if (player.dead)
			{
				modPlayer.BubbaPet = false;
			}
			if (modPlayer.BubbaPet)
			{
				Projectile.timeLeft = 2;
			}
		}
	}
	
}