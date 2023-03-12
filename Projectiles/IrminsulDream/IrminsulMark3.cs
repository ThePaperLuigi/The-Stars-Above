
using Microsoft.Xna.Framework;
using StarsAbove.Buffs.RedMage;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.IrminsulDream
{
    public class IrminsulMark3 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Irminsul's Dream");
		}

		public override void SetDefaults()
		{
			
			AIType = 0;
			Projectile.width = 34;
			Projectile.height = 34;
			Projectile.minion = false;
			Projectile.minionSlots = 0f;
			Projectile.timeLeft = 240;
			Projectile.penetrate = 999;
			Projectile.hide = false;
			Projectile.alpha = 255;
			Projectile.netImportant = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;

		}

        public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			//Projectile.scale = 0.7f;
			if (!player.GetModPlayer<WeaponPlayer>().IrminsulAttackActive)
            {
				Projectile.Kill();
            }
			if (player.dead && !player.active)
			{
				Projectile.Kill();
			}
			Projectile.timeLeft = 10;
			Projectile.alpha -= 10;

			Projectile.position.X = player.GetModPlayer<WeaponPlayer>().IrminsulBoxEnd.X - 17;
			Projectile.position.Y = player.GetModPlayer<WeaponPlayer>().IrminsulBoxStart.Y - 17;

			//This is 0 unless a auto attack has been initated, in which it'll tick up.


			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			if (Projectile.alpha > 255)
			{
				Projectile.alpha = 255;
			}

			
		}
		

		public override void Kill(int timeLeft)
		{
			

		}

	}
}
