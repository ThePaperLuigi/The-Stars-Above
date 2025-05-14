
using Microsoft.Xna.Framework;
using StarsAbove.NPCs.OffworldNPCs;
using StarsAbove.Projectiles.Bosses.Nalhaun;
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
using Terraria.Graphics.Shaders;
using StarsAbove.Projectiles.Bosses.Tsukiyomi;
using SubworldLibrary;
using StarsAbove.NPCs.WarriorOfLight;
using StarsAbove.Items.Loot;
using StarsAbove.Systems;
using StarsAbove.Projectiles.Extra;
using StarsAbove.Buffs.SubworldModifiers;
using StarsAbove.Items.Accessories;
using StarsAbove.Items.BossBags;
using static StarsAbove.NPCs.Thespian.ThespianBoss;
using System.Collections.Generic;
using StarsAbove.Systems;

namespace StarsAbove.NPCs.Arbitration
{
    [AutoloadBossHead]

	public class ArbitrationBoss : ModNPC
	{

		public int AttackTimer = 120;

		private enum Frame
		{
			Empty,
			Idle1,
			Idle2,
			Idle3,
			Idle4,
			Idle5,

			Cast1,
			Cast2,
			Cast3,
			Cast4,
			Cast5,

			Defeat

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
			
			Main.npcFrameCount[NPC.type] = 11; // make sure to set this for your modnpcs.

			NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
			// By default enemies gain health and attack if hardmode is reached. this NPC should not be affected by that
			NPCID.Sets.DontDoHardmodeScaling[Type] = true;
			// Enemies can pick up coins, let's prevent it for this NPC
			NPCID.Sets.CantTakeLunchMoney[Type] = true;
			
			//Phase 1, so no bestiary
			NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = new()
			{
				Hide = false // Hides this NPC from the bestiary
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, bestiaryData);

		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

                new FlavorTextBestiaryInfoElement($"Mods.StarsAbove.Bestiary.{Name}")
                });
        }
		public override void SetDefaults()
		{
			NPC.boss = true;
			NPC.lifeMax = 90000;
			NPC.damage = 15;
			NPC.defense = 15;
			NPC.knockBackResist = 0f;
			NPC.width = 150;
			NPC.height = 150;
			NPC.scale = 1f;
			NPC.npcSlots = 1f;
			NPC.aiStyle = -1;
			NPC.lavaImmune = true;
			NPC.noGravity = true;
			NPC.noTileCollide = false;
			NPC.value = 0f;

			//NPC.HitSound = SoundID.NPCHit54;
			//NPC.DeathSound = SoundID.NPCDeath52;

			Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Boss/Arbitration/SecondWarning");

			SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.SeaOfStarsBiome>().Type };
			NPC.netAlways = true;
		}
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
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

            potionType = ItemID.GreaterHealingPotion;


            if (!DownedBossSystem.downedArbiter)
            {
                DownedBossSystem.downedArbiter = true;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
                }
            }


            base.BossLoot(ref name, ref potionType);
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

		public override void AI()
        {
			DrawOffsetY = 94;
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
            var bossPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//REPLACE BAR WITH YOUR OWN
            bossPlayer.ArbitrationBarActive = true;
            Lighting.AddLight(NPC.Center, TorchID.White);
            NPC.velocity *= 0.98f; //So the dashes don't propel the boss away

            Player P = Main.player[NPC.target];//THIS IS THE BOSS'S MAIN TARGET
            FindTargetPlayer();
            IfAllPlayersAreDead();
			BossVisuals();
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
			if (AI_Timer < 120 && AI_State == (float)ActionState.Idle)
			{
				
			}
			else if (AI_Timer >= AttackTimer) //An attack is active. (Temp 480, usually 120, or 2 seconds)
            {
                List<RotationAction> bossRotation = new List<RotationAction>
                {
					Titanomachia,
					BloodMoon1,
                    VisionsBeyond,
                    Titanomachia2,
					DarkDeluge,		
					DarkDesign,
                    Titanomachia,
                    DarkDeluge,
					SeveredFate1,
                    OrdainedEnd,
					BloodMoon1,
                    DarkDeluge,
					Titanomachia,
                    ShadowsWithal,
                    DarkDeluge,
                    DeathRecital,
					BloodMoon3,
                    Titanomachia,
                    BloodMoon2,
					SeveredFate2,
                    DarkDeluge,
                    Titanomachia2,
                    Titanomachia,
                    DarkDeluge,
                    BloodMoon3,
					OrdainedEnd,
                    SeveredFate1,
                    OrdainedEnd,
                    BloodMoon3,
                    BloodMoon3,
                    DarkDeluge,
                    Titanomachia,
                    ShadowsWithal,
                    DarkDeluge,
                    DeathRecital,
                    DeathRecital,
                    BloodMoon3,
                    Titanomachia2,      
                    BloodMoon2,
                    SeveredFate2,
                    


                 };
                if (AI_RotationNumber >= 0 && AI_RotationNumber < bossRotation.Count)
                {
                    bossRotation[(int)AI_RotationNumber](P, NPC);
                }
                else
                {
                    AI_RotationNumber = 4;
                    return;
                }
            }
			
		}
		private void BossVisuals()
		{

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
                NPC.active = false;


            }
        }

        // Here in FindFrame, we want to set the animation frame our npc will use depending on what it is doing.
        // We set npc.frame.Y to x * frameHeight where x is the xth frame in our spritesheet, counting from 0. For convenience, we have defined a enum above.
        public override void FindFrame(int frameHeight)
		{
			// This makes the sprite flip horizontally in conjunction with the npc.direction.
			//NPC.spriteDirection = NPC.direction;
			if(NPC.HasBuff(BuffType<TsukiyomiTeleportBuff>()))
            {
				NPC.frame.Y = (int)Frame.Empty * frameHeight;
				return;
			}

			//If a projectile that replaces the boss sprite appears, hide the boss sprite.
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile other = Main.projectile[i];

				if (other.active && (other.type == ModContent.ProjectileType<TsukiBloodshedSheathe>()
					|| other.type == ModContent.ProjectileType<TsukiBuryTheLight>()
					|| other.type == ModContent.ProjectileType<TsukiTakonomicon>()
					|| other.type == ModContent.ProjectileType<TsukiShadowlessCerulean>()
					|| other.type == ModContent.ProjectileType<TsukiDeathInFourActs>()
					|| other.type == ModContent.ProjectileType<TsukiDeathInFourActs2>()
					|| other.type == ModContent.ProjectileType<TsukiLuminaryWand>()
					|| other.type == ModContent.ProjectileType<TsukiKeyOfTheKingsLaw>()
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
					
					//NPC.alpha = 250;
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
						NPC.frame.Y = (int)Frame.Cast1 * frameHeight;
					}
					else if (NPC.frameCounter < 20)
					{
						NPC.frame.Y = (int)Frame.Cast2 * frameHeight;
					}
					else if (NPC.frameCounter < 30)
					{
						NPC.frame.Y = (int)Frame.Cast3 * frameHeight;
					}
					else if (NPC.frameCounter < 40)
					{
						NPC.frame.Y = (int)Frame.Cast4 * frameHeight;
					}
					else if (NPC.frameCounter < 50)
					{
						NPC.frame.Y = (int)Frame.Cast5 * frameHeight;
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
			
			NPC.localAI[1] += 1f;
			if (NPC.localAI[1] >= 10f)
			{
				


				DownedBossSystem.downedArbiter = true;				
				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
				}
				/*
				if (modPlayer.tsukiyomiDialogue == 0)
				{
					//Force open the dialogue.
					modPlayer.chosenDialogue = 73;
					modPlayer.tsukiyomiDialogue = 2;
					modPlayer.dialoguePrep = true;
					modPlayer.starfarerDialogue = true;
					//if (Main.netMode != NetmodeID.Server) { Main.NewText(Language.GetTextValue("The Spatial Disk begins to resonate. Left click to interact."), 241, 255, 180); }
					modPlayer.tsukiyomiDialogue = 2;
				}*/
				
				NPC.life = 0;
				NPC.HitEffect(0, 0);
				NPC.checkDead(); // This will trigger ModNPC.CheckDead the second time, causing the real death.

				
			}
			return;
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
		  
			// Add the treasure bag using ItemDropRule.BossBag (automatically checks for expert mode)
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<ArbitrationBossBag>()));

            // Trophies are spawned with 1/10 chance
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Placeable.BossLoot.ArbitrationTrophyItem>(), 10));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Consumables.DemonicCrux>(), 1));
            // ItemDropRule.MasterModeCommonDrop for the relic
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Placeable.BossLoot.ArbitrationBossRelicItem>()));

            // ItemDropRule.MasterModeDropOnAllPlayers for the pet
            //npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MinionBossPetItem>(), 4));

            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            StellarSpoils.SetupBossStellarSpoils(npcLoot);

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AnomalyByte>(), 4));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.Essences.EssenceOfBloodshed>(), 2)).OnFailedRoll(ItemDropRule.Common(ModContent.ItemType<Items.Essences.EssenceOfMimicry>(), 2));

            npcLoot.Add(notExpertRule);
            StellarSpoils.SetupBossStellarSpoils(npcLoot);
		}
		
		private void SpawnAnimation()
		{

            //Once the spawn animation is done, change to ActionState.Idle

            //Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
            //Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSlamSprite>(), 0, 0, Main.myPlayer);

			if(SubworldSystem.AnyActive())
			{
                Vector2 initialMoveTo = new Vector2(14601, 5054);
                NPC.position = initialMoveTo;
            }
            
            //SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_Journey, NPC.Center);



            NPC.netUpdate = true;
			//SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_NalhaunIntroQuote, NPC.Center);
			for (int d = 0; d < 144; d++)
			{
				Dust.NewDust(NPC.Center, 0, 0, DustID.LifeDrain, 0f + Main.rand.Next(-35, 35), 0f + Main.rand.Next(-35, 35), 150, default(Color), 1.5f);
			}

            Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().locationName = "Arbitration";//lol
            Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().loadingScreenOpacity = 1f;
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
			NPC.localAI[3]++;
			NPC.direction = (Main.player[NPC.target].Center.X < NPC.Center.X).ToDirectionInt();//Face the target.
			modPlayer.CastTime = (int)NPC.localAI[3];
			modPlayer.CastTimeMax = (int)NPC.ai[3];

			if(NPC.frame.Y != (int)Frame.Empty)
            {
				

				for (int i = 0; i < 3; i++)
				{
					// Charging dust
					Vector2 vector = new Vector2(
						Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10,
						Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10);
					Dust d = Main.dust[Dust.NewDust(
						NPC.Center + vector, 1, 1,
						DustID.LifeDrain, 0, 0, 255,
						new Color(1f, 1f, 1f), 1.5f)];
					d.velocity = -vector / 16;
					d.velocity -= NPC.velocity / 8;
					d.noLight = true;
					d.noGravity = true;
				}
			}
			
			//Casting Dust

		}
		private void PersistentCast()
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

		}

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
		}
        public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
        {

        }

    }
}