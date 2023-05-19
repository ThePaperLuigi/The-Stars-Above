
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace StarsAbove.Items.Consumables
{

    public class DebugDisk : ModItem
	{
		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Debug Disk");
			/* Tooltip.SetDefault("Resets all player data from The Stars Above" +
				"\nIncludes Starfarer, Stellar Array passives, dialogue, and Stellar Nova" +
				"\nAffects all active players" +
				"\nDoes not work during a Cosmic Voyage" +
				"\nMay cause issues; this is a debug item"); */
			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13; // This helps sort inventory know this is a boss summoning item.

		}

		public override void SetDefaults() {
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Red;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
			Item.ResearchUnlockCount = 0;
		}

		// We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.

		public override void HoldItem(Player player)
		{
			
		}

		public override bool CanUseItem(Player player) {

			return true;
		}

		public override bool? UseItem(Player player) {
			

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStarfarer = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MurasamaWeaponDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().NeedlepointWeaponDialogue = 0;

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().EyeBossWeaponDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().CorruptBossWeaponDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().slimeDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().eyeDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().corruptBossDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().BeeBossDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SkeletonDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().WallOfFleshDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().TwinsDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().DestroyerDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SkeletronPrimeDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().AllMechsDefeatedDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().PlanteraDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().GolemDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().DukeFishronDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().CultistDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MoonLordDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().WarriorOfLightDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().vagrantDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().nalhaunDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().AllVanillaBossesDefeatedDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().EverythingDefeatedDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SkeletonDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SkeletonWeaponDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().HellWeaponDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().QueenBeeWeaponDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().KingSlimeWeaponDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().WallOfFleshWeaponDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MechBossWeaponDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().AllMechBossWeaponDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().PlanteraWeaponDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().GolemWeaponDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().LunaticCultistWeaponDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MoonLordWeaponDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().WarriorWeaponDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().VagrantWeaponDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().PenthesileaWeaponDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().penthDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().arbiterDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().DukeFishronWeaponDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().nalhaunBossItemDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().penthBossItemDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().arbiterBossItemDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().warriorBossItemDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().tsukiyomiDialogue = 0;
			//calamity
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().desertscourgeDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().crabulonDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().hivemindDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().perforatorDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().slimegodDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().cryogenDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aquaticscourgeDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().brimstoneelementalDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().calamitasDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().leviathanDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().astrumaureusDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().plaguebringerDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ravagerDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().astrumdeusDialogue = 0;

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenMutant = false;

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenEyeOfCthulhu= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenKingSlime= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenEaterOfWorlds= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenBrainOfCthulhu= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenQueenBee= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenSkeletron= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenWallOfFlesh= false;
			//Post hardmode
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenTwins= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenDestroyer= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenSkeletronPrime= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenPlantera= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenGolem= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenDukeFishron= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenCultist= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenMoonLord= false;

			//Custom bosses
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenWarriorOfLight = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenVagrant = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenNalhaun = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenPenth = false;
			//Calamity bosses
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenDesertScourge = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenCrabulon = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenHiveMind = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenPerforators = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenSlimeGod = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenCryogen = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenAquaticScourge = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenBrimstoneElemental = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenCalamitas = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenLeviathan = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenAstrumAureus = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenPlaguebringer = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenRavager = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenAstrumDeus = false;
			//Biomes
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>(). seenDesertBiome= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>(). seenSnowBiome= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>(). seenCorruptionBiome= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>(). seenCrimsonBiome= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>(). seenJungleBiome= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>(). seenDungeonBiome= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>(). seenGlowingMushroomBiome= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>(). seenBeachBiome= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>(). seenMeteoriteBiome= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>(). seenHallowBiome= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>(). seenSpaceBiome= false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>(). seenUnderworldBiome= false;

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenSulphurSeaBiome = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenCragBiome = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenAstralBiome = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenSunkenSeaBiome = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenAbyssBiome = false;

			//Weathers (This doesn't save and resets upon relogging.)
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenRain = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenSnow = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenSandstorm = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starfarerIntro = true;

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().starshower = 0; //
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ironskin = 0; //
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().evasionmastery = 0;//
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aquaaffinity = 0;//
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().inneralchemy = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().hikari = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().livingdead = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().umbralentropy = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().celestialevanesence = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().mysticforging = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().butchersdozen = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().flashfreeze = 0;
			//Tier 2
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>(). healthyConfidence = 0;//
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().bloomingflames = 0;//
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().astralmantle = 0;//
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().avataroflight = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().afterburner = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().weaknessexploit = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().artofwar = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().aprismatism = 0;
			//Tier 3
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().beyondinfinity = 0;//
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().keyofchronology = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().unbridledradiance = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().beyondtheboundary = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().stellarGauge = 0;

			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().chosenStellarNova = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().theofania = 0; //0 = LOCKED, 1 = UNLOCKED, 2 = SELECTED
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().laevateinn = 0; //0 = LOCKED, 1 = UNLOCKED, 2 = SELECTED
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().kiwamiryuken = 0; //0 = LOCKED, 1 = UNLOCKED, 2 = SELECTED 2 does not matter really
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().gardenofavalon = 0; //0 = LOCKED, 1 = UNLOCKED, 2 = SELECTED 2 does not matter really
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().edingenesisquasar = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().novaGaugeUnlocked = false;

			//Subworlds
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().observatoryDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().cosmicVoyageDialogue = 0;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenObservatory = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenCygnusAsteroids = false;
			Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().seenBleachedPlanet = false;


			//Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().savedNovaGaugeLocation = Microsoft.Xna.Framework.Vector2.Zero;

			return true;
		}
		public override void AddRecipes()
		{
		
		}
	}
}