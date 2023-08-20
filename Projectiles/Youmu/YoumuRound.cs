using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using System;

namespace StarsAbove.Projectiles.Youmu
{
    public class YoumuRound : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Konpaku Katana");     //The English name of the projectile
			Main.projFrames[Projectile.type] = 4;

		}

		public override void SetDefaults() {
			Projectile.width = 8;               //The width of projectile hitbox
			Projectile.height = 8;              //The height of projectile hitbox
			Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 120;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
			Projectile.minion = true;
			AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}
        public override Color? GetAlpha(Color lightColor)
        {
			//Fullbright projectile
			return Color.White;
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
			Player projOwner = Main.player[Projectile.owner];

			projOwner.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, (projOwner.Center -
				new Vector2(Projectile.Center.X + (projOwner.velocity.X * 0.05f), Projectile.Center.Y + (projOwner.velocity.Y * 0.05f))
				).ToRotation() + MathHelper.PiOver2);
			Projectile.alpha -= 40;


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
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			float dustAmount = 12f;
			float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
			for (int i = 0; (float)i < dustAmount; i++)
			{
				Vector2 spinningpoint5 = Vector2.UnitX * 0f;
				spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(15f, 1f);
				spinningpoint5 = spinningpoint5.RotatedBy(target.velocity.ToRotation() + randomConstant);
				int dust = Dust.NewDust(target.Center, 0, 0, DustID.GemDiamond);
				Main.dust[dust].scale = 1.5f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].position = target.Center + spinningpoint5;
				Main.dust[dust].velocity = target.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 3f;
			}
			base.OnHitNPC(target, hit, damageDone);
        }
        public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

		}
	}
}
