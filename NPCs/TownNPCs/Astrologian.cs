
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.GameContent.Bestiary;
using System.Collections.Generic;
using StarsAbove.Biomes;
using StarsAbove.Utilities;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using StarsAbove.Projectiles.Melee.SkyStriker;
using Terraria.GameContent;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Systems.Items;
using Terraria.GameContent.Personalities;
using Terraria.ModLoader.IO;
using Terraria.Audio;
using StarsAbove.Systems;
using StarsAbove.Items.Placeable.Cosmoturgy;
using StarsAbove.Items.Memories;
using StarsAbove.Items.Memories.TarotCard;

namespace StarsAbove.NPCs.TownNPCs
{
	class SpaceBiome : ILoadable, IShoppingBiome
	{
		public bool IsInBiome(Player player)
		{
			return player.ZoneSkyHeight;
		}

        public void Load(Mod mod)
        {
        }

        public void Unload()
        {
        }

        public string NameKey => "Space";
	}
	// [AutoloadHead] and NPC.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
	[AutoloadHead]
	public class Astrologian : ModNPC
	{
		public const string ShopName = "Shop";
		public int NumberOfTimesTalkedTo = 0;

		private static int ShimmerHeadIndex;
		private static Profiles.StackedNPCProfile NPCProfile;

		public override void Load()
		{
			// Adds our Shimmer Head to the NPCHeadLoader.
			//ShimmerHeadIndex = Mod.AddNPCHeadTexture(Type, Texture + "_Shimmer_Head");
		}

		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[Type] = 23; // The amount of frames the NPC has
			NPCID.Sets.ExtraFramesCount[Type] = 5; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs.
			NPCID.Sets.AttackFrameCount[Type] = 4;
			NPCID.Sets.DangerDetectRange[Type] = 700; // The amount of pixels away from the center of the NPC that it tries to attack enemies.
			NPCID.Sets.AttackType[Type] = 0; // The type of attack the Town NPC performs. 0 = throwing, 1 = shooting, 2 = magic, 3 = melee
			NPCID.Sets.AttackTime[Type] = 90; // The amount of time it takes for the NPC's attack animation to be over once it starts.
			NPCID.Sets.AttackAverageChance[Type] = 30; // The denominator for the chance for a Town NPC to attack. Lower numbers make the Town NPC appear more aggressive.
			NPCID.Sets.HatOffsetY[Type] = 4; // For when a party is active, the party hat spawns at a Y offset.
			NPCID.Sets.ShimmerTownTransform[NPC.type] = false; // This set says that the Town NPC has a Shimmered form. Otherwise, the Town NPC will become transparent when touching Shimmer like other enemies.

			NPCID.Sets.ShimmerTownTransform[Type] = false; // Allows for this NPC to have a different texture after touching the Shimmer liquid.

			// Connects this NPC with a custom emote.
			// This makes it when the NPC is in the world, other NPCs will "talk about him".
			// By setting this you don't have to override the PickEmote method for the emote to appear.
			//NPCID.Sets.FaceEmote[Type] = ModContent.EmoteBubbleType<ExamplePersonEmote>();

			// Influences how the NPC looks in the Bestiary
			NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
			{
				Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
				Direction = -1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
							  // Rotation = MathHelper.ToRadians(180) // You can also change the rotation of an NPC. Rotation is measured in radians
							  // If you want to see an example of manually modifying these when the NPC is drawn, see PreDraw
			};

			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

			// Set Example Person's biome and neighbor preferences with the NPCHappiness hook. You can add happiness text and remarks with localization (See an example in ExampleMod/Localization/en-US.lang).
			// NOTE: The following code uses chaining - a style that works due to the fact that the SetXAffection methods return the same NPCHappiness instance they're called on.
			NPC.Happiness
				.SetBiomeAffection<SpaceBiome>(AffectionLevel.Love)
				.SetBiomeAffection<OceanBiome>(AffectionLevel.Like)
				.SetBiomeAffection<HallowBiome>(AffectionLevel.Dislike)
				.SetBiomeAffection<UndergroundBiome>(AffectionLevel.Hate)

				.SetNPCAffection(NPCID.Wizard, AffectionLevel.Love)
				.SetNPCAffection(NPCID.GoblinTinkerer, AffectionLevel.Like)
				.SetNPCAffection(NPCID.Truffle, AffectionLevel.Dislike)
				.SetNPCAffection(NPCID.Pirate, AffectionLevel.Hate)
			; // < Mind the semicolon!

			// This creates a "profile" for ExamplePerson, which allows for different textures during a party and/or while the NPC is shimmered.
			/*NPCProfile = new Profiles.StackedNPCProfile(
				new Profiles.DefaultNPCProfile(Texture, NPCHeadLoader.GetHeadSlot(HeadTexture), Texture + "_Party"),
				new Profiles.DefaultNPCProfile(Texture + "_Shimmer", ShimmerHeadIndex, Texture + "_Shimmer_Party")
			);*/
		}

		public override void SetDefaults()
		{
			NPC.townNPC = true; // Sets NPC to be a Town NPC
			NPC.friendly = true; // NPC Will not attack player
			NPC.width = 18;
			NPC.height = 40;
			NPC.aiStyle = 7;
			NPC.damage = 10;
			NPC.defense = 15;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.5f;

			AnimationType = NPCID.Steampunker;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				// Sets your NPC's flavor text in the bestiary.
				//new FlavorTextBestiaryInfoElement("Hailing from a mysterious greyscale cube world, the Example Person is here to help you understand everything about tModLoader."),

				// You can add multiple elements if you really wanted to
				// You can also use localization keys (see Localization/en-US.lang)
				//new FlavorTextBestiaryInfoElement("Mods.ExampleMod.Bestiary.ExamplePerson")
			});
		}

		// The PreDraw hook is useful for drawing things before our sprite is drawn or running code before the sprite is drawn
		// Returning false will allow you to manually draw your NPC
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			/*
			// This code slowly rotates the NPC in the bestiary
			// (simply checking NPC.IsABestiaryIconDummy and incrementing NPC.Rotation won't work here as it gets overridden by drawModifiers.Rotation each tick)
			if (NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(Type, out NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers))
			{
				drawModifiers.Rotation += 0.001f;

				// Replace the existing NPCBestiaryDrawModifiers with our new one with an adjusted rotation
				NPCID.Sets.NPCBestiaryDrawOffset.Remove(Type);
				NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
			}
			*/
			return true;
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			int num = NPC.life > 0 ? 1 : 5;


			//More dust to compensate for no gore.
			for (int k = 0; k < num; k++)
			{
				//Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Sparkle>());
			}

			/*
			// Create gore when the NPC is killed.
			if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
			{
				// Retrieve the gore types. This NPC has shimmer and party variants for head, arm, and leg gore. (12 total gores)
				string variant = "";
				if (NPC.IsShimmerVariant) variant += "_Shimmer";
				if (NPC.altTexture == 1) variant += "_Party";
				int hatGore = NPC.GetPartyHatGore();
				int headGore = Mod.Find<ModGore>($"{Name}_Gore{variant}_Head").Type;
				int armGore = Mod.Find<ModGore>($"{Name}_Gore{variant}_Arm").Type;
				int legGore = Mod.Find<ModGore>($"{Name}_Gore{variant}_Leg").Type;

				// Spawn the gores. The positions of the arms and legs are lowered for a more natural look.
				if (hatGore > 0)
				{
					Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, hatGore);
				}
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, headGore, 1f);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 20), NPC.velocity, armGore);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 20), NPC.velocity, armGore);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 34), NPC.velocity, legGore);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 34), NPC.velocity, legGore);
			}*/
		}

		public override bool CanTownNPCSpawn(int numTownNPCs)
		{ // Requirements for the town NPC to spawn.
			for (int k = 0; k < Main.maxPlayers; k++)
			{
				Player player = Main.player[k];
				if (!player.active)
				{
					continue;
				}

				// After defeating Vagrant
				if (DownedBossSystem.downedVagrant || 
					player.HasItemInAnyInventory(ModContent.ItemType<ElectricGuitarPick>()) || //Early game memories
                    player.HasItemInAnyInventory(ModContent.ItemType<CapedFeather>()) ||
                    player.HasItemInAnyInventory(ModContent.ItemType<PrimeCut>()) ||
                    player.HasItemInAnyInventory(ModContent.ItemType<RuinedCrown>()) ||
                    player.HasItemInAnyInventory(ModContent.ItemType<Trumpet>()) ||
                    player.HasItemInAnyInventory(ModContent.ItemType<Pawn>()) ||
                    player.HasItemInAnyInventory(ModContent.ItemType<MonsterTooth>()) ||
                    player.HasItemInAnyInventory(ModContent.ItemType<NetheriteBar>()) ||
                    player.HasItemInAnyInventory(ModContent.ItemType<MercenaryAuracite>()) ||
                    player.HasItemInAnyInventory(ModContent.ItemType<RedSpiderLily>())
					)
                    
				{
					return true;
				}
			}

			return false;
		}

		public override ITownNPCProfile TownNPCProfile()
		{
			return NPCProfile;
		}

		public override List<string> SetNPCNameList()
		{
			return new List<string>() {
				"Asterope",
				"Maia",
				"Taygete",
				"Celaeno",
				"Alcyone",
				"Merope",
				"Electra",
				"Selene",
				"Sana",
				"Tiphereth",
				"Lunala"
			};
		}

		public override void FindFrame(int frameHeight)
		{
			/*npc.frame.Width = 40;
			if (((int)Main.time / 10) % 2 == 0)
			{
				npc.frame.X = 40;
			}
			else
			{
				npc.frame.X = 0;
			}*/
		}

		public override string GetChat()
		{
			WeightedRandom<string> chat = new WeightedRandom<string>();

			int wizard = NPC.FindFirstNPC(NPCID.Wizard);
			if (wizard >= 0 && Main.rand.NextBool(4))
			{
				chat.Add(LangHelper.GetTextValue($"NPCDialogue.Astrologian.WizardDialogue", Main.npc[wizard].GivenName));
			}
			// These are things that the NPC has a chance of telling you when you talk to it.
			chat.Add(LangHelper.GetTextValue($"NPCDialogue.Astrologian.StandardDialogue1"));
			chat.Add(LangHelper.GetTextValue($"NPCDialogue.Astrologian.StandardDialogue2"));
			chat.Add(LangHelper.GetTextValue($"NPCDialogue.Astrologian.StandardDialogue3"));
			chat.Add(LangHelper.GetTextValue($"NPCDialogue.Astrologian.StandardDialogue4"));
			chat.Add(LangHelper.GetTextValue($"NPCDialogue.Astrologian.StandardDialogue5"));
			chat.Add(LangHelper.GetTextValue($"NPCDialogue.Astrologian.StandardDialogue6"));
			chat.Add(LangHelper.GetTextValue($"NPCDialogue.Astrologian.RareDialogue1"), 0.1);
			chat.Add(LangHelper.GetTextValue($"NPCDialogue.Astrologian.RareDialogue2"), 0.1);

			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
            {
				chat.Add(LangHelper.GetTextValue($"NPCDialogue.Astrologian.AstralDialogue"), 0.2);

			}
			if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
			{
				chat.Add(LangHelper.GetTextValue($"NPCDialogue.Astrologian.UmbralDialogue"), 0.2);

			}
			if(Main.bloodMoon)
            {
				chat.Add(LangHelper.GetTextValue($"NPCDialogue.Astrologian.BloodMoon"));

			}
			if (Main.LocalPlayer.ZoneGraveyard)
            {
				chat.Add(LangHelper.GetTextValue($"NPCDialogue.Astrologian.Graveyard"));

			}
			if (Terraria.GameContent.Events.BirthdayParty.PartyIsUp)
            {
				chat.Add(LangHelper.GetTextValue($"NPCDialogue.Astrologian.Party"));

			}
			NumberOfTimesTalkedTo++;
			if (NumberOfTimesTalkedTo <= 1)
			{
				//This counter is linked to a single instance of the NPC, so if ExamplePerson is killed, the counter will reset.
				chat.Add(LangHelper.GetTextValue($"NPCDialogue.Astrologian.FirstDialogue"));
			}

			string chosenChat = chat; // chat is implicitly cast to a string. This is where the random choice is made.

			// Here is some additional logic based on the chosen chat line. In this case, we want to display an item in the corner for StandardDialogue4.
			if (chosenChat == LangHelper.GetTextValue($"NPCDialogue.Astrologian.StandardDialogue4"))
			{
				// Main.npcChatCornerItem shows a single item in the corner, like the Angler Quest chat.
				//Main.npcChatCornerItem = ItemID.HiveBackpack;
			}

			return chosenChat;
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{ // What the chat buttons are when you open up the chat UI
			button = Language.GetTextValue("LegacyInterface.28");
			button2 = LangHelper.GetTextValue("NPCDialogue.Astrologian.StrangePotion");
			/*
			button2 = "Awesomeify";
			if (Main.LocalPlayer.HasItem(ItemID.HiveBackpack))
			{
				button = "Upgrade " + Lang.GetItemNameValue(ItemID.HiveBackpack);
			}*/
		}

		public override void OnChatButtonClicked(bool firstButton, ref string shop)
		{
			if (firstButton)
			{
				// We want 3 different functionalities for chat buttons, so we use HasItem to change button 1 between a shop and upgrade action.
				/*
				if (Main.LocalPlayer.HasItem(ItemID.HiveBackpack))
				{
					SoundEngine.PlaySound(SoundID.Item37); // Reforge/Anvil sound

					Main.npcChatText = $"I upgraded your {Lang.GetItemNameValue(ItemID.HiveBackpack)} to a {Lang.GetItemNameValue(ModContent.ItemType<WaspNest>())}";

					int hiveBackpackItemIndex = Main.LocalPlayer.FindItem(ItemID.HiveBackpack);
					var entitySource = NPC.GetSource_GiftOrReward();

					Main.LocalPlayer.inventory[hiveBackpackItemIndex].TurnToAir();
					Main.LocalPlayer.QuickSpawnItem(entitySource, ModContent.ItemType<WaspNest>());

					return;
				}*/

				shop = ShopName; // Name of the shop tab we want to open.
			}
			else
            {
				//Boss
				//If World evil boss is defeated, continue or else "you aren't ready yet"
				if(NPC.downedBoss2)
                {
					Main.npcChatText = LangHelper.GetTextValue("NPCDialogue.Astrologian.ThespianReady");
					//Give player the boss summon item.
					//Main.npcChatCornerItem = ItemID.HiveBackpack;
					//Main.LocalPlayer.QuickSpawnItem(entitySource, ModContent.ItemType<WaspNest>());
				}
				else
                {
                    if (!WorldGen.crimson)
                    {
						Main.npcChatText = LangHelper.GetTextValue("NPCDialogue.Astrologian.ThespianNotReadyCorruption");

					}
					else
					{
						Main.npcChatText = LangHelper.GetTextValue("NPCDialogue.Astrologian.ThespianNotReadyCrimson");

						//crimson
					}
				}

			}
		}

		// Not completely finished, but below is what the NPC will sell
		public override void AddShops()
		{

			var npcShop = new NPCShop(Type, ShopName)
				.Add<CosmoturgyStationItem>()
				.Add<TarotCard>()
				.Add<KnightsShovelhead>()
                .Add<ChoiceGlasses>(Condition.InExpertMode)
                .Add<ReprintedBlueprint>(Condition.DownedEyeOfCthulhu)
                .Add<MatterManipulator>(Condition.DownedEarlygameBoss)
                .Add<AetherBarrel>(Condition.DownedSkeletron)
                .Add<MagicSigil>(Condition.Hardmode)
				.Add<MeleeSigil>(Condition.Hardmode)
                .Add<RangedSigil>(Condition.Hardmode)
                .Add<SummonSigil>(Condition.Hardmode)
                .Add<OnyxJackal>(Condition.DownedQueenSlime)
                .Add<SimulacraShifter>(Condition.DownedDestroyer)
                .Add<NookMilesTicket>(Condition.DownedPlantera)
				.Add<SigilOfHope>(Condition.DownedMoonLord)
				;

			/*
			if (ModContent.GetInstance<ExampleModConfig>().ExampleWingsToggle)
			{
				npcShop.Add<ExampleWings>(ExampleConditions.InExampleBiome);
			}

			if (ModContent.TryFind("SummonersAssociation/BloodTalisman", out ModItem bloodTalisman))
			{
				npcShop.Add(bloodTalisman.Type);
			}*/
			npcShop.Register(); // Name of this shop tab
		}

		public override void ModifyActiveShop(string shopName, Item[] items)
		{
			foreach (Item item in items)
			{
				// Skip 'air' items and null items.
				if (item == null || item.type == ItemID.None)
				{
					continue;
				}

				/*
				// If NPC is shimmered then reduce all prices by 50%.
				if (NPC.IsShimmerVariant)
				{
					int value = item.shopCustomPrice ?? item.value;
					item.shopCustomPrice = value / 2;
				}*/
			}
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			//npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ExampleCostume>()));
		}

		// Make this Town NPC teleport to the King and/or Queen statue when triggered. Return toKingStatue for only King Statues. Return !toKingStatue for only Queen Statues. Return true for both.
		public override bool CanGoToStatue(bool toKingStatue) => true;

		// Make something happen when the npc teleports to a statue. Since this method only runs server side, any visual effects like dusts or gores have to be synced across all clients manually.
		public override void OnGoToStatue(bool toKingStatue)
		{
			/*
			if (Main.netMode == NetmodeID.Server)
			{
				ModPacket packet = Mod.GetPacket();
				packet.Write((byte)ExampleMod.MessageType.ExampleTeleportToStatue);
				packet.Write((byte)NPC.whoAmI);
				packet.Send();
			}
			else
			{
				StatueTeleport();
			}*/
		}

		
		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 20;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 30;
			randExtraCooldown = 30;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = ProjectileID.Starfury;//ModContent.ProjectileType<SparklingBall>();
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 12f;
			randomOffset = 2f;
			// SparklingBall is not affected by gravity, so gravityCorrection is left alone.
		}

		public override void LoadData(TagCompound tag)
		{
			//Maybe save the amount of purchases?
			NumberOfTimesTalkedTo = tag.GetInt("numberOfTimesTalkedTo");
		}

		public override void SaveData(TagCompound tag)
		{
			tag["numberOfTimesTalkedTo"] = NumberOfTimesTalkedTo;
		}

		// Let the NPC "talk about" minion boss
		/*
		public override int? PickEmote(Player closestPlayer, List<int> emoteList, WorldUIAnchor otherAnchor)
		{
			// By default this NPC will have a chance to use the Minion Boss Emote even if Minion Boss is not downed yet
			int type = ModContent.EmoteBubbleType<MinionBossEmote>();
			// If the NPC is talking to the Demolitionist, it will be more likely to react with angry emote
			if (otherAnchor.entity is NPC { type: NPCID.Demolitionist })
			{
				type = EmoteID.EmotionAnger;
			}

			// Make the selection more likely by adding it to the list multiple times
			for (int i = 0; i < 4; i++)
			{
				emoteList.Add(type);
			}

			// Use this or return null if you don't want to override the emote selection totally
			return base.PickEmote(closestPlayer, emoteList, otherAnchor);
		}
		*/
	}
}