using Microsoft.Xna.Framework;
using StarsAbove.NPCs.WarriorOfLight;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Projectiles
{
    public class WarriorVFX : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Warrior of Light's Aura");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults()
		{
			Projectile.width = 300;               //The width of projectile hitbox
			Projectile.height = 300;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
			Projectile.scale = 1f;
			Projectile.penetrate = -1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 190;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 1f;            //How much light emit around the projectile
			Projectile.hide = true;

			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
		}
		public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{

			behindNPCsAndTiles.Add(index);

		}
		float rotationSpeed = 10f;
		bool firstSpawn = true;
		float initialSpeed = 10f;
		int initialDistance = 200;
		bool isActive = true;
		int savedDamage;
		int respawnTimer;
		bool cosmicConceptionStart;

		int fadeAway = 0;

		public override void AI()
		{



			
			if (!NPC.AnyNPCs(NPCType<WarriorOfLightBoss>()))
			{

				Projectile.Kill();
			}
			Projectile.timeLeft = 10;

			float rotationsPerSecond = rotationSpeed;
			rotationSpeed = 0.4f;
			bool rotateClockwise = true;
			//The rotation is set here

			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				


				if (npc.active && npc.type == NPCType<WarriorOfLightBoss>())
				{
					Projectile.Center = npc.Center;
					if (npc.HasBuff(BuffType<Buffs.WarriorEnd>()))
                    {
						Projectile.Kill();
                    }
				}




			}

			Projectile.rotation += (rotateClockwise ? 1 : -1) * MathHelper.ToRadians(rotationsPerSecond * 6f);

		}


	}
}
