using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;

namespace StarsAbove.Projectiles.StellarNovas.GuardiansLight
{
    public class Threadling : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Takonomicon");     //The English name of the projectile
			Main.projFrames[Projectile.type] = 4;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}

		public override void SetDefaults() {
			Projectile.width = 20;               //The width of projectile hitbox
			Projectile.height = 20;              //The height of projectile hitbox
			Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 120;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = true;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.minion = true;
			AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
			Projectile.DamageType = DamageClass.Summon;

		}
		public override bool PreDraw(ref Color lightColor)
		{
			default(Effects.GreenTrail).Draw(Projectile);

			return true;
		}
		public override void AI()
		{

			if (++Projectile.frameCounter >= 6)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 4)
				{

					Projectile.frame = 0;

				}

			}

			if (Main.rand.NextBool(3))
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.FireworkFountain_Green,
					Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 0.6f);
				dust.noGravity = true;
				dust.velocity += Projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
			Projectile.velocity.Y += 0.35f;

			if (Projectile.timeLeft < 60)
			{
				Projectile.tileCollide = false;
				Projectile.friendly = true;

				float maxDetectRadius = 800f; // The maximum radius at which a projectile can detect a target
				float projSpeed = 35f; // The speed at which the projectile moves towards the target

				// Trying to find NPC closest to the projectile
				NPC closestNPC = FindClosestNPC(maxDetectRadius);
				if (closestNPC == null)
					return;

				// If found, change the velocity of the projectile and turn it in the direction of the target
				// Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
				Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
				Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
			}
			else
            {
				Projectile.friendly = false;
			}

			


			base.AI();

        }
		public NPC FindClosestNPC(float maxDetectDistance)
		{
			NPC closestNPC = null;

			// Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

			// Loop through all NPCs(max always 200)
			for (int k = 0; k < Main.maxNPCs; k++)
			{
				NPC target = Main.npc[k];
				// Check if NPC able to be targeted. It means that NPC is
				// 1. active (alive)
				// 2. chaseable (e.g. not a cultist archer)
				// 3. max life bigger than 5 (e.g. not a critter)
				// 4. can take damage (e.g. moonlord core after all it's parts are downed)
				// 5. hostile (!friendly)
				// 6. not immortal (e.g. not a target dummy)
				if (target.CanBeChasedBy())
				{
					// The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
					float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

					// Check if it is within the radius
					if (sqrDistanceToTarget < sqrMaxDetectDistance)
					{
						sqrMaxDetectDistance = sqrDistanceToTarget;
						closestNPC = target;
					}
				}
			}

			return closestNPC;
		}

		public override bool OnTileCollide(Vector2 oldVelocity) {
			
			return false;
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			Player projOwner = Main.player[Projectile.owner];
			projOwner.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -95;

			for (int d = 0; d < 8; d++)
			{
				Dust.NewDust(Projectile.Center, 0, 0, DustID.GemEmerald, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default(Color), 0.5f);
				Dust.NewDust(Projectile.Center, 0, 0, DustID.FireworkFountain_Green, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default(Color), 0.4f);

			}
			float dustAmount = 33f;
			float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
			for (int i = 0; (float)i < dustAmount; i++)
			{
				Vector2 spinningpoint5 = Vector2.UnitX * 0f;
				spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(25f, 1f);
				spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant);
				int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemEmerald);
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
				int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemEmerald);
				Main.dust[dust].scale = 2f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].position = target.Center + spinningpoint5;
				Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * Main.rand.NextFloat(6, 32);
			}
			
		}

        public override void Kill(int timeLeft)
		{
			for (int d = 0; d < 12; d++)
			{
				Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Green, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default(Color), 0.5f);
			}
		}
	}
}
