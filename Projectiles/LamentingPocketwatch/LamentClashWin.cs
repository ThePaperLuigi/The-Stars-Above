using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.LamentingPocketwatch
{
    //
    public class LamentClashWin : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 10;
		}
		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.aiStyle = 0;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.hide = true;
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
		}

		
		public override void AI()
		{
			if (++Projectile.frameCounter >= 2)
			{
				Projectile.frameCounter = 0;
				if (Projectile.frame < 10)
				{
					Projectile.frame++;
				}
				else
				{
					
				}

			}
			
		}
    }
}