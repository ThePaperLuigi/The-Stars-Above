using Microsoft.Xna.Framework;
using StarsAbove.NPCs.Vagrant;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Vagrant
{
    public class VagrantSlamSprite : ModProjectile
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
		int fadeIn = 0;
		bool effectUsed = false;
		float projectileVelocity = 15;

		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		// It appears that for this AI, only the ai0 field is used!
		
		public override void AI() {
			DrawOriginOffsetY = -38;
			DrawOffsetX = -20;
			timer++;
			
			fadeIn += 5;
			if (timer == 0)
            {

            }
			
			if (timer >= 60)
			{




			}
			else
			{
				Projectile.alpha -= 5;

				
			}
			if (projectileVelocity < 0)
            {
				projectileVelocity = 0;
            }
			
			
			
			Projectile.alpha--;
			if (++Projectile.frameCounter >= 8)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 5)
				{

					Projectile.frame = 5;

				}

			}
			if(Projectile.frame == 3 && !effectUsed)
            {
				effectUsed = true;
				for (int i = 0; i < 10; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y + 24), 0, 0, DustID.Smoke, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 0), 100, default(Color), 2f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}
				for (int g = 0; g < 4; g++)
				{
					int goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
					goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
					goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
					goreIndex = Gore.NewGore(null, new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) + 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				}
			}
			if (Projectile.frame == 5)
			{
				Projectile.alpha += 50;
			}
			if (Projectile.alpha >= 250)
			{
				Projectile.Kill();
			}
			
			
			Projectile.position += Projectile.velocity * movementFactor;

			for (int i = 0; i < Main.maxNPCs; i++)//The sprite will always face what the boss is facing.
			{
				NPC other = Main.npc[i];

				if (other.active && other.type == ModContent.NPCType<VagrantBoss>())
				{
					Projectile.position = other.position;
					Projectile.direction = (Main.player[other.target].Center.X < Projectile.Center.X).ToDirectionInt();
					Projectile.spriteDirection = Projectile.direction;
					return;
				}
			}

			if (Projectile.spriteDirection == -1) {
				//projectile.rotation -= MathHelper.ToRadians(90f);
			}

		}

       
    }
}
