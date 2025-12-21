using Microsoft.Xna.Framework;
using StarsAbove.Buffs.StellarNovas;
using StarsAbove.Effects;
using StarsAbove.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles.StellarNovas
{
    public class UBWBladeFollowUp : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Sunset of the Sun God");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 70;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;        //The recording mode
			Main.projFrames[Projectile.type] = 14;

		}

		public override void SetDefaults()
		{
			Projectile.width = 32;               //The width of projectile hitbox
			Projectile.height = 32;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			//Projectile.DamageType = ModContent.GetInstance<Systems.CelestialDamageClass>();
			Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 240;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 1f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame

			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}
		bool firedProjectile = false;
		float rotationStrength = 12f;
		bool firstSpawn = true;
		double deg;
		bool spawnDust = false;
		public override void AI()
		{
			//DrawOriginOffsetY = -60;
			Player player = Main.player[Projectile.owner];

			if (firstSpawn)
			{
				float dustAmount = 10f;
				for (int i = 0; (float)i < dustAmount; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
					int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemTopaz);
					Main.dust[dust].scale = 2f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = Projectile.Center + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 5f;
				}
				Projectile.localAI[0] += Main.rand.Next(-10, 10);
				Projectile.scale = 1f;
				Projectile.frame = Main.rand.Next(0, 14);
				firstSpawn = false;
			}
			if (player.dead && !player.active)
			{
				Projectile.Kill();
			}
			Projectile.localAI[0]++;

			if (Projectile.localAI[0] > 60)
			{
				Projectile.friendly = true;
				//Shoot towards the mouse cursor
				if (!firedProjectile)
				{
					Projectile.velocity = (player.GetModPlayer<StarsAbovePlayer>().playerMousePos - Projectile.Center).SafeNormalize(Vector2.Zero) * 30f;

					firedProjectile = true;
				}
				float dustAmount = 2f;

				for (int i = 0; (float)i < dustAmount; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(2f, 2f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.rotation);
					int dust = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.GemTopaz);
					Main.dust[dust].scale = 1f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = Projectile.Center + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 2f;
				}
			}
			else
			{
				int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemTopaz, 0f, 0f, 150, default(Color), 1f);
				Main.dust[dust].noGravity = true;

				Projectile.friendly = false;
				if(Projectile.ai[0] == 1)
                {
					deg = Projectile.ai[1] -= rotationStrength;


				}
				else
                {
					deg = Projectile.ai[1] += rotationStrength;


				}
				rotationStrength *= 0.97f;
				double rad = deg * (Math.PI / 180);
				double dist = 100 + Projectile.ai[2];

				/*Position the player based on where the player is, the Sin/Cos of the angle times the /
				/distance for the desired distance away from the player minus the projectile's width   /
				/and height divided by two so the center of the projectile is at the right place.     */
				//Projectile.position.X = player.GetModPlayer<StarsAbovePlayer>().playerMousePos.X - (int)(Math.Cos(rad) * dist) - Projectile.width/2;
				//Projectile.position.Y = player.GetModPlayer<StarsAbovePlayer>().playerMousePos.Y - (int)(Math.Sin(rad) * dist) - Projectile.height/2;
				Projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
				Projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

				//Projectile.rotation = (Main.player[Projectile.owner].Center - Projectile.Center).SafeNormalize(Vector2.Zero).ToRotation() + MathHelper.ToRadians(0f);
				//Projectile.rotation = MathHelper.ToRadians(90f);
				Projectile.rotation = Vector2.Normalize(player.GetModPlayer<StarsAbovePlayer>().playerMousePos - Projectile.Center).ToRotation() + MathHelper.ToRadians(45f);

			}

			if (!spawnDust)
			{
				float dustAmount = 13f;

				for (int i = 0; (float)i < dustAmount; i++)
				{
					Vector2 spinningpoint5 = Vector2.UnitX * 0f;
					spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(2f, 30f);
					spinningpoint5 = spinningpoint5.RotatedBy(Projectile.rotation);
					int dust = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y + 120), 0, 0, DustID.GemTopaz);
					Main.dust[dust].scale = 1f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = Projectile.Center + spinningpoint5;
					Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
				}
				/*for (int ir = 0; ir < 30; ir++)
				{
					Vector2 positionNew = Vector2.Lerp(player.Center, Projectile.Center, (float)ir / 30);

					Dust da = Dust.NewDustPerfect(positionNew, DustID.GemTopaz, null, 240, default(Color), 1f);
					da.fadeIn = 0.3f;
					da.noLight = true;
					da.noGravity = true;

				}*/
				spawnDust = true;
			}

		}

		public override bool PreDraw(ref Color lightColor)
		{


			return true;
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			OnHitDust(target);
			target.AddBuff(BuffType<BladeWorksDamageReduction>(), 60 * 20);
			Player projOwner = Main.player[Projectile.owner];
			projOwner.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
			SoundEngine.PlaySound(StarsAboveAudio.SFX_ScytheImpact, target.Center);
			//inflict debuff that reduces damage dealt
		}

		private void OnHitDust(NPC target)
		{
			
			float dustAmount = 33f;
			float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
			for (int i = 0; (float)i < dustAmount; i++)
			{
				Vector2 spinningpoint5 = Vector2.UnitX * 0f;
				spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(25f, 1f);
				spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant);
				int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemTopaz);
				Main.dust[dust].scale = 2f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].position = target.Center + spinningpoint5;
				Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
			}
			for (int i = 0; (float)i < dustAmount; i++)
			{
				Vector2 spinningpoint5 = Vector2.UnitX * 0f;
				spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(25f, 1f);
				spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant + MathHelper.ToRadians(90));
				int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemTopaz);
				Main.dust[dust].scale = 2f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].position = target.Center + spinningpoint5;
				Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * Main.rand.NextFloat(6, 32);
			}
			for (int d = 0; d < 10; d++)
			{
				Dust.NewDust(target.Center, target.width, target.height, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1f);
			}
			if(Main.rand.NextBool(25))
            {
				
				for (int g = 0; g < 4; g++)
				{
					int goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
					goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
					goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
					goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				}
			}
			
		}
	}
}
