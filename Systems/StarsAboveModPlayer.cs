using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.IO;
using StarsAbove.Items;
using StarsAbove.Projectiles;
using StarsAbove.Buffs;
using StarsAbove.NPCs;
using Microsoft.Xna.Framework.Audio;

using StarsAbove.Dusts;
using StarsAbove.Items.Consumables;
using StarsAbove.UI.StellarNova;
using SubworldLibrary;
using StarsAbove.Buffs.SubworldModifiers;
using StarsAbove.Projectiles.Otherworld;
using StarsAbove.Projectiles.SkyStriker;
using StarsAbove.Buffs.CosmicDestroyer;
using StarsAbove.Buffs.CarianDarkMoon;
using StarsAbove.Buffs.AshenAmbition;
using StarsAbove.Biomes;
using StarsAbove.Buffs.TheOnlyThingIKnowForReal;
using StarsAbove.Buffs.VermillionDaemon;
using StarsAbove.Subworlds;
using StarsAbove.Buffs.Ozma;
using StarsAbove.Prefixes;
using StarsAbove.UI.StarfarerMenu;
using StarsAbove.Buffs.StarfarerAttire;
using StarsAbove.Buffs.HunterSymphony;
using StarsAbove.Buffs.RedMage;
using StarsAbove.Buffs.TagDamage;
using StarsAbove.Buffs.BurningDesire;
using StarsAbove.Utilities;
using StarsAbove.Buffs.CatalystMemory;
using StarsAbove.Items.Armor.StarfarerArmor;
using StarsAbove.Buffs.Farewells;

namespace StarsAbove
{
    public class StarsAbovePlayer : ModPlayer
    {

        public int firstJoinedWorld = 0;//Sets the world so progress doesn't get overwritten by joining other worlds.
        public string firstJoinedWorldName;
        public static bool enableWorldLock = false;

       

        public static bool BossEnemySpawnModDisabled = false;

        public bool TruesilverSlashing = false;
        public bool LivingDead = false;
        public bool darkHourglass = false;
        public bool willpowerOfZagreus = false;
        public int whisperShotCount = 0;
        public int izanagiPerfect = 0;
        public bool edgeHoned = false;
        public bool catalyzedWeapon = false;
        public bool enigmaticCatalyst = false;
        public bool celestialFoci = false;
        public bool celesteBlessing = false;
        public int catalystBonus = 0;
        public bool naganadelWeapon1Summoned;
        public bool naganadelWeapon2Summoned;
        public bool naganadelWeapon3Summoned;
        public bool naganadelWeapon4Summoned;
        public bool naganadelWeapon5Summoned;
        public Vector2 naganadelWeaponPosition;
        public bool farEdgeOfFateKillDrone;
        public float farEdgeOfFateDronePositionX;
        public float farEdgeOfFateDronePositionY;
        public int starblessedCooldown;
        public bool rexLapisSpear;
        public bool corn;
        public bool yunlaiTeleport;
        public bool phantomTeleport;
        public bool phantomKill;
        public Vector2 phantomSavedPosition;

        public int screenShakeTimerGlobal = -1000;

        //Accessories
        public bool luciferium;
        public bool Glitterglue;
        public bool DragonwardTalisman;
        public bool AlienCoral;
        public bool ToMurder;
        public bool PerfectlyGenericAccessory;
        public bool GaleflameFeather;
        public bool InertCoating;

        //Item specific resources
        //Veneration of Butterflies
        public int ButterflyResourceCurrent = 0;
        public const int ButterflyResourceMax = 100;
        //Rhythm Gauge
        public int RhythmTiming = 0;
        public int RhythmCombo;
        public const int RhythmTimingMax = 100;
        //Rad Gun
        public bool RadReload = false;
        public int RadBullets = 0;
        public const int RadBulletsMax = 12;

        public float RadTimer = 0;
        public bool RadTimerEnabled = false;

        //Suistrume
        public float stellarPerformancePrepTimer = 0;
        public bool stellarPerformanceStart = false;
        public bool stellarPerformanceActive = false;
        public bool stellarPerformancePrep = false;
        public bool stellarPerformancePostPrep = false;
        public bool stellarPerformanceEnding = false;
        public int stellarPerformanceSongTimer = 0;
        public int PerformanceResourceCurrent = 0;
        public float stellarPerformanceClosingIn = 1000;
        private float stellarPerformancePulseRadius = 0;
        private bool stellarPerformanceIndicator = false;
        public int stellarPerformanceDepletion;

        public bool stellarPerformanceCooldown = false;

        public const int PerformanceResourceMax = 100;
        public SoundEffectInstance stellarPerformanceSoundInstance;

        //Hullwrought
        public int empoweredHullwroughtShot;
        public int savedHullwroughtShot;

        //Burning Desire (Blaze's Chainsaw)
        public bool BurningDesireHeld;
        public int powerStrikeStacks;
        public int boilingBloodDamage;

        //Catalyst's Memory
        public int CatalystPrismicHP;
        public int CatalystMemoryProgress;
        public Vector2 CatalystPrismicPosition;

        //Golden Katana (Aurum Edge?)
        public bool GoldenKatanaHeld;

        //Irminsul's Dream
        public bool IrminsulHeld;
        public bool IrminsulAttackActive;
        public Vector2 IrminsulBoxStart;
        public Vector2 IrminsulBoxEnd;

        //Warrior of Light code //////////////////////////////////////////////////////////////////////////////////////////////////////////


        public bool WarriorBarActive = false;
        public int WarriorCastTime = 0;
        public int WarriorCastTimeMax = 100;
        public bool WarriorOfLightActive = false;
        public string WarriorOfLightNextAttack = "";
        public bool LostToWarriorOfLight = false;

        public int inWarriorOfLightFightTimer;

        public Vector2 WarriorLocation;
        public bool lookAtWarrior;

        public bool damageTakenInUndertale = false;
        public bool undertaleActive;
        public bool undertalePrep;
        public int undertaleiFrames = 120;


        public int heartX;
        public int heartY;
        public int oldHeartX;
        public int oldHeartY;

        //Nalhaun code //////////////////////////////////////////////////////////////////////////////////////////////////////////


        public bool NalhaunBarActive = false;
        public int NalhaunCastTime = 0;
        public int NalhaunCastTimeMax = 100;
        public bool NalhaunActive = false;
        public string NalhaunNextAttack = "";
        public bool LostToNalhaun = false;

        public bool isNalhaunInvincible = false;

        public int inNalhaunFightTimer;

        public int lifeForceTimer;
        public int lifeforce = 100;


        //Vagrant code //////////////////////////////////////////////////////////////////////////////////////////////////////////


        public bool VagrantBarActive = false;
        public int VagrantCastTime = 0;
        public int VagrantCastTimeMax = 100;
        public bool VagrantActive = false;
        public string VagrantNextAttack = "";
        public bool LostToVagrant = false;
        public int vagrantTimeLeft;


        public int inVagrantFightTimer;

        //Penthesilea code //////////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool PenthBarActive = false;
        public int PenthCastTime = 0;
        public int PenthCastTimeMax = 100;
        public bool PenthActive = false;
        public string PenthNextAttack = "";
        public bool LostToPenth = false;

        public bool BluePaint;
        public bool RedPaint;
        public bool YellowPaint;

        public int inPenthFightTimer;


        //Arbitration code //////////////////////////////////////////////////////////////////////////////////////////////////////////


        public bool ArbiterBarActive = false;
        public int ArbiterCastTime = 0;
        public int ArbiterCastTimeMax = 100;
        public bool ArbiterActive = false;
        public string ArbiterNextAttack = "";
        public bool LostToArbiter = false;
        public int ArbiterTimeLeft;

        public int ArbiterPhase;
        public bool IsVoidActive = false;

        public int inArbiterFightTimer;

        //Tsukiyomi code //////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool TsukiyomiBarActive = false;
        public int TsukiyomiCastTime = 0;
        public int TsukiyomiCastTimeMax = 100;
        public bool TsukiyomiActive = false;
        public string TsukiyomiNextAttack = "";
        public bool LostToTsukiyomi = false;
        public int TsukiyomiTimeLeft;

        public int TsukiyomiPhase;

        public Vector2 TsukiyomiLocation;
        public bool lookAtTsukiyomi;
        public float tsukiyomiCameraFloat;
        Vector2 screenCache;

        public bool playerMoveAfterRespawnTsukiyomi = false;

        public bool tsukiyomiPrompt1 = false; //
        public bool tsukiyomiPrompt2 = false; //
        public bool tsukiyomiPrompt3 = false; //
        public bool tsukiyomiPrompt4 = false; // 
        public bool tsukiyomiPrompt5 = false; //

        public bool tsukiyomiPrompt6 = false;//Astral
        public bool tsukiyomiPrompt7 = false;//Umbral

        public bool seenTsukiyomiIntro = false;//Not saved, but will skip the intro dialogue if active. 

        public int inTsukiyomiFightTimer;
        //End of boss variables

        //Spatial Disk (New prompts, avoids chat bloat)
        public bool NewDiskDialogue;//Turns to true whenever the Spatial Disk resonantes originally.
        //Will be set to false when either the specific dialogue is read, or the array/nova menu is accessed.

        public bool NewStellarArrayAbility;//Becomes true whenever a new Stellar Array ability is unlocked.
        //Becomes false when opening the Stellar Array.
        public bool NewStellarNova;//Becomes true whenever a new Stellar Nova is unlocked.
        //Becomes false when opening the Stellar Nova Menu.

        //Light Monolith
        public bool lightMonolith;

        //All Charged Bow code
        public float bowCharge = 0;
        public const int bowChargeMax = 100;
        public bool bowChargeActive = false;

        public float overCharge1 = 0;
        public float overCharge2 = 0;

        //powderGaugeCode ( Kroniic Principality )
        public int powderGauge = 0;
        public const int powderGaugeMax = 100;
        public bool powderGaugeIndicatorOn = false;

        public int kroniicHeld;

        public bool kroniicTeleport;
        public Vector2 kroniicSavedPosition;
        public int kroniicSavedHP;
        public int kroniicSavedMP;
        public int kroniicTimer;

        //Hawkmoon Code
        public bool hawkmoonPerfectReload = false;
        public float hawkmoonReloadTimer = 0;
        public bool hawkmoonReloadTimerEnabled = false;

        public int hawkmoonRounds;

        public int hawkmoonGauge = 0;
        public const int hawkmoonGaugeMax = 0;
        public bool hawkmoonGaugeOn = false;
        public int hawkmoonGaugeAnimateIn = 0;
        public int hawkmoonGaugeAnimateOut = 0;

        //Bury The Light
        public int judgementCutTimer = -1000;
        public bool judgementCut = false;

        public int judgementGauge = 0;
        public const int judgementGaugeMax = 100;
        public bool judgementGaugeOn = false;
        public int judgementGaugeVisibility = 0;

        //Aegis Gauge
        public int aegisGauge = 0;

        //Seraphic weapons
        public int radiance = 0;

        //Kazimierz Seraphim
        public int seraphimHeld = 0;

        //Key of the Sinner
        public bool SatanaelMinion = false;

        //Vermilion Riposte
        public int blackMana;
        public int whiteMana;
        public int blackManaDrain;
        public int whiteManaDrain;
        public int manaStack;
        public int manaStackGain;
        public int manaStackGainDelay;

        //Takodachi weapon
        public bool TakodachiMinion = false;
        public int takodachiGauge = 0;
        public Vector2 takoMinionTarget;
        public Vector2 takoTarget;

        //Sparkblossom's Beacon
        public bool FleetingSparkMinion = false;

        //Arachnid Needlepoint
        public bool RobotSpiderMinion = false;

        //The Morning Star
        public bool AlucardSwordMinion1 = false;
        public bool AlucardSwordMinion2 = false;
        public bool AlucardSwordMinion3 = false;

        //Youmu weapon
        public bool YoumuMinion = false;

        //Kifrosse
        public bool Kifrosse1 = false;
        public bool Kifrosse2 = false;
        public bool Kifrosse3 = false;
        public bool Kifrosse4 = false;
        public bool Kifrosse5 = false;
        public bool Kifrosse6 = false;
        public bool Kifrosse7 = false;
        public bool Kifrosse8 = false;
        public bool Kifrosse9 = false;

        public Vector2 KifrossePosition;

        public int activeMinions;

        public bool activateShockwaveEffect = false;
        public bool activateUltimaShockwaveEffect = false;
        public bool activateBlackHoleShockwaveEffect = false;

        //Virtue's Edge
        public int VirtueGauge;
        public int VirtueMode;
        public Vector2 BlackHolePosition;

        //Crimson Sakura Alpha
        public bool sakuraHeld;
        public bool bladeWill = false;

        //Hollowheart Albion
        public int albionHeld = 0;
        public Vector2 arondightPosition;
        public Vector2 melusinePosition;

        //Vision of Euthymia

        public bool euthymiaActive;
        public int euthymiaCooldown;
        public int eternityGauge;

        //Liberation Blazing

        //Force of Nature
        public int forceBullets = 2;

        //Genocide
        public int genocideBullets = 6;

        //Unforgotten
        public bool soulUnboundActive;
        public Vector2 soulUnboundLocation;
        public int soulUnboundDamage;

        //Shadowless Cerulean
        public int ceruleanFlameGauge;
        //El Capitan's Hardware
        public int renegadeGauge;

        //Luminary Wand
        public Vector2 lumaPosition;

        //Architect's Luminance
        public Vector2 sirenCenter;
        public Vector2 sirenCenterAdjusted;
        public Vector2 sirenTurretCenter1;
        public Vector2 sirenTurretCenter2;
        public Vector2 sirenTurretCenter3;
        public Vector2 sirenTarget;
        public Vector2 sirenEnemy;

        //Soul Reaver
        public bool SoulReaverHeld;
        public int SoulReaverSouls;

        //Stygian Nymph
        public int duality = 100;

        //Penthesilea's Muse
        public bool paintVisible = false;
        public int chosenColor = 0;//0 = red | 1 = orange | 2 = yellow | 3 = green | 4 = blue | 5 = purple

        public Vector2 inkedFoePosition;
        public bool paintTargetActive;
        public int targetPaintColor;

        //Twin Stars of Albeiro
        public Vector2 starPosition1;
        public Vector2 starPosition2;
        public Vector2 starTarget;
        public Vector2 starTarget2;

        //Sky Striker Weapon
        public int SkyStrikerForm = 1; //1 is Afterburner | 2 is Shield | 3 is Punch | 4 is Railgun (Should be reset in UpdateEffects so you don't stay in the form)
        public bool SkyStrikerMenuVisible = false;
        public bool SkyStrikerHeld = false;
        public bool SkyStrikerTransformPrep = false;
        public int SkyStrikerCombo = 0;

        //Cosmic Destroyer
        public int CosmicDestroyerGauge;
        public int CosmicDestroyerRounds;
        public float CosmicDestroyerGaugeVisibility = 0f;

        //Ashen Ambition
        public int AshenAmbitionExecuteThreshold = 220;
        public Vector2 AshenAmbitionOldPosition;
        public bool AshenExecuteKilled;
        public int CallOfTheVoid;
        public bool AshenAmbitionHeld;

        //Eternal Star
        public int EternalGauge;

        //Combo code
        public int combo;
        public int comboCooldown;
        public int comboDecay;

        //Vermillion Daemon
        public int DaemonGlobalRotation;
        public int SpectralArsenal;
        public bool VermillionDaemonHeld;

        //Sakura's Vengeance
        public int VengeanceGauge;
        public bool SakuraVengeanceHeld;
        public int VengeanceElementTimer;
        public int VengeanceElement;//0 Earth 1 Heavenly 2 Cooling 3 Volcano

        //Ozma Ascendant
        public bool OzmaHeld;
        public int OzmaSpikeVFXProgression;
        //The higher this variable is from 0-100, the further the progress of swapping between the weapon on
        //the player's back and the projectile that's being fired.

        //Urgot Weapon
        public bool ChemtankHeld;

        //Hunter's Symphony
        public bool HunterSymphonyHeld;
        public int SymphonySongsPlayed;
        public int HunterSongPlaying;

        public int GlobalRotation;

        //Kevesi and Agnian Farewells
        public bool KevesiFarewellInInventory;
        public bool AgnianFarewellInInventory;

        //Starfarers ///////////////////////////////////////////////////////////////////////////////////////////////////
        public float StarfarerSelectionVisibility = 0f;

        public int AsphodeneX = 0;
        public int AsphodeneXVelocity = 0;
        public bool AsphodeneHighlighted = false;

        public int EridaniX = 0;
        public int EridaniXVelocity = 0;
        public bool EridaniHighlighted = false;

        public int chosenStarfarer = 0; //0 = Not chosen || 1 = Asphodene || 2 = Eridani

        public int starfarerOutfit = 0; //0 Starter Outfit
        public int starfarerOutfitVanity = 0;//Vanity Outfit
        public int starfarerOutfitVisible = 0;//The visible outfit

        //Starfarer outfit buff variables
        public int hopesBrilliance = 0;
        public int hopesBrillianceMax = 100;

        public Item starfarerArmorEquipped;
        public Item starfarerVanityEquipped;

        public float starfarerDialogueVisibility = 0f;
        public bool starfarerDialogue = false;

        //public int starfarerDialogueTimer = 0;
        public bool chosenStarfarerEffect = false;

        public int chosenDialogue = 0;//This determines which dialogue option you get.
        public int dialogueLeft = 1;//This is the number of pages a dialogue option has. If it reaches 0, the box will close. It depends on chosenDialogue.
        public int expression = 0;//This is the expression relating to both the current chosenDialogue and the current dialogueLeft.
                                  //0 Neutral | 1 Dissatisfied | 2 Angry | 3 Smug | 4 Questioning | 5 Sigh | 6 Intrigued
        public bool dialoguePrep;// This will be flipped to 'false' once dialogueLeft has been established.
        public string dialogue = ""; //Depending on the chosen Starfarer, this will be the string of dialogue pushed to the Text GUI.
                                     //Keep account of dialogueLeft and tie it to that + chosenDialogue

        static public bool instantText = false;

        public string animatedDialogue = "";

        public int dialogueScrollTimer;//Reset whenever you click 'next dialogue' and uses a Substring to increment the text, playing a sound effect for each letter.
        public int dialogueScrollNumber;//Increments from dialogue Scroll Timer, this is the input to the Substring

        public string animatedPromptDialogue = "";
        public int promptDialogueScrollTimer = 0;
        public int promptDialogueScrollNumber = 0;

        public string animatedDescription = "";
        public int novaDialogueScrollTimer = 0;
        public int novaDialogueScrollNumber = 0;

        public string starfarerMenuDialogue = "";

        public string animatedStarfarerMenuDialogue = "";
        public int starfarerMenuDialogueScrollTimer = 0;
        public int starfarerMenuDialogueScrollNumber = 0;

        public string novaGaugeDescription = " ";
        public string novaGaugeDescriptionActive = " ";

        static public int dialogueScrollTimerMax;//Settings thing
        static public int dialogueAudio;

        public string novaDialogue;

        public int uniqueDialogueTimer = 0; //Every time the player gets a unique idle line, for the next 60 seconds, the Starfarer will use the generic idle line. (1800-3600 ticks.)

        public int blinkTimer = 0;

        //Visual Novel code

        public float starfarerVNDialogueVisibility;

        public int sceneID;
        public int sceneProgression;
        public int sceneLength;

        public bool VNDialogueActive;
        public bool VNDialogueThirdOption;

        public string VNCharacter1;
        public int VNCharacter1Pose;
        public int VNCharacter1Expression;

        public string VNCharacter2 = "None";
        public int VNCharacter2Pose;
        public int VNCharacter2Expression;

        public string VNDialogueVisibleName;

        public float MainSpeaker;//Visibility of the speaker.
        public float MainSpeaker2;

        public bool VNDialogueChoiceActive = false;

        public string VNDialogueChoice1 = "";
        public string VNDialogueChoice2 = "";
        public string VNDialogueChoice3 = "";


        //Shockwave
        private int rippleCount = 1;
        private int rippleSize = 15;
        private int rippleSpeed = 15;
        private float distortStrength = 50f;

        int shockwaveProgress = 400;

        public int screenShakeVelocity = 1000;


        //Unique dialogue lines
        public bool starfarerIntro = true;
        //One time dialogue lines (0 Not ready to be read | 1 Ready to be read | 2 has been read)
        //Boss lines
        public int slimeDialogue = 0;
        public int eyeDialogue = 0;
        public int corruptBossDialogue = 0;
        public int BeeBossDialogue = 0;
        public int SkeletonDialogue = 0;
        public int DeerclopsDialogue = 0;
        public int WallOfFleshDialogue = 0;
        public int QueenSlimeDialogue = 0;
        public int TwinsDialogue = 0;
        public int DestroyerDialogue = 0;
        public int SkeletronPrimeDialogue = 0;
        public int AllMechsDefeatedDialogue = 0;
        public int PlanteraDialogue = 0;
        public int DukeFishronDialogue = 0;
        public int GolemDialogue = 0;
        public int EmpressDialogue = 0;
        public int CultistDialogue = 0;
        public int MoonLordDialogue = 0;
        public int WarriorOfLightDialogue = 0;
        public int AllVanillaBossesDefeatedDialogue = 0;
        public int EverythingDefeatedDialogue = 0;
        public int vagrantDialogue = 0;
        public int nalhaunDialogue = 0;
        public int penthDialogue = 0;
        public int arbiterDialogue = 0;
        public int tsukiyomiDialogue = 0;

        public int nalhaunBossItemDialogue = 0;
        public int penthBossItemDialogue = 0;
        public int arbiterBossItemDialogue = 0;
        public int warriorBossItemDialogue = 0;

        public int allModdedBosses = 0;

        //Calamity dialogues (Pre Hardmode)
        public int desertscourgeDialogue = 0;
        public int crabulonDialogue = 0;
        public int perforatorDialogue = 0;
        public int hivemindDialogue = 0;
        public int slimegodDialogue = 0;

        //Calamity dialogues (Hardmode)
        public int cryogenDialogue = 0;
        public int aquaticscourgeDialogue = 0;
        public int brimstoneelementalDialogue = 0;
        public int calamitasDialogue = 0;
        public int leviathanDialogue = 0;
        public int astrumaureusDialogue = 0;
        public int plaguebringerDialogue = 0;
        public int ravagerDialogue = 0;
        public int astrumdeusDialogue = 0;

        //Post Hardmode
        public int profanedGuardianDialogue;
        public int dragonfollyDialogue;
        public int providenceDialogue;
        public int stormWeaverDialogue;
        public int ceaselessVoidDialogue;
        public int signusDialogue;
        public int polterghastDialogue;
        public int oldDukeDialogue;
        public int destroyerOfGodsDialogue;
        public int yharonDialogue;
        public int yharonDespawnDialogue;
        public int supremeCalamitasDialogue;



        //Weapon lines
        public int WeaponDialogueTimer = Main.rand.Next(3600, 7200);

        public int EyeBossWeaponDialogue = 0;
        public int CorruptBossWeaponDialogue = 0;
        public int SkeletonWeaponDialogue = 0;
        public int HellWeaponDialogue = 0;//After reaching the Underworld for the first time after Skeletron has been slain.
        public int QueenBeeWeaponDialogue = 0;//After talking with the Starfarer about the Hell Weapons
        public int KingSlimeWeaponDialogue = 0;//After KingSlimeWeaponDialogue
        public int WallOfFleshWeaponDialogue = 0;//After Wall of Flesh Dialogue
        public int MechBossWeaponDialogue = 0;//After first Mech is killed
        public int AllMechBossWeaponDialogue = 0;//After all Mechs are killed
        public int PlanteraWeaponDialogue = 0;//After Plantera is Killed
        public int GolemWeaponDialogue = 0;//After Golem is killed
        public int DukeFishronWeaponDialogue = 0;//After Duke Fishron is killed
        public int LunaticCultistWeaponDialogue = 0;//After Lunatic Cultist is killed
        public int MoonLordWeaponDialogue = 0;//After Moon Lord is killed
        public int WarriorWeaponDialogue = 0;
        public int AllVanillaBossesDefeatedWeaponDialogue = 0;
        public int EverythingDefeatedWeaponDialogue = 0;
        public int VagrantWeaponDialogue = 0;
        public int NalhaunWeaponDialogue = 0;
        public int PenthesileaWeaponDialogue = 0;
        public int ArbitrationWeaponDialogue = 0;
        public int QueenSlimeWeaponDialogue = 0;

        public int MiseryWeaponDialogue = 0;
        public int HullwroughtWeaponDialogue = 0;
        public int ShadowlessWeaponDialogue = 0;
        public int OceanWeaponDialogue = 0;
        public int LumaWeaponDialogue = 0;
        public int MonadoWeaponDialogue = 0;
        public int FrostMoonWeaponDialogue = 0;
        public int ClaimhWeaponDialogue = 0;
        public int MuseWeaponDialogue = 0;
        public int KifrosseWeaponDialogue = 0;
        public int ArchitectWeaponDialogue = 0;
        public int ForceWeaponDialogue = 0;
        public int GenocideWeaponDialogue = 0;
        public int TakodachiWeaponDialogue = 0;
        public int TwinStarsWeaponDialogue = 0;
        public int SkyStrikerWeaponDialogue = 0;
        public int CosmicDestroyerWeaponDialogue = 0;
        public int NeedlepointWeaponDialogue = 0;
        public int MurasamaWeaponDialogue = 0;

        public int MercyWeaponDialogue = 0;
        public int SakuraWeaponDialogue = 0;
        public int EternalWeaponDialogue = 0;
        public int DaemonWeaponDialogue = 0;
        public int OzmaWeaponDialogue = 0;
        public int UrgotWeaponDialogue = 0;
        public int BloodWeaponDialogue = 0;
        public int MorningStarWeaponDialogue = 0;
        public int VirtueWeaponDialogue = 0;
        public int RedMageWeaponDialogue = 0;
        public int BlazeWeaponDialogue = 0;
        public int PickaxeWeaponDialogue = 0;
        public int HardwareWeaponDialogue = 0;
        public int CatalystWeaponDialogue = 0;
        public int SilenceWeaponDialogue = 0;
        public int SoulWeaponDialogue = 0;
        public int GoldWeaponDialogue = 0;
        public int FarewellWeaponDialogue = 0;

        //Subworld dialogues
        public int observatoryDialogue = 0;
        public int cosmicVoyageDialogue = 0;


        //Stellar Array Code

        public float stellarArrayVisibility = 0f;
        public float stellarArrayMoveIn = 0f;
        public bool stellarArray = false;

        public int stellarGauge = 0; //This is the Stellar Gauge's charge (filled with passives)
        public int stellarGaugeMax = 5;


        public int stellarGaugeUpgraded;//0 not 1 yes. Calamity only.

        //Stellar Array Passives (0 Not unlocked | 1 Inactive | 2 Active)
        //How to add more Stellar Array Passives: 

        //Add variable
        //Add it to the Stellar Array menu
        //Edit blessings to account for new passive
        //Add unlock condition
        //Add save/load

        //if ((abilityCost + stellarGauge) <= stellarGaugeMax)
        //Remember to push the abilityCost!

        //Tier 1
        public int starshower = 0; //
        public int ironskin = 0; //
        public int evasionmastery = 0;//
        public int aquaaffinity = 0;//
        public int inneralchemy = 0;
        public int hikari = 0;
        public int livingdead = 0;
        public int umbralentropy = 0;
        public int celestialevanesence = 0;
        public int butchersdozen = 0; //
        public int mysticforging = 0;
        public int flashfreeze = 0;
        //Tier 2
        public int bonus100hp = 0;//
        public int bloomingflames = 0;//
        public int astralmantle = 0;//
        public int avataroflight = 0;
        public int afterburner = 0;
        public int weaknessexploit = 0;
        public int artofwar = 0;
        public int aprismatism = 0;
        //Tier 3
        public int beyondinfinity = 0;//
        public int keyofchronology = 0;
        public int beyondtheboundary = 0;//
        public int unbridledradiance = 0;

        //Aspected Weapon Modification
        public int MeleeAspect;//Overwrites all Aspected Weapons' damage types. This is permanent until relogging.
        public int MagicAspect;
        public int RangedAspect;
        public int SummonAspect;

        public int AspectLocked; // 0 = Unlocked 1 = Melee 2 = Magic 3 = Ranged 4 = Summon

        //Calamity
        public int RogueAspect;

        public int unbridledRadianceStack = 0;//Will increase every 1000 kills.

        int aprismatismCooldown;
        int butchersDozenKills;
        int ammoRecycleCooldown;
        int flashFreezeCooldown;

        int timeAfterGettingHit;

        int umbralEntropyCooldown;

        public int dmgSave;

        public int betweenTheBoundaryTimer = 0;
        public bool betweenTheBoundaryEbb = false;

        public string description = "";//Displays when the Stellar Array is visible. Shows the description of the ability and its cost.
        public string passiveTitle = "";
        public bool textVisible = false;//Set to true when hovering over an item.

        public bool stellarSickness = false;//When set to True, inflicts the player with Stellar Sickness for 1 minute.



        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////// STELLAR NOVA CODE /////////////////////////////////////////////////////////////////////////
        /// </summary>
        //Stellar Nova Code

        public int chosenStellarNova = 0;//0: No Nova chosen. 1: Theofania Inanis 2: Ars Laevateinn

        public int novaGauge;
        public int novaGaugeMax = 100;//This is affected by the chosen Nova
        public int novaGaugeChargeTimer;
        public int novaGaugeLossTimer;


        public int baseNovaDamageAdd = 1;
        public int novaDamage = 300;
        public int novaCritDamage = 450;
        public int novaCritChance = 10;

        public double novaDamageMod;
        public int novaCritChanceMod;
        public double novaCritDamageMod;
        public int novaChargeMod;

        public int trueNovaGaugeMax;

        public bool NovaCutInStart;
        public int NovaCutInTimer;
        public float NovaCutInVelocity;
        public float NovaCutInX;
        public float NovaCutInOpacity;
        public int randomNovaDialogue;

        public float novaUIOpacity;
        public bool novaUIActive;
        public float descriptionOpacity;
        public int descriptionY;
        public int descriptionYVelocity;
        public string hoverText;
        public string prismDescription;

        public bool loadPrisms;

        public string abilityName;
        public string abilitySubName;
        public string abilityDescription;
        public string starfarerBonus;
        public string baseStats;



        public string affix1; //Name of the affix.
        public string affix2;
        public string affix3;

        public string affixDesc1; //Describes the affix.
        public string affixDesc2;
        public string affixDesc3;

        public string specialAffix;//Unused

        public Item affixItem1;
        public Item affixItem2;
        public Item affixItem3;

        public bool apocryphicPrism;

        public int theofania; //0 = LOCKED, 1 = UNLOCKED, 2 = SELECTED
        public int laevateinn; //0 = LOCKED, 1 = UNLOCKED, 2 = SELECTED
        public int kiwamiryuken; //0 = LOCKED, 1 = UNLOCKED, 2 = SELECTED 2 does not matter really
        public int gardenofavalon; //0 = LOCKED, 1 = UNLOCKED, 2 = SELECTED 2 does not matter really
        public int edingenesisquasar; //0 = LOCKED, 1 = UNLOCKED, 2 = SELECTED 2 does not matter really

        public int astarteDriverAttacks; //Amount of attacks left after casting Edin Genesis Quasar
        public int astarteDriverCooldown;

        public bool ruinedKingPrism;//Tier 3
        public bool cosmicPhoenixPrism;

        //Tier 2
        public bool lightswornPrism;
        public bool spatialPrism;
        public bool paintedPrism;
        public bool burnishedPrism;
        public bool voidsentPrism;

        public bool royalSlimePrism;
        public bool mechanicalPrism;
        public bool overgrownPrism;
        public bool lihzahrdPrism;
        public bool typhoonPrism;
        public bool empressPrism;
        public bool luminitePrism;



        public Vector2 playerMousePos;

        public int ryukenTimer;

        public bool novaGaugeUnlocked = false;


        public int inCombat = 0;//If greater than 0, player is inCombat. default is 15 seconds, or 1200 ticks. Additonally, the NovaGauge will decrease if out of combat, or increase if Asphodene.
        public static int inCombatMax = 900;//Default is 900, or 15 seconds in combat.

        /// <summary>
        /// These are the variables for the Starfarer Prompt system
        /// </summary>
        /// <returns></returns>

        public float promptVisibility = 0f;
        public float promptMoveIn = 0f;

        public string promptDialogue;
        public bool promptIsActive;
        public int starfarerPromptCooldown;
        public int starfarerPromptActiveTimer;
        public int promptExpression;

        public static int starfarerPromptCooldownMax;

        public static int starfarerPromptActiveTimerSetting = 100;

        //First time dialogues (these save and aren't repeated.) They may also expedite the cooldown on Prompts
        //Bosses
        public bool seenEyeOfCthulhu;
        public bool seenKingSlime;
        public bool seenEaterOfWorlds;
        public bool seenBrainOfCthulhu;
        public bool seenQueenBee;
        public bool seenSkeletron;
        public bool seenDeerclops;
        public bool seenWallOfFlesh;
        //Post hardmode
        public bool seenQueenSlime;
        public bool seenTwins;
        public bool seenDestroyer;
        public bool seenSkeletronPrime;
        public bool seenPlantera;
        public bool seenGolem;
        public bool seenDukeFishron;
        public bool seenCultist;
        public bool seenEmpress;
        public bool seenMoonLord;

        //Custom bosses
        public bool seenWarriorOfLight;
        public bool seenVagrant;
        public bool seenNalhaun;
        public bool seenPenth;
        public bool seenArbiter;

        //Biomes
        public bool seenDesertBiome;
        public bool seenSnowBiome;
        public bool seenCorruptionBiome;
        public bool seenCrimsonBiome;
        public bool seenJungleBiome;
        public bool seenDungeonBiome;
        public bool seenGlowingMushroomBiome;
        public bool seenBeachBiome;
        public bool seenMeteoriteBiome;
        public bool seenHallowBiome;
        public bool seenSpaceBiome;
        public bool seenUnderworldBiome;

        //Calamity Biomes
        public bool seenCragBiome;
        public bool seenAstralBiome;
        public bool seenSunkenSeaBiome;
        public bool seenSulphurSeaBiome;
        public bool seenAbyssBiome;

        //Thorium Biomes/Hooks
        public bool seenAquaticDepthsBiome;
        public bool seenGraniteBiome;
        public bool seenMarbleBiome;

        //Verdant
        public bool seenVerdantBiome;

        //Calamity Bosses
        //Pre Hardmode
        public bool seenDesertScourge;
        public bool seenCrabulon;
        public bool seenPerforators;
        public bool seenHiveMind;
        public bool seenSlimeGod;
        //Hardmode
        public bool seenCryogen; // 1
        public bool seenAquaticScourge; // 2
        public bool seenBrimstoneElemental; // 3
        public bool seenCalamitas; // 4
        public bool seenSiren; // 4.5
        public bool seenLeviathan; // 5
        public bool seenAnahita; //5.5
        public bool seenAstrumAureus; // 6
        public bool seenPlaguebringer; // 7
        public bool seenRavager; // 8
        public bool seenAstrumDeus; // 9
        //Post Hardmode
        public bool seenProfanedGuardian;
        public bool seenDragonfolly;
        public bool seenProvidence;
        public bool seenStormWeaver;
        public bool seenCeaselessVoid;
        public bool seenSignus;
        public bool seenPolterghast;
        public bool seenOldDuke;
        public bool seenDog;
        public bool seenYharon;
        public bool seenYharonDespawn;
        public bool seenSupremeCalamitas;

        public bool seenDraedon;
        public bool seenArtemis;
        public bool seenThanatos;
        public bool seenAres;

        public bool yharonPresent;

        //Thorium Bosses
        public bool seenGrandThunderBird;
        public bool seenQueenJellyfish;
        public bool seenViscount;
        public bool seenGraniteEnergyStorm;
        public bool seenBuriedChampion;
        public bool seenStarScouter;
        public bool seenBoreanStrider;
        public bool seenCoznix;
        public bool seenLich;
        public bool seenAbyssion;
        public bool seenPrimordials;

        //SOTS Bosses
        public bool seenPutridPinky;
        public bool seenPharaoh;
        public bool seenAdvisor;
        public bool seenPolaris;
        public bool seenLux;
        public bool seenSubspaceSerpent;

        //Fargos Souls Bosses
        public bool seenTrojanSquirrel;
        public bool seenDeviantt;
        public bool seenEridanus;
        public bool seenAbominationn;
        public bool seenMutant;

        public bool seenUnknownBoss;
        public int seenUnknownBossTimer;
        public List<int> seenBossesList = new List<int>();


        //Subworlds
        public bool seenObservatory;
        public bool seenCygnusAsteroids;
        public bool seenBleachedPlanet;
        public bool seenConfluence;
        public bool seenCity;


        //Weathers (This doesn't save and resets upon relogging.)
        public bool seenRain;
        public bool seenSnow;
        public bool seenSandstorm;

        public static bool disablePrompts;
        public static bool disablePromptsBuffs;
        public static bool disablePromptsCombat;

        public static bool noLockedCamera;

        public static bool voicesEnabled;

        public bool archivePopulated = false;
        //////////////
        /// <summary>
        /// 
        /// </summary>
        ////////////////////////////////////////////////////////////////////////////////// STARFARER MENU

        public bool starfarerMenuActive;

        public float starfarerMenuUIOpacity;
        public float costumeChangeOpacity;
        int starfarerOutfitSaved = 0;

        public bool archiveActive;

        //The Archive has 4 buttons, they correspond to Idle Dialogue, Boss Dialogue, Weapon Dialogue, and Starfarer Prompts.
        //Selecting a button changes the minimum/maximum values of the indicator. Pressing the left and right buttons cycle through the dialogues.
        //The dialogues start at 1, not 0.
        // Once a dialogue has been selected, the Starfarer Menu will close, and the dialogue will appear.
        //The Starfarer Menu will shift to the left to make room. Otherwise, it's centered.
        public int archiveChosenList = 0; //0 = Idle | 1 = Boss | 2 = Weapon | 3 = VN

        public int archiveListNumber = 1;//This is reset to 1 after you've chosen a different list.
        public int archiveListMax = 1;//This is the total amount of dialogues available.

        public string archiveListInfo = "";

        public bool canViewArchive = false; //Disable viewing the dialogue if it hasn't been unlocked.

        public List<IdleArchiveListing> IdleArchiveList = new List<IdleArchiveListing>();
        public List<BossArchiveListing> BossArchiveList = new List<BossArchiveListing>();
        public List<BossArchiveListingCalamity> BossArchiveListCalamity = new List<BossArchiveListingCalamity>();
        public List<WeaponArchiveListing> WeaponArchiveList = new List<WeaponArchiveListing>();
        public List<VNArchiveListing> VNArchiveList = new List<VNArchiveListing>();


        public int IdleArchiveListMax = 2;
        public int BossArchiveListMax = 2;
        public int BossArchiveListMaxCalamity = 2;
        public int WeaponArchiveListMax = 2;
        public int VNArchiveListMax = 2;

        //Lore list?
        //
        //Pets
        public bool Ghost;
        public bool PekoraPet;
        public bool KasumiPet;
        public bool WarriorPet;
        public bool EreshkigalPet;
        public bool MajimaPet;

        public bool VitchPet;
        //
        public bool EspeonPet;
        public bool UmbreonPet;
        //
        public bool BloopPet;
        public bool BubbaPet;
        //
        public bool DuckHuntBirdPet;
        public bool DuckHuntDogPet;

        public bool GuilmonPet;
        public bool BiyomonPet;

        public bool BladeWolfPet;

        public bool MantisCatPet;

        public bool FerryPet;

        public bool HuTaoPet;
        public bool OmoriPet;

        public bool PyraPet;
        public bool LukaPet;
        public bool CrimsonDragonetPet;
        public bool HanakoPet;
        public bool PhymPet;
        public bool NecoArcPet;
        public bool SuiseiPet;
        public bool SanaPet;
        public bool MumeiPet;
        public bool GrahaPet;

        public bool seenMusicWarning;

        //player.GetModPlayer<StarsAbovePlayer>().heroLives = 3;


        // The Sea of Stars code
        public bool SeaOfStars;//When the player is in the Sea of Stars, they are inflicted with Cosmic Voyager, preventing destroying or placing blocks in the subworlds.
        public float gravityMod;
        public bool Observatory;
        public bool BleachedWorld;
        public bool City;
        //public int cachedSpawnX = player.SpawnX;


        


        //player.GetModPlayer<StarsAbovePlayer>().VARIABLENAME = VALUE;
        public override void SaveData(TagCompound tag)
        {

            tag["chosenStarfarer"] = chosenStarfarer;

            tag["starfarerOutfit"] = starfarerOutfit;
            tag["starfarerOutfitVanity"] = starfarerOutfitVanity;

            tag["starshower"] = starshower;
            tag["ironskin"] = ironskin;
            tag["evasionmastery"] = evasionmastery;
            tag["aquaaffinity"] = aquaaffinity;
            tag["hikari"] = hikari;
            tag["livingdead"] = livingdead;
            tag["inneralchemy"] = inneralchemy;
            tag["umbralentropy"] = umbralentropy;
            tag["celestialevanesence"] = celestialevanesence;
            tag["butchersdozen"] = butchersdozen;
            tag["mysticforging"] = mysticforging;
            tag["flashfreeze"] = flashfreeze;

            tag["bonus100hp"] = bonus100hp;
            tag["bloomingflames"] = bloomingflames;
            tag["astralmantle"] = astralmantle;
            tag["avataroflight"] = avataroflight;
            tag["afterburner"] = afterburner;
            tag["weaknessexploit"] = weaknessexploit;
            tag["artofwar"] = artofwar;
            tag["aprismatism"] = aprismatism;

            tag["beyondinfinity"] = beyondinfinity;
            tag["keyofchronology"] = keyofchronology;
            tag["beyondtheboundary"] = beyondtheboundary;
            tag["unbridledradiance"] = unbridledradiance;

            tag["aspectlocked"] = AspectLocked;

            tag["stellarGauge"] = stellarGauge;
            tag["stellarGaugeMax"] = stellarGaugeMax;
            tag["stellarGaugeUpgraded"] = stellarGaugeUpgraded;


            tag["starfarerIntro"] = starfarerIntro;
            tag["slimeDialogue"] = slimeDialogue;
            tag["eyeDialogue"] = eyeDialogue;
            tag["corruptBossDialogue"] = corruptBossDialogue;
            tag["BeeBossDialogue"] = BeeBossDialogue;
            tag["SkeletonDialogue"] = SkeletonDialogue;
            tag["WallOfFleshDialogue"] = WallOfFleshDialogue;
            tag["DeerclopsDialogue"] = DeerclopsDialogue;
            tag["QueenSlimeDialogue"] = QueenSlimeDialogue;
            tag["TwinsDialogue"] = TwinsDialogue;
            tag["DestroyerDialogue"] = DestroyerDialogue;
            tag["SkeletronPrimeDialogue"] = SkeletronPrimeDialogue;
            tag["AllMechsDefeatedDialogue"] = AllMechsDefeatedDialogue;
            tag["PlanteraDialogue"] = PlanteraDialogue;
            tag["DukeFishronDialogue"] = DukeFishronDialogue;
            tag["GolemDialogue"] = GolemDialogue;
            tag["EmpressDialogue"] = EmpressDialogue;
            tag["CultistDialogue"] = CultistDialogue;
            tag["MoonLordDialogue"] = MoonLordDialogue;
            tag["WarriorOfLightDialogue"] = WarriorOfLightDialogue;
            tag["vagrantDialogue"] = vagrantDialogue;
            tag["nalhaunDialogue"] = nalhaunDialogue;
            tag["penthDialogue"] = penthDialogue;
            tag["arbiterDialogue"] = arbiterDialogue;
            tag["tsukiyomiDialogue"] = tsukiyomiDialogue;


            tag["nalhaunitem"] = nalhaunBossItemDialogue;
            tag["penthitem"] = penthBossItemDialogue;
            tag["arbiteritem"] = arbiterBossItemDialogue;
            tag["warrioritem"] = warriorBossItemDialogue;

            tag["allModdedBosses"] = allModdedBosses;
            tag["AllVanillaBossesDefeatedDialogue"] = AllVanillaBossesDefeatedDialogue;
            tag["EverythingDefeatedDialogue"] = EverythingDefeatedDialogue;

            tag["desertscourgeDialogue"] = desertscourgeDialogue;
            tag["crabulonDialogue"] = crabulonDialogue;
            tag["hivemindDialogue"] = hivemindDialogue;
            tag["perforatorDialogue"] = perforatorDialogue;
            tag["slimegodDialogue"] = slimegodDialogue;

            tag["cryogenDialogue"] = cryogenDialogue;
            tag["aquaticscourgeDialogue"] = aquaticscourgeDialogue;
            tag["brimstoneelementalDialogue"] = brimstoneelementalDialogue;
            tag["calamitasDialogue"] = calamitasDialogue;
            tag["leviathanDialogue"] = leviathanDialogue;
            tag["astrumaureusDialogue"] = astrumaureusDialogue;
            tag["plaguebringerDialogue"] = plaguebringerDialogue;
            tag["ravagerDialogue"] = ravagerDialogue;
            tag["astrumdeusDialogue"] = astrumdeusDialogue;

            tag["EyeBossWeaponDialogue"] = EyeBossWeaponDialogue;
            tag["CorruptBossWeaponDialogue"] = CorruptBossWeaponDialogue;
            tag["SkeletonWeaponDialogue"] = SkeletonWeaponDialogue;
            tag["HellWeaponDialogue"] = HellWeaponDialogue;
            tag["QueenBeeWeaponDialogue"] = QueenBeeWeaponDialogue;
            tag["KingSlimeWeaponDialogue"] = KingSlimeWeaponDialogue;
            tag["WallOfFleshWeaponDialogue"] = WallOfFleshWeaponDialogue;
            tag["MechBossWeaponDialogue"] = MechBossWeaponDialogue;
            tag["AllMechBossWeaponDialogue"] = AllMechBossWeaponDialogue;
            tag["PlanteraWeaponDialogue"] = PlanteraWeaponDialogue;
            tag["GolemWeaponDialogue"] = GolemWeaponDialogue;
            tag["DukeFishronWeaponDialogue"] = DukeFishronWeaponDialogue;
            tag["LunaticCultistWeaponDialogue"] = LunaticCultistWeaponDialogue;
            tag["MoonLordWeaponDialogue"] = MoonLordWeaponDialogue;
            tag["WarriorWeaponDialogue"] = WarriorWeaponDialogue;
            tag["VagrantWeaponDialogue"] = VagrantWeaponDialogue;
            tag["NalhaunWeaponDialogue"] = NalhaunWeaponDialogue;
            tag["PenthWeaponDialogue"] = PenthesileaWeaponDialogue;
            tag["ArbiterWeaponDialogue"] = ArbitrationWeaponDialogue;
            tag["QueenSlimeWeaponDialogue"] = QueenSlimeWeaponDialogue;


            tag["MiseryWeaponDialogue"] = MiseryWeaponDialogue;
            tag["HullwroughtWeaponDialogue"] = HullwroughtWeaponDialogue;
            tag["ShadowlessWeaponDialogue"] = ShadowlessWeaponDialogue;
            tag["OceanWeaponDialogue"] = OceanWeaponDialogue;
            tag["LumaWeaponDialogue"] = LumaWeaponDialogue;
            tag["MonadoWeaponDialogue"] = MonadoWeaponDialogue;
            tag["FrostMoonWeaponDialogue"] = FrostMoonWeaponDialogue;
            tag["ClaimhWeaponDialogue"] = ClaimhWeaponDialogue;
            tag["MuseWeaponDialogue"] = MuseWeaponDialogue;
            tag["KifrosseWeaponDialogue"] = KifrosseWeaponDialogue;
            tag["ArchitectWeaponDialogue"] = ArchitectWeaponDialogue;
            tag["ForceWeaponDialogue"] = ForceWeaponDialogue;
            tag["GenocideWeaponDialogue"] = GenocideWeaponDialogue;
            tag["TakodachiWeaponDialogue"] = TakodachiWeaponDialogue;
            tag["TwinStarsWeaponDialogue"] = TwinStarsWeaponDialogue;
            tag["SkyStrikerWeaponDialogue"] = SkyStrikerWeaponDialogue;
            tag["CosmicDestroyerWeaponDialogue"] = CosmicDestroyerWeaponDialogue;
            tag["NeedlepointWeaponDialogue"] = NeedlepointWeaponDialogue;
            tag["MurasamaWeaponDialogue"] = MurasamaWeaponDialogue;
            tag["MercyWeaponDialogue"] = MercyWeaponDialogue;
            tag["SakuraWeaponDialogue"] = SakuraWeaponDialogue;
            tag["EternalWeaponDialogue"] = EternalWeaponDialogue;
            tag["DaemonWeaponDialogue"] = DaemonWeaponDialogue;
            tag["OzmaWeaponDialogue"] = OzmaWeaponDialogue;
            tag["UrgotWeaponDialogue"] = UrgotWeaponDialogue;
            tag["BloodWeaponDialogue"] = BloodWeaponDialogue;
            tag["MorningStarWeaponDialogue"] = MorningStarWeaponDialogue;
            tag["VirtueWeaponDialogue"] = VirtueWeaponDialogue;
            tag["RedMageWeaponDialogue"] = RedMageWeaponDialogue;
            tag["BlazeWeaponDialogue"] = BlazeWeaponDialogue;
            tag["PickaxeWeaponDialogue"] = PickaxeWeaponDialogue;
            tag["HardwareWeaponDialogue"] = HardwareWeaponDialogue;
            tag["CatalystWeaponDialogue"] = CatalystWeaponDialogue;
            tag["SilenceWeaponDialogue"] = SilenceWeaponDialogue;
            tag["SoulWeaponDialogue"] = SoulWeaponDialogue;
            tag["GoldWeaponDialogue"] = GoldWeaponDialogue;
            tag["FarewellWeaponDialogue"] = FarewellWeaponDialogue;


            tag["observatoryDialogue"] = observatoryDialogue;
            tag["cosmicVoyageDialogue"] = cosmicVoyageDialogue;

            tag["unbridledRadianceStack"] = unbridledRadianceStack;

            tag["novaGaugeUnlocked"] = novaGaugeUnlocked;
            tag["theofania"] = theofania;
            tag["laevateinn"] = laevateinn;
            tag["kiwamiryuken"] = kiwamiryuken;
            tag["gardenofavalon"] = gardenofavalon;
            tag["edingenesisquasar"] = edingenesisquasar;
            tag["chosenStellarNova"] = chosenStellarNova;

            tag["seenEyeOfCthulhu"] = seenEyeOfCthulhu;
            tag["seenKingSlime"] = seenKingSlime;
            tag["seenEaterOfWorlds"] = seenEaterOfWorlds;
            tag["seenBrainOfCthulhu"] = seenBrainOfCthulhu;
            tag["seenQueenBee"] = seenQueenBee;
            tag["seenSkeletron"] = seenSkeletron;
            tag["seenWallOfFlesh"] = seenWallOfFlesh;
            tag["seenTwins"] = seenTwins;

            tag["seenDestroyer"] = seenDestroyer;
            tag["seenSkeletronPrime"] = seenSkeletronPrime;
            tag["seenPlantera"] = seenPlantera;
            tag["seenGolem"] = seenGolem;
            tag["seenDukeFishron"] = seenDukeFishron;
            tag["seenCultist"] = seenCultist;
            tag["seenMoonLord"] = seenMoonLord;
            tag["seenWarriorOfLight"] = seenWarriorOfLight;
            tag["seenVagrant"] = seenVagrant;
            tag["seenNalhaun"] = seenNalhaun;
            tag["seenPenth"] = seenPenth;
            tag["seenArbiter"] = seenArbiter;

            tag["seenDeerclops"] = seenDeerclops;
            tag["seenQueenSlime"] = seenQueenSlime;
            tag["seenEmpress"] = seenEmpress;

            tag["seenDesertScourge"] = seenDesertScourge;
            tag["seenCrabulon"] = seenCrabulon;
            tag["seenHiveMind"] = seenHiveMind;
            tag["seenPerforators"] = seenPerforators;
            tag["seenSlimeGod"] = seenSlimeGod;

            tag["seenCryogen"] = seenCryogen;
            tag["seenAquaticScourge"] = seenAquaticScourge;
            tag["seenBrimstoneElemental"] = seenBrimstoneElemental;
            tag["seenCalamitas"] = seenCalamitas;
            tag["seenLeviathan"] = seenLeviathan;
            tag["seenAnahita"] = seenAnahita;
            tag["seenAstrumAureus"] = seenAstrumAureus;
            tag["seenPlaguebringer"] = seenPlaguebringer;
            tag["seenRavager"] = seenRavager;
            tag["seenAstrumDeus"] = seenAstrumDeus;

            tag["seenProfanedGuardian"] = seenProfanedGuardian;
            tag["seenDragonfolly"] = seenDragonfolly;
            tag["seenProvidence"] = seenProvidence;
            tag["seenStormWeaver"] = seenStormWeaver;
            tag["seenCeaselessVoid"] = seenCeaselessVoid;
            tag["seenSignus"] = seenSignus;
            tag["seenPolterghast"] = seenPolterghast;
            tag["seenOldDuke"] = seenOldDuke;
            tag["seenDog"] = seenDog;
            tag["seenYharon"] = seenYharon;
            tag["seenYharonDespawn"] = seenYharonDespawn;
            tag["seenSupremeCalamitas"] = seenSupremeCalamitas;

            tag["seenDraedon"] = seenDraedon;
            tag["seenArtemis"] = seenArtemis;
            tag["seenThanatos"] = seenThanatos;
            tag["seenAres"] = seenAres;

            tag["seenGrandThunderBird"] = seenGrandThunderBird;
            tag["seenQueenJellyfish"] = seenQueenJellyfish;
            tag["seenViscount"] = seenViscount;
            tag["seenGraniteEnergyStorm"] = seenGraniteEnergyStorm;
            tag["seenBuriedChampion"] = seenBuriedChampion;
            tag["seenStarScouter"] = seenStarScouter;
            tag["seenBoreanStrider"] = seenBoreanStrider;
            tag["seenCoznix"] = seenCoznix;
            tag["seenLich"] = seenLich;
            tag["seenAbyssion"] = seenAbyssion;
            tag["seenPrimordials"] = seenPrimordials;

            tag["seenPutridPinky"] = seenPutridPinky;
            tag["seenPharaoh"] = seenPharaoh;
            tag["seenAdvisor"] = seenAdvisor;
            tag["seenPolaris"] = seenPolaris;
            tag["seenLux"] = seenLux;
            tag["seenSubspaceSerpent"] = seenSubspaceSerpent;

            tag["seenTrojanSquirrel"] = seenTrojanSquirrel;
            tag["seenDeviantt"] = seenDeviantt;
            tag["seenEridanus"] = seenEridanus;
            tag["seenAbominationn"] = seenAbominationn;
            tag["seenMutant"] = seenMutant;

            tag["seenObservatory"] = seenObservatory;
            tag["seenCygnusAsteroids"] = seenCygnusAsteroids;
            tag["seenBleachedPlanet"] = seenBleachedPlanet;

            tag["seenDesert"] = seenDesertBiome;
            tag["seenHallow"] = seenHallowBiome;
            tag["seenCorruption"] = seenCorruptionBiome;
            tag["seenCrimson"] = seenCrimsonBiome;
            tag["seenSnow"] = seenSnowBiome;
            tag["seenJungle"] = seenJungleBiome;
            tag["seenMushroom"] = seenGlowingMushroomBiome;
            tag["seenBeach"] = seenBeachBiome;
            tag["seenMeteor"] = seenMeteoriteBiome;
            tag["seenDungeon"] = seenDungeonBiome;
            tag["seenSpace"] = seenSpaceBiome;
            tag["seenUnderworld"] = seenUnderworldBiome;

            tag["seenCrag"] = seenCragBiome;
            tag["seenAstral"] = seenAstralBiome;
            tag["seenSunkenSea"] = seenSunkenSeaBiome;
            tag["seenSulphurSea"] = seenSulphurSeaBiome;
            tag["seenAbyss"] = seenAbyssBiome;

            tag["seenAquaticDepths"] = seenAquaticDepthsBiome;
            tag["seenGranite"] = seenGraniteBiome;
            tag["seenMarble"] = seenMarbleBiome;

            tag["seenVerdant"] = seenVerdantBiome;


            tag["affixSlot1"] = StellarNovaUI._affixSlot1.Item;
            tag["affixSlot2"] = StellarNovaUI._affixSlot2.Item;
            tag["affixSlot3"] = StellarNovaUI._affixSlot3.Item;

            tag["starfarerOutfitItem"] = StarfarerMenu._starfarerArmorSlot.Item;
            tag["starfarerVanityItem"] = StarfarerMenu._starfarerVanitySlot.Item;

            tag["starfarerVisibleOutfit"] = starfarerOutfitVisible;
            tag["starfarerSavedOutfit"] = starfarerOutfitSaved;
            tag["seenBossesList"] = seenBossesList;

            tag["firstJoinedWorld"] = firstJoinedWorld;
            tag["firstJoinedWorldName"] = firstJoinedWorldName;


            base.SaveData(tag);
        }

        public override void LoadData(TagCompound tag)
        {
            firstJoinedWorld = tag.GetInt("firstJoinedWorld");
            firstJoinedWorldName = tag.GetString("firstJoinedWorldName");


            chosenStarfarer = tag.GetInt("chosenStarfarer");

            starfarerOutfit = tag.GetInt("starfarerOutfit");
            starfarerOutfitVanity = tag.GetInt("starfarerOutfitVanity");

            starfarerIntro = tag.GetBool("starfarerIntro");
            slimeDialogue = tag.GetInt("slimeDialogue");
            eyeDialogue = tag.GetInt("eyeDialogue");
            corruptBossDialogue = tag.GetInt("corruptBossDialogue");
            BeeBossDialogue = tag.GetInt("BeeBossDialogue");
            SkeletonDialogue = tag.GetInt("SkeletonDialogue");
            DeerclopsDialogue = tag.GetInt("DeerclopsDialogue");
            WallOfFleshDialogue = tag.GetInt("WallOfFleshDialogue");
            QueenSlimeDialogue = tag.GetInt("QueenSlimeDialogue");
            TwinsDialogue = tag.GetInt("TwinsDialogue");
            DestroyerDialogue = tag.GetInt("DestroyerDialogue");
            SkeletronPrimeDialogue = tag.GetInt("SkeletronPrimeDialogue");
            AllMechsDefeatedDialogue = tag.GetInt("AllMechsDefeatedDialogue");
            PlanteraDialogue = tag.GetInt("PlanteraDialogue");
            DukeFishronDialogue = tag.GetInt("DukeFishronDialogue");
            GolemDialogue = tag.GetInt("GolemDialogue");
            EmpressDialogue = tag.GetInt("EmpressDialogue");
            CultistDialogue = tag.GetInt("CultistDialogue");
            MoonLordDialogue = tag.GetInt("MoonLordDialogue");
            WarriorOfLightDialogue = tag.GetInt("WarriorOfLightDialogue");
            vagrantDialogue = tag.GetInt("vagrantDialogue");
            nalhaunDialogue = tag.GetInt("nalhaunDialogue");
            penthDialogue = tag.GetInt("penthDialogue");
            arbiterDialogue = tag.GetInt("arbiterDialogue");
            tsukiyomiDialogue = tag.GetInt("tsukiyomiDialogue");


            nalhaunBossItemDialogue = tag.GetInt("nalhaunitem");
            penthBossItemDialogue = tag.GetInt("penthitem");
            arbiterBossItemDialogue = tag.GetInt("arbiteritem");
            warriorBossItemDialogue = tag.GetInt("warrioritem");


            allModdedBosses = tag.GetInt("allModdedBosses");
            AllVanillaBossesDefeatedDialogue = tag.GetInt("AllVanillaBossesDefeatedDialogue");
            EverythingDefeatedDialogue = tag.GetInt("EverythingDefeatedDialogue");

            desertscourgeDialogue = tag.GetInt("desertscourgeDialogue");
            crabulonDialogue = tag.GetInt("crabulonDialogue");
            hivemindDialogue = tag.GetInt("hivemindDialogue");
            perforatorDialogue = tag.GetInt("perforatorDialogue");
            slimegodDialogue = tag.GetInt("slimegodDialogue");

            cryogenDialogue = tag.GetInt("cryogenDialogue");
            aquaticscourgeDialogue = tag.GetInt("aquaticscourgeDialogue");
            brimstoneelementalDialogue = tag.GetInt("brimstoneelementalDialogue");
            calamitasDialogue = tag.GetInt("calamitasDialogue");
            leviathanDialogue = tag.GetInt("leviathanDialogue");
            astrumaureusDialogue = tag.GetInt("astrumaureusDialogue");
            plaguebringerDialogue = tag.GetInt("plaguebringerDialogue");
            ravagerDialogue = tag.GetInt("ravagerDialogue");
            astrumdeusDialogue = tag.GetInt("astrumdeusDialogue");

            EyeBossWeaponDialogue = tag.GetInt("EyeBossWeaponDialogue");
            CorruptBossWeaponDialogue = tag.GetInt("CorruptBossWeaponDialogue");
            SkeletonWeaponDialogue = tag.GetInt("SkeletonWeaponDialogue");
            HellWeaponDialogue = tag.GetInt("HellWeaponDialogue");
            QueenBeeWeaponDialogue = tag.GetInt("QueenBeeWeaponDialogue");
            KingSlimeWeaponDialogue = tag.GetInt("KingSlimeWeaponDialogue");
            WallOfFleshWeaponDialogue = tag.GetInt("WallOfFleshWeaponDialogue");
            MechBossWeaponDialogue = tag.GetInt("MechBossWeaponDialogue");
            AllMechBossWeaponDialogue = tag.GetInt("AllMechBossWeaponDialogue");
            PlanteraWeaponDialogue = tag.GetInt("PlanteraWeaponDialogue");
            GolemWeaponDialogue = tag.GetInt("GolemWeaponDialogue");
            DukeFishronWeaponDialogue = tag.GetInt("DukeFishronWeaponDialogue");
            LunaticCultistWeaponDialogue = tag.GetInt("LunaticCultistWeaponDialogue");
            MoonLordWeaponDialogue = tag.GetInt("MoonLordWeaponDialogue");
            WarriorWeaponDialogue = tag.GetInt("WarriorWeaponDialogue");
            VagrantWeaponDialogue = tag.GetInt("VagrantWeaponDialogue");
            NalhaunWeaponDialogue = tag.GetInt("NalhaunWeaponDialogue");
            PenthesileaWeaponDialogue = tag.GetInt("PenthWeaponDialogue");
            ArbitrationWeaponDialogue = tag.GetInt("ArbiterWeaponDialogue");
            QueenSlimeWeaponDialogue = tag.GetInt("QueenSlimeWeaponDialogue");

            MiseryWeaponDialogue = tag.GetInt("MiseryWeaponDialogue");
            HullwroughtWeaponDialogue = tag.GetInt("HullwroughtWeaponDialogue");
            ShadowlessWeaponDialogue = tag.GetInt("ShadowlessWeaponDialogue");
            OceanWeaponDialogue = tag.GetInt("OceanWeaponDialogue");
            LumaWeaponDialogue = tag.GetInt("LumaWeaponDialogue");
            MonadoWeaponDialogue = tag.GetInt("MonadoWeaponDialogue");
            FrostMoonWeaponDialogue = tag.GetInt("FrostMoonWeaponDialogue");
            ClaimhWeaponDialogue = tag.GetInt("ClaimhWeaponDialogue");
            MuseWeaponDialogue = tag.GetInt("MuseWeaponDialogue");
            KifrosseWeaponDialogue = tag.GetInt("KifrosseWeaponDialogue");
            ArchitectWeaponDialogue = tag.GetInt("ArchitectWeaponDialogue");
            ForceWeaponDialogue = tag.GetInt("ForceWeaponDialogue");
            GenocideWeaponDialogue = tag.GetInt("GenocideWeaponDialogue");
            TakodachiWeaponDialogue = tag.GetInt("TakodachiWeaponDialogue");
            TwinStarsWeaponDialogue = tag.GetInt("TwinStarsWeaponDialogue");
            SkyStrikerWeaponDialogue = tag.GetInt("SkyStrikerWeaponDialogue");
            CosmicDestroyerWeaponDialogue = tag.GetInt("CosmicDestroyerWeaponDialogue");
            NeedlepointWeaponDialogue = tag.GetInt("NeedlepointWeaponDialogue");
            MurasamaWeaponDialogue = tag.GetInt("MurasamaWeaponDialogue");
            MercyWeaponDialogue = tag.GetInt("MercyWeaponDialogue");
            SakuraWeaponDialogue = tag.GetInt("SakuraWeaponDialogue");
            EternalWeaponDialogue = tag.GetInt("EternalWeaponDialogue");
            DaemonWeaponDialogue = tag.GetInt("DaemonWeaponDialogue");
            OzmaWeaponDialogue = tag.GetInt("OzmaWeaponDialogue");
            UrgotWeaponDialogue = tag.GetInt("UrgotWeaponDialogue");
            BloodWeaponDialogue = tag.GetInt("BloodWeaponDialogue");
            MorningStarWeaponDialogue = tag.GetInt("MorningStarWeaponDialogue");
            VirtueWeaponDialogue = tag.GetInt("VirtueWeaponDialogue");
            RedMageWeaponDialogue = tag.GetInt("RedMageWeaponDialogue");
            BlazeWeaponDialogue = tag.GetInt("BlazeWeaponDialogue");
            PickaxeWeaponDialogue = tag.GetInt("PickaxeWeaponDialogue");
            HardwareWeaponDialogue = tag.GetInt("HardwareWeaponDialogue");
            CatalystWeaponDialogue = tag.GetInt("CatalystWeaponDialogue");
            SilenceWeaponDialogue = tag.GetInt("SilenceWeaponDialogue");
            SoulWeaponDialogue = tag.GetInt("SoulWeaponDialogue");
            GoldWeaponDialogue = tag.GetInt("GoldWeaponDialogue");
            FarewellWeaponDialogue = tag.GetInt("FarewellWeaponDialogue");


            observatoryDialogue = tag.GetInt("observatoryDialogue");
            cosmicVoyageDialogue = tag.GetInt("cosmicVoyageDialogue");



            starshower = tag.GetInt("starshower");
            ironskin = tag.GetInt("ironskin");
            evasionmastery = tag.GetInt("evasionmastery");
            aquaaffinity = tag.GetInt("aquaaffinity");
            hikari = tag.GetInt("hikari");
            inneralchemy = tag.GetInt("inneralchemy");
            livingdead = tag.GetInt("livingdead");
            umbralentropy = tag.GetInt("umbralentropy");
            celestialevanesence = tag.GetInt("celestialevanesence");
            butchersdozen = tag.GetInt("butchersdozen");
            mysticforging = tag.GetInt("mysticforging");
            flashfreeze = tag.GetInt("flashfreeze");

            bonus100hp = tag.GetInt("bonus100hp");
            bloomingflames = tag.GetInt("bloomingflames");
            astralmantle = tag.GetInt("astralmantle");
            avataroflight = tag.GetInt("avataroflight");
            afterburner = tag.GetInt("afterburner");
            weaknessexploit = tag.GetInt("weaknessexploit");
            artofwar = tag.GetInt("artofwar");
            aprismatism = tag.GetInt("aprismatism");

            beyondinfinity = tag.GetInt("beyondinfinity");
            keyofchronology = tag.GetInt("keyofchronology");
            beyondtheboundary = tag.GetInt("beyondtheboundary");
            unbridledradiance = tag.GetInt("unbridledradiance");

            AspectLocked = tag.GetInt("aspectlocked");

            unbridledRadianceStack = tag.GetInt("unbridledRadianceStack");

            stellarGauge = tag.GetInt("stellarGauge");
            stellarGaugeMax = tag.GetInt("stellarGaugeMax");
            stellarGaugeUpgraded = tag.GetInt("stellarGaugeUpgraded");


            novaGaugeUnlocked = tag.GetBool("novaGaugeUnlocked");
            chosenStellarNova = tag.GetInt("chosenStellarNova");
            theofania = tag.GetInt("theofania");
            laevateinn = tag.GetInt("laevateinn");
            kiwamiryuken = tag.GetInt("kiwamiryuken");
            gardenofavalon = tag.GetInt("gardenofavalon");
            edingenesisquasar = tag.GetInt("edingenesisquasar");

            seenEyeOfCthulhu = tag.GetBool("seenEyeOfCthulhu");
            seenKingSlime = tag.GetBool("seenKingSlime");
            seenEaterOfWorlds = tag.GetBool("seenEaterOfWorlds");
            seenBrainOfCthulhu = tag.GetBool("seenBrainOfCthulhu");
            seenQueenBee = tag.GetBool("seenQueenBee");
            seenSkeletron = tag.GetBool("seenSkeletron");
            seenWallOfFlesh = tag.GetBool("seenWallOfFlesh");
            seenTwins = tag.GetBool("seenTwins");
            seenDestroyer = tag.GetBool("seenDestroyer");
            seenSkeletronPrime = tag.GetBool("seenSkeletronPrime");
            seenPlantera = tag.GetBool("seenPlantera");
            seenGolem = tag.GetBool("seenGolem");
            seenDukeFishron = tag.GetBool("seenDukeFishron");
            seenCultist = tag.GetBool("seenCultist");
            seenMoonLord = tag.GetBool("seenMoonLord");
            seenWarriorOfLight = tag.GetBool("seenWarriorOfLight");
            seenVagrant = tag.GetBool("seenVagrant");
            seenNalhaun = tag.GetBool("seenNalhaun");
            seenPenth = tag.GetBool("seenPenth");
            seenArbiter = tag.GetBool("seenArbiter");

            seenDeerclops = tag.GetBool("seenDeerclops");
            seenQueenSlime = tag.GetBool("seenQueenSlime");
            seenEmpress = tag.GetBool("seenEmpress");

            seenDesertScourge = tag.GetBool("seenDesertScourge");
            seenCrabulon = tag.GetBool("seenCrabulon");
            seenPerforators = tag.GetBool("seenPerforators");
            seenHiveMind = tag.GetBool("seeHiveMind");
            seenSlimeGod = tag.GetBool("seenSlimeGod");

            seenCryogen = tag.GetBool("seenCryogen");
            seenAquaticScourge = tag.GetBool("seenAquaticScourge");
            seenBrimstoneElemental = tag.GetBool("seenBrimstoneElemental");
            seenCalamitas = tag.GetBool("seenCalamitas");
            seenLeviathan = tag.GetBool("seenLeviathan");
            seenAnahita = tag.GetBool("seenAnahita");
            seenAstrumAureus = tag.GetBool("seenAstrumAureus");
            seenPlaguebringer = tag.GetBool("seenPlaguebringer");
            seenRavager = tag.GetBool("seenRavager");
            seenAstrumDeus = tag.GetBool("seenAstrumDeus");

            seenProfanedGuardian = tag.GetBool("seenProfanedGuardian");
            seenDragonfolly = tag.GetBool("seenDragonfolly");
            seenProvidence = tag.GetBool("seenProvidence");
            seenStormWeaver = tag.GetBool("seenStormWeaver");
            seenCeaselessVoid = tag.GetBool("seenCeaselessVoid");
            seenSignus = tag.GetBool("seenSignus");
            seenPolterghast = tag.GetBool("seenPolterghast");
            seenOldDuke = tag.GetBool("seenOldDuke");
            seenDog = tag.GetBool("seenDog");
            seenYharon = tag.GetBool("seenYharon");
            seenYharonDespawn = tag.GetBool("seenYharonDespawn");
            seenSupremeCalamitas = tag.GetBool("seenSupremeCalamitas");

            seenDraedon = tag.GetBool("seenDraedon");
            seenArtemis = tag.GetBool("seenArtemis");
            seenThanatos = tag.GetBool("seenThanatos");
            seenAres = tag.GetBool("seenAres");

            seenGrandThunderBird = tag.GetBool("seenGrandThunderBird");
            seenQueenJellyfish = tag.GetBool("seenQueenJellyfish");
            seenViscount = tag.GetBool("seenViscount");
            seenGraniteEnergyStorm = tag.GetBool("seenGraniteEnergyStorm");
            seenBuriedChampion = tag.GetBool("seenBuriedChampion");
            seenStarScouter = tag.GetBool("seenStarScouter");
            seenBoreanStrider = tag.GetBool("seenBoreanStrider");
            seenCoznix = tag.GetBool("seenCoznix");
            seenLich = tag.GetBool("seenLich");
            seenAbyssion = tag.GetBool("seenAbyssion");
            seenPrimordials = tag.GetBool("seenPrimordials");

            seenPutridPinky = tag.GetBool("seenPutridPinky");
            seenPharaoh = tag.GetBool("seenPharaoh");
            seenAdvisor = tag.GetBool("seenAdvisor");
            seenPolaris = tag.GetBool("seenPolaris");
            seenLux = tag.GetBool("seenLux");
            seenSubspaceSerpent = tag.GetBool("seenSubspaceSerpent");

            seenTrojanSquirrel = tag.GetBool("seenTrojanSquirrel");
            seenDeviantt = tag.GetBool("seenDeviantt");
            seenEridanus = tag.GetBool("seenEridanus");
            seenAbominationn = tag.GetBool("seenAbomination");
            seenMutant = tag.GetBool("seenMutant");

            seenObservatory = tag.GetBool("seenObservatory");
            seenCygnusAsteroids = tag.GetBool("seenCygnusAsteroids");
            seenBleachedPlanet = tag.GetBool("seenBleachedPlanet");



            seenDesertBiome = tag.GetBool("seenDesert");
            seenHallowBiome = tag.GetBool("seenHallow");
            seenCorruptionBiome = tag.GetBool("seenCorruption");
            seenCrimsonBiome = tag.GetBool("seenCrimson");
            seenSnowBiome = tag.GetBool("seenSnow");
            seenJungleBiome = tag.GetBool("seenJungle");
            seenGlowingMushroomBiome = tag.GetBool("seenMushroom");
            seenMeteoriteBiome = tag.GetBool("seenMeteor");
            seenBeachBiome = tag.GetBool("seenBeach");
            seenDungeonBiome = tag.GetBool("seenDungeon");
            seenSpaceBiome = tag.GetBool("seenSpace");
            seenUnderworldBiome = tag.GetBool("seenUnderworld");

            seenCragBiome = tag.GetBool("seenCrag");
            seenAstralBiome = tag.GetBool("seenAstral");
            seenSunkenSeaBiome = tag.GetBool("seenSunkenSea");
            seenSulphurSeaBiome = tag.GetBool("seenSulphurSea");
            seenAbyssBiome = tag.GetBool("seenAbyss");

            seenAquaticDepthsBiome = tag.GetBool("seenAquaticDepths");
            seenGraniteBiome = tag.GetBool("seenGranite");
            seenMarbleBiome = tag.GetBool("seenMarble");

            seenVerdantBiome = tag.GetBool("seenVerdant");


            affixItem1 = tag.Get<Item>("affixSlot1");
            affixItem2 = tag.Get<Item>("affixSlot2");
            affixItem3 = tag.Get<Item>("affixSlot3");

            //tag["starfarerVisibleOutfit"] = starfarerOutfitVisible;
            starfarerArmorEquipped = tag.Get<Item>("starfarerOutfitItem");
            starfarerVanityEquipped = tag.Get<Item>("starfarerVanityItem");

            starfarerOutfitVisible = tag.GetInt("starfarerVisibleOutfit");
            starfarerOutfitSaved = tag.GetInt("starfarerSavedOutfit");

            seenBossesList = tag.Get<List<int>>("seenBossesList");

            seenMusicWarning = tag.GetBool("seenMusicWarning");

        }

        public override void OnEnterWorld(Player player)
        {
            SubworldSystem.noReturn = false; //Fix missing save and quit bug?

            if (player.whoAmI == Main.myPlayer && enableWorldLock)
            {
                if (firstJoinedWorld == 0)
                {
                    firstJoinedWorld = Main.worldID;
                    firstJoinedWorldName = Main.worldName;
                    if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(Language.GetTextValue($"{player.name} has been binded to {Main.worldName}."), 220, 100, 247); }
                    if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(Language.GetTextValue($"The Stars Above progression will only occur on this world. (Check Mod Settings if necessary)"), 255, 126, 114); }

                }
                if (Main.worldID != firstJoinedWorld)
                {
                    if (firstJoinedWorldName != null)
                    {
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(Language.GetTextValue($"{player.name} has already been binded to {firstJoinedWorldName}. (World ID {firstJoinedWorld})"), 220, 100, 247); }

                    }
                    else
                    {
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(Language.GetTextValue($"{player.name} has already been binded to World ID {firstJoinedWorld}."), 220, 100, 247); }

                    }
                    if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(Language.GetTextValue($"Disable the client-side configuration option 'Enable Player Progress World Lock' to enable The Stars Above progression on this world."), 255, 126, 114); }

                }



            }
            if (NPC.downedMoonlord && !DownedBossSystem.downedWarrior && SubworldSystem.Current == null)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(Language.GetTextValue("The world is shrouded with Light!"), 190, 100, 247); }


                }

            }
            if (novaGaugeUnlocked)
            {
                StellarNovaUI._affixSlot1.Item = affixItem1;
                StellarNovaUI._affixSlot2.Item = affixItem2;
                StellarNovaUI._affixSlot3.Item = affixItem3;
                //StellarNovaUI.loadPrisms = true;
                affix1 = affixItem1.Name;
                affix2 = affixItem2.Name;
                affix3 = affixItem3.Name;
            }
            StarfarerMenu._starfarerArmorSlot.Item = starfarerArmorEquipped;
            StarfarerMenu._starfarerVanitySlot.Item = starfarerVanityEquipped;


            base.OnEnterWorld(player);

        }
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {


            return new[] {
                new Item(ModContent.ItemType<SpatialDisk>()),
                //new Item(ModContent.ItemType<Bifrost>()),
            };
        }


        public override void OnHitAnything(float x, float y, Entity victim)
        {





        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (item.ModItem is LiberationBlazing)
            {
                if (Player.HasBuff(BuffType<Buffs.CoreOfFlames>()))
                {
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_electroSmack, Player.Center);


                    SoundEngine.PlaySound(StarsAboveAudio.SFX_electroSmack, Player.Center);

                    Projectile.NewProjectile(Player.GetSource_ItemUse(item), target.Center.X, target.Center.Y, 0, 0, Mod.Find<ModProjectile>("ScarletOutburst").Type, damage, 0, Player.whoAmI, 0f);
                }
            }
            if (item.ModItem is ClaimhSolais)
            {
                if (crit)
                {
                    if (radiance >= 10)
                    {
                        Player.AddBuff(BuffID.Ironskin, 240);
                    }
                    else
                    {
                        radiance++;
                    }

                }
            }
            if (Player.GetModPlayer<StarsAbovePlayer>().sakuraHeld)
            {
                if (!Player.GetModPlayer<StarsAbovePlayer>().bladeWill)
                {
                    if (Main.rand.Next(15) == 1)
                    {
                        //player.QuickSpawnItem(null,mod.ItemType("RedOrb"));
                        int k = Item.NewItem(Player.GetSource_ItemUse(item), (int)target.position.X + Main.rand.Next(-20, 20), (int)target.position.Y + Main.rand.Next(-20, 20), target.width, target.height, Mod.Find<ModItem>("RedOrb").Type, 1, false);
                        if (Main.netMode == 1)
                        {
                            NetMessage.SendData(21, -1, -1, null, k, 1f);
                        }

                    }
                    if (Main.rand.Next(15) == 1)
                    {
                        //player.QuickSpawnItem(null,mod.ItemType("BlueOrb"));
                        int k = Item.NewItem(Player.GetSource_ItemUse(item), (int)target.position.X + Main.rand.Next(-20, 20), (int)target.position.Y + Main.rand.Next(-20, 20), target.width, target.height, Mod.Find<ModItem>("BlueOrb").Type, 1, false);
                        if (Main.netMode == 1)
                        {
                            NetMessage.SendData(21, -1, -1, null, k, 1f);
                        }

                    }
                    if (Main.rand.Next(15) == 1)
                    {
                        //player.QuickSpawnItem(null,mod.ItemTypeif("YellowOrb"));
                        int k = Item.NewItem(Player.GetSource_ItemUse(item), (int)target.position.X + Main.rand.Next(-20, 20), (int)target.position.Y + Main.rand.Next(-20, 20), target.width, target.height, Mod.Find<ModItem>("YellowOrb").Type, 1, false);
                        if (Main.netMode == 1)
                        {
                            NetMessage.SendData(21, -1, -1, null, k, 1f);
                        }

                    }
                }
                else
                {
                    if (Main.rand.Next(3) == 1)
                    {
                        //player.QuickSpawnItem(null,mod.ItemType("BladeOrb"));
                        int k = Item.NewItem(Player.GetSource_ItemUse(item), (int)target.position.X + Main.rand.Next(-20, 20), (int)target.position.Y + Main.rand.Next(-20, 20), target.width, target.height, Mod.Find<ModItem>("BladeOrb").Type, 1, false);
                        if (Main.netMode == 1)
                        {
                            NetMessage.SendData(21, -1, -1, null, k, 1f);
                        }

                    }
                }
            }

            if (Glitterglue)
            {
                if (Main.rand.Next(0, 100) > 95)
                {
                    target.AddBuff(BuffType<Glitterglued>(), 240);
                }
            }
            if (Player.HasBuff(BuffType<AstarteDriver>()) && starfarerOutfit == 3)
            {
                Projectile.NewProjectile(null, target.Center.X, target.Center.Y, 0f, 0f, ProjectileType<StarfarerFollowUp>(), damage / 3, knockback, Player.whoAmI);

            }


            if (!target.active)
            {
                OnKillEnemy(target);
            }
            base.OnHitNPC(item, target, damage, knockback, crit);
        }
        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (target.type == NPCType<WarriorOfLight>())
            {
                inWarriorOfLightFightTimer = 4200;
            }
            if (target.type == NPCType<Nalhaun>())
            {
                inNalhaunFightTimer = 1200;

            }
            if (target.type == NPCType<Tsukiyomi>())
            {
                inTsukiyomiFightTimer = 1200;

            }
            if (target.type == NPCType<Arbitration>())
            {
                inArbiterFightTimer = 1200;

            }
            if (target.type == NPCType<Penthesilea>())
            {
                inPenthFightTimer = 1200;

            }
            if (target.lifeMax > 5)
            {
                inCombat = inCombatMax;
            }
            if (!target.active && luciferium)
            {
                Player.AddBuff(BuffType<SatedAnguish>(), 900);
            }
            if (!target.active && butchersdozen == 2)
            {
                butchersDozenKills++;
            }
            if (crit && flashfreeze == 2 && flashFreezeCooldown < 0)
            {
                //damage += target.lifeMax;
                SoundEngine.PlaySound(StarsAboveAudio.SFX_electroSmack, Player.Center);

                Projectile.NewProjectile(null, target.Center.X, target.Center.Y, 0, 0, Mod.Find<ModProjectile>("FlashFreezeExplosion").Type, damage / 4, 0, Player.whoAmI, 0f);
                flashFreezeCooldown = 240;


            }
            if (starfarerOutfit == 4)
            {
                hopesBrilliance++;
                if (target.target != Player.whoAmI)
                {
                    damage = (int)(damage * 0.6f);
                }
            }
            if (mysticforging == 2)
            {
                if (Player.statMana > 5)
                {
                    Player.statMana -= 5;
                    eternityGauge += 5;
                    damage = damage + (damage / 12);

                    Player.manaRegenDelay = 260;
                }
            }
            if (bloomingflames == 2 && (Player.statLife < 100 || Player.HasBuff(BuffType<InfernalEnd>())))
            {
                target.AddBuff(BuffID.OnFire, 60);
            }
            if (ruinedKingPrism && target.life <= target.lifeMax / 2 && crit)
            {
                target.AddBuff(BuffType<Buffs.Ruination>(), 1800);
            }
            if (Player.HasBuff(BuffType<Buffs.SovereignDominion>()) && target.HasBuff(BuffType<Buffs.Ruination>()))
            {
                if (crit)
                {
                    Player.statLife += damage / 10;
                }
            }
            if (Player.HasBuff(BuffType<Buffs.Kifrosse.AmaterasuGrace>()) && target.HasBuff(BuffID.Frostburn))
            {
                damage = damage + (damage / 2);
            }
            unbridledRadianceStack++;
            if (Player.HasBuff(BuffType<Buffs.AstarteDriver>()) && !target.HasBuff(BuffType<Buffs.AstarteDriverEnemyCooldown>()))
            {
                //Main.PlaySound(SoundLoader.customSoundType, (int)target.Center.X, (int)target.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/GunbladeImpact"));
                //screenShakeTimerGlobal = -80;
                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    crit = true;
                }

                else
                {
                    crit = false;
                }

                if (crit)
                {
                    damage = (int)(novaCritDamage * (1 + novaCritDamageMod));
                    damage /= 2;

                }
                else
                {
                    damage = (int)(novaDamage * (1 + novaDamageMod));
                }
                if (chosenStarfarer == 1)
                {
                    if (crit)
                    {
                        damage += baseNovaDamageAdd / 10;
                        if (damage >= target.lifeMax + target.defense)
                        {
                            astarteDriverAttacks++;
                        }
                    }
                }
                if (chosenStarfarer == 2)
                {
                    if (target.life < baseNovaDamageAdd / 10)
                    {
                        damage = baseNovaDamageAdd / 4;
                        crit = true;
                        //astarteDriverAttacks++;
                    }
                }
                onEnemyHitWithNova(target, 5, ref damage, ref crit);
                target.AddBuff(BuffType<Buffs.AstarteDriverEnemyCooldown>(), 20);
            }
            if (target.HasBuff(BuffType<Buffs.Starblight>()) && umbralentropy == 2)
            {
                Player.statLife += Math.Min(damage / 100, 5);
                Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                CombatText.NewText(textPos, new Color(43, 255, 43, 240), $"{Math.Min(damage / 100, 15)}", false, false);
                umbralEntropyCooldown = 60;
            }
            if (crit)
            {
                if (celestialevanesence == 2)
                {
                    Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                    CombatText.NewText(textPos, new Color(81, 62, 247, 240), $"{Math.Min((int)(damage * 0.05), 5)}", false, false);
                    Player.statMana += Math.Min((int)(damage * 0.05), 5);
                }
                if (weaknessexploit == 2)
                {
                    if (target.HasBuff(BuffType<Stun>()) || target.life < (int)(target.lifeMax * 0.2))
                    {
                        if (damage + (damage * 0.3) < target.life)
                        {
                            Rectangle textPos = new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height);
                            CombatText.NewText(textPos, new Color(255, 30, 30, 240), $"{Math.Round(damage * 0.3)}", false, false);
                            target.life -= (int)(damage * 0.3);
                        }
                    }
                    else
                    {
                        if (damage + (damage * 0.1) < target.life)
                        {
                            Rectangle textPos = new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height);
                            CombatText.NewText(textPos, new Color(255, 30, 30, 240), $"{Math.Round(damage * 0.1)}", false, false);
                            target.life -= (int)(damage * 0.1);
                        }
                    }

                }
                if (umbralentropy == 2)
                {
                    target.AddBuff(BuffType<Buffs.Starblight>(), 180);

                }
            }
            if (Player.HasBuff(BuffType<Buffs.SurtrTwilight>()))
            {
                target.AddBuff(BuffID.OnFire, 480);
            }
            if (crit && item.ModItem is ArchitectLuminance && MeleeAspect == 2)
            {
                damage += Player.statDefense;
            }
            if (crit && artofwar == 2)
            {
                if (Main.rand.Next(0, 100) < Player.GetCritChance(DamageClass.Generic))
                {
                    Rectangle textPos = new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height);
                    CombatText.NewText(textPos, new Color(180, 0, 0, 240), "!", false, false);
                    damage = (int)(damage * 1.5f);

                    if (Main.rand.Next(0, 100) < Player.GetCritChance(DamageClass.Generic))
                    {
                        CombatText.NewText(textPos, new Color(255, 0, 0, 240), "!!!", false, false);
                        damage = (int)(damage * 1.5f);
                    }
                }

            }

            base.ModifyHitNPC(item, target, ref damage, ref knockback, ref crit);

        }
        public override void OnConsumeMana(Item item, int manaConsumed)
        {
            if (euthymiaActive)
            {
                eternityGauge += manaConsumed;
            }

            base.OnConsumeMana(item, manaConsumed);
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            // if (novaUIActive || starfarerMenuActive || stellarArray)
            // {
            //    damage = 0;
            //     target.life++;
            //}
            // else
            // {
            if (target.lifeMax > 5)
            {
                inCombat = inCombatMax;
            }
            ///}
            if (target.type == NPCType<WarriorOfLight>())
            {
                inWarriorOfLightFightTimer = 4200;
            }
            if (target.type == NPCType<Nalhaun>())
            {
                inNalhaunFightTimer = 1200;
                if (isNalhaunInvincible)
                {

                }
            }
            if (target.type == NPCType<Tsukiyomi>())
            {
                inTsukiyomiFightTimer = 1200;

            }
            if (target.type == NPCType<Arbitration>())
            {
                inArbiterFightTimer = 1200;

            }
            if (target.type == NPCType<Penthesilea>())
            {
                inPenthFightTimer = 1200;

            }


            if (mysticforging == 2)
            {
                if (Player.statMana >= 5)
                {
                    Player.statMana -= 5;
                    eternityGauge += 5;
                    Player.manaRegenDelay = 260;
                    damage = damage + (damage / 12);
                }
            }
            if (bloomingflames == 2)
            {
                target.AddBuff(BuffID.OnFire, 60);
            }
            if (starfarerOutfit == 4)
            {
                hopesBrilliance++;
                if (target.target != Player.whoAmI)
                {
                    damage = (int)(damage * 0.6f);
                }
            }
            //unbridledRadianceStack++;
            if (Player.HasBuff(BuffType<Buffs.SurtrTwilight>()) && proj.type != Mod.Find<ModProjectile>("LaevateinnDamage").Type)
            {

                target.AddBuff(BuffID.OnFire, 480);
            }
            if (ruinedKingPrism && target.life <= target.lifeMax / 2 && crit)
            {
                target.AddBuff(BuffType<Buffs.Ruination>(), 1800);
            }
            if (Player.HasBuff(BuffType<Buffs.SovereignDominion>()) && target.HasBuff(BuffType<Buffs.Ruination>()))
            {
                if (crit)
                {
                    Player.statLife += damage / 10;
                }
            }
            if (target.HasBuff(BuffType<Buffs.Starblight>()) && umbralentropy == 2)
            {
                if (umbralEntropyCooldown <= 0)
                {
                    Player.statLife += Math.Min(damage / 100, 5);
                    Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                    CombatText.NewText(textPos, new Color(43, 255, 43, 240), $"{Math.Min(damage / 100, 15)}", false, false);
                    umbralEntropyCooldown = 60;
                }
                else
                {

                }

            }
            if (target.HasBuff(BuffType<Glitterglued>()))
            {
                if (!crit)
                {
                    if (Main.rand.Next(0, 100) > 70)
                    {
                        crit = true;
                    }
                }
            }
            if (proj.type == Mod.Find<ModProjectile>("AshenAmbitionExecute").Type)
            {
                if (target.life <= AshenAmbitionExecuteThreshold)
                {
                    damage = target.life;
                    crit = true;
                    //player.DelBuff(BuffType<AshenAmbitionCooldown>());

                    AshenExecuteKilled = true;

                }
            }
            if (proj.type == Mod.Find<ModProjectile>("Theofania").Type)
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_GunbladeImpact, Player.Center);
                screenShakeTimerGlobal = -80;
                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    crit = true;
                }
                else
                {
                    crit = false;
                }

                if (crit)
                {
                    novaGauge += trueNovaGaugeMax / 40;
                    damage = (int)(novaCritDamage * (1 + novaCritDamageMod));
                    damage /= 2;
                    damage /= 2;

                }
                else
                {
                    damage = (int)(novaDamage * (1 + novaDamageMod));
                    damage /= 2;
                }

                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 21, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 91, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 197, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 159, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);

                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 220, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                }

                target.AddBuff(BuffType<Buffs.VoidAtrophy1>(), 1800);
                onEnemyHitWithNova(target, 1, ref damage, ref crit);


            }

            if (proj.type == Mod.Find<ModProjectile>("Theofania2").Type)
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_GunbladeImpact, Player.Center);
                screenShakeTimerGlobal = -80;
                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    crit = true;
                }
                else
                {
                    crit = false;
                }

                if (crit)
                {
                    novaGauge += trueNovaGaugeMax / 40;
                    damage = (int)(novaCritDamage * (1 + novaCritDamageMod));
                    damage /= 2;
                    damage /= 2;

                }
                else
                {
                    damage = (int)(novaDamage * (1 + novaDamageMod));
                    damage /= 2;
                }

                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 21, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 91, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 197, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 159, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);

                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 220, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                }
                if (target.HasBuff(BuffType<Buffs.VoidAtrophy1>()))
                {
                    if (chosenStarfarer == 1)
                    {
                        target.AddBuff(BuffType<Buffs.VoidAtrophy2>(), 1800);
                    }
                    else
                    {
                        Player.AddBuff(BuffType<Buffs.VoidStrength>(), 300);
                    }

                }

                onEnemyHitWithNova(target, 1, ref damage, ref crit);

            }
            if (proj.type == Mod.Find<ModProjectile>("Theofania3").Type)
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_GunbladeImpact, Player.Center);
                screenShakeTimerGlobal = -80;
                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    crit = true;
                }
                else
                {
                    crit = false;
                }

                if (crit)
                {
                    novaGauge += trueNovaGaugeMax / 40;
                    damage = (int)(novaCritDamage * (1 + novaCritDamageMod));
                    damage /= 2;
                    damage /= 4;

                }
                else
                {
                    damage = (int)(novaDamage * (1 + novaDamageMod));
                    damage /= 4;
                }

                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 21, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 91, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 197, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 159, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);

                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 220, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                }
                if (target.HasBuff(BuffType<Buffs.VoidAtrophy1>()))
                {
                    if (chosenStarfarer == 1)
                    {
                        target.AddBuff(BuffType<Buffs.VoidAtrophy2>(), 1800);
                    }
                    else
                    {
                        Player.AddBuff(BuffType<Buffs.VoidStrength>(), 300);
                    }

                }

                onEnemyHitWithNova(target, 1, ref damage, ref crit);

            }
            if (proj.type == Mod.Find<ModProjectile>("LaevateinnDamage").Type)
            {
                //Main.PlaySound(SoundLoader.customSoundType, (int)target.Center.X, (int)target.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/GunbladeImpact"));
                //screenShakeTimerGlobal = -80;
                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    crit = true;
                }

                else
                {
                    crit = false;
                }

                if (crit)
                {
                    damage = (int)(novaCritDamage * (1 + novaCritDamageMod)) / 5;
                    damage /= 2;

                }
                else
                {
                    damage = (int)(novaDamage * (1 + novaDamageMod)) / 5;
                }
                if (target.HasBuff(BuffID.OnFire))
                {

                    damage += 100;

                    if (chosenStarfarer == 1)
                    {
                        if (crit)
                        {
                            damage += 100;

                        }
                        damage += 50;

                    }
                }
                onEnemyHitWithNova(target, 2, ref damage, ref crit);
                //target.AddBuff(BuffType<Buffs.VoidAtrophy>(), 180);
            }
            if (Player.HasBuff(BuffType<Buffs.AstarteDriver>()) && !target.HasBuff(BuffType<Buffs.AstarteDriverEnemyCooldown>()))
            {
                //Main.PlaySound(SoundLoader.customSoundType, (int)target.Center.X, (int)target.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/GunbladeImpact"));
                //screenShakeTimerGlobal = -80;
                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    crit = true;
                }

                else
                {
                    crit = false;
                }

                if (crit)
                {
                    damage = (int)(novaCritDamage * (1 + novaCritDamageMod));
                    damage /= 2;

                }
                else
                {
                    damage = (int)(novaDamage * (1 + novaDamageMod));
                }
                if (chosenStarfarer == 1)
                {
                    if (crit)
                    {
                        damage += baseNovaDamageAdd / 10;
                        if (damage >= target.lifeMax + target.defense)
                        {
                            astarteDriverAttacks++;
                        }
                    }
                }
                if (chosenStarfarer == 2)
                {
                    if (target.life < baseNovaDamageAdd / 2)
                    {
                        damage = baseNovaDamageAdd / 4;
                        crit = true;
                        //astarteDriverAttacks++;
                    }
                }
                onEnemyHitWithNova(target, 5, ref damage, ref crit);
                target.AddBuff(BuffType<Buffs.AstarteDriverEnemyCooldown>(), 20);
            }
            if (target.HasBuff(BuffType<Buffs.IrysGaze>()))
            {
                damage += 50;
                if (proj.minion)
                {
                    int uniqueCrit = Main.rand.Next(100);
                    if (uniqueCrit <= 15)
                    {
                        crit = true;
                    }

                }
            }
            if (target.HasBuff(BuffType<Buffs.InfernalBleed>()))
            {
                if (target.life - Math.Min((-(target.life - target.lifeMax)) * 0.02, 250) > 1)
                {
                    target.life -= (int)Math.Min((-(target.life - target.lifeMax)) * 0.02, 250);
                    Rectangle textPos = new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height);
                    CombatText.NewText(textPos, new Color(234, 0, 0, 100), $"{(int)Math.Min((-(target.life - target.lifeMax)) * 0.02, 250)}", false, false);
                }

                Player.AddBuff(BuffID.Rage, 480);
            }
            if (crit)
            {
                if (celestialevanesence == 2)
                {
                    Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                    CombatText.NewText(textPos, new Color(81, 62, 247, 240), $"{Math.Min((int)(damage * 0.05), 5)}", false, false);
                    Player.statMana += Math.Min((int)(damage * 0.05), 5);
                }
                if (weaknessexploit == 2)
                {
                    if (target.HasBuff(BuffType<Stun>()) || target.life < (int)(target.lifeMax * 0.2))
                    {
                        if (damage + (damage * 0.3) < target.life)
                        {
                            Rectangle textPos = new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height);
                            CombatText.NewText(textPos, new Color(255, 30, 30, 240), $"{Math.Round(damage * 0.3)}", false, false);
                            target.life -= (int)(damage * 0.3);
                        }
                    }
                    else
                    {
                        if (damage + (damage * 0.1) < target.life)
                        {
                            Rectangle textPos = new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height);
                            CombatText.NewText(textPos, new Color(255, 30, 30, 240), $"{Math.Round(damage * 0.1)}", false, false);
                            target.life -= (int)(damage * 0.1);
                        }
                    }
                }

                if (umbralentropy == 2)
                {
                    target.AddBuff(BuffType<Buffs.Starblight>(), 180);

                }
            }
            if (proj.type == Mod.Find<ModProjectile>("kiwamiryukenconfirm").Type)
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_CounterFinish, Player.Center);
                screenShakeTimerGlobal = -80;
                //Player.GetModPlayer<StarsAbovePlayer>().activateShockwaveEffect = true;
                novaGauge += 5;
                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    crit = true;
                }
                else
                {
                    crit = false;
                }
                if (chosenStarfarer == 1 && Player.statLife <= Player.statLifeMax2 / 2)
                {
                    crit = true;
                }

                if (crit)
                {

                    damage = (int)(novaCritDamage * (1 + novaCritDamageMod));
                    damage /= 2;

                }
                else
                {
                    damage = (int)(novaDamage * (1 + novaDamageMod));
                }

                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 21, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 91, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 197, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 159, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);

                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 220, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                }
                for (int i = 0; i < 70; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }
                // Fire Dust spawn
                for (int i = 0; i < 80; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 3f);
                    Main.dust[dustIndex].noGravity = true;
                    Main.dust[dustIndex].velocity *= 5f;
                    dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
                    Main.dust[dustIndex].velocity *= 3f;
                }
                // Large Smoke Gore spawn
                for (int g = 0; g < 4; g++)
                {
                    int goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                }
                //target.AddBuff(BuffType<Buffs.VoidAtrophy>(), 180);
                onEnemyHitWithNova(target, 3, ref damage, ref crit);
            }
            if (proj.type == Mod.Find<ModProjectile>("HawkmoonRound").Type)
            {
                damage += 10;
                crit = true;
            }
            if (proj.type == Mod.Find<ModProjectile>("KazimierzSeraphimProjectile").Type)
            {
                if (radiance > 0)
                {
                    if (radiance >= 5)
                    {
                        crit = true;
                    }
                    damage += radiance * 10;

                    for (int d = 0; d < 12; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default(Color), 1.5f);
                    }
                }
                radiance = 0;

            }
            if (proj.type == Mod.Find<ModProjectile>("TemporalTimepiece2").Type || proj.type == Mod.Find<ModProjectile>("TemporalTimepiece3").Type)
            {
                if (powderGauge >= 80)
                {
                    damage += 60;
                }
                powderGauge += 5;

            }
            if (proj.type == Mod.Find<ModProjectile>("SteelTempestSwing").Type)
            {
                if (Main.rand.Next(0, 101) >= 50)
                {
                    crit = true;
                }
                else
                {
                    crit = false;
                }
                soulUnboundDamage += damage;

            }
            if (proj.type == Mod.Find<ModProjectile>("SteelTempestSwing2").Type)
            {
                crit = false;
                Rectangle textPos = new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height);
                CombatText.NewText(textPos, new Color(255, 30, 30, 240), $"{(damage / 2)}", false, false);
                if (target.life - damage / 2 > 1)
                {
                    target.life -= (damage / 2);
                }
                soulUnboundDamage += damage;
            }
            if (proj.type == Mod.Find<ModProjectile>("SteelTempestSwing3").Type)
            {
                target.AddBuff(BuffType<Buffs.MortalWounds>(), 600);
                soulUnboundDamage += damage;
            }
            if (proj.type == Mod.Find<ModProjectile>("SteelTempestSwing4").Type)
            {
                if (target.HasBuff(BuffType<Buffs.MortalWounds>()))
                {

                    damage *= 10;
                    crit = true;
                    for (int d = 0; d < 40; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 235, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
                    }

                    int index = target.FindBuffIndex(BuffType<MortalWounds>());
                    if (index > -1)
                    {
                        target.DelBuff(index);
                    }
                }
                else
                {



                }
                soulUnboundDamage += damage;
            }

            if (proj.type == Mod.Find<ModProjectile>("RexLapisMeteor2").Type)
            {
                if (target.HasBuff(BuffType<Buffs.Petrified>()))
                {

                }
                else
                {
                    if(crit)
                    {
                        target.AddBuff(BuffType<Buffs.Petrified>(), 180);
                    }    
                    

                }

            }
            if (proj.type == Mod.Find<ModProjectile>("WhisperRound").Type)
            {
                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 113, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                }
                if (crit)
                {
                    damage /= 2;  //remove vanilla 2x bonus
                    damage = (int)(damage * 15.0f); //crank that baby up
                    Player.GetModPlayer<StarsAbovePlayer>().whisperShotCount = 0;
                    for (int d = 0; d < 30; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 21, 0f + Main.rand.Next(-30, 30), 0f + Main.rand.Next(-30, 30), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 20; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 45, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                    }
                }

            }
            if (proj.type == Mod.Find<ModProjectile>("SatanaelRound").Type)
            {
                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 115, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                }


            }
            if (proj.type == Mod.Find<ModProjectile>("TakodachiRound").Type)
            {
                takodachiGauge++;


            }
            if (proj.type == Mod.Find<ModProjectile>("TakonomiconLaser").Type)
            {
                damage /= 3;
                takodachiGauge += 3;


            }
            if (proj.type == Mod.Find<ModProjectile>("TwinStarLaser1").Type || proj.type == Mod.Find<ModProjectile>("TwinStarLaser2").Type)
            {
                for (int d = 0; d < 2; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 20, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 2; d++)
                {
                    Dust dust1 = Dust.NewDustDirect(target.position, target.width, target.height, 91, 0f + Main.rand.Next(-1, 1), 0f + Main.rand.Next(-1, 1), 150, default(Color), 1.5f);
                    dust1.noGravity = true;
                }
                if (crit)
                {
                    Player.AddBuff(BuffType<BinaryMagnitude>(), 5);
                }
                if (Player.statMana > 250)
                {
                    damage = (int)(damage * 1.50f);
                }
                damage += Player.statManaMax2 / 8;

            }
            if (proj.type == Mod.Find<ModProjectile>("ForceOfNatureRound").Type)
            {

                Player.AddBuff(BuffID.Swiftness, 240);



            }
            if (proj.type == Mod.Find<ModProjectile>("IzanagiRound").Type)
            {
                if (edgeHoned)
                {
                    damage /= 2;  //remove vanilla 2x bonus
                    damage = (int)(damage * 10.0f); //that's a lot of damage
                    if (MeleeAspect == 2)
                    {

                    }
                    else
                    {
                        Player.statMana += 100;
                    }


                }
                else
                {

                    if (crit)
                    {
                        damage /= 2;  //remove vanilla 2x bonus
                        damage = (int)(damage * 4.0f); //crank that baby up
                    }
                }

            }
            if (proj.type == Mod.Find<ModProjectile>("AmiyaSlashBurst").Type)
            {
                crit = true;


            }
            if (proj.type == Mod.Find<ModProjectile>("HullwroughtRound").Type)
            {
                if (savedHullwroughtShot >= 5)
                {
                    crit = true;
                    target.AddBuff(BuffType<Buffs.Stun>(), 20);
                }
                damage += savedHullwroughtShot * 180;


            }
            if (proj.type == Mod.Find<ModProjectile>("BloodSlash1").Type || proj.type == Mod.Find<ModProjectile>("BloodSlash2").Type || proj.type == Mod.Find<ModProjectile>("BladeArtDragon").Type)
            {
                if (crit && Player.statLife < Player.statLifeMax2)
                {
                    damage *= -(Player.statLife / Player.statLifeMax2) + 1;
                }



            }

            if (proj.type == Mod.Find<ModProjectile>("CarianSwing1").Type || proj.type == Mod.Find<ModProjectile>("CarianSwing2").Type)
            {
                if (crit)
                {
                    target.AddBuff(BuffID.Frostburn, 180);
                }

            }
            if (proj.type == Mod.Find<ModProjectile>("CarianSwingE1").Type || proj.type == Mod.Find<ModProjectile>("CarianSwingE2").Type)
            {

                target.AddBuff(BuffID.Frostburn, 180);


            }
            if (proj.type == Mod.Find<ModProjectile>("CosmicDestroyerRound").Type)
            {
                /*if (target.life < (target.lifeMax/2))
                {
                    crit = true;
                }*/
                CosmicDestroyerGauge++;
                if (CosmicDestroyerGauge > 100)
                {
                    CosmicDestroyerGauge = 100;
                }

            }
            if (proj.type == Mod.Find<ModProjectile>("CosmicDestroyerRound2").Type)
            {
                if (target.life < (target.lifeMax / 2))
                {
                    crit = true;
                }


            }
            if (proj.type == Mod.Find<ModProjectile>("SkyStrikerRailgunRound").Type)
            {
                if (crit)
                {
                    damage *= 2;
                    for (int d = 0; d < 10; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 220, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 5; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 20, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                    }
                }


            }
            if (proj.type == Mod.Find<ModProjectile>("SkyStrikerSwing1").Type || proj.type == Mod.Find<ModProjectile>("SkyStrikerSwing2").Type)
            {
                if (crit)
                {
                    if (target.HasBuff(BuffID.OnFire))
                    {
                        damage += 50;
                        int index = target.FindBuffIndex(BuffID.OnFire);
                        if (index > -1)
                        {
                            target.DelBuff(index);
                        }
                    }
                }
                else
                {
                    target.AddBuff(BuffID.OnFire, 480);
                }


            }
            if (proj.type == Mod.Find<ModProjectile>("AmiyaSwing1").Type || proj.type == Mod.Find<ModProjectile>("AmiyaSwing2").Type)
            {
                if (!Player.HasBuff(BuffType<Burnout>()))
                {
                    ceruleanFlameGauge += 1;
                    if (crit)
                    {
                        ceruleanFlameGauge += 4;
                    }

                }
                if (ceruleanFlameGauge >= 100)//If 'Burnout' then no charge
                {
                    ceruleanFlameGauge = 100;
                }

                //target.AddBuff(BuffType<Buffs.Stun>(), 120);

            }
            if (proj.type == Mod.Find<ModProjectile>("AmiyaSwingE1").Type || proj.type == Mod.Find<ModProjectile>("AmiyaSwingE2").Type)
            {
                crit = true;

                //target.AddBuff(BuffType<Buffs.Stun>(), 120);

            }
            if (proj.type == Mod.Find<ModProjectile>("AmiyaSlash").Type)
            {
                target.AddBuff(BuffID.Frostburn, 1200);
                target.AddBuff(BuffType<Buffs.Stun>(), 60);

            }
            if (proj.type == Mod.Find<ModProjectile>("OutbreakRound").Type)
            {

                if (target.HasBuff(BuffType<Buffs.NanitePlague>()))
                {
                    target.GetGlobalNPC<StarsAboveGlobalNPC>().NanitePlagueLevel++;
                    damage += target.GetGlobalNPC<StarsAboveGlobalNPC>().NanitePlagueLevel;
                    if (crit)
                    {
                        damage /= 2;  //remove vanilla 2x bonus
                        damage = (int)(damage * 3.0f); //crank that baby up
                    }


                }
                else
                {
                    target.AddBuff(BuffType<Buffs.NanitePlague>(), 360);


                }

            }
            if (proj.type == Mod.Find<ModProjectile>("NaganadelProjectileFinal1").Type)
            {
                damage /= 2;
                for (int d = 0; d < 15; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 172, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                }

            }
            if (proj.type == Mod.Find<ModProjectile>("NaganadelProjectileFinal2").Type)
            {
                Player.AddBuff(BuffID.Swiftness, 420);


                if (crit)
                {
                    damage /= 2;  //remove vanilla 2x bonus
                    damage = (int)(damage * 6.0f); //crank that baby up
                    for (int d = 0; d < 30; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 247, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 20; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 45, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                    }
                }
            }
            if (proj.type == Mod.Find<ModProjectile>("NaganadelProjectileFinal3").Type)
            {
                if (target.HasBuff(BuffID.OnFire))
                {

                    damage *= 2;

                }
                target.AddBuff(BuffID.OnFire, 640);

            }
            if (proj.type == Mod.Find<ModProjectile>("NaganadelProjectileFinal4").Type)
            {
                for (int d = 0; d < 5; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 254, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 272, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                }

            }
            if (proj.type == Mod.Find<ModProjectile>("NaganadelProjectileFinal5").Type)
            {
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 247, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
                }

                if (crit)
                {
                    damage /= 2;  //remove vanilla 2x bonus
                    damage = (int)(damage * 3.0f); //crank that baby up
                }
                target.AddBuff(BuffID.Poisoned, 240);

            }
            if (proj.type == Mod.Find<ModProjectile>("ScarletOutburst").Type)
            {

                target.AddBuff(BuffID.OnFire, 180);

            }
            if (proj.type == Mod.Find<ModProjectile>("kiwamiryukenstun").Type)
            {

                target.AddBuff(BuffType<Buffs.RyukenStun>(), 60);

            }
            if (proj.type == Mod.Find<ModProjectile>("ButterflyProjectile").Type)
            {
                if (target.HasBuff(BuffID.Confused))
                {

                    damage += 12;

                }
                target.AddBuff(BuffID.Confused, 340);

            }
            if (proj.type == Mod.Find<ModProjectile>("MelusineBeam").Type)
            {
                if (target.HasBuff(BuffID.Frostburn))
                {
                    damage += 50;
                    crit = true;
                    int index = target.FindBuffIndex(BuffID.Frostburn);
                    if (index > -1)
                    {
                        target.DelBuff(index);
                    }
                }
                if (Main.rand.Next(0, 100) <= 50)
                {
                    // Fire Dust spawn
                    for (int i = 0; i < 20; i++)
                    {
                        int dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 3f);
                        Main.dust[dustIndex].noGravity = true;
                        Main.dust[dustIndex].velocity *= 5f;
                        dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
                        Main.dust[dustIndex].velocity *= 3f;
                    }
                    target.AddBuff(BuffID.OnFire, 360);
                    for (int d = 0; d < 15; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 55, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 51), 150, default(Color), 1.5f);
                    }

                }
                for (int d = 0; d < 15; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 100, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                }

                // Smoke Dust spawn
                for (int i = 0; i < 10; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }

                // Large Smoke Gore spawn
                for (int g = 0; g < 2; g++)
                {
                    int goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                }
            }
            if (proj.type == Mod.Find<ModProjectile>("ArondightBeam").Type)
            {
                if (target.HasBuff(BuffID.OnFire))
                {
                    damage += 50;
                    crit = true;
                    int index = target.FindBuffIndex(BuffID.OnFire);
                    if (index > -1)
                    {
                        target.DelBuff(index);
                    }
                }
                if (Main.rand.Next(0, 100) <= 50)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        int dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 20, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 3f);
                        Main.dust[dustIndex].noGravity = true;
                        Main.dust[dustIndex].velocity *= 5f;
                        dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 20, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
                        Main.dust[dustIndex].velocity *= 3f;
                    }
                    target.AddBuff(BuffID.Frostburn, 360);
                    for (int d = 0; d < 15; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 157, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
                    }

                }
                for (int d = 0; d < 15; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 150, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                }
                // Smoke Dust spawn
                for (int i = 0; i < 10; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }
                // Fire Dust spawn

                // Large Smoke Gore spawn
                for (int g = 0; g < 2; g++)
                {
                    int goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                }

            }
            if (proj.type == Mod.Find<ModProjectile>("MonadoEmpoweredCritSwing").Type)
            {

                crit = true;

            }
            if (proj.type == Mod.Find<ModProjectile>("RiptideBolt").Type)
            {

                target.AddBuff(BuffType<Buffs.Riptide>(), 720);

            }
            if (proj.type == Mod.Find<ModProjectile>("IzanagiRound").Type)
            {
                if (edgeHoned)
                {

                }
                else
                {
                    Player.GetModPlayer<StarsAbovePlayer>().izanagiPerfect += 1;

                }

            }
            if (proj.type == Mod.Find<ModProjectile>("Bubble").Type)
            {
                if (target.HasBuff(BuffType<Buffs.Riptide>()))
                {
                    crit = true;
                }
                else
                {
                    if (Main.rand.Next(0, 101) <= 30)//
                    {
                        crit = true;
                    }

                }

            }
            if (proj.type == Mod.Find<ModProjectile>("Starchild").Type)
            {
                if (Main.rand.Next(25) == 1)
                {
                    //player.QuickSpawnItem(null,mod.ItemType("RedOrb"));
                    //Main.PlaySound(SoundLoader.customSoundType, (int)target.Center.X, (int)target.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/StarbitCollected"));

                    int k = Item.NewItem(Player.GetSource_Misc("PlayerDropItemCheck"), (int)target.position.X + Main.rand.Next(-250, 250), (int)target.position.Y + Main.rand.Next(-250, -50), target.width * 2, target.height, Mod.Find<ModItem>("StarBitRed").Type, 1, false);
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(21, -1, -1, null, k, 1f);
                    }

                }
                if (Main.rand.Next(25) == 1)
                {
                    // Main.PlaySound(SoundLoader.customSoundType, (int)target.Center.X, (int)target.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/StarbitCollected"));

                    //player.QuickSpawnItem(null,mod.ItemType("RedOrb"));
                    int k = Item.NewItem(Player.GetSource_Misc("PlayerDropItemCheck"), (int)target.position.X + Main.rand.Next(-250, 250), (int)target.position.Y + Main.rand.Next(-250, -50), target.width * 2, target.height, Mod.Find<ModItem>("StarBitOrange").Type, 1, false);
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(21, -1, -1, null, k, 1f);
                    }

                }
                if (Main.rand.Next(25) == 1)
                {
                    //Main.PlaySound(SoundLoader.customSoundType, (int)target.Center.X, (int)target.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/StarbitCollected"));

                    //player.QuickSpawnItem(null,mod.ItemType("RedOrb"));
                    int k = Item.NewItem(Player.GetSource_Misc("PlayerDropItemCheck"), (int)target.position.X + Main.rand.Next(-250, 250), (int)target.position.Y + Main.rand.Next(-250, -50), target.width * 2, target.height, Mod.Find<ModItem>("StarBitYellow").Type, 1, false);
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(21, -1, -1, null, k, 1f);
                    }

                }
                if (Main.rand.Next(25) == 1)
                {
                    //Main.PlaySound(SoundLoader.customSoundType, (int)target.Center.X, (int)target.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/StarbitCollected"));

                    //player.QuickSpawnItem(null,mod.ItemType("RedOrb"));
                    int k = Item.NewItem(Player.GetSource_Misc("PlayerDropItemCheck"), (int)target.position.X + Main.rand.Next(-250, 250), (int)target.position.Y + Main.rand.Next(-250, -50), target.width * 2, target.height, Mod.Find<ModItem>("StarBitGreen").Type, 1, false);
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(21, -1, -1, null, k, 1f);
                    }

                }
                if (Main.rand.Next(25) == 1)
                {
                    //Main.PlaySound(SoundLoader.customSoundType, (int)target.Center.X, (int)target.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/StarbitCollected"));

                    //player.QuickSpawnItem(null,mod.ItemType("RedOrb"));
                    int k = Item.NewItem(Player.GetSource_Misc("PlayerDropItemCheck"), (int)target.position.X + Main.rand.Next(-250, 250), (int)target.position.Y + Main.rand.Next(-250, -50), target.width * 2, target.height, Mod.Find<ModItem>("StarBitBlue").Type, 1, false);
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(21, -1, -1, null, k, 1f);
                    }

                }
                if (Main.rand.Next(25) == 1)
                {
                    //Main.PlaySound(SoundLoader.customSoundType, (int)target.Center.X, (int)target.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/StarbitCollected"));

                    //player.QuickSpawnItem(null,mod.ItemType("RedOrb"));
                    int k = Item.NewItem(Player.GetSource_Misc("PlayerDropItemCheck"), (int)target.position.X + Main.rand.Next(-250, 250), (int)target.position.Y + Main.rand.Next(-250, -50), target.width * 2, target.height, Mod.Find<ModItem>("StarBitPurple").Type, 1, false);
                    if (Main.netMode == 1)
                    {
                        NetMessage.SendData(21, -1, -1, null, k, 1f);
                    }

                }
            }
            if (proj.minion && proj.type != Mod.Find<ModProjectile>("ApalistikProjectile").Type && proj.type != Mod.Find<ModProjectile>("ApalistikUpgradedProjectile").Type && (Main.LocalPlayer.HeldItem.ModItem is Apalistik || Main.LocalPlayer.HeldItem.ModItem is ApalistikUpgraded))
            {
                if (Main.rand.Next(0, 101) <= 10)//
                {
                    target.AddBuff(BuffType<Buffs.OceanCulling>(), 240);
                }

            }
            if (proj.type == Mod.Find<ModProjectile>("StygianSwing1").Type)
            {
                //duality += 10;

            }
            if (proj.type == Mod.Find<ModProjectile>("PaintSwingR").Type)
            {
                Projectile.NewProjectile(null, target.Center, Vector2.Zero, Mod.Find<ModProjectile>("SplatterRed").Type, 0, 0, Player.whoAmI, 0, 1);
                target.AddBuff(BuffID.OnFire, 240);
                if (target.HasBuff(BuffType<RedPaint>()))
                {
                    crit = true;
                }
                else
                {
                    if (target.HasBuff(BuffType<GreenPaint>()))
                    {
                        damage /= 3;

                    }
                    else
                    {
                        damage /= 2;

                    }
                }

            }
            if (proj.type == Mod.Find<ModProjectile>("PaintSwingO").Type)
            {
                Projectile.NewProjectile(null, target.Center, Vector2.Zero, Mod.Find<ModProjectile>("SplatterOrange").Type, 0, 0, Player.whoAmI, 0, 1);
                target.AddBuff(BuffID.Ichor, 240);
                if (target.HasBuff(BuffType<OrangePaint>()))
                {
                    crit = true;
                }
                else
                {
                    if (target.HasBuff(BuffType<BluePaint>()))
                    {
                        damage /= 3;

                    }
                    else
                    {
                        damage /= 2;

                    }
                }
            }
            if (proj.type == Mod.Find<ModProjectile>("PaintSwingY").Type)
            {
                Projectile.NewProjectile(null, target.Center, Vector2.Zero, Mod.Find<ModProjectile>("SplatterYellow").Type, 0, 0, Player.whoAmI, 0, 1);
                target.AddBuff(BuffID.Midas, 240);
                if (target.HasBuff(BuffType<YellowPaint>()))
                {
                    crit = true;
                }
                else
                {
                    if (target.HasBuff(BuffType<PurplePaint>()))
                    {
                        damage /= 3;

                    }
                    else
                    {
                        damage /= 2;

                    }
                }
            }
            if (proj.type == Mod.Find<ModProjectile>("PaintSwingG").Type)
            {
                Projectile.NewProjectile(null, target.Center, Vector2.Zero, Mod.Find<ModProjectile>("SplatterGreen").Type, 0, 0, Player.whoAmI, 0, 1);
                target.AddBuff(BuffID.CursedInferno, 240);
                if (target.HasBuff(BuffType<GreenPaint>()))
                {
                    crit = true;
                }
                else
                {
                    if (target.HasBuff(BuffType<RedPaint>()))
                    {
                        damage /= 3;

                    }
                    else
                    {
                        damage /= 2;

                    }
                }
            }
            if (proj.type == Mod.Find<ModProjectile>("PaintSwingB").Type)
            {
                Projectile.NewProjectile(null, target.Center, Vector2.Zero, Mod.Find<ModProjectile>("SplatterBlue").Type, 0, 0, Player.whoAmI, 0, 1);
                target.AddBuff(BuffID.Frostburn, 240);
                if (target.HasBuff(BuffType<BluePaint>()))
                {
                    crit = true;
                }
                else
                {
                    if (target.HasBuff(BuffType<OrangePaint>()))
                    {
                        damage /= 3;

                    }
                    else
                    {
                        damage /= 2;

                    }

                }
            }
            if (proj.type == Mod.Find<ModProjectile>("PaintSwingP").Type)
            {
                Projectile.NewProjectile(null, target.Center, Vector2.Zero, Mod.Find<ModProjectile>("SplatterPurple").Type, 0, 0, Player.whoAmI, 0, 1);
                target.AddBuff(BuffID.Venom, 240);
                if (target.HasBuff(BuffType<PurplePaint>()))
                {
                    crit = true;
                }
                else
                {
                    if (target.HasBuff(BuffType<YellowPaint>()))
                    {
                        damage /= 3;

                    }
                    else
                    {
                        damage /= 2;

                    }
                }
            }
            if (proj.minion && proj.type != Mod.Find<ModProjectile>("ApalistikProjectile").Type && proj.type != Mod.Find<ModProjectile>("ApalistikUpgradedProjectile").Type)
            {
                if (target.HasBuff(BuffType<Buffs.Riptide>()))
                {

                    damage = (int)(damage * 1.3f);

                    for (int d = 0; d < 30; d++)
                    {
                        Dust.NewDust(target.Center, 0, 0, 15, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 28; d++)
                    {
                        Dust.NewDust(target.Center, 0, 0, DustType<Dusts.bubble>(), 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-15, 15), 0, default(Color), 1.5f);
                    }

                    int index = target.FindBuffIndex(BuffType<Buffs.Riptide>());
                    if (index > -1)
                    {
                        target.DelBuff(index);
                    }
                }

            }
            if (proj.type == Mod.Find<ModProjectile>("ApalistikProjectile").Type || proj.type == Mod.Find<ModProjectile>("ApalistikUpgradedProjectile").Type)
            {

                target.AddBuff(BuffType<Buffs.Riptide>(), 240);


                if (target.HasBuff(BuffType<Buffs.OceanCulling>()))
                {
                    damage = (int)(damage * 1.5f);
                    for (int i = 0; i < 5; i++)
                    {

                        Vector2 vel = new Vector2(Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-4, 4));
                        Projectile.NewProjectile(null, target.Center, vel, Mod.Find<ModProjectile>("Bubble").Type, damage / 8, 3, Player.whoAmI, 0, 1);
                    }

                    int index = target.FindBuffIndex(BuffType<Buffs.OceanCulling>());
                    if (index > -1)
                    {
                        target.DelBuff(index);
                    }
                }
            }
            if (proj.type == Mod.Find<ModProjectile>("UltimaPlanet1").Type || proj.type == Mod.Find<ModProjectile>("UltimaPlanet2").Type || proj.type == Mod.Find<ModProjectile>("UltimaPlanet3").Type || proj.type == Mod.Find<ModProjectile>("UltimaPlanet4").Type || proj.type == Mod.Find<ModProjectile>("UltimaPlanet5").Type)
            {

                //target.AddBuff(BuffType<Buffs.Riptide>(), 240); Spatial rend?
                Player.AddBuff(BuffType<Buffs.UniversalManipulation>(), 720);
                crit = true;


                for (int i = 0; i < 3; i++)
                {

                    Vector2 vel = new Vector2(Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-4, 4));
                    Projectile.NewProjectile(null, target.Center, vel, Mod.Find<ModProjectile>("Asteroid").Type, damage / 4, 3, Player.whoAmI, 0, 1);
                }
                //Projectile.NewProjectile(null, target.Center.X, target.Center.Y, 0, 0, Mod.Find<ModProjectile>("UltimaFollowUp").Type, 0, 0, Player.whoAmI, 0f);

            }
            if (proj.type == Mod.Find<ModProjectile>("UltimaSwing1").Type)
            {
                if (Player.HasBuff(BuffType<Buffs.UniversalManipulation>()))
                {
                    int index = Player.FindBuffIndex(BuffType<UniversalManipulation>());
                    if (index > -1)
                    {
                        Player.DelBuff(index);
                    }
                    Player.AddBuff(BuffType<Buffs.CelestialCacophony>(), 720);
                }
                //Projectile.NewProjectile(null, target.Center.X, target.Center.Y, 0, 0, Mod.Find<ModProjectile>("UltimaFollowUp").Type, 0, 0, Player.whoAmI, 0f);
            }
            if (proj.type == Mod.Find<ModProjectile>("tartagliaSwing").Type)
            {
                if (target.HasBuff(BuffType<Buffs.Riptide>()))
                {
                    damage += 90;
                }

            }
            if (proj.type == Mod.Find<ModProjectile>("Starchild").Type)
            {
                damage += Player.statDefense;

            }
            if (proj.type == Mod.Find<ModProjectile>("ClaimhBurst").Type)
            {
                crit = true;
                damage += (Player.statDefense * 20) + (radiance * 50);

            }
            if (proj.type == Mod.Find<ModProjectile>("YellowStarBit").Type)
            {
                target.AddBuff(BuffType<Buffs.Stun>(), 60);

            }
            if (proj.type == Mod.Find<ModProjectile>("GreenStarBit").Type)
            {
                if (Main.rand.Next(0, 101) <= 30)
                {
                    crit = true;
                }

            }
            if (proj.type == Mod.Find<ModProjectile>("BlueStarBit").Type)
            {
                Player.statMana += 10;

            }
            if (proj.type == Mod.Find<ModProjectile>("OrangeStarBit").Type)
            {
                target.AddBuff(BuffID.OnFire, 240);

            }
            if (proj.type == Mod.Find<ModProjectile>("PurpleStarBit").Type)
            {
                target.AddBuff(BuffType<Buffs.Starblight>(), 240);

            }
            if (proj.type == Mod.Find<ModProjectile>("PhantomInTheMirrorProjectile").Type)
            {
                target.AddBuff(BuffType<PhantomTagDamage>(), 240);
                target.AddBuff(BuffID.Frostburn, 120);

            }
            if (proj.type == Mod.Find<ModProjectile>("BloodstainedCrescent").Type)
            {

                if (target.HasBuff(BuffID.Frostburn))
                {
                    Player.statMana += 90;
                    damage += 200;
                    int index = target.FindBuffIndex(BuffID.Frostburn);
                    if (index > -1)
                    {
                        target.DelBuff(index);
                    }
                }



            }
            if (proj.type == Mod.Find<ModProjectile>("BuryTheLightSlash").Type)
            {
                if (target.HasBuff(BuffID.ShadowFlame) && crit)
                {
                    judgementGauge += 1;

                    if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
                    {
                        damage += 132;
                    }
                    else
                    {

                    }
                    damage += 90;
                }
                if (target.HasBuff(BuffID.Frostburn) && crit)
                {
                    judgementGauge += 1;


                    if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
                    {
                        damage += 132;
                    }
                    else
                    {

                    }
                    damage += 50;
                    target.AddBuff(BuffID.ShadowFlame, 1200);
                }
                if (target.HasBuff(BuffType<Buffs.Starblight>()) && crit)
                {
                    judgementGauge += 1;
                    /* 
                    if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
                    {
                        damage += 132;
                    }
                    else
                    {
                        
                    }*/
                    damage += 20;
                    target.AddBuff(BuffID.Frostburn, 1200);
                }


                if (crit)
                {
                    target.AddBuff(BuffType<Buffs.Starblight>(), 1200);
                }

            }
            if (proj.type == Mod.Find<ModProjectile>("BuryTheLightSlash2").Type)
            {
                if (target.HasBuff(BuffType<Buffs.Starblight>()))
                {
                    crit = true;
                    /* 
                    if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
                    {
                        damage += 3052;
                    }
                    else
                    {
                       
                    }*/
                    damage += 1000;
                    int index = target.FindBuffIndex(BuffType<Buffs.Starblight>());
                    if (index > -1)
                    {
                        target.DelBuff(index);
                    }
                }
                if (target.HasBuff(BuffID.Frostburn))
                {
                    crit = true;
                    damage += 2000;

                    target.AddBuff(BuffID.ShadowFlame, 1200);
                    int index = target.FindBuffIndex(BuffID.Frostburn);
                    if (index > -1)
                    {
                        target.DelBuff(index);
                    }
                }
                if (target.HasBuff(BuffID.ShadowFlame))
                {
                    crit = true;
                    /* 
                    if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
                    {
                        damage += 5122;
                        damage += Math.Min((target.lifeMax / 10), 1000000);

                    }
                    else
                    {
                        
                    }*/
                    damage += 3000;
                    int index = target.FindBuffIndex(BuffID.ShadowFlame);
                    if (index > -1)
                    {
                        target.DelBuff(index);
                    }
                }


            }

            if (proj.type == Mod.Find<ModProjectile>("AegisDriverOn").Type)
            {

                if (target.HasBuff(BuffID.OnFire))
                {
                    target.AddBuff(BuffID.OnFire, 640);
                    damage += 5;

                }
                target.AddBuff(BuffID.OnFire, 640);

                if (Player.GetModPlayer<StarsAbovePlayer>().aegisGauge >= 100)
                {
                    damage *= 2;
                    crit = true;
                    Player.GetModPlayer<StarsAbovePlayer>().aegisGauge = 0;
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_GunbladeImpact, Player.Center);

                    for (int d = 0; d < 30; d++)
                    {
                        Dust.NewDust(target.Center, 0, 0, 0, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 44; d++)
                    {
                        Dust.NewDust(target.Center, 0, 0, 0, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 26; d++)
                    {
                        Dust.NewDust(target.Center, 0, 0, 133, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 150, default(Color), 1.5f);
                    }

                    // Smoke Dust spawn
                    for (int i = 0; i < 70; i++)
                    {
                        int dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
                        Main.dust[dustIndex].velocity *= 1.4f;
                    }
                    // Fire Dust spawn
                    for (int i = 0; i < 80; i++)
                    {
                        int dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 3f);
                        Main.dust[dustIndex].noGravity = true;
                        Main.dust[dustIndex].velocity *= 5f;
                        dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
                        Main.dust[dustIndex].velocity *= 3f;
                    }
                    // Large Smoke Gore spawn
                    for (int g = 0; g < 4; g++)
                    {
                        int goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                        goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                        goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                        goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[goreIndex].scale = 1.5f;
                        Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                        Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    }
                }

            }
            if (proj.type == Mod.Find<ModProjectile>("GenocideArtillery").Type || proj.type == Mod.Find<ModProjectile>("GenocideRound").Type || proj.type == Mod.Find<ModProjectile>("GenocideRoundBlast").Type || proj.type == Mod.Find<ModProjectile>("GenocideRoundFinalBlast").Type || proj.type == Mod.Find<ModProjectile>("GenocideArtilleryBlast").Type)
            {

                if (target.HasBuff(BuffType<MortalWounds>()))
                {

                    damage += damage / 2;

                }



            }
            if (proj.type == Mod.Find<ModProjectile>("GenocideRound").Type || proj.type == Mod.Find<ModProjectile>("GenocideRoundBlast").Type || proj.type == Mod.Find<ModProjectile>("GenocideRoundFinalBlast").Type)
            {
                Player.AddBuff(BuffType<GenocideBuff>(), 240);
            }
            if (crit && artofwar == 2)
            {
                if (Main.rand.Next(0, 100) < Player.GetCritChance(DamageClass.Generic))
                {


                    if (Main.rand.Next(0, 100) < Player.GetCritChance(DamageClass.Generic))
                    {
                        Rectangle textPos = new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height);
                        CombatText.NewText(textPos, new Color(255, 0, 0, 240), "Triple crit!", true, false);
                        damage = (int)(damage * 2f);
                    }
                    else
                    {
                        Rectangle textPos = new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height);
                        CombatText.NewText(textPos, new Color(180, 0, 0, 240), "Double crit!", true, false);
                        damage = (int)(damage * 1.5f);
                    }
                }
            }
           
            base.ModifyHitNPCWithProj(proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
        }
        public override void ModifyScreenPosition()
        {
            Vector2 centerScreen = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
            if (lookAtTsukiyomi)
            {
                screenCache = Vector2.Lerp(screenCache, TsukiyomiLocation - centerScreen, 0.1f);
                Main.screenPosition = screenCache;

            }
            else
            {
                screenCache = Main.screenPosition;
            }
            if (lookAtWarrior)
            {
                screenCache = Vector2.Lerp(screenCache, WarriorLocation - centerScreen, 0.1f);
                Main.screenPosition = screenCache;

            }
            else
            {
                screenCache = Main.screenPosition;
            }
            /*if (lookAtTsukiyomi)
            {
                
                
                //Main.screenPosition = new Vector2(TsukiyomiLocation.X - (Main.screenWidth/2),TsukiyomiLocation.Y - (Main.screenHeight/2));
                
            }*/

            if (judgementCutTimer < 0 && judgementCutTimer > -100)
            {
                Main.screenPosition += new Vector2(Main.rand.Next(-screenShakeVelocity / 100, screenShakeVelocity / 100), Main.rand.Next(-screenShakeVelocity / 100, screenShakeVelocity / 100));
                if (screenShakeVelocity >= 100)
                {
                    screenShakeVelocity -= 10;

                }
            }
            else
            {
                screenShakeVelocity = 1000;

            }
            if (screenShakeTimerGlobal < 0 && screenShakeTimerGlobal > -100)
            {
                Main.screenPosition += new Vector2(Main.rand.Next(-screenShakeVelocity / 100, screenShakeVelocity / 100), Main.rand.Next(-screenShakeVelocity / 100, screenShakeVelocity / 100));
                if (screenShakeVelocity >= 100)
                {
                    screenShakeVelocity -= 10;

                }
            }
            else
            {
                screenShakeVelocity = 1000;

            }
            base.ModifyScreenPosition();
        }

        public override void OnHitNPCWithProj(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (ruinedKingPrism && target.life <= target.lifeMax / 2 && crit)
            {
                target.AddBuff(BuffType<Buffs.Ruination>(), 1800);
            }
            if (Player.HasBuff(BuffType<BoilingBloodBuff>()))
            {
                boilingBloodDamage += damage / 4;
            }
           

            if (crit)
            {
                if (weaknessexploit == 2)
                {
                    if (target.HasBuff(BuffType<Stun>()) || target.life < (int)(target.lifeMax * 0.2))
                    {
                        if (damage + (damage * 0.3) < target.life)
                        {
                            Rectangle textPos = new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height);
                            CombatText.NewText(textPos, new Color(255, 30, 30, 240), $"{Math.Round(damage * 0.3)}", false, false);
                            target.life -= (int)(damage * 0.3);
                        }
                    }
                    else
                    {
                        if (damage + (damage * 0.1) < target.life)
                        {
                            Rectangle textPos = new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height);
                            CombatText.NewText(textPos, new Color(255, 30, 30, 240), $"{Math.Round(damage * 0.1)}", false, false);
                            target.life -= (int)(damage * 0.1);
                        }
                    }
                }



            }
            if (Player.HasBuff(BuffType<AstarteDriver>()) && starfarerOutfit == 3)
            {
                Projectile.NewProjectile(null, target.Center.X, target.Center.Y, 0f, 0f, ProjectileType<StarfarerFollowUp>(), damage / 3, knockback, Player.whoAmI);

            }
            if (!target.active && luciferium)
            {
                Player.AddBuff(BuffType<SatedAnguish>(), 900);
            }
            if (!target.active && butchersdozen == 2)
            {
                butchersDozenKills++;
            }
            if (crit && flashfreeze == 2 && flashFreezeCooldown < 0)
            {
                //damage += target.lifeMax;
                SoundEngine.PlaySound(StarsAboveAudio.SFX_electroSmack, Player.Center);

                Projectile.NewProjectile(null, target.Center.X, target.Center.Y, 0, 0, Mod.Find<ModProjectile>("FlashFreezeExplosion").Type, damage / 4, 0, Player.whoAmI, 0f);
                flashFreezeCooldown = 240;


            }
            if (!disablePromptsCombat && !projectile.hostile)
            {


                if (!target.active)
                {
                    if (Main.rand.Next(0, 4) == 0)
                    {
                        if (Player.statLife <= Player.statLifeMax2 / 5)
                        {
                            starfarerPromptActive("onKillEnemyDanger");
                        }
                        else
                        {
                            if (target.lifeMax <= 5 && target.damage <= 1)
                            {
                                starfarerPromptActive("onKillCritter");
                            }
                            else
                            {
                                starfarerPromptActive("onKillEnemy");
                            }



                        }

                    }

                }
                if (!target.active && target.lifeMax >= 15000)
                {

                    starfarerPromptActive("onKillBossEnemy");


                }
                if (!target.active && target.lifeMax == 5 && target.CanBeChasedBy())
                {

                    


                }
            }
            if (damage >= target.life / 3)
            {
                if (Main.rand.Next(0, 4) == 0)
                {
                    //starfarerPromptActive("onBigDamage");
                }

            }
            if (crit && !disablePromptsCombat)
            {
                if (Main.rand.Next(0, 8) == 0)
                {
                    starfarerPromptActive("onCrit");
                }

            }

            if (Player.HasBuff(BuffType<Buffs.SovereignDominion>()) && target.HasBuff(BuffType<Buffs.Ruination>()))
            {
                if (crit)
                {
                    Player.statLife += damage / 10;
                }
            }
            if (projectile.type != Mod.Find<ModProjectile>("EuthymiaFollowUp").Type && euthymiaActive && euthymiaCooldown <= 0)
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_electroSmack, Player.Center);

                Projectile.NewProjectile(null, target.Center.X, target.Center.Y, 0, 0, Mod.Find<ModProjectile>("EuthymiaFollowUp").Type, Math.Min(damage / 5, 500), 0, Player.whoAmI, 0f);
                euthymiaCooldown = 120 - (eternityGauge / 10);

            }
            if (projectile.type == Mod.Find<ModProjectile>("kiwamiryukenstun").Type)
            {
                Player.AddBuff(BuffType<Buffs.KiwamiRyukenConfirm>(), 60);
                for (int d = 0; d < 4; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 113, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default(Color), 1.5f);
                }
            }
            if (Glitterglue)
            {
                if (Main.rand.Next(0, 100) > 95)
                {
                    target.AddBuff(BuffType<Glitterglued>(), 240);
                }
            }
            if (target.HasBuff(BuffType<Glitterglued>()))
            {
                if (!crit)
                {
                    if (Main.rand.Next(0, 100) > 70)
                    {
                        crit = true;
                    }
                }
            }

            if (projectile.type == Mod.Find<ModProjectile>("HuckleberryRound").Type && (!target.active))
            {
                Player.statMana += 12;
                Player.AddBuff(BuffID.Wrath, 100);
                for (int d = 0; d < 4; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 113, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default(Color), 1.5f);
                }
            }
            if (projectile.type == Mod.Find<ModProjectile>("BuryTheLightSlash").Type)
            {
                if (Player.statLife < Player.statLifeMax2 - 10)
                {
                    Player.statLife += 1;
                }
                if (Player.statMana < Player.statManaMax2 - 5)
                {
                    Player.statMana += 5;
                }
                judgementGauge += 3;
                if (crit)
                {
                    judgementGauge += 7;
                }
                for (int d = 0; d < 4; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 113, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default(Color), 1.5f);
                }
            }
            if (projectile.type == Mod.Find<ModProjectile>("yunlaiSwing").Type && (!target.active))
            {
                Player.statMana += 80;

                for (int d = 0; d < 4; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 113, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default(Color), 1.5f);
                }
            }
            if (projectile.type == Mod.Find<ModProjectile>("SkyStrikerMeleeClaw").Type)
            {
                if (target.life < target.lifeMax / 2)
                {
                    Player.AddBuff(BuffID.Wrath, 120);
                }

                if (crit)
                {

                    Vector2 direction = Vector2.Normalize(target.position - Player.Center);
                    Vector2 velocity = direction * 35f;
                    Vector2 targetPosition = Player.Center;


                    Projectile.NewProjectile(null, targetPosition.X, targetPosition.Y, velocity.X, velocity.Y, ProjectileType<SkyStrikerClaw>(), damage, 2f, Player.whoAmI, 0, Main.rand.Next(-200, 200) * 0.001f * Player.gravDir);

                }


            }
            if (projectile.type == Mod.Find<ModProjectile>("ignitionAstraSwing").Type)
            {
                if (!target.active)
                {
                    for (int d = 0; d < 4; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 113, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default(Color), 1.5f);
                    }
                }
                Dust.NewDust(target.position, target.width, target.height, 21, 0f, 0f, 150, default(Color), 1.5f);
                target.AddBuff(BuffType<Buffs.Starblight>(), 91020);

            }
            if (projectile.type == Mod.Find<ModProjectile>("tartagliaSwing").Type)
            {
                SoundEngine.PlaySound(SoundID.Splash, target.position);
                Dust.NewDust(target.position, target.width, target.height, 15, 0f, 0f, 150, default(Color), 1.5f);



            }

            if (projectile.type == Mod.Find<ModProjectile>("NaganadelProjectileFinal1").Type)
            {
                Player.statMana += 20;

                for (int d = 0; d < 8; d++)
                {


                    if (Main.rand.NextBool(3))
                    {
                        Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 204,
                            projectile.velocity.X * .2f + Main.rand.Next(-5, 5), projectile.velocity.Y * .2f + Main.rand.Next(-5, 5), 200, Scale: 1.2f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(96, Main.LocalPlayer);

                        dust.velocity += projectile.velocity * 0.3f;
                        dust.velocity *= 0.2f;
                    }
                    if (Main.rand.NextBool(4))
                    {
                        Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 204,
                            0, 0, 254, Scale: 0.3f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(96, Main.LocalPlayer);
                        dust.velocity += projectile.velocity * 0.5f;
                        dust.velocity *= 0.5f;
                    }


                }

            }
            if (projectile.type == Mod.Find<ModProjectile>("NaganadelProjectileFinal2").Type)
            {


                for (int d = 0; d < 8; d++)
                {


                    if (Main.rand.NextBool(3))
                    {
                        Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 204,
                            projectile.velocity.X * .2f + Main.rand.Next(-30, 30), projectile.velocity.Y * .2f + Main.rand.Next(-30, 30), 200, Scale: 1.2f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(66, Main.LocalPlayer);

                        dust.velocity += projectile.velocity * 0.3f;
                        dust.velocity *= 0.2f;
                    }
                    if (Main.rand.NextBool(4))
                    {
                        Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 204,
                            0, 0, 254, Scale: 0.3f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(66, Main.LocalPlayer);
                        dust.velocity += projectile.velocity * 0.5f;
                        dust.velocity *= 0.5f;
                    }
                }
            }
            if (projectile.type == Mod.Find<ModProjectile>("NaganadelProjectileFinal3").Type)
            {
                for (int d = 0; d < 15; d++)
                {

                    Dust.NewDust(target.position, target.width, target.height, 6, 0f + Main.rand.Next(-30, 30), 0f + Main.rand.Next(-30, 30), 150, default(Color), 1.5f);


                }
                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 90, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);

                }

                for (int d = 0; d < 8; d++)
                {


                    if (Main.rand.NextBool(3))
                    {
                        Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 204,
                            projectile.velocity.X * .2f + Main.rand.Next(-30, 30), projectile.velocity.Y * .2f + Main.rand.Next(-30, 30), 200, Scale: 1.2f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(122, Main.LocalPlayer);

                        dust.velocity += projectile.velocity * 0.3f;
                        dust.velocity *= 0.2f;
                    }
                    if (Main.rand.NextBool(4))
                    {
                        Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 204,
                            0, 0, 254, Scale: 0.3f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(122, Main.LocalPlayer);
                        dust.velocity += projectile.velocity * 0.5f;
                        dust.velocity *= 0.5f;
                    }
                }
            }
            if (projectile.type == Mod.Find<ModProjectile>("NaganadelProjectileFinal4").Type)
            {

                for (int i = 0; i < 2 + Main.rand.Next(1, 3); i++)
                {
                    Projectile.NewProjectile(null, projectile.position.X, projectile.position.Y - 800, 0 + Main.rand.Next(-10, 10), 0 + Main.rand.Next(1, 40), ProjectileID.StarWrath, damage / 2, 0, Player.whoAmI, 0f);

                }
                for (int d = 0; d < 8; d++)
                {


                    if (Main.rand.NextBool(3))
                    {
                        Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 204,
                            projectile.velocity.X * .2f + Main.rand.Next(-30, 30), projectile.velocity.Y * .2f + Main.rand.Next(-30, 30), 200, Scale: 1.2f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);

                        dust.velocity += projectile.velocity * 0.3f;
                        dust.velocity *= 0.2f;
                    }
                    if (Main.rand.NextBool(4))
                    {
                        Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 204,
                            0, 0, 254, Scale: 0.3f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                        dust.velocity += projectile.velocity * 0.5f;
                        dust.velocity *= 0.5f;
                    }
                }
            }
            if (projectile.type == Mod.Find<ModProjectile>("NaganadelProjectileFinal5").Type)
            {
                Player.statMana += 10;
                Player.AddBuff(BuffID.Swiftness, 120);

                Projectile.NewProjectile(null, projectile.position.X, projectile.position.Y - 800, 0 + Main.rand.Next(-10, 10), 0 + Main.rand.Next(1, 40), ProjectileID.LunarFlare, damage / 2, 0, Player.whoAmI, 0f);


                for (int d = 0; d < 8; d++)
                {


                    if (Main.rand.NextBool(3))
                    {
                        Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 204,
                            projectile.velocity.X * .2f + Main.rand.Next(-30, 30), projectile.velocity.Y * .2f + Main.rand.Next(-30, 30), 200, Scale: 1.2f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(120, Main.LocalPlayer);

                        dust.velocity += projectile.velocity * 0.3f;
                        dust.velocity *= 0.2f;
                    }
                    if (Main.rand.NextBool(4))
                    {
                        Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 204,
                            0, 0, 254, Scale: 0.3f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(120, Main.LocalPlayer);
                        dust.velocity += projectile.velocity * 0.5f;
                        dust.velocity *= 0.5f;
                    }
                }
            }
            if (projectile.type == Mod.Find<ModProjectile>("ButterflyProjectile").Type)
            {


                for (int d = 0; d < 8; d++)
                {


                    if (Main.rand.NextBool(3))
                    {
                        Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 204,
                            projectile.velocity.X * .2f + Main.rand.Next(-30, 30), projectile.velocity.Y * .2f + Main.rand.Next(-30, 30), 200, Scale: 1.2f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);

                        dust.velocity += projectile.velocity * 0.3f;
                        dust.velocity *= 0.2f;
                    }
                    if (Main.rand.NextBool(4))
                    {
                        Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 204,
                            0, 0, 254, Scale: 0.3f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                        dust.velocity += projectile.velocity * 0.5f;
                        dust.velocity *= 0.5f;
                    }
                }
            }
            if (projectile.type == Mod.Find<ModProjectile>("ClaimhBurst").Type)
            {

                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 21, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 91, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 197, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 159, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);

                }
                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, DustType<Dusts.Star>(), 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
                }





            }
            if (projectile.type == Mod.Find<ModProjectile>("StygianSwing1").Type)
            {
                for (int d = 0; d < 7; d++)
                {
                    Dust dust = Main.dust[Terraria.Dust.NewDust(target.position, target.width, target.height, 159, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(103, Main.LocalPlayer);


                }
            }
            if (projectile.type == Mod.Find<ModProjectile>("StygianSwing3").Type)
            {
                if (Main.LocalPlayer.HasBuff(BuffType<Buffs.ClawsOfNyx>()))
                {
                    for (int d = 0; d < 4; d++)
                    {
                        Dust dust = Main.dust[Terraria.Dust.NewDust(target.position, target.width, target.height, 219, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f)];
                        //dust.shader = GameShaders.Armor.GetSecondaryShader(103, Main.LocalPlayer);

                    }
                }
                else
                {
                    for (int d = 0; d < 7; d++)
                    {
                        Dust dust = Main.dust[Terraria.Dust.NewDust(target.position, target.width, target.height, 159, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f)];
                        dust.shader = GameShaders.Armor.GetSecondaryShader(103, Main.LocalPlayer);


                    }
                }
            }
            if (projectile.type == Mod.Find<ModProjectile>("IzanagiRound").Type)
            {
                for (int d = 0; d < 5; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 159, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);

                }
                for (int d = 0; d < 5; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 91, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                }
                if (edgeHoned)
                {
                    for (int d = 0; d < 30; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 21, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 30; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 91, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 30; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 197, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 30; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 159, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);

                    }
                    for (int d = 0; d < 30; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 220, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                    }


                    Player.GetModPlayer<StarsAbovePlayer>().edgeHoned = false;
                }
                else
                {


                }

            }
            if (projectile.type == Mod.Find<ModProjectile>("HullwroughtRound").Type)
            {

                screenShakeTimerGlobal = -80;
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 0, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
                }

                for (int d = 0; d < 26; d++)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 133, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 7, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 40; d++)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 269, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default(Color), 1.5f);
                }
                for (int d = 0; d < 50; d++)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 78, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
                }

                // Play explosion sound
                SoundEngine.PlaySound(SoundID.Item89, projectile.position);
                // Smoke Dust spawn
                for (int i = 0; i < 70; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }
                // Fire Dust spawn
                for (int i = 0; i < 80; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 3f);
                    Main.dust[dustIndex].noGravity = true;
                    Main.dust[dustIndex].velocity *= 5f;
                    dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
                    Main.dust[dustIndex].velocity *= 3f;
                }
                // Large Smoke Gore spawn
                for (int g = 0; g < 4; g++)
                {
                    int goreIndex = Gore.NewGore(null, new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                }

            }
            if (projectile.type == Mod.Find<ModProjectile>("DrachenlanceProjectile").Type)
            {
                if (Main.LocalPlayer.HasBuff(BuffType<Buffs.BloodOfTheDragon>()))
                {
                    //Main.LocalPlayer.velocity = Main.LocalPlayer.velocity * -1;
                    for (int d = 0; d < 15; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 21, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                    }


                }
                if (Main.LocalPlayer.HasBuff(BuffType<Buffs.LifeOfTheDragon>()))
                {
                    //Main.LocalPlayer.velocity = Main.LocalPlayer.velocity * -1;
                    //Main.LocalPlayer.statLife += 50;
                    for (int d = 0; d < 15; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 258, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                    }

                }
            }
            if (projectile.type == Mod.Find<ModProjectile>("CarianSwingE1").Type || projectile.type == Mod.Find<ModProjectile>("CarianSwingE2").Type)
            {

                for (int d = 0; d < 6; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 21, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.9f);
                }



            }
            if (!target.active)
            {
                OnKillEnemy(target);
            }

        }
        /*bool voidActive = IsVoidActive;
             Player.ManageSpecialBiomeVisuals("StarsAbove:Void", voidActive, Player.Center);*/

        public override void OnRespawn(Player player)
        {


            
            if (luciferium)
            {
                player.AddBuff(BuffID.PotionSickness, 3600);
            }
            base.OnRespawn(player);

        }
        public override void SetStaticDefaults()
        {


        }
        public override void PreUpdate()
        {
            if(!BossEnemySpawnModDisabled)
            {
                for (int i = 0; i <= Main.maxNPCs; i++)
                {
                    if (Main.npc[i].boss && Main.npc[i].active)
                    {
                        Player.AddBuff(BuffType<BossEnemySpawnMod>(), 10);
                    }

                }
            }
            
            if(VNDialogueActive || starfarerDialogue)
            {
                Player.AddBuff(BuffType<Conversationalist>(), 10);
            }
           

            if (CatalystMemoryProgress < 0)
            {
                CatalystMemoryProgress = 0;
            }
            CatalystMemoryProgress--;
            
            GlobalRotation++;
            if (GlobalRotation >= 360)
            {
                GlobalRotation = 0;
            }
            timeAfterGettingHit++;
            if (timeAfterGettingHit >= 720 && inneralchemy == 2)
            {
                Player.AddBuff(BuffType<InnerAlchemy>(), 10);
            }


            activeMinions = (int)(Player.slotsMinions);

            if (Player.HeldItem.ModItem?.Mod == ModLoader.GetMod("StarsAbove"))//Drill Mount Bug
            {
                Player.buffImmune[BuffID.DrillMount] = true;
                Player.ClearBuff(BuffID.DrillMount);



            }
            if (chosenStarfarer != 0)
            {
                if (MeleeAspect == 0)
                {
                    MeleeAspect = 1;
                }
                if (MagicAspect == 0)
                {
                    MagicAspect = 1;
                }
                if (RangedAspect == 0)
                {
                    RangedAspect = 1;
                }
                if (SummonAspect == 0)
                {
                    SummonAspect = 1;
                }
                if (RogueAspect == 0)
                {
                    RogueAspect = 1;
                }
            }
            if (AspectLocked != 0)
            {
                if (AspectLocked == 1)
                {
                    MeleeAspect = 2;
                    MagicAspect = 1;
                    RangedAspect = 1;
                    SummonAspect = 1;
                }
                if (AspectLocked == 2)
                {
                    MeleeAspect = 1;
                    MagicAspect = 2;
                    RangedAspect = 1;
                    SummonAspect = 1;
                }
                if (AspectLocked == 3)
                {
                    MeleeAspect = 1;
                    MagicAspect = 1;
                    RangedAspect = 2;
                    SummonAspect = 1;
                }
                if (AspectLocked == 4)
                {
                    MeleeAspect = 1;
                    MagicAspect = 1;
                    RangedAspect = 1;
                    SummonAspect = 2;
                }
                if (AspectLocked == 5)
                {
                    if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
                    {

                        MeleeAspect = 1;
                        MagicAspect = 1;
                        RangedAspect = 1;
                        SummonAspect = 1;
                        RogueAspect = 2;

                    }
                    else
                    {
                        MeleeAspect = 1;
                        MagicAspect = 1;
                        RangedAspect = 1;
                        SummonAspect = 1;
                        RogueAspect = 1;
                        AspectLocked = 0;
                    }

                }


            }
            if (Player.HasBuff(BuffType<OzmaAttack>()))
            {
                OzmaSpikeVFXProgression += 3;

            }
            else
            {

                OzmaSpikeVFXProgression -= 3;
            }
            if (OzmaSpikeVFXProgression < 0)
            {
                OzmaSpikeVFXProgression = 0;
            }
            if (OzmaSpikeVFXProgression > 100)
            {
                OzmaSpikeVFXProgression = 100;
            }
            if (blackManaDrain > 0)
            {
                blackMana--;
                blackManaDrain--;
            }
            if (whiteManaDrain > 0)
            {
                whiteMana--;
                whiteManaDrain--;
            }


            if (!Player.HasBuff(BuffType<WhiteEnchantment>()) && !Player.HasBuff(BuffType<BlackEnchantment>()))
            {
                manaStack = 0;
            }
            if (Player.HasBuff(BuffType<MagitonOverheat>()) && CosmicDestroyerRounds <= 0)
            {
                for (int i = 0; i < Player.CountBuffs(); i++)
                    if (Player.buffType[i] == BuffType<MagitonOverheat>())
                    {
                        Player.DelBuff(i);


                    }

                Player.AddBuff(BuffType<Overheated>(), 60);

            }
            if (Player.HasBuff(BuffType<ArtificeSirenBuff>()))
            {
                sirenTarget = Vector2.Lerp(sirenTarget, sirenEnemy, 0.05f);

            }
            if (Player.HasBuff(BuffType<TakodachiLaserBuff>()))
            {
                takoTarget = Vector2.Lerp(takoTarget, playerMousePos, 0.05f);

            }
            if (Player.HasBuff(BuffType<TwinStarsBuff>()))
            {
                starTarget = Vector2.Lerp(starTarget, playerMousePos, 0.05f);
                starTarget2 = Vector2.Lerp(starTarget2, starTarget, 0.05f);

            }
            if (CallOfTheVoid > 0 && AshenAmbitionHeld)
            {
                if (CallOfTheVoid == 1)
                {
                    Player.AddBuff(BuffType<CallOfTheVoid1>(), 2);
                }
                if (CallOfTheVoid == 2)
                {
                    Player.AddBuff(BuffType<CallOfTheVoid2>(), 2);

                }
                if (CallOfTheVoid == 3)
                {
                    Player.AddBuff(BuffType<CallOfTheVoid3>(), 2);

                }
                if (CallOfTheVoid == 4)
                {
                    Player.AddBuff(BuffType<CallOfTheVoid4>(), 2);

                }
                if (CallOfTheVoid >= 5)
                {
                    CallOfTheVoid = 0;
                }
            }
            if (SpectralArsenal > 0 && VermillionDaemonHeld)
            {
                if (SpectralArsenal == 1)
                {
                    Player.AddBuff(BuffType<SpectralArsenal1>(), 2);
                }
                if (SpectralArsenal == 2)
                {
                    Player.AddBuff(BuffType<SpectralArsenal2>(), 2);

                }
                if (SpectralArsenal >= 3)
                {
                    Player.AddBuff(BuffType<SpectralArsenal3>(), 2);

                }

            }
            if (!VermillionDaemonHeld)
            {
                SpectralArsenal = 0;
            }
            if (SakuraVengeanceHeld)
            {
                VengeanceElementTimer++;
            }
            if (VengeanceElementTimer <= 1800 && VengeanceElementTimer >= 0)//
            {

                VengeanceElement = 0;
            }
            if (VengeanceElementTimer <= 3600 && VengeanceElementTimer > 1800)//
            {

                VengeanceElement = 1;
            }
            if (VengeanceElementTimer <= 5400 && VengeanceElementTimer > 3600)//
            {

                VengeanceElement = 2;
            }
            if (VengeanceElementTimer <= 7200 && VengeanceElementTimer > 5400)//
            {

                VengeanceElement = 3;
            }
            if (VengeanceElementTimer > 7200)//
            {

                VengeanceElementTimer = 0;
            }
            if (!SkyStrikerHeld)
            {
                //SkyStrikerForm = 0;
                SkyStrikerMenuVisible = false;
            }
            if (SkyStrikerTransformPrep)
            {
                SkyStrikerTransformPrep = false;
                for (int i = 0; i < 10; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(Player.Center.X, Player.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default(Color), 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }
                // Fire Dust spawn

                // Large Smoke Gore spawn
                for (int g = 0; g < 2; g++)
                {
                    int goreIndex = Gore.NewGore(null, new Vector2(Player.position.X + (float)(Player.width / 2) - 24f, Player.position.Y + (float)(Player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(Player.position.X + (float)(Player.width / 2) - 24f, Player.position.Y + (float)(Player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(Player.position.X + (float)(Player.width / 2) - 24f, Player.position.Y + (float)(Player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(Player.position.X + (float)(Player.width / 2) - 24f, Player.position.Y + (float)(Player.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                }
            }
            if (duality > 100)
            {
                duality = 100;
            }
            if (duality < 0)
            {
                duality = 0;
            }
            if (eternityGauge < 500)
            {
                eternityGauge++;
            }
            flashFreezeCooldown--;
            
            seenUnknownBossTimer--;
            starfarerMenuDialogueScrollTimer++;
            if (starfarerMenuDialogueScrollTimer >= dialogueScrollTimerMax && starfarerMenuActive)
            {
                if (instantText)
                {
                    starfarerMenuDialogueScrollNumber = starfarerMenuDialogue.Length;


                }

                if (starfarerMenuDialogueScrollNumber < starfarerMenuDialogue.Length)
                {

                    starfarerMenuDialogueScrollNumber++;
                    if (dialogueAudio != 5)
                    {

                        if (dialogueAudio == 1)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_textsoundeffect, Player.Center);
                            //Main.PlaySound(SoundID.MenuTick, player.position);
                        }
                        if (dialogueAudio == 2)
                        {
                            SoundEngine.PlaySound(SoundID.MenuTick, Player.position);
                        }
                        if (dialogueAudio == 3)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_textsoundeffect2, Player.Center);
                        }
                        if (dialogueAudio == 4)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_textsoundeffect3, Player.Center);
                        }

                    }
                }
                starfarerMenuDialogueScrollTimer = 0;
            }
            novaDialogueScrollTimer++;
            if (novaDialogueScrollTimer >= dialogueScrollTimerMax && novaUIActive)
            {
                if (instantText)
                {
                    novaDialogueScrollNumber = description.Length;


                }

                if (novaDialogueScrollNumber < description.Length)
                {

                    novaDialogueScrollNumber++;
                    if (dialogueAudio != 5)
                    {

                        if (dialogueAudio == 1)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_textsoundeffect, Player.Center);
                            //Main.PlaySound(SoundID.MenuTick, player.position);
                        }
                        if (dialogueAudio == 2)
                        {
                            SoundEngine.PlaySound(SoundID.MenuTick, Player.position);
                        }
                        if (dialogueAudio == 3)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_textsoundeffect2, Player.Center);
                        }
                        if (dialogueAudio == 4)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_textsoundeffect3, Player.Center);
                        }

                    }
                }
                novaDialogueScrollTimer = 0;
            }
            dialogueScrollTimer++;

            if (dialogueScrollTimer >= dialogueScrollTimerMax)
            {
                if (instantText)
                {

                    dialogueScrollNumber = dialogue.Length;


                }

                if (dialogueScrollNumber < dialogue.Length)
                {

                    dialogueScrollNumber++;
                    if (dialogueAudio != 5)
                    {

                        if (dialogueAudio == 1)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_textsoundeffect, Player.Center);
                            //Main.PlaySound(SoundID.MenuTick, player.position);
                        }
                        if (dialogueAudio == 2)
                        {
                            SoundEngine.PlaySound(SoundID.MenuTick, Player.position);
                        }
                        if (dialogueAudio == 3)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_textsoundeffect2, Player.Center);
                        }
                        if (dialogueAudio == 4)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_textsoundeffect3, Player.Center);
                        }

                    }
                }
                dialogueScrollTimer = 0;
            }
           
            promptDialogueScrollTimer++;
            if (promptDialogueScrollTimer >= dialogueScrollTimerMax && promptIsActive)
            {
                if (instantText)
                {

                    promptDialogueScrollNumber = promptDialogue.Length;

                }
                if (promptDialogueScrollNumber < promptDialogue.Length)
                {

                    promptDialogueScrollNumber++;
                    if (dialogueAudio != 5)
                    {

                        if (dialogueAudio == 1)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_textsoundeffect, Player.Center);
                            //Main.PlaySound(SoundID.MenuTick, player.position);
                        }
                        if (dialogueAudio == 2)
                        {
                            SoundEngine.PlaySound(SoundID.MenuTick, Player.position);
                        }
                        if (dialogueAudio == 3)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_textsoundeffect2, Player.Center);
                        }
                        if (dialogueAudio == 4)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_textsoundeffect3, Player.Center);
                        }
                    }
                }
                promptDialogueScrollTimer = 0;
            }
            if (promptIsActive)
            {

                animatedPromptDialogue = promptDialogue.Substring(0, promptDialogueScrollNumber);//Prompt dialogue increment magic
            }
            if (novaUIActive && chosenStarfarer != 0)
            {

                animatedDescription = description.Substring(0, novaDialogueScrollNumber);//Prompt dialogue increment magic
                animatedDescription = Wrap(animatedDescription, 55);
            }
            if (starfarerMenuActive && chosenStarfarer != 0)
            {
                novaGauge = 0;
                animatedStarfarerMenuDialogue = starfarerMenuDialogue.Substring(0, starfarerMenuDialogueScrollNumber);//Prompt dialogue increment magic
                animatedStarfarerMenuDialogue = Wrap(animatedStarfarerMenuDialogue, 46);
            }
            ammoRecycleCooldown--;
            aprismatismCooldown--;
            betweenTheBoundaryTimer--;
            if (betweenTheBoundaryTimer <= 0 && beyondtheboundary == 2 && !stellarArray)
            {
                if (betweenTheBoundaryEbb)
                {
                    Player.AddBuff(BuffType<Buffs.Ebb>(), 480);
                    betweenTheBoundaryEbb = false;
                }
                else
                {
                    Player.AddBuff(BuffType<Buffs.Flow>(), 480);
                    betweenTheBoundaryEbb = true;
                }


                betweenTheBoundaryTimer = 480;

            }
            blinkTimer++;
            vagrantTimeLeft--;
            if (blinkTimer >= 600)
            {
                blinkTimer = 0;
            }
            if (vagrantTimeLeft <= 0)
            {
                vagrantTimeLeft = 0;
            }
            if (inCombat < 0)
            {
                butchersDozenKills = 0;
                SoulReaverSouls = 0;
            }
            if (butchersDozenKills >= 12 && !Player.dead && Player.active)
            {
                if (!Player.HasBuff(BuffType<Buffs.ButchersDozen>()))
                {
                    /*
                    if (chosenStarfarer == 1)
                    {
                        if (Player.ownedProjectileCounts[Mod.Find<ModProjectile>("AsphodeneBurst").Type] < 1)
                            Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("AsphodeneBurst").Type, 0, 0, Player.whoAmI, 0, 1);

                    }
                    if (chosenStarfarer == 2)
                    {
                        if (Player.ownedProjectileCounts[Mod.Find<ModProjectile>("EridaniBurst").Type] < 1)
                            Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("EridaniBurst").Type, 0, 0, Player.whoAmI, 0, 1);

                    }*/
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onButchersDozen");
                }
                Player.AddBuff(BuffType<Buffs.ButchersDozen>(), 2);
            }
            umbralEntropyCooldown--;
            euthymiaCooldown--;
            if (promptIsActive)
            {
                if (promptDialogueScrollNumber >= promptDialogue.Length)
                {
                    starfarerPromptActiveTimer--;
                }
            }
            starfarerPromptCooldown--;
            kroniicTimer--;
            if (kroniicTimer < 0)
            {
                if (powderGaugeIndicatorOn)
                {
                    Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                    CombatText.NewText(textPos, new Color(255, 255, 125, 240), "The Timeframe vanishes...", false, false);
                    powderGaugeIndicatorOn = false;
                    Player.GetModPlayer<StarsAbovePlayer>().kroniicTeleport = false;
                    Player.AddBuff(BuffType<Buffs.KroniicPrincipalityCooldown>(), 3600);//7200 is 2 minutes
                }

            }
            if (eternityGauge > 1000)
            {
                eternityGauge = 1000;
            }
            if (eternityGauge < 0)
            {
                eternityGauge = 0;
            }
            if (powderGauge > 100)
            {
                powderGauge = 100;
            }
            if (powderGauge < 0)
            {
                powderGauge = 0;
            }
            if (!euthymiaActive)
            {
                eternityGauge = 0;
            }
            if (kroniicTeleport)
            {
                for (int i = 0; i < 30; i++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 44);
                    offset.Y += (float)(Math.Cos(angle) * 44);

                    Dust d = Dust.NewDustPerfect(kroniicSavedPosition + offset, 20, Vector2.Zero, 200, default(Color), 0.7f);
                    d.fadeIn = 0.1f;
                    d.noGravity = true;
                }
            }
            //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"{starfarerPromptCooldown}"), 190, 100, 247);}
            if (VagrantActive)
            {
                inVagrantFightTimer = 30;
            }
            if (starfarerPromptActiveTimer < 0 && promptIsActive)
            {
                promptDialogue = "";
                promptIsActive = false;
                promptDialogueScrollTimer = 0;
                promptDialogueScrollNumber = 0;
                starfarerPromptCooldown = (starfarerPromptCooldownMax * 60) * 60;
                //starfarerPromptCooldown = 200;
            }
            if (cosmicPhoenixPrism)
            {
                if (Player.statMana < 40)
                {
                    Player.wingTime = 0;
                }
            }
            if (phantomTeleport)
            {
                Player.statMana--;
                Player.manaRegenDelay = 220;
            }
            if (!Main.dedServ)
            {
                if (NalhaunActive)
                {
                    lifeForceTimer++;

                }
                if (!Player.active)
                {
                    lifeforce = 100;
                }
                if (lifeForceTimer >= 10 && inNalhaunFightTimer > 0)
                {
                    if (!Player.active)
                    {
                        lifeforce++;
                        lifeForceTimer = 0;
                    }
                    else
                    {
                        lifeforce--;
                        lifeForceTimer = 0;
                    }

                }
                if (lifeforce > 100)
                {
                    lifeforce = 100;
                }

                novaDamageMod = 0;
                novaCritChanceMod = 0;
                novaCritDamageMod = 0;
                novaChargeMod = 0;

                //
                // 
                #region archive
                /////////////////////////////////////////////////////ARCHIVE//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///
                if (!archivePopulated)
                {
                    IdleArchiveList.Add(new IdleArchiveListing(
                          "", //Name of the archive listing.
                          $"", //Description of the listing.
                          false, //Unlock requirements.
                          0,
                          "")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                   LangHelper.GetTextValue($"Archive.DefaultIdleDialogue.Name", Player.name), //Name of the archive listing.
                   LangHelper.GetTextValue($"Archive.DefaultIdleDialogue.Description", Player.name), //Description of the listing.
                   true, //Unlock requirements.
                   2,
                   "")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle Conversation 1", //Name of the archive listing.
                           "Pre Hardmode idle dialogue.", //Description of the listing.
                           true, //Unlock requirements.
                           3,
                           "")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle Conversation 2", //Name of the archive listing.
                           "Pre Hardmode idle dialogue.", //Description of the listing.
                           true, //Unlock requirements.
                           4,
                           "")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle Conversation 3", //Name of the archive listing.
                           "Pre Hardmode idle dialogue.", //Description of the listing.
                           true, //Unlock requirements.
                           5,
                           "")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle Conversation 4", //Name of the archive listing.
                           "Pre Hardmode idle dialogue.", //Description of the listing.
                           true, //Unlock requirements.
                           6,
                           "")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle Conversation 5", //Name of the archive listing.
                           "Pre Hardmode idle dialogue.", //Description of the listing.
                           true, //Unlock requirements.
                           7,
                           "")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle Conversation 6", //Name of the archive listing.
                           "Pre Hardmode idle dialogue.", //Description of the listing.
                           true, //Unlock requirements.
                           8,
                           "")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle Conversation 7", //Name of the archive listing.
                           "Pre Hardmode idle dialogue.", //Description of the listing.
                           true, //Unlock requirements.
                           9,
                           "")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle Conversation 8", //Name of the archive listing.
                           "Pre Hardmode idle dialogue.", //Description of the listing.
                           true, //Unlock requirements.
                           10,
                           "")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle Conversation 9", //Name of the archive listing.
                           "Pre Hardmode idle dialogue.", //Description of the listing.
                           true, //Unlock requirements.
                           11,
                           "")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle Conversation 10", //Name of the archive listing.
                           "Hardmode idle dialogue.", //Description of the listing.
                           Main.hardMode, //Unlock requirements.
                           12,
                           "Enter Hardmode")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle Conversation 11", //Name of the archive listing.
                           "Hardmode idle dialogue.", //Description of the listing.
                           Main.hardMode, //Unlock requirements.
                           13,
                           "Enter Hardmode")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle Conversation 12", //Name of the archive listing.
                           "Hardmode idle dialogue.", //Description of the listing.
                           Main.hardMode, //Unlock requirements.
                           14,
                           "Enter Hardmode")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle Conversation 13", //Name of the archive listing.
                           "Hardmode idle dialogue.", //Description of the listing.
                           Main.hardMode, //Unlock requirements.
                           15,
                           "Enter Hardmode")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle Conversation 14", //Name of the archive listing.
                           "Hardmode idle dialogue.", //Description of the listing.
                           Main.hardMode, //Unlock requirements.
                           16,
                           "Enter Hardmode")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle Conversation 15", //Name of the archive listing.
                           "Hardmode idle dialogue.", //Description of the listing.
                           Main.hardMode, //Unlock requirements.
                           17,
                           "Enter Hardmode")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle Conversation 16", //Name of the archive listing.
                           "Hardmode idle dialogue.", //Description of the listing.
                           Main.hardMode, //Unlock requirements.
                           18,
                           "Enter Hardmode")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle Conversation 17", //Name of the archive listing.
                           "Hardmode idle dialogue.", //Description of the listing.
                           Main.hardMode, //Unlock requirements.
                           19,
                           "Enter Hardmode")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle Conversation 18", //Name of the archive listing.
                           "Hardmode idle dialogue.", //Description of the listing.
                           Main.hardMode, //Unlock requirements.
                           20,
                           "Enter Hardmode")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "'A World Shrouded in Light'", //Name of the archive listing.
                           "Idle dialogue during Light Everlasting.", //Description of the listing.
                           NPC.downedMoonlord, //Unlock requirements.
                           21,
                           "Unlocked after witnessing Light Everlasting for the first time.")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Entering the Observatory", //Name of the archive listing.
                           "Dialogue on the Observatory Hyperborea.", //Description of the listing.
                           seenObservatory, //Unlock requirements.
                           22,
                           "Unlocked after entering the Observatory Hyperborea for the first time.")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Explaining Cosmic Voyages", //Name of the archive listing.
                           "An explanation of the mechanics of Cosmic Voyages.", //Description of the listing.
                           seenObservatory, //Unlock requirements.
                           24,
                           "Unlocked after entering the Observatory Hyperborea for the first time.")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle in the Observatory", //Name of the archive listing.
                           "Neutral dialogue within the Observatory Hyperborea.", //Description of the listing.
                           seenObservatory, //Unlock requirements.
                           23,
                           "Unlocked after entering the Observatory Hyperborea for the first time.")); //Corresponding dialogue ID.
                    IdleArchiveList.Add(new IdleArchiveListing(
                           "Idle in Space", //Name of the archive listing.
                           "Neutral dialogue when on a normal-type Cosmic Voyage. Unused.", //Description of the listing.
                           seenObservatory, //Unlock requirements.
                           23,
                           "Unlocked after entering the Observatory Hyperborea for the first time.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                          "", //Name of the archive listing.
                          $"", //Description of the listing.
                          false, //Unlock requirements.
                          0,
                          "")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Slime King Dethroned", //Name of the archive listing.
                           "Unlocked after defeating King Slime.", //Description of the listing.
                           slimeDialogue == 2, //Unlock requirements.
                           51,
                           "Defeat King Slime.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Eye of Cthulhu Pierced", //Name of the archive listing.
                           "Unlocked after defeating Eye of Cthulhu.", //Description of the listing.
                           eyeDialogue == 2, //Unlock requirements.
                           52,
                           "Defeat Eye of Cthulhu.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Worldly Evil Sanctified", //Name of the archive listing.
                           "Unlocked after defeating the Corruption/Crimson boss.", //Description of the listing.
                           corruptBossDialogue == 2, //Unlock requirements.
                           53,
                           "Defeat the world's Corruption/Crimson boss.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Queen Bee Exterminated", //Name of the archive listing.
                           "Unlocked after defeating Queen Bee.", //Description of the listing.
                           BeeBossDialogue == 2, //Unlock requirements.
                           54,
                           "Defeat Queen Bee.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Skeletron Buried", //Name of the archive listing.
                           "Unlocked after defeating Skeletron.", //Description of the listing.
                           SkeletonDialogue == 2, //Unlock requirements.
                           55,
                           "Defeat Skeletron.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Deerclops Extinct", //Name of the archive listing.
                           "Unlocked after defeating Deerclops.", //Description of the listing.
                           DeerclopsDialogue == 2, //Unlock requirements.
                           76,
                           "Defeat Deerclops.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Wall of Flesh Purged", //Name of the archive listing.
                           "Unlocked after defeating the Wall of Flesh.", //Description of the listing.
                           WallOfFleshDialogue == 2, //Unlock requirements.
                           56,
                           "Defeat the Wall of Flesh.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Queen Slime Overthrown", //Name of the archive listing.
                           "Unlocked after defeating Queen Slime", //Description of the listing.
                           QueenSlimeDialogue == 2, //Unlock requirements.
                           74,
                           "Defeat Queen Slime")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "The Twins Scrapped", //Name of the archive listing.
                           "Unlocked after defeating the Twins.", //Description of the listing.
                           TwinsDialogue == 2, //Unlock requirements.
                           57,
                           "Defeat the Twins.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "The Destroyer Deleted", //Name of the archive listing.
                           "Unlocked after defeating the Destroyer.", //Description of the listing.
                           DestroyerDialogue == 2, //Unlock requirements.
                           58,
                           "Defeat the Destroyer.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Skeletron Prime Erased", //Name of the archive listing.
                           "Unlocked after defeating Skeletron Prime.", //Description of the listing.
                           SkeletronPrimeDialogue == 2, //Unlock requirements.
                           59,
                           "Defeat Skeletron Prime.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "All Mechanical Bosses Rended", //Name of the archive listing.
                           "Unlocked after defeating all of the Mechanical Bosses.", //Description of the listing.
                           AllMechsDefeatedDialogue == 2, //Unlock requirements.
                           60,
                           "Defeat all of the Mechanical Bosses.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Plantera Uprooted", //Name of the archive listing.
                           "Unlocked after defeating Plantera.", //Description of the listing.
                           PlanteraDialogue == 2, //Unlock requirements.
                           61,
                           "Defeat Plantera.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Golem Deactivated", //Name of the archive listing.
                           "Unlocked after defeating Golem.", //Description of the listing.
                           GolemDialogue == 2, //Unlock requirements.
                           62,
                           "Defeat Golem.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Duke Fishron Hunted", //Name of the archive listing.
                           "Unlocked after defeating Duke Fishron.", //Description of the listing.
                           DukeFishronDialogue == 2, //Unlock requirements.
                           63,
                           "Defeat Duke Fishron.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Empress of Light Dimmed", //Name of the archive listing.
                           "Unlocked after defeating the Empress of Light.", //Description of the listing.
                           EmpressDialogue == 2, //Unlock requirements.
                           75,
                           "Defeat the Empress of Light.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Lunatic Cultist Crucified", //Name of the archive listing.
                           "Unlocked after defeating the Lunatic Cultist.", //Description of the listing.
                           CultistDialogue == 2, //Unlock requirements.
                           64,
                           "Defeat the Lunatic Cultist.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Moon Lord Defeated", //Name of the archive listing.
                           "Unlocked after defeating the Moon Lord.", //Description of the listing.
                           MoonLordDialogue == 2, //Unlock requirements.
                           65,
                           "Defeat the Moon Lord.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Terraria's Hero", //Name of the archive listing.
                           "Unlocked after defeating all vanilla Terraria bosses. Grants an Essence.", //Description of the listing.
                           AllVanillaBossesDefeatedDialogue == 2, //Unlock requirements.
                           67,
                           "Defeat all vanilla bosses.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Hero of the Realm", //Name of the archive listing.
                           "Unlocked after defeating all vanilla Terraria bosses, and cleansing the world of Light Everlasting. Grants an Essence.", //Description of the listing.
                           EverythingDefeatedDialogue == 2, //Unlock requirements.
                           68,
                           "Defeat all vanilla bosses and the Warrior of Light in Expert Mode."));
                    BossArchiveList.Add(new BossArchiveListing(
                           "Perseus's Appeal: The Burnished King", //Name of the archive listing.
                           "Grants the item to summon the Burnished King.", //Description of the listing.
                           nalhaunBossItemDialogue == 2, //Unlock requirements.
                           301,
                           "???")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Perseus's Appeal: The Witch of Ink", //Name of the archive listing.
                           "Grants the item to summon the Witch of Ink", //Description of the listing.
                           penthBossItemDialogue == 2, //Unlock requirements.
                           302,
                           "???")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Perseus's Appeal: The Arbiter", //Name of the archive listing.
                           "Grants the item to summon Arbitration.", //Description of the listing.
                           arbiterBossItemDialogue == 2, //Unlock requirements.
                           303,
                           "???")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Perseus's Appeal: The Warrior of Light", //Name of the archive listing.
                           "Grants the item to summon the Warrior of Light.", //Description of the listing.
                           warriorBossItemDialogue == 2, //Unlock requirements.
                           304,
                           "???")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Nalhaun Kneeled", //Name of the archive listing.
                           "Unlocked after defeating Nalhaun, the Burnished King. Grants a material needed for confronting the final boss.", //Description of the listing.
                           nalhaunDialogue == 2, //Unlock requirements.
                           70,
                           "Defeat Nalhaun, the Burnished King.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Penthesilea Washed", //Name of the archive listing.
                           "Unlocked after defeating Penthesilea, the Witch of Ink. Grants a material needed for confronting the final boss.", //Description of the listing.
                           penthDialogue == 2, //Unlock requirements.
                           71,
                           "Defeat Penthesilea, the Witch of Ink.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Arbitration Purified", //Name of the archive listing.
                           "Unlocked after defeating Arbitration. Grants a material needed for confronting the final boss.", //Description of the listing.
                           arbiterDialogue == 2, //Unlock requirements.
                           72,
                           "Defeat Arbitration.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "Warrior of Light Vanquished", //Name of the archive listing.
                           "Unlocked after defeating the Warrior of Light. Grants a material needed for confronting the final boss.", //Description of the listing.
                           WarriorOfLightDialogue == 2, //Unlock requirements.
                           66,
                           "Defeat the Warrior of Light.")); //Corresponding dialogue ID.
                    BossArchiveList.Add(new BossArchiveListing(
                           "The First Starfarer Defeated", //Name of the archive listing.
                           "Unlocked after defeating Tsukiyomi, the First Starfarer. Grants an item used for crafting.", //Description of the listing.
                           tsukiyomiDialogue == 2, //Unlock requirements.
                           73,
                           "Defeat ???")); //Corresponding dialogue ID.
                    if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
                    {
                        BossArchiveList.Add(new BossArchiveListing(
                           "Desert Scourge Defeated", //Name of the archive listing.
                           "Unlocked after defeating the Desert Scourge", //Description of the listing.
                           desertscourgeDialogue == 2, //Unlock requirements.
                           201,
                           "Defeat the Desert Scourge")); //Corresponding dialogue ID.
                        BossArchiveList.Add(new BossArchiveListing(
                               "Crabulon Defeated", //Name of the archive listing.
                           "Unlocked after defeating Crabulon", //Description of the listing.
                           crabulonDialogue == 2, //Unlock requirements.
                           202,
                           "Defeat Crabulon")); //Corresponding dialogue ID.
                        BossArchiveList.Add(new BossArchiveListing(
                               "Hive Mind Defeated", //Name of the archive listing.
                               "Unlocked after defeating the Hive Mind", //Description of the listing.
                               hivemindDialogue == 2, //Unlock requirements.
                               203,
                               "Defeat the Hive Mind")); //Corresponding dialogue ID.
                        BossArchiveList.Add(new BossArchiveListing(
                               "Perforators Defeated", //Name of the archive listing.
                               "Unlocked after defeating the Perforators", //Description of the listing.
                               perforatorDialogue == 2, //Unlock requirements.
                               204,
                               "Defeat the Perforators")); //Corresponding dialogue ID.
                        BossArchiveList.Add(new BossArchiveListing(
                               "Slime God Defeated", //Name of the archive listing.
                               "Unlocked after defeating the Slime God", //Description of the listing.
                               slimegodDialogue == 2, //Unlock requirements.
                               205,
                               "Defeat the Slime God")); //Corresponding dialogue ID.
                        BossArchiveList.Add(new BossArchiveListing(
                               "Cryogen Defeated", //Name of the archive listing.
                               "Unlocked after defeating Cryogen", //Description of the listing.
                               cryogenDialogue == 2, //Unlock requirements.
                               206,
                               "Defeat Cryogen")); //Corresponding dialogue ID.
                        BossArchiveList.Add(new BossArchiveListing(
                               "Aquatic Scourge Defeated", //Name of the archive listing.
                               "Unlocked after defeating the Aquatic Scourge", //Description of the listing.
                               aquaticscourgeDialogue == 2, //Unlock requirements.
                               207,
                               "Defeat the Aquatic Scourge")); //Corresponding dialogue ID.
                        BossArchiveList.Add(new BossArchiveListing(
                               "Brimstone Elemental Defeated", //Name of the archive listing.
                               "Unlocked after defeating the Brimstone Elemental", //Description of the listing.
                               brimstoneelementalDialogue == 2, //Unlock requirements.
                               208,
                               "Defeat the Brimstone Elemental")); //Corresponding dialogue ID.
                        BossArchiveList.Add(new BossArchiveListing(
                               "Calamitas's Clone Defeated", //Name of the archive listing.
                               "Unlocked after defeating Calamitas's Clone", //Description of the listing.
                               calamitasDialogue == 2, //Unlock requirements.
                               209,
                               "Defeat Calamitas")); //Corresponding dialogue ID.
                        BossArchiveList.Add(new BossArchiveListing(
                               "Leviathan Defeated", //Name of the archive listing.
                               "Unlocked after defeating the Leviathan", //Description of the listing.
                               leviathanDialogue == 2, //Unlock requirements.
                               210,
                               "Defeat the Leviathan")); //Corresponding dialogue ID.
                        BossArchiveList.Add(new BossArchiveListing(
                               "Astrum Aureus Defeated", //Name of the archive listing.
                               "Unlocked after defeating Astrum Aureus", //Description of the listing.
                               astrumaureusDialogue == 2, //Unlock requirements.
                               211,
                               "Defeat Astrum Aureus")); //Corresponding dialogue ID.
                        BossArchiveList.Add(new BossArchiveListing(
                               "Plaguebringer Goliath", //Name of the archive listing.
                               "Unlocked after defeating the Plaguebringer Goliath", //Description of the listing.
                               plaguebringerDialogue == 2, //Unlock requirements.
                               212,
                               "Defeat the Plaguebringer Goliath")); //Corresponding dialogue ID.
                        BossArchiveList.Add(new BossArchiveListing(
                               "Ravager Defeated", //Name of the archive listing.
                               "Unlocked after defeating the Ravager", //Description of the listing.
                               ravagerDialogue == 2, //Unlock requirements.
                               213,
                               "Defeat the Ravager")); //Corresponding dialogue ID.
                        BossArchiveList.Add(new BossArchiveListing(
                               "Astrum Deus Defeated", //Name of the archive listing.
                               "Unlocked after defeating Astrum Deus", //Description of the listing.
                               astrumdeusDialogue == 2, //Unlock requirements.
                               214,
                               "Defeat Astrum Deus")); //Corresponding dialogue ID.
                    }
                    //Weapons
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "", //Name of the archive listing.
                          $"", //Description of the listing.
                          false, //Unlock requirements.
                          0,
                          "")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                           "King Slime Weapon", //Name of the archive listing.
                           $"Grants the Essence for either the [i:{ItemType<Astral>()}] Aegis Driver or the [i:{ItemType<Umbral>()}] Rad Gun.", //Description of the listing.
                           KingSlimeWeaponDialogue == 2, //Unlock requirements.
                           104,
                           "Defeat King Slime")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                           "Eye of Cthulhu Weapon", //Name of the archive listing.
                           $"Grants the Essence for either " +
                           $"[i:{ItemType<Astral>()}] Carian Dark Moon " +
                           $"or " +
                           $"[i:{ItemType<Umbral>()}] Konpaku Katana.", //Description of the listing.
                           EyeBossWeaponDialogue == 2, //Unlock requirements.
                           136,
                           "Defeat Eye of Cthulhu")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Graveyard Weapon", //Name of the archive listing.
                         $"Grants the Essence for either " +
                           $"[i:{ItemType<Astral>()}] Kevesi Farewell " +
                           $"or " +
                           $"[i:{ItemType<Umbral>()}] Agnian Farewell.", //Description of the listing.
                        FarewellWeaponDialogue == 2, //Unlock requirements.
                        159,
                        "Visit a Graveyard biome.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Corruption/Crimson Boss Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Astral>()}] Neo Dealmaker " +
                          $"or" +
                          $"[i:{ItemType<Umbral>()}] Ashen Ambition.", //Description of the listing.
                          CorruptBossWeaponDialogue == 2, //Unlock requirements.
                          137,
                          "Defeat the boss of the world's evil")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Corruption/Crimson Boss Weapon", //Name of the archive listing.
                          $"Grants the Essence for " +
                          $"[i:{ItemType<Spatial>()}] Takonomicon. ", //Description of the listing.
                          TakodachiWeaponDialogue == 2, //Unlock requirements.
                          133,
                          "Defeat the boss of the world's evil, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Queen Bee Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Astral>()}] Skofnung " +
                          $"or " +
                          $"[i:{ItemType<Umbral>()}] Inugami Ripsaw.", //Description of the listing.
                          QueenBeeWeaponDialogue == 2, //Unlock requirements.
                          103,
                          "Defeat the Queen Bee.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Skeletron Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Umbral>()}] Death in Four Acts " +
                          $"or " +
                          $"[i:{ItemType<Astral>()}] Der Freischutz.", //Description of the listing.
                          SkeletonWeaponDialogue == 2, //Unlock requirements.
                          101,
                          "Defeat Skeletron.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Skeletron Weapon", //Name of the archive listing.
                          $"Grants the Essence for " +
                          $"[i:{ItemType<Spatial>()}] Misery's Company. ", //Description of the listing.
                          MiseryWeaponDialogue == 2, //Unlock requirements.
                          120,
                          "Defeat Skeletron, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Skeletron Weapon", //Name of the archive listing.
                          $"Grants the Essence for " +
                          $"[i:{ItemType<Spatial>()}] Apalistik. ", //Description of the listing.
                          OceanWeaponDialogue == 2, //Unlock requirements.
                          123,
                          "Defeat Skeletron, then visit the beach.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Skeletron Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Astral>()}] Persephone " +
                          $"or " +
                          $"[i:{ItemType<Umbral>()}] Kazimierz Seraphim.", //Description of the listing.
                          HellWeaponDialogue == 2, //Unlock requirements.
                          102,
                          "Defeat Skeletron, then visit the Underworld.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Skeletron Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Astral>()}] Karlan Truesilver " +
                          $"or " +
                          $"[i:{ItemType<Umbral>()}] Every Moment Matters.", //Description of the listing.
                          WallOfFleshWeaponDialogue == 2, //Unlock requirements.
                          105,
                          "Defeat Skeletron.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Wall of Flesh Weapon", //Name of the archive listing.
                          $"Grants the Essence for " +
                          $"[i:{ItemType<Spatial>()}] Luminary Wand. ", //Description of the listing.
                          LumaWeaponDialogue == 2, //Unlock requirements.
                          124,
                          "Defeat the Wall of Flesh, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Wall of Flesh Weapon", //Name of the archive listing.
                          $"Grants the Essence for " +
                          $"[i:{ItemType<Spatial>()}] Force-of-Nature. ", //Description of the listing.
                          ForceWeaponDialogue == 2, //Unlock requirements.
                          131,
                          "Defeat the Wall of Flesh, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Hallowed Biome Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Aurum Edge. ", //Description of the listing.
                        GoldWeaponDialogue == 2, //Unlock requirements.
                        158,
                        "Visit the Hallowed biome.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Queen Slime Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Astral>()}] Hunter's Symphony " +
                          $"or " +
                          $"[i:{ItemType<Umbral>()}] Sparkblossom's Beacon.", //Description of the listing.
                          QueenSlimeWeaponDialogue == 2, //Unlock requirements.
                          149,//Corresponding dialogue ID.
                          "Defeat Queen Slime."));
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Vagrant of Space and Time Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Astral>()}] Izanagi's Edge " +
                          $"or " +
                          $"[i:{ItemType<Umbral>()}] Hawkmoon.", //Description of the listing.
                          VagrantWeaponDialogue == 2, //Unlock requirements.
                          115,
                          "Defeat the Vagrant of Space and Time.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Any Mechanical Boss Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Astral>()}] Veneration of Butterflies " +
                          $"or " +
                          $"[i:{ItemType<Umbral>()}] Memento Muse.", //Description of the listing.
                          MechBossWeaponDialogue == 2, //Unlock requirements.
                          106,
                          "Defeat any mechanical boss.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Any Mechanical Boss Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Astral>()}] Ride the Bull " +
                          $"or " +
                          $"[i:{ItemType<Umbral>()}] Drachenlance.", //Description of the listing.
                          MechBossWeaponDialogue == 2, //Unlock requirements.
                          107,
                          "Defeat any mechanical boss, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                         "Any Mechanical Boss Weapon", //Name of the archive listing.
                         $"Grants the Essence for " +
                         $"[i:{ItemType<Spatial>()}] Xenoblade. ", //Description of the listing.
                         MonadoWeaponDialogue == 2, //Unlock requirements.
                         125,
                         "Defeat any mechanical boss, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                         "Skeletron Prime Weapon", //Name of the archive listing.
                         $"Grants the Essence for " +
                         $"[i:{ItemType<Spatial>()}] Armaments of the Sky Striker. ", //Description of the listing.
                         SkyStrikerWeaponDialogue == 2, //Unlock requirements.
                         135,
                         "Defeat Skeletron Prime.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                         "All Mechanical Bosses Weapon", //Name of the archive listing.
                         $"Grants the Essence for " +
                         $"[i:{ItemType<Spatial>()}] Hullwrought. ", //Description of the listing.
                         HullwroughtWeaponDialogue == 2, //Unlock requirements.
                         121,
                         "Defeat all the mechanical bosses.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                       "All Mechanical Bosses Weapon", //Name of the archive listing.
                       $"Grants the Essence for " +
                       $"[i:{ItemType<Spatial>()}] El Capitan's Hardware.", //Description of the listing.
                       HardwareWeaponDialogue == 2, //Unlock requirements.
                       154,
                       "Defeat all the mechanical bosses, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Nalhaun Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Astral>()}] Phantom in the Mirror " +
                          $"or " +
                          $"[i:{ItemType<Umbral>()}] Hollowheart Albion.", //Description of the listing.
                          NalhaunWeaponDialogue == 2, //Unlock requirements.
                          117,
                          "Defeat Nalhaun, the Burnished King.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Plantera Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Astral>()}] Crimson Outbreak " +
                          $"or " +
                          $"[i:{ItemType<Umbral>()}] Voice of the Fallen.", //Description of the listing.
                          PlanteraWeaponDialogue == 2, //Unlock requirements.
                          108,
                          "Defeat Plantera.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Plantera Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Kifrosse. ", //Description of the listing.
                        KifrosseWeaponDialogue == 2, //Unlock requirements.
                        129,
                        "Defeat Plantera, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Frost Queen Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Astral>()}] Stygian Nymph " +
                          $"or " +
                          $"[i:{ItemType<Umbral>()}] Caesura of Despair.", //Description of the listing.
                          FrostMoonWeaponDialogue == 2, //Unlock requirements.
                          126,
                          "Defeat the Frost Queen.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Penthesilea Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Astral>()}] Vision of Euthymia " +
                          $"or " +
                          $"[i:{ItemType<Umbral>()}] Kroniic Principality.", //Description of the listing.
                          PenthesileaWeaponDialogue == 2, //Unlock requirements.
                          118,
                          "Defeat Penthesilea, the Witch of Ink.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Penthesilea Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Penthesilea's Muse. ", //Description of the listing.
                        MuseWeaponDialogue == 2, //Unlock requirements.
                        128,
                        "Defeat Penthesilea, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Golem Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Astral>()}] Plenilune Gaze " +
                          $"or " +
                          $"[i:{ItemType<Umbral>()}] Tartaglia.", //Description of the listing.
                          GolemWeaponDialogue == 2, //Unlock requirements.
                          109,
                          "Defeat Golem.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Golem Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Genocide. ", //Description of the listing.
                        GenocideWeaponDialogue == 2, //Unlock requirements.
                        132,
                        "Defeat Golem, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Golem Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Gloves of the Black Silence. ", //Description of the listing.
                        SilenceWeaponDialogue == 2, //Unlock requirements.
                        156,
                        "Defeat Golem, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Arbitration Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Astral>()}] Liberation Blazing " +
                          $"or " +
                          $"[i:{ItemType<Umbral>()}] Unforgotten.", //Description of the listing.
                          ArbitrationWeaponDialogue == 2, //Unlock requirements.
                          119,
                          "Defeat Arbitration.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Arbitration Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Claimh Solais. ", //Description of the listing.
                        ClaimhWeaponDialogue == 2, //Unlock requirements.
                        127,
                        "Defeat Arbitration, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Duke Fishron Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Astral>()}] Key of the Sinner " +
                          $"or " +
                          $"[i:{ItemType<Umbral>()}] Crimson Sakura Alpha.", //Description of the listing.
                          DukeFishronWeaponDialogue == 2, //Unlock requirements.
                          116,
                          "Defeat Duke Fishron.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Lunatic Cultist Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Astral>()}] Rex Lapis " +
                          $"or " +
                          $"[i:{ItemType<Umbral>()}] Yunlai Stiletto.", //Description of the listing.
                          LunaticCultistWeaponDialogue == 2, //Unlock requirements.
                          110,
                          "Defeat Lunatic Cultist.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Lunatic Cultist Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Twin Stars of Albiero. ", //Description of the listing.
                        TwinStarsWeaponDialogue == 2, //Unlock requirements.
                        134,
                        "Defeat Lunatic Cultist, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Lunatic Cultist Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Catalyst's Memory. ", //Description of the listing.
                        CatalystWeaponDialogue == 2, //Unlock requirements.
                        155,
                        "Defeat Lunatic Cultist, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Moon Lord Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Astral>()}] Suistrume " +
                          $"or " +
                          $"[i:{ItemType<Umbral>()}] Naganadel.", //Description of the listing.
                          MoonLordWeaponDialogue == 2, //Unlock requirements.
                          111,
                          "Defeat Moon Lord.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Moon Lord Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Shadowless Cerulean. ", //Description of the listing.
                        ShadowlessWeaponDialogue == 2, //Unlock requirements.
                        122,
                        "Defeat Moon Lord, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Moon Lord Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Soul Reaver. ", //Description of the listing.
                        SoulWeaponDialogue == 2, //Unlock requirements.
                        157,
                        "Defeat Moon Lord, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                         "Moon Lord Weapon", //Name of the archive listing.
                         $"Grants the Essence for " +
                         $"[i:{ItemType<Spatial>()}] Virtue's Edge. ", //Description of the listing.
                         VirtueWeaponDialogue == 2, //Unlock requirements.
                         150,
                         "Defeat Moon Lord, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                          "Warrior of Light Weapon", //Name of the archive listing.
                          $"Grants the Essence for either " +
                          $"[i:{ItemType<Astral>()}] Key of the King's Law " +
                          $"or " +
                          $"[i:{ItemType<Umbral>()}] Light Unrelenting.", //Description of the listing.
                          WarriorWeaponDialogue == 2, //Unlock requirements.
                          112,
                          "Defeat Warrior of Light.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Warrior of Light Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Vermilion Riposte. ", //Description of the listing.
                        RedMageWeaponDialogue == 2, //Unlock requirements.
                        151,
                        "Defeat the Warrior of Light, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                       "Warrior of Light Weapon", //Name of the archive listing.
                       $"Grants the Essence for " +
                       $"[i:{ItemType<Spatial>()}] Burning Desire. ", //Description of the listing.
                       BlazeWeaponDialogue == 2, //Unlock requirements.
                       152,
                       "Defeat the Warrior of Light, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                       "Warrior of Light Weapon", //Name of the archive listing.
                       $"Grants the Essence for " +
                       $"[i:{ItemType<Spatial>()}] The Everlasting Pickaxe. ", //Description of the listing.
                       PickaxeWeaponDialogue == 2, //Unlock requirements.
                       153,
                       "Defeat the Warrior of Light, then wait.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Tsukiyomi Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Architect's Luminance. ", //Description of the listing.
                        ArchitectWeaponDialogue == 2, //Unlock requirements.
                        130,
                        "Defeat ???")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Tsukiyomi Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Cosmic Destroyer. ", //Description of the listing.
                        CosmicDestroyerWeaponDialogue == 2, //Unlock requirements.
                        138,
                        "Defeat ???")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Empress of Light Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Arachnid Needlepoint. ", //Description of the listing.
                        NeedlepointWeaponDialogue == 2, //Unlock requirements.
                        140,
                        "Defeat the Empress of Light.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Empress of Light Weapon EX", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] The Only Thing I Know For Real. ", //Description of the listing.
                        MurasamaWeaponDialogue == 2, //Unlock requirements.
                        139,
                        "Defeat the Empress of Light in Master Mode.")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Golem Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Mercy. ", //Description of the listing.
                        MercyWeaponDialogue == 2, //Unlock requirements.
                        141,
                        "Defeat Golem, then wait. ")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Empress of Light Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Sakura's Vengeance. ", //Description of the listing.
                        SakuraWeaponDialogue == 2, //Unlock requirements.
                        142,
                        "Defeat Empress of Light. ")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Empress of Light Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Eternal Star. ", //Description of the listing.
                        EternalWeaponDialogue == 2, //Unlock requirements.
                        143,
                        "Defeat Moon Lord, then wait. ")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Moon Lord Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Vermilion Daemon. ", //Description of the listing.
                        DaemonWeaponDialogue == 2, //Unlock requirements.
                        144,
                        "Defeat Moon Lord, then wait. ")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Lunatic Cultist Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Ozma Ascendant. ", //Description of the listing.
                        OzmaWeaponDialogue == 2, //Unlock requirements.
                        145,
                        "Defeat Lunatic Cultist, then wait. ")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Queen Slime Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] Dreadnought Chemtank. ", //Description of the listing.
                        UrgotWeaponDialogue == 2, //Unlock requirements.
                        146,
                        "Defeat Queen Slime, then wait. ")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Pumpkin King Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] The Blood Blade. ", //Description of the listing.
                        BloodWeaponDialogue == 2, //Unlock requirements.
                        147,
                        "Defeat Pumpking King, then wait. ")); //Corresponding dialogue ID.
                    WeaponArchiveList.Add(new WeaponArchiveListing(
                        "Deerclops Weapon", //Name of the archive listing.
                        $"Grants the Essence for " +
                        $"[i:{ItemType<Spatial>()}] The Morning Star. ", //Description of the listing.
                        MorningStarWeaponDialogue == 2, //Unlock requirements.
                        148,
                        "Defeat Deerclops, then wait. ")); //Corresponding dialogue ID.
                                                           //Corresponding dialogue ID.

                    //VN
                    VNArchiveList.Add(new VNArchiveListing(
                          "", //Name of the archive listing.
                          $"", //Description of the listing.
                          false, //Unlock requirements.
                          0,
                          "")); //Corresponding dialogue ID.
                    VNArchiveList.Add(new VNArchiveListing(
                           "Intro Dialogue", //Name of the archive listing.
                           $"The Starfarer's introduction dialogue.", //Description of the listing.
                           chosenStarfarer == 1, //Unlock requirements.
                           3,
                           "Asphodene's intro dialogue.")); //Corresponding dialogue ID.
                    VNArchiveList.Add(new VNArchiveListing(
                           "Eridani's Intro Dialogue", //Name of the archive listing.
                           $"The Starfarer's introduction dialogue.", //Description of the listing.
                           chosenStarfarer == 2, //Unlock requirements.
                           6,
                           "Eridani's intro dialogue.")); //Corresponding dialogue ID.
                    VNArchiveList.Add(new VNArchiveListing(
                           "Vagrant Post-Battle (Asphodene)", //Name of the archive listing.
                           $"Perseus's introduction.", //Description of the listing.
                           chosenStarfarer == 1 && DownedBossSystem.downedVagrant, //Unlock requirements.
                           9,
                           "Defeat the Vagrant of Space and Time. (Asphodene)")); //Corresponding dialogue ID.
                    VNArchiveList.Add(new VNArchiveListing(
                           "Vagrant Post-Battle (Eridani)", //Name of the archive listing.
                           $"Perseus's introduction.", //Description of the listing.
                           chosenStarfarer == 2 && DownedBossSystem.downedVagrant, //Unlock requirements.
                           10,
                           "Defeat the Vagrant of Space and Time. (Eridani)")); //Corresponding dialogue ID.
                    IdleArchiveListMax = IdleArchiveList.Count;
                    BossArchiveListMax = BossArchiveList.Count;
                    WeaponArchiveListMax = WeaponArchiveList.Count;
                    VNArchiveListMax = VNArchiveList.Count;
                    archivePopulated = true;
                }

                archiveListInfo = "";
                if (archiveChosenList == 0)//0 is idle, 1 is boss, 2 is weapon (prompts use the OnEvent system.)
                {
                    archiveListMax = IdleArchiveListMax - 1;//Begins at 2, which is fallback dialogue. 24
                }
                if (archiveChosenList == 1)//0 is idle, 1 is boss, 2 is weapon, (prompts use the OnEvent system.)
                {
                    archiveListMax = BossArchiveListMax - 1;//23
                }
                if (archiveChosenList == 2)//0 is idle, 1 is boss, 2 is weapon, (prompts use the OnEvent system.)
                {
                    archiveListMax = WeaponArchiveListMax - 1;//46
                }
                if (archiveChosenList == 3)//This will be the VN style dialogue archive. It doesn't have to be implemented on 1.1, as nothing is locked behind it except dialogue.
                {
                    archiveListMax = VNArchiveListMax - 1;//
                }


                //TODO: Replace the ArchiveChosenList == system with automatically selecting the correct information from the Idle Archive Listing.
                //Also, text wrap the description.
                if (archiveActive && archivePopulated)
                {
                    if (archiveChosenList == 0)
                    {
                        if (IdleArchiveList[archiveListNumber].IsViewable)
                        {
                            canViewArchive = true;
                            archiveListInfo = Wrap("" + IdleArchiveList[archiveListNumber].Name + ":" + "\n" + IdleArchiveList[archiveListNumber].ListInformation, 25);

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = Wrap(IdleArchiveList[archiveListNumber].UnlockConditions, 25);
                        }

                    }
                    if (archiveChosenList == 1)
                    {
                        if (BossArchiveList[archiveListNumber].IsViewable)
                        {
                            canViewArchive = true;
                            archiveListInfo = Wrap("" + BossArchiveList[archiveListNumber].Name + ":" + "\n" + BossArchiveList[archiveListNumber].ListInformation, 25);

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = Wrap(BossArchiveList[archiveListNumber].UnlockConditions, 25);
                        }

                    }
                    if (archiveChosenList == 2)
                    {
                        if (WeaponArchiveList[archiveListNumber].IsViewable)
                        {
                            canViewArchive = true;
                            archiveListInfo = Wrap("" + WeaponArchiveList[archiveListNumber].Name + ":" + "\n" + WeaponArchiveList[archiveListNumber].ListInformation, 25);

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = Wrap(WeaponArchiveList[archiveListNumber].UnlockConditions, 25);
                        }

                    }
                    if (archiveChosenList == 3)
                    {
                        if (VNArchiveList[archiveListNumber].IsViewable)
                        {
                            canViewArchive = true;
                            archiveListInfo = Wrap("" + VNArchiveList[archiveListNumber].Name + ":" + "\n" + VNArchiveList[archiveListNumber].ListInformation, 25);

                        }
                        else
                        {
                            canViewArchive = false;
                            archiveListInfo = Wrap(VNArchiveList[archiveListNumber].UnlockConditions, 25);
                        }

                    }

                }








                #endregion
                #region Stellar Novas

                //////////////////////////////////////////////////////////////////////////////////////////// PRISMS
                if (affix1 == Mod.Find<ModItem>("RefulgentPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("RefulgentPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("RefulgentPrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    novaDamageMod += 0.2;//20%
                    novaCritChanceMod -= 14;
                    novaCritDamageMod -= 0.1;
                    novaChargeMod += 5;
                }
                if (affix1 == Mod.Find<ModItem>("EverflamePrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("EverflamePrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("EverflamePrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    novaDamageMod += 0.1;
                    novaCritChanceMod += 7;
                    novaCritDamageMod += 0.1;
                    novaChargeMod -= 15;
                }
                if (affix1 == Mod.Find<ModItem>("CrystallinePrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("CrystallinePrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("CrystallinePrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    novaDamageMod -= 0.2;
                    novaCritChanceMod -= 7;
                    novaCritDamageMod += 0.4;
                    //novaChargeMod -= 15;
                }
                if (affix1 == Mod.Find<ModItem>("VerdantPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("VerdantPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("VerdantPrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    //novaDamageMod += 50;
                    novaCritChanceMod += 21;
                    //novaCritDamageMod += 225;
                    novaChargeMod -= 15;
                }
                if (affix1 == Mod.Find<ModItem>("RadiantPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("RadiantPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("RadiantPrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    novaDamageMod -= 0.1;
                    novaCritChanceMod -= 7;
                    novaCritDamageMod -= 0.1;
                    novaChargeMod += 15;
                }
                if (affix1 == Mod.Find<ModItem>("ApocryphicPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("ApocryphicPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("ApocryphicPrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    novaDamageMod += 0.2;
                    novaCritChanceMod -= 14;
                    novaCritDamageMod += 0.1;
                    novaChargeMod -= 5;
                }
                if (affix1 == Mod.Find<ModItem>("AlchemicPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("AlchemicPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("AlchemicPrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    novaDamageMod -= 0.1;
                    novaCritChanceMod += 14;
                    novaCritDamageMod += 0.1;
                    novaChargeMod -= 10;
                }
                if (affix1 == Mod.Find<ModItem>("CastellicPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("CastellicPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("CastellicPrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    novaDamageMod += 0.3;
                    novaCritChanceMod -= 7;
                    novaCritDamageMod -= 0.2;
                    //novaChargeMod -= 10;
                }
                if (affix1 == Mod.Find<ModItem>("LucentPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("LucentPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("LucentPrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    novaDamageMod -= 0.3;
                    //novaCritChanceMod -= 7;
                    novaCritDamageMod += 0.1;
                    novaChargeMod += 10;
                }
                if (affix1 == Mod.Find<ModItem>("PhylacticPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("PhylacticPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("PhylacticPrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    novaDamageMod -= 0.1;
                    novaCritChanceMod += 21;
                    //novaCritDamageMod += 75;
                    novaChargeMod -= 10;
                }
                if (affix1 == "Calamitous Prism" || affix2 == "Calamitous Prism" || affix3 == "Calamitous Prism")
                {
                    //novaDamageMod += 10000;
                    //novaCritChanceMod += 21;
                    //novaCritDamageMod += 17775;
                    //novaChargeMod -= 10;
                }
                if (affix1 == Mod.Find<ModItem>("LightswornPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("LightswornPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("LightswornPrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    lightswornPrism = true;
                }
                else
                {
                    lightswornPrism = false;
                }
                if (affix1 == Mod.Find<ModItem>("BurnishedPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("BurnishedPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("BurnishedPrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    burnishedPrism = true;
                }
                else
                {
                    burnishedPrism = false;
                }
                if (affix1 == Mod.Find<ModItem>("SpatialPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("SpatialPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("SpatialPrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    spatialPrism = true;
                }
                else
                {
                    spatialPrism = false;
                }
                if (affix1 == Mod.Find<ModItem>("PaintedPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("PaintedPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("PaintedPrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    paintedPrism = true;
                }
                else
                {
                    paintedPrism = false;
                }
                if (affix1 == Mod.Find<ModItem>("VoidsentPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("VoidsentPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("VoidsentPrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    voidsentPrism = true;
                }
                //1.1.6 prisms
                if (affix1 == Mod.Find<ModItem>("RoyalSlimePrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("RoyalSlimePrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("RoyalSlimePrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    royalSlimePrism = true;
                }
                else
                {
                    royalSlimePrism = false;
                }
                if (affix1 == Mod.Find<ModItem>("MechanicalPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("MechanicalPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("MechanicalPrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    mechanicalPrism = true;
                }
                else
                {
                    mechanicalPrism = false;
                }
                if (affix1 == Mod.Find<ModItem>("OvergrownPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("OvergrownPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("OvergrownPrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    overgrownPrism = true;
                }
                else
                {
                    overgrownPrism = false;
                }
                if (affix1 == Mod.Find<ModItem>("LihzahrdPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("LihzahrdPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("LihzahrdPrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    lihzahrdPrism = true;
                }
                else
                {
                    lihzahrdPrism = false;
                }
                if (affix1 == Mod.Find<ModItem>("TyphoonPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("TyphoonPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("TyphoonPrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    typhoonPrism = true;
                }
                else
                {
                    typhoonPrism = false;
                }
                if (affix1 == Mod.Find<ModItem>("EmpressPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("EmpressPrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("EmpressPrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    empressPrism = true;
                }
                else
                {
                    empressPrism = false;
                }
                if (affix1 == Mod.Find<ModItem>("LuminitePrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix2 == Mod.Find<ModItem>("LuminitePrism").DisplayName.GetTranslation(Language.ActiveCulture) ||
                    affix3 == Mod.Find<ModItem>("LuminitePrism").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    luminitePrism = true;
                }
                else
                {
                    luminitePrism = false;
                }
                //Tier 3 Prisms
                if (affix1 == Mod.Find<ModItem>("PrismOfTheRuinedKing").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    ruinedKingPrism = true;

                }
                else
                {
                    ruinedKingPrism = false;

                }
                if (affix1 == Mod.Find<ModItem>("PrismOfTheCosmicPhoenix").DisplayName.GetTranslation(Language.ActiveCulture))
                {
                    cosmicPhoenixPrism = true;

                }
                else
                {
                    cosmicPhoenixPrism = false;

                }
                //Starfarer stuff here.
                if (starfarerArmorEquipped != null)
                {
                    if (starfarerArmorEquipped.type == ItemType<FaerieVoyagerAttire>())
                    {
                        starfarerOutfit = 1;
                    }

                    if (starfarerArmorEquipped.type == ItemType<StellarCasualAttire>())
                    {
                        starfarerOutfit = 2;
                    }

                    if (starfarerArmorEquipped.type == ItemType<CelestialPrincessGenesis>())
                    {
                        starfarerOutfit = 3;
                    }

                    if (starfarerArmorEquipped.type == ItemType<AegisOfHopesLegacy>())
                    {
                        starfarerOutfit = 4;
                    }

                }
                if (starfarerVanityEquipped != null)
                {
                    if (starfarerVanityEquipped.type == ItemType<FaerieVoyagerAttire>())
                    {
                        starfarerOutfitVanity = 1;
                    }
                    if (starfarerVanityEquipped.type == ItemType<StellarCasualAttire>())
                    {
                        starfarerOutfitVanity = 2;
                    }
                    if (starfarerVanityEquipped.type == ItemType<CelestialPrincessGenesis>())
                    {
                        starfarerOutfitVanity = 3;
                    }
                    if (starfarerVanityEquipped.type == ItemType<AegisOfHopesLegacy>())
                    {
                        starfarerOutfitVanity = 4;
                    }
                    if (starfarerVanityEquipped.type == ItemType<FamiliarLookingAttire>())
                    {
                        starfarerOutfitVanity = -1;
                    }
                }
                if (chosenStellarNova == 0)//No Nova selected
                {
                    abilityName = "";
                    abilitySubName = "No Stellar Nova has been selected.";
                    abilityDescription = "";
                    starfarerBonus = "";
                    baseStats = "";


                }

                ////////////////////////////////////////////////////////////////////////////////////////////////
                ///PRISM DESCRIPTIONS HAVE BEEN REMOVED (HOVERTEXT NOW)
                ///

                //////////////////////////////////
                if (chosenStellarNova == 1)//Theofania Inanis
                {
                    novaDamage = baseNovaDamageAdd;
                    novaGaugeMax = 90;
                    novaCritChance = 50;
                    novaCritDamage = (int)(baseNovaDamageAdd * 1.45);

                    abilityName = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.TheofaniaInanis.AbilityName");
                    abilitySubName = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.TheofaniaInanis.AbilitySubName");
                    abilityDescription = starfarerBonus = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.TheofaniaInanis.AbilityDescription");

                    if (chosenStarfarer == 1)
                    {
                        starfarerBonus = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.TheofaniaInanis.AstralBonus");
                    }
                    else
                    {
                        starfarerBonus = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.TheofaniaInanis.UmbralBonus");
                    }

                    baseStats = "" +
                        $"{novaDamage} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.BaseDamage") +
                        $"\n{novaGaugeMax} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.BaseEnergyCost") +
                        $"\n" +//int               0.1(x) double
                        $"\n{Math.Round(novaDamage * (1 + novaDamageMod), 5)} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.Damage") +
                        $"\n{novaCritChance + novaCritChanceMod}% " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.CritChance") +
                        $"\n{Math.Round(novaCritDamage * (1 + novaCritDamageMod), 5)} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.CritDamage") +
                        $"\n{novaGaugeMax - novaChargeMod} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.EnergyCost");


                }
                if (chosenStellarNova == 2)//Ars Laevateinn
                {
                    novaDamage = 250 + baseNovaDamageAdd;
                    novaGaugeMax = 110;
                    novaCritChance = 35;
                    novaCritDamage = (int)(baseNovaDamageAdd * 2.8);

                    abilityName = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.ArsLaevateinn.AbilityName");
                    abilitySubName = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.ArsLaevateinn.AbilitySubName");
                    abilityDescription = starfarerBonus = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.ArsLaevateinn.AbilityDescription");

                    if (chosenStarfarer == 1)
                    {
                        starfarerBonus = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.ArsLaevateinn.AstralBonus");
                    }
                    else
                    {
                        starfarerBonus = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.ArsLaevateinn.UmbralBonus");
                    }

                    baseStats = "" +
                        $"{novaDamage} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.BaseDamage") +
                        $"\n{novaGaugeMax} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.BaseEnergyCost") +
                        $"\n" +
                        $"\n{Math.Round(novaDamage * (1 + novaDamageMod), 5)} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.Damage") +
                        $"\n{novaCritChance + novaCritChanceMod}% " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.CritChance") +
                        $"\n{Math.Round(novaCritDamage * (1 + novaCritDamageMod), 5)} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.CritDamage") +
                        $"\n{novaGaugeMax - novaChargeMod} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.EnergyCost");


                }
                if (chosenStellarNova == 3)//Kiwami Ryuken (KiwamiRyuken)
                {
                    novaDamage = baseNovaDamageAdd / 2;
                    novaGaugeMax = 50;
                    novaCritChance = 70;
                    novaCritDamage = (int)(baseNovaDamageAdd);

                    abilityName = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.KiwamiRyuken.AbilityName");
                    abilitySubName = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.KiwamiRyuken.AbilitySubName");
                    abilityDescription = starfarerBonus = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.KiwamiRyuken.AbilityDescription");

                    if (chosenStarfarer == 1)
                    {
                        starfarerBonus = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.KiwamiRyuken.AstralBonus");
                    }
                    else
                    {
                        starfarerBonus = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.KiwamiRyuken.UmbralBonus");
                    }

                    baseStats = "" +
                        $"{novaDamage} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.BaseDamage") +
                        $"\n{novaGaugeMax} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.BaseEnergyCost") +
                        $"\n" +
                        $"\n{Math.Round(novaDamage * (1 + novaDamageMod), 5)} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.Damage") +
                        $"\n{novaCritChance + novaCritChanceMod}% " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.CritChance") +
                        $"\n{Math.Round(novaCritDamage * (1 + novaCritDamageMod), 5)} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.CritDamage") +
                        $"\n{novaGaugeMax - novaChargeMod} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.EnergyCost");


                }
                if (chosenStellarNova == 4)//Garden of Avalon
                {
                    novaDamage = baseNovaDamageAdd / 500;
                    novaGaugeMax = 150;
                    novaCritChance = 35;
                    novaCritDamage = 100;

                    abilityName = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.GardenOfAvalon.AbilityName");
                    abilitySubName = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.GardenOfAvalon.AbilitySubName");
                    abilityDescription = starfarerBonus = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.GardenOfAvalon.AbilityDescription");
                    if (chosenStarfarer == 1)
                    {
                        starfarerBonus = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.GardenOfAvalon.AstralBonus");
                    }
                    else
                    {
                        starfarerBonus = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.GardenOfAvalon.UmbralBonus");
                    }

                    baseStats = "" +
                        $"{novaDamage} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.BaseHealStrength") +
                        $"\n{novaGaugeMax} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.BaseEnergyCost") +
                        $"\n" +
                        $"\n{Math.Round(novaDamage * (1 + novaDamageMod), 5)} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.HealStrength") +
                        $"\n{novaCritChance + novaCritChanceMod}% " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.CritChance") +
                        $"\n{Math.Round(novaCritDamage * (1 + novaCritDamageMod), 5)} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.CritHealStrength") +
                        $"\n{novaGaugeMax - novaChargeMod} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.EnergyCost");


                }
                if (chosenStellarNova == 5)//Edin Genesis Quasar
                {
                    novaDamage = baseNovaDamageAdd / 15;
                    novaGaugeMax = 180;
                    novaCritChance = 25;
                    novaCritDamage = (int)((baseNovaDamageAdd / 10) * 1.3);

                    abilityName = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.EdinGenesisQuasar.AbilityName");
                    abilitySubName = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.EdinGenesisQuasar.AbilitySubName");
                    abilityDescription = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.EdinGenesisQuasar.AbilityDescription");

                    if (chosenStarfarer == 1)
                    {
                        starfarerBonus = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.EdinGenesisQuasar.AstralBonus", baseNovaDamageAdd / 10);
                    }
                    else
                    {
                        starfarerBonus = LangHelper.GetTextValue("StellarNova.StellarNovaInfo.EdinGenesisQuasar.UmbralBonus", baseNovaDamageAdd / 10);
                    }

                    baseStats = "" +
                        $"{novaDamage} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.BaseDamage") +
                        $"\n{novaGaugeMax} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.BaseEnergyCost") +
                        $"\n" +
                        $"\n{Math.Round(novaDamage * (1 + novaDamageMod), 5)} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.AttackDamage") +
                        $"\n{novaCritChance + novaCritChanceMod}% " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.CritChance") +
                        $"\n{Math.Round(novaCritDamage * (1 + novaCritDamageMod), 5)} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.CritDamage") +
                        $"\n{novaGaugeMax - novaChargeMod} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.EnergyCost");

                    
                }
                abilityDescription = Wrap(abilityDescription, 85);
                starfarerBonus = Wrap(starfarerBonus, 85);
                //
                #endregion


                costumeChangeOpacity -= 0.1f;

                if (starfarerMenuActive)
                {
                    if (starfarerMenuUIOpacity > 1f)
                        starfarerMenuUIOpacity = 1f;
                    starfarerMenuUIOpacity += 0.1f;
                }
                else
                {
                    if (starfarerMenuUIOpacity < 0f)
                        starfarerMenuUIOpacity = 0f;
                    starfarerMenuUIOpacity -= 0.1f;
                }
                if (novaUIActive)
                {
                    if (novaUIOpacity > 1f)
                        novaUIOpacity = 1f;
                    novaUIOpacity += 0.1f;
                }
                else
                {
                    if (novaUIOpacity < 0f)
                        novaUIOpacity = 0f;
                    novaUIOpacity -= 0.1f;
                }
                if (descriptionY >= 40)
                {
                    descriptionY = 40;
                }
                if (!textVisible)
                {
                    if (descriptionOpacity < 0f)
                        descriptionOpacity = 0f;
                    descriptionOpacity -= 0.1f;
                    if (descriptionY <= -100)
                    {
                        descriptionY = -100;
                    }
                    descriptionY -= 10;

                }
                else
                {
                    if (descriptionOpacity > 1f)
                        descriptionOpacity = 1f;
                    descriptionOpacity += 0.1f;
                    if (descriptionY >= 0)
                    {
                        descriptionY = 0;
                    }
                    descriptionY += 10;
                }
                NovaCutInTimer--;
                if (NovaCutInVelocity > 0 && NovaCutInTimer > 100)
                {
                    NovaCutInOpacity += 0.1f;
                    NovaCutInVelocity -= 1;

                }
                if (NovaCutInVelocity < 20 && NovaCutInTimer < 40)
                {
                    NovaCutInOpacity -= 0.1f;
                    NovaCutInVelocity += 1;

                }

                if (WarriorOfLightActive == false && undertaleActive == true)
                {
                    undertaleActive = false;
                }
                if (undertalePrep)
                {
                    undertaleiFrames = 120;
                    Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().heartX = 380;
                    Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().heartY = 160;

                    undertalePrep = false;
                }
                if (damageTakenInUndertale == true)
                {
                    damageTakenInUndertale = false;
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_TakingDamage, Player.Center);

                }
                inWarriorOfLightFightTimer--;
                inNalhaunFightTimer--;
                inVagrantFightTimer--;
                inWarriorOfLightFightTimer--;
                inPenthFightTimer--;
                inArbiterFightTimer--;
                inCombat--;
                uniqueDialogueTimer--;
                if (radiance > 10)
                {
                    radiance = 10;
                }
                if (Player.GetModPlayer<StarsAbovePlayer>().AsphodeneHighlighted)
                {
                    AsphodeneX += AsphodeneXVelocity;

                    if (AsphodeneXVelocity > 0)
                    {
                        AsphodeneXVelocity--;
                    }
                    else
                    {
                        AsphodeneXVelocity = 0;
                    }

                }
                else
                {
                    AsphodeneX = 0;
                }
                if (Player.GetModPlayer<StarsAbovePlayer>().EridaniHighlighted)
                {
                    EridaniX += EridaniXVelocity;

                    if (EridaniXVelocity < 0)
                    {
                        EridaniXVelocity++;
                    }
                    else
                    {
                        EridaniXVelocity = 0;
                    }

                }
                else
                {
                    EridaniX = 0;
                }
                if (CosmicDestroyerGaugeVisibility > 0f)
                {
                    CosmicDestroyerGaugeVisibility -= 0.05f;

                }
                else
                {
                    CosmicDestroyerGaugeVisibility = 0f;
                }
                if (Player.GetModPlayer<StarsAbovePlayer>().starfarerDialogue == true)
                {


                    if (starfarerDialogueVisibility < 2f)
                    {
                        starfarerDialogueVisibility += 0.1f;
                    }
                    else
                    {
                        starfarerDialogueVisibility = 2f;
                    }

                }
                else
                {
                    if (starfarerDialogueVisibility > 0)
                    {
                        starfarerDialogueVisibility -= 0.3f;
                    }
                    else
                    {
                        starfarerDialogueVisibility = 0;
                    }
                }
                if (Player.GetModPlayer<StarsAbovePlayer>().VNDialogueActive == true)
                {


                    if (starfarerVNDialogueVisibility < 1f)
                    {
                        starfarerVNDialogueVisibility += 0.1f;
                    }
                    else
                    {
                        starfarerVNDialogueVisibility = 1f;
                    }

                }
                else
                {
                    if (starfarerVNDialogueVisibility > 0)
                    {
                        starfarerVNDialogueVisibility -= 0.1f;
                    }
                    else
                    {
                        starfarerVNDialogueVisibility = 0;
                    }
                }
                if (Player.GetModPlayer<StarsAbovePlayer>().stellarArray == true)
                {


                    if (stellarArrayVisibility < 2f)
                    {
                        stellarArrayVisibility += 0.1f;
                    }
                    else
                    {
                        stellarArrayVisibility = 2f;
                    }

                }
                else
                {
                    if (stellarArrayVisibility > 0)
                    {
                        stellarArrayVisibility -= 0.3f;
                    }
                    else
                    {
                        stellarArrayVisibility = 0;
                    }
                }
                if (Player.GetModPlayer<StarsAbovePlayer>().promptIsActive == true)
                {


                    if (promptVisibility < 2f)
                    {
                        promptVisibility += 0.1f;
                    }
                    else
                    {
                        promptVisibility = 2f;
                    }

                }
                else
                {
                    if (promptVisibility > 0)
                    {
                        promptVisibility -= 0.3f;
                    }
                    else
                    {
                        promptVisibility = 0;
                    }
                }
                if (Player.GetModPlayer<StarsAbovePlayer>().stellarArray == true)
                {
                    if (stellarArrayMoveIn > 0f)
                    {
                        stellarArrayMoveIn -= 2f;
                    }
                    else
                    {
                        stellarArrayMoveIn = 0f;
                    }

                }
                else
                {
                    if (stellarArrayMoveIn < 0f)
                    {
                        stellarArrayMoveIn += 2f;
                    }
                    else
                    {
                        stellarArrayMoveIn = 0f;
                    }
                }
                if (Player.GetModPlayer<StarsAbovePlayer>().promptVisibility > 0.5)
                {
                    if (promptMoveIn > 0f)
                    {
                        promptMoveIn -= 2f;
                    }
                    else
                    {
                        promptMoveIn = 0f;
                    }

                }
                else
                {
                    if (promptMoveIn < 0f)
                    {
                        promptMoveIn += 2f;
                    }
                    else
                    {
                        promptMoveIn = 0f;
                    }
                }
                #region dialogue
                //Dialogue code ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||| Dialogue code!
                //                                                                           | This is the dialogue limit!

                if (chosenDialogue != 0)
                {
                    StarsAboveDialogueSystem.SetupDialogueSystem(chosenStarfarer, ref chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, Player, Mod);


                }

                #endregion

                #region VNDialogue
                //Set up the VN Dialogue.
                if (sceneID != -1 && VNDialogueActive)//All this boils down to moving the crazy amount of dialogue lines outside of StarsAbovePlayer. Future me knows that you could've just made another StarsAbovePlayer. Oops.
                {
                    sceneLength = (int)VNScenes.SetupVNSystem(sceneID, sceneProgression)[0];
                    VNCharacter1 = (string)VNScenes.SetupVNSystem(sceneID, sceneProgression)[6];//The active character. If there is a second character, this character will be drawn on the left. If not, they are drawn in the middle.
                    VNCharacter1Pose = (int)VNScenes.SetupVNSystem(sceneID, sceneProgression)[7];
                    VNCharacter1Expression = (int)VNScenes.SetupVNSystem(sceneID, sceneProgression)[8]; //The expression needs the pose to align correctly.
                    VNCharacter2 = (string)VNScenes.SetupVNSystem(sceneID, sceneProgression)[9];
                    if (VNCharacter2 != "None")//If there is a 2nd character, set up their pose and expression.
                    {
                        VNCharacter2Pose = (int)VNScenes.SetupVNSystem(sceneID, sceneProgression)[10];
                        VNCharacter2Expression = (int)VNScenes.SetupVNSystem(sceneID, sceneProgression)[11];
                    }
                    else
                    {
                        VNCharacter2Pose = -1;
                        VNCharacter2Expression = -1;
                    }
                    VNDialogueVisibleName = (string)VNScenes.SetupVNSystem(sceneID, sceneProgression)[12];//The name that shows up in the text box.

                    VNDialogueThirdOption = (bool)VNScenes.SetupVNSystem(sceneID, sceneProgression)[14];//If the third option is available.
                    
                    if (sceneProgression > sceneLength)
                    {
                        VNDialogueActive = false;
                        VNDialogueChoiceActive = false;
                        sceneID = -1;
                        sceneProgression = 0;
                        sceneLength = 0;
                        dialogue = "";
                        dialogueScrollTimer = 0;
                        dialogueScrollNumber = 0;
                        
                        VNDialogueChoice1 = "";
                        VNDialogueChoice2 = "";
                        VNDialogueThirdOption = false;
                    }
                    dialogue = Wrap((string)VNScenes.SetupVNSystem(sceneID, sceneProgression)[13], 50);
                    //animatedDialogue = dialogue.Substring(0, dialogueScrollNumber);
                }
                #endregion
                animatedDialogue = dialogue.Substring(0, dialogueScrollNumber);//Dialogue increment magic
                //Boss kill prompts
                stellarGaugeMax = 5;
                SetupStarfarerOutfit();

                if (Main.worldID == firstJoinedWorld || !enableWorldLock)
                {
                  /*  if (SubworldSystem.IsActive<Observatory>() && observatoryDialogue == 0)
                    {
                        observatoryDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;

                    }
                    if (observatoryDialogue == 2 && cosmicVoyageDialogue == 0)
                    {
                        cosmicVoyageDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;

                    }*/

                    if (NPC.downedSlimeKing && slimeDialogue == 0)
                    {
                        slimeDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                        if (Main.expertMode)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                            NewStellarArrayAbility = true;
                        }
                    }
                    if (NPC.downedBoss1 && eyeDialogue == 0)
                    {
                        eyeDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                        NewStellarArrayAbility = true;


                    }
                    if (NPC.downedBoss2 && corruptBossDialogue == 0)
                    {
                        corruptBossDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                        NewStellarArrayAbility = true;

                    }
                    if (NPC.downedQueenBee && BeeBossDialogue == 0)
                    {
                        BeeBossDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                        NewStellarArrayAbility = true;

                    }
                    if (NPC.downedBoss3 && SkeletonDialogue == 0)
                    {
                        SkeletonDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                        NewStellarArrayAbility = true;

                    }
                    if (NPC.downedDeerclops && DeerclopsDialogue == 0)
                    {
                        DeerclopsDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;

                    }
                    if (Main.hardMode && WallOfFleshDialogue == 0)//Hardmode
                    {
                        WallOfFleshDialogue = 1;

                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                        NewStellarArrayAbility = true;
                    }
                    if (WallOfFleshWeaponDialogue == 2 && ForceWeaponDialogue == 0)//Hardmode
                    {
                        ForceWeaponDialogue = 1;

                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                        //if (Main.netMode != NetmodeID.Server){Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247);}
                    }
                    if (GolemWeaponDialogue == 2 && GenocideWeaponDialogue == 0)//Hardmode
                    {
                        GenocideWeaponDialogue = 1;

                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                        //if (Main.netMode != NetmodeID.Server){Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247);}
                    }
                    if (CorruptBossWeaponDialogue == 2 && TakodachiWeaponDialogue == 0)//Hardmode
                    {
                        TakodachiWeaponDialogue = 1;

                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                        //if (Main.netMode != NetmodeID.Server){Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247);}
                    }
                    if (NPC.downedMechBoss1 && TwinsDialogue == 0)//The Twins
                    {
                        TwinsDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                        if (bloomingflames == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                            NewStellarArrayAbility = true;
                        }


                    }
                    if (SkeletronPrimeDialogue == 2 && SkyStrikerWeaponDialogue == 0)//Hardmode
                    {
                        SkyStrikerWeaponDialogue = 1;

                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                        //if (Main.netMode != NetmodeID.Server){Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247);}
                    }
                    if (LunaticCultistWeaponDialogue == 2 && TwinStarsWeaponDialogue == 0)//Hardmode
                    {
                        TwinStarsWeaponDialogue = 1;

                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                        //if (Main.netMode != NetmodeID.Server){Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247);}
                    }
                    //Dialogue for Calamity Mod bosses.


                    if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
                    {
                        if ((bool)calamityMod.Call("GetBossDowned", "desertscourge") && desertscourgeDialogue == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            desertscourgeDialogue = 1;
                        }
                        if ((bool)calamityMod.Call("GetBossDowned", "crabulon") && crabulonDialogue == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            crabulonDialogue = 1;
                        }
                        if ((bool)calamityMod.Call("GetBossDowned", "hivemind") && hivemindDialogue == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            hivemindDialogue = 1;
                        }
                        if ((bool)calamityMod.Call("GetBossDowned", "perforator") && perforatorDialogue == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            perforatorDialogue = 1;
                        }
                        if ((bool)calamityMod.Call("GetBossDowned", "slimegod") && slimegodDialogue == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            slimegodDialogue = 1;
                        }
                        //Hardmode
                        if ((bool)calamityMod.Call("GetBossDowned", "cryogen") && cryogenDialogue == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            cryogenDialogue = 1;
                        }
                        if ((bool)calamityMod.Call("GetBossDowned", "aquaticscourge") && aquaticscourgeDialogue == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            aquaticscourgeDialogue = 1;
                        }
                        if ((bool)calamityMod.Call("GetBossDowned", "brimstoneelemental") && brimstoneelementalDialogue == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            brimstoneelementalDialogue = 1;
                        }
                        if ((bool)calamityMod.Call("GetBossDowned", "calamitas") && calamitasDialogue == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            calamitasDialogue = 1;
                        }
                        if ((bool)calamityMod.Call("GetBossDowned", "anahitaleviathan") && leviathanDialogue == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            leviathanDialogue = 1;
                        }
                        if ((bool)calamityMod.Call("GetBossDowned", "astrumaureus") && astrumaureusDialogue == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            astrumaureusDialogue = 1;
                        }
                        if ((bool)calamityMod.Call("GetBossDowned", "plaguebringergoliath") && plaguebringerDialogue == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            plaguebringerDialogue = 1;
                        }
                        if ((bool)calamityMod.Call("GetBossDowned", "ravager") && ravagerDialogue == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            ravagerDialogue = 1;
                        }
                        if ((bool)calamityMod.Call("GetBossDowned", "astrumdeus") && astrumdeusDialogue == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            astrumdeusDialogue = 1;
                        }
                    }

                    if (NPC.downedQueenSlime && QueenSlimeDialogue == 0)
                    {
                        QueenSlimeDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;

                    }
                    if (DownedBossSystem.downedNalhaun && nalhaunDialogue == 0)
                    {
                        nalhaunDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;

                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(Language.GetTextValue("You have acquired a new Stellar Nova!"), 190, 100, 247); }
                        NewStellarNova = true;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                        NewStellarArrayAbility = true;


                    }
                    if (DownedBossSystem.downedPenth && penthDialogue == 0)
                    {
                        penthDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;

                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(Language.GetTextValue("You have acquired a new Stellar Nova!"), 190, 100, 247); }
                        NewStellarNova = true;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                        NewStellarArrayAbility = true;


                    }
                    if (DownedBossSystem.downedArbiter && arbiterDialogue == 0)
                    {
                        arbiterDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;

                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(Language.GetTextValue("You have acquired a new Stellar Nova!"), 190, 100, 247); }
                        NewStellarNova = true;
                        if (Main.expertMode)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                            NewStellarArrayAbility = true;
                        }



                    }
                    if (DownedBossSystem.downedTsuki && tsukiyomiDialogue == 0)
                    {
                        tsukiyomiDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;





                    }


                    if (NPC.downedMechBoss2 && DestroyerDialogue == 0)//The Destroyer
                    {
                        DestroyerDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                        if (bloomingflames == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                            NewStellarArrayAbility = true;
                        }


                    }
                    if (NPC.downedMechBoss3 && SkeletronPrimeDialogue == 0)//Skeletron Prime
                    {
                        SkeletronPrimeDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                        if (bloomingflames == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                            NewStellarArrayAbility = true;
                        }


                    }
                    if (SkeletronPrimeDialogue == 2 && TwinsDialogue == 2 && DestroyerDialogue == 2 && AllMechsDefeatedDialogue == 0)//All Mech Bosses Defeated + Dialogue read
                    {
                        AllMechsDefeatedDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;




                    }
                    if (NPC.downedPlantBoss && PlanteraDialogue == 0)
                    {
                        PlanteraDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                        NewStellarArrayAbility = true;
                        //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You have acquired a new Stellar Nova!"), 190, 100, 247);}

                    }
                    if (NPC.downedGolemBoss && GolemDialogue == 0)
                    {
                        GolemDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                        if (Main.expertMode)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                            NewStellarArrayAbility = true;
                        }

                    }
                    if (NPC.downedEmpressOfLight && EmpressDialogue == 0)
                    {
                        EmpressDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;


                    }
                    if (NPC.downedAncientCultist && CultistDialogue == 0)
                    {
                        CultistDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                        if (Main.expertMode)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                            NewStellarArrayAbility = true;
                        }
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(Language.GetTextValue("You have acquired a new Stellar Nova!"), 190, 100, 247); }
                        NewStellarNova = true;

                    }
                    if (NPC.downedMoonlord && MoonLordDialogue == 0)
                    {
                        MoonLordDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(Language.GetTextValue("The sky becomes heavy with overwhelming Light..."), 255, 225, 107); }


                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;

                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                        NewStellarArrayAbility = true;


                    }
                    if (NPC.downedFishron && DukeFishronDialogue == 0)
                    {
                        DukeFishronDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                    }
                    if (DownedBossSystem.downedWarrior && WarriorOfLightDialogue == 0)
                    {
                        WarriorOfLightDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(Language.GetTextValue("The Light flooding this world has been cleansed!"), 255, 225, 107); }
                        if (ModLoader.TryGetMod("BossChecklist", out Mod BossChecklist))
                        {


                        }


                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;
                        if (BossChecklist != null)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(Language.GetTextValue("The Boss Checklist updates to reveal a hidden foe..!"), 141, 155, 180); }

                        }

                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                        NewStellarArrayAbility = true;


                    }
                    if (DownedBossSystem.downedVagrant && vagrantDialogue == 0)
                    {
                        vagrantDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(Language.GetTextValue("Stellar Novas have been unlocked!"), 255, 225, 107); }

                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;

                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                        NewStellarArrayAbility = true;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(Language.GetTextValue("You have acquired a new Stellar Nova!"), 190, 100, 247); }
                        NewStellarNova = true;


                    }
                    if (NPC.downedBoss1 && NPC.downedSlimeKing && NPC.downedBoss2 && NPC.downedBoss3 && NPC.downedQueenBee && NPC.downedQueenSlime && NPC.downedEmpressOfLight && Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss && NPC.downedFishron && NPC.downedMoonlord && AllVanillaBossesDefeatedDialogue == 0)
                    {
                        AllVanillaBossesDefeatedDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;

                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                        NewStellarArrayAbility = true;


                    }
                    if (NPC.downedBoss1 && NPC.downedSlimeKing && NPC.downedBoss2 && NPC.downedBoss3 && NPC.downedQueenBee && NPC.downedQueenSlime && NPC.downedEmpressOfLight && Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss && NPC.downedFishron && NPC.downedMoonlord && DownedBossSystem.downedWarrior && Main.expertMode && EverythingDefeatedDialogue == 0)
                    {
                        //Expert mode only
                        EverythingDefeatedDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;

                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                        NewStellarArrayAbility = true;


                    }

                    if (Player.ZoneUnderworldHeight && SkeletonWeaponDialogue == 2 && HellWeaponDialogue == 0)
                    {
                        HellWeaponDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;

                    }
                    //Boss Spawn items
                    if (nalhaunBossItemDialogue == 0 && (SkeletronPrimeDialogue == 2 || TwinsDialogue == 2 || DestroyerDialogue == 2) && vagrantDialogue == 2)
                    {
                        nalhaunBossItemDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 151, 255, 90); }

                    }
                    if (penthBossItemDialogue == 0 && PlanteraDialogue == 2 && vagrantDialogue == 2)
                    {
                        penthBossItemDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 151, 255, 90); }

                    }
                    if (arbiterBossItemDialogue == 0 && GolemDialogue == 2 && vagrantDialogue == 2)
                    {
                        arbiterBossItemDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 151, 255, 90); }

                    }
                    if (warriorBossItemDialogue == 0 && MoonLordDialogue == 2 && vagrantDialogue == 2)
                    {
                        warriorBossItemDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 151, 255, 90); }

                    }
                    //Zone specific weapons do not have delay
                    if (Player.ZoneHallow && GoldWeaponDialogue == 0)
                    {
                        GoldWeaponDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;

                        return;

                    }
                    if (Player.ZoneGraveyard && FarewellWeaponDialogue == 0)
                    {
                        FarewellWeaponDialogue = 1;
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                        NewDiskDialogue = true;

                        return;

                    }
                    if (WeaponDialogueTimer <= 0)//7200 = 2 min in between 
                    {//The order of these should not matter.
                        if (SkeletonDialogue == 2 && SkeletonWeaponDialogue == 0)
                        {
                            SkeletonWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                            return;

                        }
                        if (tsukiyomiDialogue == 2 && ArchitectWeaponDialogue == 0)
                        {
                            ArchitectWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;


                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;


                        }
                        if (ArchitectWeaponDialogue == 2 && CosmicDestroyerWeaponDialogue == 0)
                        {
                            CosmicDestroyerWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;


                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                            return;



                        }
                        if (MurasamaWeaponDialogue == 0 && NPC.downedEmpressOfLight && Main.masterMode && DownedBossSystem.downedVagrant)
                        {
                            MurasamaWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                            return;
                        }
                        if (MercyWeaponDialogue == 0 && NPC.downedGolemBoss)
                        {
                            MercyWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                            return;
                        }
                        if (SakuraWeaponDialogue == 0 && NPC.downedEmpressOfLight)
                        {
                            SakuraWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                            return;
                        }
                        if (EternalWeaponDialogue == 0 && NPC.downedMoonlord)
                        {
                            EternalWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                            return;
                        }
                        if (DaemonWeaponDialogue == 0 && NPC.downedMoonlord)
                        {
                            DaemonWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                            return;
                        }
                        if (OzmaWeaponDialogue == 0 && NPC.downedAncientCultist)
                        {
                            OzmaWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                            return;
                        }
                        if (UrgotWeaponDialogue == 0 && NPC.downedQueenSlime)
                        {
                            UrgotWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                            return;
                        }
                        if (BloodWeaponDialogue == 0 && NPC.downedHalloweenKing)
                        {
                            BloodWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                            return;
                        }
                        if (MorningStarWeaponDialogue == 0 && NPC.downedDeerclops)
                        {
                            MorningStarWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                            return;
                        }
                        if (VirtueWeaponDialogue == 0 && NPC.downedMoonlord)
                        {
                            VirtueWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                            return;
                        }
                        if (QueenSlimeWeaponDialogue == 0 && NPC.downedQueenSlime)
                        {
                            QueenSlimeWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                            return;
                        }
                        if (NeedlepointWeaponDialogue == 0 && NPC.downedEmpressOfLight)
                        {
                            NeedlepointWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                            return;
                        }
                        if (eyeDialogue == 2 && EyeBossWeaponDialogue == 0)
                        {
                            EyeBossWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                            return;

                        }
                        if (corruptBossDialogue == 2 && CorruptBossWeaponDialogue == 0)
                        {
                            CorruptBossWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                            return;

                        }
                        if (nalhaunDialogue == 2 && NalhaunWeaponDialogue == 0)
                        {
                            NalhaunWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (vagrantDialogue == 2 && VagrantWeaponDialogue == 0)
                        {
                            VagrantWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (BeeBossDialogue == 2 && QueenBeeWeaponDialogue == 0)
                        {
                            QueenBeeWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (SkeletonWeaponDialogue == 2 && OceanWeaponDialogue == 0 && seenBeachBiome)
                        {
                            OceanWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (SkeletonWeaponDialogue == 2 && MiseryWeaponDialogue == 0)
                        {
                            MiseryWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (slimeDialogue == 2 && KingSlimeWeaponDialogue == 0)
                        {
                            KingSlimeWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (WallOfFleshDialogue == 2 && WallOfFleshWeaponDialogue == 0)
                        {
                            WallOfFleshWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;
                        }
                        if (WallOfFleshWeaponDialogue == 2 && LumaWeaponDialogue == 0)
                        {
                            LumaWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;
                        }
                        if ((TwinsDialogue == 2 || DestroyerDialogue == 2 || SkeletronPrimeDialogue == 2) && MechBossWeaponDialogue == 0)
                        {
                            MechBossWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;
                        }
                        if (TwinsDialogue == 2 && DestroyerDialogue == 2 && SkeletronPrimeDialogue == 2 && AllMechBossWeaponDialogue == 0 && MechBossWeaponDialogue == 2 && AllMechBossWeaponDialogue == 0)
                        {
                            AllMechBossWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (MechBossWeaponDialogue == 2 && HullwroughtWeaponDialogue == 0)
                        {
                            HullwroughtWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (HullwroughtWeaponDialogue == 2 && MonadoWeaponDialogue == 0)
                        {
                            MonadoWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }

                        if (PlanteraDialogue == 2 && PlanteraWeaponDialogue == 0)
                        {
                            PlanteraWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (NPC.downedChristmasIceQueen && FrostMoonWeaponDialogue == 0)
                        {
                            FrostMoonWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (GolemDialogue == 2 && GolemWeaponDialogue == 0)
                        {
                            GolemWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (penthDialogue == 2 && PenthesileaWeaponDialogue == 0)
                        {
                            PenthesileaWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (PenthesileaWeaponDialogue == 2 && MuseWeaponDialogue == 0)
                        {
                            MuseWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (PlanteraWeaponDialogue == 2 && KifrosseWeaponDialogue == 0)
                        {
                            KifrosseWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (arbiterDialogue == 2 && ArbitrationWeaponDialogue == 0)
                        {
                            ArbitrationWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (ArbitrationWeaponDialogue == 2 && ClaimhWeaponDialogue == 0)
                        {
                            ClaimhWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (DukeFishronDialogue == 2 && DukeFishronWeaponDialogue == 0)
                        {
                            DukeFishronWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (CultistDialogue == 2 && LunaticCultistWeaponDialogue == 0)
                        {
                            LunaticCultistWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (MoonLordDialogue == 2 && MoonLordWeaponDialogue == 0)
                        {
                            MoonLordWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (MoonLordWeaponDialogue == 2 && ShadowlessWeaponDialogue == 0)
                        {
                            ShadowlessWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (WarriorOfLightDialogue == 2 && WarriorWeaponDialogue == 0)
                        {
                            WarriorWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (WarriorOfLightDialogue == 2 && RedMageWeaponDialogue == 0)
                        {
                            RedMageWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (WarriorOfLightDialogue == 2 && BlazeWeaponDialogue == 0)
                        {
                            BlazeWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (WarriorOfLightDialogue == 2 && PickaxeWeaponDialogue == 0)
                        {
                            PickaxeWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (AllMechsDefeatedDialogue == 2 && HardwareWeaponDialogue == 0)
                        {
                            HardwareWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (LunaticCultistWeaponDialogue == 2 && CatalystWeaponDialogue == 0)
                        {
                            CatalystWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (GolemWeaponDialogue == 2 && SilenceWeaponDialogue == 0)
                        {
                            SilenceWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        if (MoonLordWeaponDialogue == 2 && SoulWeaponDialogue == 0)
                        {
                            SoulWeaponDialogue = 1;
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.DiskReady"), 241, 255, 180); }
                            NewDiskDialogue = true;
                            WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                            return;

                        }
                        
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                    }



                    //Stellar Array upgrades
                    baseNovaDamageAdd = 2000;
                    if (NPC.downedSlimeKing && Main.expertMode == true)
                    {

                        if (aquaaffinity == 0)
                        {
                            aquaaffinity = 1;
                        }
                    }
                    if (NPC.downedBoss1)
                    {

                        if (starshower == 0)
                        {
                            starshower = 1;
                        }
                    }
                    if (NPC.downedBoss2) // Eater/Brain
                    {
                        if (ironskin == 0)
                        {
                            ironskin = 1;
                        }
                    }
                    if (NPC.downedQueenBee)
                    {
                        if (evasionmastery == 0)
                        {
                            evasionmastery = 1;
                        }
                    }
                    if (NPC.downedBoss3) // Skeletron
                    {
                        if (inneralchemy == 0)
                        {
                            inneralchemy = 1;
                        }
                    }
                    if (Main.hardMode)
                    {

                        if (bonus100hp == 0)
                        {
                            bonus100hp = 1;
                        }
                    }
                    if (DownedBossSystem.downedVagrant)
                    {

                        novaGaugeUnlocked = true;
                        if (theofania == 0)
                        {
                            theofania = 1;
                        }
                    }
                    /*if(novaGaugeUnlocked && Main.hardMode)
                    {
                        DownedBossSystem.downedVagrant = true;
                    }*/
                    if (DownedBossSystem.downedNalhaun)
                    {
                        if (butchersdozen == 0)
                        {
                            butchersdozen = 1;
                        }
                        if (laevateinn == 0)
                        {
                            laevateinn = 1;
                        }
                    }
                    if (DownedBossSystem.downedPenth)
                    {
                        if (mysticforging == 0)
                        {
                            mysticforging = 1;
                        }
                        if (gardenofavalon == 0)
                        {
                            gardenofavalon = 1;
                        }
                    }
                    if (NPC.downedMechBossAny)
                    {
                        baseNovaDamageAdd = 2150;
                        if (bloomingflames == 0)
                        {
                            bloomingflames = 1;
                        }


                    }
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                    {
                        
                        baseNovaDamageAdd = 2350;
                        if (astralmantle == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                            NewStellarArrayAbility = true;
                            astralmantle = 1;
                        }
                    }

                    if (NPC.downedPlantBoss)
                    {
                        baseNovaDamageAdd = 3050;
                        if (afterburner == 0)
                        {
                            afterburner = 1;
                        }

                    }
                    if (NPC.downedHalloweenKing)
                    {

                        if (livingdead == 0)
                        {
                            livingdead = 1;
                        }
                    }
                    if (NPC.downedChristmasIceQueen)
                    {
                        if (hikari == 0)
                        {
                            hikari = 1;
                        }
                    }
                    if (NPC.downedGolemBoss)
                    {
                        baseNovaDamageAdd = 4000;
                    }
                    if (NPC.downedGolemBoss && Main.expertMode == true)
                    {

                        if (weaknessexploit == 0)
                        {
                            weaknessexploit = 1;
                        }
                    }
                    if (NPC.downedAncientCultist)
                    {
                        baseNovaDamageAdd = 5200;
                        if (edingenesisquasar == 0)
                        {
                            edingenesisquasar = 1;
                        }
                    }
                    if (NPC.downedAncientCultist && Main.expertMode == true)
                    {
                        if (celestialevanesence == 0)
                        {
                            celestialevanesence = 1;
                        }
                    }
                    if (NPC.downedMoonlord)
                    {
                        baseNovaDamageAdd = 6600;
                        if (umbralentropy == 0)
                        {
                            umbralentropy = 1;
                        }

                    }
                    if (DownedBossSystem.downedArbiter)
                    {
                        if (kiwamiryuken == 0)
                        {
                            kiwamiryuken = 1;
                        }


                    }
                    if (DownedBossSystem.downedArbiter && Main.expertMode)
                    {
                        if (flashfreeze == 0)
                        {

                            flashfreeze = 1;
                        }




                    }

                    if (NPC.downedMoonlord && Main.expertMode == true)
                    {
                        if (keyofchronology == 0)
                        {
                            keyofchronology = 1;
                        }
                    }
                    if (DownedBossSystem.downedWarrior && DownedBossSystem.downedVagrant && DownedBossSystem.downedPenth && DownedBossSystem.downedNalhaun && DownedBossSystem.downedArbiter)
                    {


                        if (artofwar == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                            NewStellarArrayAbility = true;
                            artofwar = 1;
                        }

                    }
                    if (DownedBossSystem.downedWarrior && DownedBossSystem.downedVagrant && DownedBossSystem.downedPenth && DownedBossSystem.downedNalhaun && DownedBossSystem.downedArbiter && Main.expertMode == true)
                    {


                        if (aprismatism == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                            NewStellarArrayAbility = true;
                            aprismatism = 1;
                        }

                    }
                    if (DownedBossSystem.downedWarrior)
                    {
                        baseNovaDamageAdd = 17550;
                        if (avataroflight == 0)
                        {
                            avataroflight = 1;
                        }



                    }
                    if (NPC.downedBoss1 && NPC.downedSlimeKing && NPC.downedBoss2 && NPC.downedBoss3 && NPC.downedQueenBee && Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss && NPC.downedFishron && NPC.downedMoonlord)
                    {
                        if (beyondinfinity == 0)
                        {
                            beyondinfinity = 1;
                        }

                    }
                    if (NPC.downedBoss1 && NPC.downedSlimeKing && NPC.downedBoss2 && NPC.downedBoss3 && NPC.downedQueenBee && Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss && NPC.downedFishron && NPC.downedMoonlord && DownedBossSystem.downedWarrior && DownedBossSystem.downedVagrant && DownedBossSystem.downedPenth && DownedBossSystem.downedNalhaun && DownedBossSystem.downedArbiter)
                    {
                        if (beyondtheboundary == 0)
                        {
                            if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                            NewStellarArrayAbility = true;
                            beyondtheboundary = 1;
                        }
                    }
                    if (NPC.downedBoss1 && NPC.downedSlimeKing && NPC.downedBoss2 && NPC.downedBoss3 && NPC.downedQueenBee && Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss && NPC.downedFishron && NPC.downedMoonlord && DownedBossSystem.downedWarrior && Main.expertMode == true)
                    {
                        if (unbridledradiance == 0)
                        {
                            unbridledradiance = 1;
                        }
                    }
                    // 

                    if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod1))
                    {
                        if ((bool)calamityMod.Call("GetBossDowned", "providence"))
                        {
                            baseNovaDamageAdd = 20200;
                        }
                        if ((bool)calamityMod.Call("GetBossDowned", "allsentinel"))
                        {
                            baseNovaDamageAdd = 39000;
                        }
                        if ((bool)calamityMod.Call("GetBossDowned", "devourerofgods"))
                        {
                            stellarGaugeMax++;
                            baseNovaDamageAdd = 62500;
                            if (stellarGaugeUpgraded != 1)
                            {
                                if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(Language.GetTextValue("The Stellar Array reaches new heights!"), 255, 0, 115); }
                                stellarGaugeUpgraded = 1;
                            }
                        }
                        if ((bool)calamityMod.Call("GetBossDowned", "yharon"))
                        {
                            baseNovaDamageAdd = 73000;
                        }
                        if ((bool)calamityMod.Call("GetBossDowned", "supremecalamitas"))
                        {
                            baseNovaDamageAdd = 97500;
                        }
                    }

                }
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                WeaponDialogueTimer--;



                if (stellarGauge > stellarGaugeMax)
                {
                    Player.AddBuff(BuffType<StellarOverload>(), 2);
                }
                if (chosenStarfarerEffect == true)
                {
                    activateShockwaveEffect = true;

                    SoundEngine.PlaySound(StarsAboveAudio.SFX_StarfarerChosen, Player.Center);
                    Dust dust;
                    for (int d = 0; d < 50; d++)
                    {
                        dust = Main.dust[Terraria.Dust.NewDust(Player.Center, 0, 0, 181, 0f + Main.rand.Next(-22, 22), 0f + Main.rand.Next(-22, 22), 0, new Color(255, 255, 255), 1f)];
                    }

                    chosenStarfarerEffect = false;
                }
                if (activateShockwaveEffect)
                {
                    rippleCount = 4;
                    rippleSpeed = 60;
                    rippleSize = 35;
                    activateShockwaveEffect = false;
                    if (Main.netMode != NetmodeID.Server && !Filters.Scene["Shockwave"].IsActive())
                    {
                        Filters.Scene.Activate("Shockwave", Player.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(Player.Center);
                    }
                    shockwaveProgress = 0;
                }
                if (activateUltimaShockwaveEffect)
                {
                    rippleCount = 1;
                    rippleSpeed = 15;
                    rippleSize = 3;
                    activateUltimaShockwaveEffect = false;
                    if (Main.netMode != NetmodeID.Server && !Filters.Scene["Shockwave"].IsActive())
                    {
                        Filters.Scene.Activate("Shockwave", Player.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(Player.Center);
                    }
                    shockwaveProgress = 0;
                }
                if (activateBlackHoleShockwaveEffect)
                {
                    rippleCount = 8;
                    rippleSpeed = 10;
                    rippleSize = 65;
                    activateBlackHoleShockwaveEffect = false;
                    if (Main.netMode != NetmodeID.Server && !Filters.Scene["Shockwave"].IsActive())
                    {
                        Filters.Scene.Activate("Shockwave", Player.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(BlackHolePosition);
                    }
                    shockwaveProgress = 0;
                }
                if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
                {
                    float progress = (shockwaveProgress) / 140f;
                    Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f));
                }
                if (shockwaveProgress >= 480)
                {
                    Filters.Scene.Deactivate("Shockwave");

                }
                shockwaveProgress++;

                //Nova Gauge charging.
                StellarNovaEnergy();

                base.PreUpdate();
            }
            playerMousePos = Main.MouseWorld;
            if (lookAtTsukiyomi)
            {
                tsukiyomiCameraFloat += 0.1f;
            }
            else
            {
                tsukiyomiCameraFloat -= 0.1f;
            }

            base.PreUpdate();
        }
        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (aprismatism == 2 && aprismatismCooldown <= 0)
            {
                //Projectile.NewProjectile(source, Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ProjectileType<StarfarerFollowUp>(), damage * 2, knockback, Player.whoAmI);
                //aprismatismCooldown = 240;
            }
            return base.Shoot(item, source, position, velocity, type, damage, knockback);
        }
        
        
        public override void OnConsumeAmmo(Item weapon, Item ammo)
        {
            
            base.OnConsumeAmmo(weapon, ammo);
        }
        public override void PostUpdateRunSpeeds()
        {
            if (evasionmastery == 2)
            {
                Player.maxRunSpeed *= 1.25f;
                Player.accRunSpeed *= 1.25f;
                //player.runAcceleration += 1.4f;

            }
            if (hikari == 2)
            {
                //player.maxRunSpeed += 0.1f;
            }
            if (Player.HasBuff(BuffType<VitalitySong>()))
            {
                Player.maxRunSpeed *= 1.15f;
                //Player.runAcceleration += 1.15f;
            }
            if (Player.HasBuff(BuffType<CatalyzedBlade>()))
            {
                Player.maxRunSpeed *= 1.1f;
                Player.accRunSpeed *= 1.1f;
            }
            if (Player.HasBuff(BuffType<Bedazzled>()))
            {
                Player.maxRunSpeed *= 1.1f;
                Player.accRunSpeed *= 1.1f;
            }
            if (Player.HasBuff(BuffType<AmmoRecycle>()))
            {
                Player.maxRunSpeed *= 1.5f;
                Player.runAcceleration += 1.5f;
            }
            if (Player.HasBuff(BuffType<GenocideBuff>()))
            {
                Player.maxRunSpeed *= 1.25f;
                Player.accRunSpeed *= 1.25f;
            }
            if (Player.HasBuff(BuffType<CallOfTheVoid1>()))
            {
                Player.maxRunSpeed *= 1.05f;
                Player.accRunSpeed *= 1.05f;
            }
            if (Player.HasBuff(BuffType<CallOfTheVoid2>()))
            {
                Player.maxRunSpeed *= 1.10f;
                Player.accRunSpeed *= 1.10f;
            }
            if (Player.HasBuff(BuffType<CallOfTheVoid3>()))
            {
                Player.maxRunSpeed *= 1.15f;
                Player.accRunSpeed *= 1.15f;
            }
            if (Player.HasBuff(BuffType<CallOfTheVoid4>()))
            {
                Player.maxRunSpeed *= 1.20f;
                Player.accRunSpeed *= 1.20f;
            }
            if (Player.HasBuff(BuffType<Buffs.SakuraVengeance.SakuraHeavenBuff>()))
            {
                Player.maxRunSpeed *= 1.20f;
                Player.accRunSpeed *= 1.20f;
            }
            if (Player.HasBuff(BuffType<Buffs.SakuraVengeance.ElementalChaos>()))
            {
                Player.maxRunSpeed *= 1.20f;
                Player.accRunSpeed *= 1.20f;
            }
            base.PostUpdateRunSpeeds();
        }
        public override void PostUpdate()
        {

            //These trigger Starfarer prompts

            //Debuffs / buffs(maybe later?)
            lookAtWarrior = false;
            lookAtTsukiyomi = false;
            if (!disablePromptsBuffs)
            {
                if (Player.HasBuff(BuffID.OnFire))
                {

                    starfarerPromptActive("onFire");
                }
                if (Player.HasBuff(BuffID.Poisoned))
                {

                    starfarerPromptActive("onPoison");
                }
                if (Player.HasBuff(BuffID.Bleeding))
                {

                    starfarerPromptActive("onBleeding");
                }
                if (Player.HasBuff(BuffID.Ichor))
                {

                    starfarerPromptActive("onIchor");
                }
                if (Player.HasBuff(BuffID.Silenced))
                {

                    starfarerPromptActive("onSilence");
                }
                if (Player.HasBuff(BuffID.Cursed))
                {

                    starfarerPromptActive("onCurse");
                }
                if (Player.HasBuff(BuffID.Frozen))
                {

                    starfarerPromptActive("onFrozen");
                }
                if (Player.HasBuff(BuffID.Webbed))
                {

                    starfarerPromptActive("onWebbed");
                }
                if (Player.HasBuff(BuffID.Frostburn))
                {

                    starfarerPromptActive("onFrostburn");
                }
                if (Player.HasBuff(BuffID.Stoned))
                {

                    starfarerPromptActive("onStoned");
                }
                if (Player.HasBuff(BuffID.Burning))
                {

                    starfarerPromptActive("onBurning");
                }
                if (Player.HasBuff(BuffID.Suffocation))
                {

                    starfarerPromptActive("onSuffocation");
                }
                if (Player.HasBuff(BuffID.Confused))
                {

                    starfarerPromptActive("onConfusion");
                }
            }

            if (Player.breath <= 0)
            {
                starfarerPromptActive("onDrowning");

            }

            //Bosses

            if (NPC.AnyNPCs(NPCID.EyeofCthulhu) && !seenEyeOfCthulhu)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onEyeOfCthulhu");
                seenUnknownBossTimer = 300;

            }
            if (NPC.AnyNPCs(NPCID.KingSlime) && !seenKingSlime)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onKingSlime");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCID.EaterofWorldsHead) && !seenEaterOfWorlds)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onEaterOfWorlds");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCID.BrainofCthulhu) && !seenBrainOfCthulhu)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onBrainOfCthulhu");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCID.QueenBee) && !seenQueenBee)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onQueenBee");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCID.SkeletronHead) && !seenSkeletron)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onSkeletron");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCID.WallofFlesh) && !seenWallOfFlesh)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onWallOfFlesh");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCID.Retinazer) && !seenTwins)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onTwins");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCID.Deerclops) && !seenDeerclops)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onDeerclops");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCID.QueenSlimeBoss) && !seenQueenSlime)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onQueenSlime");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCID.HallowBoss) && !seenEmpress)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onEmpress");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCID.TheDestroyer) && !seenDestroyer)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onDestroyer");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCID.SkeletronPrime) && !seenSkeletronPrime)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onSkeletronPrime");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCID.Plantera) && !seenPlantera)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onPlantera");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCID.Golem) && !seenGolem)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onGolem");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCID.DukeFishron) && !seenDukeFishron)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onDukeFishron");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCID.CultistBoss) && !seenCultist)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onLunaticCultist");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCID.MoonLordHead) && !seenMoonLord)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onMoonLord");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.WarriorOfLight>()) && !seenWarriorOfLight)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onWarriorOfLight");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.VagrantOfSpaceAndTime>()) && !seenVagrant)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onVagrant");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Nalhaun>()) && !seenNalhaun)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onNalhaun");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Penthesilea>()) && !seenPenth)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onPenth");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Arbitration>()) && !seenArbiter)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onArbiter");
                seenUnknownBossTimer = 300;
            }

            //Calamity Mod Bosses
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("DesertScourgeBody").Type) && !seenDesertScourge)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onDesertScourge");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("Crabulon").Type) && !seenCrabulon)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onCrabulon");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("HiveMind").Type) && !seenHiveMind)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onHiveMind");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("PerforatorHive").Type) && !seenPerforators)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onPerforators");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("PerforatorHive").Type) && !seenPerforators)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onPerforators");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("SlimeGodCore").Type) && !seenSlimeGod)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onSlimeGod");
                    seenUnknownBossTimer = 300;
                }//HARDMODE
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("Cryogen").Type) && !seenCryogen)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onCryogen");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("AquaticScourgeHead").Type) && !seenAquaticScourge)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onAquaticScourge");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("BrimstoneElemental").Type) && !seenBrimstoneElemental)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onBrimstoneElemental");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("CalamitasClone").Type) && !seenCalamitas)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onCalamitas");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("Leviathan").Type) && !seenLeviathan)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onLeviathan");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("LeviathanStart").Type) && !seenSiren)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onSiren");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("Anahita").Type) && !seenAnahita)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onAnahita");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("AstrumAureus").Type) && !seenAstrumAureus)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onAstrumAureus");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("PlaguebringerGoliath").Type) && !seenPlaguebringer)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onPlaguebringer");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("RavagerBody").Type) && !seenRavager)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onRavager");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("AstrumDeusBody").Type) && !seenAstrumDeus)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onAstrumDeus");
                    seenUnknownBossTimer = 300;
                }
                //POST MOON LORD
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("ProfanedGuardianCommander").Type) && !seenProfanedGuardian)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onProfanedGuardian");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("Bumblefuck").Type) && !seenDragonfolly)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onDragonfolly");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("Providence").Type) && !seenProvidence)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onProvidence");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("StormWeaverBody").Type) && !seenStormWeaver)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onStormWeaver");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("CeaselessVoid").Type) && !seenCeaselessVoid)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onCeaselessVoid");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("Signus").Type) && !seenSignus)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onSignus");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("Polterghast").Type) && !seenPolterghast)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onPolterghast");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("OldDuke").Type) && !seenOldDuke)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onOldDuke");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("DevourerofGodsBody").Type) && !seenDog)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onDog");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("Yharon").Type) && !seenYharon)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    yharonPresent = true;
                    starfarerPromptActive("onYharon");
                    seenUnknownBossTimer = 300;
                }

                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("AstrumDeusBody").Type) && !seenAstrumDeus)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onAstrumDeus");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("SupremeCalamitas").Type) && !seenSupremeCalamitas)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onSupremeCalamitas");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("Draedon").Type) && !seenDraedon)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onDraedon");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("AresBody").Type) && !seenAres)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onAres");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("Artemis").Type) && !seenArtemis)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onArtemis");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("ThanatosHead").Type) && !seenThanatos)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onThanatos");
                    seenUnknownBossTimer = 300;
                }
            }

            //Fargos Mod Bosses
            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargoSoulsMod))
            {
                if (NPC.AnyNPCs(fargoSoulsMod.Find<ModNPC>("TrojanSquirrel").Type) && !seenTrojanSquirrel)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onTrojanSquirrel");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(fargoSoulsMod.Find<ModNPC>("DeviBoss").Type) && !seenDeviantt)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onDeviantt");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(fargoSoulsMod.Find<ModNPC>("CosmosChampion").Type) && !seenEridanus)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onEridanus");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(fargoSoulsMod.Find<ModNPC>("AbomBoss").Type) && !seenAbominationn)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onAbominationn");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(fargoSoulsMod.Find<ModNPC>("MutantBoss").Type) && !seenMutant)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onMutant");
                    seenUnknownBossTimer = 300;
                }

            }

            if (ModLoader.TryGetMod("SOTS", out Mod SOTS))
            {
                if (NPC.AnyNPCs(SOTS.Find<ModNPC>("PutridPinkyPhase2").Type) && !seenPutridPinky)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onPutridPinky");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(SOTS.Find<ModNPC>("PharaohsCurse").Type) && !seenPharaoh)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onPharaoh");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(SOTS.Find<ModNPC>("TheAdvisorHead").Type) && !seenAdvisor)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onAdvisor");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(SOTS.Find<ModNPC>("Polaris").Type) && !seenPolaris)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onPolaris");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(SOTS.Find<ModNPC>("Lux").Type) && !seenLux)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onLux");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(SOTS.Find<ModNPC>("SubspaceSerpentBody").Type) && !seenSubspaceSerpent)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onSubspaceSerpent");
                    seenUnknownBossTimer = 300;
                }


            }
            //Thorium Mod Bosses
            /*
            if (thoriumMod != null)
            {
                if (NPC.AnyNPCs(thoriumMod.Find<ModNPC>("TheGrandThunderBirdv2").Type) && !seenGrandThunderBird)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onGrandThunderBird");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(thoriumMod.Find<ModNPC>("QueenJelly").Type) && !seenQueenJellyfish)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onQueenJellyfish");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(thoriumMod.Find<ModNPC>("Viscount").Type) && !seenViscount)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onViscount");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(thoriumMod.Find<ModNPC>("GraniteEnergyStorm").Type) && !seenGraniteEnergyStorm)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onGraniteEnergyStorm");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(thoriumMod.Find<ModNPC>("TheBuriedWarrior").Type) && !seenBuriedChampion)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onBuriedChampion");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(thoriumMod.Find<ModNPC>("ThePrimeScouter").Type) && !seenStarScouter)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onStarScouter");
                    seenUnknownBossTimer = 300;
                }
                //HARDMODE
                if (NPC.AnyNPCs(thoriumMod.Find<ModNPC>("BoreanStrider").Type) && !seenBoreanStrider)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onBoreanStrider");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(thoriumMod.Find<ModNPC>("FallenDeathBeholder").Type) && !seenCoznix)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onCoznix");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(thoriumMod.Find<ModNPC>("Lich").Type) && !seenLich)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onLich");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(thoriumMod.Find<ModNPC>("Abyssion").Type) && !seenAbyssion)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onAbyssion");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(thoriumMod.Find<ModNPC>("SlagFury").Type) && !seenPrimordials)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                        
                    }
                    starfarerPromptActive("onPrimordials");
                    seenUnknownBossTimer = 300;
                }
            }*/
            //If the boss isn't listed...
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.boss && !seenBossesList.Contains(npc.type))
                {
                    if (seenUnknownBossTimer > 0)
                    {
                        //starfarerPromptCooldown = 0;
                    }
                    else
                    {
                        if (starfarerPromptCooldown > 0)
                        {
                            starfarerPromptCooldown = 0;
                        }
                        //seenUnknownBossTimer;
                        seenUnknownBossTimer = 300;
                        starfarerPromptActive("onUnknownBoss");
                    }
                    seenBossesList.Add(npc.type);
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue($"{seenBossesList[1]}"), 190, 100, 247);}
                }
            }

            //Biomes


            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod7))
            {
                if (Player.ZoneBeach && !seenBeachBiome && !(bool)calamityMod.Call("GetInZone", Main.LocalPlayer, "sulphursea"))
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onEnterBeach");
                }

            }
            else
            {
                if (Player.ZoneBeach && !seenBeachBiome)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onEnterBeach");
                }
            }

            if (Player.ZoneSnow && !seenSnowBiome)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onEnterSnow");
            }
            if (Player.ZoneCorrupt && !seenCorruptionBiome)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onEnterCorruption");
            }
            if (Player.ZoneCrimson && !seenCrimsonBiome)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onEnterCrimson");
            }
            if (Player.ZoneDesert && !seenDesertBiome)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onEnterDesert");
            }
            if (Player.ZoneJungle && !seenJungleBiome)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onEnterJungle");
            }
            if (Player.ZoneGlowshroom && !seenGlowingMushroomBiome)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onEnterMushroom");
            }
            if (Player.ZoneMeteor && !seenMeteoriteBiome)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onEnterMeteorite");
            }
            if (Player.ZoneUnderworldHeight && !seenUnderworldBiome)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onEnterUnderworld");
            }
            if (Player.ZoneHallow && !seenHallowBiome && SubworldSystem.Current == null)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onEnterHallow");
            }
            if (Player.ZoneSkyHeight && !seenSpaceBiome && SubworldSystem.Current == null)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onEnterSpace");
            }
            if (Player.ZoneDungeon && !seenDungeonBiome)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onEnterDungeon");
            }
            if (SubworldSystem.IsActive<Observatory>() && !seenObservatory)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onObservatory");
            }
            if (SubworldSystem.IsActive<CygnusAsteroids>() && !seenCygnusAsteroids)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onSpaceRuins");
            }
            if (SubworldSystem.IsActive<BleachedPlanet>() && !seenBleachedPlanet)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onCitadel");
            }
            if (SubworldSystem.IsActive<EternalConfluence>() && !seenConfluence)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onConfluence");
            }
            

            //Modded Biomes

            if (ModLoader.TryGetMod("Verdant", out Mod verdantMod))
            {
                if ((bool)verdantMod.Call("InVerdant") && !seenVerdantBiome)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onEnterVerdant");
                }
            }
            
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod9))
            {
                if ((bool)calamityMod.Call("GetInZone", Main.LocalPlayer, "crags") && !seenCragBiome)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onEnterCrag");
                }

                if ((bool)calamityMod.Call("GetInZone", Main.LocalPlayer, "astral") && !seenAstralBiome)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onEnterAstral");
                }

                if ((bool)calamityMod.Call("GetInZone", Main.LocalPlayer, "sunkensea") && !seenSunkenSeaBiome)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onEnterSunkenSea");
                }

                if ((bool)calamityMod.Call("GetInZone", Main.LocalPlayer, "sulphursea") && !seenSulphurSeaBiome)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onEnterSulphurSea");
                }

                if ((bool)calamityMod.Call("GetInZone", Main.LocalPlayer, "abyss") && !seenAbyssBiome)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onEnterAbyss");
                }
            }
            /*if (thoriumMod != null)
            {
                if ((bool)thoriumMod.Call("GetZoneGranite", Main.LocalPlayer) && !seenGraniteBiome)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onEnterGranite");
                }

                if ((bool)thoriumMod.Call("GetZoneMarble", Main.LocalPlayer) && !seenMarbleBiome)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onEnterMarble");
                }

                if ((bool)thoriumMod.Call("GetZoneAquaticDepths", Main.LocalPlayer) && !seenAquaticDepthsBiome)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onEnterAquaticDepths");
                }
                

            }*/


            //Weather
            if (Player.ZoneRain && !seenRain)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onRain");
            }
            if (Player.ZoneSnow && Player.ZoneRain && !seenSnow)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onSnow");
            }
            if (Player.ZoneSandstorm && !seenSandstorm)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onSandstorm");
            }

            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.AstarteDriver>()))
            {
                novaGauge = 0;
            }
            if (astarteDriverAttacks >= 5)
            {
                astarteDriverAttacks = 5;
            }
            astarteDriverCooldown--;
            ryukenTimer--;
            if (NovaCutInTimer > 0)
            {
                novaGauge -= (trueNovaGaugeMax / 20);
            }
            trueNovaGaugeMax = novaGaugeMax - novaChargeMod;
        }

        public override void UpdateBadLifeRegen()
        {
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.LivingDead>()))
            {

            }
        }

        private void OnKillEnemy(NPC npc)
        {
            if (aquaaffinity == 2)//Cyclic Hunter
            {

                if (ammoRecycleCooldown <= 0)
                {
                    Player.AddBuff(BuffType<Buffs.AmmoRecycle>(), 30);
                    ammoRecycleCooldown = 120;
                    Player.statMana += 8;
                }

            }
            if (starfarerOutfit == 1)//Faerie Attire
            {
                novaGauge += 1;
                if (novaGauge < trueNovaGaugeMax / 2)
                {
                    novaGauge += 1;
                }
                if (npc.boss)
                {
                    Player.AddBuff(BuffType<LucentBliss>(), 7200);
                }
            }
            //
            if (starfarerOutfit == 3)//Celestial
            {
                if (npc.CanBeChasedBy() && !npc.SpawnedFromStatue)
                {
                    if (Player.HasBuff(BuffType<AstarteDriver>()))
                    {
                        Player.AddBuff(BuffType<AstarteDriver>(), 1500);
                    }
                }

            }
            if(Player.HasBuff(BuffType<OffSeersJourney>()))
            {
                if (KevesiFarewellInInventory)
                {

                    for (int d = 0; d < 10; d++)
                    {
                        Dust.NewDust(npc.Center, 0, 0, DustType<WaterShine>(), 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default(Color), 1.0f);
                    }

                    Player.AddBuff(BuffType<FarewellOfFlames>(), 600);
                }
                
            }
            if (Player.HasBuff(BuffType<OffSeersPurpose>()))
            {
                
                if (AgnianFarewellInInventory)
                {

                    for (int d = 0; d < 10; d++)
                    {
                        Dust.NewDust(npc.Center, 0, 0, DustType<Shine>(), 0f + Main.rand.Next(-3, 3), 0f + Main.rand.Next(-3, 3), 150, default(Color), 1.0f);
                    }

                    Player.AddBuff(BuffType<FarewellOfFlames>(), 600);
                }
            }
            if (SoulReaverHeld)
            {
                SoulReaverSouls++;
                if (SoulReaverSouls > 10)
                {
                    SoulReaverSouls = 10;
                }
            }
        }

        private void TsukiyomiTeleport(NPC npc)
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                int halfWidth = 900;
                int halfHeight = 1800;
                Vector2 newPosition = Player.position;
                if (Player.position.X <= npc.Center.X - halfWidth)
                {
                    newPosition.X = npc.Center.X;

                }
                else if (Player.position.X + Player.width >= npc.Center.X + halfWidth)
                {
                    newPosition.X = npc.Center.X;

                }
                else if (Player.position.Y <= npc.Center.Y - halfHeight)
                {
                    newPosition.Y = npc.Center.Y;

                }
                else if (Player.position.Y + Player.height >= npc.Center.Y + halfHeight)
                {
                    newPosition.Y = npc.Center.Y;

                }
                if (newPosition != Player.position)
                {
                    Player.AddBuff(BuffType<Invincibility>(), 10);
                    Player.Teleport(newPosition, 1, 0);
                    NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, Player.whoAmI, newPosition.X, newPosition.Y, 1, 0, 0);

                }
            }
        }
        private void VagrantTeleport(NPC npc)
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                int halfWidth = VagrantOfSpaceAndTime.arenaWidth / 2;
                int halfHeight = VagrantOfSpaceAndTime.arenaHeight / 2;
                Vector2 newPosition = Player.position;
                if (Player.position.X <= npc.Center.X - halfWidth)
                {
                    newPosition.X = npc.Center.X + halfWidth - Player.width - 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X -= 16f;
                    }
                }
                else if (Player.position.X + Player.width >= npc.Center.X + halfWidth)
                {
                    newPosition.X = npc.Center.X - halfWidth + 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X += 16f;
                    }
                }
                else if (Player.position.Y <= npc.Center.Y - halfHeight)
                {
                    newPosition.Y = npc.Center.Y + halfHeight - Player.height - 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.Y -= 16f;
                    }
                }
                else if (Player.position.Y + Player.height >= npc.Center.Y + halfHeight)
                {
                    newPosition.Y = npc.Center.Y - halfHeight + 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.Y += 16f;
                    }
                }
                if (newPosition != Player.position)
                {
                    Player.Teleport(newPosition, 1, 0);
                    NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, Player.whoAmI, newPosition.X, newPosition.Y, 1, 0, 0);

                }
            }
        }
        private void WarriorTeleport(NPC npc)
        {
            if (inWarriorOfLightFightTimer > 0)
            {
                int halfWidth = WarriorOfLight.arenaWidth / 2;
                int halfHeight = WarriorOfLight.arenaHeight / 2;
                Vector2 newPosition = Player.position;
                if (Player.position.X <= npc.Center.X - halfWidth)//Left wall
                {
                    newPosition.X = npc.Center.X - halfWidth - Player.width - 1;
                    Player.velocity = new Vector2(20, Player.velocity.Y);
                    // if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("1"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X -= 16f;

                    }
                }
                else if (Player.position.X + Player.width >= npc.Center.X + halfWidth)//Right Wall
                {
                    newPosition.X = npc.Center.X + halfWidth + 1;
                    Player.velocity = new Vector2(-20, Player.velocity.Y);
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("2"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X += 16f;

                    }
                }
                else if (Player.position.Y <= npc.Center.Y - halfHeight)//Top
                {
                    newPosition.Y = npc.Center.Y - halfHeight - Player.height - 1;
                    Player.velocity = new Vector2(Player.velocity.X, 20);
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("3"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.Y -= 16f;

                    }
                }
                else if (Player.position.Y + Player.height >= npc.Center.Y + halfHeight)//Bottom
                {
                    newPosition.Y = npc.Center.Y + halfHeight + 1;
                    Player.velocity = new Vector2(Player.velocity.X, -20);
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("4"), 190, 100, 247);}
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {

                        newPosition.Y += 16f;
                    }
                }
                if (newPosition != Player.position)
                {
                    //player.Teleport(newPosition, 1, 0);
                    //NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, player.whoAmI, newPosition.X, newPosition.Y, 1, 0, 0);


                }
            }

        }
        private void PenthTeleport(NPC npc)
        {
            if (inPenthFightTimer > 0 && Player.immuneTime <= 0)
            {
                int halfWidth = Penthesilea.arenaWidth / 2;
                int halfHeight = Penthesilea.arenaHeight / 2;
                Vector2 newPosition = Player.position;
                if (Player.position.X <= npc.Center.X - halfWidth)
                {

                    newPosition.X = npc.Center.X - halfWidth + 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X += 16f;
                    }
                }
                else if (Player.position.X + Player.width >= npc.Center.X + halfWidth)
                {
                    newPosition.X = npc.Center.X + halfWidth - Player.width - 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.X -= 16f;
                    }
                }
                else if (Player.position.Y <= npc.Center.Y - halfHeight)
                {
                    newPosition.Y = npc.Center.Y - halfHeight + 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.Y += 16f;
                    }
                }
                else if (Player.position.Y + Player.height >= npc.Center.Y + halfHeight)
                {


                    newPosition.Y = npc.Center.Y + halfHeight - Player.height - 1;
                    while (Collision.SolidCollision(newPosition, Player.width, Player.height))
                    {
                        newPosition.Y -= 16f;
                    }
                }
                if (newPosition != Player.position)
                {
                    Player.Teleport(newPosition, 1, 0);
                    NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, Player.whoAmI, newPosition.X, newPosition.Y, 1, 0, 0);

                }
            }

        }
        public static bool SameTeam(Player player1, Player player2)
        {
            // Always affects self
            if (player1.whoAmI == player2.whoAmI) return true;
            // If on a team, must be sharding a team
            if (player1.team > 0 && player1.team != player2.team) return false;
            // Not on same team during PVP
            if (player1.hostile && player2.hostile && (player1.team == 0 || player2.team == 0)) return false;
            // Banner applies to all (See Nebula Buff mechanics)
            return true;
        }//Thank you to WeaponOut!
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Main.LocalPlayer.active && !Main.LocalPlayer.dead)
            {



                //Edin Genesis Quasar Casts
                if (chosenStellarNova == 5 && StarsAbove.novaKey.JustPressed && !stellarArray && !starfarerDialogue && astarteDriverAttacks > 0 && Main.LocalPlayer.HasBuff(BuffType<Buffs.AstarteDriver>()) && astarteDriverCooldown < 0)
                {

                    if (Player.whoAmI == Main.myPlayer)
                    {
                        //
                        astarteDriverCooldown = 120;
                        astarteDriverAttacks--;

                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(Player.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(Player.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, Player.Center);
                        Vector2 mousePosition = Player.DirectionTo(Player.GetModPlayer<StarsAbovePlayer>().playerMousePos) * Main.rand.Next(20, 22);
                        for (int i = 0; i < 10; i++)
                        {
                            int type = Main.rand.Next(new int[] { ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });
                            Projectile.NewProjectile(null, Player.Center.X, Player.Center.Y, mousePosition.X, mousePosition.Y, type, novaDamage / 10, 3, Player.whoAmI, 0f);

                        }


                        int numberProjectiles = 10;
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = mousePosition.RotatedByRandom(MathHelper.ToRadians(40));
                            int type = Main.rand.Next(new int[] { ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });

                            Projectile.NewProjectile(null, Player.Center.X, Player.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, novaDamage / 10, 3, Player.whoAmI);
                        }
                        Vector2 shotKnockback = Vector2.Normalize(mousePosition) * 15f * -1f;
                        Player.velocity = shotKnockback;
                        if (chosenStarfarer == 2)
                        {
                            Player.AddBuff(BuffType<Buffs.Invincibility>(), 60);
                        }
                        //Vector2 mousePosition = Main.MouseWorld;
                        //Projectile.NewProjectile(null,new Vector2(player.Center.X - 100, player.Center.Y), Vector2.Zero, mod.ProjectileType("Theofania2"), novaDamage + novaDamageMod, 4, player.whoAmI, 0, 1);                                                                                                                                                                                                                                                                                                                                             //Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
                        //Projectile.NewProjectile(null,player.Center.X, player.Center.Y - 200, (Main.MouseWorld).ToRotation(), direction, ProjectileID.StarWrath, novaDamage + novaDamageMod, 0, player.whoAmI, 0f);

                    }
                }
                if (chosenStellarNova == 1 && StarsAbove.novaKey.JustPressed && !stellarArray && !starfarerDialogue && chosenStellarNova != 0 && Main.LocalPlayer.HasBuff(BuffType<Buffs.TheofaniaTricast>()))//Theofania Tricast
                {

                    if (Player.whoAmI == Main.myPlayer)
                    {
                        //
                        for (int i = 0; i < Player.CountBuffs(); i++)
                            if (Player.buffType[i] == BuffType<Buffs.TheofaniaTricast>())
                            {
                                Player.DelBuff(i);


                            }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(Player.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(Player.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_theofaniaActive, Player.Center);

                        Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y), Vector2.Zero, Mod.Find<ModProjectile>("Theofania3").Type, novaDamage, 4, Player.whoAmI, 0, 1);                                                                                                                                                             //Vector2 mousePosition = Main.MouseWorld;
                                                                                                                                                                                                                                                                                                                                                                 //Projectile.NewProjectile(null,new Vector2(player.Center.X - 100, player.Center.Y), Vector2.Zero, mod.ProjectileType("Theofania2"), novaDamage + novaDamageMod, 4, player.whoAmI, 0, 1);                                                                                                                                                                                                                                                                                                                                             //Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
                                                                                                                                                                                                                                                                                                                                                                 //Projectile.NewProjectile(null,player.Center.X, player.Center.Y - 200, (Main.MouseWorld).ToRotation(), direction, ProjectileID.StarWrath, novaDamage + novaDamageMod, 0, player.whoAmI, 0f);

                    }
                }
                //dualCast
                if (chosenStellarNova == 1 && StarsAbove.novaKey.JustPressed && !stellarArray && !starfarerDialogue && chosenStellarNova != 0 && Main.LocalPlayer.HasBuff(BuffType<Buffs.TheofaniaDualcast>()))//Theofania Dualcast
                {
                    if (Player.whoAmI == Main.myPlayer)
                    {
                        //
                        for (int i = 0; i < Player.CountBuffs(); i++)
                            if (Player.buffType[i] == BuffType<Buffs.TheofaniaDualcast>())
                            {
                                Player.DelBuff(i);


                            }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(Player.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(Player.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_theofaniaActive, Player.Center);

                        Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y), Vector2.Zero, Mod.Find<ModProjectile>("Theofania2").Type, novaDamage, 4, Player.whoAmI, 0, 1);                                                                                                                                                             //Vector2 mousePosition = Main.MouseWorld;
                                                                                                                                                                                                                                                                                                                                                                 //Projectile.NewProjectile(null,new Vector2(player.Center.X - 100, player.Center.Y), Vector2.Zero, mod.ProjectileType("Theofania2"), novaDamage + novaDamageMod, 4, player.whoAmI, 0, 1);                                                                                                                                                                                                                                                                                                                                             //Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
                        if (chosenStarfarer == 1)
                        {
                            Player.AddBuff(BuffType<Buffs.TheofaniaTricast>(), 600);

                        }

                    }
                }//triCast
                if (novaGauge == trueNovaGaugeMax && StarsAbove.novaKey.JustPressed && !stellarArray && !starfarerDialogue && chosenStellarNova == 3)
                {
                    if (Player.whoAmI == Main.myPlayer)
                    {
                        //Activate the Stellar Novas here.
                        if (chosenStellarNova == 3)//Kiwami Ryuken
                        {
                            novaGauge = 0;
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, Player.Center);
                            for (int d = 0; d < 105; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                            }
                            for (int d = 0; d < 105; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                            }
                            if (chosenStarfarer == 1)
                            {
                                Player.AddBuff(BuffType<Buffs.KiwamiRyuken>(), 60);

                            }
                            else
                            {
                                Player.AddBuff(BuffType<Buffs.KiwamiRyuken>(), 60);

                            }

                            //Projectile.NewProjectile(null,new Vector2(player.Center.X, player.Center.Y - 860), Vector2.Zero, mod.ProjectileType("Laevateinn"), novaDamage + novaDamageMod, 4, player.whoAmI, 0, 1);//The 1 here means that ai1 will be set to 1. this is good for the first cast.
                            //Vector2 mousePosition = Main.MouseWorld;
                            //Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
                            //Projectile.NewProjectile(null,player.Center.X, player.Center.Y - 200, (Main.MouseWorld).ToRotation(), direction, ProjectileID.StarWrath, novaDamage + novaDamageMod, 0, player.whoAmI, 0f);
                        }
                    }
                }
                else
                if (novaGauge == trueNovaGaugeMax && StarsAbove.novaKey.JustPressed && !stellarArray && !starfarerDialogue && chosenStellarNova != 0)
                {
                    Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().NovaCutInTimer = 140;
                    Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().NovaCutInVelocity = 20;
                    Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().NovaCutInX = 0;
                    Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().NovaCutInOpacity = 0;
                    Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().randomNovaDialogue = Main.rand.Next(0, 6);
                    if (!voicesEnabled)
                    {

                        if (chosenStarfarer == 1)
                        {
                            if (randomNovaDialogue == 0)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.AN0, Player.Center);
                            }
                            if (randomNovaDialogue == 1)
                            {
                                if (chosenStellarNova == 1)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.AN11, Player.Center);
                                }
                                if (chosenStellarNova == 2)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.AN12, Player.Center);
                                }
                                if (chosenStellarNova == 3)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.AN13, Player.Center);
                                }
                                if (chosenStellarNova == 4)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.AN14, Player.Center);
                                }
                                if (chosenStellarNova == 5)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.AN15, Player.Center);
                                }

                            }
                            if (randomNovaDialogue == 2)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.AN2, Player.Center);
                            }
                            if (randomNovaDialogue == 3)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.AN3, Player.Center);
                            }
                            if (randomNovaDialogue == 4)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.AN4, Player.Center);
                            }
                            if (randomNovaDialogue == 5)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.AN5, Player.Center);
                            }

                        }
                        if (chosenStarfarer == 2)
                        {
                            if (randomNovaDialogue == 0)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.EN0, Player.Center);
                            }
                            if (randomNovaDialogue == 1)
                            {
                                if (chosenStellarNova == 1)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.EN11, Player.Center);
                                }
                                if (chosenStellarNova == 2)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.EN12, Player.Center);
                                }
                                if (chosenStellarNova == 3)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.EN13, Player.Center);
                                }
                                if (chosenStellarNova == 4)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.EN14, Player.Center);
                                }
                                if (chosenStellarNova == 5)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.EN15, Player.Center);
                                }

                            }
                            if (randomNovaDialogue == 2)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.EN2, Player.Center);
                            }
                            if (randomNovaDialogue == 3)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.EN3, Player.Center);
                            }
                            if (randomNovaDialogue == 4)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.EN4, Player.Center);
                            }
                            if (randomNovaDialogue == 5)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.EN5, Player.Center);
                            }

                        }

                    }
                    Player.GetModPlayer<StarsAbovePlayer>().activateShockwaveEffect = true;


                    if (Player.whoAmI == Main.myPlayer)
                    {
                        //Activate the Stellar Novas here.
                        if (chosenStellarNova == 1)//Theofania Inanis
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_theofaniaActive, Player.Center);
                            Player.AddBuff(BuffType<Buffs.TheofaniaDualcast>(), 600);
                            for (int d = 0; d < 105; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                            }
                            for (int d = 0; d < 105; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                            }

                            Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y), Vector2.Zero, Mod.Find<ModProjectile>("Theofania").Type, novaDamage, 4, Player.whoAmI, 0, 1);//The 1 here means that ai1 will be set to 1. this is good for the first cast.
                                                                                                                                                                                                       //Vector2 mousePosition = Main.MouseWorld;
                                                                                                                                                                                                       //Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
                                                                                                                                                                                                       //Projectile.NewProjectile(null,player.Center.X, player.Center.Y - 200, (Main.MouseWorld).ToRotation(), direction, ProjectileID.StarWrath, novaDamage + novaDamageMod, 0, player.whoAmI, 0f);
                            onActivateStellarNova();
                        }
                    }

                    if (Player.whoAmI == Main.myPlayer)
                    {
                        //Activate the Stellar Novas here.
                        if (chosenStellarNova == 2)//Ars Laevateinn
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_theofaniaActive, Player.Center);
                            for (int d = 0; d < 105; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                            }
                            for (int d = 0; d < 105; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                            }

                            Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y - 860), Vector2.Zero, Mod.Find<ModProjectile>("Laevateinn").Type, novaDamage, 4, Player.whoAmI, 0, 1);//The 1 here means that ai1 will be set to 1. this is good for the first cast.
                            Player.AddBuff(BuffType<Buffs.SurtrTwilight>(), 600);                                                                                                        //Vector2 mousePosition = Main.MouseWorld;
                                                                                                                                                                                         //Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
                            onActivateStellarNova();                                                                                                                                                           //Projectile.NewProjectile(null,player.Center.X, player.Center.Y - 200, (Main.MouseWorld).ToRotation(), direction, ProjectileID.StarWrath, novaDamage + novaDamageMod, 0, player.whoAmI, 0f);
                        }
                    }
                    if (Player.whoAmI == Main.myPlayer)
                    {
                        //Activate the Stellar Novas here.
                        if (chosenStellarNova == 4)//The Garden of Avalon
                        {
                            //Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/theofaniaActive"));
                            for (int d = 0; d < 105; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                            }
                            for (int d = 0; d < 105; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                            }

                            Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y - 860), Vector2.Zero, Mod.Find<ModProjectile>("GardenOfAvalon").Type, novaDamage, 4, Player.whoAmI, 0, 1);//The 1 here means that ai1 will be set to 1. this is good for the first cast.
                            onActivateStellarNova();
                            //player.AddBuff(BuffType<Buffs.GardenOfAvalon>(), (novaDamage / 100));                                                                                                                                                                             //Vector2 mousePosition = Main.MouseWorld;
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_GardenOfAvalonActivated, Player.Center);
                            //activateGardenBuff();


                            //Garden of Avalon buffs.
                            Player.AddBuff(BuffType<Buffs.GardenOfAvalon>(), 8 * 60);
                            if (chosenStarfarer == 2)
                            {
                                Player.AddBuff(BuffType<Buffs.DreamlikeCharisma>(), 8 * 60);

                            }


                            if (chosenStarfarer == 1)
                            {
                                Player.AddBuff(BuffType<Buffs.Invincibility>(), (4 * 60));
                                Player.statLife += 100;

                            }
                            else
                            {
                                Player.AddBuff(BuffType<Buffs.Invincibility>(), (2 * 60));
                            }
                            int uniqueCrit = Main.rand.Next(100);
                            if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                            {
                                Rectangle textPos2 = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height); //Mana Heal
                                CombatText.NewText(textPos2, new Color(255, 132, 2, 240), $"Critical cast!", false, false);

                                Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 40, Player.width, Player.height); //Heal
                                CombatText.NewText(textPos, new Color(63, 221, 53, 240), $"{(int)(novaCritDamage * (1 + novaCritDamageMod))}", false, false);
                                Player.statLife += (int)(novaCritDamage * (1 + novaCritDamageMod));
                                Player.AddBuff(BuffType<Buffs.SolemnAegis>(), (15 * 60));
                            }
                            else
                            {

                            }


                        }
                    }
                    if (Player.whoAmI == Main.myPlayer)
                    {
                        //Activate the Stellar Novas here.
                        if (chosenStellarNova == 5)//Edin Shugra Quasar
                        {


                            //Projectile.NewProjectile(null,new Vector2(player.Center.X, player.Center.Y - 860), Vector2.Zero, mod.ProjectileType("Laevateinn"), novaDamage + novaDamageMod, 4, player.whoAmI, 0, 1);//The 1 here means that ai1 will be set to 1. this is good for the first cast.

                            Player.AddBuff(BuffType<Buffs.AstarteDriverPrep>(), 180);                                                                                                        //Vector2 mousePosition = Main.MouseWorld;
                                                                                                                                                                                             //Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
                                                                                                                                                                                             //Projectile.NewProjectile(null,player.Center.X, player.Center.Y - 200, (Main.MouseWorld).ToRotation(), direction, ProjectileID.StarWrath, novaDamage + novaDamageMod, 0, player.whoAmI, 0f);
                        }
                    }

                }
            }
        }

        public override void PreUpdateBuffs()
        {

            if (HunterSongPlaying == 1)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player p = Main.player[i];
                    if (p.active && p.Distance(Player.Center) < 1000)
                    {

                        p.AddBuff(BuffType<ChallengerSong>(), 1200);  //
                        for (int d = 0; d < 15; d++)
                        {
                            Dust.NewDust(p.Center, 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 0.8f);
                        }
                    }



                }
                Player.GetModPlayer<StarsAbovePlayer>().SymphonySongsPlayed++;
                SoundEngine.PlaySound(StarsAboveAudio.SFX_HuntingHornBasic, Player.Center);
                Player.AddBuff(BuffType<HunterSymphonyCooldown>(), 1200);
                HunterSongPlaying = 0;
            }


            for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.active && npc.type == NPCType<VagrantOfSpaceAndTime>())
                {

                    VagrantTeleport(npc);
                    break;
                }
            }
            for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.active && npc.type == NPCType<Tsukiyomi>())
                {

                    TsukiyomiTeleport(npc);
                    break;
                }
            }
            for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.active && npc.type == NPCType<Tsukiyomi2>())
                {

                    TsukiyomiTeleport(npc);
                    break;
                }
            }
            for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.active && npc.type == NPCType<WarriorOfLight>())
                {

                    WarriorTeleport(npc);
                    break;
                }
            }
            for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.active && npc.type == NPCType<Penthesilea>())
                {

                    PenthTeleport(npc);
                    break;
                }
            }

            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Nalhaun>()))
            {
                Player.AddBuff(BuffType<Buffs.SoulSapping>(), 2);
            }
            if (NPC.downedMoonlord && !DownedBossSystem.downedWarrior && SubworldSystem.Current == null)
            {

                Player.AddBuff(BuffType<Buffs.EverlastingLight>(), 2);
                if (inWarriorOfLightFightTimer > 0)
                {
                    Player.AddBuff(BuffType<Buffs.Determination>(), 2);
                }

            }
            if (stellarSickness == true)
            {
                Player.AddBuff(BuffType<Buffs.StellarSickness>(), 3600);

                stellarSickness = false;
            }
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.KiwamiRyukenConfirm>()))
            {


            }
            judgementCutTimer--;
            screenShakeTimerGlobal--;

            if (judgementCutTimer == 0)
            {
                judgementCut = true;
            }
            else
            {
                judgementCut = false;
            }
            if (Main.LocalPlayer.HeldItem.ModItem is BuryTheLight)
            {

            }
            else
            {
                judgementGauge = 0;
                judgementGaugeVisibility--;
                if (judgementGaugeVisibility < 0)
                {
                    judgementGaugeVisibility = 0;
                }
                else
                {

                    //Filters.Scene.Deactivate("Shockwave");


                }
            }
            if (Main.LocalPlayer.HeldItem.ModItem is SpatialDisk)
            {

            }
            else
            {
                StarfarerSelectionVisibility -= 0.1f;
                if (StarfarerSelectionVisibility < 0)
                {
                    StarfarerSelectionVisibility = 0;
                }
            }


            if (NPC.downedMechBoss1 || NPC.downedMechBoss2 || NPC.downedMechBoss3)
            {
                catalystBonus = 14;
            }
            if (NPC.downedPlantBoss)
            {
                catalystBonus = 22;
            }
            if (NPC.downedMoonlord)
            {
                catalystBonus = 40;
            }

            else
            {
                catalystBonus = 0;
            }

            if (Player.GetModPlayer<StarsAbovePlayer>().judgementGauge > 100)
            {
                judgementGauge = 100;
            }

            if (Player.GetModPlayer<StarsAbovePlayer>().corn)
            {
                Main.LocalPlayer.AddBuff(BuffID.WellFed, 1);

            }
            if (Player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
            {
                Player.AddBuff(BuffType<Buffs.AsphodeneBlessing>(), 2);

            }
            if (Player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
            {
                Player.AddBuff(BuffType<Buffs.EridaniBlessing>(), 2);

            }

            if (Player.GetModPlayer<StarsAbovePlayer>().celestialFoci)
            {

                Player.respawnTimer = 480;//8 seconds



            }

            //foreach (Player castPlayer in Main.player)
            //{ 
            if (Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceStart == true)
            {
                Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceActive = true;
                Player.GetModPlayer<StarsAbovePlayer>().stellarPerformancePrep = true;
                Player.GetModPlayer<StarsAbovePlayer>().stellarPerformancePostPrep = false;
                Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceEnding = false;
                Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceSongTimer = 0;

                //SoundEffectInstance x = Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/SuistrumeSound"));
                //player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceSoundInstance = x;
                Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceClosingIn = 1000f;
                Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceIndicator = false;
                Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceStart = false;
            }
            if (Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceActive == true)//The song lasts for 5000~ ticks
            {


                Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceSongTimer++;
                if (Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceSongTimer >= 5061)
                {

                    for (int d = 0; d < 40; d++)
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, 21, 0f + Main.rand.Next(-30, 30), 0f + Main.rand.Next(-30, 30), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 35; d++)
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, 45, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                    }
                    Player.AddBuff(BuffType<Buffs.StellarPerformanceCooldown>(), 3600);

                    Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceEnding = true;
                    Player.GetModPlayer<StarsAbovePlayer>().stellarPerformancePostPrep = false;
                    Player.GetModPlayer<StarsAbovePlayer>().PerformanceResourceCurrent = 0;

                    Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceActive = false;

                }

            }
            if (Player.GetModPlayer<StarsAbovePlayer>().stellarPerformancePrep == true)
            {

                for (int i = 0; i < 10; i++)
                {
                    Vector2 vector = new Vector2(
                        Main.rand.Next(-2048, 2048) * (0.003f * Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceClosingIn / 6) - 10,
                        Main.rand.Next(-2048, 2048) * (0.003f * Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceClosingIn / 6) - 10);
                    Dust d = Main.dust[Dust.NewDust(
                        Player.MountedCenter + vector, 1, 1,
                        45, 0, 0, 255,
                        new Color(0.8f, 0.4f, 1f), 1.5f)];
                    d.velocity = -vector / 16;
                    d.velocity -= Player.velocity / 8;
                    d.noLight = true;
                    d.noGravity = true;

                }
                for (int i = 0; i < 30; i++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceClosingIn);
                    offset.Y += (float)(Math.Cos(angle) * Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceClosingIn);

                    Dust d = Dust.NewDustPerfect(Player.Center + offset, 45, Player.velocity, 200, default(Color), 0.7f);
                    d.fadeIn = 1f;
                    d.noGravity = true;
                }

                Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceClosingIn -= 2;
                Player.GetModPlayer<StarsAbovePlayer>().stellarPerformancePrepTimer += 0.1f;
                if (Player.GetModPlayer<StarsAbovePlayer>().stellarPerformancePrepTimer >= 0.401f)
                {
                    Player.GetModPlayer<StarsAbovePlayer>().PerformanceResourceCurrent++;
                    Player.GetModPlayer<StarsAbovePlayer>().stellarPerformancePrepTimer = 0;
                }
                if (Player.GetModPlayer<StarsAbovePlayer>().PerformanceResourceCurrent >= 100)
                {
                    for (int d = 0; d < 100; d++)
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, 21, 0f + Main.rand.Next(-40, 40), 0f + Main.rand.Next(-40, 40), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 35; d++)
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, 45, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default(Color), 1.5f);
                    }
                    stellarPerformancePulseRadius = 0;
                    Player.GetModPlayer<StarsAbovePlayer>().stellarPerformancePostPrep = true;
                    Player.GetModPlayer<StarsAbovePlayer>().stellarPerformancePrep = false;

                }
            }
            if (Player.GetModPlayer<StarsAbovePlayer>().stellarPerformancePostPrep == true) // POST PREP /////////////////////////////////////////////////////////////////////////
            {
                stellarPerformanceDepletion++;
                Player.AddBuff(BuffType<Buffs.StellarPerformance>(), 2);
                if (stellarPerformancePulseRadius < 320)
                    stellarPerformancePulseRadius += 5.3f;
                if (stellarPerformancePulseRadius >= 320 && stellarPerformancePulseRadius < 420)
                    stellarPerformancePulseRadius += 2;
                if (stellarPerformancePulseRadius >= 420)
                    stellarPerformancePulseRadius++;

                if (stellarPerformanceDepletion >= 5)
                {
                    if (!(Player.velocity == Vector2.Zero))
                    {
                        if (Player.GetModPlayer<StarsAbovePlayer>().PerformanceResourceCurrent < 100)
                        {
                            Player.GetModPlayer<StarsAbovePlayer>().PerformanceResourceCurrent++;
                        }
                    }
                    else
                    {
                        Player.GetModPlayer<StarsAbovePlayer>().PerformanceResourceCurrent--;
                    }
                    stellarPerformanceDepletion = 0;
                }
                if (Main.rand.NextBool(3))
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, 45, 0f + Main.rand.Next(-30, 30), 0f + Main.rand.Next(-30, 30), 150, default(Color), 1.5f);
                }
                if (!(Player.velocity == Vector2.Zero))
                {
                    if (Main.rand.NextBool(5))
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, DustType<MusicNote>(), 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default(Color), 1.5f);
                    }
                }
                if (Main.rand.NextBool(2))
                {
                    Vector2 position = new Vector2(Player.position.X - (800 / 2), Player.position.Y - (800 / 2));
                    Dust.NewDust(position, 800, 800, DustType<MusicNote>(), 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default(Color), 1.5f);
                }

                if (Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceIndicator == true)
                {
                    for (int i = 0; i < 30; i++)
                    {//Circle
                        Vector2 offset = new Vector2();
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        offset.X += (float)(Math.Sin(angle) * 520f);
                        offset.Y += (float)(Math.Cos(angle) * 520f);

                        Dust d = Dust.NewDustPerfect(Player.Center + offset, 45, Player.velocity, 200, default(Color), 0.7f);
                        d.fadeIn = 1f;
                        d.noGravity = true;
                    }
                }
                for (int i = 0; i < 30; i++)
                {//Circle pulse
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * stellarPerformancePulseRadius);
                    offset.Y += (float)(Math.Cos(angle) * stellarPerformancePulseRadius);

                    Dust d = Dust.NewDustPerfect(Player.Center + offset, 45, Player.velocity, 200, default(Color), 0.7f);
                    d.fadeIn = 0.2f;
                    d.noGravity = true;
                }
                if (stellarPerformancePulseRadius >= 520)
                {
                    Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceIndicator = true;
                    stellarPerformancePulseRadius = 0;
                }


            }
            if (Player.GetModPlayer<StarsAbovePlayer>().stellarPerformancePostPrep == true)
            {
                Player.AddBuff(BuffType<Buffs.StellarPerformance>(), 1);


            }
            if (Player.GetModPlayer<StarsAbovePlayer>().stellarPerformancePostPrep == true)
            {

                if (Player.GetModPlayer<StarsAbovePlayer>().PerformanceResourceCurrent < 0)
                {
                    Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceSoundInstance?.Stop();
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_SuistrumeFail, Player.Center);
                    Player.AddBuff(BuffType<Buffs.StellarPerformanceCooldown>(), 7200);

                    Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceActive = false;
                    Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceEnding = true;
                    Player.GetModPlayer<StarsAbovePlayer>().stellarPerformancePostPrep = false;
                    Player.GetModPlayer<StarsAbovePlayer>().PerformanceResourceCurrent = 0;

                }

            }


            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.ButterflyTrance>()))
            {

                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.position;
                int playerWidth = Main.LocalPlayer.width;
                int playerHeight = Main.LocalPlayer.height;

                Dust.NewDust(position, playerWidth, playerHeight, 164, 0f, 0f, 150, default(Color), 1.5f);




            }

            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.EverlastingLight>()) || lightMonolith)
            {

                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.position;
                int playerWidth = Main.LocalPlayer.width;
                int playerHeight = Main.LocalPlayer.height;
                if (Main.LocalPlayer.ZoneOverworldHeight)
                {
                    Dust.NewDust(new Vector2(position.X - 1200, position.Y - 550), 2200, 1, 64, 0f, 4f, 64, default(Color), 1.5f);
                    Dust.NewDust(new Vector2(position.X - 1200, position.Y - 550), 2200, 1, 64, 0f, 4f, 64, default(Color), 1.3f);
                    Dust.NewDust(new Vector2(position.X - 1200, position.Y - 550), 2200, 1, 64, 0f, 4f, 64, default(Color), 0.9f);
                    Dust.NewDust(new Vector2(position.X - 1200, position.Y - 550), 2200, 1, 64, 0f, 4f, 64, default(Color), 2f);
                }





            }

            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.StellarListener>()))
            {

                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.position;
                int playerWidth = Main.LocalPlayer.width;
                int playerHeight = Main.LocalPlayer.height;

                Dust.NewDust(position, playerWidth, playerHeight, 206, 0f, 0f, 150, default(Color), 1.5f);
                if (!(Player.velocity == Vector2.Zero))
                {
                    if (Main.rand.NextBool(5))
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, DustType<MusicNote>(), 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default(Color), 1.5f);
                    }
                }



            }
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.StellarPerformanceCooldown>()))
            {
                Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceCooldown = true;

            }
            else
            {
                Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceCooldown = false;

            }
            //}

            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.LivingDead>()))
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.position;
                int playerWidth = Main.LocalPlayer.width;
                int playerHeight = Main.LocalPlayer.height;
                for (int d = 0; d < 5; d++)
                {
                    dust = Main.dust[Terraria.Dust.NewDust(position, playerWidth, playerHeight, 258, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                }


            }
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.BloodOfTheDragon>()))
            {

                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.position;
                int playerWidth = Main.LocalPlayer.width;
                int playerHeight = Main.LocalPlayer.height;
                for (int d = 0; d < 5; d++)
                {
                    Dust.NewDust(position, playerWidth, playerHeight, 206, 0f, 0f, 150, default(Color), 1.5f);
                }



            }
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.KiwamiRyuken>()))
            {

                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.position;
                int playerWidth = Main.LocalPlayer.width;
                int playerHeight = Main.LocalPlayer.height;
                for (int d = 0; d < 5; d++)
                {
                    Dust.NewDust(position, playerWidth, playerHeight, 206, 0f, 0f, 150, default(Color), 1.5f);
                }



            }
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.KiwamiRyukenConfirm>()))
            {


                for (int i = 0; i < 90; i++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * ryukenTimer * 12);
                    offset.Y += (float)(Math.Cos(angle) * ryukenTimer * 12);

                    Dust d = Dust.NewDustPerfect(Player.Center + offset, 90, Player.velocity, 20, default(Color), 0.4f);

                    d.fadeIn = 1f;
                    d.noGravity = true;
                }


            }
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.LifeOfTheDragon>()))
            {

                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.position;
                int playerWidth = Main.LocalPlayer.width;
                int playerHeight = Main.LocalPlayer.height;
                for (int d = 0; d < 5; d++)
                {
                    Dust.NewDust(position, playerWidth, playerHeight, 258, 0f, 0f, 150, default(Color), 1.5f);
                }


            }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.LimitBreak>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<Buffs.LimitBreakCooldown>(), 3600);


                    }
                }

            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.CosmicConception>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<Buffs.CosmicConceptionCooldown>(), 7200);


                    }
                }

            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.Voidform>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Vector2 placement2 = new Vector2((Player.Center.X), Player.Center.Y);
                        Projectile.NewProjectile(null, placement2.X, placement2.Y, 0, 0, Mod.Find<ModProjectile>("radiate").Type, 0, 0f, 0);
                        Player.AddBuff(BuffType<Buffs.CosmicRecoil>(), 60);
                        screenShakeTimerGlobal = 0;
                        for (int i2 = 0; i2 < 70; i2++)
                        {

                            Vector2 vel = new Vector2(Main.rand.NextFloat(-9, 9), Main.rand.NextFloat(-9, 9));
                            int type = Main.rand.Next(new int[] { ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });

                            Projectile.NewProjectile(null, Player.Center, vel, type, baseNovaDamageAdd / 10, 6, Player.whoAmI, 0, 1);
                        }


                    }
                }

            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.WrathfulCeruleanFlame>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<Buffs.Burnout>(), 1200);


                    }
                }

            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.Afterburner>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<Buffs.AfterburnerCooldown>(), 1500);


                    }
                }

            if (Player.HasBuff(BuffType<Buffs.LimitBreak>()))
            {
                Vector2 position = Main.LocalPlayer.position;
                int playerWidth = Main.LocalPlayer.width;
                int playerHeight = Main.LocalPlayer.height;

                for (int d = 0; d < 5; d++)
                {

                    Dust.NewDust(position, playerWidth, playerHeight, 206, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default(Color), 1.5f);

                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.

                    dust = Main.dust[Terraria.Dust.NewDust(position, playerWidth, playerHeight, 206, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 0, new Color(255, 255, 255), 1f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(81, Main.LocalPlayer);

                }
                if (Player.statMana < Player.statManaMax2)
                {
                    Player.statMana += 2;
                    Player.statLife -= 2;
                    if (Player.statLife <= 0)
                    {
                        if (!Main.LocalPlayer.HasBuff(BuffType<Buffs.LivingDead>()))
                        {
                            for (int d = 0; d < 50; d++)
                            {
                                Dust dust;
                                Dust.NewDust(position, playerWidth, playerHeight, 106, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                                dust = Main.dust[Terraria.Dust.NewDust(position, playerWidth, playerHeight, 206, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 0, new Color(255, 255, 255), 1f)];
                                dust.shader = GameShaders.Armor.GetSecondaryShader(81, Player);

                            }
                        }
                        Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + "'s body was broken, along with their limits."), 500, 0);




                    }
                }



            }
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.LeftDebuff>()))
            {
                for (int i = 0; i < 2; i++)
                {//



                    Vector2 vector32 = new Vector2(Player.Center.X - 500, Player.Center.Y);
                    for (int i3 = 0; i3 < 50; i3++)
                    {
                        Vector2 position = Vector2.Lerp(Player.Center, vector32, (float)i3 / 50);
                        Dust d = Dust.NewDustPerfect(position, 20, null, 240, default(Color), 0.3f);
                        d.fadeIn = 0.3f;
                        d.noLight = true;
                        d.noGravity = true;
                    }
                    for (int i2 = 0; i2 < 5; i2++)
                    {//Circle
                        Vector2 offset = new Vector2();
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        offset.X += (float)(Math.Sin(angle) * 30f);
                        offset.Y += (float)(Math.Cos(angle) * 30f);

                        Dust d2 = Dust.NewDustPerfect(vector32 + offset, 20, Player.velocity, 200, default(Color), 0.7f);
                        d2.fadeIn = 0.0001f;
                        d2.noGravity = true;
                    }
                }
            }
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.RightDebuff>()))
            {
                for (int i = 0; i < 2; i++)
                {//



                    Vector2 vector32 = new Vector2(Player.Center.X + 500, Player.Center.Y);
                    for (int i3 = 0; i3 < 50; i3++)
                    {
                        Vector2 position = Vector2.Lerp(Player.Center, vector32, (float)i3 / 50);
                        Dust d = Dust.NewDustPerfect(position, 20, null, 240, default(Color), 0.3f);
                        d.fadeIn = 0.3f;
                        d.noLight = true;
                        d.noGravity = true;
                    }
                    for (int i2 = 0; i2 < 5; i2++)
                    {//Circle
                        Vector2 offset = new Vector2();
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        offset.X += (float)(Math.Sin(angle) * 30f);
                        offset.Y += (float)(Math.Cos(angle) * 30f);

                        Dust d2 = Dust.NewDustPerfect(vector32 + offset, 20, Player.velocity, 200, default(Color), 0.7f);
                        d2.fadeIn = 0.0001f;
                        d2.noGravity = true;
                    }
                }
            }
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.RedPaint>()))
            {
                for (int i = 0; i < 5; i++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 30f);
                    offset.Y += (float)(Math.Cos(angle) * 30f);

                    Dust d = Dust.NewDustPerfect(Player.Center + offset, 219, Player.velocity, 200, default(Color), 0.7f);
                    d.fadeIn = 0.0001f;
                    d.noGravity = true;
                }
            }
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.BluePaint>()))
            {
                for (int i = 0; i < 5; i++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 30f);
                    offset.Y += (float)(Math.Cos(angle) * 30f);

                    Dust d = Dust.NewDustPerfect(Player.Center + offset, 221, Player.velocity, 200, default(Color), 0.7f);
                    d.fadeIn = 0.0001f;
                    d.noGravity = true;
                }
            }
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.YellowPaint>()))
            {
                for (int i = 0; i < 5; i++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 30f);
                    offset.Y += (float)(Math.Cos(angle) * 30f);

                    Dust d = Dust.NewDustPerfect(Player.Center + offset, 222, Player.velocity, 200, default(Color), 0.7f);
                    d.fadeIn = 0.0001f;
                    d.noGravity = true;
                }
            }
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.Pyretic>()))
            {
                for (int i = 0; i < 30; i++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 70f);
                    offset.Y += (float)(Math.Cos(angle) * 70f);

                    Dust d = Dust.NewDustPerfect(Player.Center + offset, 90, Player.velocity, 200, default(Color), 0.7f);
                    d.fadeIn = 1f;
                    d.noGravity = true;
                }
            }
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.DeepFreeze>()))
            {
                for (int i = 0; i < 30; i++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 70f);
                    offset.Y += (float)(Math.Cos(angle) * 70f);

                    Dust d = Dust.NewDustPerfect(Player.Center + offset, 135, Player.velocity, 200, default(Color), 0.7f);
                    d.fadeIn = 1f;
                    d.noGravity = true;
                }
            }
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.Invincibility>()))//Invincibility VFX
            {
                for (int i = 0; i < 12; i++)
                {

                }

            }
            if (Player.HasBuff(BuffType<Buffs.FlashOfEternity>()))//Astarte Effects
            {
                for (int i = 0; i < 2; i++)
                {
                    if (Main.rand.Next(0, 3) == 0)
                    {
                        // Charging dust
                        Vector2 vector = new Vector2(
                            Main.rand.Next(-6, 6) * (0.003f * 200) - 10,
                            Main.rand.Next(-6, 6) * (0.003f * 200) - 10);
                        Dust d = Main.dust[Dust.NewDust(
                            Player.Center + vector, 1, 1,
                            DustType<Dusts.Butterfly>(), 0, 0, 255,
                            new Color(1f, 1f, 1f), 1.5f)];
                        // d.shader = GameShaders.Armor.GetSecondaryShader(77, Main.LocalPlayer);
                        d.velocity = -vector / 16;
                        d.velocity -= Player.velocity / 8;
                        d.noLight = true;
                        d.noGravity = true;
                    }

                }

            }
            if (Player.HasBuff(BuffType<Buffs.AstarteDriverPrep>()))//Astarte Effects
            {
                for (int i = 0; i < 5; i++)
                {
                    // Charging dust
                    Vector2 vector = new Vector2(
                        Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10,
                        Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10);
                    Dust d = Main.dust[Dust.NewDust(
                        Player.Center + vector, 1, 1,
                        132, 0, 0, 255,
                        new Color(1f, 1f, 1f), 1.5f)];
                    d.shader = GameShaders.Armor.GetSecondaryShader(77, Main.LocalPlayer);
                    d.velocity = -vector / 16;
                    d.velocity -= Player.velocity / 8;
                    d.noLight = true;
                    d.noGravity = true;
                }

            }
            if (Player.HasBuff(BuffType<Buffs.AstarteDriver>()))//Astarte Effects
            {

                for (int i = 0; i < 1; i++)
                {
                    // Charging dust
                    Vector2 vector = new Vector2(
                        Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10,
                        Main.rand.Next(-2048, 2048) * (0.003f * 200) - 10);
                    Dust d = Main.dust[Dust.NewDust(
                        Player.Center + vector, 1, 1,
                        132, 0, 0, 255,
                        new Color(1f, 1f, 1f), 1.5f)];
                    d.shader = GameShaders.Armor.GetSecondaryShader(77, Main.LocalPlayer);
                    d.velocity = -vector / 16;
                    d.velocity -= Player.velocity / 8;
                    d.noLight = true;
                    d.noGravity = true;
                }

            }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.LeftDebuff>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        //player.velocity = Vector2.Zero;
                        Vector2 vector32 = new Vector2(Player.position.X - 500, Player.position.Y);
                        Player.Teleport(vector32, 1, 0);
                        NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, (float)Player.whoAmI, vector32.X, vector32.Y, 1, 0, 0);

                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.RightDebuff>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        //player.velocity = Vector2.Zero;
                        Vector2 vector32 = new Vector2(Player.position.X + 500, Player.position.Y);
                        Player.Teleport(vector32, 1, 0);
                        NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, (float)Player.whoAmI, vector32.X, vector32.Y, 1, 0, 0);
                    }
                }


            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.EyeOfEuthymiaBuff>())
                {
                    if (Player.buffTime[i] == 600)
                    {
                        Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                        CombatText.NewText(textPos, new Color(255, 0, 125, 240), "The Eye of Euthymia has 10 seconds left!", false, false);
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.CoreOfFlames>())
                {
                    if (Player.buffTime[i] == 300)
                    {
                        Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                        CombatText.NewText(textPos, new Color(255, 255, 125, 240), "The Core of Flames has 5 seconds left!", false, false);
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.BurningDesire.BoilingBloodBuff>())
                {
                    if (Player.buffTime[i] == 300)
                    {
                        Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                        CombatText.NewText(textPos, new Color(255, 255, 125, 240), "Boiling Blood has 5 seconds left!", false, false);
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.CoreOfFlamesCooldown>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                        CombatText.NewText(textPos, new Color(255, 255, 125, 240), "Liberation Blazing is ready to strike!", false, false);
                    }
                }

            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.SoulUnbound>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        //Spawn the attacking projectile here.
                        Vector2 placement2 = new Vector2((Player.Center.X), Player.Center.Y);
                        Projectile.NewProjectile(null, placement2.X, placement2.Y, 0, 0, Mod.Find<ModProjectile>("radiateChaos").Type, 0, 0f, 0);
                        Projectile.NewProjectile(null, placement2.X, placement2.Y, 0, 0, Mod.Find<ModProjectile>("UnforgottenBurst").Type, soulUnboundDamage / 3, 0f, 0);
                        Player.AddBuff(BuffType<Buffs.SoulUnboundCooldown>(), 1320);
                        Player.Teleport(soulUnboundLocation, 1, 0);
                        NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, (float)Player.whoAmI, soulUnboundLocation.X, soulUnboundLocation.Y, 1, 0, 0);
                        soulUnboundActive = false;
                        soulUnboundDamage = 0;
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.SeabornWrath>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<Buffs.SeabornCooldown>(), 1800);//7

                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.LivingDead>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<Buffs.LivingDeadCooldown>(), 14400);//7200 is 2 minutes, 14400 is 4 minutes.

                        if (Player.statLife < 150)
                        {
                            Player.DelBuff(i);
                            Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + " crumbled under the weight of Living Dead."), 500, 0);
                        }
                        else
                        {

                        }
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.BurningDesire.BoilingBloodBuff>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<Buffs.BurningDesire.BoilingBloodCooldown>(), 1200);


                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.VermillionDaemon.Retribution>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        SpectralArsenal = 0;
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.ArtificeSirenBuff>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<Buffs.ArtificeSirenCooldown>(), 7200);//7200 is 2 minutes
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<AshenAmbitionEnd>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        if (AshenExecuteKilled)
                        {
                            if (CallOfTheVoid >= 4)
                            {
                                int index2 = Player.FindBuffIndex(BuffType<AshenAmbitionCooldown>());
                                if (index2 > -1)
                                {
                                    Player.DelBuff(index2);
                                }
                                Player.AddBuff(BuffType<AshenStrength>(), 120);

                                Player.AddBuff(BuffType<AshenAmbitionCooldown>(), 600);
                            }
                            else
                            {
                                int index2 = Player.FindBuffIndex(BuffType<AshenAmbitionCooldown>());
                                if (index2 > -1)
                                {
                                    Player.DelBuff(index2);
                                }
                                Player.AddBuff(BuffType<AshenAmbitionCooldown>(), 120);

                            }

                            CallOfTheVoid++;
                            Player.velocity.Y += 12;
                        }
                        else
                        {
                            CallOfTheVoid = 0;
                            NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, (float)Player.whoAmI, Player.GetModPlayer<StarsAbovePlayer>().AshenAmbitionOldPosition.X, Player.GetModPlayer<StarsAbovePlayer>().AshenAmbitionOldPosition.Y, 1, 0, 0);
                        }
                        Player.Teleport(Player.GetModPlayer<StarsAbovePlayer>().AshenAmbitionOldPosition, 1, 0);

                        AshenExecuteKilled = false;
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<AshenAmbitionPrep>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<AshenAmbitionActive>(), 60);//7200 is 2 minutes
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.CosmicDestroyer.MagitonOverheat>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<Buffs.CosmicDestroyer.Overheated>(), 60);//7200 is 2 minutes
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.TakodachiLaserBuff>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<Buffs.TakodachiLaserBuffCooldown>(), 3600);//3600 is 1 minute
                    }
                }

            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.StarshieldBuff>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<Buffs.StarshieldCooldown>(), 1200);//
                    }
                }
            /*
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.Wormhole>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y - 100), Vector2.Zero, Mod.Find<ModProjectile>("Gateway").Type, 0, 0, Player.whoAmI, 0, 1);
                        for (int d = 0; d < 30; d++)
                        {
                            Dust.NewDust(Player.position, Player.width, Player.height, 20, 0f + Main.rand.Next(-30, 30), 0f + Main.rand.Next(-30, 30), 150, default(Color), 1.5f);
                        }

                        for (int d = 0; d < 36; d++)
                        {
                            Dust.NewDust(Player.position, Player.width, Player.height, 221, 0f + Main.rand.Next(-36, 36), 0f + Main.rand.Next(-36, 36), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 30; d++)
                        {
                            Dust.NewDust(Player.position, Player.width, Player.height, 7, 0f + Main.rand.Next(-33, 33), 0f + Main.rand.Next(-33, 33), 150, default(Color), 1.5f);
                        }
                    }
                }


            if (player.HasBuff(BuffType<Wormhole>()))
            {
                int index = player.FindBuffIndex(BuffType<Wormhole>());
                if (index > -1)
                {
                    player.DelBuff(index);
                }
                //Projectile.NewProjectile(null,new Vector2(player.Center.X, player.Center.Y), Vector2.Zero, mod.ProjectileType("GatewayVFX"), 0, 0, player.whoAmI, 0, 1);                                                                                                                                                             //Vector2 mousePosition = Main.MouseWorld;
                
            }*/

            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.Pyretic>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        if (Player.velocity != Vector2.Zero)
                        {
                            Player.AddBuff(BuffID.OnFire, 180);
                            Player.statLife -= 50;
                        }

                        if (Player.statLife < 0)
                        {
                            Player.DelBuff(i);
                            Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + " burnt to a crisp by continuing to move during Pyretic."), 0, 0);
                        }
                        else
                        {

                        }
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.AstarteDriverPrep>())
                {



                    if (Player.buffTime[i] == 1)
                    {
                        onActivateStellarNova();
                        astarteDriverAttacks = 3;
                        Player.AddBuff(BuffType<Buffs.AstarteDriver>(), 1500);
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_summoning, Player.Center);
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(Player.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(Player.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default(Color), 1.5f);
                        }
                    }

                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.AstarteDriver>())
                {

                    for (int a = 0; a < 15; a++)
                    {//Circle
                        Vector2 vector = new Vector2(
                    Main.rand.Next(-48, 48) * (0.003f * 40 - 10),
                    Main.rand.Next(-48, 48) * (0.003f * 40 - 10));
                        Dust d3 = Main.dust[Dust.NewDust(
                            Player.MountedCenter + vector, 1, 1,
                            88, 0, 0, 255,
                            new Color(0.8f, 0.4f, 1f), 0.8f)];
                        d3.velocity = -vector / 12;
                        d3.velocity -= Player.velocity / 8;
                        d3.noLight = true;
                        d3.noGravity = true;
                        Vector2 vector2 = new Vector2(
                            Main.rand.Next(-48, 48) * (0.003f * 40 - 10),
                            Main.rand.Next(-48, 48) * (0.003f * 40 - 10));
                        Dust d4 = Main.dust[Dust.NewDust(
                            Player.MountedCenter + vector, 1, 1,
                            86, 0, 0, 255,
                            new Color(0.8f, 0.4f, 1f), 0.8f)];
                        d4.velocity = -vector / 12;
                        d4.velocity -= Player.velocity / 8;
                        d4.noLight = true;
                        d4.noGravity = true;
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.DeepFreeze>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        if (Player.velocity == Vector2.Zero)
                        {
                            Player.AddBuff(BuffID.Frozen, 120);
                            Player.statLife -= 50;
                        }

                        if (Player.statLife < 0)
                        {
                            Player.DelBuff(i);
                            Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + " froze to death by staying still during Deep Freeze"), 0, 0);
                        }
                        else
                        {

                        }
                    }
                }

            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<MoonlitGreatblade>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<MoonlitCooldown>(), 480);

                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.BerserkerMode>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<Buffs.BerserkerModeCooldown>(), 1800);

                    }
                }

            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.KiwamiRyuken>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        if (chosenStarfarer == 2)
                        {
                            novaGauge = (trueNovaGaugeMax / 2) + 10;
                        }
                        else
                        {
                            novaGauge += trueNovaGaugeMax / 2;
                        }

                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Buffs.KiwamiRyukenConfirm>())
                {
                    if (Player.buffTime[i] == 1)
                    {

                        Player.DelBuff(i);
                        float launchSpeed = -13f;
                        Vector2 mousePosition = Main.MouseWorld;
                        Vector2 direction = Vector2.Normalize(mousePosition - Player.Center);
                        Player.velocity = direction * launchSpeed;

                        Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y), direction, Mod.Find<ModProjectile>("kiwamiryukenconfirm").Type, novaDamage, 40, Player.whoAmI, 0, 1);                                                                                                                                                             //Vector2 mousePosition = Main.MouseWorld;

                    }
                }

            if (starfarerOutfit == 3) // Celestial Princess' Genesis
            {

                if (!Player.HasBuff(BuffType<AstarteDriver>()))
                {
                    Player.GetDamage(DamageClass.Generic) -= 0.7f;
                }
                if (Player.HasBuff(BuffType<AstarteDriver>()))
                {
                    Player.GetDamage(DamageClass.Generic) += 0.15f;
                }
            }
            if (starfarerOutfit == 4) // Aegis of Hope's Legacy
            {
                Player.aggro += 5;
                Player.AddBuff(BuffType<HopesBrillianceBuff>(), 2);
                if (hopesBrilliance > hopesBrillianceMax)
                {
                    hopesBrilliance = hopesBrillianceMax;
                }

                if (beyondinfinity == 2)
                {
                    Player.GetDamage(DamageClass.Generic) += 0.3f;
                }
                if (keyofchronology == 2)
                {

                }
                if (beyondtheboundary == 2)
                {
                    if (Player.HasBuff(BuffType<Flow>()))
                    {
                        Player.statDefense += 40;
                    }
                    if (Player.HasBuff(BuffType<Ebb>()))
                    {
                        Player.statDefense -= 20;
                    }
                }
                if (unbridledradiance == 2)
                {
                    if (novaGauge < 10)
                    {
                        novaGauge = 10;
                    }
                }

            }

            base.PreUpdateBuffs();
        }
        public override void PostUpdateBuffs()
        {


            




            applySuistrumeBuff();
            if (Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceEnding == true)
            {
                Player.statLife = Player.statLifeMax2;
                Player.statMana = Player.statManaMax2;
                Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceEnding = false;
            }

            if (Player.GetModPlayer<StarsAbovePlayer>().lifeforce < 0 && !Player.immune)
            {
                Player.GetModPlayer<StarsAbovePlayer>().lifeforce = 100;

                Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + "'s life was drained away."), 500, 0);


                SoundEngine.PlaySound(StarsAboveAudio.SFX_Death, Player.Center);

                for (int d = 0; d < 70; d++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, 60, 0f, 0f, 150, default(Color), 1.5f);

                }
            }
            if (Player.immune && lifeforce < 50)
            {
                lifeforce++;
            }
            base.PostUpdateBuffs();
        }

        private void applySuistrumeBuff()
        {
            foreach (Player buffPlayer in Main.player)
            {
                if (!buffPlayer.active || buffPlayer.dead) continue;
                if (!buffPlayer.GetModPlayer<StarsAbovePlayer>().stellarPerformancePostPrep == true)
                    continue;

                foreach (Player otherPlayer in Main.player)
                {
                    if (SameTeam(otherPlayer, buffPlayer))
                    {
                        if (
                                otherPlayer.position.X >= buffPlayer.position.X - 500 &&
                                otherPlayer.position.X <= buffPlayer.position.X + 500 &&
                                otherPlayer.position.Y >= buffPlayer.position.Y - 500 &&
                                otherPlayer.position.Y <= buffPlayer.position.Y + 500)
                        {
                            otherPlayer.AddBuff(BuffType<Buffs.StellarListener>(), 2);
                        }
                    }
                }
            }
        }
        private void activatea()
        {
            foreach (Player buffPlayer in Main.player)
            {
                if (!buffPlayer.active || buffPlayer.dead) continue;

                foreach (Player otherPlayer in Main.player)
                {
                    if (SameTeam(otherPlayer, buffPlayer))
                    {
                        if (
                            otherPlayer.position.X >= buffPlayer.position.X - 1000 &&
                            otherPlayer.position.X <= buffPlayer.position.X + 1000 &&
                            otherPlayer.position.Y >= buffPlayer.position.Y - 1000 &&
                            otherPlayer.position.Y <= buffPlayer.position.Y + 1000)
                        {
                            otherPlayer.AddBuff(BuffType<Buffs.GardenOfAvalon>(), (((int)(novaDamage * (1 + novaDamageMod)) / 250)) * 60);
                            if (chosenStarfarer == 2)
                            {
                                otherPlayer.AddBuff(BuffType<Buffs.DreamlikeCharisma>(), (((int)(novaDamage * (1 + novaDamageMod)) / 250)) * 60);

                            }
                            if (chosenStarfarer == 1)
                            {
                                otherPlayer.AddBuff(BuffType<Buffs.Invincibility>(), (4 * 60));
                                otherPlayer.statLife += 100;

                            }
                            else
                            {
                                otherPlayer.AddBuff(BuffType<Buffs.Invincibility>(), (2 * 60));
                            }
                        }

                    }
                }

            }
        }
        public override bool CanBeHitByProjectile(Projectile proj)
        {
            if (proj.type == Mod.Find<ModProjectile>("RedSplatterDamage").Type)
            {
                if (Main.LocalPlayer.HasBuff(BuffType<Buffs.RedPaint>()))
                {

                    return false;
                }
            }
            if (proj.type == Mod.Find<ModProjectile>("YellowSplatterDamage").Type)
            {
                if (Main.LocalPlayer.HasBuff(BuffType<Buffs.YellowPaint>()))
                {

                    return false;
                }
            }
            if (proj.type == Mod.Find<ModProjectile>("BlueSplatterDamage").Type)
            {
                if (Main.LocalPlayer.HasBuff(BuffType<Buffs.BluePaint>()))
                {

                    return false;
                }
            }
            return true;
        }
        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {


            base.ModifyHitByProjectile(proj, ref damage, ref crit);
        }
        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            if (proj.type == Mod.Find<ModProjectile>("BlazingSkies").Type
                || proj.type == Mod.Find<ModProjectile>("SaberDamage").Type
                || proj.type == Mod.Find<ModProjectile>("SolemnConfiteorDamage").Type
                || proj.type == Mod.Find<ModProjectile>("TheBitterEnd").Type
                               )
            {
                if (Main.expertMode == true)
                {
                    Main.LocalPlayer.AddBuff(BuffType<Buffs.Vulnerable>(), 1800);
                }
            }

            base.OnHitByProjectile(proj, damage, crit);
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
        {
            

            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.Invincibility>()))
            {
                return false;
            }
            if (Player.HasBuff(BuffType<GuntriggerParry>()))
            {

                Player.AddBuff(BuffType<JetstreamBloodshed>(), 10);
                Player.ClearBuff(BuffType<ImpactRecoil>());
                Player.AddBuff(BuffType<Invincibility>(), 20);
                Projectile.NewProjectile(null, Player.Center.X, Player.Center.Y, 0, 0, ProjectileType<fastRadiate>(), 0, 0, Player.whoAmI, 0f);
                SoundEngine.PlaySound(StarsAboveAudio.SFX_GuntriggerParry, Player.Center);

                if (Player.statLife <= 100)
                {
                    Player.ClearBuff(BuffType<GuntriggerParryCooldown>());
                }
                return false;
            }
            if (Player.HasBuff(BuffType<JetstreamBloodshed>()) && !Player.HasBuff(BuffType<GuntriggerParry>()))
            {
                Player.ClearBuff(BuffType<JetstreamBloodshed>());
            }

            if (!Player.HasBuff(BuffType<StarshieldBuff>()) && !Player.HasBuff(BuffType<StarshieldCooldown>()) && starshower == 2)
            {
                Player.AddBuff(BuffType<StarshieldBuff>(), 120);
                Projectile.NewProjectile(null, Player.Center, Vector2.Zero, ProjectileType<Starshield>(), 0, 0, Player.whoAmI);
                Player.statMana += 20;
            }
            if(Player.HasBuff(BuffType<Bedazzled>()))//If the player has a Prismic...
            {
                CatalystPrismicHP -= (int)(damage * 0.8);//The Prismic absorbs 80% of the damage taken.
                damage = (int)(damage * 0.2);
                //VFX to show the interaction.
                for (int i = 0; i < 100; i++)
                {
                    Vector2 position = Vector2.Lerp(Player.Center, CatalystPrismicPosition, (float)i / 100);
                    Dust d = Dust.NewDustPerfect(position, DustID.PurpleCrystalShard, null, 240, default(Color), 0.6f);
                    d.fadeIn = 0.3f;
                    d.noLight = true;
                    d.noGravity = true;

                }

                //If the Prismic has less than 1 HP...
                if (CatalystPrismicHP < 1)
                {
                    //The Prismic shatters.
                    Player.ClearBuff(BuffType<Bedazzled>());
                    Player.AddBuff(BuffType<DazzlingAria>(), 120);
                }

            }


            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.SolemnAegis>()) && !Main.LocalPlayer.HasBuff(BuffType<Buffs.Invincibility>()))
            {
                for (int d = 0; d < 16; d++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, 221, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                }
                Player.immune = true;
                Player.immuneTime = 30;
                int index = Player.FindBuffIndex(BuffType<SolemnAegis>());
                if (index > -1)
                {
                    Player.DelBuff(index);
                }
                return false;

            }
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.UniversalManipulation>()))
            {

                int index = Player.FindBuffIndex(BuffType<UniversalManipulation>());
                if (index > -1)
                {
                    Player.DelBuff(index);
                }
                return true;

            }
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.FlashOfEternity>()))
            {
                Player.immune = true;
                Player.immuneTime = 30;
                int index = Player.FindBuffIndex(BuffType<FlashOfEternity>());
                if (index > -1)
                {
                    Player.DelBuff(index);
                }
                return false;

            }
            if (evasionmastery == 2)
            {
                if (Main.rand.Next(0, 101) <= 3)
                {
                    Player.immuneTime = 30;
                    return false;
                }
            }
            if (hikari == 2)
            {
                Player.AddBuff(BuffType<NullRadiance>(), 360);
            }
            if (euthymiaActive)
            {
                eternityGauge -= damage / 2;
            }
            if (bloomingflames == 2)
            {
                Player.AddBuff(BuffType<InfernalEnd>(), 180);
            }
            if (damage >= 250 && damage < Player.statLife)
            {
                if (Main.rand.Next(0, 8) == 0)
                {
                    starfarerPromptActive("onTakeHeavyDamage");

                }
            }

            if (Player.HasBuff(BuffType<Buffs.SakuraVengeance.SakuraEarthBuff>()) || Player.HasBuff(BuffType<Buffs.SakuraVengeance.ElementalChaos>()))
            {
                damage = (int)Math.Round(damage * 0.75);
            }

            if (!Main.dedServ)
            {
                inCombat = 1200;
                timeAfterGettingHit = 0;
                if (ruinedKingPrism)
                {
                    if (novaGauge >= 2)
                    {
                        novaGauge -= 2;

                    }
                }
                if (cosmicPhoenixPrism)
                {
                    if (Player.statLife - damage < 50 && !Main.LocalPlayer.HasBuff(BuffType<Buffs.GoingSupercriticalCooldown>()))
                    {
                        novaGauge = novaGaugeMax;
                        Main.LocalPlayer.AddBuff(BuffType<Buffs.GoingSupercriticalCooldown>(), 7200);
                        Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                        CombatText.NewText(textPos, new Color(255, 0, 125, 240), "Going supercritical!", false, false);
                    }
                }
                if (chosenStarfarer == 2 && novaGaugeUnlocked)
                {

                }
                if (chosenStarfarer == 1 && novaGaugeUnlocked)
                {

                }
                if (Main.LocalPlayer.HasBuff(BuffType<Buffs.KiwamiRyuken>()))
                {
                    Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().NovaCutInTimer = 140;
                    Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().NovaCutInVelocity = 20;
                    Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().NovaCutInX = 0;
                    Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().NovaCutInOpacity = 0;
                    Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().randomNovaDialogue = Main.rand.Next(0, 6);
                    if (!voicesEnabled)
                    {

                        if (chosenStarfarer == 1)
                        {
                            if (randomNovaDialogue == 0)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.AN0, Player.Center);
                            }
                            if (randomNovaDialogue == 1)
                            {
                                if (chosenStellarNova == 0)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.AN11, Player.Center);
                                }
                                if (chosenStellarNova == 0)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.AN12, Player.Center);
                                }
                                if (chosenStellarNova == 0)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.AN13, Player.Center);
                                }
                                if (chosenStellarNova == 0)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.AN14, Player.Center);
                                }
                                if (chosenStellarNova == 0)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.AN15, Player.Center);
                                }

                            }
                            if (randomNovaDialogue == 2)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.AN2, Player.Center);
                            }
                            if (randomNovaDialogue == 3)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.AN3, Player.Center);
                            }
                            if (randomNovaDialogue == 4)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.AN4, Player.Center);
                            }
                            if (randomNovaDialogue == 5)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.AN5, Player.Center);
                            }

                        }
                        if (chosenStarfarer == 2)
                        {
                            if (randomNovaDialogue == 0)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.EN0, Player.Center);
                            }
                            if (randomNovaDialogue == 1)
                            {
                                if (chosenStellarNova == 0)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.EN11, Player.Center);
                                }
                                if (chosenStellarNova == 0)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.EN12, Player.Center);
                                }
                                if (chosenStellarNova == 0)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.EN13, Player.Center);
                                }
                                if (chosenStellarNova == 0)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.EN14, Player.Center);
                                }
                                if (chosenStellarNova == 0)
                                {
                                    SoundEngine.PlaySound(StarsAboveAudio.EN15, Player.Center);
                                }

                            }
                            if (randomNovaDialogue == 2)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.EN2, Player.Center);
                            }
                            if (randomNovaDialogue == 3)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.EN3, Player.Center);
                            }
                            if (randomNovaDialogue == 4)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.EN4, Player.Center);
                            }
                            if (randomNovaDialogue == 5)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.EN5, Player.Center);
                            }

                        }



                    }
                    ryukenTimer = 60;
                    int index = Player.FindBuffIndex(BuffType<KiwamiRyuken>());
                    if (index > -1)
                    {
                        Player.DelBuff(index);
                    }
                    if (chosenStarfarer == 2)
                    {

                        Player.statLife += 40;

                        Player.statMana += 40;

                    }
                    Vector2 mousePosition = Main.MouseWorld;
                    Vector2 direction = Vector2.Normalize(mousePosition - Player.Center);
                    Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y), direction, Mod.Find<ModProjectile>("kiwamiryukenstun").Type, 1, 0, Player.whoAmI, 0, 1);
                    onActivateStellarNova();
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_CounterImpact, Player.Center);
                    damage = 0;
                    playSound = false;
                    return true;
                }

                if (Player.GetModPlayer<StarsAbovePlayer>().keyofchronology == 2)
                {
                    if (damage >= 200 && !(Main.LocalPlayer.HasBuff(BuffType<Buffs.KeyOfChronologyCooldown>())))
                    {
                        if (starfarerPromptCooldown > 0)
                        {
                            starfarerPromptCooldown = 0;
                        }
                        starfarerPromptActive("onKeyOfChronology");
                        /*
                        if (chosenStarfarer == 1)
                        {
                            if (Player.ownedProjectileCounts[Mod.Find<ModProjectile>("AsphodeneBurst").Type] < 1)
                                Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("AsphodeneBurst").Type, 0, 0, Player.whoAmI, 0, 1);

                        }
                        if (chosenStarfarer == 2)
                        {
                            if (Player.ownedProjectileCounts[Mod.Find<ModProjectile>("EridaniBurst").Type] < 1)
                                Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("EridaniBurst").Type, 0, 0, Player.whoAmI, 0, 1);

                        }*/
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_TimeEffect, Player.Center);
                        for (int d = 0; d < 12; d++)
                        {
                            Dust.NewDust(Player.position, 0, 0, 113, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 1.5f);
                        }
                        Player.statLife += damage;
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            NPC npc = Main.npc[i];
                            if (npc.active && !npc.boss && npc.Distance(Player.Center) < 1000)
                            {
                                npc.AddBuff(BuffType<Stun>(), 300);
                            }
                        }

                        Main.LocalPlayer.AddBuff(BuffType<Buffs.Invincibility>(), 300);
                        Main.LocalPlayer.AddBuff(BuffType<Buffs.KeyOfChronologyCooldown>(), 7200);

                        if (starfarerOutfit == 4)
                        {
                            Player.ClearBuff(BuffID.PotionSickness);
                            Player.potionDelay = 0;
                            Player.potionDelayTime = 0;
                            Player.AddBuff(BuffID.Heartreach, 1800);

                        }

                        return false;
                    }

                    return true;
                }
                if (Main.LocalPlayer.HasBuff(BuffType<Buffs.ButterflyTrance>()))
                {
                    damage /= 2;

                    return true;
                }
                if (Player.GetModPlayer<StarsAbovePlayer>().astralmantle == 2)
                {

                    return true;
                }

                if (Player.GetModPlayer<StarsAbovePlayer>().aquaaffinity == 2)
                {

                    return true;
                }
                if (Player.GetModPlayer<StarsAbovePlayer>().ironskin == 2)
                {
                    if (damage >= 100)
                    {
                        damage -= 30;
                    }
                    if (Player.statLife < 100)
                    {
                        damage = (int)(damage * 0.8f);
                    }

                    return true;
                }
                if (Player.GetModPlayer<StarsAbovePlayer>().starfarerOutfit == 3)
                {

                    damage = (int)(damage * 0.9f);
                    novaGauge += 3;

                    return true;
                }
                if (starfarerOutfit == 4) // Aegis of Hope's Legacy
                {
                    hopesBrilliance++;
                    Player.AddBuff(BuffType<NascentAria>(), 180);
                    Player.AddBuff(BuffID.Ironskin, 480);
                    Player.AddBuff(BuffID.Regeneration, 480);
                    Player.AddBuff(BuffID.Endurance, 480);


                }

                if (Main.LocalPlayer.HasBuff(BuffType<Buffs.DashInvincibility>()))
                {
                    damage = 0;

                    return true;
                }
                if (Player.GetModPlayer<StarsAbovePlayer>().stellarPerformancePostPrep == true)
                {
                    if (damage > 20)
                    {
                        Player.GetModPlayer<StarsAbovePlayer>().PerformanceResourceCurrent -= 20;

                    }
                    else
                    {
                        Player.GetModPlayer<StarsAbovePlayer>().PerformanceResourceCurrent -= damage;
                    }
                }
            }

            if (starfarerOutfit == 1)
            {
                Player.AddBuff(BuffType<Vulnerable>(), 240);
            }

            return true;
        }


        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {

            if (SubworldSystem.Current != null)
            {
                int randomMessage = Main.rand.Next(0, 5);
                if (randomMessage == 0)
                {
                    damageSource = PlayerDeathReason.ByCustomReason(Player.name + " died beyond their world.");

                }
                if (randomMessage == 1)
                {
                    damageSource = PlayerDeathReason.ByCustomReason(Player.name + " was lost in space.");

                }
                if (randomMessage == 2)
                {
                    damageSource = PlayerDeathReason.ByCustomReason(Player.name + " drifted away from their home planet.");

                }
                if (randomMessage == 3)
                {
                    damageSource = PlayerDeathReason.ByCustomReason(Player.name + " was brought to kneel beyond their world.");

                }
                if (randomMessage == 4)
                {
                    damageSource = PlayerDeathReason.ByCustomReason(Player.name + " died within another realm.");

                }
            }

            if (NPC.AnyNPCs(Mod.Find<ModNPC>("WarriorOfLight").Type))
            {
                if (inWarriorOfLightFightTimer <= 0)
                {

                }
                else
                {
                    damageSource = PlayerDeathReason.ByCustomReason(Player.name + " was struck down during their duel with The Warrior Of Light.");
                }

                return true;
            }
            if (NPC.AnyNPCs(Mod.Find<ModNPC>("Penthesilea").Type))
            {
                if (inPenthFightTimer <= 0)
                {

                }
                else
                {
                    damageSource = PlayerDeathReason.ByCustomReason(Player.name + " faltered during their fight with the Witch of Ink.");
                }

                return true;
            }
            if (NPC.AnyNPCs(Mod.Find<ModNPC>("Nalhaun").Type))
            {
                if (inNalhaunFightTimer <= 0)
                {

                }
                else
                {
                    damageSource = PlayerDeathReason.ByCustomReason(Player.name + " was cleaved in twain by the Burnished King.");
                }

                return true;
            }
            if (NPC.AnyNPCs(Mod.Find<ModNPC>("Arbitration").Type))
            {
                if (inArbiterFightTimer <= 0)
                {

                }
                else
                {
                    damageSource = PlayerDeathReason.ByCustomReason(Player.name + " was found wanting.");
                }

                return true;
            }
            if (NPC.AnyNPCs(Mod.Find<ModNPC>("Tsukiyomi").Type))
            {
                if (inTsukiyomiFightTimer <= 0)
                {

                }
                else
                {
                    damageSource = PlayerDeathReason.ByCustomReason(Player.name + " has been completely reduced to elementary particles.");
                }

                return true;
            }
            if (NPC.AnyNPCs(Mod.Find<ModNPC>("VagrantOfSpaceAndTime").Type))
            {
                if (inVagrantFightTimer <= 0)
                {

                }
                else
                {
                    damageSource = PlayerDeathReason.ByCustomReason(Player.name + " was rent asunder by the Vagrant of Space and Time.");
                }

                return true;
            }
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.LivingDead>()))
            {
                playSound = false;
                genGore = false;
                Player.statLife = 1;

                return false;
            }
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.SpatialBurn>()))
            {

                damageSource = PlayerDeathReason.ByCustomReason(Player.name + " couldn't handle the vacuum of space.");

                return true;
            }
            if (Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceActive == true)
            {


                Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceSoundInstance?.Stop();
                SoundEngine.PlaySound(StarsAboveAudio.SFX_SuistrumeFail, Player.Center);
                Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceActive = false;
                Player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceEnding = true;
                Player.GetModPlayer<StarsAbovePlayer>().stellarPerformancePostPrep = false;
                Player.GetModPlayer<StarsAbovePlayer>().PerformanceResourceCurrent = 0;

            }

            if (willpowerOfZagreus)
            {

                if (!Main.LocalPlayer.HasBuff(BuffType<Buffs.DeathDefianceCooldown>()))
                {


                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = Player.Center;
                    dust = Main.dust[Terraria.Dust.NewDust(position, 0, 0, 247, 0f, 0f, 0, new Color(255, 0, 0), 1f)];

                    Player.AddBuff(BuffType<Buffs.DeathDefianceCooldown>(), 28800);

                    Player.statLife = Player.statLifeMax / 2;
                    return false;


                }
            }
            if (livingdead == 2)
            {

                if (!Main.LocalPlayer.HasBuff(BuffType<Buffs.LivingDead>()))
                {
                    if (!Main.LocalPlayer.HasBuff(BuffType<Buffs.LivingDeadCooldown>()))
                    {
                        if (starfarerPromptCooldown > 0)
                        {
                            starfarerPromptCooldown = 0;
                        }
                        starfarerPromptActive("onLivingDead");
                        /*
                        if (chosenStarfarer == 1)
                        {
                            if (Player.ownedProjectileCounts[Mod.Find<ModProjectile>("AsphodeneBurst").Type] < 1)
                                Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("AsphodeneBurst").Type, 0, 0, Player.whoAmI, 0, 1);

                        }
                        if (chosenStarfarer == 2)
                        {
                            if (Player.ownedProjectileCounts[Mod.Find<ModProjectile>("EridaniBurst").Type] < 1)
                                Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("EridaniBurst").Type, 0, 0, Player.whoAmI, 0, 1);

                        }*/
                        Player.AddBuff(BuffType<Buffs.LivingDead>(), 600);
                        Dust dust;
                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        Vector2 position = Player.Center;
                        dust = Main.dust[Terraria.Dust.NewDust(position, 0, 0, 247, 0f, 0f, 0, new Color(255, 0, 0), 1f)];


                        Player.statLife = 1;
                        return false;
                    }

                }
                else
                {
                    playSound = false;
                    genGore = false;
                    Player.statLife = 1;

                    return false;

                }
                return true;
            }
            if (darkHourglass)
            {

                if (!Main.LocalPlayer.HasBuff(BuffType<Buffs.LivingDead>()))
                {
                    if (!Main.LocalPlayer.HasBuff(BuffType<Buffs.LivingDeadCooldown>()))
                    {
                        Player.AddBuff(BuffType<Buffs.LivingDead>(), 600);
                        Dust dust;
                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        Vector2 position = Main.LocalPlayer.Center;
                        dust = Main.dust[Terraria.Dust.NewDust(position, 0, 0, 247, 0f, 0f, 0, new Color(255, 0, 0), 1f)];


                        Player.statLife = 1;
                        return false;
                    }

                }
                else
                {
                    playSound = false;
                    genGore = false;
                    Player.statLife = 1;

                    return false;

                }
                return true;
            }

            if (MajimaPet)
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_BakaMitai, Player.Center);
            }
            if (Ghost)
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_GuardianDown, Player.Center);
            }
            if (Main.rand.Next(0, 5) == 0)
            {
                starfarerPromptActive("onDeath");

            }
            inWarriorOfLightFightTimer = 0;
            inVagrantFightTimer = 0;
            inNalhaunFightTimer = 0;
            inPenthFightTimer = 0;
            inArbiterFightTimer = 0;
            butchersDozenKills = 0;
            return true;
        }
        public void starfarerPromptActive(string eventPrompt)
        {
            if (starfarerPromptCooldown <= 0 && !promptIsActive && !disablePrompts && chosenStarfarer != 0)
            {
                //If the check was successful...
                SoundEngine.PlaySound(SoundID.MenuOpen, Player.position); //Menu sound here
                promptIsActive = true;
                starfarerPromptActiveTimer = 210;
                if (eventPrompt == "Tsukiyomi1")
                {
                    starfarerPromptActiveTimer = 350;

                }
                promptMoveIn = 15f;
                int randomDialogue;

                //0 Neutral | 1 Dissatisfied | 2 Angry | 3 Smug | 4 Questioning | 5 Sigh | 6 Intrigued
                if (chosenStarfarer == 1) //Asphodene's lines                                          | Soft limit
                {
                    
                    //When the player is debuffed..
                    if (eventPrompt == "onPoison")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.1", Player.name);//Looks like you've been poisoned. I'll send you a get well card.
                    }
                    if (eventPrompt == "onIchor")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.2", Player.name); //It looks like your defenses are lowered! Be careful.
                    }
                    if (eventPrompt == "onSilence")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.3", Player.name); //You've been silenced! Magic is out of the question.
                    }
                    if (eventPrompt == "onCurse")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.4", Player.name); //I think you've been cursed. That's.. not good. You aren't able to use anything for now.
                    }
                    if (eventPrompt == "onFrozen")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.5", Player.name); //You've been frozen! 
                    }
                    if (eventPrompt == "onFrostburn")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.6", Player.name); //You're burning up? Or freezing? It looks like it hurts regardless!
                        //promptDialogue = "You're burning up! Or freezing? " +
                        //    " ..It hurts regardless!" +
                        //    " ";
                    }
                    if (eventPrompt == "onWebbed")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.7", Player.name); //Ew. It looks like you're covered in webs.. 
                    }
                    if (eventPrompt == "onStoned")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.8", Player.name); //You've been petrified! How does that even work? 
                    }
                    if (eventPrompt == "onBurning")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.9", Player.name); //You're standing on something REALLY hot. I'm no expert on thermodynamics, but you should not do that.
                    }
                    if (eventPrompt == "onSuffocation")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.10", Player.name); //You're dying- and quickly. Get out of there! 
                    }
                    if (eventPrompt == "onFire")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.11", Player.name); //That's probably not good.
                    }
                    if (eventPrompt == "onDrowning")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.12", Player.name); //I hate to break this to you, but you're about to drown.
                    }
                    if (eventPrompt == "onBleeding")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.13", Player.name); //You're losing a LOT of blood. Yikes. You can't regenerate health any more.
                    }
                    if (eventPrompt == "onConfusion")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.14", Player.name); //[You can't make out the words- looks like you've been confused.] 
                    }

                    //During combat..
                    if (eventPrompt == "onKillCritter")
                    {
                        starfarerPromptActiveTimer = starfarerPromptActiveTimerSetting;
                        randomDialogue = Main.rand.Next(0, 3);
                        promptExpression = 5;
                        if (randomDialogue == 0)
                        {
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.15", Player.name); //Whoops..
                        }
                        if (randomDialogue == 1)
                        {
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.16", Player.name); //Sorry, little guy.
                        }
                        if (randomDialogue == 2)
                        {
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.17", Player.name); //Oops.
                        }
                    }
                    if (eventPrompt == "onKillEnemy")
                    {
                        randomDialogue = Main.rand.Next(0, 20);
                        starfarerPromptActiveTimer = starfarerPromptActiveTimerSetting;
                        if (randomDialogue == 0)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.18", Player.name); //That's one down.
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 6;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.19", Player.name); //One more defeated!
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.20", Player.name); //That takes care of that.
                        }
                        if (randomDialogue == 3)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.21", Player.name); //Another one down.
                        }
                        if (randomDialogue == 4)
                        {
                            promptExpression = 6;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.22", Player.name); //You're great at this!
                        }
                        if (randomDialogue == 5)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.23", Player.name); //Nice work. That's one down.
                        }
                        if (randomDialogue == 6)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.24", Player.name); //There it goes!
                        }
                        if (randomDialogue == 7)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.25", Player.name); //That'll teach em!
                        }
                        if (randomDialogue == 8)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.26", Player.name); //That'll show them!
                        }
                        if (randomDialogue == 9)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.27", Player.name); //Don't mess with us!
                        }
                        if (randomDialogue == 10)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.28", Player.name); //Easy!
                        }
                        if (randomDialogue == 11)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.29", Player.name); //Didn't even break a sweat.
                        }
                        if (randomDialogue == 12)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.30", Player.name); //That was so easy!
                        }
                        if (randomDialogue == 13)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.31", Player.name); //Enough of that one.
                        }
                        if (randomDialogue == 14)
                        {
                            promptExpression = 6;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.32", Player.name); //An easy victory.
                        }
                        if (randomDialogue == 15)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.33", Player.name); //Right, that's that.
                        }
                        if (randomDialogue == 16)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.34", Player.name); //How could we ever lose?
                        }
                        if (randomDialogue == 17)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.35", Player.name); //It's over.
                        }
                        if (randomDialogue == 18)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.36", Player.name); //No more of that.
                        }
                        if (randomDialogue == 19)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.37", Player.name); //A good encounter.
                        }
                    }
                    if (eventPrompt == "onKillEnemyDanger")
                    {
                        starfarerPromptActiveTimer = starfarerPromptActiveTimerSetting;
                        randomDialogue = Main.rand.Next(0, 3);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.38", Player.name); //That was close..
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.39", Player.name); //That was almost really bad.
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.40", Player.name); //Thank goodness we killed it in time.
                        }
                    }
                    if (eventPrompt == "onKillBossEnemy")
                    {
                        starfarerPromptActiveTimer = 150;
                        randomDialogue = Main.rand.Next(0, 3);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.41", Player.name); //Finally. You got it!
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 6;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.42", Player.name); //And that takes care of that one.
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.43", Player.name); //That was a strong one.
                        }
                    }
                    if (eventPrompt == "onCrit")
                    {
                        starfarerPromptActiveTimer = starfarerPromptActiveTimerSetting;
                        randomDialogue = Main.rand.Next(0, 6);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.44", Player.name); //A decisive crit!
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 6;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.45", Player.name); //They felt that one!
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.46", Player.name); //A perfectly-timed attack.
                        }
                        if (randomDialogue == 3)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.47", Player.name); //Nice, you hit their weak spot.
                        }
                        if (randomDialogue == 4)
                        {
                            promptExpression = 6;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.48", Player.name); //A critical hit..!
                        }
                        if (randomDialogue == 5)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.49", Player.name); //Well struck. That was definitely critical.
                        }
                    }
                    if (eventPrompt == "onTakeHeavyDamage")
                    {
                        starfarerPromptActiveTimer = starfarerPromptActiveTimerSetting;
                        randomDialogue = Main.rand.Next(0, 6);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.50", Player.name); //Wow. I felt that one..
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 5;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.51", Player.name); //That's.. not good.
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.52", Player.name); //Ouch...
                        }
                        if (randomDialogue == 3)
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.53", Player.name); //That wasn't good at all..
                        }
                        if (randomDialogue == 4)
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.54", Player.name); //You should probably heal after that one.
                        }
                        if (randomDialogue == 5)
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.55", Player.name); //Barely a scratch.. right?
                        }
                    }
                    if (eventPrompt == "onDeath")
                    {
                        randomDialogue = Main.rand.Next(0, 3);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.56", Player.name); //That wasn't supposed to happen..
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 5;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.57", Player.name); //I don't think you can walk that one off.
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.58", Player.name); //....Ouch.
                        }
                    }

                    //Upon first contact with a boss..
                    if (eventPrompt == "onUnknownBoss")
                    {

                        randomDialogue = Main.rand.Next(0, 5);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.59", Player.name); //A powerful foe approaches! We aren't going to lose this.
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 6;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.60", Player.name); //An incredibly strong foe draws near! Let's give them a fight to remember!
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 2;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.61", Player.name); //A strong foe draws near. It's time to fight.
                        }
                        if (randomDialogue == 3)
                        {
                            promptExpression = 2;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.62", Player.name); //Right. No more games. A powerful foe is approaching.
                        }
                        if (randomDialogue == 4)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.63", Player.name);//Looks like danger's on the horizon, {0}. I've been itching for a fight!
                            //promptDialogue = $"{Player.name}, danger approaches!" +
                            //                " I've been itching for a fight!";
                        }

                    }
                    if (eventPrompt == "onEyeOfCthulhu")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.64", Player.name); //The.. eyeball.. approaches. Watch yourself- it's a big one. It gets stronger when it's on its last legs, I think.
                        seenEyeOfCthulhu = true;
                    }
                    if (eventPrompt == "onKingSlime")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.65", Player.name); //It's the lord of the slimes! I think it has a teleportation-like ability..
                        seenKingSlime = true;
                    }
                    if (eventPrompt == "onEaterOfWorlds")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.66", Player.name); //A colossal worm! Watch where it emerges; it'll try and hit your blind spots!
                        seenEaterOfWorlds = true;
                    }
                    if (eventPrompt == "onBrainOfCthulhu")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.67", Player.name); //This thing is trying to attack your mind directly..! Don't be fooled by the mirages!
                        seenBrainOfCthulhu = true;
                    }
                    if (eventPrompt == "onQueenBee")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.68", Player.name); //Watch out.. the Queen Bee is awake! Make sure to dodge the horizontal charges!
                        seenQueenBee = true;
                    }
                    if (eventPrompt == "onSkeletron")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.69", Player.name); //Don't underestimate this monster! Stay away from his skull and arms!
                        seenSkeletron = true;
                    }
                    if (eventPrompt == "onWallOfFlesh")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.70", Player.name); //That thing is massive! If you can, try and build a path to fight it on.
                        seenWallOfFlesh = true;
                    }
                    if (eventPrompt == "onTwins")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.71", Player.name); //There's two giant eyeballs coming your way! Let's see... The red one will shoot at you, and the green one charges.
                        seenTwins = true;
                    }
                    if (eventPrompt == "onDeerclops")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.72", Player.name); //What in the world is that thing? A deer? It's only got one eye... aim for it!
                        seenDeerclops = true;
                    }
                    if (eventPrompt == "onQueenSlime")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.73", Player.name); //It's another colossal slime! It looks like it's going to summon some minions!
                        seenQueenSlime = true;
                    }
                    if (eventPrompt == "onEmpress")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.74", Player.name); //A Hallow-aspected foe has appeared! Maybe leave the butterfly alone next time...?
                        seenEmpress = true;
                    }
                    if (eventPrompt == "onDestroyer")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.75", Player.name); //A giant mechanical worm.. What can we do..? How about trying to attack multiple parts of it at once?
                        seenDestroyer = true;
                    }
                    if (eventPrompt == "onSkeletronPrime")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.76", Player.name); //It's a more advanced version of Skeletron.. Try going for the arms first, instead of the head.
                        seenSkeletronPrime = true;
                    }
                    if (eventPrompt == "onPlantera")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.77", Player.name); //Plantera is awake! Be mindful of its vines. It would be great to have a huge arena to fight it in.
                        seenPlantera = true;
                    }
                    if (eventPrompt == "onGolem")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.78", Player.name); //An ancient mechanical monster.. Watch out for the traps in the temple while fighting it.
                        seenGolem = true;
                    }
                    if (eventPrompt == "onDukeFishron")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.79", Player.name); //You reeled in something crazy! Something tells me you should stay near the sea!
                        seenDukeFishron = true;
                    }
                    if (eventPrompt == "onLunaticCultist")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.80", Player.name); //It's one of those Lunatic Cultists.. Stop them before they unleash a calamity..!
                        seenCultist = true;
                    }
                    if (eventPrompt == "onMoonLord")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.81", Player.name); //I can't believe it.. it's the Moon Lord! This is the final battle! We have to win this!
                        seenMoonLord = true;
                    }
                    if (eventPrompt == "onWarriorOfLight")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.82", Player.name); //The Warrior of Light approaches.. When he breaks his limits, prepare yourself!
                        seenWarriorOfLight = true;
                    }
                    if (eventPrompt == "onVagrant")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.83", Player.name); //Something about this foe seems familiar... Attacks won't work; just survive for as long as you can!
                        seenVagrant = true;
                    }
                    if (eventPrompt == "onNalhaun")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.84", Player.name); //Don't underestimate this foe..! Keep grabbing the stolen lifeforce he's taking from you!
                        seenNalhaun = true;
                    }
                    if (eventPrompt == "onPenth")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.85", Player.name); //This witch attacks with paint! Mind what color you're doused in!
                        seenPenth = true;
                    }
                    if (eventPrompt == "onArbiter")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.86", Player.name); //This thing changes its attack patterns! Take note of its stances, or else!
                        seenArbiter = true;
                    }
                    //Calamity mod bosses!
                    if (eventPrompt == "onDesertScourge")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.87", Player.name); //There's something coming from below, and fast! Prepare yourself.. this is one strong worm!
                        seenDesertScourge = true;
                    }
                    if (eventPrompt == "onCrabulon")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.88", Player.name); //It's a massive moving mass of mycelium.. Let's take out the trash!
                        seenCrabulon = true;
                    }
                    if (eventPrompt == "onHiveMind")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.89", Player.name); //A corrupted beast draws near! Kill its minions quickly, lest it overwhelm you!
                        seenHiveMind = true;
                    }
                    if (eventPrompt == "onPerforators")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.90", Player.name); //What in the world is that thing? Aim for that disgusting Hive before it's too late!
                        seenPerforators = true;
                    }
                    if (eventPrompt == "onSlimeGod")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.91", Player.name); //So this is the source of all the world's slimes. Don't get hasty; I'm certain those slimes will split when hurt!
                        seenSlimeGod = true;
                    }
                    //Hardmode Calamity Bosses
                    if (eventPrompt == "onCryogen")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.92", Player.name); //Something is strange about this thing, but I don't know what. Don't get frozen... but you could probably already tell.
                        seenCryogen = true;
                    }
                    if (eventPrompt == "onAquaticScourge")
                    {

                        if (seenDesertScourge)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.93", Player.name); //Hey, it's the Desert Scourge- but not so dried up! We've seen one before, how bad could this one be?
                        }
                        else
                        {
                            promptExpression = 2;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.94", Player.name); //It's a giant sea serpent! Hang on... This thing is SERIOUSLY dangerous!
                        }

                        seenAquaticScourge = true;
                    }
                    if (eventPrompt == "onBrimstoneElemental")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.95", Player.name); //The flames have brought forth a demonic spirit! Take care to watch your footing, lest you succumb to lava!
                        seenBrimstoneElemental = true;
                    }
                    if (eventPrompt == "onCalamitas")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.96", Player.name); //This thing... it's a herald of destruction.. You know what devestation it can bring. Don't lose..!
                        seenCalamitas = true;
                    }
                    if (eventPrompt == "onSiren")
                    {
                        if (leviathanDialogue == 2)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.97", Player.name); //...It's Anahita again. Are you going to challenge the Leviathan?
                        }
                        else
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.98", Player.name); //...Do you hear that? Whatever it is.. it sounds really dangerous.
                        }

                        seenSiren = true;
                    }
                    if (eventPrompt == "onAnahita")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.99", Player.name); //That demon of the sea is fighting back! I hope you're ready for this...
                        seenAnahita = true;
                    }
                    if (eventPrompt == "onLeviathan")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.100", Player.name); //Whoa. This thing is enormous!! Who knew she had this up her sleeve..?
                        seenLeviathan = true;
                    }
                    if (eventPrompt == "onAstrumAureus")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.101", Player.name); //The Astral Infection has corrupted whatever this was. Don't underestimate it. The Infection can do anything...
                        seenAstrumAureus = true;
                    }
                    if (eventPrompt == "onPlaguebringer")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.102", Player.name); //Oh, great.. a giant robotic bug. It kind of looks like the Queen Bee, so try and remember her attacks!
                        seenPlaguebringer = true;
                    }
                    if (eventPrompt == "onRavager")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.103", Player.name); //This is.. an amalgamation of flesh and machinery.. Above everything, try and stay away from it!
                        seenRavager = true;
                    }
                    if (eventPrompt == "onAstrumDeus")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.104", Player.name); //This is.. a descendant of a cosmic god! {Player.name}, don't get reckless!
                        seenAstrumDeus = true;
                    }
                    //Post Moon Lord Calamity Bosses
                    if (eventPrompt == "onProfanedGuardian")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.105", Player.name); //Something's reacting to the Profaned Shard! Prepare for a fight!
                        seenProfanedGuardian = true;
                    }
                    if (eventPrompt == "onDragonfolly")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.106", Player.name); //You've summoned a draconic monster..! It looks to be incredibly quick!
                        seenDragonfolly = true;
                    }
                    if (eventPrompt == "onProvidence")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.107", Player.name); //The fiery god of both light and dark descends.. No way we're losing to this thing!
                        seenProvidence = true;
                    }
                    if (eventPrompt == "onStormWeaver")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.108", Player.name); //A spatial worm is approaching! Don't get careless.
                        seenStormWeaver = true;
                    }
                    if (eventPrompt == "onCeaselessVoid")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.109", Player.name); //That rune has summoned some sort of.. void entity..?
                        seenCeaselessVoid = true;
                    }
                    if (eventPrompt == "onSignus")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.110", Player.name); //Someone.. or something.. is approaching. It can shapeshift at will! Get ready!
                        seenSignus = true;
                    }
                    if (eventPrompt == "onPolterghast")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.111", Player.name); //Something's awake. The Dungeon's volatile spirits have coalesed...
                        seenPolterghast = true;
                    }
                    if (eventPrompt == "onOldDuke")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.112", Player.name); //The acid ocean has spat out a monster! Wait until it gets tired to strike!
                        seenOldDuke = true;
                    }
                    if (eventPrompt == "onDog")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.113", Player.name); //The Devourer of Gods has arrived! Fight! Fight with all your strength!
                        seenDog = true;
                    }
                    if (eventPrompt == "onYharon")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.114", Player.name); //Yharim's loyal beast, in the flesh..! Let's send this tyrant down a peg!
                        seenYharon = true;
                    }
                    if (eventPrompt == "onYharonDespawn")
                    {
                        promptExpression = 4;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.115", Player.name); //Huh? Where's it going? We were in the middle of something!
                        seenYharonDespawn = true;
                    }
                    if (eventPrompt == "onSupremeCalamitas")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.116", Player.name); //We bow to no one! Let's bring this witch down!
                        seenSupremeCalamitas = true;
                    }
                    if (eventPrompt == "onDraedon")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.117", Player.name); //This is Yharim's loyal scientist. His knowledge is nigh-infinite.. Stay cautious.
                        seenDraedon = true;
                    }
                    if (eventPrompt == "onArtemis")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.118", Player.name); //Twin mechanical eyes- incredibly powerful. Don't get overwhelmed..!
                        seenArtemis = true;
                    }
                    if (eventPrompt == "onThanatos")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.119", Player.name); //The very earth trembles before this foe. Draedon's toys have become stronger...
                        seenThanatos = true;
                    }
                    if (eventPrompt == "onAres")
                    {
                       
                        promptExpression = 2;
                        //promptDialogue = $"Heads up, {Player.name}!" +
                        //                $" That machine is blotting out the sky!";
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.120", Player.name);//Heads up, {0}! Looks like that machine's blotting out the whole sky!
                        seenAres = true;
                    }
                    //Fargos Souls Bosses
                    if (eventPrompt == "onTrojanSquirrel")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.121", Player.name);//Whoa, look at this thing. What a go-getter! Oh, right- strategy. Looks like you can aim for its arms, so try that.
                        seenTrojanSquirrel = true;
                    }
                    if (eventPrompt == "onDeviantt")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.122", Player.name);
                        //promptDialogue = $"Aww, this pint-sized witch wants a fight? Alright then!" +
                        //                $" It looks like you'll be barraged with debuffs- Get the heals ready.";
                        seenDeviantt = true;
                    }
                    if (eventPrompt == "onEridanus")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.123", Player.name);
                        //promptDialogue = $"Hah? Just look at this muppet... thinking he's better than us or something!" +
                        //                $" Give him a good whallop!";
                        seenEridanus = true;
                    }
                    if (eventPrompt == "onAbominationn")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.124", Player.name);
                        //promptDialogue = $"Let's get this show started!" +
                        //                $" He'll be attacking with the power of worldly invaders, if you remember those!";
                        seenAbominationn = true;
                    }
                    if (eventPrompt == "onMutant")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.125", Player.name);
                        //promptDialogue = $"If I had a nickel for times you've thrown voodoo dolls into lava... Never mind." +
                        //                $" Mutant's super upset, super strong, and super coming straight for you.";
                        seenMutant = true;
                    }
                    //Thorium bosses.
                    if (eventPrompt == "onGrandThunderBird")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.126", Player.name); //A lightning bird is approaching! It's not too powerful, but don't get careless.
                        seenGrandThunderBird = true;
                    }
                    if (eventPrompt == "onQueenJellyfish")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.127", Player.name); //Hey. Doesn't that jellyfish look a little too big? Whatever its issue is, it's coming this way!
                        seenQueenJellyfish = true;
                    }
                    if (eventPrompt == "onViscount")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.128", Player.name); //Ugh, what is that thing? A vampire? Quick, find some silver! Or is that werewolves..?
                        seenViscount = true;
                    }
                    if (eventPrompt == "onGraniteEnergyStorm")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.129", Player.name); //Looks like the granite has woken up..? Time to bury it where it belongs.
                        seenGraniteEnergyStorm = true;
                    }
                    if (eventPrompt == "onBuriedChampion")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.130", Player.name); //This gladiator looks stronger than the others. Let's give it a fight to remember!
                        seenBuriedChampion = true;
                    }
                    if (eventPrompt == "onStarScouter")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.131", Player.name); //It's an alien spaceship! It looks like it'll be stronger on low HP!
                        seenStarScouter = true;
                    }
                    //Thorium Hardmode
                    if (eventPrompt == "onBoreanStrider")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.132", Player.name); //Watch yourself.. looks like a Borean Strider is nearby.
                        seenBoreanStrider = true;
                    }
                    if (eventPrompt == "onCoznix")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.133", Player.name); //Looks like a Beholder's reacted with the Void Lens. Let's stab it in its big ugly eye..!
                        seenCoznix = true;
                    }
                    if (eventPrompt == "onLich")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.134", Player.name); //The Grim Harvest Sigil has summoned a powerful foe. I don't know what else you expected!
                        seenLich = true;
                    }
                    if (eventPrompt == "onAbyssion")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.135", Player.name); //The Abyssal Shadows are converging! I sense powerful dark magic from this sea creature..
                        seenAbyssion = true;
                    }
                    if (eventPrompt == "onPrimordials")
                    {
                        promptExpression = 6;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.136", Player.name); //The elements themselves are on the hunt. Let's show them a thing or two!
                        seenPrimordials = true;
                    }

                    //Secrets of the Shadows
                    if (eventPrompt == "onPutridPinky")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.181", Player.name); //Watch yourself.. looks like a Borean Strider is nearby.
                        seenPutridPinky = true;
                    }
                    if (eventPrompt == "onPharaoh")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.182", Player.name); //Looks like a Beholder's reacted with the Void Lens. Let's stab it in its big ugly eye..!
                        seenPharaoh = true;
                    }
                    if (eventPrompt == "onAdvisor")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.183", Player.name); //The Grim Harvest Sigil has summoned a powerful foe. I don't know what else you expected!
                        seenAdvisor = true;
                    }
                    if (eventPrompt == "onPolaris")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.184", Player.name); //The Abyssal Shadows are converging! I sense powerful dark magic from this sea creature..
                        seenPolaris = true;
                    }
                    if (eventPrompt == "onLux")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.185", Player.name); //The elements themselves are on the hunt. Let's show them a thing or two!
                        seenLux = true;
                    }
                    if (eventPrompt == "onSubspaceSerpent")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.186", Player.name); //The elements themselves are on the hunt. Let's show them a thing or two!
                        seenSubspaceSerpent = true;
                    }

                    //Upon entering a biome for the first time..
                    if (eventPrompt == "onEnterDesert")
                    {
                        promptExpression = 5;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.137", Player.name); //It's sweltering here. Deserts will be the same wherever you are, I guess.
                        seenDesertBiome = true;
                    }
                    if (eventPrompt == "onEnterJungle")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.138", Player.name); //So this is the Jungle. Take care when exploring. Ah, but make sure to explore everything!
                        seenJungleBiome = true;
                    }
                    if (eventPrompt == "onEnterBeach")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.139", Player.name); //Well, it's the ocean. ...Did you want me to say something else? It's an ocean.
                        seenBeachBiome = true;
                    }
                    if (eventPrompt == "onEnterSpace")
                    {
                        promptExpression = 6;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.140", Player.name); //Isn't it nice up here?  
                        seenSpaceBiome = true;
                    }
                    if (eventPrompt == "onEnterUnderworld")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.141", Player.name); //The Underworld, huh? Interesting. This goes without saying, but it's incredibly dangerous here.
                        seenUnderworldBiome = true;
                    }
                    if (eventPrompt == "onEnterCorruption")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.142", Player.name); //The world is being corrupted away here. Let's not tarry.
                        seenCorruptionBiome = true;
                    }
                    if (eventPrompt == "onEnterCrimson")
                    {
                        promptExpression = 5;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.143", Player.name); //The ground here feels like flesh. I feel like we shouldn't stay long- but that's obvious..
                        seenCrimsonBiome = true;
                    }
                    if (eventPrompt == "onEnterSnow")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.144", Player.name); //It's snow! I've been always fond of snow. The water-based kind, of course.
                        seenSnowBiome = true;
                    }
                    if (eventPrompt == "onEnterHallow")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.145", Player.name); //It's the Hallow, the stuff of legend! Awesome! Everything here wants to kill us, though. Bummer.
                        seenHallowBiome = true;
                    }
                    if (eventPrompt == "onEnterMushroom")
                    {
                        promptExpression = 6;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.146", Player.name); //Whoa! This place is funky. You don't see these mushrooms every day.
                        seenGlowingMushroomBiome = true;
                    }
                    if (eventPrompt == "onEnterDungeon")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.147", Player.name); //There's a skele-ton of enemies here! Don't you enjoy a little danger? I sure do.
                        seenDungeonBiome = true;
                    }
                    if (eventPrompt == "onEnterMeteorite")
                    {
                        promptExpression = 6;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.148", Player.name); //So this is the meteor impact we heard. I bet we can make some crazy stuff with a meteorite.
                        seenMeteoriteBiome = true;
                    }

                    //Verdant
                    if (eventPrompt == "onEnterVerdant")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.180", Player.name);
                        seenVerdantBiome = true;
                    }

                    //Calamity Biomes
                    if (eventPrompt == "onEnterCrag")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.149", Player.name); //This is.. the Brimstone Crag. Something dreadful happened here.
                        seenCragBiome = true;
                    }
                    if (eventPrompt == "onEnterAstral")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.150", Player.name); //So this is the Astral Infection. I've read about it, but actually seeing it...
                        seenAstralBiome = true;
                    }
                    if (eventPrompt == "onEnterSunkenSea")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.151", Player.name); //Wow.. This place is quite unique. I can't quite explain.
                        seenSunkenSeaBiome = true;
                    }
                    if (eventPrompt == "onEnterSulphurSea")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.152", Player.name); //Whatever was done to this place is irreversible. This ocean has been stained red with blood.
                        seenSulphurSeaBiome = true;
                    }
                    if (eventPrompt == "onEnterAbyss")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.153", Player.name); //You're entering the true depths of the ocean. I hope you realize how dangerous this is!
                        seenAbyssBiome = true;
                    }

                    //Thorium Biomes
                    if (eventPrompt == "onEnterGranite")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.154", Player.name); //This cavern seems to be made of hard granite.. Curious.
                        seenGraniteBiome = true;
                    }
                    if (eventPrompt == "onEnterMarble")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.155", Player.name); //This cave exhudes royalty. Perhaps it has to do with the abundance of marble.
                        seenMarbleBiome = true;
                    }
                    if (eventPrompt == "onEnterAquaticDepths")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.156", Player.name); //This is a deep part of the ocean. Take care you don't drown...
                        seenAquaticDepthsBiome = true;
                    }


                    //Upon certain weather conditions..
                    if (eventPrompt == "onRain")
                    {
                        promptExpression = 6;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.157", Player.name); //Looks like it started raining. Hopefully this doesn't put a damper on things.. heh.
                        seenRain = true;
                    }
                    if (eventPrompt == "onSnow")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.158", Player.name); //It's a blizzard! Don't get frostbite, now. That'd be embarassing.
                        seenSnow = true;
                    }
                    if (eventPrompt == "onSandstorm")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.159", Player.name); //A sandstorm has kicked up. It gets everywhere, so be careful.
                        seenSandstorm = true;
                    }

                    //Upon activation of certain Stellar Array passives...
                    if (eventPrompt == "onKeyOfChronology")
                    {
                        randomDialogue = Main.rand.Next(0, 3);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 6;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.160", Player.name); //Time and space bend to my will!
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.161", Player.name); //Not on my watch!
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.162", Player.name); //Turn back the clock..!
                        }
                    }
                    if (eventPrompt == "onLivingDead")
                    {
                        randomDialogue = Main.rand.Next(0, 3);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 2;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.163", Player.name); //By your undying rage..!
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.164", Player.name); //It's just a flesh wound.
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.165", Player.name); //Embrace the abyss! Set yourself free!
                        }
                    }
                    if (eventPrompt == "onButchersDozen")
                    {
                        randomDialogue = Main.rand.Next(0, 3);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.166", Player.name); //No one will escape!
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.167", Player.name); //Like lambs to slaughter!
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.168", Player.name); //Butcher them!
                        }
                    }

                    if (eventPrompt == "onStellarNovaCharged")
                    {
                        randomDialogue = Main.rand.Next(0, 6);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 2;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.169", Player.name); //Let's show them our power.
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.170", Player.name); //I'm ready to unleash my power!
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.171", Player.name); //I'm ready to use the Stellar Nova!
                        }
                        if (randomDialogue == 3)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.172", Player.name); //OK. Done charging- let's go.
                        }
                        if (randomDialogue == 4)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.173", Player.name); //Just tell me when.
                        }
                        if (randomDialogue == 5)
                        {
                            promptExpression = 6;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.174", Player.name); //Here we go.
                        }
                    }

                    //Visiting Subworlds for the first time.
                    if (eventPrompt == "onObservatory")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.175", Player.name); //You've made it to the Observatory! So, what do you think?
                        seenObservatory = true;
                    }
                    if (eventPrompt == "onSpaceRuins")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.176", Player.name); //Looks like you've arrived.  These are asteroids of interest- let's do some exploring.
                        seenCygnusAsteroids = true;
                    }
                    if (eventPrompt == "onCitadel")
                    {
                        promptExpression = 4;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.177", Player.name); //We've made it. This planet is strange.. The surface has been wiped clean... What happened?
                        seenBleachedPlanet = true;
                    }
                    if (eventPrompt == "onConfluence")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.178", Player.name); //She was so close all along... Use the Mnemonic Sigil on the arena's center to begin.
                        seenConfluence = true;
                    }
                    if (eventPrompt == "onCity")
                    {
                        promptExpression = 4;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.179", Player.name); //It looks like we can't explore this yet... Maybe next time.
                        seenCity = true;
                    }
                }
                if (chosenStarfarer == 2) //Eridani's lines                                              | Soft limit
                {
                    
                    //When the player is debuffed..c
                    if (eventPrompt == "onPoison")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.1", Player.name); //Uhh.. you're looking a little sick.

                    }
                    if (eventPrompt == "onIchor")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.2", Player.name); //The Ichor is dampening your defenses. Watch out.
                    }
                    if (eventPrompt == "onSilence")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.3", Player.name); //You've been silenced.. No magic for now.
                    }
                    if (eventPrompt == "onCurse")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.4", Player.name); //That looks to me like a curse.. You can't use anything right now.
                    }
                    if (eventPrompt == "onFrozen")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.5", Player.name); //That doesn't look good. You're frozen solid. 
                    }
                    if (eventPrompt == "onFrostburn")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.6", Player.name); //You've been inflicted with Frostburn! That's.. confusing. 
                    }
                    if (eventPrompt == "onWebbed")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.7", Player.name); //Yuck- you've been rendered immobile by webs. 
                    }
                    if (eventPrompt == "onStoned")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.8", Player.name); //You've been petrified.. 
                    }
                    if (eventPrompt == "onBurning")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.9", Player.name); //You're taking damage from a hot surface! 
                    }
                    if (eventPrompt == "onSuffocation")
                    {

                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.10", Player.name); //You're dying fast. Find a way out of there. 
                    }
                    if (eventPrompt == "onFire")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.11", Player.name); //Ouch. That's probably not good.

                    }
                    if (eventPrompt == "onDrowning")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.12", Player.name); //You need some air, quick.
                    }
                    if (eventPrompt == "onBleeding")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.13", Player.name); //That's a lot of blood.. You aren't regenerating health now, that's for sure.
                    }
                    if (eventPrompt == "onConfusion")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.14", Player.name); //[You can't make out the words.] 
                    }

                    //During combat..
                    if (eventPrompt == "onKillCritter")
                    {
                        starfarerPromptActiveTimer = starfarerPromptActiveTimerSetting;
                        randomDialogue = Main.rand.Next(0, 3);
                        promptExpression = 5;
                        if (randomDialogue == 0)
                        {
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.15", Player.name); //Whoops.
                        }
                        if (randomDialogue == 1)
                        {
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.16", Player.name); //Did you mean to do that?
                        }
                        if (randomDialogue == 2)
                        {
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.17", Player.name); //Sorry, little one.
                        }
                    }
                    if (eventPrompt == "onKillEnemy")
                    {
                        randomDialogue = Main.rand.Next(0, 20);
                        starfarerPromptActiveTimer = starfarerPromptActiveTimerSetting;
                        if (randomDialogue == 0)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.18", Player.name); //Good work.
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.19", Player.name); //That's another one down.
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.20", Player.name); //Well fought.
                        }
                        if (randomDialogue == 3)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.21", Player.name); //Another one down.
                        }
                        if (randomDialogue == 4)
                        {
                            promptExpression = 6;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.22", Player.name); //You're pretty good at this.
                        }
                        if (randomDialogue == 5)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.23", Player.name); //Nice work.
                        }
                        if (randomDialogue == 6)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.24", Player.name); //They'll never beat us.
                        }
                        if (randomDialogue == 7)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.25", Player.name); //You've defeated it.
                        }
                        if (randomDialogue == 8)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.26", Player.name); //That'll show them.
                        }
                        if (randomDialogue == 9)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.27", Player.name); //A good fight.
                        }
                        if (randomDialogue == 10)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.28", Player.name); //Easy.
                        }
                        if (randomDialogue == 11)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.29", Player.name); //No sweat.
                        }
                        if (randomDialogue == 12)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.30", Player.name); //Wasn't even a problem.
                        }
                        if (randomDialogue == 13)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.31", Player.name); //We got that taken care of.
                        }
                        if (randomDialogue == 14)
                        {
                            promptExpression = 6;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.32", Player.name); //Good going.
                        }
                        if (randomDialogue == 15)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.33", Player.name); //Right, that's that.
                        }
                        if (randomDialogue == 16)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.34", Player.name); //How could we ever lose?
                        }
                        if (randomDialogue == 17)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.35", Player.name); //It's over.
                        }
                        if (randomDialogue == 18)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.36", Player.name); //No more of that.
                        }
                        if (randomDialogue == 19)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.37", Player.name); //A good encounter.
                        }

                    }
                    if (eventPrompt == "onKillEnemyDanger")
                    {
                        starfarerPromptActiveTimer = starfarerPromptActiveTimerSetting;
                        randomDialogue = Main.rand.Next(0, 3);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.38", Player.name); //That was a little close for comfort..
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.39", Player.name); //That's over with, but we're still in trouble.
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.40", Player.name); //That was close.
                        }
                    }
                    if (eventPrompt == "onKillBossEnemy")
                    {
                        randomDialogue = Main.rand.Next(0, 3);
                        starfarerPromptActiveTimer = 150;
                        if (randomDialogue == 0)
                        {
                            promptExpression = 6;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.41", Player.name); //Finally. It's defeated.
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.42", Player.name); //You bested it, finally. Good job.
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.43", Player.name); //What a powerful foe...
                        }
                    }
                    if (eventPrompt == "onCrit")
                    {
                        randomDialogue = Main.rand.Next(0, 6);
                        starfarerPromptActiveTimer = starfarerPromptActiveTimerSetting;
                        if (randomDialogue == 0)
                        {
                            promptExpression = 6;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.44", Player.name); //Perfect attack!
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 6;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.45", Player.name); //A critical hit!
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.46", Player.name); //Expertly done.
                        }
                        if (randomDialogue == 3)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.47", Player.name); //Nice, you hit their weak spot.
                        }
                        if (randomDialogue == 4)
                        {
                            promptExpression = 6;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.48", Player.name); //That was great!
                        }
                        if (randomDialogue == 5)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.49", Player.name); //A decisive blow.
                        }
                    }
                    if (eventPrompt == "onTakeHeavyDamage")
                    {
                        randomDialogue = Main.rand.Next(0, 6);
                        starfarerPromptActiveTimer = 150;
                        if (randomDialogue == 0)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.50", Player.name); //Ouch. That looked like it hurt.
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 5;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.51", Player.name); //Are you alright?
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.52", Player.name); //That doesn't look good.
                        }
                        if (randomDialogue == 3)
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.53", Player.name); //This could be going better..
                        }
                        if (randomDialogue == 4)
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.54", Player.name); //Oww..
                        }
                        if (randomDialogue == 5)
                        {
                            promptExpression = 5;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.55", Player.name); //Yikes.
                        }
                    }
                    if (eventPrompt == "onDeath")
                    {
                        randomDialogue = Main.rand.Next(0, 3);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.56", Player.name); //This is.. bad..
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 5;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.57", Player.name); //That is.. not good.
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 2;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.58", Player.name); //They'll pay for that..
                        }
                    }

                    //Upon first contact with a boss..
                    if (eventPrompt == "onUnknownBoss")
                    {
                        randomDialogue = Main.rand.Next(0, 5);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.59", Player.name); //A powerful foe approaches. Let's send it back to where it belongs.
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 2;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.60", Player.name); //I can sense powerful energy approaching. Ready or not, it's time for a fight.
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 2;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.61", Player.name); //A strong opponent draws near. Prepare yourself.
                        }
                        if (randomDialogue == 3)
                        {
                            promptExpression = 2;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.62", Player.name); //Stay alert, {0}. Something powerful is on its way.
                        }
                        if (randomDialogue == 4)
                        {
                            promptExpression = 2;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.63", Player.name); //Danger approaches. We will show no mercy.
                        }
                    }
                    if (eventPrompt == "onEyeOfCthulhu")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.64", Player.name); //Here it comes. Whatever it is, it's dangerous. Take extra care when it's weak; it'll be forced into a frenzy.
                        seenEyeOfCthulhu = true;
                    }
                    if (eventPrompt == "onKingSlime")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.65", Player.name); //That's a.. giant slime. It seems to be able to move really quickly. Watch for that.
                        seenKingSlime = true;
                    }
                    if (eventPrompt == "onEaterOfWorlds")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.66", Player.name); //This must be the Eater of Worlds. It'll try and suprise you from below.
                        seenEaterOfWorlds = true;
                    }
                    if (eventPrompt == "onBrainOfCthulhu")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.67", Player.name); //Watch out. It's trying to attack your mind itself. Pay close attention to the mirages.
                        seenBrainOfCthulhu = true;
                    }
                    if (eventPrompt == "onQueenBee")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.68", Player.name); //That giant bee is attacking! Don't get caught in the honey!
                        seenQueenBee = true;
                    }
                    if (eventPrompt == "onSkeletron")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.69", Player.name); //The Clothier has turned into.. this.. Stay away from its skull and arms.
                        seenSkeletron = true;
                    }
                    if (eventPrompt == "onWallOfFlesh")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.70", Player.name); //This thing is incredibly strong.. Whatever you do, keep running..!
                        seenWallOfFlesh = true;
                    }
                    if (eventPrompt == "onTwins")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.71", Player.name); //The Eye of Cthulhu is back? Wait, there's two of them..! Try and focus one at a time!
                        seenTwins = true;
                    }
                    if (eventPrompt == "onDeerclops")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.72", Player.name); //It's a cyclops... wait.. It has antlers... Is it a deer? Is it both..?
                        seenDeerclops = true;
                    }
                    if (eventPrompt == "onQueenSlime")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.73", Player.name); //Looks to be another type of gigantic slime. I have a feeling it'll use minions to do its bidding.
                        seenQueenSlime = true;
                    }
                    if (eventPrompt == "onEmpress")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.74", Player.name); //Something dangerous stirs in the Hallow... It looks to draw overwhelming power during the daytime!
                        seenEmpress = true;
                    }
                    if (eventPrompt == "onDestroyer")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.75", Player.name); //The Destroyer approaches.. Let's try using area-of-effect attacks against it.
                        seenDestroyer = true;
                    }
                    if (eventPrompt == "onSkeletronPrime")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.76", Player.name); //Skeletron is back, and better than ever! Try prioritizing the appendages first.
                        seenSkeletronPrime = true;
                    }
                    if (eventPrompt == "onPlantera")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.77", Player.name); //The menace of the Jungle is attacking..! Don't get stuck on your surroundings!
                        seenPlantera = true;
                    }
                    if (eventPrompt == "onGolem")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.78", Player.name); //The Lizhard's beast is awake! Mind the Jungle Temple's traps during this fight.
                        seenGolem = true;
                    }
                    if (eventPrompt == "onDukeFishron")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.79", Player.name); //You've reeled in a dangerous foe! Stay near the sea lest it become enraged!
                        seenDukeFishron = true;
                    }
                    if (eventPrompt == "onLunaticCultist")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.80", Player.name); //It's a Lunatic Cultist.. You have to defeat them before they can summon calamity!
                        seenCultist = true;
                    }
                    if (eventPrompt == "onMoonLord")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.81", Player.name); //The Moon Lord.. We come face to face with a god. We have to win this! There's no other option!
                        seenMoonLord = true;
                    }
                    if (eventPrompt == "onWarriorOfLight")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.82", Player.name); //The Warrior of Light approaches.. His Limit Breaks are incredibly strong!
                        seenWarriorOfLight = true;
                    }
                    if (eventPrompt == "onVagrant")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.83", Player.name); //Something about this foe seems familiar... Your attacks won't work; just try and survive!
                        seenVagrant = true;
                    }
                    if (eventPrompt == "onNalhaun")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.84", Player.name); //Don't underestimate this king of eld! Take back the lifeforce he's stealing!
                        seenNalhaun = true;
                    }
                    if (eventPrompt == "onPenth")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.85", Player.name); //She's covering you with paint. Take careful note of your color!
                        seenPenth = true;
                    }
                    if (eventPrompt == "onArbiter")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.86", Player.name); //It seems to be able to swap forms! Try to memorize its attacks..!
                        seenArbiter = true;
                    }
                    //Calamity Mod Bosses

                    if (eventPrompt == "onDesertScourge")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.87", Player.name); //Something's tunneling in the sand..! Try and get off the ground!
                        seenDesertScourge = true;
                    }
                    if (eventPrompt == "onCrabulon")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.88", Player.name); //A mycelium beast, huh? It's spewing mushrooms everywhere...
                        seenCrabulon = true;
                    }
                    if (eventPrompt == "onHiveMind")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.89", Player.name); //The corruption has spewed out a new beast. Look out for its barrage of minions.
                        seenHiveMind = true;
                    }
                    if (eventPrompt == "onPerforators")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.90", Player.name); //Flesh beasts draw near..! Focus your attention on the Hive!
                        seenPerforators = true;
                    }
                    if (eventPrompt == "onSlimeGod")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.179", Player.name); //This huge amalgamate draws from the world's evils! Killing one slime will empower the other!
                        seenSlimeGod = true;
                    }
                    //Hardmode Calamity Bosses
                    if (eventPrompt == "onCryogen")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.91", Player.name); //This foe radiates mystic ice. Stay away from it- those icy spikes are not for show.
                        seenCryogen = true;
                    }
                    if (eventPrompt == "onAquaticScourge")
                    {

                        if (seenDesertScourge)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.92", Player.name); //The Desert Scourge makes a return... I knew it- This was what it once was.
                        }
                        else
                        {
                            promptExpression = 2;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.93", Player.name); //A colossal serpent makes its way towards us.. It may overwhelm you.. don't get reckless.
                        }

                        seenAquaticScourge = true;
                    }
                    if (eventPrompt == "onBrimstoneElemental")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.94", Player.name); //That Charred Idol has summoned a flaming spirit.. It can teleport everywhere- don't lose track of it.
                        seenBrimstoneElemental = true;
                    }
                    if (eventPrompt == "onCalamitas")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.95", Player.name); //Calamitas.. With a name like that, it begs respect. We've seen what it can do. You musn't lose this fight.
                        seenCalamitas = true;
                    }
                    if (eventPrompt == "onSiren")
                    {
                        if (leviathanDialogue == 2)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.96", Player.name); //It's Anahita singing again. Do you want to fight her once more?
                        }
                        else
                        {

                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.97", Player.name); //Can you hear that, {0}? Something's singing in the ocean?

                        }

                        seenSiren = true;
                    }
                    if (eventPrompt == "onAnahita")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.98", Player.name); //Ah.. It looks like you've angered her. I hope you're prepared for a fight..
                        seenAnahita = true;
                    }
                    if (eventPrompt == "onLeviathan")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.99", Player.name); //What in the world.. It's massive! Take caution; the playing field has changed!
                        seenLeviathan = true;
                    }
                    if (eventPrompt == "onAstrumAureus")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.100", Player.name); //It's a mechanical beast, but it's been corrupted by the Astral Infection. Stay cautious.
                        seenAstrumAureus = true;
                    }
                    if (eventPrompt == "onPlaguebringer")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.101", Player.name); //Ugh. It's a disgusting bug, but now it has artillery. When it gets wounded, those missiles will likely detonate.
                        seenPlaguebringer = true;
                    }
                    if (eventPrompt == "onRavager")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.102", Player.name); //Yuck. It's a huge pile of flesh and bones. From what I can tell, getting too close will end you fast.
                        seenRavager = true;
                    }
                    if (eventPrompt == "onAstrumDeus")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.103", Player.name); //This is a descendant of a cosmic god! We can't lose to something like this!
                        seenAstrumDeus = true;
                    }
                    //Post Moon Lord Calamity Bosses
                    if (eventPrompt == "onProfanedGuardian")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.104", Player.name); //The Profaned Shard has summoned something! Prepare for a fight!
                        seenProfanedGuardian = true;
                    }
                    if (eventPrompt == "onDragonfolly")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.105", Player.name); //Hey, those pheromones.. Looks like it attracted a feral beast.
                        seenDragonfolly = true;
                    }
                    if (eventPrompt == "onProvidence")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.106", Player.name); //A deity of light and darkness approaches!. We musn't lose this..!
                        seenProvidence = true;
                    }
                    if (eventPrompt == "onStormWeaver")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.107", Player.name); //Something just entered the upper atmosphere! Brace for impact!
                        seenStormWeaver = true;
                    }
                    if (eventPrompt == "onCeaselessVoid")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.108", Player.name); //Wait. I sense something from the Dungeon's depths. Prepare yourself for a fight!
                        seenCeaselessVoid = true;
                    }
                    if (eventPrompt == "onSignus")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.109", Player.name); //A demon is approaching..! Watch out! Whatever it wants, it'll kill you for it!
                        seenSignus = true;
                    }
                    if (eventPrompt == "onPolterghast")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.110", Player.name); //The ghosts of the dungeon.. They're fusing together! Prepare yourself!
                        seenPolterghast = true;
                    }
                    if (eventPrompt == "onOldDuke")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.111", Player.name); //Something descends from the acid rain! It's looking for a fight!
                        seenOldDuke = true;
                    }
                    if (eventPrompt == "onDog")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.112", Player.name); //The Devourer of Gods has appeared! You must give this battle your all!
                        seenDog = true;
                    }
                    if (eventPrompt == "onYharon")
                    {
                        promptExpression = 3;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.113", Player.name); //The Jungle Dragon roars. Let's throw a wrench in Yharim's plans.
                        seenYharon = true;
                    }
                    if (eventPrompt == "onYharonDespawn")
                    {
                        promptExpression = 4;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.114", Player.name); //Where's it going? Were we.. not strong enough?
                        seenYharonDespawn = true;
                    }
                    if (eventPrompt == "onSupremeCalamitas")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.115", Player.name); //No way.. Its power is ineffable. {Player.name}.. We have to win this!
                        seenSupremeCalamitas = true;
                    }
                    //Draedon Update
                    if (eventPrompt == "onDraedon")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.116", Player.name); //Draedon is here... Be mindful of his tactics. His knowledge spans the world over.
                        seenDraedon = true;
                    }
                    if (eventPrompt == "onArtemis")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.117", Player.name); //Stay sharp. Multiple foes detected. Draedon's inventions are on the move.
                        seenArtemis = true;
                    }
                    if (eventPrompt == "onThanatos")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.118", Player.name); //Quick- find higher ground! A colossal mechanical serpent draws near!
                        seenThanatos = true;
                    }
                    if (eventPrompt == "onAres")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.178", Player.name); //A being of the apocalypse has appeared. It wields destruction in all its appendages!

                        seenAres = true;
                    }
                    //Fargos Souls Bosses
                    if (eventPrompt == "onTrojanSquirrel")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.119", Player.name); //...Never mind the absurdity of the giant mechanical squirrel. Focus on attacking its head and arms to stop its most dangerous attacks.

                        seenTrojanSquirrel = true;
                    }
                    if (eventPrompt == "onDeviantt")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.120", Player.name); //Huh? She looks weak, but she's actually kind of powerful... If you get hit, you'll be slammed with debuffs; keep your healing ready.

                        seenDeviantt = true;
                    }
                    if (eventPrompt == "onEridanus")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.121", Player.name); //Who does this guy think he is, wielding celestial power all willy-nilly? Not to mention... Ugh- I don't have to spell it out, do I?

                        
                        seenEridanus = true;
                    }
                    if (eventPrompt == "onAbominationn")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.122", Player.name); //Well, no time like the present. His arsenal revolves around invader events- we've seen our share of those.

                        
                        seenAbominationn = true;
                    }
                    if (eventPrompt == "onMutant")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.123", Player.name); //Oh, you just HAD to throw that thing into lava, did you? Well, good luck, because Mutant is pissed. Didn't see that coming- wait, I did.

                        
                        seenMutant = true;
                    }
                    //Thorium bosses.
                    if (eventPrompt == "onGrandThunderBird")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.124", Player.name); //The Storm Flare has called something down! Hmm? It looks to be lightning-aspected.
                        seenGrandThunderBird = true;
                    }
                    if (eventPrompt == "onQueenJellyfish")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.125", Player.name); //Some sort of jellyfish monster is approaching. It looks like there's someone trapped inside..?
                        seenQueenJellyfish = true;
                    }
                    if (eventPrompt == "onViscount")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.126", Player.name); //It's a giant vampire bat! Mind yourself...
                        seenViscount = true;
                    }
                    if (eventPrompt == "onGraniteEnergyStorm")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.127", Player.name); //The cavern's energy has congealed into a monster! What in the world..?
                        seenGraniteEnergyStorm = true;
                    }
                    if (eventPrompt == "onBuriedChampion")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.128", Player.name); //A knight has arisen from the marble cave! Look out- it's prepared to fight!
                        seenBuriedChampion = true;
                    }
                    if (eventPrompt == "onStarScouter")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.129", Player.name); //It's an alien spaceship! It looks like it'll be stronger on low HP!
                        seenStarScouter = true;
                    }
                    //Thorium Hardmode
                    if (eventPrompt == "onBoreanStrider")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.130", Player.name); //Do you see that..? It looks like a giant snow spider! Let's kill it quickly..
                        seenBoreanStrider = true;
                    }
                    if (eventPrompt == "onCoznix")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.131", Player.name); //A Beholder..? Don't get reckless.. These foes are incredibly fearsome!
                        seenCoznix = true;
                    }
                    if (eventPrompt == "onLich")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.132", Player.name); //That sigil you used.. I can sense an undead beast approaching! Get ready for a fight!
                        seenLich = true;
                    }
                    if (eventPrompt == "onAbyssion")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.133", Player.name); //The Abyssal Shadows are converging..! This sea monster holds dominion over dark magic!
                        seenAbyssion = true;
                    }
                    if (eventPrompt == "onPrimordials")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.134", Player.name); //Primordial energy is forming all around you.. The elements themselves are on the attack!
                        seenPrimordials = true;
                    }
                    //Secrets of the Shadows
                    if (eventPrompt == "onPutridPinky")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.181", Player.name); //Watch yourself.. looks like a Borean Strider is nearby.
                        seenPutridPinky = true;
                    }
                    if (eventPrompt == "onPharaoh")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.182", Player.name); //Looks like a Beholder's reacted with the Void Lens. Let's stab it in its big ugly eye..!
                        seenPharaoh = true;
                    }
                    if (eventPrompt == "onAdvisor")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.183", Player.name); //The Grim Harvest Sigil has summoned a powerful foe. I don't know what else you expected!
                        seenAdvisor = true;
                    }
                    if (eventPrompt == "onPolaris")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.184", Player.name); //The Abyssal Shadows are converging! I sense powerful dark magic from this sea creature..
                        seenPolaris = true;
                    }
                    if (eventPrompt == "onLux")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.185", Player.name); //The elements themselves are on the hunt. Let's show them a thing or two!
                        seenLux = true;
                    }
                    if (eventPrompt == "onSubspaceSerpent")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.186", Player.name); //The elements themselves are on the hunt. Let's show them a thing or two!
                        seenSubspaceSerpent = true;
                    }

                    //Upon entering a biome for the first time..
                    if (eventPrompt == "onEnterDesert")
                    {
                        promptExpression = 4;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.135", Player.name); //It's a desert. Seriously.. I'm no fan of heat- and it's hot.
                        seenDesertBiome = true;
                    }
                    if (eventPrompt == "onEnterJungle")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.136", Player.name); //This verdant environment must be the Jungle. This place is dangerous, but also rewarding.
                        seenJungleBiome = true;
                    }
                    if (eventPrompt == "onEnterSpace")
                    {
                        promptExpression = 6;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.137", Player.name); //You're nearing the limit of breathable air. I do not recommend burning up in the atmosphere. Don't ask.
                        seenSpaceBiome = true;
                    }
                    if (eventPrompt == "onEnterUnderworld")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.138", Player.name); //So this is the place where the dead rest. Let's see what we can find.
                        seenUnderworldBiome = true;
                    }
                    if (eventPrompt == "onEnterBeach")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.139", Player.name); //It's the ocean... We can't cross it, so best leave it be.
                        seenBeachBiome = true;
                    }
                    if (eventPrompt == "onEnterCorruption")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.140", Player.name); //I can feel the evil aura radiating from this place. I wouldn't mind leaving as soon as possible.
                        seenCorruptionBiome = true;
                    }
                    if (eventPrompt == "onEnterCrimson")
                    {
                        promptExpression = 5;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.141", Player.name); //This place is.. disgusting. Let's not stay longer than we have to.
                        seenCrimsonBiome = true;
                    }
                    if (eventPrompt == "onEnterSnow")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.142", Player.name); //It's a typical boreal forest. Not that I dislike snowy places.
                        seenSnowBiome = true;
                    }
                    if (eventPrompt == "onEnterHallow")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.143", Player.name); //We've stepped straight into fairytale land. It would be prettier if everything here wasn't hostile.
                        seenHallowBiome = true;
                    }
                    if (eventPrompt == "onEnterMushroom")
                    {
                        promptExpression = 6;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.144", Player.name); //Giant glowing mushrooms.. It feels rather.. welcoming? We should take some back home, just in case.
                        seenGlowingMushroomBiome = true;
                    }
                    if (eventPrompt == "onEnterDungeon")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.145", Player.name); //I can feel traps everywhere. Strong weapons are buried here as well. Stay ready.
                        seenDungeonBiome = true;
                    }
                    if (eventPrompt == "onEnterMeteorite")
                    {
                        promptExpression = 4;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.146", Player.name); //So this is the meteor impact we heard. Wonder what you can make out of it..?
                        seenMeteoriteBiome = true;
                    }

                    //Verdant
                    if (eventPrompt == "onEnterVerdant")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.180", Player.name);
                        seenVerdantBiome = true;
                    }


                    //Calamity Biomes
                    if (eventPrompt == "onEnterCrag")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.147", Player.name); //The Brimstone Crag.. I can feel the horrors of the past..
                        seenCragBiome = true;
                    }
                    if (eventPrompt == "onEnterAstral")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.148", Player.name); //This is the Astral Infection. A subject I'm all too familiar with...
                        seenAstralBiome = true;
                    }
                    if (eventPrompt == "onEnterSunkenSea")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.149", Player.name); //This place is quite beautiful. Who knew it would be under the desert..?
                        seenSunkenSeaBiome = true;
                    }
                    if (eventPrompt == "onEnterSulphurSea")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.150", Player.name); //This was once a pleasant coastline. What happened here..?
                        seenSulphurSeaBiome = true;
                    }
                    if (eventPrompt == "onEnterAbyss")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.151", Player.name); //You've decided to breach the ocean's depths. Please consider how dangerous this really is..
                        seenAbyssBiome = true;
                    }

                    //Thorium Biomes
                    if (eventPrompt == "onEnterGranite")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.152", Player.name); //This cavern seems to be all granite. Some rouge spirits have made their home here.
                        seenGraniteBiome = true;
                    }
                    if (eventPrompt == "onEnterMarble")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.153", Player.name); //So this is a Marble Cavern. Perhaps we can use the material to build?
                        seenMarbleBiome = true;
                    }
                    if (eventPrompt == "onEnterAquaticDepths")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.154", Player.name); //We're very deep in the ocean. These waters are trouble, I can tell that much.
                        seenAquaticDepthsBiome = true;
                    }

                    //Upon certain weather conditions..
                    if (eventPrompt == "onRain")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.155", Player.name); //It's raining. Hopefully nothing important gets too wet.
                        seenRain = true;
                    }
                    if (eventPrompt == "onSnow")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.156", Player.name); //Looks like it's a blizzard. 'Cold' doesn't begin to cover it.
                        seenSnow = true;
                    }
                    if (eventPrompt == "onSandstorm")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.157", Player.name); //A sandstorm has kicked up. Mind your eyes.
                        seenSandstorm = true;

                    }

                    //For certain Stellar Array abilities...

                    if (eventPrompt == "onKeyOfChronology")
                    {
                        randomDialogue = Main.rand.Next(0, 3);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 6;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.158", Player.name); //Time and space bend to my will!
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.159", Player.name); //Let's try that again, shall we?
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.160", Player.name); //Chronal reversal complete..!
                        }
                    }
                    if (eventPrompt == "onLivingDead")
                    {
                        randomDialogue = Main.rand.Next(0, 3);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 2;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.161", Player.name); //March forth, and keep fighting!
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.162", Player.name); //We're not about to stop here!
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.163", Player.name); //Embrace the abyss! Set yourself free!
                        }
                    }
                    if (eventPrompt == "onButchersDozen")
                    {
                        randomDialogue = Main.rand.Next(0, 3);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.164", Player.name); //They'll all fall before us.
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.165", Player.name); //Like lambs to slaughter!
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.166", Player.name); //We'll finish them all off!
                        }
                    }
                    //
                    if (eventPrompt == "onStellarNovaCharged")
                    {
                        randomDialogue = Main.rand.Next(0, 6);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 2;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.167", Player.name); //I'm ready when you are.
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.168", Player.name); //The Stellar Nova is ready!
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.169", Player.name); //I'm ready. Are you?
                        }
                        if (randomDialogue == 3)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.170", Player.name); //Let's do this.
                        }
                        if (randomDialogue == 4)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.171", Player.name); //Just tell me when.
                        }
                        if (randomDialogue == 5)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.172", Player.name); //Let's find an opening.
                        }
                    }

                    //Visiting Subworlds for the first time.
                    if (eventPrompt == "onObservatory")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.173", Player.name); //Celestial coordinates established. Welcome to the Observatory Hyperborea.
                        seenObservatory = true;
                    }
                    if (eventPrompt == "onSpaceRuins")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.174", Player.name); //We've made it. These are asteroids of interest. Let's look around.
                        seenCygnusAsteroids = true;
                    }
                    if (eventPrompt == "onCitadel")
                    {
                        promptExpression = 4;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.175", Player.name); //This is it. An entire planet that's devoid of color? The surface looks like it was wiped away somehow...
                        seenBleachedPlanet = true;
                    }
                    if (eventPrompt == "onConfluence")
                    {
                        promptExpression = 2;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.176", Player.name); //She was here the whole time...? Well.. I'm ready when you are. Use the Sigil in the middle.
                        seenConfluence = true;
                    }
                    if (eventPrompt == "onCity")
                    {
                        promptExpression = 4;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.177", Player.name); //It looks like we can't explore this yet... Maybe next time.
                        seenCity = true;
                    }
                }
                promptDialogue = Wrap(promptDialogue, 78);
            }
        }
        public void StellarNovaEnergy()
        {
            if (inCombat > 0)
            {
                if (chosenStellarNova != 0)
                    if (novaGaugeChargeTimer >= 60)
                    {

                        if (novaGauge == trueNovaGaugeMax - 1)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_superReadySFX, Player.Center);

                            Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                            CombatText.NewText(textPos, new Color(255, 0, 125, 240), "Stellar Nova ready!", false, false);
                            if (Main.rand.Next(0, 5) == 0)
                            {
                                starfarerPromptActive("onStellarNovaCharged");
                            }
                        }

                        //Natural charge rate.
                        novaGauge++;

                        if (unbridledradiance == 2)
                        {

                            novaGauge++;
                        }
                        if (avataroflight == 2 && Player.statLife >= 500)
                        {

                            novaGaugeChargeTimer += 5;
                        }
                        if (astralmantle == 2 && Player.statMana > 200)
                        {

                            novaGaugeChargeTimer += 15;
                        }
                        if (Player.HeldItem.prefix == ModContent.PrefixType<NovaPrefix1>())
                        {
                            novaGaugeChargeTimer += 2;
                        }
                        if (Player.HeldItem.prefix == ModContent.PrefixType<NovaPrefix2>())
                        {
                            novaGaugeChargeTimer += 4;
                        }
                        if (Player.HeldItem.prefix == ModContent.PrefixType<NovaPrefix3>())
                        {
                            novaGaugeChargeTimer += 6;
                        }
                        if (Player.HeldItem.prefix == ModContent.PrefixType<NovaPrefix4>())
                        {
                            novaGaugeChargeTimer += 8;
                        }
                        if (Player.HeldItem.prefix == ModContent.PrefixType<BadNovaPrefix1>())
                        {
                            novaGaugeChargeTimer -= 2;
                        }
                        if (Player.HeldItem.prefix == ModContent.PrefixType<BadNovaPrefix2>())
                        {
                            novaGaugeChargeTimer -= 4;
                        }
                        novaGaugeChargeTimer = 0;
                    }


                if (novaGaugeUnlocked && novaGauge < trueNovaGaugeMax)
                {
                    novaGaugeChargeTimer++;

                }

            }
            else
            {
                novaGaugeChargeTimer = 0;
                if (unbridledradiance == 2)
                {

                }
                else
                {
                    novaGaugeLossTimer++;
                    if (novaGaugeLossTimer >= 35)
                    {
                        novaGaugeLossTimer = 0;
                        novaGauge--;
                    }

                }
            }
            if (novaGauge > trueNovaGaugeMax)
            {
                novaGauge = trueNovaGaugeMax;
            }
            if (novaGauge < 0)
            {

                novaGauge = 0;
            }
        }
        private void SetupStarfarerOutfit()
        {


            if (starfarerOutfitVanity != 0)//Is there a vanity outfit equipped? This will change drawing.
            {
                starfarerOutfitVisible = starfarerOutfitVanity;
            }
            else if (starfarerOutfit != 0)
            {
                starfarerOutfitVisible = starfarerOutfit;
            }
            else //If there isn't anything in either slot, default to these values.
            {
                starfarerOutfitVisible = 0;


            }
            if (starfarerOutfitVanity == -1)//Familiar Looking Attire equipped?
            {
                starfarerOutfitVisible = 0;
            }
            if (starfarerOutfitVisible != starfarerOutfitSaved)
            {
                costumeChangeOpacity = 1f;
                //Add dialogue here for changing outfits.
                starfarerOutfitSaved = starfarerOutfitVisible;
            }
            //Change actual stats here.
            if (starfarerOutfit == 0) //No outfits equipped. This is the default state.
            {

            }
            if (starfarerOutfit == 1) //Faerie Voyager outfit.
            {
                if (starfarerOutfit == 1)//Faerie Voyager
                {

                    stellarGaugeMax++;
                }
            }
            if (starfarerOutfit == 2) //Stellar Casual outfit.
            {

            }
            if (starfarerOutfit == 3) // Celestial Princess' Genesis
            {
                chosenStellarNova = 5;

                if (!Player.HasBuff(BuffType<AstarteDriver>()))
                {
                    Player.GetDamage(DamageClass.Generic) -= 0.7f;
                }
                if (Player.HasBuff(BuffType<AstarteDriver>()))
                {
                    Player.GetDamage(DamageClass.Generic) += 0.15f;
                }
            }
        }
        public void onActivateStellarNova()
        {
            //if (player.ownedProjectileCounts[mod.ProjectileType("SpaceBurstFX")] < 1)
            Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("SpaceBurstFX").Type, 0, 0, Player.whoAmI, 0, 1);
            Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("SpaceBurstFX2").Type, 0, 0, Player.whoAmI, 0, 1);
            activateShockwaveEffect = true;


            if (chosenStarfarer == 1)
            {


                Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("AsphodeneBurstFX").Type, 0, 0, Player.whoAmI, 0, 1);
                Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("AsphodeneBurstFX2").Type, 0, 0, Player.whoAmI, 0, 1);
                // Projectile.NewProjectile(null,new Vector2(player.Center.X, player.Center.Y - 500), Vector2.Zero, mod.ProjectileType("BurstFX3"), 0, 0, player.whoAmI, 0, 1);
                // Projectile.NewProjectile(null,new Vector2(player.Center.X, player.Center.Y - 500), Vector2.Zero, mod.ProjectileType("BurstFX4"), 0, 0, player.whoAmI, 0, 1);
                // Projectile.NewProjectile(null,new Vector2(player.Center.X, player.Center.Y - 500), Vector2.Zero, mod.ProjectileType("BurstFX5"), 0, 0, player.whoAmI, 0, 1);
                //Projectile.NewProjectile(null,new Vector2(player.Center.X, player.Center.Y - 500), Vector2.Zero, mod.ProjectileType("BurstFX6"), 0, 0, player.whoAmI, 0, 1);
                Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("AsphodeneBurst" + starfarerOutfitVisible).Type, 0, 0, Player.whoAmI, 0, 1);



            }
            if (chosenStarfarer == 2)
            {

                Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("EridaniBurstFX").Type, 0, 0, Player.whoAmI, 0, 1);
                Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("EridaniBurstFX2").Type, 0, 0, Player.whoAmI, 0, 1);
                Projectile.NewProjectile(null, new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("EridaniBurst" + starfarerOutfitVisible).Type, 0, 0, Player.whoAmI, 0, 1);



            }
            if (ruinedKingPrism)
            {
                Player.AddBuff(BuffType<Buffs.SovereignDominion>(), 900);
            }
            if (cosmicPhoenixPrism)
            {
                if (Player.statLife < 100)
                {
                    Player.statMana = Player.statManaMax2;
                }
                else
                {
                    Player.statMana += 100;
                }
            }
            if (lightswornPrism)
            {
                Player.AddBuff(BuffType<Buffs.Lightblessed>(), 480);
            }
            if (burnishedPrism)
            {
                Player.AddBuff(BuffID.Rage, 720);
                Player.AddBuff(BuffID.Wrath, 720);
                Player.AddBuff(BuffID.Titan, 720);
                Player.AddBuff(BuffID.Endurance, 720);
            }
            if (spatialPrism)
            {
                Player.AddBuff(BuffID.Regeneration, 720);
                Player.AddBuff(BuffID.ManaRegeneration, 720);
                Player.AddBuff(BuffID.Heartreach, 720);
            }
            if (voidsentPrism)
            {
                Vector2 placement2 = new Vector2((Player.Center.X), Player.Center.Y);
                Projectile.NewProjectile(null, placement2.X, placement2.Y, 0, 0, Mod.Find<ModProjectile>("radiate").Type, 0, 0f, 0);
                Projectile.NewProjectile(null, placement2.X, placement2.Y, 0, 0, Mod.Find<ModProjectile>("VoidsentBurst").Type, baseNovaDamageAdd / 10, 0f, 0);
            }
            if (overgrownPrism)
            {
                Player.statLife += 100;
                Player.AddBuff(BuffID.Wrath, 1200);
                Player.AddBuff(BuffType<Vulnerable>(), 1200);
            }
            if (lihzahrdPrism)
            {
                Player.AddBuff(BuffType<AncientBulwark>(), 480);
            }
            if (mechanicalPrism)
            {
                Player.AddBuff(BuffType<MechanicalPrismBuff>(), 600);
            }


        }
        private void onEnemyHitWithNova(NPC target, int nova, ref int damage, ref bool crit)
        {
            if (starfarerOutfit == 4 && hopesBrilliance > 0)
            {
                for (int i = 0; i < hopesBrilliance / 10; i++)
                {
                    damage = (int)(damage * (1.02));
                }

                hopesBrilliance = 0;
            }
            if (empressPrism)
            {
                crit = true;
                damage = (int)(damage * 0.7);
            }

            if (paintedPrism)
            {
                target.AddBuff(BuffID.Ichor, 720);
                target.AddBuff(BuffID.Frostburn, 720);
                target.AddBuff(BuffID.Oiled, 720);

            }
            if (mechanicalPrism && Player.HasBuff(BuffType<MechanicalPrismBuff>()))
            {
                Player.ClearBuff(BuffType<MechanicalPrismBuff>());
                target.AddBuff(BuffType<Stun>(), 120);
                Vector2 placement2 = new Vector2((target.Center.X), target.Center.Y);

                Projectile.NewProjectile(null, placement2.X, placement2.Y, 0, 0, Mod.Find<ModProjectile>("VoidsentBurst").Type, damage / 10, 0f, 0);
            }
            if (royalSlimePrism)
            {
                if (crit)
                {
                    damage = (int)(damage * 1.4);
                }
                else
                {
                    damage = (int)(damage * 0.8);
                }
            }
            if (typhoonPrism)
            {
                if (crit && !Player.HasBuff(BuffType<TyphoonPrismCooldown>()))
                {
                    damage += Math.Min((int)(target.lifeMax * 0.05), 40000);
                    Player.AddBuff(BuffType<TyphoonPrismCooldown>(), 240);
                }

            }

            if (luminitePrism)
            {
                if (trueNovaGaugeMax >= 200)
                {
                    damage += (int)(damage * 1.5);
                }
            }
        }
        public override void ResetEffects()
        {
            

            KevesiFarewellInInventory = false;
            AgnianFarewellInInventory = false;

            Observatory = false;
            SeaOfStars = false;
            BurningDesireHeld = false;
            GoldenKatanaHeld = false;
            IrminsulHeld = false;
            AshenAmbitionHeld = false;
            VermillionDaemonHeld = false;
            SakuraVengeanceHeld = false;
            ChemtankHeld = false;
            HunterSymphonyHeld = false;
            SkyStrikerHeld = false;
            IsVoidActive = false;
            euthymiaActive = false;
            SoulReaverHeld = false;

            Player.GetModPlayer<StarsAbovePlayer>().seraphimHeld--;
            Player.GetModPlayer<StarsAbovePlayer>().kroniicHeld--;
            Player.GetModPlayer<StarsAbovePlayer>().albionHeld--;
            if (Player.statMana <= 0)
            {
                phantomTeleport = false;
            }
            Player.GetModPlayer<StarsAbovePlayer>().starblessedCooldown--;
            RobotSpiderMinion = false;
            SatanaelMinion = false;
            AlucardSwordMinion1 = false;
            AlucardSwordMinion2 = false;
            AlucardSwordMinion3 = false;
            TakodachiMinion = false;
            FleetingSparkMinion = false;
            YoumuMinion = false;

            Kifrosse1 = false;
            Kifrosse2 = false;
            Kifrosse3 = false;
            Kifrosse4 = false;
            Kifrosse5 = false;
            Kifrosse6 = false;
            Kifrosse7 = false;
            Kifrosse8 = false;
            Kifrosse9 = false;
            GuilmonPet = false;
            BladeWolfPet = false;
            MantisCatPet = false;
            NecoArcPet = false;
            BiyomonPet = false;
            PyraPet = false;
            SuiseiPet = false;
            SanaPet = false;
            MumeiPet = false;
            GrahaPet = false;
            LukaPet = false;
            CrimsonDragonetPet = false;
            HanakoPet = false;
            Ghost = false;
            FerryPet = false;
            PekoraPet = false;
            KasumiPet = false;
            WarriorPet = false;
            EreshkigalPet = false;
            OmoriPet = false;
            HuTaoPet = false;
            MajimaPet = false;
            VitchPet = false;
            PhymPet = false;
            EspeonPet = false;
            UmbreonPet = false;
            BubbaPet = false;
            BloopPet = false;
            DuckHuntBirdPet = false;
            DuckHuntDogPet = false;
            darkHourglass = false;
            willpowerOfZagreus = false;
            catalyzedWeapon = false;
            celestialFoci = false;
            sakuraHeld = false;
            celesteBlessing = false;
            enigmaticCatalyst = false;
            corn = false;
            WarriorBarActive = false;
            WarriorOfLightActive = false;
            NalhaunBarActive = false;
            PenthBarActive = false;
            PenthActive = false;
            BluePaint = false;
            YellowPaint = false;
            RedPaint = false;
            if (!NalhaunActive)
            {
                lifeforce = 100;
            }
            NalhaunActive = false;
            VagrantBarActive = false;
            VagrantActive = false;
            ArbiterActive = false;
            ArbiterBarActive = false;
            TsukiyomiActive = false;
            TsukiyomiBarActive = false;

            //Accessories
            luciferium = false;
            Glitterglue = false;
            PerfectlyGenericAccessory = false;
            DragonwardTalisman = false;
            GaleflameFeather = false;
            ToMurder = false;
            AlienCoral = false;

        }
        public override void clientClone(ModPlayer clientClone)
        {
            StarsAbovePlayer clone = clientClone as StarsAbovePlayer;
            // Here we would make a backup clone of values that are only correct on the local players Player instance.
            // Some examples would be RPG stats from a GUI, Hotkey states, and Extra Item Slots
            clone.stellarPerformanceActive = stellarPerformanceActive;
            clone.stellarPerformanceCooldown = stellarPerformanceCooldown;

            clone.SkyStrikerForm = SkyStrikerForm;

        }
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            /*public bool WarriorBarActive;
		public int WarriorCastTime = 0;
		public int WarriorCastTimeMax = 100;
		public bool WarriorOfLightActive;
		public string WarriorOfLightNextAttack;
		public bool LostToWarriorOfLight = false;*/

            ModPacket packet = Mod.GetPacket();
            packet.Write(stellarPerformanceStart);
            packet.Write(stellarPerformanceActive);
            packet.Write(stellarPerformanceCooldown);
            packet.Write(WarriorBarActive);
            packet.Write(WarriorCastTime);
            packet.Write(WarriorCastTimeMax);
            packet.Write(WarriorOfLightActive);
            packet.Write(WarriorOfLightNextAttack);
            packet.Write(LostToWarriorOfLight);
            packet.Write(SkyStrikerForm);
            base.SyncPlayer(toWho, fromWho, newPlayer);
        }
        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            // Here we would sync something like an RPG stat whenever the player changes it.
            StarsAbovePlayer clone = clientPlayer as StarsAbovePlayer;
            if (clone.stellarPerformanceActive != stellarPerformanceActive)
            {
                // Send a Mod Packet with the changes.
                var packet = Mod.GetPacket();
                packet.Write((byte)Player.whoAmI);
                packet.Write(stellarPerformanceActive);
                packet.Write(stellarPerformanceCooldown);
                packet.Send();
            }
            if (clone.SkyStrikerForm != SkyStrikerForm)
            {
                // Send a Mod Packet with the changes.
                var packet = Mod.GetPacket();
                packet.Write((byte)Player.whoAmI);
                packet.Write(SkyStrikerForm);
                packet.Send();
            }
        }
        public override void FrameEffects()
        {
            if (Player.active && !Player.dead)
            {
                if (SkyStrikerHeld)
                {
                    if (Player.HasBuff(BuffType<Buffs.SkyStrikerBuffs.StrikerAttackBuff>()))
                    {
                        
                        Player.legs = EquipLoader.GetEquipSlot(Mod, "AfterburnerBottom", EquipType.Legs);
                        Player.body = EquipLoader.GetEquipSlot(Mod, "AfterburnerTop", EquipType.Body);
                        Player.wings = EquipLoader.GetEquipSlot(Mod, "AfterburnerWings", EquipType.Wings);
                    }
                    if (Player.HasBuff(BuffType<Buffs.SkyStrikerBuffs.StrikerShieldBuff>()))
                    {
                        Player.legs = EquipLoader.GetEquipSlot(Mod, "ShieldBottom", EquipType.Legs);
                        Player.body = EquipLoader.GetEquipSlot(Mod, "ShieldTop", EquipType.Body);
                        Player.wings = EquipLoader.GetEquipSlot(Mod, "ShieldWings", EquipType.Wings);
                    }
                    if (Player.HasBuff(BuffType<Buffs.SkyStrikerBuffs.StrikerMeleeBuff>()))
                    {
                        Player.legs = EquipLoader.GetEquipSlot(Mod, "MeleeBottom", EquipType.Legs);
                        Player.body = EquipLoader.GetEquipSlot(Mod, "MeleeTop", EquipType.Body);
                        Player.wings = EquipLoader.GetEquipSlot(Mod, "MeleeWings", EquipType.Wings);
                    }
                    if (Player.HasBuff(BuffType<Buffs.SkyStrikerBuffs.StrikerSniperBuff>()))
                    {
                        Player.legs = EquipLoader.GetEquipSlot(Mod, "SniperBottom", EquipType.Legs);
                        Player.body = EquipLoader.GetEquipSlot(Mod, "SniperTop", EquipType.Body);
                        Player.wings = EquipLoader.GetEquipSlot(Mod, "SniperWings", EquipType.Wings);
                    }

                }
                if (ChemtankHeld)
                {
                    Player.legs = EquipLoader.GetEquipSlot(Mod, "UrgotLegs", EquipType.Legs);

                }


            }





            base.FrameEffects();

        }


        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            base.ModifyDrawInfo(ref drawInfo);
        }
        public override void HideDrawLayers(PlayerDrawSet drawInfo)
        {
            if (Player.HasBuff(BuffType<Buffs.Ignited>()))//
            {
                foreach (var layer in PlayerDrawLayerLoader.Layers)
                {

                    layer.Hide();

                }
            }
            if (Player.HasBuff(BuffType<Buffs.FlashOfEternity>()))//
            {
                foreach (var layer in PlayerDrawLayerLoader.Layers)
                {

                    layer.Hide();

                }
            }
            if (Player.HasBuff(BuffType<Buffs.Voidform>()))//
            {
                foreach (var layer in PlayerDrawLayerLoader.Layers)
                {

                    layer.Hide();

                }
            }
            if (Player.HasBuff(BuffType<Buffs.IrysBuff>()))//
            {
                foreach (var layer in PlayerDrawLayerLoader.Layers)
                {

                    layer.Hide();

                }
            }
            if (Player.HasBuff(BuffType<Buffs.Invisibility>()))//
            {
                foreach (var layer in PlayerDrawLayerLoader.Layers)
                {

                    layer.Hide();

                }
            }

            base.HideDrawLayers(drawInfo);
        }

        private static string Wrap(string v, int size)
        {
            v = v.TrimStart();
            if (v.Length <= size) return v;
            var nextspace = v.LastIndexOf(' ', size);
            if (-1 == nextspace) nextspace = Math.Min(v.Length, size);
            return v.Substring(0, nextspace) + ((nextspace >= v.Length) ?
            "" : "\n" + Wrap(v.Substring(nextspace), size));
        }
    }

};