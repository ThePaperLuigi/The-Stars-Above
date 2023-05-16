
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Nalhaun
{
    public class NalhaunCleave : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Rend");
			
		}

		public override void SetDefaults() {
			Projectile.width = 600;
			Projectile.height = 800;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 10;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.damage = 200;
			Projectile.penetrate = -1;
			Projectile.hide = true;
			Projectile.hostile = true;
			Projectile.tileCollide = false;

		}
		public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{

			behindNPCsAndTiles.Add(index);

		}

		public override void AI() {

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;

			/*
			for (int d = 0; d < 130; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 0, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-100, 100), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 144; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 0, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-150, 150), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 126; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 133, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-160, 160), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 30; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 7, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-130, 130), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 40; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 269, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-130, 130), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 50; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, 78, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-150, 150), 150, default(Color), 1.5f);
			}
			*/
			Projectile.alpha -= 10;
				if (Projectile.alpha < 200)
				{
					Projectile.alpha = 200;
				}

			
		}
	}
}
