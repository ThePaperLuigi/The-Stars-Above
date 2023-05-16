using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
namespace StarsAbove.Projectiles.StellarNovas
{
    public class Laevateinn : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Ars Laevateinn");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 15;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 1;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 610;               //The width of projectile hitbox
			Projectile.height = 1320;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			// projectile.ranged = false /* tModPorter - this is redundant, for more info see https://github.com/tModLoader/tModLoader/wiki/Update-Migration-Guide#damage-classes */ ;           //Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 999;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 700;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.light = 1f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.damage = 1;
			Projectile.alpha = 120;
			Projectile.localNPCHitCooldown = 600;
			Projectile.netUpdate = true;
		}
		float spin = 0;
		int spinCount = 0;

		int pulseTimer;
		int pulseTimerReverse = 100;
		public override void AI()
		{

			Projectile.damage = 0;
			Player projOwner = Main.player[Projectile.owner];
			if(Projectile.ai[0] == 0)
			{


				//projectile.rotation += MathHelper.ToRadians(120);

				Projectile.velocity = new Vector2(0, 100);
			}
			Projectile.ai[0]++;
			
			pulseTimer++;
			pulseTimerReverse--;
			Projectile.rotation += MathHelper.ToRadians(spin);
			if(pulseTimer==5 && spinCount > 0 && spinCount < 6)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center, Vector2.Zero, ProjectileType<LaevateinnDamage>(), 1, 0f, Projectile.owner, 0f, 0f);
				projOwner.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
				SoundEngine.PlaySound(StarsAboveAudio.SFX_GunbladeImpact, Projectile.Center);
				for (int d = 0; d < 130; d++)
				{
					Dust.NewDust(Projectile.Center, 0, 0, 0, 0f + Main.rand.Next(-30, 30), 0f + Main.rand.Next(-30, 30), 150, default(Color), 1.5f);
				}
				for (int d = 0; d < 144; d++)
				{
					Dust.NewDust(Projectile.Center, 0, 0, 0, 0f + Main.rand.Next(-35, 35), 0f + Main.rand.Next(-35, 35), 150, default(Color), 1.5f);
				}
				for (int d = 0; d < 126; d++)
				{
					Dust.NewDust(Projectile.Center, 0, 0, 133, 0f + Main.rand.Next(-36, 36), 0f + Main.rand.Next(-36, 36), 150, default(Color), 1.5f);
				}
				for (int d = 0; d < 130; d++)
				{
					Dust.NewDust(Projectile.Center, 0, 0, 7, 0f + Main.rand.Next(-33, 33), 0f + Main.rand.Next(-33, 33), 150, default(Color), 1.5f);
				}
				for (int d = 0; d < 140; d++)
				{
					Dust.NewDust(Projectile.Center, 0, 0, 269, 0f + Main.rand.Next(-33, 33), 0f + Main.rand.Next(-33, 33), 150, default(Color), 1.5f);
				}
				for (int d = 0; d < 150; d++)
				{
					Dust.NewDust(Projectile.Center, 0, 0, 78, 0f + Main.rand.Next(-35, 35), 0f + Main.rand.Next(-35, 35), 150, default(Color), 1.5f);
				}
				// Smoke Dust spawn
				for (int i = 0; i < 70; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-36, 36), 0f + Main.rand.Next(-36, 36), 100, default(Color), 2f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}
				// Fire Dust spawn
				for (int i = 0; i < 80; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-36, 36), 0f + Main.rand.Next(-36, 36), 100, default(Color), 3f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity *= 5f;
					dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-36, 36), 0f + Main.rand.Next(-36, 36), 100, default(Color), 2f);
					Main.dust[dustIndex].velocity *= 3f;
				}
				// Large Smoke Gore spawn
				for (int g = 0; g < 7; g++)
				{
					int goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 4.5f + Main.rand.Next(-18, 18);
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 4.5f + Main.rand.Next(-18, 18);
					goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 4.5f + Main.rand.Next(-18, 18);
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 4.5f + Main.rand.Next(-18, 18);
					goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 4.5f + Main.rand.Next(-18, 18);
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 4.5f + Main.rand.Next(-18, 18);
					goreIndex = Gore.NewGore(null,new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 4.5f + Main.rand.Next(-18, 18);
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 4.5f + Main.rand.Next(-18, 18);
				}

			}
			if (pulseTimer < 20)
			{

				//spin += 0.9468f;
				spin += 2.842f;
				// 
			}
			if (pulseTimer > 20 && pulseTimer < 40)
			{
				
					spin -= 2.842f;
				

					
			}
			if(pulseTimer >= 100)
			{
				pulseTimerReverse = 100;
				spinCount++;
				pulseTimer = 0;
			}
			if (Projectile.ai[0] <= 120)
			{
				
				Projectile.velocity *= 0.9f;

			}
			if (Projectile.timeLeft < 100)
			{
				
				Projectile.velocity = new Vector2(0, -35);
				Projectile.velocity *= 1.9f;
			}
			else
			{
				
					for (int d = 0; d < 1; d++)
					{
						Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, 204,
							Projectile.velocity.X * .2f + Main.rand.Next(-50, 50), Projectile.velocity.Y * .2f + Main.rand.Next(-50, 50), 130, Scale: 1.2f);
						dust.velocity += Projectile.velocity * 0.3f;
						dust.velocity *= 0.2f;
					}
					for (int d = 0; d < 1; d++)
					{
						Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, 55,
							Projectile.velocity.X * .2f + Main.rand.Next(-50, 50), Projectile.velocity.Y * .2f + Main.rand.Next(-50, 50), 130, Scale: 1.2f);
						dust.velocity += Projectile.velocity * 0.3f;
						dust.velocity *= 0.2f;
					}
					for (int d = 0; d < 1; d++)
					{
						Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, 90,
							Projectile.velocity.X * .2f + Main.rand.Next(-50, 50), Projectile.velocity.Y * .2f + Main.rand.Next(-50, 50), 130, Scale: 1.2f);
						dust.velocity += Projectile.velocity * 0.3f;
						dust.velocity *= 0.2f;
					}
					for (int i = 0; i < 30; i++)
					{//Circle
						Vector2 offset = new Vector2();
						double angle = Main.rand.NextDouble() * 2d * Math.PI;
						offset.X += (float)(Math.Sin(angle) * 650);
						offset.Y += (float)(Math.Cos(angle) * 650);

						Dust d = Dust.NewDustPerfect(Projectile.Center + offset, 90, Projectile.velocity, 200, default(Color), 0.7f);
						d.fadeIn = 1f;
						d.noGravity = true;
					}
				if (spinCount < 5)
				{
					for (int i = 0; i < 60; i++)
					{//Circle
						Vector2 offset = new Vector2();
						double angle = Main.rand.NextDouble() * 2d * Math.PI;
						offset.X += (float)(Math.Sin(angle) * pulseTimerReverse * 6.3);
						offset.Y += (float)(Math.Cos(angle) * pulseTimerReverse * 6.3);

						Dust d = Dust.NewDustPerfect(Projectile.Center + offset, 90, Projectile.velocity, 200, default(Color), 0.7f);
						d.fadeIn = 1f;
						d.noGravity = true;
					}
				}
			}
			base.AI();
		}
		public override bool OnTileCollide(Vector2 oldVelocity) {
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 5 times
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0) {
				Projectile.Kill();
			}
			else {
				Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
				if (Projectile.velocity.X != oldVelocity.X) {
					Projectile.velocity.X = -oldVelocity.X;
				}
				if (Projectile.velocity.Y != oldVelocity.Y) {
					Projectile.velocity.Y = -oldVelocity.Y;
				}
			}
			return false;
		}

		public override bool PreDraw(ref Color lightColor) {
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Width() * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++) {
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw((Texture2D)TextureAssets.Projectile[Projectile.type], drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}

		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			
			// Play explosion sound
			
			// Large Smoke Gore spawn
			

		}
	}
}
