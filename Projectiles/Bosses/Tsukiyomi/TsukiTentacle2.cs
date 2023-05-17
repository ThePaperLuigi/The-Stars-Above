
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Tsukiyomi
{
    public class TsukiTentacle2 : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Takonomicon");

		}

		public override void SetDefaults() {
			Projectile.width = 60;
			Projectile.height = 200;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.timeLeft = 150;
			Projectile.hide = false;
			Projectile.ownerHitCheck = false;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.hostile = true;

		}
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
			hitbox.Width /= 2;
			hitbox.Height /= 2;


            base.ModifyDamageHitbox(ref hitbox);
        }
        public override void AI() {
			
			Projectile.spriteDirection = Projectile.direction;


			if (Projectile.timeLeft < 145)
			{
				Projectile.velocity = Vector2.Zero;
			}
			else
            {
				Projectile.rotation = Projectile.ai[0];

			}



			if (Projectile.timeLeft < 50)
			{
				Projectile.alpha += 20;
			}
			else
			{

				Projectile.alpha -= 10;
				if (Projectile.alpha < 0)
				{
					Projectile.alpha = 0;
				}
			}
			if (Projectile.alpha >= 255)
			{
				Projectile.Kill();
			}


		}
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(target.Center, 0, 0, DustID.Shadowflame, Main.rand.NextFloat(-7, 7), Main.rand.NextFloat(-7, 7), 150, default(Color), 0.9f);

			}
			target.AddBuff(BuffID.ShadowFlame, 420);
			 
        }
      
	}
}
