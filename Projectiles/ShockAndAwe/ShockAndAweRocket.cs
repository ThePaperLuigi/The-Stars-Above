using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using static Terraria.ModLoader.ModContent;
using System;

namespace StarsAbove.Projectiles.ShockAndAwe
{
	public class ShockAndAweRocket : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 8;               //The width of projectile hitbox
			Projectile.height = 8;              //The height of projectile hitbox
			Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Ranged;           //Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 240;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0f;            //How much light emit around the projectile
			Projectile.ignoreWater = false;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = true;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}

		
        public override void AI()
        {
			DrawOffsetX = -10;
			int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 2f);
			Main.dust[dustIndex].noGravity = true;
		}
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
			if(target.velocity.Y != 0)
            {
				modifiers.SetCrit();
				float dustAmount = 40f;
				for (int i = 0; (float)i < dustAmount; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(24f, 1f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
					int dust = Dust.NewDust(target.Center, 0, 0, DustID.Flare);
					Main.dust[dust].scale = 2f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = target.Center + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 5f;
				}
				for (int i = 0; (float)i < dustAmount; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(24f, 1f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation() + MathHelper.ToRadians(90));
					int dust = Dust.NewDust(target.Center, 0, 0, DustID.Flare);
					Main.dust[dust].scale = 2f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = target.Center + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 5f;
				}
				for (int i = 0; (float)i < dustAmount; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
					int dust = Dust.NewDust(target.Center, 0, 0, DustID.Flare);
					Main.dust[dust].scale = 2f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = target.Center + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 5f;
				}
				for (int i = 0; (float)i < dustAmount; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
					int dust = Dust.NewDust(target.Center, 0, 0, DustID.Flare);
					Main.dust[dust].scale = 2f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = target.Center + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 9f;
				}
			}
            base.ModifyHitNPC(target, ref modifiers);
        }

        public override void Kill(int timeLeft)
		{
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

			if(Main.myPlayer == Projectile.owner)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileType<ShockAndAweRocketExplosion>(), Projectile.damage, 0, Projectile.owner);

			}
		}
	}
}
