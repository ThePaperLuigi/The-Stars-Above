using Microsoft.Xna.Framework;
using StarsAbove.NPCs.Tsukiyomi;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Tsukiyomi
{
    public class TsukiTrackingBoltStrong : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Death In Four Acts");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 50;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}

		public override void SetDefaults() {
			Projectile.width = 22;               //The width of projectile hitbox
			Projectile.height = 22;              //The height of projectile hitbox
			Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false;         //Can the projectile deal damage to enemies?
			Projectile.hostile = true;         //Can the projectile deal damage to the player?
			//projectile.minion = true;           //Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 99;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 900;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
		}
		public override bool PreDraw(ref Color lightColor)
		{
			default(Effects.RedTrail).Draw(Projectile);

			return true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity) {
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 5 times
			
			return false;
		}

		public override void AI()
		{
			Projectile.ai[0]++;
			SearchForTargets(out bool foundTarget, out Vector2 targetCenter);
			if (Projectile.ai[0] == 60)
            {
				if (foundTarget)
				{
					
					Vector2 position = Projectile.Center;

					float launchSpeed = 12f;
					Vector2 direction = Vector2.Normalize(targetCenter - Projectile.Center);
					Projectile.velocity = direction * launchSpeed;
				}

			}
			else
            {
				
            }
			if(Projectile.ai[0] < 60)
            {
				Projectile.velocity *= 0.98f;
			}

			
		}
		private void SearchForTargets(out bool foundTarget, out Vector2 targetCenter)
		{


			targetCenter = Projectile.position;
			foundTarget = false;



			if (!foundTarget)
			{
				// This code is required either way, used for finding a target
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];

					if (npc.active && npc.type == ModContent.NPCType<TsukiyomiBoss>() || npc.type == ModContent.NPCType<TsukiyomiBoss>())//Change later
					{
						foundTarget = true;
						targetCenter = Main.player[npc.target].Center;
					}
				}
			}

			// friendly needs to be set to true so the minion can deal contact damage
			// friendly needs to be set to false so it doesn't damage things like target dummies while idling
			// Both things depend on if it has a target or not, so it's just one assignment here
			// You don't need this assignment if your minion is shooting things instead of dealing contact damage
			//Projectile.friendly = foundTarget;
		}
		public override void Kill(int timeLeft)
		{
			for (int d = 0; d < 12; d++)
			{
				Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default(Color), 1.5f);
			}


		}
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			target.AddBuff(BuffID.OnFire, 60);
            base.OnHitPlayer(target, damage, crit);
        }
    }
}
