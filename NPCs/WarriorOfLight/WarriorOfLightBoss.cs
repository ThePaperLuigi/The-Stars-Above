
using Microsoft.Xna.Framework;
using StarsAbove.NPCs.OffworldNPCs;
//using StarsAbove.Projectiles.Bosses.WarriorOfLight;
using StarsAbove.Projectiles.Bosses.Vagrant;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;

using static StarsAbove.NPCs.AttackLibrary.AttackLibrary;
using StarsAbove.Buffs.Boss;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Buffs;
using StarsAbove.Utilities;
using StarsAbove.Projectiles.Bosses.WarriorOfLight;
using StarsAbove.Items.Accessories;

namespace StarsAbove.NPCs.WarriorOfLight
{
	[AutoloadBossHead]

	public class WarriorOfLightBoss : ModNPC
	{

		// Our texture is 36x36 with 2 pixels of padding vertically, so 38 is the vertical spacing.
		// These are for our benefit and the numbers could easily be used directly in the code below, but this is how we keep code organized.
		private enum Frame
		{
			Empty,
			Idle1,
			Idle2,
			Idle3,
			Idle4,
			Idle5,
		}

		// These are reference properties. One, for example, lets us write AI_State as if it's NPC.ai[0], essentially giving the index zero our own name.
		// Here they help to keep our AI code clear of clutter. Without them, every instance of "AI_State" in the AI code below would be "npc.ai[0]", which is quite hard to read.
		// This is all to just make beautiful, manageable, and clean code.
		public ref float AI_State => ref NPC.ai[0];
		public ref float AI_Timer => ref NPC.ai[1];

		public ref float AI_RotationNumber => ref NPC.ai[2];//Where the boss is in its rotation.
		public ref float AI_CastTimerMax => ref NPC.ai[3];//Continually ticks down from the value given by the attack; at zero, the cast is finished.
		public ref float AI_CastTimer => ref NPC.localAI[3];//Continually ticks down from the value given by the attack; at zero, the cast is finished.


		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("WarriorOfLight, the Burnished King");
			
			Main.npcFrameCount[NPC.type] = 6; // make sure to set this for your modnpcs.

			// Specify the debuffs it is immune to
			/*NPCID.Sets.DebuffImmunitySets.Add(Type, new NPCDebuffImmunityData
			{
				SpecificallyImmuneTo = new int[] {
					BuffID.Poisoned // This NPC will be immune to the Poisoned debuff.
				}
			});*/
			
			
			NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
			// By default enemies gain health and attack if hardmode is reached. this NPC should not be affected by that
			NPCID.Sets.DontDoHardmodeScaling[Type] = true;
			// Enemies can pick up coins, let's prevent it for this NPC
			NPCID.Sets.CantTakeLunchMoney[Type] = true;
			// Automatically group with other bosses
			NPCID.Sets.BossBestiaryPriority.Add(Type);

			NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Hide = true // Hides this NPC from the bestiary
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, bestiaryData);

		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			
			

		}
		public override void SetDefaults()
		{
			NPC.boss = true;
			NPC.lifeMax = 140000;
			NPC.damage = 30;
			NPC.defense = 35;
			NPC.knockBackResist = 0f;
			NPC.width = 200;
			NPC.height = 200;
			NPC.scale = 1f;
			NPC.npcSlots = 1f;
			NPC.aiStyle = 0;
			NPC.lavaImmune = true;
			NPC.noGravity = true;
			NPC.noTileCollide = false;
			NPC.value = 0f;
			DrawOffsetY = 0;

			NPC.HitSound = SoundID.NPCHit54;
			//NPC.DeathSound = SoundID.NPCDeath52;


			Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Boss/WarriorOfLight/ToTheEdgeInstrumental");
			SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.SeaOfStarsBiome>().Type };
			NPC.netAlways = true;
		}
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
			if(NPC.localAI[1] > 0)
            {
				Microsoft.Xna.Framework.Color color1 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
				Vector2 drawOrigin = new Vector2(NPC.width * 0.5f, NPC.height * 0.5f);
				int r1 = (int)color1.R;
				drawOrigin.Y += 34f;
				drawOrigin.Y += 8f;
				--drawOrigin.X;
				Vector2 position1 = NPC.Bottom - Main.screenPosition;
				Texture2D texture2D2 = (Texture2D)Request<Texture2D>("StarsAbove/Effects/WarriorVFX");
				float num11 = (float)((double)Main.GlobalTimeWrappedHourly / 7.0);
				float timeFloatAlt = (float)((double)Main.GlobalTimeWrappedHourly / 5.0);

				//These control fade out (unused)
				float num12 = num11;
				if ((double)num12 > 0.5)
					num12 = 1f - num11;
				if ((double)num12 < 0.0)
					num12 = 0.0f;
				float num13 = (float)(((double)num11 + 0.5) % 1.0);
				float num14 = num13;
				if ((double)num14 > 0.5)
					num14 = 1f - num13;
				if ((double)num14 < 0.0)
					num14 = 0.0f;
				Microsoft.Xna.Framework.Rectangle r2 = texture2D2.Frame(1, 1, 0, 0);
				drawOrigin = r2.Size() / 2f;
				Vector2 position3 = position1 + new Vector2(0.0f, -100f);
				Microsoft.Xna.Framework.Color color3 = new Microsoft.Xna.Framework.Color(245, 220, 135) * 1.6f; //This is the color of the pulse!
																												//Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3, NPC.rotation, drawOrigin, NPC.scale * 0.5f, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
				float num15 = 2f; //+ num11 * 2.75f; //Scale?
				Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3, NPC.rotation + num11, drawOrigin, NPC.scale * 0.5f * num15, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
				float num16 = 2f; //+ num13 * 2.75f; //Scale?
				Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3, NPC.rotation - timeFloatAlt, drawOrigin, NPC.scale * 0.5f * num16, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
				Texture2D texture2D3 = (Texture2D)TextureAssets.Extra[89];
				Microsoft.Xna.Framework.Rectangle r3 = texture2D3.Frame(1, 1, 0, 0);

			}

			return base.PreDraw(spriteBatch, screenPos, drawColor);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return 0f;
		}
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * bossAdjustment * balance);
			//NPC.defense *= numPlayers * 5;
		}
		public override void BossLoot(ref string name, ref int potionType)
        {
			
			potionType = ItemID.None;


			base.BossLoot(ref name, ref potionType);
        }
        public override bool CheckDead()
		{
			if (NPC.ai[0] != (float)ActionState.Dying) //If the boss is defeated, but the death animation hasn't played yet, play the death animation.
			{
				SoundEngine.PlaySound(StarsAboveAudio.WarriorOfLight_YouStillStand, NPC.Center);

				NPC.ai[0] = (float)ActionState.Dying; //Flag boss as "dying"
				NPC.damage = 0; //Disable contact damage
				NPC.life = NPC.lifeMax; //HP set to max
				NPC.dontTakeDamage = true; //Invulnerable
				NPC.netUpdate = true; //Sync to clients
				return false; //Boss isn't dead yet!
			}
			return true;
		}

		public override void AI()
        {
			
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
            var bossPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

            bossPlayer.WarriorOfLightBarActive = true;
            NPC.velocity *= 0.98f; //So the dashes don't propel the boss away

            Player P = Main.player[NPC.target];//THIS IS THE BOSS'S MAIN TARGET
            FindTargetPlayer();
            IfAllPlayersAreDead();
            if (NPC.ai[0] == (float)ActionState.Dying)
            {
                DeathAnimation();//The boss is in its dying animation. No other AI code will run.

                return;
            }
            switch (AI_State)
            {
                case (float)ActionState.Spawning:
                    SpawnAnimation();
                    break;
                case (float)ActionState.PersistentCast:
                    PersistentCast();
                    break;
                case (float)ActionState.Casting:
                    Casting();
                    break;
                case (float)ActionState.Idle:
                    Idle();
                    break;
            }
			//if AI_Timer is less than 120, it isn't casting- let's try changing phases here
			if(AI_Timer < 120 && AI_State == (float)ActionState.Idle)
            {

            }
            else if (AI_Timer >= 120) //An attack is active.
            {
				//Test Rotation
				/*
				if (AI_RotationNumber == 0)
				{
					//
					WarriorSummoning3(P, NPC);
					return;
				}
				if (AI_RotationNumber == 1)
				{
					//
					CoruscantSaberIn(P, NPC);
					return;
				}
				else
				{
					AI_RotationNumber = 0;
					return;
				}*/
				
                //Attacks begin here.
                if (AI_RotationNumber == 0)
                {
					//
					TheBitterEnd(P, NPC);
                    return;
                }
				else if (AI_RotationNumber == 1)
				{
					//
					CoruscantSaber(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 2)
				{
					//
					Transplacement(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 3)
				{
					//
					RefulgentReprobation(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 4)
				{
					//
					Transplacement(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 5)
				{
					//
					AbsoluteIce(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 6)
				{
					//
					PassageOfArms1(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 7)
				{
					//
					ThreadsOfFate1(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 8)
				{
					//
					EphemeralEdge(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 9)
				{
					//
					ThreadsOfFate3(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 10)
				{
					//
					RadiantReprobation(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 11)
				{
					//
					AbsoluteIce(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 12)
				{
					//
					CoruscantSaber(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 13)
				{
					//
					AbsoluteFire(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 14)
				{
					//
					RefulgentReprobation(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 15)
				{
					if(NPC.life <= (NPC.lifeMax * 0.8))
                    {
						
						AI_RotationNumber = 16;
					}
					else
					{
						AI_RotationNumber = 0;

					}
					return;
				}
				else if (AI_RotationNumber == 16)
				{
					Ascendance(P, NPC);
					
					return;
				}
				else if (AI_RotationNumber == 17)
				{
					WarriorSummoning1(P, NPC);
					
					return;
				}
				else if (AI_RotationNumber == 18)
				{
					Transplacement(P, NPC);
					
					return;
				}
				else if (AI_RotationNumber == 19)
				{
					TheBitterEnd(P, NPC);
					
					return;
				}
				else if (AI_RotationNumber == 20)
				{
					//
					RadiantReprobation(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 21)
				{
					//
					Transplacement(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 22)
				{
					//
					ImbuedSaber(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 23)
				{
					//
					TheBitterEnd(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 24)
				{
					//
					ImbuedCoruscance(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 25)
				{
					//
					ResoluteReprobation(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 26)
				{
					//
					EphemeralEdge(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 27)
				{
					//
					SearingLight(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 28)
				{
					//
					Recenter(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 29)
				{
					//
					HopeConfluence(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 30)
				{
					//
					PassageOfArms2(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 31)
				{
					//
					ImbuedSaber(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 32)
				{
					//
					SearingLight(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 33)
				{
					//
					WarriorSummoning2(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 34)
				{
					//
					EphemeralEdge(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 35)
				{
					//
					ImbuedCoruscance(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 36)
				{
					//
					RefulgentReprobation(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 37)
				{
					//
					ThreadsOfFate1(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 38)
				{
					//
					SearingLight(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 39)
				{
					//
					ThreadsOfFate2(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 40)
				{
					//
					Transplacement(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 41)
				{
					//
					TheBitterEnd(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 42)
				{
					//
					WarriorSummoning3(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 43)
				{
					//
					CoruscantSaberIn(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 44)
				{
					//
					TheBitterEnd(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 45)
				{
					//
					SearingLight(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 46)
				{
					//
					ImbuedSaber(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 47)
				{
					//
					RadiantReprobation(P, NPC);
					return;
				}
				else if (AI_RotationNumber == 48)
				{
					//
					ImbuedCoruscance(P, NPC);
					return;
				}
				else
                {
                    AI_RotationNumber = 17;
                    return;
                }
				

			}

			BossVisuals();
			//DrawOffsetY = MathHelper.Lerp(-10, 10, EaseHelper.Pulse(NPC.localAI[0]));
		}
		private void BossVisuals()
        {
			if (NPC.localAI[1] != 0)
			{
				Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Boss/WarriorOfLight/ToTheEdge");
			}
			else
			{
				Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Boss/WarriorOfLight/ToTheEdgeInstrumental");
			}
		}
        private void FindTargetPlayer()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
        }

        private void IfAllPlayersAreDead()
        {
            if (Main.player[NPC.target].dead)
            {
                //Leave if all players are dead.
                Vector2 vector8 = new Vector2(NPC.Center.X, NPC.Center.Y);
                for (int d = 0; d < 100; d++)
                {
                    Dust.NewDust(vector8, 0, 0, 269, 0f + Main.rand.Next(-40, 40), 0f + Main.rand.Next(-40, 40), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 65; d++)
                {
                    Dust.NewDust(vector8, 0, 0, 21, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 35; d++)
                {
                    Dust.NewDust(vector8, 0, 0, 50, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 35; d++)
                {
                    Dust.NewDust(vector8, 0, 0, 55, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default(Color), 1.5f);
                }
				for (int i = 0; i < Main.maxPlayers; i++)
				{
					Player player = Main.player[i];
					if (player.active)
					{
						int k = Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, 0, 0, ModContent.ItemType<SigilOfHope>(), 1, false);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, null, k, 1f);
						}

					}


				}
				
				NPC.active = false;


            }
        }

        // Here in FindFrame, we want to set the animation frame our npc will use depending on what it is doing.
        // We set npc.frame.Y to x * frameHeight where x is the xth frame in our spritesheet, counting from 0. For convenience, we have defined a enum above.
        public override void FindFrame(int frameHeight)
		{
			// This makes the sprite flip horizontally in conjunction with the npc.direction.
			//NPC.spriteDirection = NPC.direction;

			//If a projectile that replaces the boss sprite appears, hide the boss sprite.
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile other = Main.projectile[i];

				if (other.active && (other.type == ModContent.ProjectileType<WarriorOfLightCastingSprite>()
					|| other.type == ModContent.ProjectileType<WarriorOfLightSwingingSprite>()
					//|| other.type == ModContent.ProjectileType<WarriorOfLightLoseSwordSprite>()
					//|| other.type == ModContent.ProjectileType<WarriorOfLightCastSprite>()
					|| other.type == ModContent.ProjectileType<VagrantSwordSprite>()
					&& other.alpha < 1))
					
				{
					if(other.alpha > 1)
                    {
						if (NPC.frame.Y == (int)Frame.Empty)
						{
							NPC.frame.Y = (int)Frame.Idle1 * frameHeight;
						}
					}
					else
                    {
						NPC.frame.Y = (int)Frame.Empty * frameHeight;
					}
					
					//NPC.alpha = 255;
					return;
				}
			}
			
			//NPC.alpha = 0;
			//NPC.alpha -= 100;//The sprite should appear again after being replaced.
			// For the most part, our animation matches up with our states.
			switch (AI_State)
			{
				case (float)ActionState.Idle:
					NPC.frameCounter++;
					if (NPC.frameCounter < 10)
					{
						NPC.frame.Y = (int)Frame.Idle1 * frameHeight;
					}
					else if (NPC.frameCounter < 20)
					{
						NPC.frame.Y = (int)Frame.Idle2 * frameHeight;
					}
					else if (NPC.frameCounter < 30)
					{
						NPC.frame.Y = (int)Frame.Idle3 * frameHeight;
					}
					else if (NPC.frameCounter < 40)
					{
						NPC.frame.Y = (int)Frame.Idle4 * frameHeight;
					}
					else if (NPC.frameCounter < 50)
					{
						NPC.frame.Y = (int)Frame.Idle5 * frameHeight;
					}
					else
					{
						NPC.frameCounter = 0;
					}

					break;
				case (float)ActionState.Casting:
					NPC.frameCounter++;
					if (NPC.frameCounter < 10)
					{
						NPC.frame.Y = (int)Frame.Idle1 * frameHeight;
					}
					else if (NPC.frameCounter < 20)
					{
						NPC.frame.Y = (int)Frame.Idle2 * frameHeight;
					}
					else if (NPC.frameCounter < 30)
					{
						NPC.frame.Y = (int)Frame.Idle3 * frameHeight;
					}
					else if (NPC.frameCounter < 40)
					{
						NPC.frame.Y = (int)Frame.Idle4 * frameHeight;
					}
					else if (NPC.frameCounter < 50)
					{
						NPC.frame.Y = (int)Frame.Idle5 * frameHeight;
					}
					else
					{
						NPC.frameCounter = 0;
					}
					break;
				default:
					NPC.frameCounter++;
					if (NPC.frameCounter < 10)
					{
						NPC.frame.Y = (int)Frame.Idle1 * frameHeight;
					}
					else if (NPC.frameCounter < 20)
					{
						NPC.frame.Y = (int)Frame.Idle2 * frameHeight;
					}
					else if (NPC.frameCounter < 30)
					{
						NPC.frame.Y = (int)Frame.Idle3 * frameHeight;
					}
					else if (NPC.frameCounter < 40)
					{
						NPC.frame.Y = (int)Frame.Idle4 * frameHeight;
					}
					else if (NPC.frameCounter < 50)
					{
						NPC.frame.Y = (int)Frame.Idle5 * frameHeight;
					}
					else
					{
						NPC.frameCounter = 0;
					}

					break;
			}
		}

		public override bool? CanFallThroughPlatforms()
		{


			return false;
		}
		private void DeathAnimation()
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/silence");
			NPC.dontTakeDamage = true;
			
			NPC.ai[1] += 1f; // increase our death timer.
							 //npc.velocity = Vector2.UnitY * npc.velocity.Length();
			NPC.velocity.X *= 0.95f; // lose inertia
									 //if expert mode, boss flies up and new phase starts?
									 //npc.velocity.Y = npc.velocity.Y - 0.02f;
			if (NPC.velocity.Y < 0.1f)
			{
				NPC.velocity.Y = NPC.velocity.Y + 0.01f;
			}
			if (NPC.velocity.Y > 0.1f)
			{
				NPC.velocity.Y = NPC.velocity.Y - 0.01f;
			}
			
			if (Main.rand.NextBool(5) && NPC.ai[1] < 180f)
			{
				
				// This dust spawn adapted from the Pillar death code in vanilla.
				for (int dustNumber = 0; dustNumber < 3; dustNumber++)
				{
					Dust dust = Main.dust[Dust.NewDust(NPC.Left, NPC.width, NPC.height / 2, DustID.FireworkFountain_Yellow, 0f, 0f, 0, default(Color), 0.4f)];
					dust.position = NPC.Center + Vector2.UnitY.RotatedByRandom(4.1887903213500977) * new Vector2(NPC.width, NPC.height) * 0.8f * (0.8f + Main.rand.NextFloat() * 0.2f);
					dust.velocity.X = 0f;
					dust.velocity.Y = -Math.Abs(dust.velocity.Y - (float)dustNumber + NPC.velocity.Y - 4f) * 3f;
					dust.noGravity = true;
					dust.fadeIn = 1f;
					dust.scale = 1f + Main.rand.NextFloat() + (float)dustNumber * 0.3f;
				}
			}

			if (NPC.ai[1] >= 280f)
			{
				Main.LocalPlayer.GetModPlayer<BossPlayer>().warriorCutsceneProgress2 = 10;
				for (int d = 0; d < 305; d++)
				{
					Dust.NewDust(NPC.Center, 0, 0, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default(Color), 1.5f);
				}

				//
				
				if (!NPC.AnyNPCs(NPCType<WarriorOfLightBossFinalPhase>()))
				{
					int index = NPC.NewNPC(null, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<WarriorOfLightBossFinalPhase>(), NPC.whoAmI);


					if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
					{
						NetMessage.SendData(MessageID.SyncNPC, number: index);
					}
					
				}
				/*
				

				DownedBossSystem.downedWarriorOfLight = true;
				
				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
				}
				*/

				NPC.life = 0;
				NPC.HitEffect(0, 0);
				NPC.checkDead(); // This will trigger ModNPC.CheckDead the second time, causing the real death.

				
			}
			return;
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{/*
		  * 
			// Do NOT misuse the ModifyNPCLoot and OnKill hooks: the former is only used for registering drops, the latter for everything else
			//Chance for a Prism
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Prisms.BurnishedPrism>(), 4));

			// Add the treasure bag using ItemDropRule.BossBag (automatically checks for expert mode)
			//npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<VagrantBossBag>()));

			// Trophies are spawned with 1/10 chance
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Placeable.BossLoot.WarriorOfLightTrophyItem>(), 10));

			// ItemDropRule.MasterModeCommonDrop for the relic
			npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Placeable.BossLoot.WarriorOfLightBossRelicItem>()));

			// ItemDropRule.MasterModeDropOnAllPlayers for the pet
			//npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MinionBossPetItem>(), 4));

			// All our drops here are based on "not expert", meaning we use .OnSuccess() to add them into the rule, which then gets added
			LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
			LeadingConditionRule ExpertRule = new LeadingConditionRule(new Conditions.IsExpert());

			// Notice we use notExpertRule.OnSuccess instead of npcLoot.Add so it only applies in normal mode
			// Boss masks are spawned with 1/7 chance
			//notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MinionBossMask>(), 7));

			notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Prisms.BurnishedPrism>(), 4));

			// Finally add the leading rule
			npcLoot.Add(ExpertRule);
			npcLoot.Add(notExpertRule);*/
			npcLoot.RemoveWhere(rule => true);
		}
		
		private void SpawnAnimation()
		{
			
			//Once the spawn animation is done, change to ActionState.Idle

			//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
			//Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSlamSprite>(), 0, 0, Main.myPlayer);
			/*
			for (int i = 0; i < Main.maxPlayers; i++)
			{
				Player player = Main.player[i];
				if (player.active && player.Distance(NPC.Center) < 300)
				{
					player.velocity = Vector2.Normalize(NPC.Center - player.Center) * -10f;
				}
			}
			*/
			if(NPC.localAI[0] == 0)
            {
				Main.LocalPlayer.GetModPlayer<BossPlayer>().warriorCutsceneProgress = 10;
				NPC.position.X = Main.player[NPC.target].Center.X - 50;
				NPC.position.Y = Main.player[NPC.target].position.Y - 160;
			}
			

			NPC.netUpdate = true;
			if (Main.netMode == NetmodeID.SinglePlayer)
			{
				NPC.dontTakeDamage = true;

			}
			for (int i = 0; i < Main.maxPlayers; i++)
			{
				Player player = Main.player[i];
				if (player.active && player.Distance(NPC.Center) < 1000)
                {
					player.AddBuff(BuffType<Invincibility>(), 10);
					player.AddBuff(BuffType<DownForTheCount>(), 10);
				}
					

			}
			//Boss spawn timer.
			NPC.localAI[0]++;
			if (NPC.localAI[0] >= 440)
			{
				NPC.dontTakeDamage = false;

				NPC.netUpdate = true;

				AI_State = (float)ActionState.Idle;
			}
		}
		private void Idle()
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();
			modPlayer.NextAttack = "";
			NPC.direction = (Main.player[NPC.target].Center.X < NPC.Center.X).ToDirectionInt();//Face the target.
			NPC.spriteDirection = NPC.direction;
			AI_Timer++;//The boss's rotation timer ticks upwards.


		}
		private void Casting()
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();
			NPC.localAI[3]++;
			NPC.direction = (Main.player[NPC.target].Center.X < NPC.Center.X).ToDirectionInt();//Face the target.
			NPC.spriteDirection = NPC.direction;

			modPlayer.CastTime = (int)NPC.localAI[3];
			modPlayer.CastTimeMax = (int)NPC.ai[3];
		}
		private void PersistentCast()
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

		}
		//Draw the portal
		
	}
}