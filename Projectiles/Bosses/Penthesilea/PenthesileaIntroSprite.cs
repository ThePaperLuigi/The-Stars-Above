using Microsoft.Xna.Framework;
using StarsAbove.NPCs.Vagrant;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Penthesilea
{
    public class PenthesileaIntroSprite : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("The Vagrant of Space and Time");
			Main.projFrames[Projectile.type] = 6;
		}

		public override void SetDefaults() {
			Projectile.width = 200;
			Projectile.height = 200;
			Projectile.aiStyle = 0;
			Projectile.penetrate = -1;
			Projectile.scale = 1;
			Projectile.alpha = 0;
			Projectile.damage = 0;
			Projectile.hide = false;
			Projectile.ownerHitCheck = true;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			
		}
		int timer;
		
		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		// It appears that for this AI, only the ai0 field is used!
		
		public override void AI() {
			DrawOriginOffsetY = 51;
			DrawOffsetX = -18;
			timer++;
			
			Projectile.alpha--;
			if (++Projectile.frameCounter >= 8)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 5)
				{

					Projectile.frame = 5;

				}

			}
			
			if (Projectile.frame == 5)
			{
                Projectile.Kill();
                float dustAmount = 50f;
                for (int i = 0; (float)i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                    spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                    int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmethyst);
                    Main.dust[dust].scale = 2f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = new Vector2(Projectile.Center.X, Projectile.Center.Y + 51) + spinningpoint5;
                    Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 20f;
                }
            }
            if (Projectile.alpha >= 250)
			{
			}
			
			
			if (Projectile.spriteDirection == -1) {
				//projectile.rotation -= MathHelper.ToRadians(90f);
			}

		}

       
    }
}
