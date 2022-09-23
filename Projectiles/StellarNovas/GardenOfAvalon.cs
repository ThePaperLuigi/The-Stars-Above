using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
namespace StarsAbove.Projectiles.StellarNovas
{
    public class GardenOfAvalon : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("The Garden of Avalon");     //The English name of the projectile
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
			Projectile.timeLeft = 400;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.light = 1f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.damage = 0;
			Projectile.hide = true;
			Projectile.alpha = 120;
			Projectile.localNPCHitCooldown = 600;
			Projectile.netUpdate = true;
		}
       
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{
			// Add this projectile to the list of projectiles that will be drawn BEFORE tiles and NPC are drawn. This makes the projectile appear to be BEHIND the tiles and NPC.
			behindNPCsAndTiles.Add(index);
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
			
			if (pulseTimer < 20)
			{

				//spin += 0.9468f;
				// 
			}
			if (pulseTimer > 20 && pulseTimer < 40)
			{
				
					
				

					
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
				for (int i = 0; i < 150; i++)
				{
					Vector2 vector = new Vector2(
						Main.rand.Next(-2048, 2048) * (0.003f * 300) - 10,
						Main.rand.Next(-2048, 2048) * (0.003f * 300) - 10);
					Dust d = Main.dust[Dust.NewDust(
						Projectile.Center + vector, 1, 1,
						45, 0, 0, 255,
						new Color(0.8f, 0.4f, 1f), 1.5f)];
					d.velocity = -vector / 16;
					d.velocity -= Projectile.velocity / 8;
					d.noLight = true;
					d.noGravity = true;

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
