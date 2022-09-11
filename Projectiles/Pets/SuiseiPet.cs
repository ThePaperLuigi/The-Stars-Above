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
	public class SuiseiPet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			//
			//
			DisplayName.SetDefault("Suisei"); // Automatic from .lang files
			Main.projFrames[Projectile.type] = 13;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 70;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
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
				if (idleAnimation >= 80 && idleAnimation < 280)
				{
					Projectile.frame = 12;
				}
				if (idleAnimation >= 280 && idleAnimation < 290)
				{
					Projectile.frame = 11;
				}
				if (idleAnimation >= 290 && idleAnimation < 300)
				{
					Projectile.frame = 10;
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
				modPlayer.SuiseiPet = false;
			}
			if (modPlayer.SuiseiPet)
			{
				Projectile.timeLeft = 2;
			}
		}
		public override bool PreDraw(ref Color lightColor)
		{
			if(Projectile.velocity.Y != 0)
            {
				default(Effects.BlueTrail).Draw(Projectile);
			}
			

			return true;
		}

	}
	
}