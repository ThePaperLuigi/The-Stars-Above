using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;

namespace StarsAbove.Projectiles.SunsetOfTheSunGod
{
    public class KarnaSpearThrow : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Sunset of the Sun God");     //The English name of the projectile
			Main.projFrames[Projectile.type] = 1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 240;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}

		public override void SetDefaults() {
			Projectile.width = 220;               //The width of projectile hitbox
			Projectile.height = 220;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 60;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.DamageType = DamageClass.Ranged;
			//AIType = ProjectileID.Bullet;           //Act exactly like default Bullet

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			default(Effects.RedTrail).Draw(Projectile);

			return true;
		}
		bool firstSpawn = true;
		public override void AI()
        {
			Player projOwner = Main.player[Projectile.owner];

			if (firstSpawn)
            {
				SoundEngine.PlaySound(SoundID.DD2_JavelinThrowersAttack, projOwner.position);


				//Projectile.velocity *= 0.72f;
				Projectile.timeLeft = 60;
				Projectile.scale = 1f;
				firstSpawn = false;
			}
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);

            Projectile.velocity *= 1.04f;
			if(Projectile.timeLeft > 50)
            {
				projOwner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (projOwner.Center - Projectile.Center).ToRotation() + MathHelper.PiOver2);

			}
			if (Projectile.timeLeft == 58)
            {
				Projectile.alpha = 0;
				float dustAmount = 66f;
				for (int i = 0; (float)i < dustAmount; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(1f, 4f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
					int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain);
					Main.dust[dust].scale = 2f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = Projectile.Center + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
				}
			}
			if (Projectile.timeLeft == 56)
			{
				float dustAmount = 36f;
				for (int i = 0; (float)i < dustAmount; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(1f, 8f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
					int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain);
					Main.dust[dust].scale = 1f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = Projectile.Center + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 5f;
				}
			}
			if (Projectile.timeLeft < 10)
            {
				Projectile.scale *= 0.985f;

			}
			

			base.AI();

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            KarnaOnHitDust(target);
            base.OnHitNPC(target, damage, knockback, crit);
        }

        private void KarnaOnHitDust(NPC target)
        {
            for (int d = 0; d < 23; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(6));
                float scale = -1f + (Main.rand.NextFloat() * 1.2f);
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(target.Center, 0, 0, DustID.FireworkFountain_Red, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 1f);
                Main.dust[dustIndex].noGravity = true;

            }
            float dustAmount = 33f;
            float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
            for (int i = 0; (float)i < dustAmount; i++)
            {
                Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(18f, 1f);
                spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant);
                int dust = Dust.NewDust(target.Center, 0, 0, DustID.LifeDrain);
                Main.dust[dust].scale = 2f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = target.Center + spinningpoint5;
                Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 4f;
            }
        }

        public override void Kill(int timeLeft)
		{
			for (int d = 0; d < 18; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmethyst, Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-6, 6), 150, default(Color), 0.5f);
				Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Pink, Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-6, 6), 150, default(Color), 0.4f);

			}
			if(Projectile.penetrate != 0)
            {
				for (int d = 0; d < 23; d++)//Visual effects
				{
					Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(6));
					float scale = 3f + (Main.rand.NextFloat() * 0.6f);
					perturbedSpeed = perturbedSpeed * scale;
					int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 0.6f);
					Main.dust[dustIndex].noGravity = true;

				}
				for (int d = 0; d < 23; d++)//Visual effects
				{
					Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(6));
					float scale = 3f + (Main.rand.NextFloat() * 0.6f);
					perturbedSpeed = -perturbedSpeed * scale;
					int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.LifeDrain, perturbedSpeed.X, perturbedSpeed.Y, 150, default(Color), 0.6f);
					Main.dust[dustIndex].noGravity = true;

				}
			}
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			//SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
		}
	}
}
