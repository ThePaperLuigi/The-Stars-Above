using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Nalhaun
{
    public class BossLaevateinnDamage : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Ars Laevateinn");
			
		}

		public override void SetDefaults() {
			Projectile.width = 1450;//1320
			Projectile.height = 1450;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 10;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 255;
			Projectile.penetrate = -1;
			Projectile.hostile =  true;
			Projectile.friendly = false;
			Projectile.damage = 200;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.netUpdate = true;


		}

		public override bool CanHitPlayer(Player target)
		{
			//Within the arena.
			if (target.Distance(Projectile.Center) < 1500)
			{
				if (target.Distance(Projectile.Center) < 450)
				{
					return true;
				}
			}



			return false;
		}

		public override void AI() {

			
			
			
		}
	}
}
