
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
using StarsAbove.Projectiles;
using SubworldLibrary;

namespace StarsAbove.NPCs.Tsukiyomi
{
	[AutoloadBossHead]

	public class TsukiyomiBoss : ModNPC
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
			DisplayName.SetDefault("Tsukiyomi, the First Starfarer");
			
			Main.npcFrameCount[NPC.type] = 11; // make sure to set this for your modnpcs.

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
			
			//Phase 1, so no bestiary
			NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Hide = false // Hides this NPC from the bestiary
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, bestiaryData);

		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			int associatedNPCType = ModContent.NPCType<WarriorOfLight>();
			bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);

			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {

				new FlavorTextBestiaryInfoElement($"Mods.StarsAbove.Bestiary.{Name}")
			});
		}
		public override void SetDefaults()
		{
			NPC.boss = true;
			NPC.lifeMax = 1350000;
			NPC.damage = 0;
			NPC.defense = 25;
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
			DrawOffsetY = 42;

			//NPC.HitSound = SoundID.NPCHit54;
			//NPC.DeathSound = SoundID.NPCDeath52;

			Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/TheExtremeIntro");

			SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.SeaOfStarsBiome>().Type };
			NPC.netAlways = true;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return 0f;
		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			NPC.lifeMax = (int)(NPC.lifeMax * bossLifeScale * numPlayers);
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

		public override void AI()
        {
			
			var modPlayer = Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>();
            var bossPlayer = Main.LocalPlayer.GetModPlayer<BossPlayer>();

            bossPlayer.TsukiyomiBarActive = true;

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
            if (AI_Timer >= 120) //An attack is active. (Temp 480, usually 120, or 2 seconds)
            {
				if (Main.expertMode)
				{
					switch (AI_RotationNumber)
					{

						case 0:
                            AI_RotationNumber = 26;//ThreadsOfFate1(P, NPC);
							break;
						case 1:
							Anosios1(P, NPC);
							break;
						case 2:
							ThreadsOfFate2(P, NPC);
							break;
						case 3:
							ThreadsOfFate1(P, NPC);
							break;
						case 4:
							CelestialOpposition(P, NPC);
							break;
						case 5:
							HypertunedMeteorShower(P, NPC);
							break;
						case 6:
							ThreadsOfFate1(P, NPC);
							break;
						case 7:
							OriginStarfall(P, NPC);
							break;
						case 8:
							ThreadsOfFate3(P, NPC);
							break;
						case 9:
							GraspingVoid(P, NPC);
							break;
						case 10:
							HypertunedMeteorShower(P, NPC);
							break;
						case 11:
							ThreadsOfFate3(P, NPC);
							break;
						case 12:
							CosmicUpsurge(P, NPC);
							break;
						case 13:
							ThreadsOfFate1(P, NPC);
							break;
						case 14:
							OriginStarfall2(P, NPC);
							break;
						case 15:
							AI_RotationNumber = 26; //Skip ahead because this phase takes too long
							break;
						case 16:
							CelestialOpposition(P, NPC);
							break;
						case 17:
							ThreadsOfFate1(P, NPC);
							break;
						case 18:
							GraspingVoid(P, NPC);
							break;
						case 19:
							ThreadsOfFate2(P, NPC);
							break;
						case 20:
							CosmicUpsurge(P, NPC);
							break;
						case 21:
							ThreadsOfFate1(P, NPC);
							break;
						case 22:
							AethericDisruption(P, NPC);
							break;
						case 23:
							ThreadsOfFate3(P, NPC);
							break;
						case 24:
							Anosios1(P, NPC);
							break;
						case 25:
							CelestialOpposition(P, NPC);
							break;
						case 26:
							ThreadsOfFate1(P, NPC);
							break;
						case 27:
							TsukiyomiPhaseChange(P, NPC); //Transition into Phase 2
							break;
						case 28:
							TsukiyomiAspectedWeapons(P, NPC);
							break;
						case 29:
							CarianDarkMoon1(P, NPC);
							break;
						case 30:
							CosmicUpsurge(P, NPC);
							break;
						case 31:
							DeathInFourActs1(P, NPC);
							break;
						case 32:
							ThreadsOfFate1(P, NPC);
							break;
						case 33:
							StygianMemento(P, NPC);
							break;
						case 34:
							GraspingVoid(P, NPC);
							break;
						case 35:
							CaesuraOfDespair(P, NPC);
							break;
						case 36:
							Takonomicon1(P, NPC);
							break;
						case 37:
							OriginStarfall(P, NPC);
							break;
						case 38:
							ShadowlessCerulean1(P, NPC);
							break;
						case 39:
							KeyOfTheKingsLaw1(P, NPC);
							break;
						case 40:
							VoiceOfTheOutbreak(P, NPC);
							break;
						case 41:
							ThreadsOfFate2(P, NPC);
							break;
						case 42:
							HypertunedMeteorShower(P, NPC);
							break;
						case 43:
							Anosios1(P, NPC);
							break;
						case 44:
							Takonomicon2(P, NPC);
							break;
						case 45:
							ThreadsOfFate1(P, NPC);
							break;
						case 46:
							ThreadsOfFate3(P, NPC);
							break;
						case 47:
							LuminaryWand(P, NPC);
							break;
						case 48:
							CaesuraOfDespair(P, NPC);
							break;
						case 49:
							CosmicUpsurgeFast(P, NPC);
							break;
						case 50:
							DeathInFourActs1(P, NPC);
							break;
						case 51:
							CelestialOpposition(P, NPC);
							break;
						case 52:
							ThreadsOfFate2(P, NPC);
							break;
						case 53:
							ThreadsOfFate1(P, NPC);
							break;
						case 54:
							TsukiyomiPhaseChange2(P, NPC); //Final phase transition
							break;
						case 55:
							BuryTheLight1(P, NPC);
							break;
						case 56:
							CosmicUpsurgeFast(P, NPC);
							break;
						case 57:
							HypertunedKeyOfTheKingsLaw(P, NPC);
							break;
						case 58:
							TheOnlyThingIKnowForReal1(P, NPC);
							break;
						case 59:
							HypertunedStygianMemento(P, NPC);
							break;
						case 60:
							Pandaemonium1(P, NPC);
							break;
						case 61:
							CarianDarkMoon2(P, NPC);
							break;
						case 62:
							Takonomicon2(P, NPC);
							break;
						case 63:
							Anosios2(P, NPC);
							break;
						case 64:
							ShadowlessCerulean1(P, NPC);
							break;
						case 65:
							HypertunedCelestialOpposition(P, NPC);
							break;
						case 66:
							HypertunedVoiceOfTheOutbreak(P, NPC);
							break;
						case 67:
							BuryTheLight2(P, NPC);
							break;
						case 68:
							HypertunedDeathInFourActs(P, NPC);
							break;
						case 69:
							Anosios2(P, NPC);
							break;
						case 70:
							Takonomicon2(P, NPC);
							break;
						case 71:
							ThreadsOfFate1(P, NPC);
							break;
						case 72:
							ThreadsOfFate3(P, NPC);
							break;
						case 73:
							LuminaryWand(P, NPC);
							break;
						case 74:
							CarianDarkMoon2(P, NPC);
							break;
						case 75:
							CosmicUpsurgeFast(P, NPC);
							break;
						case 76:
							Pandaemonium2 (P, NPC);
							break;
						case 77:
							HypertunedCelestialOpposition(P, NPC);
							break;
						case 78:
							ThreadsOfFate2(P, NPC);
							break;
						case 79:
							BuryTheLight1(P, NPC);
							break;
						case 80:
							CaesuraOfDespair(P, NPC);
							break;
						case 81:
							TheOnlyThingIKnowForReal1(P, NPC);
							break;
						case 82:
							HypertunedKeyOfTheKingsLaw(P, NPC);
							break;
						case 83:
							ThreadsOfFate3(P, NPC);
							break;
						case 84:
							HypertunedDeathInFourActs(P, NPC);
							break;
						case 85:
							TheOnlyThingIKnowForReal1(P, NPC);
							break;
						case 86:
							CarianDarkMoon2(P, NPC);
							break;
						case 87:
							CaesuraOfDespair(P, NPC);
							break;
						case 88:
							ShadowlessCerulean1(P, NPC);
							break;
						case 89:
							OriginStarfall2(P, NPC);
							break;
						case 90:
							TheOnlyThingIKnowForReal1(P, NPC);
							break;
						case 91:
							LuminaryWand(P, NPC);
							break;
						case 92:
							Pandaemonium1(P, NPC);
							break;
						case 93:
							Anosios2(P, NPC);
							break;
						case 94:
							HypertunedStygianMemento(P, NPC);
							break;
						case 95:
							Anosios2(P, NPC);
							break;
						case 96:
							HypertunedVoiceOfTheOutbreak(P, NPC);
							break;
						case 97:
							HypertunedDeathInFourActs(P, NPC);
							break;
						case 98:
							CosmicUpsurgeFast(P, NPC);
							break;
						case 99:
							Takonomicon2(P, NPC);
							break;
						case 100:
							Pandaemonium2(P, NPC);
							break;
						case 101:
							TheOnlyThingIKnowForReal1(P, NPC);
							break;
						case 102:
							OriginStarfall2(P, NPC);
							break;
						case 103:
							HypertunedCelestialOpposition(P, NPC);
							break;
						case 104:
							CarianDarkMoon2(P, NPC);
							break;
						case 105:
							CaesuraOfDespair(P, NPC);
							break;
						case 106:
							HypertunedKeyOfTheKingsLaw(P, NPC);
							break;
						case 107:
							CosmicUpsurgeFast(P, NPC);
							break;
						case 108:
							HypertunedDeathInFourActs(P, NPC);
							break;
						default:
							AI_RotationNumber = 55;//She'll never go back to phase 1 mechanics. (usually the mechanic right after TsukiyomiAspectedWeapons)
							return;

					}
				}
				else
                {
					switch (AI_RotationNumber)
					{

						case 0:
							ThreadsOfFate1(P, NPC);
							break;
						case 1:
							Anosios1(P, NPC);
							break;
						case 2:
							ThreadsOfFate2(P, NPC);
							break;
						case 3:
							ThreadsOfFate1(P, NPC);
							break;
						case 4:
							CelestialOpposition(P, NPC);
							break;
						case 5:
							HypertunedMeteorShower(P, NPC);
							break;
						case 6:
							ThreadsOfFate1(P, NPC);
							break;
						case 7:
							OriginStarfall(P, NPC);
							break;
						case 8:
							ThreadsOfFate3(P, NPC);
							break;
						case 9:
							GraspingVoid(P, NPC);
							break;
						case 10:
							HypertunedMeteorShower(P, NPC);
							break;
						case 11:
							ThreadsOfFate3(P, NPC);
							break;
						case 12:
							CosmicUpsurge(P, NPC);
							break;
						case 13:
							ThreadsOfFate1(P, NPC);
							break;
						case 14:
							OriginStarfall2(P, NPC);
							break;
						case 15:
							AI_RotationNumber = 27; //Skip ahead because this phase takes too long
							break;
						case 16:
							CelestialOpposition(P, NPC);
							break;
						case 17:
							ThreadsOfFate1(P, NPC);
							break;
						case 18:
							GraspingVoid(P, NPC);
							break;
						case 19:
							ThreadsOfFate2(P, NPC);
							break;
						case 20:
							CosmicUpsurge(P, NPC);
							break;
						case 21:
							ThreadsOfFate1(P, NPC);
							break;
						case 22:
							AethericDisruption(P, NPC);
							break;
						case 23:
							ThreadsOfFate3(P, NPC);
							break;
						case 24:
							Anosios1(P, NPC);
							break;
						case 25:
							CelestialOpposition(P, NPC);
							break;
						case 26:
							ThreadsOfFate1(P, NPC);
							break;
						case 27:
							TsukiyomiPhaseChange(P, NPC); //Transition into Phase 2
							break;
						case 28:
							TsukiyomiAspectedWeapons(P, NPC); //Bury The Light will always follow her voice line
							break;
						case 29:
							CarianDarkMoon1(P, NPC);
							break;
						case 30:
							CosmicUpsurgeFast(P, NPC);
							break;
						case 31:
							KeyOfTheKingsLaw1(P, NPC);
							break;
						case 32:
							ThreadsOfFate1(P, NPC);
							break;
						case 33:
							StygianMemento(P, NPC);
							break;
						case 34:
							CosmicUpsurgeFast(P, NPC);
							break;
						case 35:
							CarianDarkMoon1(P, NPC);
							break;
						case 36:
							Takonomicon1(P, NPC);
							break;
						case 37:
							OriginStarfall(P, NPC);
							break;
						case 38:
							ShadowlessCerulean1(P, NPC);
							break;
						case 39:
							KeyOfTheKingsLaw1(P, NPC);
							break;
						case 40:
							VoiceOfTheOutbreak(P, NPC);
							break;
						case 41:
							BuryTheLight2(P, NPC);
							break;
						case 42:
							CelestialOpposition(P, NPC);
							break;
						case 43:
							Anosios1(P, NPC);
							break;
						case 44:
							Takonomicon1(P, NPC);
							break;
						case 45:
							Takonomicon2(P, NPC);
							break;
						case 46:
							ThreadsOfFate3(P, NPC);
							break;
						case 47:
							LuminaryWand(P, NPC);
							break;
						case 48:
							CarianDarkMoon1(P, NPC);
							break;
						case 49:
							CosmicUpsurgeFast(P, NPC);
							break;
						case 50:
							DeathInFourActs1(P, NPC);
							break;
						case 51:
							CelestialOpposition(P, NPC);
							break;
						case 52:
							ThreadsOfFate2(P, NPC);
							break;
						case 53:
							BuryTheLight1(P, NPC);
							break;
						case 54:
							CaesuraOfDespair(P, NPC);
							break;
						case 55:
							TheOnlyThingIKnowForReal1(P, NPC);
							break;
						case 56:
							KeyOfTheKingsLaw1(P, NPC);
							break;
						case 57:
							ThreadsOfFate3(P, NPC);
							break;
						case 58:
							ThreadsOfFate2(P, NPC);
							break;
						case 59:
							Takonomicon2(P, NPC);
							break;
						case 60:
							CarianDarkMoon1(P, NPC);
							break;
						case 61:
							CaesuraOfDespair(P, NPC);
							break;
						case 62:
							ShadowlessCerulean1(P, NPC);
							break;
						case 63:
							CelestialOpposition(P, NPC);
							break;
						case 64:
							OriginStarfall(P, NPC);
							break;
						case 65:
							LuminaryWand(P, NPC);
							break;
						case 66:
							ThreadsOfFate3(P, NPC);
							break;
						case 67:
							Anosios1(P, NPC);
							break;
						case 68:
							StygianMemento(P, NPC);
							break;
						case 69:
							Anosios1(P, NPC);
							break;
						case 70:
							VoiceOfTheOutbreak(P, NPC);
							break;
						case 71:
							DeathInFourActs1(P, NPC);
							break;
						case 72:
							CosmicUpsurgeFast(P, NPC);
							break;
						case 73:
							Takonomicon1(P, NPC);
							break;
						case 74:
							Takonomicon2(P, NPC);
							break;
						case 75:
							ThreadsOfFate2(P, NPC);
							break;
						case 76:
							OriginStarfall2(P, NPC);
							break;
						case 77:
							CelestialOpposition(P, NPC);
							break;
						case 78:
							CarianDarkMoon1(P, NPC);
							break;
						case 79:
							CaesuraOfDespair(P, NPC);
							break;
						case 80:
							KeyOfTheKingsLaw1(P, NPC);
							break;
						case 81:
							CosmicUpsurge(P, NPC);
							break;
						case 82:
							Anosios1(P, NPC);
							break;
						default:
							AI_RotationNumber = 29;//She'll never go back to phase 1 mechanics. (usually the mechanic right after TsukiyomiAspectedWeapons)
							return;

					}
				}

            }
			
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
						player.AddBuff(BuffType<Buffs.SubworldModifiers.MoonTurmoil>(), 10);

					}
					//If boss is in phase 2...
					if (NPC.localAI[0] == 2)
					{
						player.AddBuff(BuffType<Buffs.SubworldModifiers.ChaosTurmoil>(), 10);
						
					}
                    else
                    {
						
                    }

					player.GetModPlayer<StarsAbovePlayer>().TsukiyomiLocation = NPC.Center;
					player.GetModPlayer<StarsAbovePlayer>().lookAtTsukiyomi = true;

				}


			}
			if(Main.expertMode)
            {
				if (NPC.localAI[0] == 2)
				{
					Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Angela3");
					NPC.defense = 25;
				}
				else if (NPC.localAI[0] == 1)
				{
					Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Angela2");
					NPC.defense = 120;
				}
				else
                {
					Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Angela1");
					NPC.defense = 120;

				}
			}
			else
            {
				if (NPC.localAI[0] != 0)
				{
					Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/TheExtreme");
					NPC.defense = 5;
				}
				else
				{
					Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/TheExtremeIntro");
					NPC.defense = 120;
				}
			}
			


			//Returning from the teleport.
			int index = NPC.FindBuffIndex(BuffType<TsukiyomiTeleport>());
			if (index >= 0)
            {
				NPC.dontTakeDamage = true;
				if (NPC.buffTime[index] == 60)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{

						Projectile.NewProjectile(null, new Vector2(NPC.Center.X, NPC.Center.Y + 44), Vector2.Zero, ProjectileType<reverseRadiate>(), 0, 0f, Main.myPlayer);

					}
					
				}
				if (NPC.buffTime[index] == 1)
                {
					SoundEngine.PlaySound(SoundID.Item6, NPC.Center);

					for (int g = 0; g < 4; g++)
					{
						int goreIndex = Gore.NewGore(null, new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) + 44f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex].scale = 1.5f;
						Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
						Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
						goreIndex = Gore.NewGore(null, new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) + 44f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex].scale = 1.5f;
						Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
						Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
						goreIndex = Gore.NewGore(null, new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) + 44f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex].scale = 1.5f;
						Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
						Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
						goreIndex = Gore.NewGore(null, new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) + 44f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex].scale = 1.5f;
						Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
						Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
					}
				}
            }
			else
            {
				NPC.dontTakeDamage = false;
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
			if(NPC.HasBuff(BuffType<TsukiyomiTeleport>()))
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
			


			
			if(NPC.localAI[1] == 10)
            {
				NPC.AddBuff(BuffType<TsukiyomiTeleport>(), 240);
				SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_Stronger, NPC.Center);

				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					

					Projectile.NewProjectile(null, new Vector2(NPC.Center.X + 30, NPC.Center.Y - 35), Vector2.Zero, ProjectileType<TsukiWormhole>(), 0, 0f, Main.myPlayer);
					Projectile.NewProjectile(null, NPC.Center, Vector2.Zero, ProjectileType<TsukiTeleport>(), 0, 0f, Main.myPlayer);




				}
			}
			if (NPC.localAI[1] >= 240f)
			{
				


				DownedBossSystem.downedTsuki = true;
				
				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
				}

				SubworldSystem.Exit();

				if(modPlayer.edingenesisquasar == 0)
                {
					modPlayer.edingenesisquasar = 1;

				}
				if (modPlayer.tsukiyomiDialogue == 0)
				{
					if (Main.netMode != NetmodeID.Server) { Main.NewText(Language.GetTextValue("The Spatial Disk begins to resonate. Left click to interact."), 241, 255, 180); }
					modPlayer.tsukiyomiDialogue = 1;
				}
				
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
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Placeable.BossLoot.NalhaunTrophyItem>(), 10));

			// ItemDropRule.MasterModeCommonDrop for the relic
			npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Placeable.BossLoot.NalhaunBossRelicItem>()));

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

			if(SubworldSystem.AnyActive<StarsAbove>())
            {
				Vector2 initialMoveTo = new Vector2(14184, 6445);
				NPC.position = initialMoveTo;
			}
			else
            {
				Vector2 initialMoveTo = new Vector2(Main.player[NPC.target].Center.X - 80, Main.player[NPC.target].Center.Y - 350);
				NPC.position = initialMoveTo;
			}
			SoundEngine.PlaySound(StarsAboveAudio.Tsukiyomi_Journey, NPC.Center);



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
				for (int i = 0; i < 5; i++)
				{//Circle


					Dust d = Main.dust[Dust.NewDust(new Vector2(NPC.Center.X + 29, NPC.Center.Y + 24), 0, 2, 20, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextFloat(-0.5f, -4.5f), 20, default(Color), 0.7f)];
					d.shader = GameShaders.Armor.GetSecondaryShader(114, Main.LocalPlayer);
					d.fadeIn = 1f;
					d.noGravity = true;
				}

				for (int i = 0; i < 3; i++)
				{
					// Charging dust
					Vector2 vector = new Vector2(
						Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10,
						Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10);
					Dust d = Main.dust[Dust.NewDust(
						NPC.Center + vector, 1, 1,
						20, 0, 0, 255,
						new Color(1f, 1f, 1f), 1.5f)];
					d.shader = GameShaders.Armor.GetSecondaryShader(114, Main.LocalPlayer);
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
		//Draw the portal
		
	}
}