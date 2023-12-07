
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
using Terraria.Utilities;
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
using StarsAbove.Systems;
using StarsAbove.Projectiles.Extra;
using StarsAbove.Dusts;
using System.Collections.Generic;
using static Terraria.GameContent.Animations.IL_Actions.NPCs;
using StarsAbove.Items.BossBags;
using StarsAbove.Items.Memories;
using Terraria.UI;

namespace StarsAbove.NPCs.Thespian
{
    [AutoloadBossHead]

	public class ThespianBoss : ModNPC
	{
		public static readonly int arenaWidth = (int)(1f * 1000);
		public static readonly int arenaHeight = (int)(1f * 520);

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
		private int portalFrame
		{
			get => (int)NPC.localAI[2];
			set => NPC.localAI[2] = value;
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
			int associatedNPCType = ModContent.NPCType<WarriorOfLightBoss>();
			bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);

			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
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
			NPC.width = 100;
			NPC.height = 100;
			NPC.scale = 1f;
			NPC.npcSlots = 1f;
			NPC.aiStyle = -1;
			NPC.lavaImmune = true;
			NPC.noGravity = true;
			NPC.noTileCollide = false;
            NPC.value = Item.buyPrice(0, 3, 15, 45);


            //NPC.HitSound = SoundID.NPCHit54;
            //NPC.DeathSound = SoundID.NPCDeath52;

            Music = MusicID.OtherworldlyUGHallow;

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
			
			potionType = ItemID.None;


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
        public delegate void RotationAction(Player player, NPC npc);
        
        public override void AI()
        {
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
            var bossPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

			//REPLACE BAR WITH YOUR OWN
            bossPlayer.ThespianBarActive = true;

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
				if (NPC.life <= (NPC.lifeMax * 0.9) && AI_RotationNumber < 26) //At a certain HP threshold, skip ahead to Phase 2 (Phase 2 can be naturally reached by waiting)
				{
					//AI_RotationNumber = 1;
					//NPC.netUpdate = true;
				}
			}
			else if (AI_Timer >= AttackTimer) //An attack is active. (Temp 480, usually 120, or 2 seconds)
            {
                List<RotationAction> bossRotation = new List<RotationAction>
				{
					AethericAlkaheist,
					StygianAugurUp,
					StygianAugurRight,
					PhlogistonPyrotechnics, // exploding from the left
					AthanoricArena,
                    ParadigmOfChaos,
                    AlchemicalAnarchy, // use random buff from Paradigm to do an effect
                    ParadigmOfChaos, //gain random buff       
					RingmastersWill,
					StygianAugurDown,
					StygianAugurLeft,
					LetsCelebrate,
                    AethericAlkaheist,
                    Lixiviate,
                    AlchemicalAnarchy,
                    AthanoricArena,
                    ParadigmOfChaos,
                    AlchemicalAnarchy,
                    StygianAugurUp,
                    StygianAugurDown,
                    ParadigmOfChaos,
                    LetsCelebrate,
                    AlchemicalAnarchy,
                    AethericAlkaheist,
                    Lixiviate,
                    PhlogistonPyrotechnics,
                    StygianAugurDown,
                    RingmastersWillStopMoving,
					StygianAugurUp,
                    AethericAlkaheist,
                    RingmastersWill,
                    AlchemicalAnarchy,
                    StygianAugurLeft,
                    LetsCelebrate,
                    StygianAugurRight,
                    ParadigmOfChaos,
                    AlchemicalAnarchy,


                };
                if (AI_RotationNumber >= 0 && AI_RotationNumber < bossRotation.Count)
                {
                    bossRotation[(int)AI_RotationNumber](P, NPC);
                }
                else
                {
                    AI_RotationNumber = 0;
                    return;
                }
                /*
                switch (AI_RotationNumber)
				{

					case 0:
						StygianAugurUp(P, NPC);
						break;
					case 1:
						StygianAugurRight(P, NPC);
						break;
					case 2:
						PhlogistonPyrotechnics(P, NPC);
						break;
                    case 3:
                        RingmastersWill(P, NPC);
                        break;
                    case 4:
						StygianAugurDown(P, NPC);
						break;
					case 5:
						StygianAugurLeft(P, NPC);
						break;
					case 6:
						Lixiviate(P, NPC);
						break;				
					case 7:
						AthanoricArena(P, NPC);
						break;
					default:
						AI_RotationNumber = 0;//Once phase 2 is reached, always go back to the 1st mechanic after phase 2.
						return;

				}*/

            }
			
		}
        public override Color? GetAlpha(Color lightColor)
        {
            //return Color.White;
            return new Color(255, 255, 255, 0) * (1f - NPC.alpha / 255f);
        }
        private void BossVisuals()
        {
			for (int i = 0; i < Main.maxPlayers; i++)
			{
				Player player = Main.player[i];
				if (player.active)
				{
					//If boss is in phase 2...
					if (NPC.localAI[0] == 1 || NPC.localAI[0] == 2)
					{

					}
					//If boss is in phase 3...
					if (NPC.localAI[0] == 2)
					{
						
					}
                    else
                    {
						
                    }


				}


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
						for (int i = 0; i < 2; i++)
						{
							Dust d2 = Main.dust[Dust.NewDust(new Vector2(NPC.Center.X + 22, NPC.Center.Y + 1), 0, 2, DustID.GemAmethyst, Main.rand.NextFloat(-0.02f, 0.02f), Main.rand.NextFloat(-0.5f, -2.5f), 20, default(Color), 0.7f)];
							d2.fadeIn = 1f;
							d2.velocity.X *= 0.3f;
							d2.noGravity = true;

						}
						NPC.frame.Y = (int)Frame.Cast5 * frameHeight;
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
			if (NPC.localAI[1] >= 240f)
			{
				


				DownedBossSystem.downedThespian = true;
				
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
			// Do NOT misuse the ModifyNPCLoot and OnKill hooks: the former is only used for registering drops, the latter for everything else

			// Add the treasure bag using ItemDropRule.BossBag (automatically checks for expert mode)
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<ThespianBossBag>()));

            // Trophies are spawned with 1/10 chance
            //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Placeable.BossLoot.VagrantTrophyItem>(), 10));


            // ItemDropRule.MasterModeCommonDrop for the relic
            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Placeable.BossLoot.ThespianBossRelicItem>()));

			// ItemDropRule.MasterModeDropOnAllPlayers for the pet
			//npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MinionBossPetItem>(), 4));

			// All our drops here are based on "not expert", meaning we use .OnSuccess() to add them into the rule, which then gets added
			LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
			LeadingConditionRule ExpertRule = new LeadingConditionRule(new Conditions.IsExpert());
            notExpertRule.OnSuccess(npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NetheriteBar>(), 4, 1, 1)));
            notExpertRule.OnSuccess(npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BottledChaos>(), 4, 1, 1)));
            StellarSpoils.SetupBossStellarSpoils(npcLoot);
		}
		
		private void SpawnAnimation()
		{

			//Once the spawn animation is done, change to ActionState.Idle

			//Sprite animation. Easier to work with, because it's not tied to the main sprite sheet.
			//Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<VagrantSlamSprite>(), 0, 0, Main.myPlayer);

			Vector2 initialMoveTo = new Vector2(Main.player[NPC.target].Center.X - 50, Main.player[NPC.target].Center.Y - 200);
			NPC.position = initialMoveTo;
			//SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_Journey, NPC.Center);



			NPC.netUpdate = true;
			//SoundEngine.PlaySound(StarsAboveAudio.Nalhaun_NalhaunIntroQuote, NPC.Center);
			for (int d = 0; d < 130; d++)
			{
				Dust.NewDust(NPC.Center, 0, 0, DustID.SparksMech, 0f + Main.rand.Next(-30, 30), 0f + Main.rand.Next(-30, 30), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 144; d++)
			{
				Dust.NewDust(NPC.Center, 0, 0, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-35, 35), 0f + Main.rand.Next(-35, 35), 150, default(Color), 1.5f);
			}
			for (int d = 0; d < 126; d++)
			{
				Dust.NewDust(NPC.Center, 0, 0, DustID.BlueFairy, 0f + Main.rand.Next(-36, 36), 0f + Main.rand.Next(-36, 36), 150, default(Color), 1.5f);
			}

            Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().locationName = "Thespian";//lol
            Main.LocalPlayer.GetModPlayer<CelestialCartographyPlayer>().loadingScreenOpacity = 1f;
            AI_State = (float)ActionState.Idle;
		}
		private void Idle()
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();
			modPlayer.NextAttack = "";
			NPC.direction = (Main.player[NPC.target].Center.X < NPC.Center.X).ToDirectionInt();//Face the target.

			AI_Timer++;//The boss's rotation timer ticks upwards.

			for (int i = 0; i < 2; i++)
			{//Circle
				Dust d = Main.dust[Dust.NewDust(new Vector2(NPC.Center.X + 16, NPC.Center.Y + 1), 0, 2, DustID.GemAmethyst, Main.rand.NextFloat(-0.02f, 0.02f), Main.rand.NextFloat(-0.5f, -4.5f), 20, default(Color), 0.7f)];
				d.fadeIn = 1f;
				d.velocity.X *= 0.3f;
				d.noGravity = true;
				Dust d2 = Main.dust[Dust.NewDust(new Vector2(NPC.Center.X - 28, NPC.Center.Y + 1), 0, 2, DustID.GemAmethyst, Main.rand.NextFloat(-0.02f, 0.02f), Main.rand.NextFloat(-0.5f, -4.5f), 20, default(Color), 0.7f)];
				d2.fadeIn = 1f;
				d2.velocity.X *= 0.3f;
				d2.noGravity = true;

			}
		}
		private void Casting()
		{
			var modPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();
			NPC.localAI[3]++;
			NPC.direction = (Main.player[NPC.target].Center.X < NPC.Center.X).ToDirectionInt();//Face the target.
			modPlayer.CastTime = (int)NPC.localAI[3];
			modPlayer.CastTimeMax = (int)NPC.ai[3];
			
			if (NPC.frame.Y != (int)Frame.Empty)
            {
				

				for (int i = 0; i < 3; i++)
				{
					// Charging dust
					Vector2 vector = new Vector2(
						Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10,
						Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10);
					Dust d = Main.dust[Dust.NewDust(
						NPC.Center + vector, 1, 1,
						DustID.GemAmethyst, 0, 0, 255,
						new Color(1f, 1f, 1f), 0.9f)];
					d.velocity = -vector / 16;
					d.velocity -= NPC.velocity / 8;
					d.noLight = false;
					d.noGravity = true;
				}
			}
			if(NPC.frame.Y == (int)Frame.Cast5 * NPC.height)
            {
				
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
        public float quadraticFloatTimer;
        public float quadraticFloat;
		public float faceTransition;
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {

            SpriteEffects spriteEffects = SpriteEffects.None;

            // Getting texture of projectile
            Texture2D texture = (Texture2D)Request<Texture2D>("StarsAbove/NPCs/Thespian/ThespianCurseAngry");

            Texture2D bossAngry = (Texture2D)Request<Texture2D>("StarsAbove/NPCs/Thespian/ThespianCurseAngry");
            Texture2D bossHappy = (Texture2D)Request<Texture2D>("StarsAbove/NPCs/Thespian/ThespianCurseHappy");
            Texture2D bossSad = (Texture2D)Request<Texture2D>("StarsAbove/NPCs/Thespian/ThespianCurseSad");
            Texture2D bossTransition = (Texture2D)Request<Texture2D>("StarsAbove/NPCs/Thespian/ThespianCurseTransition");

            Rectangle sourceRectangle = new(0, (int)NPC.frame.Y, texture.Width, 200);

            Vector2 origin = sourceRectangle.Size() / 2f;

            quadraticFloatTimer += 0.0001f + MathHelper.Lerp(0.0001f,0f,NPC.life/NPC.lifeMax);
            quadraticFloat = EaseHelper.InOutQuad(EaseHelper.Pulse(quadraticFloatTimer));

            if (NPC.HasBuff(BuffType<ThespianAngry>()))
			{
                Main.EntitySpriteDraw(bossAngry,
                    new Vector2(NPC.Center.X, NPC.Center.Y - 20 - MathHelper.Lerp(-5, 35, quadraticFloat)) - screenPos ,
                    sourceRectangle, Color.White, NPC.rotation, origin, NPC.scale, spriteEffects, 0);
            }
            else if (NPC.HasBuff(BuffType<ThespianSad>()))
            {
                Main.EntitySpriteDraw(bossSad,
                    new Vector2(NPC.Center.X,NPC.Center.Y - 20 - MathHelper.Lerp(-5, 35, quadraticFloat)) - screenPos,
                    sourceRectangle, Color.White, NPC.rotation, origin, NPC.scale, spriteEffects, 0);
            }
			else
			{
                Main.EntitySpriteDraw(bossHappy,
                    new Vector2(NPC.Center.X, NPC.Center.Y - 20 - MathHelper.Lerp(-5, 35, quadraticFloat)) - screenPos,
                    sourceRectangle, Color.White, NPC.rotation, origin, NPC.scale, spriteEffects, 0);
            }
            if(NPC.HasBuff(BuffType<ThespianTransition>()))
			{
				faceTransition = 1f;
			}
			faceTransition -= 0.01f;
			faceTransition = MathHelper.Clamp(faceTransition, 0f, 1f);
            Main.EntitySpriteDraw(bossTransition,
                   new Vector2(NPC.Center.X, NPC.Center.Y - 20 - MathHelper.Lerp(-5, 35, quadraticFloat)) - screenPos,
                   sourceRectangle, Color.White * faceTransition, NPC.rotation, origin, NPC.scale, spriteEffects, 0);

            return base.PreDraw(spriteBatch, screenPos, drawColor);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			Microsoft.Xna.Framework.Color color1 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
			Vector2 drawOrigin = new Vector2(NPC.width * 0.5f, NPC.height * 0.5f);
			int r1 = (int)color1.R;
			//drawOrigin.Y += 34f;
			//drawOrigin.Y += 8f;
			--drawOrigin.X;
			Vector2 position1 = NPC.Bottom - Main.screenPosition;
			Texture2D texture2D2 = (Texture2D)Request<Texture2D>("StarsAbove/Effects/ThespianWallsEffect");
            float num11 = (float)((double)Main.GlobalTimeWrappedHourly % 4.0 / 4.0);
            if (Main.LocalPlayer.HasBuff(BuffType<AthanoricCurse>()))
			{
                num11 = (float)((double)Main.GlobalTimeWrappedHourly % 0.5 / 0.5);
            }
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
			Vector2 position3 = position1 + new Vector2(0.0f, -46f);
			Microsoft.Xna.Framework.Color color3 = Color.Pink;
			Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3, NPC.rotation, drawOrigin, 1f, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
			float num15 = 1f + num11 * 0.15f;
			Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num12, NPC.rotation, drawOrigin, 1f * num15, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);
			float num16 = 1f + num13 * 0.15f;
			Main.spriteBatch.Draw(texture2D2, position3, new Microsoft.Xna.Framework.Rectangle?(r2), color3 * num14, NPC.rotation, drawOrigin, 1f * num16, SpriteEffects.None ^ SpriteEffects.FlipHorizontally, 0.0f);

			/*
			int portalWidth = 48;
			int portalDepth = 18;
			Color color = new Color(242, 166, 231);
			int centerX = (int)NPC.Center.X;
			int centerY = (int)NPC.Center.Y;
			Main.instance.LoadProjectile(ProjectileID.PortalGunGate);
			for (int x = centerX - arenaWidth / 2; x < centerX + arenaWidth / 2; x += portalWidth)
			{
				int frameNum = (portalFrame / 6 + x / portalWidth) % Main.projFrames[ProjectileID.PortalGunGate];
				Rectangle frame = new Rectangle(0, frameNum * (portalWidth + 2), portalDepth, portalWidth);
				Vector2 drawPos = new Vector2(x + portalWidth / 2, centerY - arenaHeight / 2) - Main.screenPosition;
				spriteBatch.Draw((Texture2D)TextureAssets.Projectile[ProjectileID.PortalGunGate], drawPos, frame, color, (float)-Math.PI / 2f, new Vector2(portalDepth / 2, portalWidth / 2), 1f, SpriteEffects.None, 0f);
				drawPos.Y += arenaHeight;
				spriteBatch.Draw((Texture2D)TextureAssets.Projectile[ProjectileID.PortalGunGate], drawPos, frame, color, (float)Math.PI / 2f, new Vector2(portalDepth / 2, portalWidth / 2), 1f, SpriteEffects.None, 0f);
			}
			for (int y = centerY - arenaHeight / 2; y < centerY + arenaHeight / 2; y += portalWidth)
			{
				int frameNum = (portalFrame / 6 + y / portalWidth) % Main.projFrames[ProjectileID.PortalGunGate];
				Rectangle frame = new Rectangle(0, frameNum * (portalWidth + 2), portalDepth, portalWidth);
				Vector2 drawPos = new Vector2(centerX - arenaWidth / 2, y + portalWidth / 2) - Main.screenPosition;
				spriteBatch.Draw((Texture2D)TextureAssets.Projectile[ProjectileID.PortalGunGate], drawPos, frame, color, (float)Math.PI, new Vector2(portalDepth / 2, portalWidth / 2), 1f, SpriteEffects.None, 0f);
				drawPos.X += arenaWidth;
				spriteBatch.Draw((Texture2D)TextureAssets.Projectile[ProjectileID.PortalGunGate], drawPos, frame, color, 0f, new Vector2(portalDepth / 2, portalWidth / 2), 1f, SpriteEffects.None, 0f);
			}
			*/
        }
	}
}