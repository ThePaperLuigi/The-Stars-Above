using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace StarsAbove.Projectiles.StellarNovas
{
    public class Theofania2 : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Theofania Inanis");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 1;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 210;               //The width of projectile hitbox
			Projectile.height = 420;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			// projectile.ranged = false /* tModPorter - this is redundant, for more info see https://github.com/tModLoader/tModLoader/wiki/Update-Migration-Guide#damage-classes */ ;           //Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 999;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.light = 1f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.alpha = 120;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}
		float spin = 0;
		public override void AI()
		{
			
			
			Player projOwner = Main.player[Projectile.owner];
			if(Projectile.ai[0] == 0)
			{


				//projectile.rotation += MathHelper.ToRadians(120);

				Projectile.velocity = new Vector2(0, -40);
			}
			Projectile.ai[0]++;
			
			
			
			
			if (Projectile.ai[0] <= 120)
			{
				Projectile.rotation += MathHelper.ToRadians(spin);
				Projectile.alpha--;
				Projectile.damage = 0;
				Projectile.velocity *= 0.9f;
				
					spin+=0.4f;
				
				
				
			}
			if(Projectile.ai[0] == 121)
			{
				
				Projectile.velocity = Projectile.DirectionTo(projOwner.GetModPlayer<StarsAbovePlayer>().playerMousePos) * 4f;
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

			}	
			if(Projectile.ai[0] >= 122 && Projectile.ai[0] < 162)
			{
				//if (projectile.rotation <= projectile.velocity.ToRotation() + MathHelper.ToRadians(90f))
				//{
				//	projectile.rotation += 0.1f;
				//}
				//if (projectile.rotation >= projectile.velocity.ToRotation() + MathHelper.ToRadians(90f))
				//{
				//	projectile.rotation -= 0.1f;
				//}
				for (int d = 0; d < 1; d++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, 159,
						Projectile.velocity.X * .2f + Main.rand.Next(-10, 10), Projectile.velocity.Y * .2f + Main.rand.Next(-10, 10), 130, Scale: 1.2f);
					dust.velocity += Projectile.velocity * 0.3f;
					dust.velocity *= 0.2f;
				}
				for (int d = 0; d < 1; d++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, 112,
						Projectile.velocity.X * .2f + Main.rand.Next(-10, 10), Projectile.velocity.Y * .2f + Main.rand.Next(-10, 10), 130, Scale: 1.2f);
					dust.velocity += Projectile.velocity * 0.3f;
					dust.velocity *= 0.2f;
				}
				Projectile.velocity *= -1.1f;
				//projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(270f);
			}
			////if (projectile.ai[0] >= 153 && projectile.ai[0] < 170)
			//{
				
			//	projectile.position += projectile.velocity * -2;
				
				
				
				//projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(270f);
			//}
			if (Projectile.ai[0] >= 162)
			{
				Projectile.damage = 1;
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
				Projectile.velocity *= 1.1f;
				//projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
			}
			for (int d = 0; d < 1; d++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, 112,
					Projectile.velocity.X * .2f + Main.rand.Next(-40, 40), Projectile.velocity.Y * .2f + Main.rand.Next(-40, 40), 130, Scale: 1.2f);
				dust.velocity += Projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
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
