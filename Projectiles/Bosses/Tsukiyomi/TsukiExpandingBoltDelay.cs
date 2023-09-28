using Microsoft.Xna.Framework;
using StarsAbove.NPCs.Nalhaun;
using StarsAbove.NPCs.Tsukiyomi;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Tsukiyomi
{
    public class TsukiExpandingBoltDelay : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Starmatter");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}

		public override void SetDefaults() {
			Projectile.width = 42;               //The width of projectile hitbox
			Projectile.height = 42;              //The height of projectile hitbox
			Projectile.aiStyle = -1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false;         //Can the projectile deal damage to enemies?
			Projectile.hostile = true;         //Can the projectile deal damage to the player?
			//projectile.minion = true;           //Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 99;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 680;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}
		public override bool PreDraw(ref Color lightColor)
		{
			if (Projectile.ai[0] > 0)
			{
				
				return false;
			}
			default(Effects.BlueTrail).Draw(Projectile);

			return true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity) {
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 5 times
			
			return false;
		}
		float spawnProgress;
		bool firstSpawn = true;
		double deg;
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			//Orbit Nalhaun. ai[0] is the max orbit distance, ai[1] is the rotation starting position

			if (firstSpawn)
			{
				
				//Projectile.ai[1] = 90;

				firstSpawn = false;
			}
			if (!NPC.AnyNPCs(ModContent.NPCType<TsukiyomiBoss>()))
			{

				Projectile.Kill();
			}

			Projectile.ai[0]--;
			if(Projectile.ai[0] > 0)
            {
				Projectile.alpha = 255;
				return;
			}
			Projectile.alpha -= 5;
			if (spawnProgress < 0.2)
            {
				spawnProgress += 0.001f;

			}
			else
            {
				spawnProgress += 0.005f;

			}
			spawnProgress = Math.Clamp(spawnProgress, 0, 1f);
			if (spawnProgress >= 1f)
			{
				Projectile.Kill();
			}
			

			deg = Projectile.ai[1];
			Projectile.ai[1] += 0.2f;

			//deg = 10;

			double rad = deg * (Math.PI / 180);
			double dist = MathHelper.Lerp(10, 1200, EaseHelper.InOutQuad(spawnProgress));

			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC other = Main.npc[i];

				if (other.active && (other.type == ModContent.NPCType<TsukiyomiBoss>()))

				{
					Projectile.position.X = other.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
					Projectile.position.Y = other.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
					Projectile.rotation = Vector2.Normalize(other.Center - Projectile.Center).ToRotation() + MathHelper.ToRadians(0f);
					return;
				}
			}
		}

		public override void OnKill(int timeLeft)
		{
			

		}
		
	}
}
