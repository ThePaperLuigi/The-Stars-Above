using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove;
using System;
using Terraria;using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Pets
{
	public class PyraPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			//
			//DrawOffsetX = -20;
			DisplayName.SetDefault("Pyra"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 10;
			
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

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
			
			
			if (player.dead)
			{
				modPlayer.PyraPet = false;
			}
			if (modPlayer.PyraPet)
			{
				Projectile.timeLeft = 2;
			}
		}

		
	}
	
}