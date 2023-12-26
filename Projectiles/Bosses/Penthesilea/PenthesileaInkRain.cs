using Microsoft.Xna.Framework;
using StarsAbove.NPCs.Nalhaun;
using StarsAbove.NPCs.Penthesilea;
using StarsAbove.NPCs.WarriorOfLight;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarsAbove.Projectiles.Bosses.Penthesilea
{
    public class PenthesileaInkRain : ModProjectile
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Velvet Crystal");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 50;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
		}

		public override void SetDefaults() {
			Projectile.width = 22;               //The width of projectile hitbox
			Projectile.height = 22;              //The height of projectile hitbox
			Projectile.aiStyle = 0;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false;         //Can the projectile deal damage to enemies?
			Projectile.hostile = true;         //Can the projectile deal damage to the player?
			//projectile.minion = true;           //Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 99;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 360;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 1f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 0;            //Set to above 0 if you want the projectile to update multiple time in a frame
			//AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}
		public override bool PreDraw(ref Color lightColor)
		{

			return false;
		}
		public override bool OnTileCollide(Vector2 oldVelocity) {
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 5 times
			
			return false;
		}

		public override void AI()
		{
			if (Projectile.ai[1] == 0)
			{
				
				Projectile.ai[1]++;
			}
			

			Projectile.ai[0]++;
			
			SearchForTargets(out bool foundTarget, out Vector2 targetCenter);

			if (Projectile.ai[0] >= 10)
			{
				if (foundTarget)
				{
					SoundEngine.PlaySound(SoundID.Drip, Projectile.Center);

					Projectile.ai[0] = 0;
					int type = ModContent.ProjectileType<InkBlot>();
                    float launchSpeed = 10f;

                    if (Main.rand.NextBool(6))
					{
						launchSpeed = 5f;
                        type = ModContent.ProjectileType<InkClot>();
                    }

					Vector2 position = Projectile.Center;

					Vector2 direction = Vector2.Normalize(targetCenter - Projectile.Center);
					Vector2 velocity = direction * launchSpeed;

					if(Main.netMode != NetmodeID.MultiplayerClient)
					{
						int randomPosition = Main.rand.Next(-800, 800);
                        int index = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position.X + randomPosition, position.Y, 0, velocity.Y, type, Projectile.damage, 0f, Main.myPlayer);
                        
						float dustAmount = 30f;
                        for (int i = 0; (float)i < dustAmount; i++)
                        {
                            Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                            spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                            spinningpoint5 = spinningpoint5.RotatedBy(Projectile.velocity.ToRotation());
                            int dust = Dust.NewDust(Projectile.Center, 0, 0, DustID.GemAmethyst);
                            Main.dust[dust].scale = 2f;
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].position = new Vector2(Projectile.Center.X + randomPosition, Projectile.Center.Y) + spinningpoint5;
                            Main.dust[dust].velocity = Projectile.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 3f;
                        }
                    }
                    

                }

				Projectile.ai[0] = 0;
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

					if (npc.active && npc.type == ModContent.NPCType<PenthesileaBoss>())
					{
						foundTarget = true;
						targetCenter = Main.player[npc.target].Center;
					}
				}
			}

		}

		public override void OnKill(int timeLeft)
		{
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			//Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			
			// Play explosion sound
			//Main.PlaySound(SoundID.Drown, projectile.position);
			// Smoke Dust spawn
			
			// Fire Dust spawn (CHANGE TO ICE DUST)
			
			// Large Smoke Gore spawn
			

		}
	}
}
