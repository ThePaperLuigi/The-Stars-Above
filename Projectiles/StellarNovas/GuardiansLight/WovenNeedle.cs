using Microsoft.Xna.Framework;
using StarsAbove.NPCs.Tsukiyomi;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.StellarNovas.GuardiansLight
{
    public class WovenNeedle : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Death In Four Acts");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 50;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}

		public override void SetDefaults() {
			Projectile.width = 22;               //The width of projectile hitbox
			Projectile.height = 22;              //The height of projectile hitbox
			Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.minion = false;           //Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 900;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = true;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
		}
		public override bool PreDraw(ref Color lightColor)
		{
			default(Effects.GreenTrail).Draw(Projectile);

			return true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity) {
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 5 times
			
			return true;
		}

		public override void AI()
		{
			Projectile.ai[0]++;
			float projSpeed = 50f; // The speed at which the projectile moves towards the target

			if (Projectile.ai[0] == 65)
			{
				Projectile.velocity = (Main.MouseWorld - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
				Projectile.tileCollide = true;

			}

			Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.GemEmerald, null, 0, default, 1);
			d.noGravity = true;
			d.fadeIn = 0.1f;

			if (Projectile.ai[0] < 60)
            {
				Projectile.tileCollide = false;
				Projectile.velocity *= 0.97f;
			}

			
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
		public override void OnKill(int timeLeft)
		{
			Vector2 position = Main.rand.NextVector2FromRectangle(Projectile.Hitbox);
			ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
			particleOrchestraSettings.PositionInWorld = Projectile.Center;
			ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.TrueNightsEdge, particleOrchestraSettings, Projectile.owner);
			for (int d = 0; d < 12; d++)
			{
				Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Green, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default(Color), 0.6f);
			}
			if (Projectile.owner == Main.myPlayer)
			{
				Vector2 vel = new Vector2(Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-6, -12));
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vel, ModContent.ProjectileType<Threadling>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 1);

			}

		}
        
    }
}
