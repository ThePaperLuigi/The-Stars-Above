
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Tsukiyomi
{
    public class TsukiBloodshedSheathe : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("The Only Thing I Know For Real");
			Main.projFrames[Projectile.type] = 13;

			//DrawOriginOffsetY = -40;
		}

		public override void SetDefaults() {
			Projectile.width = 200;
			Projectile.height = 200;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.timeLeft = 255;
			Projectile.hide = false;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
		}

		

		// It appears that for this AI, only the ai0 field is used!
		public override void AI() {

			DrawOffsetX = -30;
			DrawOriginOffsetY = 64;

			// Since we access the owner player instance so much, it's useful to create a helper local variable for this
			// Sadly, Projectile/ModProjectile does not have its own
			Player projOwner = Main.player[Projectile.owner];
			
			Projectile.timeLeft = 10;
			
			
			//Projectile.alpha -= 255;


			Projectile.ai[0]--;
			if(Projectile.ai[0] < 0)
            {
				Projectile.Kill();
            }
			
			if (++Projectile.frameCounter >= 5)
			{
				Projectile.frameCounter = 0;
				if (Projectile.frame < 12)
				{
					
					//Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/electroSmack"));
					Projectile.frame++;
				}
				else
				{
					
					
				}

			}

							
		}
	}
}
