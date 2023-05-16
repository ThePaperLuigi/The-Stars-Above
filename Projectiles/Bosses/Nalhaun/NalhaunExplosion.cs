using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Nalhaun
{
    //
    public class NalhaunExplosion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Apostate's Truth");
			Main.projFrames[Projectile.type] = 5;
		}
		public override void SetDefaults()
		{
			Projectile.width = 176;
			Projectile.height = 176;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;

			Projectile.hide = false;
			Projectile.ownerHitCheck = false;
			Projectile.aiStyle = -1;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.hostile = true;
			//Projectile.DamageType = DamageClass.Ranged;
			//AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
			

		}

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		
		// It appears that for this AI, only the ai0 field is used!
		public override void AI()
		{
			Lighting.AddLight(Projectile.Center, new Vector3(1f, 0.4f, 0.64f));
			
			Player projOwner = Main.player[Projectile.owner];
			if (Projectile.frame == 5)
			{
				
				Projectile.Kill();
			}
			if (++Projectile.frameCounter >= 3)
			{
				Projectile.frameCounter = 0;
				if (Projectile.frame < 5)
				{
					Projectile.frame++;
				}
				else
				{
					
				}

			}
			

			
		}
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
			damage = (int)(target.statLifeMax * 0.2);
            base.ModifyHitPlayer(target, ref damage, ref crit);
        }

    }
}