
using Microsoft.Xna.Framework;
using StarsAbove.Projectiles.Bosses.Vagrant;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using static StarsAbove.NPCs.AttackLibrary.AttackLibrary;

namespace StarsAbove.NPCs.Vagrant
{

	public class VagrantBoss : ModNPC
	{
		public static readonly int arenaWidth = (int)(1.2f * 960);
		public static readonly int arenaHeight = (int)(1.2f * 600);



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
			Idle6,

			Asleep,
			Notice,
			Falling,
			Flutter1,
			Flutter2,
			Flutter3
		}

		// These are reference properties. One, for example, lets us write AI_State as if it's NPC.ai[0], essentially giving the index zero our own name.
		// Here they help to keep our AI code clear of clutter. Without them, every instance of "AI_State" in the AI code below would be "npc.ai[0]", which is quite hard to read.
		// This is all to just make beautiful, manageable, and clean code.
		public ref float AI_State => ref NPC.ai[0];
		public ref float AI_Timer => ref NPC.ai[1];

		public ref float AI_RotationNumber => ref NPC.ai[2];//Where the boss is in its rotation.
		public ref float AI_CastTimer => ref NPC.ai[3];//Continually ticks down from the value given by the attack; at zero, the cast is finished.

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Vagrant of Space and Time");
			
			Main.npcFrameCount[NPC.type] = 7; // make sure to set this for your modnpcs.

			// Specify the debuffs it is immune to
			NPCID.Sets.DebuffImmunitySets.Add(Type, new NPCDebuffImmunityData
			{
				SpecificallyImmuneTo = new int[] {
					BuffID.Poisoned // This NPC will be immune to the Poisoned debuff.
				}
			});
			/*
			DisplayName.SetDefault("The Vagrant of Space and Time");
			NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
			// By default enemies gain health and attack if hardmode is reached. this NPC should not be affected by that
			NPCID.Sets.DontDoHardmodeScaling[Type] = true;
			// Enemies can pick up coins, let's prevent it for this NPC
			NPCID.Sets.CantTakeLunchMoney[Type] = true;
			// Automatically group with other bosses
			NPCID.Sets.BossBestiaryPriority.Add(Type);

			var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{ // Influences how the NPC looks in the Bestiary
				CustomTexturePath = "StarsAbove/Bestiary/PerseusPortrait", // If the NPC is multiple parts like a worm, a custom texture for the Bestiary is encouraged.
				Position = new Vector2(142f, 74f),
				PortraitScale = 0.8f,
				PortraitPositionXOverride = -136f,
				PortraitPositionYOverride = 58f
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
			*/
		}
		public override void SetDefaults()
		{
			NPC.boss = true;
			NPC.lifeMax = 12000;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.knockBackResist = 0f;
			NPC.width = 160;
			NPC.height = 160;
			NPC.scale = 1f;
			NPC.npcSlots = 1f;
			NPC.aiStyle = -1;
			NPC.lavaImmune = true;
			NPC.noGravity = false;
			NPC.noTileCollide = false;
			DrawOffsetY = -2;
			NPC.HitSound = SoundID.NPCHit54;
			NPC.DeathSound = SoundID.NPCDeath52;

			NPC.value = Item.buyPrice(0, 1, 75, 45);

			Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/CosmicWill");

			SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.SeaOfStarsBiome>().Type };

			//Music =  mod.GetSoundSlot(SoundType.Music, "Sounds/Music/CosmicWill");
			NPC.netAlways = true;
		}

		int castTime;
		int castTimeMax;

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return 0f;
		}

		public override bool CheckDead()
		{
			if (NPC.ai[0] != (float)ActionState.Dying) //If the boss is defeated, but the death animation hasn't played yet, play the death animation.
			{
				NPC.ai[0] = (float)ActionState.Dying; //Flag boss as "dying"
				NPC.damage = 0; //Disable contact damage
				NPC.life = NPC.lifeMax; //HP set to max
				NPC.dontTakeDamage = true; //Invulnerable
				NPC.netUpdate = true; //Sync to clients
				return false; //Boss isn't dead yet!
			}
			return true;
		}

		// Our AI here makes our NPC sit waiting for a player to enter range, jumps to attack, flutter mid-fall to stay afloat a little longer, then falls to the ground. Note that animation should happen in FindFrame
		public override void AI()
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
			var bossPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			bossPlayer.VagrantBarActive = true;
			NPC.velocity *= 0.98f; //So the dashes don't propel the boss away

			Player P = Main.player[NPC.target];//THIS IS THE BOSS'S MAIN TARGET
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
			{
				NPC.TargetClosest(true);
			}

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
			if (AI_Timer >= 120) //An attack is active.
			{
				if (AI_RotationNumber == 0)
				{
					VorpalAssault(P, NPC);
					return;
				}
				if (AI_RotationNumber == 1)
				{
					VorpalBarrage(P, NPC);
					return;
				}
				if (AI_RotationNumber == 2)
				{
					Starfall(P, NPC);
					return;
				}
				if (AI_RotationNumber == 3)
				{
					VorpalAssault(P, NPC);
					return;
				}
				if (AI_RotationNumber == 4)
				{
					VorpalBarrage(P, NPC);
					return;
				}
				if (AI_RotationNumber == 5)
				{
					VorpalSiege(P, NPC);
					return;
				}
				if (AI_RotationNumber == 6)
				{
					InverseStarfall(P, NPC);
					return;
				}
				if (AI_RotationNumber == 7)
				{
					VorpalAssault(P, NPC);
					return;
				}
				if (AI_RotationNumber == 8)
				{
					UmbralUpsurge(P, NPC);
					return;
				}
				if (AI_RotationNumber == 9)
				{
					MeteorShower(P, NPC);
					return;
				}
				else
                {
					Starfall(P, NPC);
					if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI) { Main.NewText(Language.GetTextValue($"Execute Attack Else"), 220, 100, 247); }
					return;
				}
				

				/*
				if (AI_RotationNumber == 1)
				{
					VorpalAssault(P, NPC);
					if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI) { Main.NewText(Language.GetTextValue($"Attack 1"), 220, 100, 247); }

				}
				if (AI_RotationNumber == 2)
				{
					VorpalAssault(P, NPC);
					if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI) { Main.NewText(Language.GetTextValue($"Attack 2"), 220, 100, 247); }

				}
				if (AI_RotationNumber == 3)
				{
					VorpalAssault(P, NPC);
					if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI) { Main.NewText(Language.GetTextValue($"Attack 3"), 220, 100, 247); }

				}//Fix this tomorrow. Only works if it's an else statement.. why?
				
				*/

			}
			if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI) { Main.NewText(Language.GetTextValue($"Rotation Number {AI_RotationNumber}"), 220, 100, 247); }
			if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI) { Main.NewText(Language.GetTextValue($"Timer {AI_Timer}"), 220, 100, 247); }
			if (Main.netMode != NetmodeID.Server && Main.myPlayer == Main.LocalPlayer.whoAmI) { Main.NewText(Language.GetTextValue($"State {AI_State}"), 220, 100, 247); }
			
		}

		// Here in FindFrame, we want to set the animation frame our npc will use depending on what it is doing.
		// We set npc.frame.Y to x * frameHeight where x is the xth frame in our spritesheet, counting from 0. For convenience, we have defined a enum above.
		public override void FindFrame(int frameHeight)
		{
			// This makes the sprite flip horizontally in conjunction with the npc.direction.
			NPC.spriteDirection = NPC.direction;

			//If a projectile that replaces the boss sprite appears, hide the boss sprite.
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile other = Main.projectile[i];

				if (other.active && (other.type == ModContent.ProjectileType<VagrantBurstSprite>()
					|| other.type == ModContent.ProjectileType<VagrantBowSprite>()
					|| other.type == ModContent.ProjectileType<VagrantSpearSprite>()
					|| other.type == ModContent.ProjectileType<VagrantSlamSprite>()
					))
					
				{
					NPC.frame.Y = (int)Frame.Empty * frameHeight;
					NPC.alpha = 250;
					return;
				}
			}
			//NPC.alpha = 0;
			NPC.alpha -= 40;//The sprite should appear again after being replaced.
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
					else if (NPC.frameCounter < 60)
					{
						NPC.frame.Y = (int)Frame.Idle6 * frameHeight;
					}
					else
					{
						NPC.frameCounter = 0;
					}
					break;
				case (float)ActionState.Asleep:
					// npc.frame.Y is the goto way of changing animation frames. npc.frame starts from the top left corner in pixel coordinates, so keep that in mind.
					NPC.frame.Y = (int)Frame.Asleep * frameHeight;
					break;
				case (float)ActionState.Notice:
					// Going from Notice to Asleep makes our npc look like it's crouching to jump.
					if (AI_Timer < 10)
					{
						NPC.frame.Y = (int)Frame.Notice * frameHeight;
					}
					else
					{
						NPC.frame.Y = (int)Frame.Asleep * frameHeight;
					}

					break;
				case (float)ActionState.Jump:
					NPC.frame.Y = (int)Frame.Falling * frameHeight;
					break;
				case (float)ActionState.Hover:
					// Here we have 3 frames that we want to cycle through.
					NPC.frameCounter++;

					if (NPC.frameCounter < 10)
					{
						NPC.frame.Y = (int)Frame.Flutter1 * frameHeight;
					}
					else if (NPC.frameCounter < 20)
					{
						NPC.frame.Y = (int)Frame.Flutter2 * frameHeight;
					}
					else if (NPC.frameCounter < 30)
					{
						NPC.frame.Y = (int)Frame.Flutter3 * frameHeight;
					}
					else
					{
						NPC.frameCounter = 0;
					}

					break;
				case (float)ActionState.Fall:
					NPC.frame.Y = (int)Frame.Falling * frameHeight;
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
			if (Main.rand.NextBool(5) && NPC.ai[1] < 420f)
			{
				for (int i = 0; i < 40; i++)
				{
					// Charging dust
					Vector2 vector = new Vector2(
						Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10,
						Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10);
					Dust d = Main.dust[Dust.NewDust(
						NPC.Center + vector, 1, 1,
						269, 0, 0, 255,
						new Color(1f, 1f, 1f), 1.5f)];
					d.velocity = -vector / 16;
					d.velocity -= NPC.velocity / 8;
					d.noLight = true;
					d.noGravity = true;
				}
				// This dust spawn adapted from the Pillar death code in vanilla.
				for (int dustNumber = 0; dustNumber < 3; dustNumber++)
				{
					Dust dust = Main.dust[Dust.NewDust(NPC.Left, NPC.width, NPC.height / 2, 242, 0f, 0f, 0, default(Color), 1f)];
					dust.position = NPC.Center + Vector2.UnitY.RotatedByRandom(4.1887903213500977) * new Vector2(NPC.width * 1.5f, NPC.height * 1.1f) * 0.8f * (0.8f + Main.rand.NextFloat() * 0.2f);
					dust.velocity.X = 0f;
					dust.velocity.Y = -Math.Abs(dust.velocity.Y - (float)dustNumber + NPC.velocity.Y - 4f) * 3f;
					dust.noGravity = true;
					dust.fadeIn = 1f;
					dust.scale = 1f + Main.rand.NextFloat() + (float)dustNumber * 0.3f;
				}
			}

			if (NPC.ai[1] >= 480f)
			{
				for (int d = 0; d < 305; d++)
				{
					Dust.NewDust(NPC.Center, 0, 0, 21, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
				}

				DownedBossSystem.downedVagrant = true;
				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
				}
				modPlayer.VagrantActive = false;
				modPlayer.VagrantBarActive = false;

				NPC.life = 0;
				NPC.HitEffect(0, 0);
				NPC.checkDead(); // This will trigger ModNPC.CheckDead the second time, causing the real death.

				
			}
			return;
		}
		private void SpawnAnimation()
		{
			//This will play the "superman landing" animation, knock back all players, and begin the fight. Note that unlike the old Vagrant this one can be hit.
			//Once the spawn animation is done, change to ActionState.Idle

			//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
			Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSlamSprite>(), 0, 0, Main.myPlayer);

			AI_State = (float)ActionState.Idle;
		}
		private void Idle()
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();
			modPlayer.NextAttack = "";
			NPC.direction = (Main.player[NPC.target].Center.X < NPC.Center.X).ToDirectionInt();//Face the target.

			AI_Timer++;//The boss's rotation timer ticks upwards.


		}
		private void Casting()
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();
			NPC.ai[3]--;
			NPC.direction = (Main.player[NPC.target].Center.X < NPC.Center.X).ToDirectionInt();//Face the target.
			modPlayer.CastTime = (int)NPC.ai[3];
		}
		private void PersistentCast()
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

		}

		private void FallAsleep()
		{
			// TargetClosest sets npc.target to the player.whoAmI of the closest player.
			// The faceTarget parameter means that npc.direction will automatically be 1 or -1 if the targeted player is to the right or left.
			// This is also automatically flipped if npc.confused.
			NPC.TargetClosest(true);

			// Now we check the make sure the target is still valid and within our specified notice range (500)
			if (NPC.HasValidTarget && Main.player[NPC.target].Distance(NPC.Center) < 500f)
			{
				// Since we have a target in range, we change to the Notice state. (and zero out the Timer for good measure)
				AI_State = (float)ActionState.Notice;
				AI_Timer = 0;
			}
		}

		private void Notice()
		{
			// If the targeted player is in attack range (250).
			if (Main.player[NPC.target].Distance(NPC.Center) < 250f)
			{
				// Here we use our Timer to wait .33 seconds before actually jumping. In FindFrame you'll notice AI_Timer also being used to animate the pre-jump crouch
				AI_Timer++;

				if (AI_Timer >= 20)
				{
					AI_State = (float)ActionState.Jump;
					AI_Timer = 0;
				}
			}
			else
			{
				NPC.TargetClosest(true);

				if (!NPC.HasValidTarget || Main.player[NPC.target].Distance(NPC.Center) > 500f)
				{
					// Out targeted player seems to have left our range, so we'll go back to sleep.
					AI_State = (float)ActionState.Asleep;
					AI_Timer = 0;
				}
			}
		}

		private void Jump()
		{
			AI_Timer++;

			if (AI_Timer == 1)
			{
				// We apply an initial velocity the first tick we are in the Jump frame. Remember that -Y is up.
				NPC.velocity = new Vector2(NPC.direction * 2, -10f);
			}
			else if (AI_Timer > 40)
			{
				// after .66 seconds, we go to the hover state. //TODO, gravity?
				AI_State = (float)ActionState.Hover;
				AI_Timer = 0;
			}
		}


	}
}