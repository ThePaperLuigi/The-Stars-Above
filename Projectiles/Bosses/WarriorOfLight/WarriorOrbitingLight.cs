using Microsoft.Xna.Framework;
using StarsAbove.NPCs.Nalhaun;
using StarsAbove.NPCs.WarriorOfLight;
using StarsAbove.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.WarriorOfLight
{
    public class WarriorOrbitingLight : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Heatbolt");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 50;    //The length of old position to be recorded
			Main.projFrames[Projectile.type] = 11;
		}

		public override void SetDefaults() {
			Projectile.width = 22;               //The width of projectile hitbox
			Projectile.height = 22;              //The height of projectile hitbox
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
		}
		public override bool PreDraw(ref Color lightColor)
		{
			default(Effects.YellowTrail).Draw(Projectile);

			return true;
		}
		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			Projectile.timeLeft = 50;


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
		{//Ai 2 is the rotation value (usually 1)
			Player player = Main.player[Projectile.owner];

			//Orbit Nalhaun. ai[0] is the max orbit distance, ai[1] is the rotation starting position

			if (firstSpawn)
			{
				
				//Projectile.ai[1] = 90;

				firstSpawn = false;
			}
			if (!NPC.AnyNPCs(ModContent.NPCType<WarriorOfLightBoss>()) && !NPC.AnyNPCs(ModContent.NPCType<WarriorOfLightBossFinalPhase>()))
			{

				Projectile.Kill();
			}
			if (spawnProgress < 0.8)
			{
				Projectile.hostile = false;
				spawnProgress += 0.03f;

			}
			else
			{
				
				spawnProgress += 0.01f;
				//Projectile.alpha += 35;
			}
			spawnProgress = Math.Clamp(spawnProgress, 0, 1f);
			if (spawnProgress >= 1f)
            {
				Projectile.hostile = true;
			}

			Lighting.AddLight(Projectile.Center, new Vector3(0.99f, 0.6f, 0.3f));
			if (++Projectile.frameCounter >= 4)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 11 && Projectile.timeLeft < 50)
				{
					//Main.PlaySound(SoundID.Drip, projectile.Center, 0);
					Projectile.Kill();

				}
				if (++Projectile.frame >= 4 && Projectile.timeLeft > 50)
				{
					Projectile.frame = 0;

				}

			}

			deg = Projectile.ai[1];
			
			Projectile.ai[1] += Projectile.ai[2];

			//deg = 10;

			double rad = deg * (Math.PI / 180);
			double dist = MathHelper.Lerp(1200, Projectile.ai[0], EaseHelper.InOutQuad(spawnProgress));

			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC other = Main.npc[i];

				if (other.active && (other.type == ModContent.NPCType<WarriorOfLightBoss>() || other.type == ModContent.NPCType<WarriorOfLightBossFinalPhase>()))

				{
					Projectile.position.X = other.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
					Projectile.position.Y = other.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
					Projectile.rotation = Vector2.Normalize(other.Center - Projectile.Center).ToRotation() + MathHelper.ToRadians(0f);
					return;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			//Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			for (int d = 0; d < 12; d++)
			{
				Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default(Color), 0.5f);
			}
			
		}
		
	}
}
