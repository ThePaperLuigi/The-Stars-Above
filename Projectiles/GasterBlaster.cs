
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;

namespace StarsAbove.Projectiles
{
    public class GasterBlaster : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Gaster Blaster");
			Main.projFrames[Projectile.type] = 2;
			DrawOriginOffsetY = -9;
			DrawOffsetX = -7;
		}

		public override void SetDefaults() {
			Projectile.width = 120;
			Projectile.height = 100;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 200;
			Projectile.penetrate = -1;
			Projectile.scale = 1f;
			Projectile.alpha = 0;
			Projectile.penetrate = -1;
			Projectile.hostile = true;
			Projectile.friendly = false;


		}
		int startRotation = 54;
		int spinVelocity = 40;
		Vector2 leaveVelocity = new Vector2(0,0);
		Vector2 enterVelocity = new Vector2(0, -20);
		// In here the AI uses this example, to make the code more organized and readable
		// Also showcased in ExampleJavelinProjectile.cs
		public float movementFactor // Change this value to alter how fast the spear moves
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		
		public override void AI() {

			if(Projectile.timeLeft == 200)
            {
				SoundEngine.PlaySound(StarsAboveAudio.SFX_BlasterPrep, Projectile.Center);
				Projectile.rotation += MathHelper.ToRadians(Projectile.ai[1]);
				
            }
			Projectile.velocity = enterVelocity;
			enterVelocity.Y++;
			if(enterVelocity.Y > 0)
            {
				enterVelocity.Y = 0;
            }


			if (Projectile.ai[1] > 0)
            {
				Projectile.rotation += MathHelper.ToRadians(Projectile.ai[1]);
				Projectile.ai[1]--;
			}
			
			
			Projectile.ai[0]++;

			Player projOwner = Main.player[Projectile.owner];
			float Speed = 60f;
			if (Projectile.ai[0] >= 3)
            {
				
				Projectile.ai[0] = 0;
            }
			// Fade in
			if(Projectile.timeLeft == 100)
            {
				SoundEngine.PlaySound(StarsAboveAudio.SFX_BlasterFire, Projectile.Center);
			}
			if(Projectile.timeLeft <= 100 && Projectile.timeLeft > 30)
            {
				Projectile.frame = 1;
				if (Projectile.ai[0] >= 1)
				{
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, (float)((Math.Cos(Projectile.rotation) * Speed) * -1), (float)((Math.Sin(Projectile.rotation) * Speed) * -1), Mod.Find<ModProjectile>("Blaster").Type, 80, 0f, 0);
					Projectile.ai[0] = 0;
				}
				
			}
			if(Projectile.timeLeft < 30)
            {
				Projectile.velocity = leaveVelocity;
				leaveVelocity.Y++;
            }
			
			//projectile.scale+= 0.001f;
			
			if (Main.rand.NextBool(5))
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, 20,
					Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 269, Scale: 1.2f);
				dust.velocity += Projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
		}
		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			
			// Large Smoke Gore spawn
			for (int g = 0; g < 2; g++)
			{
				int goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
			}
			// reset size to normal width and height.
			

		}
	}
}
