using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.EternalStar
{
    public class EmblazonedMalachite : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Malachite Star");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 270;               //The width of projectile hitbox
			Projectile.height = 270;              //The height of projectile hitbox
			Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Magic;           //Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 5;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 600;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 1f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}
		float rotationSpeed = 10f;
		public override void AI()
        {

			float rotationsPerSecond = rotationSpeed;
			rotationSpeed = 1f;
			
			bool rotateClockwise = true;
			//The rotation is set here
			int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 220, Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1), 100, default(Color), 1f);
			Main.dust[dustIndex].noGravity = true;
			Main.dust[dustIndex].velocity *= 5f;

			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);
			Projectile.alpha -= 10;

			base.AI();
        }
		public override bool PreDraw(ref Color lightColor)
		{
			Lighting.AddLight(new Vector2(Projectile.Center.X, Projectile.Center.Y), 81 * 0.001f, 194 * 0.001f, 58 * 0.001f);//Vanilla code adapted
			for (int i = 0; i < 1; i++)
			{
				int num7 = 16;
				float num9 = 6f;
				float num8 = (float)(Math.Cos((double)Main.GlobalTimeWrappedHourly % 2.40000009536743 / 2.40000009536743 * 6.28318548202515) / 5 + 0.5);
				float amount1 = 0.5f;
				float num10 = 0.0f;
				float addY = 0f;
				float addHeight = 0f;
				SpriteEffects spriteEffects = SpriteEffects.None;
				Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
				Vector2 vector2_3 = new Vector2((float)(Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Width() / 2), (float)(Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Height() / 1 / 2));
				Vector2 position1 = Projectile.Center - Main.screenPosition - new Vector2((float)texture.Width, (float)(texture.Height / 1)) * Projectile.scale / 2f + vector2_3 * Projectile.scale + new Vector2(0.0f, addY + addHeight + 0);
				Color color2 = new Color(255, 255, 255, 150);
				Rectangle r = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Frame(1, 1, 0, 0);
				for (int index2 = 0; index2 < num7; ++index2)
				{
					Color newColor2 = color2;
					Color color3 = Projectile.GetAlpha(newColor2) * (0.85f - num8);
					Vector2 position2 = new Vector2(Projectile.Center.X, Projectile.Center.Y) + ((float)((double)index2 / (double)num7 * 6.28318548202515) + Projectile.rotation + num10).ToRotationVector2() * (float)(4.0 * (double)num8 + 2.0) - Main.screenPosition - new Vector2((float)texture.Width, (float)(texture.Height / 1)) * Projectile.scale / 2f + vector2_3 * Projectile.scale + new Vector2(0.0f, addY + addHeight + 0);
					Main.spriteBatch.Draw((Texture2D)Terraria.GameContent.TextureAssets.Projectile[Projectile.type], position2, new Microsoft.Xna.Framework.Rectangle?(r), color3, Projectile.rotation, vector2_3, Projectile.scale * 1.05f, spriteEffects, 0.0f);
				}
			}
			return base.PreDraw(ref lightColor);
		}
		public override bool OnTileCollide(Vector2 oldVelocity) {
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 5 times
			
			return false;
		}

		

		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			//Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);

			// Play explosion sound
			SoundEngine.PlaySound(StarsAboveAudio.SFX_HolyStab, Projectile.Center);
			for (int i = 0; i < 10; i++)
			{
				if (Main.rand.Next(10) == 1)
				{
					//player.QuickSpawnProjectile(null,mod.ProjectileType("RedOrb"));
					//Main.PlaySound(SoundLoader.customSoundType, (int)target.Center.X, (int)target.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/StarbitCollected"));

					int k = Item.NewItem(null, (int)Projectile.Center.X + Main.rand.Next(-50, 50), (int)Projectile.Center.Y + Main.rand.Next(-50, 50), 0, 0, Mod.Find<ModItem>("MalachiteFragment").Type, 1, false);
					if (Main.netMode == 1)
					{
						NetMessage.SendData(21, -1, -1, null, k, 1f);
					}

				}
			}
			for (int i = 0; i < 70; i++)
			{
				int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 220, Main.rand.NextFloat(-25,25), Main.rand.NextFloat(-25, 25), 100, default(Color), 1f);
				Main.dust[dustIndex].noGravity = true;
				
				dustIndex = Dust.NewDust(Projectile.Center, 0, 0, 220, Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(-20, 20), 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 3f;
			}
			
			

		}
	}
}
