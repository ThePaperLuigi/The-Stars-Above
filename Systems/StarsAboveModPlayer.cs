using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using StarsAbove.Biomes;
using StarsAbove.Buffs;
using StarsAbove.Buffs.Boss;
using StarsAbove.Buffs.Celestial.UltimaThule;
using StarsAbove.Buffs.Magic.StygianNymph;
using StarsAbove.Buffs.Magic.VenerationOfButterflies;
using StarsAbove.Buffs.Melee.PenthesileaMuse;
using StarsAbove.Buffs.Melee.Umbra;
using StarsAbove.Buffs.Memories;
using StarsAbove.Buffs.StarfarerAttire;
using StarsAbove.Buffs.StellarArray;
using StarsAbove.Buffs.StellarNovas;
using StarsAbove.Buffs.StellarPrisms;
using StarsAbove.Buffs.StellarSpoils.EmberFlask;
using StarsAbove.Buffs.SubworldModifiers;
using StarsAbove.Buffs.Subworlds;
using StarsAbove.Buffs.Summon.Kifrosse;
using StarsAbove.Dialogue;
using StarsAbove.Items.Armor.StarfarerArmor;
using StarsAbove.Items.Consumables;
using StarsAbove.Items.Prisms;
using StarsAbove.NPCs;
using StarsAbove.NPCs.Arbitration;
using StarsAbove.NPCs.Nalhaun;
using StarsAbove.NPCs.Penthesilea;
using StarsAbove.NPCs.Starfarers;
using StarsAbove.NPCs.Thespian;
using StarsAbove.NPCs.Tsukiyomi;
using StarsAbove.NPCs.WarriorOfLight;
using StarsAbove.Projectiles.StellarArray;
using StarsAbove.Projectiles.StellarNovas;
using StarsAbove.Projectiles.StellarNovas.GuardiansLight;
using StarsAbove.Subworlds;
using StarsAbove.Subworlds.ThirdRegion;
using StarsAbove.Systems;
using StarsAbove.Systems;
using StarsAbove.Systems.StellarNovas;
using StarsAbove.UI.StarfarerMenu;
using StarsAbove.UI.StellarNova;
using StarsAbove.Utilities;
using SubworldLibrary;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using Terraria.WorldBuilding;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove
{
    public class StarsAbovePlayer : ModPlayer
    {
        //Includes Stellar Array values, Dialogue flags, and Stellar Nova stuff.

        //Also includes (OLD) Boss variables

        public int firstJoinedWorld = 0;//Sets the world so progress doesn't get overwritten by joining other worlds.
        public string firstJoinedWorldName;
        public static bool enableWorldLock = false;
        public bool SyncWorldProgress = false;
        public bool AlwaysSyncWorldProgress = false;


        public static bool BossEnemySpawnModDisabled = false;

        public bool seenIntroCutscene = false;
        public int IntroDialogueTimer;

        public int screenShakeTimerGlobal = -1000;

        public int GlobalRotation;

        public bool activateShockwaveEffect = false;
        public bool activateAuthorityShockwaveEffect = false;
        public bool activateUltimaShockwaveEffect = false;
        public bool activateBlackHoleShockwaveEffect = false;

        public bool activateArbiterShockwaveEffect = false;

        //Warrior of Light code //////////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool onEverlastingLightText = false;

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
        public int dioskouroiDialogue = 0;
        public int nalhaunDialogue = 0;
        public int penthDialogue = 0;
        public int arbiterDialogue = 0;
        public int tsukiyomiDialogue = 0;
        public int thespianDialogue = 0;
        public int starfarerPostBattleDialogue = 0;

        public int vagrantBossItemDialogue = 0;
        public int dioskouroiBossItemDialogue = 0;
        public int nalhaunBossItemDialogue = 0;
        public int penthBossItemDialogue = 0;
        public int arbiterBossItemDialogue = 0;
        public int warriorBossItemDialogue = 0;
        public int starfarerBossItemDialogue = 0;

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

        //Long-form dialogue
        public int astrolabeIntroDialogue;
        public int observatoryIntroDialogue;
        public int yojimboIntroDialogue;
        public int garridineIntroDialogue;

        public int MnemonicDialogue1;
        public int MnemonicDialogue2;
        public int MnemonicDialogue3;
        public int MnemonicDialogue4;

        //Weapon lines
        public int WeaponDialogueTimer = Main.rand.Next(3600, 7200);

        public int EyeBossWeaponDialogue = 0;
        public int CorruptBossWeaponDialogue = 0;
        public int SkeletonWeaponDialogue = 0;
        public int Stellaglyph2WeaponDialogue = 0;
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
        public int UmbraWeaponDialogue = 0;
        public int SaltwaterWeaponDialogue = 0;
        public int ChaosWeaponDialogue = 0;
        public int ClockWeaponDialogue = 0;
        public int NanomachineWeaponDialogue = 0;
        public int LevinstormWeaponDialogue = 0;
        public int GoldlewisWeaponDialogue = 0;
        public int SanguineWeaponDialogue = 0;
        public int KarnaWeaponDialogue = 0;
        public int ManiacalWeaponDialogue = 0;
        public int AuthorityWeaponDialogue = 0;

        //v1.5
        public int KineticWeaponDialogue = 0;
        public int DreamerWeaponDialogue = 0;
        public int SoldierWeaponDialogue = 0;
        public int TrickspinWeaponDialogue = 0;

        //v2.0
        public int DragaliaWeaponDialogue = 0;
        public int GundbitWeaponDialogue = 0;
        public int WavedancerWeaponDialogue = 0;
        public int ClarentWeaponDialogue = 0;
        public int ThespianWeaponDialogue = 0;

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

        //Tier 1 Asphodene
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

        //Tier 1 Eridani
        public int spectralNail = 0; //
        public int armsthrift = 0; //
        public int mysticIncision = 0;//
        public int stayTheCourse = 0;//
        public int catharsis = 0;
        public int fabledFashion = 0;
        public int kineticConversion = 0;
        public int kiTwinburst = 0;
        public int arborealEchoes = 0;
        public int swiftstrikeTheory = 0; //
        public int inevitableEnd = 0;
        public int lavenderRefrain = 0;

        //Tier 2
        public int healthyConfidence = 0;//
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

        //Thorium
        public int BardAspect;
        public int HealerAspect;
        public int ThrowerAspect;

        public int unbridledRadianceStack = 0;//Will increase every 1000 kills.

        int stayTheCourseTimer;
        float stayTheCourseStacks;
        int aprismatismCooldown;
        int butchersDozenKills;
        int ammoRecycleCooldown;
        int flashFreezeCooldown;
        int healthyConfidenceHealAmount;
        int healthyConfidenceHealTimer;
        int beyondInfinityTimer;
        float beyondInfinityDamageMod;
        float lavenderRefrainMaxManaReduction;

        public int armsthriftWeaponIDOld = 0;
        public DamageClass armsthriftWeaponTypeOld;

        public int oldHP;

        int timeAfterGettingHit;

        int umbralEntropyCooldown;

        public int dmgSave;

        public int spatialStratagemTimer;

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

        public int chosenStellarNova = 0;//0: No Nova chosen. 1: Prototokia Aster 2: Ars Laevateinn

        public bool novaReadyInfo = false;
        public bool gaugeChargeAnimation = false;

        public int novaGauge;
        public int novaGaugeMax = 100;//This is affected by the chosen Nova
        public int novaGaugeChargeTimer;
        public int novaGaugeLossTimer;

        public float novaDrain = 0f;

        public float novaGaugeChangeAlpha = 0f;
        public float novaGaugeChangeAlphaSlow = 0f;

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

        public int prototokia; //0 = LOCKED, 1 = UNLOCKED, 2 = SELECTED
        public int laevateinn; //0 = LOCKED, 1 = UNLOCKED, 2 = SELECTED
        public int kiwamiryuken; //0 = LOCKED, 1 = UNLOCKED, 2 = SELECTED 2 does not matter really
        public int gardenofavalon; //0 = LOCKED, 1 = UNLOCKED, 2 = SELECTED 2 does not matter really
        public int edingenesisquasar; //0 = LOCKED, 1 = UNLOCKED, 2 = SELECTED 2 does not matter really
        public int unlimitedbladeworks;
        public int guardianslight;

        public int goldenGunShots;
        public bool squallReady;

        //Cutscenes, new feature
        public int astarteCutsceneProgress = 0;

        public int astarteDriverAttacks; //Amount of attacks left after casting Edin Genesis Quasar
        public int astarteDriverCooldown;

        public int WhiteFade;

        public bool ruinedKingPrism;//Tier 3
        public bool cosmicPhoenixPrism;

        //Tier 2
        public bool lightswornPrism;
        public bool spatialPrism;
        public bool paintedPrism;
        public bool burnishedPrism;
        public bool voidsentPrism;
        public bool geminiPrism;

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


        // These are the variables for the Starfarer Prompt system


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
        public bool seenThespian;
        public bool seenStarfarers;

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

        //Spirit Bosses
        public bool seenScarabeus;
        public bool seenMoonJellyWizard;
        public bool seenVinewrathBane;
        public bool seenAncientAvian;
        public bool seenStarplateVoyager;
        public bool seenInfernon;
        public bool seenDusking;
        public bool seenAtlas;

        //Wrath of the Gods
        public bool seenNoxusEgg;
        public bool seenEntropicGod;
        public bool seenNamelessDeity;

        //Starlight River
        public bool seenCeiros;
        public bool seenGlassweaver;
        public bool seenAuroracle;

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

        public static bool disableBlur;
        public static bool disableScreenShake;

        public static bool noLockedCamera;

        public static bool voicesDisabled;

        ////////////////////////////////////////////////////////////////////////////////// STARFARER MENU

        public bool starfarerMenuActive;

        public float starfarerMenuUIOpacity = 0f;
        public float costumeChangeOpacity;
        int starfarerOutfitSaved = 0;



        //Lore list?
        //


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

            tag["spectralNail"] = spectralNail;
            tag["armsthrift"] = armsthrift;
            tag["mysticIncision"] = mysticIncision;
            tag["stayTheCourse"] = stayTheCourse;
            tag["catharsis"] = catharsis;
            tag["fabledFashion"] = fabledFashion;
            tag["kineticConversion"] = kineticConversion;
            tag["kiTwinburst"] = kiTwinburst;
            tag["arborealEchoes"] = arborealEchoes;
            tag["swiftstrikeTheory"] = swiftstrikeTheory;
            tag["inevitableEnd"] = inevitableEnd;
            tag["lavenderRefrain"] = lavenderRefrain;

            tag[" healthyConfidence"] = healthyConfidence;
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

            tag["dioskouroiDialogue"] = dioskouroiDialogue;


            tag["nalhaunDialogue"] = nalhaunDialogue;
            tag["penthDialogue"] = penthDialogue;
            tag["arbiterDialogue"] = arbiterDialogue;
            tag["tsukiyomiDialogue"] = tsukiyomiDialogue;
            tag["thespianDialogue"] = thespianDialogue;
            tag["starfarerPostBattleDialogue"] = starfarerPostBattleDialogue;

            tag["vagrantitem"] = vagrantBossItemDialogue;
            tag["dioskouroiitem"] = dioskouroiBossItemDialogue;
            tag["nalhaunitem"] = nalhaunBossItemDialogue;
            tag["penthitem"] = penthBossItemDialogue;
            tag["arbiteritem"] = arbiterBossItemDialogue;
            tag["warrioritem"] = warriorBossItemDialogue;
            tag["starfareritem"] = starfarerBossItemDialogue;

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

            //Long form dialogue
            tag["astrolabeIntroDialogue"] = astrolabeIntroDialogue;
            tag["observatoryIntroDialogue"] = observatoryIntroDialogue;
            tag["yojimboIntroDialogue"] = yojimboIntroDialogue;
            tag["garridineIntroDialogue"] = garridineIntroDialogue;

            tag["MnemonicDialogue1"] = MnemonicDialogue1;
            tag["MnemonicDialogue2"] = MnemonicDialogue2;
            tag["MnemonicDialogue3"] = MnemonicDialogue3;
            tag["MnemonicDialogue4"] = MnemonicDialogue4;


            tag["EyeBossWeaponDialogue"] = EyeBossWeaponDialogue;
            tag["CorruptBossWeaponDialogue"] = CorruptBossWeaponDialogue;
            tag["SkeletonWeaponDialogue"] = SkeletonWeaponDialogue;
            tag["BeeWeaponDialogue"] = Stellaglyph2WeaponDialogue;
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
            tag["UmbraWeaponDialogue"] = UmbraWeaponDialogue;
            tag["SaltwaterWeaponDialogue"] = SaltwaterWeaponDialogue;
            tag["ChaosWeaponDialogue"] = ChaosWeaponDialogue;
            tag["ClockWeaponDialogue"] = ClockWeaponDialogue;

            tag["NanomachineWeaponDialogue"] = NanomachineWeaponDialogue;
            tag["LevinstormWeaponDialogue"] = LevinstormWeaponDialogue;

            tag["GoldlewisWeaponDialogue"] = GoldlewisWeaponDialogue;

            tag["SanguineWeaponDialogue"] = SanguineWeaponDialogue;
            tag["KarnaWeaponDialogue"] = KarnaWeaponDialogue;
            tag["ManiacalWeaponDialogue"] = ManiacalWeaponDialogue;
            tag["AuthorityWeaponDialogue"] = AuthorityWeaponDialogue;

            tag["KineticWeaponDialogue"] = KineticWeaponDialogue;
            tag["DreamerWeaponDialogue"] = DreamerWeaponDialogue;
            tag["SoldierWeaponDialogue"] = SoldierWeaponDialogue;
            tag["TrickspinWeaponDialogue"] = TrickspinWeaponDialogue;

            tag["DragaliaWeaponDialogue"] = DragaliaWeaponDialogue;
            tag["GundbitWeaponDialogue"] = GundbitWeaponDialogue;
            tag["WavedancerWeaponDialogue"] = WavedancerWeaponDialogue;
            tag["ClarentWeaponDialogue"] = ClarentWeaponDialogue;
            tag["ThespianWeaponDialogue"] = ThespianWeaponDialogue;


            tag["observatoryDialogue"] = observatoryDialogue;
            tag["cosmicVoyageDialogue"] = cosmicVoyageDialogue;

            tag["unbridledRadianceStack"] = unbridledRadianceStack;

            tag["novaGaugeUnlocked"] = novaGaugeUnlocked;
            tag["prototokia"] = prototokia;
            tag["laevateinn"] = laevateinn;
            tag["kiwamiryuken"] = kiwamiryuken;
            tag["gardenofavalon"] = gardenofavalon;
            tag["edingenesisquasar"] = edingenesisquasar;
            tag["unlimitedbladeworks"] = unlimitedbladeworks;
            tag["guardianslight"] = guardianslight;
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
            tag["seenThespian"] = seenThespian;
            tag["seenStarfarers"] = seenStarfarers;

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

            tag["seenScarabeus"] = seenScarabeus;
            tag["seenMoonJellyWizard"] = seenMoonJellyWizard;
            tag["seenVinewrathBane"] = seenVinewrathBane;
            tag["seenAncientAvian"] = seenAncientAvian;
            tag["seenStarplateVoyager"] = seenStarplateVoyager;
            tag["seenInfernon"] = seenInfernon;
            tag["seenDusking"] = seenDusking;
            tag["seenAtlas"] = seenAtlas;

            tag["seenNoxusEgg"] = seenNoxusEgg;
            tag["seenEntropicGod"] = seenEntropicGod;
            tag["seenNamelessDeity"] = seenNamelessDeity;

            tag["seenCeiros"] = seenCeiros;
            tag["seenGlassweaver"] = seenGlassweaver;
            tag["seenAuroracle"] = seenAuroracle;

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

            tag["AlwaysSync"] = AlwaysSyncWorldProgress;

            tag["seenIntroCutscene"] = seenIntroCutscene;
            base.SaveData(tag);
        }

        public override void LoadData(TagCompound tag)
        {
            seenIntroCutscene = tag.GetBool("seenIntroCutscene");

            firstJoinedWorld = tag.GetInt("firstJoinedWorld");
            firstJoinedWorldName = tag.GetString("firstJoinedWorldName");
            AlwaysSyncWorldProgress = tag.GetBool("AlwaysSync");

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
            dioskouroiDialogue = tag.GetInt("dioskouroiDialogue");

            nalhaunDialogue = tag.GetInt("nalhaunDialogue");
            penthDialogue = tag.GetInt("penthDialogue");
            arbiterDialogue = tag.GetInt("arbiterDialogue");
            tsukiyomiDialogue = tag.GetInt("tsukiyomiDialogue");
            thespianDialogue = tag.GetInt("thespianDialogue");
            starfarerPostBattleDialogue = tag.GetInt("starfarerPostBattleDialogue");

            vagrantBossItemDialogue = tag.GetInt("vagrantitem");
            dioskouroiBossItemDialogue = tag.GetInt("dioskouroiitem");
            nalhaunBossItemDialogue = tag.GetInt("nalhaunitem");
            penthBossItemDialogue = tag.GetInt("penthitem");
            arbiterBossItemDialogue = tag.GetInt("arbiteritem");
            warriorBossItemDialogue = tag.GetInt("warrioritem");
            starfarerBossItemDialogue = tag.GetInt("starfareritem");


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

            astrolabeIntroDialogue = tag.GetInt("astrolabeIntroDialogue");
            observatoryIntroDialogue = tag.GetInt("observatoryIntroDialogue");
            yojimboIntroDialogue = tag.GetInt("yojimboIntroDialogue");
            garridineIntroDialogue = tag.GetInt("garridineIntroDialogue");

            MnemonicDialogue1 = tag.GetInt("MnemonicDialogue1");
            MnemonicDialogue2 = tag.GetInt("MnemonicDialogue2");
            MnemonicDialogue3 = tag.GetInt("MnemonicDialogue3");
            MnemonicDialogue4 = tag.GetInt("MnemonicDialogue4");


            EyeBossWeaponDialogue = tag.GetInt("EyeBossWeaponDialogue");
            CorruptBossWeaponDialogue = tag.GetInt("CorruptBossWeaponDialogue");
            SkeletonWeaponDialogue = tag.GetInt("SkeletonWeaponDialogue");
            Stellaglyph2WeaponDialogue = tag.GetInt("BeeWeaponDialogue");
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
            UmbraWeaponDialogue = tag.GetInt("UmbraWeaponDialogue");
            SaltwaterWeaponDialogue = tag.GetInt("SaltwaterWeaponDialogue");
            ChaosWeaponDialogue = tag.GetInt("ChaosWeaponDialogue");
            ClockWeaponDialogue = tag.GetInt("ClockWeaponDialogue");

            LevinstormWeaponDialogue = tag.GetInt("LevinstormWeaponDialogue");
            NanomachineWeaponDialogue = tag.GetInt("NanomachineWeaponDialogue");

            GoldlewisWeaponDialogue = tag.GetInt("GoldlewisWeaponDialogue");
            SanguineWeaponDialogue = tag.GetInt("SanguineWeaponDialogue");
            KarnaWeaponDialogue = tag.GetInt("KarnaWeaponDialogue");
            ManiacalWeaponDialogue = tag.GetInt("ManiacalWeaponDialogue");
            AuthorityWeaponDialogue = tag.GetInt("AuthorityWeaponDialogue");

            KineticWeaponDialogue = tag.GetInt("KineticWeaponDialogue");
            DreamerWeaponDialogue = tag.GetInt("DreamerWeaponDialogue");
            SoldierWeaponDialogue = tag.GetInt("SoldierWeaponDialogue");
            TrickspinWeaponDialogue = tag.GetInt("TrickspinWeaponDialogue");

            DragaliaWeaponDialogue = tag.GetInt("DragaliaWeaponDialogue");
            GundbitWeaponDialogue = tag.GetInt("GundbitWeaponDialogue");
            WavedancerWeaponDialogue = tag.GetInt("WavedancerWeaponDialogue");
            ClarentWeaponDialogue = tag.GetInt("ClarentWeaponDialogue");
            ThespianWeaponDialogue = tag.GetInt("ThespianWeaponDialogue");


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

            spectralNail = tag.GetInt("spectralNail");
            armsthrift = tag.GetInt("armsthrift");
            mysticIncision = tag.GetInt("mysticIncision");
            stayTheCourse = tag.GetInt("stayTheCourse");
            catharsis = tag.GetInt("catharsis");
            fabledFashion = tag.GetInt("fabledFashion");
            kineticConversion = tag.GetInt("kineticConversion");
            kiTwinburst = tag.GetInt("kiTwinburst");
            arborealEchoes = tag.GetInt("arborealEchoes");
            swiftstrikeTheory = tag.GetInt("swiftstrikeTheory");
            inevitableEnd = tag.GetInt("inevitableEnd");
            lavenderRefrain = tag.GetInt("lavenderRefrain");

            healthyConfidence = tag.GetInt(" healthyConfidence");
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
            prototokia = tag.GetInt("prototokia");
            laevateinn = tag.GetInt("laevateinn");
            kiwamiryuken = tag.GetInt("kiwamiryuken");
            gardenofavalon = tag.GetInt("gardenofavalon");
            edingenesisquasar = tag.GetInt("edingenesisquasar");
            unlimitedbladeworks = tag.GetInt("unlimitedbladeworks");
            guardianslight = tag.GetInt("guardianslight");

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
            seenThespian = tag.GetBool("seenThespian");
            seenStarfarers = tag.GetBool("seenStarfarers");

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

            seenScarabeus = tag.GetBool("seenScarabeus");
            seenMoonJellyWizard = tag.GetBool("seenMoonJellyWizard");
            seenVinewrathBane = tag.GetBool("seenVinewrathBane");
            seenAncientAvian = tag.GetBool("seenAncientAvian");
            seenStarplateVoyager = tag.GetBool("seenStarplateVoyager");
            seenInfernon = tag.GetBool("seenInfernon");
            seenDusking = tag.GetBool("seenDusking");
            seenAtlas = tag.GetBool("seenAtlas");

            seenNoxusEgg = tag.GetBool("seenNoxusEgg");
            seenEntropicGod = tag.GetBool("seenEntropicGod");
            seenNamelessDeity = tag.GetBool("seenNamelessDeity");

            seenCeiros = tag.GetBool("seenCeiros");
            seenGlassweaver = tag.GetBool("seenGlassweaver");
            seenAuroracle = tag.GetBool("seenAuroracle");


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

        public override void OnEnterWorld()
        {
            if (SubworldSystem.noReturn == true)
            {
                // SubworldSystem.noReturn = false; //Fix missing save and quit bug? As of 8/9/23 this is still relevant (just found that out the fun way)

            }
            //Load the equipment.
            if (novaGaugeUnlocked)
            {
                if (!affixItem1.IsAir && affixItem1.ModItem != null)
                {
                    StellarNovaUI._affixSlot1.Item = affixItem1;
                    affix1 = affixItem1.Name;
                }
                if (!affixItem2.IsAir && affixItem3.ModItem != null)
                {
                    StellarNovaUI._affixSlot2.Item = affixItem2;
                    affix2 = affixItem2.Name;
                }
                if (!affixItem3.IsAir && affixItem3.ModItem != null)
                {
                    StellarNovaUI._affixSlot3.Item = affixItem3;
                    affix3 = affixItem3.Name;
                }
            }
            else
            {
                //affixItem1 = null;
                //affixItem2 = null;
                //affixItem3 = null;
                //StellarNovaUI._affixSlot1.Item = null;
                //StellarNovaUI._affixSlot2.Item = null;
                //StellarNovaUI._affixSlot3.Item = null;
            }
            StarfarerMenu._starfarerArmorSlot.Item = starfarerArmorEquipped;
            StarfarerMenu._starfarerVanitySlot.Item = starfarerVanityEquipped;

            if (starfarerArmorEquipped != null && starfarerArmorEquipped.IsAir)
            {

            }
            if (starfarerVanityEquipped != null && starfarerVanityEquipped.IsAir)
            {

            }
            //If this is the first time a character has joined a world.
            if (firstJoinedWorld == 0)
            {
                firstJoinedWorld = Main.worldID;
                firstJoinedWorldName = Main.worldName;
            }
            //If the player has already joined a world...
            if (firstJoinedWorld == Main.worldID) //If it's the same world, sync progress.
            {
                SyncWorldProgress = true;
            }
            else //If it's a different world, don't automatically sync progress.
            {
                SyncWorldProgress = false;
            }

            if (!AlwaysSyncWorldProgress)
            {
                //Prompt the player to sync progress or not.
                if (firstJoinedWorld != Main.worldID && !SubworldSystem.AnyActive())
                {
                    sceneID = 2;
                    VNDialogueActive = true;
                }
                return;
            }
            else
            {
                //If AlwaysSyncWorldProgress is true, ignore the check.
                SyncWorldProgress = true;
            }

            if(chosenStarfarer == 2)
            {
                if(starshower == 2 || ironskin == 2 || evasionmastery == 2 || aquaaffinity == 2 || hikari == 2 || livingdead == 2 || umbralentropy == 2 || celestialevanesence == 2 || butchersdozen == 2 || mysticforging == 2 || flashfreeze == 2)
                {
                    if ( starshower == 2)
                    {
                         stellarGauge -= 1;
                         starshower = 1;
                    }
                    if ( ironskin == 2)
                    {
                         stellarGauge -= 1;
                         ironskin = 1;
                    }
                    if ( evasionmastery == 2)
                    {
                         stellarGauge -= 1;
                         evasionmastery = 1;
                    }
                    if ( healthyConfidence == 2)
                    {
                         stellarGauge -= 2;
                         healthyConfidence = 1;
                    }
                    if ( aquaaffinity == 2)
                    {
                         stellarGauge -= 1;
                         aquaaffinity = 1;
                    }
                    if ( bloomingflames == 2)
                    {
                         stellarGauge -= 2;
                         bloomingflames = 1;
                    }
                    if ( astralmantle == 2)
                    {
                         stellarGauge -= 2;
                         astralmantle = 1;
                    }
                    if ( beyondinfinity == 2)
                    {
                         stellarGauge -= 3;
                         beyondinfinity = 1;
                    }
                    if ( keyofchronology == 2)
                    {
                         stellarGauge -= 3;
                         keyofchronology = 1;

                    }
                    if ( avataroflight == 2)
                    {
                         stellarGauge -= 2;
                         avataroflight = 1;
                    }
                    if ( hikari == 2)
                    {
                         stellarGauge -= 1;
                         hikari = 1;
                    }
                    if ( inneralchemy == 2)
                    {
                         stellarGauge -= 1;
                         inneralchemy = 1;
                    }
                    if ( livingdead == 2)
                    {
                         stellarGauge -= 1;
                         livingdead = 1;
                    }
                    if ( umbralentropy == 2)
                    {
                         stellarGauge -= 1;
                         umbralentropy = 1;
                    }
                    if ( celestialevanesence == 2)
                    {
                         stellarGauge -= 1;
                         celestialevanesence = 1;
                    }
                    if ( afterburner == 2)
                    {
                         stellarGauge -= 2;
                         afterburner = 1;
                    }
                    if ( weaknessexploit == 2)
                    {
                         stellarGauge -= 2;
                         weaknessexploit = 1;
                    }
                    if ( aprismatism == 2)
                    {
                         stellarGauge -= 2;
                         aprismatism = 1;
                    }
                    if ( unbridledradiance == 2)
                    {
                         stellarGauge -= 3;
                         unbridledradiance = 1;
                    }
                    if ( beyondtheboundary == 2)
                    {
                         stellarGauge -= 3;
                         beyondtheboundary = 1;
                    }
                    if ( butchersdozen == 2)
                    {
                         stellarGauge -= 1;
                         butchersdozen = 1;
                    }
                    if ( mysticforging == 2)
                    {
                         stellarGauge -= 1;
                         mysticforging = 1;
                    }
                    if ( flashfreeze == 2)
                    {
                         stellarGauge -= 1;
                         flashfreeze = 1;
                    }
                    if ( artofwar == 2)
                    {
                         stellarGauge -= 2;
                         artofwar = 1;
                    }
                    if ( mysticIncision == 2)
                    {
                         stellarGauge -= 1;
                         mysticIncision = 1;
                    }
                    if ( lavenderRefrain == 2)
                    {
                         stellarGauge -= 1;
                         lavenderRefrain = 1;
                    }
                    if ( swiftstrikeTheory == 2)
                    {
                         stellarGauge -= 1;
                         swiftstrikeTheory = 1;
                    }
                    if ( inevitableEnd == 2)
                    {
                         stellarGauge -= 1;
                         inevitableEnd = 1;
                    }
                    if ( arborealEchoes == 2)
                    {
                         stellarGauge -= 1;
                         arborealEchoes = 1;
                    }
                    if ( kiTwinburst == 2)
                    {
                         stellarGauge -= 1;
                         kiTwinburst = 1;
                    }
                    if ( kineticConversion == 2)
                    {
                         stellarGauge -= 1;
                         kineticConversion = 1;
                    }
                    if ( fabledFashion == 2)
                    {
                         stellarGauge -= 1;
                         fabledFashion = 1;
                    }
                    if ( catharsis == 2)
                    {
                         stellarGauge -= 1;
                         catharsis = 1;
                    }
                    if ( stayTheCourse == 2)
                    {
                         stellarGauge -= 1;
                         stayTheCourse = 1;
                    }
                    if ( armsthrift == 2)
                    {
                         stellarGauge -= 1;
                         armsthrift = 1;
                    }
                    if ( spectralNail == 2)
                    {
                         stellarGauge -= 1;
                         spectralNail = 1;
                    }
                }
            }




        }
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {


            return new[] {
                new Item(ItemType<SpatialDisk>()),
                //new Item(ModContent.ItemType<Bifrost>()),
            };
        }


        public override void OnHitAnything(float x, float y, Entity victim)
        {





        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            OnHitStarfarerDialogue(target);
            if (target.lifeMax > 5 && target.CanBeChasedBy())
            {
                inCombat = inCombatMax;
            }
           
            if(kiTwinburst == 2)
            {
                target.SimpleStrikeNPC(damageDone,hit.HitDirection,hit.Crit,hit.Knockback,hit.DamageType);
                
            }
            //Astarte Driver
            if (Player.HasBuff(BuffType<AstarteDriver>()) && !target.HasBuff(BuffType<AstarteDriverEnemyCooldown>()))
            {
                if (hit.Crit)
                {
                    if (chosenStarfarer == 1)
                    {
                        if (!target.active)
                        {
                            astarteDriverAttacks++;
                        }
                    }
                }
                target.AddBuff(BuffType<AstarteDriverEnemyCooldown>(), 60);
                OnEnemyHitWithNova(target, 5, ref damageDone, ref hit.Crit);
            }
            
            if (target.HasBuff(BuffType<Starblight>()) && umbralentropy == 2)
            {
                if (umbralEntropyCooldown <= 0)
                {
                    Player.Heal(Math.Min(damageDone / 100, 5));
                    umbralEntropyCooldown = 60;
                }
            }

            if (Player.HasBuff(BuffType<SovereignDominion>()) && target.HasBuff(BuffType<Ruination>()))
            {
                if (hit.Crit)
                {
                    Player.Heal(damageDone / 10);
                }
            }
            if (ruinedKingPrism && target.life <= target.lifeMax / 2 && hit.Crit)
            {
                target.AddBuff(BuffType<Ruination>(), 1800);
            }
            if (hit.Crit)
            {
                if (celestialevanesence == 2)
                {

                    Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                    CombatText.NewText(textPos, new Color(81, 62, 247, 240), $"{Math.Min((int)(damageDone * 0.05), 5)}", false, false);
                    Player.statMana += Math.Min((int)(damageDone * 0.05), 5);
                }
                if (flashfreeze == 2 && flashFreezeCooldown < 0)
                {
                    //damage += target.lifeMax;
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_electroSmack, Player.Center);
                    Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0, 0, Mod.Find<ModProjectile>("FlashFreezeExplosion").Type, damageDone / 4, 0, Player.whoAmI, 0f);
                    flashFreezeCooldown = 480;


                }
                if (umbralentropy == 2)
                {
                    target.AddBuff(BuffType<Starblight>(), 180);

                }
                if (Player.HasBuff(BuffType<AstarteDriver>()) && !target.HasBuff(BuffType<AstarteDriverEnemyCooldown>()))
                {

                    OnEnemyHitWithNova(target, 5, ref damageDone, ref hit.Crit);
                    target.AddBuff(BuffType<AstarteDriverEnemyCooldown>(), 60);

                }
            }

            if (weaknessexploit == 2 && hit.Crit)
            {
                if (target.HasBuff(BuffID.Confused)
                    || target.HasBuff(BuffID.CursedInferno)
                    || target.HasBuff(BuffID.Ichor)
                    || target.HasBuff(BuffID.BetsysCurse)
                    || target.HasBuff(BuffID.Midas)
                    || target.HasBuff(BuffID.Poisoned)
                    || target.HasBuff(BuffID.Venom)
                    || target.HasBuff(BuffID.OnFire)
                    || target.HasBuff(BuffID.Frostburn)
                    || target.HasBuff(BuffID.ShadowFlame))
                {
                    if (damageDone + damageDone * 0.25 < target.life)
                    {
                        Rectangle textPos = new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height);
                        CombatText.NewText(textPos, new Color(255, 30, 30, 240), $"{Math.Round(damageDone * 0.2)}", false, false);
                        target.life -= (int)(damageDone * 0.25);
                    }
                }
                else if (damageDone + damageDone * 0.1 < target.life)
                {
                    Rectangle textPos = new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height);
                    CombatText.NewText(textPos, new Color(255, 30, 30, 240), $"{Math.Round(damageDone * 0.1)}", false, false);
                    target.life -= (int)(damageDone * 0.1);
                }

            }
            if (Player.HasBuff(BuffType<SurtrTwilight>()))
            {
                target.AddBuff(BuffID.OnFire, 480);
            }
            if (!target.active)
            {
                OnKillEnemy(target);
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Projectile, consider using OnHitNPC instead */
        {
            if (Player.HasBuff(BuffType<SurtrTwilight>()) && proj.type != ProjectileType<LaevateinnDamage>())
            {

                target.AddBuff(BuffID.OnFire, 480);
            }


            if (Player.ownedProjectileCounts[ProjectileType<UnlimitedBladeWorksBackground>()] >= 1
                && proj.type != ProjectileType<UBWBladeFollowUp>()
                && proj.type != ProjectileType<UBWBladeFollowUpDelay>()
                && !Player.HasBuff(BuffType<UBWFollowUpCooldown>())
                && chosenStarfarer == 2
                && Player.HasBuff(BuffType<Bladeforged>()))
            {
                Player.AddBuff(BuffType<UBWFollowUpCooldown>(), 180);
                for (int i = 0; i < 3; i++)
                {
                    float offsetAmount = i * 120;
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0f, 0f, ProjectileType<UBWBladeFollowUp>(), baseNovaDamageAdd, 0, Player.whoAmI, 0, offsetAmount);

                }
                int killBlades = 3;
                if (killBlades > 0)
                {
                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile projTarget = Main.projectile[i];

                        if (projTarget.active && projTarget.type == ProjectileType<UBWBladeProjectile>())
                        {
                            if (Main.rand.NextBool(3))
                            {
                                SoundEngine.PlaySound(SoundID.Item37, Player.Center);
                                for (int ix = 0; ix < 30; ix++)
                                {
                                    Vector2 position = Vector2.Lerp(Player.Center, projTarget.Center, (float)ix / 30);
                                    Dust d = Dust.NewDustPerfect(position, DustID.GemTopaz, null, 240, default, 0.6f);
                                    d.fadeIn = 0.3f;
                                    d.noGravity = true;

                                }
                                projTarget.Kill();
                                killBlades--;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            if (Player.HasBuff(BuffType<AstarteDriver>()) && starfarerOutfit == 3 && proj.type != ProjectileType<StarfarerFollowUp>())
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, ProjectileType<StarfarerFollowUp>(), damageDone / 3, 0, Player.whoAmI);

            }

            if (!target.active && butchersdozen == 2)
            {
                butchersDozenKills++;
            }
            if (hit.Crit && flashfreeze == 2 && flashFreezeCooldown < 0)
            {
                //damage += target.lifeMax;
                SoundEngine.PlaySound(StarsAboveAudio.SFX_electroSmack, Player.Center);

                Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0, 0, Mod.Find<ModProjectile>("FlashFreezeExplosion").Type, damageDone / 4, 0, Player.whoAmI, 0f);
                flashFreezeCooldown = 240;


            }

            if (hit.Crit && !disablePromptsCombat)
            {
                if (Main.rand.Next(0, 8) == 0)
                {
                    starfarerPromptActive("onCrit");
                }

            }

            if (proj.type == ProjectileType<kiwamiryukenstun>())
            {
                Player.AddBuff(BuffType<KiwamiRyukenConfirm>(), 60);
                for (int d = 0; d < 4; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 113, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default(Color), 1.5f);
                }
            }


            if (proj.type == ProjectileType<Prototokia>())
            {
                prototokiaOnHit(target);
                target.AddBuff(BuffType<VoidAtrophy1>(), 1800);
                OnEnemyHitWithNova(target, 1, ref damageDone, ref hit.Crit);
            }

            if (proj.type == ProjectileType<Prototokia2>())
            {
                if (target.HasBuff(BuffType<VoidAtrophy1>()))
                {
                    if (chosenStarfarer == 1)
                    {
                        target.AddBuff(BuffType<VoidAtrophy2>(), 1800);
                    }
                    else
                    {
                        Player.AddBuff(BuffType<VoidStrength>(), 300);
                    }

                }
                prototokiaOnHit(target);
                OnEnemyHitWithNova(target, 1, ref damageDone, ref hit.Crit);
            }
            if (proj.type == ProjectileType<Prototokia3>())
            {
                prototokiaOnHit(target);
                OnEnemyHitWithNova(target, 1, ref damageDone, ref hit.Crit);
            }
            if (proj.type == ProjectileType<LaevateinnDamage>())
            {
                OnEnemyHitWithNova(target, 2, ref damageDone, ref hit.Crit);
            }
            if (proj.type == ProjectileType<kiwamiryukenconfirm>())
            {
                KiwamiRyukenOnHit(target);
                OnEnemyHitWithNova(target, 3, ref damageDone, ref hit.Crit);
            }

        }

        private void OnHitStarfarerDialogue(NPC target)
        {
            if (!disablePromptsCombat)
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
        }
        public override void GetHealMana(Item item, bool quickHeal, ref int healValue)
        {
            if(lavenderRefrain == 2)
            {
                
                
            }
            base.GetHealMana(item, quickHeal, ref healValue);
        }

        public override void OnMissingMana(Item item, int neededMana)
        {
            if(mysticIncision == 2 && Player.statLife >= Player.statLifeMax2/2)
            {
                Player.statMana = neededMana;
            }
            base.OnMissingMana(item, neededMana);
        }
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if(beyondinfinity == 2 && item.OriginalDamage <= 100)
            {
                bool gray = item.OriginalRarity == ItemRarityID.Gray;
                bool white = item.OriginalRarity == ItemRarityID.White;
                bool blue = item.OriginalRarity == ItemRarityID.Blue;
                bool green = item.OriginalRarity == ItemRarityID.Green;
                bool orange = item.OriginalRarity == ItemRarityID.Orange;
                bool lightred = item.OriginalRarity == ItemRarityID.LightRed;
                bool pink = item.OriginalRarity == ItemRarityID.Pink;
                bool lightpurple = item.OriginalRarity == ItemRarityID.LightPurple;
                bool lime = item.OriginalRarity == ItemRarityID.Lime;
                bool yellow = item.OriginalRarity == ItemRarityID.Yellow;
                bool cyan = item.OriginalRarity == ItemRarityID.Cyan;
                bool red = item.OriginalRarity == ItemRarityID.Red;
                bool purple = item.OriginalRarity == ItemRarityID.Purple;

                float damageMult = 1f +
                    (gray ? 20f : 0f) + //No weapons have a base rarity of gray, so this is just for fun
                    (white ? 12f : 0f) +
                    (blue ? 9f : 0f) +
                    (green ? 7f : 0f) +
                    (orange ? 6f : 0f) +
                    (lightred ? 4f : 0f) +
                    (pink ? 2f : 0f) +
                    (lightpurple ? 1.5f : 0f) +
                    (lime ? 1f : 0f) +
                    (yellow ? 0.5f : 0f) +
                    (cyan ? 0.5f : 0f) +
                    (red ? 0.05f : 0f) +
                    (purple ? 0.01f : 0f);

                damage *= damageMult;
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (arborealEchoes == 2)
            {
                if (target.life == target.lifeMax && target.CanBeChasedBy())
                {
                    modifiers.FinalDamage += 0.3f;
                    Player.Heal(10);
                }
            }
            if (kiTwinburst == 2)
            {
                modifiers.FinalDamage /= 2;
            }
            if (stayTheCourseStacks >= 0.15f && stayTheCourse == 2)
            {
                modifiers.CritDamage += 0.15f;
            }
            if (kineticConversion == 2)
            {
                modifiers.SourceDamage *= 1.1f + Player.velocity.Length() / 7f * 0.2f;
            }
            if (mysticforging == 2)
            {
                modifiers.DisableCrit();
                modifiers.SourceDamage += 0f + MathHelper.Lerp(0f, 0.6f, Player.GetCritChance(DamageClass.Generic) / 100f) / 2f;
            }


            //Will be replaced when these bosses get their new AI.

            if (target.type == NPCType<ArbitrationBoss>())
            {
                inArbiterFightTimer = 1200;

            }
            if (target.type == NPCType<PenthesileaBoss>())
            {
                inPenthFightTimer = 1200;

            }



            if (!target.active && butchersdozen == 2)
            {
                butchersDozenKills++;
            }

            if (starfarerOutfit == 4)
            {
                hopesBrilliance++;
                if (target.target != Player.whoAmI)
                {
                    modifiers.SourceDamage += 0.6f;
                }
            }

            if (bloomingflames == 2 && (Player.statLife < 100 || Player.HasBuff(BuffType<InfernalEnd>())))
            {
                target.AddBuff(BuffID.OnFire, 60);
            }

            if (Player.HasBuff(BuffType<AmaterasuGrace>()) && target.HasBuff(BuffID.Frostburn))
            {
                modifiers.SourceDamage += 0.5f;
            }
            if (Player.HasBuff(BuffType<AstarteDriver>()) && !target.HasBuff(BuffType<AstarteDriverEnemyCooldown>()))
            {
                modifiers.DamageVariationScale *= 0;

                modifiers.SourceDamage *= 0f;//Reset damage as we're using unique damage calculation.

                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    //If the attack was a crit
                    modifiers.SetCrit();
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage to get rid of crit damage calculation.
                    modifiers.FinalDamage.Flat = (float)(novaCritDamage * (1 + novaCritDamageMod));
                    ModifyHitEnemyWithNova(target, ref modifiers);
                    ModifyHitEnemyWithNovaCrit(target, ref modifiers);

                    if (chosenStarfarer == 1)
                    {
                        modifiers.FinalDamage.Flat += baseNovaDamageAdd / 10;
                    }
                }
                else
                {
                    //If the attack was not a crit
                    modifiers.DisableCrit();
                    modifiers.FinalDamage.Flat = (float)(novaDamage * (1 + novaDamageMod));
                    ModifyHitEnemyWithNovaNoCrit(target, ref modifiers);
                    ModifyHitEnemyWithNova(target, ref modifiers);
                }


                if (chosenStarfarer == 2)
                {
                    if (target.life < baseNovaDamageAdd / 10)
                    {
                        modifiers.SetCrit();
                        modifiers.FinalDamage.Flat += baseNovaDamageAdd / 4;

                    }
                }

            }

            if (weaknessexploit == 2)
            {
                modifiers.NonCritDamage -= 0.1f;


            }


        }
        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Item, consider using OnHitNPC instead */
        {


        }
        //Melee hits
        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Item, consider using ModifyHitNPC instead */
        {

        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Projectile, consider using ModifyHitNPC instead */
        {
            if (proj.type == ProjectileType<UBWBladeFollowUp>() || proj.type == ProjectileType<UBWBladeFollowUpDelay>())
            {
                modifiers.SourceDamage *= 0f;//Reset damage as we're using unique damage calculation.

                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    modifiers.SetCrit();
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage to get rid of crit damage calculation.
                    modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod)) * 0.016f;
                    ModifyHitEnemyWithNova(target, ref modifiers);
                    ModifyHitEnemyWithNovaCrit(target, ref modifiers);

                }
                else
                {
                    modifiers.DisableCrit();
                    modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod)) * 0.016f;
                    ModifyHitEnemyWithNovaNoCrit(target, ref modifiers);
                    ModifyHitEnemyWithNova(target, ref modifiers);
                }

            }
            if (proj.type == ProjectileType<ThundercrashDamage>())
            {
                modifiers.SourceDamage *= 0f;//Reset damage as we're using unique damage calculation.

                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    modifiers.SetCrit();
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage to get rid of crit damage calculation.
                    modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod));
                    //modifiers.FinalDamage *= 0.02f;//Reduce the final damage due to the amount of blades.
                    ModifyHitEnemyWithNova(target, ref modifiers);
                    ModifyHitEnemyWithNovaCrit(target, ref modifiers);

                }
                else
                {
                    modifiers.DisableCrit();
                    modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod));
                    //modifiers.FinalDamage *= 0.02f;//Reduce the final damage due to the amount of blades.
                    ModifyHitEnemyWithNovaNoCrit(target, ref modifiers);
                    ModifyHitEnemyWithNova(target, ref modifiers);
                }

            }
            if (proj.type == ProjectileType<GoldenGunBullet>())
            {
                modifiers.SourceDamage *= 0f;//Reset damage as we're using unique damage calculation.

                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    modifiers.SetCrit();
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage to get rid of crit damage calculation.
                    modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod)) * 0.33f;
                    ModifyHitEnemyWithNova(target, ref modifiers);
                    ModifyHitEnemyWithNovaCrit(target, ref modifiers);

                }
                else
                {
                    modifiers.DisableCrit();
                    modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod)) * 0.33f;
                    ModifyHitEnemyWithNovaNoCrit(target, ref modifiers);
                    ModifyHitEnemyWithNova(target, ref modifiers);
                }

            }
            if (proj.type == ProjectileType<SilenceSquall1>())
            {
                modifiers.SourceDamage *= 0f;//Reset damage as we're using unique damage calculation.

                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    modifiers.SetCrit();
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage to get rid of crit damage calculation.
                    modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod)) * 0.5f;
                    ModifyHitEnemyWithNova(target, ref modifiers);
                    ModifyHitEnemyWithNovaCrit(target, ref modifiers);

                }
                else
                {
                    modifiers.DisableCrit();
                    modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod)) * 0.5f;
                    ModifyHitEnemyWithNovaNoCrit(target, ref modifiers);
                    ModifyHitEnemyWithNova(target, ref modifiers);
                }

            }
            if (proj.type == ProjectileType<SilenceSquall2>())
            {
                modifiers.SourceDamage *= 0f;//Reset damage as we're using unique damage calculation.

                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    modifiers.SetCrit();
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage to get rid of crit damage calculation.
                    modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod)) * 0.5f;
                    ModifyHitEnemyWithNova(target, ref modifiers);
                    ModifyHitEnemyWithNovaCrit(target, ref modifiers);

                }
                else
                {
                    modifiers.DisableCrit();
                    modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod)) * 0.5f;
                    ModifyHitEnemyWithNovaNoCrit(target, ref modifiers);
                    ModifyHitEnemyWithNova(target, ref modifiers);
                }

            }
            if (proj.type == ProjectileType<SilenceSquallDamageField>())
            {
                modifiers.SourceDamage *= 0f;//Reset damage as we're using unique damage calculation.

                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    modifiers.SetCrit();
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage to get rid of crit damage calculation.
                    modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod)) / 8f;
                    ModifyHitEnemyWithNova(target, ref modifiers);
                    ModifyHitEnemyWithNovaCrit(target, ref modifiers);

                }
                else
                {
                    modifiers.DisableCrit();
                    modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod)) / 8f;
                    ModifyHitEnemyWithNovaNoCrit(target, ref modifiers);
                    ModifyHitEnemyWithNova(target, ref modifiers);
                }

            }
            if (proj.type == ProjectileType<GoldenGunExplosion>())
            {
                modifiers.SourceDamage *= 0f;//Reset damage as we're using unique damage calculation.

                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    modifiers.SetCrit();
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage to get rid of crit damage calculation.
                    modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod)) * 0.5f;
                    ModifyHitEnemyWithNova(target, ref modifiers);
                    ModifyHitEnemyWithNovaCrit(target, ref modifiers);

                }
                else
                {
                    modifiers.DisableCrit();
                    modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod)) * 0.5f;
                    ModifyHitEnemyWithNovaNoCrit(target, ref modifiers);
                    ModifyHitEnemyWithNova(target, ref modifiers);
                }

            }
            if (proj.type == ProjectileType<NovaBomb>())
            {
                modifiers.SourceDamage *= 0f;//Reset damage as we're using unique damage calculation.

                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    modifiers.SetCrit();
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage to get rid of crit damage calculation.
                    modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod)) * 0.5f;
                    modifiers.FinalDamage *= 0.5f;
                    ModifyHitEnemyWithNova(target, ref modifiers);
                    ModifyHitEnemyWithNovaCrit(target, ref modifiers);

                }
                else
                {
                    modifiers.DisableCrit();
                    modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod)) * 0.5f;
                    modifiers.FinalDamage *= 0.5f;
                    ModifyHitEnemyWithNovaNoCrit(target, ref modifiers);
                    ModifyHitEnemyWithNova(target, ref modifiers);
                }

            }
            if (proj.type == ProjectileType<NovaBombExplosion>())
            {
                modifiers.SourceDamage *= 0f;//Reset damage as we're using unique damage calculation.

                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    modifiers.SetCrit();
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage to get rid of crit damage calculation.
                    modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod)) * 0.5f;
                    ModifyHitEnemyWithNova(target, ref modifiers);
                    ModifyHitEnemyWithNovaCrit(target, ref modifiers);

                }
                else
                {
                    modifiers.DisableCrit();
                    modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod)) * 0.5f;
                    ModifyHitEnemyWithNovaNoCrit(target, ref modifiers);
                    ModifyHitEnemyWithNova(target, ref modifiers);
                }

            }
            if (proj.type == ProjectileType<WovenNeedle>())
            {
                modifiers.SourceDamage *= 0f;//Reset damage as we're using unique damage calculation.

                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    modifiers.SetCrit();
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage to get rid of crit damage calculation.
                    modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod)) / 10;
                    modifiers.FinalDamage *= 0.5f;
                    ModifyHitEnemyWithNova(target, ref modifiers);
                    ModifyHitEnemyWithNovaCrit(target, ref modifiers);

                }
                else
                {
                    modifiers.DisableCrit();
                    modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod)) / 10;
                    modifiers.FinalDamage *= 0.5f;
                    ModifyHitEnemyWithNovaNoCrit(target, ref modifiers);
                    ModifyHitEnemyWithNova(target, ref modifiers);
                }

            }
            if (proj.type == ProjectileType<Threadling>())
            {
                modifiers.SourceDamage *= 0f;//Reset damage as we're using unique damage calculation.

                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    modifiers.SetCrit();
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage to get rid of crit damage calculation.
                    modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod)) / 10;
                    modifiers.FinalDamage *= 0.5f;
                    ModifyHitEnemyWithNova(target, ref modifiers);
                    ModifyHitEnemyWithNovaCrit(target, ref modifiers);

                }
                else
                {
                    modifiers.DisableCrit();
                    modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod)) / 10;
                    modifiers.FinalDamage *= 0.5f;
                    ModifyHitEnemyWithNovaNoCrit(target, ref modifiers);
                    ModifyHitEnemyWithNova(target, ref modifiers);
                }

            }
            if (proj.type == ProjectileType<Prototokia>())
            {
                modifiers.SourceDamage *= 0f;//Reset damage as we're using unique damage calculation.

                SoundEngine.PlaySound(StarsAboveAudio.SFX_GunbladeImpact, Player.Center);
                screenShakeTimerGlobal = -80;
                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    modifiers.SetCrit();
                    novaGauge += trueNovaGaugeMax / 40;
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage to get rid of crit damage calculation.
                    if (proj.penetrate < 999)
                    {//If the projectile has penetrated at least 1 target
                        modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod)) * 0.25f;
                    }
                    else
                    {
                        modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod)) * 0.5f;
                    }
                    ModifyHitEnemyWithNova(target, ref modifiers);
                    ModifyHitEnemyWithNovaCrit(target, ref modifiers);

                }
                else
                {
                    modifiers.DisableCrit();
                    if (proj.penetrate < 999)
                    {//If the projectile has penetrated at least 1 target
                        modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod)) * 0.25f;
                    }
                    else
                    {
                        modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod)) * 0.5f;
                    }
                    ModifyHitEnemyWithNovaNoCrit(target, ref modifiers);
                    ModifyHitEnemyWithNova(target, ref modifiers);
                }
                if (proj.penetrate < 999)
                {//If the projectile has penetrated at least 1 target
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage
                }
            }

            if (proj.type == ProjectileType<Prototokia2>())
            {
                modifiers.SourceDamage *= 0f;//Reset damage as we're using unique damage calculation.

                SoundEngine.PlaySound(StarsAboveAudio.SFX_GunbladeImpact, Player.Center);
                screenShakeTimerGlobal = -80;
                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    modifiers.SetCrit();
                    novaGauge += trueNovaGaugeMax / 40;
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage to get rid of crit damage calculation.
                    if (proj.penetrate < 999)
                    {//If the projectile has penetrated at least 1 target
                        modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod)) * 0.25f;
                    }
                    else
                    {
                        modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod)) * 0.5f;
                    }

                    ModifyHitEnemyWithNova(target, ref modifiers);
                    ModifyHitEnemyWithNovaCrit(target, ref modifiers);

                }
                else
                {
                    modifiers.DisableCrit();
                    if (proj.penetrate < 999)
                    {//If the projectile has penetrated at least 1 target
                        modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod)) * 0.25f;
                    }
                    else
                    {
                        modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod)) * 0.5f;
                    }
                    ModifyHitEnemyWithNovaNoCrit(target, ref modifiers);
                    ModifyHitEnemyWithNova(target, ref modifiers);
                }


            }
            if (proj.type == ProjectileType<Prototokia3>())
            {
                modifiers.SourceDamage *= 0f;//Reset damage as we're using unique damage calculation.

                SoundEngine.PlaySound(StarsAboveAudio.SFX_GunbladeImpact, Player.Center);
                screenShakeTimerGlobal = -80;
                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    modifiers.SetCrit();
                    novaGauge += trueNovaGaugeMax / 40;
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage to get rid of crit damage calculation.
                    if (proj.penetrate < 999)
                    {//If the projectile has penetrated at least 1 target
                        modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod)) * 0.08f;
                    }
                    else
                    {
                        modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod)) * 0.25f;
                    }
                    ModifyHitEnemyWithNova(target, ref modifiers);
                    ModifyHitEnemyWithNovaCrit(target, ref modifiers);

                }
                else
                {
                    modifiers.DisableCrit();
                    if (proj.penetrate < 999)
                    {//If the projectile has penetrated at least 1 target
                        modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod)) * 0.08f;
                    }
                    else
                    {
                        modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod)) * 0.25f;
                    }
                    ModifyHitEnemyWithNovaNoCrit(target, ref modifiers);
                    ModifyHitEnemyWithNova(target, ref modifiers);
                }
            }
            if (proj.type == ProjectileType<LaevateinnDamage>())
            {
                modifiers.SourceDamage *= 0f;//Reset damage as we're using unique damage calculation.

                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    modifiers.SetCrit();
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage to get rid of crit damage calculation.
                    if (target.HasBuff(BuffID.OnFire))
                    {
                        modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod)) / 4;

                    }
                    else
                    {
                        modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod)) / 5;

                    }
                    ModifyHitEnemyWithNova(target, ref modifiers);
                    ModifyHitEnemyWithNovaCrit(target, ref modifiers);

                }
                else
                {
                    modifiers.DisableCrit();
                    modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod)) / 5;
                    ModifyHitEnemyWithNovaNoCrit(target, ref modifiers);
                    ModifyHitEnemyWithNova(target, ref modifiers);

                }
                if (target.HasBuff(BuffID.OnFire))
                {

                    modifiers.FinalDamage += 0.05f;

                    if (chosenStarfarer == 1)
                    {
                        modifiers.FinalDamage += 0.05f;
                    }
                }

            }
            if (proj.type == ProjectileType<kiwamiryukenconfirm>())
            {
                modifiers.SourceDamage *= 0f;//Reset damage as we're using unique damage calculation.

                SoundEngine.PlaySound(StarsAboveAudio.SFX_CounterFinish, Player.Center);
                screenShakeTimerGlobal = -80;
                novaGauge += 5;
                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    modifiers.SetCrit();
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage to get rid of crit damage calculation.
                    modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod));
                    ModifyHitEnemyWithNova(target, ref modifiers);
                    ModifyHitEnemyWithNovaCrit(target, ref modifiers);

                }
                else
                {
                    modifiers.DisableCrit();
                    modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod));
                    ModifyHitEnemyWithNovaNoCrit(target, ref modifiers);
                    ModifyHitEnemyWithNova(target, ref modifiers);
                }

            }
            if (Player.HasBuff(BuffType<AstarteDriver>()) && !target.HasBuff(BuffType<AstarteDriverEnemyCooldown>()))
            {
                modifiers.SourceDamage *= 0f;//Reset damage as we're using unique damage calculation.

                //Main.PlaySound(SoundLoader.customSoundType, (int)target.Center.X, (int)target.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/GunbladeImpact"));
                //screenShakeTimerGlobal = -80;
                int uniqueCrit = Main.rand.Next(100);
                if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                {
                    //If the attack was a crit
                    modifiers.SetCrit();
                    modifiers.FinalDamage *= 0.5f;//Halve the final damage to get rid of crit damage calculation.
                    modifiers.FinalDamage.Flat += (float)(novaCritDamage * (1 + novaCritDamageMod));
                    if (chosenStarfarer == 1)
                    {
                        modifiers.FinalDamage.Flat += baseNovaDamageAdd / 10;
                    }
                    ModifyHitEnemyWithNova(target, ref modifiers);
                    ModifyHitEnemyWithNovaCrit(target, ref modifiers);

                }
                else
                {
                    //If the attack was not a crit
                    modifiers.DisableCrit();
                    modifiers.FinalDamage.Flat += (float)(novaDamage * (1 + novaDamageMod));
                    ModifyHitEnemyWithNovaNoCrit(target, ref modifiers);
                    ModifyHitEnemyWithNova(target, ref modifiers);
                }


                if (chosenStarfarer == 2)
                {
                    if (target.life < baseNovaDamageAdd / 10)
                    {
                        modifiers.SetCrit();
                        modifiers.FinalDamage.Flat += baseNovaDamageAdd / 4;

                    }
                }

                target.AddBuff(BuffType<AstarteDriverEnemyCooldown>(), 60);
            }
        }

        private static void KiwamiRyukenOnHit(NPC target)
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
            for (int i = 0; i < 70; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            // Fire Dust spawn
            for (int i = 0; i < 80; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 3f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 5f;
                dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
                Main.dust[dustIndex].velocity *= 3f;
            }
            // Large Smoke Gore spawn
            for (int g = 0; g < 4; g++)
            {
                int goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default, Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default, Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default, Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                goreIndex = Gore.NewGore(null, new Vector2(target.position.X + (float)(target.width / 2) - 24f, target.position.Y + (float)(target.height / 2) - 24f), default, Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
            }
        }

        private void prototokiaOnHit(NPC target)
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

        }


        public override void ModifyScreenPosition()
        {
            Vector2 centerScreen = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
            if (lookAtTsukiyomi)
            {
                screenCache = Vector2.Lerp(screenCache, new Vector2(TsukiyomiLocation.X, TsukiyomiLocation.Y + 200) - centerScreen, 0.1f);
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
            
            if (screenShakeTimerGlobal < 0 && screenShakeTimerGlobal > -100)
            {
                if(disableScreenShake)
                {
                    return;
                }
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
        public override void UpdateEquips()
        {
            if(fabledFashion == 2)
            {
                for (int k = 10; k < 13; k++)
                {
                    Item item = Player.armor[k];
                    if (!item.IsAir && Player.IsItemSlotUnlockedAndUsable(k) && (!item.expertOnly || Main.expertMode))
                    {
                        Player.GrantArmorBenefits(item);
                    }
                }
            }
            

            base.UpdateEquips();
        }

        public override void SetStaticDefaults()
        {


        }
        public override void PreUpdate()
        {
            GlobalRotation++;
            if (GlobalRotation >= 360)
            {
                GlobalRotation = 0;
            }
            IntroDialogueTimer--;

            DrillMountBug();
            BossEnemySpawnModifier();
            DialogueEnemySpawnModifier();

            CutsceneProgress();
            timeAfterGettingHit++;

            //Stellar Array Values
            HealthyConfidence();
            BeyondInfinity();
            InnerAlchemy();
            BetweenTheBoundary();
            flashFreezeCooldown--;
            ammoRecycleCooldown--;
            aprismatismCooldown--;
            ButchersDozen();
            MysticForging();
            StayTheCourse();
            MysticIncision();
            umbralEntropyCooldown--;
            InevitableEnd();
            SwiftstrikeTheory();
            EmberFlask();
            
            DialogueScroll();

            AspectedDamageModification();
            Armsthrift();

            //Prevents "unknown boss" dialogue from repeating.
            seenUnknownBossTimer--;

            //Starfarer blink.
            blinkTimer++;
            if (blinkTimer >= 600)
            {
                blinkTimer = 0;
            }

            if (VagrantActive)
            {
                inVagrantFightTimer = 30;
            }

            if (promptIsActive)
            {
                if (promptDialogueScrollNumber >= promptDialogue.Length)
                {
                    starfarerPromptActiveTimer--;
                }
            }
            starfarerPromptCooldown--;



            if (starfarerPromptActiveTimer < 0 && promptIsActive)
            {
                promptDialogue = "";
                promptIsActive = false;
                promptDialogueScrollTimer = 0;
                promptDialogueScrollNumber = 0;
                starfarerPromptCooldown = starfarerPromptCooldownMax * 60 * 60;
                //starfarerPromptCooldown = 200;
            }
            if (cosmicPhoenixPrism)
            {
                if (Player.statMana < 40)
                {
                    Player.wingTime = 0;
                }
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


                StellarNova();



                costumeChangeOpacity -= 0.1f;

                GaussianBlur();
                NeonVeilShaderEffect();
                if (starfarerMenuActive)
                {
                    starfarerMenuUIOpacity += 0.1f;
                    gaussianBlurProgress += 0.2f;
                }
                else
                {
                    starfarerMenuUIOpacity -= 0.1f;
                }
                if (novaUIActive)
                {
                    gaussianBlurProgress += 0.2f;

                    novaUIOpacity += 0.1f;
                }
                else
                {
                    novaUIOpacity -= 0.1f;
                }
                if (stellarArray)
                {
                    gaussianBlurProgress += 0.2f;

                }
                starfarerMenuUIOpacity = MathHelper.Clamp(starfarerMenuUIOpacity, 0, 1);
                novaUIOpacity = MathHelper.Clamp(novaUIOpacity, 0, 1);


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



                WarriorOfLightUndertale();

                inWarriorOfLightFightTimer--;
                inNalhaunFightTimer--;
                inVagrantFightTimer--;
                inWarriorOfLightFightTimer--;
                inPenthFightTimer--;
                inArbiterFightTimer--;
                inCombat--;

                uniqueDialogueTimer--;

                StarfarerSelectionAnimation();
                StarfarerDialogueVisibility();
                StellarArrayVisibility();
                StarfarerPromptVisibility();

                //If there is dialogue...
                if (chosenDialogue != 0)
                {
                    StarsAboveDialogueSystem.SetupDialogueSystem(chosenStarfarer, ref chosenDialogue, ref dialoguePrep, ref dialogueLeft, ref expression, ref dialogue, Player, Mod);
                }
                SetupVNDialogue();

                animatedDialogue = dialogue.Substring(0, dialogueScrollNumber);//Dialogue increment magic

                //Boss kill prompts
                stellarGaugeMax = 5;
                SetupStarfarerOutfit();
                StellarDiskDialogue();
                StellarNovaSetup();
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
                        dust = Main.dust[Dust.NewDust(Player.Center, 0, 0, 181, 0f + Main.rand.Next(-22, 22), 0f + Main.rand.Next(-22, 22), 0, new Color(255, 255, 255), 1f)];
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
                if (activateAuthorityShockwaveEffect)
                {
                    rippleCount = 2;
                    rippleSpeed = 35;
                    rippleSize = 30;
                    activateAuthorityShockwaveEffect = false;
                    if (Main.netMode != NetmodeID.Server && !Filters.Scene["Shockwave"].IsActive())
                    {
                        Filters.Scene.Activate("Shockwave", Player.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(new Vector2(Player.Center.X, Player.Center.Y - 300));
                    }
                    shockwaveProgress = 0;
                }
                if (activateArbiterShockwaveEffect)
                {
                    if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
                    {
                        Filters.Scene.Deactivate("Shockwave");

                    }

                    rippleCount = 1;
                    rippleSpeed = 25;
                    rippleSize = 4;
                    activateArbiterShockwaveEffect = false;
                    if (Main.netMode != NetmodeID.Server && !Filters.Scene["Shockwave"].IsActive())
                    {
                        Filters.Scene.Activate("Shockwave", Player.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(new Vector2(Player.Center.X, Player.Center.Y));
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
                        Filters.Scene.Activate("Shockwave", Player.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(Player.GetModPlayer<WeaponPlayer>().BlackHolePosition);
                    }
                    shockwaveProgress = 0;
                }
                if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
                {
                    float progress = shockwaveProgress / 140f;
                    Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f));
                }
                if (shockwaveProgress >= 480)
                {
                    Filters.Scene.Deactivate("Shockwave");

                }
                shockwaveProgress++;

                //Nova Gauge charging.
                StellarNovaEnergy();


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


        }
        int lavenderRefrainReductionTimer;
        private void LavenderRefrain()
        {
            if (lavenderRefrain == 2)
            {
                lavenderRefrainMaxManaReduction = Math.Clamp(lavenderRefrainMaxManaReduction, 0f, 1f);
                Player.statManaMax2 = (int)(Player.statManaMax2 * lavenderRefrainMaxManaReduction);
                if (timeAfterGettingHit > 600)
                {
                    lavenderRefrainReductionTimer++;
                    if(lavenderRefrainReductionTimer > 120)
                    {
                        lavenderRefrainMaxManaReduction += 0.1f;

                        lavenderRefrainReductionTimer = 0;
                    }
                    lavenderRefrainMaxManaReduction = MathHelper.Clamp(lavenderRefrainMaxManaReduction, 0f, 1f);
                }
            }
        }

        private void SwiftstrikeTheory()
        {
            if (swiftstrikeTheory == 2)
            {
                if (Player.immune && Player.immuneTime > 0)
                {
                    Player.GetAttackSpeed(DamageClass.Generic) += 0.8f;
                }
            }
        }
        private void InevitableEnd()
        {
            if(inevitableEnd == 2)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && !npc.boss && npc.Distance(Player.Center) < 300 && npc.life < npc.lifeMax * 0.1 )
                    {
                        npc.StrikeInstantKill();//should be fine?

                    }
                }
            }
            
        }
        private void MysticIncision()
        {
            Player.GetArmorPenetration(DamageClass.Generic) += 20f;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.boss && npc.Distance(Player.Center) < 100)
                {
                    Player.GetArmorPenetration(DamageClass.Generic) += 50f;

                }
            }
        }
        
        private void Armsthrift()
        {
            if (armsthrift == 2)
            {
                if (armsthriftWeaponIDOld == 0)
                {
                    armsthriftWeaponIDOld = Player.HeldItem.type;
                    armsthriftWeaponTypeOld = Player.HeldItem.DamageType;
                    return;
                }
                if(armsthriftWeaponIDOld != Player.HeldItem.type && !Player.HasBuff(BuffType<ArmsthriftCooldown>()) && Player.HeldItem.damage > 0)
                {
                    Player.AddBuff(BuffType<ArmsthriftCooldown>(), 10 * 60);

                    if (armsthriftWeaponTypeOld != Player.HeldItem.DamageType)
                    {
                        //stronger buff
                        Player.AddBuff(BuffType<ArmsthriftBuffStrong>(), 60 * 5);
                    }
                    else
                    {
                        //buff
                        Player.AddBuff(BuffType<ArmsthriftBuff>(), 60 * 5);

                    }
                }
                if(Player.HeldItem.damage > 0)
                {
                    armsthriftWeaponIDOld = Player.HeldItem.type;
                    armsthriftWeaponTypeOld = Player.HeldItem.DamageType;
                }
                
            }
        }

        public float gaussianBlurProgress;
        private void GaussianBlur()
        {
            if(disableBlur)
            {
                if (Filters.Scene["GaussianBlur"].IsActive() && Main.netMode != NetmodeID.Server)
                {
                    Filters.Scene.Deactivate("GaussianBlur");

                }
                return;
            }
            gaussianBlurProgress -= 0.1f;
            if (Filters.Scene["GaussianBlur"].IsActive() && Main.netMode != NetmodeID.Server)
            {
                Filters.Scene["GaussianBlur"].GetShader().UseColor((float)(gaussianBlurProgress), 1, 1);

            }
            
            if (gaussianBlurProgress > 0f)//|| novaUIOpacity >= 1f || stellarArray || Player.GetModPlayer<CelestialCartographyPlayer>().CelestialCartographyActive)
            {
                if (!Filters.Scene["GaussianBlur"].IsActive() && Main.netMode != NetmodeID.Server)
                {
                    Filters.Scene.Activate("GaussianBlur").GetShader().UseColor(1,1,1);

                }
            }
            else if(gaussianBlurProgress <= 0f)
            {
                if (Filters.Scene["GaussianBlur"].IsActive() && Main.netMode != NetmodeID.Server)
                {
                    Filters.Scene.Deactivate("GaussianBlur");

                }
            }
            gaussianBlurProgress = MathHelper.Clamp(gaussianBlurProgress, 0f, 1f);
        }
        private void NeonVeilShaderEffect()
        {
            if(Player.InModBiome<NeonVeilBiome>())
            {
                if (!Filters.Scene["NeonVeilReflectionEffect"].IsActive() && Main.netMode != NetmodeID.Server)
                {
                    Filters.Scene.Activate("NeonVeilReflectionEffect").GetShader().UseColor(1, 1, 1).UseTargetPosition(new Vector2(Player.Center.X, (Main.maxTilesY - 110)*16));

                }
                if (Filters.Scene["NeonVeilReflectionEffect"].IsActive() && Main.netMode != NetmodeID.Server)
                {
                    float intensity = MathHelper.Lerp(1f, 3f, MathHelper.Clamp(Math.Abs(Player.Center.Y - ((Main.maxTilesY - 110) * 16)) / 70 - 1.98f, 0f, 1f));
                    Filters.Scene["NeonVeilReflectionEffect"].GetShader().UseIntensity(intensity).UseTargetPosition(new Vector2(Main.screenPosition.X, (Main.maxTilesY - 110) * 16));
                }
            }
            else
            {
                if (Filters.Scene["NeonVeilReflectionEffect"].IsActive() && Main.netMode != NetmodeID.Server)
                {
                    Filters.Scene.Deactivate("NeonVeilReflectionEffect");

                }
            }

        }

        private void EmberFlask()
        {
            if (Player.HasBuff(BuffType<EmberFlaskUsed>()))
            {
                if (inCombat > 0)
                {
                    Player.AddBuff(BuffType<EmberFlaskUsed>(), 10);
                }
                else
                {
                    Player.ClearBuff(BuffType<EmberFlaskUsed>());
                }
            }
        }
        private void StayTheCourse()
        {
            if (inCombat > 0 && stayTheCourse == 2)
            {
                stayTheCourseTimer++;
                if(stayTheCourseTimer > 60 * 10)
                {
                    //10 seconds has passed
                    stayTheCourseStacks += 0.1f;
                    stayTheCourseTimer = 0;
                    if(stayTheCourseStacks >= 0.15f)
                    {
                        stayTheCourseStacks = 0.15f;
                    }
                }
                //Main.NewText(stayTheCourseStacks);
            }
            else
            {
                stayTheCourseStacks = 0;
                stayTheCourseTimer = 0;
            }
        }
        private void CutsceneProgress()
        {
            astarteCutsceneProgress--;
            WhiteFade--;
        }
        private void StellarNovaSetup()
        {
            baseNovaDamageAdd = 100;
            if (NPC.downedSlimeKing)
            {
                baseNovaDamageAdd = 150;

            }
            if (NPC.downedBoss1)
            {
                baseNovaDamageAdd = 220;

            }
            if (NPC.downedBoss2)
            {
                baseNovaDamageAdd = 300;


            }
            if (NPC.downedBoss3)
            {
                baseNovaDamageAdd = 400;


            }
            if (Main.hardMode)
            {
                baseNovaDamageAdd = 900;


            }
            if (NPC.downedMechBossAny)
            {
                baseNovaDamageAdd = 1450;


            }
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {

                baseNovaDamageAdd = 2350;

            }

            if (NPC.downedPlantBoss)
            {
                baseNovaDamageAdd = 3050;


            }

            if (NPC.downedGolemBoss)
            {
                baseNovaDamageAdd = 4000;
            }

            if (NPC.downedAncientCultist)
            {
                baseNovaDamageAdd = 5200;

            }

            if (NPC.downedMoonlord)
            {
                baseNovaDamageAdd = 6600;

            }

            if (DownedBossSystem.downedWarrior)
            {
                baseNovaDamageAdd = 17550;

            }



            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                if ((bool)calamityMod.Call("GetBossDowned", "providence"))
                {
                    baseNovaDamageAdd = 20200;
                }
                if ((bool)calamityMod.Call("GetBossDowned", "allsentinel"))
                {
                    baseNovaDamageAdd = 24000;
                }
                if ((bool)calamityMod.Call("GetBossDowned", "devourerofgods"))
                {
                    baseNovaDamageAdd = 27500;

                    stellarGaugeMax++;
                    if (stellarGaugeUpgraded != 1)
                    {
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(Language.GetTextValue("The Stellar Array reaches new heights!"), 255, 0, 115); }
                        stellarGaugeUpgraded = 1;
                    }
                }
                if ((bool)calamityMod.Call("GetBossDowned", "yharon"))
                {
                    baseNovaDamageAdd = 33000;
                }
                if ((bool)calamityMod.Call("GetBossDowned", "supremecalamitas"))
                {
                    baseNovaDamageAdd = 47500;
                }
            }
        }
        private void StellarDiskDialogue()
        {
            if (SyncWorldProgress)
            {
                /*  if (SubworldSystem.IsActive<Observatory>() && observatoryDialogue == 0)
                  {
                      observatoryDialogue = 1;
                                          InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                      NewDiskDialogue = true;

                  }
                  if (observatoryDialogue == 2 && cosmicVoyageDialogue == 0)
                  {
                      cosmicVoyageDialogue = 1;
                                          InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                      NewDiskDialogue = true;

                  }*/

                if (NPC.downedSlimeKing && slimeDialogue == 0)
                {
                    slimeDialogue = 1;
                    InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                    if (Main.expertMode)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                        NewStellarArrayAbility = true;
                    }
                }
                if (NPC.downedBoss1 && eyeDialogue == 0)
                {
                    eyeDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                    InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                    NewStellarArrayAbility = true;


                }
                if (slimeDialogue == 2 && astrolabeIntroDialogue == 0)
                {
                    astrolabeIntroDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;



                }
                if (observatoryIntroDialogue == 0 && astrolabeIntroDialogue == 2 && SubworldSystem.IsActive<Observatory>())
                {
                    observatoryIntroDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;



                }
                if (NPC.downedBoss2 && corruptBossDialogue == 0)
                {
                    corruptBossDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                    InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                    NewStellarArrayAbility = true;

                }
                if (NPC.downedQueenBee && BeeBossDialogue == 0)
                {
                    BeeBossDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                    InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                    NewStellarArrayAbility = true;

                }

                if (NPC.downedBoss3 && SkeletonDialogue == 0)
                {
                    SkeletonDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                    InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                    NewStellarArrayAbility = true;

                }
                if (NPC.downedDeerclops && DeerclopsDialogue == 0)
                {
                    DeerclopsDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;

                }
                if (Main.hardMode && WallOfFleshDialogue == 0)//Hardmode
                {
                    WallOfFleshDialogue = 1;

                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                    InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                    NewStellarArrayAbility = true;
                }
                if (WallOfFleshWeaponDialogue == 2 && ForceWeaponDialogue == 0)//Hardmode
                {
                    ForceWeaponDialogue = 1;

                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247);}
                }
                if (GolemWeaponDialogue == 2 && GenocideWeaponDialogue == 0)//Hardmode
                {
                    GenocideWeaponDialogue = 1;

                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247);}
                }
                if (CorruptBossWeaponDialogue == 2 && TakodachiWeaponDialogue == 0)//Hardmode
                {
                    TakodachiWeaponDialogue = 1;

                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247);}
                }
                if (NPC.downedMechBoss1 && TwinsDialogue == 0)//The Twins
                {
                    TwinsDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                    if (bloomingflames == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                        NewStellarArrayAbility = true;
                    }


                }
                if (SkeletronPrimeDialogue == 2 && SkyStrikerWeaponDialogue == 0)//Hardmode
                {
                    SkyStrikerWeaponDialogue = 1;

                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247);}
                }
                if (LunaticCultistWeaponDialogue == 2 && TwinStarsWeaponDialogue == 0)//Hardmode
                {
                    TwinStarsWeaponDialogue = 1;

                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247);}
                }
                //Dialogue for Calamity Mod bosses.


                if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
                {
                    if ((bool)calamityMod.Call("GetBossDowned", "desertscourge") && desertscourgeDialogue == 0)
                    {
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        desertscourgeDialogue = 1;
                    }
                    if ((bool)calamityMod.Call("GetBossDowned", "crabulon") && crabulonDialogue == 0)
                    {
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        crabulonDialogue = 1;
                    }
                    if ((bool)calamityMod.Call("GetBossDowned", "hivemind") && hivemindDialogue == 0)
                    {
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        hivemindDialogue = 1;
                    }
                    if ((bool)calamityMod.Call("GetBossDowned", "perforator") && perforatorDialogue == 0)
                    {
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        perforatorDialogue = 1;
                    }
                    if ((bool)calamityMod.Call("GetBossDowned", "slimegod") && slimegodDialogue == 0)
                    {
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        slimegodDialogue = 1;
                    }
                    //Hardmode
                    if ((bool)calamityMod.Call("GetBossDowned", "cryogen") && cryogenDialogue == 0)
                    {
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        cryogenDialogue = 1;
                    }
                    if ((bool)calamityMod.Call("GetBossDowned", "aquaticscourge") && aquaticscourgeDialogue == 0)
                    {
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        aquaticscourgeDialogue = 1;
                    }
                    if ((bool)calamityMod.Call("GetBossDowned", "brimstoneelemental") && brimstoneelementalDialogue == 0)
                    {
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        brimstoneelementalDialogue = 1;
                    }
                    if ((bool)calamityMod.Call("GetBossDowned", "calamitasClone") && calamitasDialogue == 0)
                    {
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        calamitasDialogue = 1;
                    }
                    if ((bool)calamityMod.Call("GetBossDowned", "anahitaleviathan") && leviathanDialogue == 0)
                    {
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        leviathanDialogue = 1;
                    }
                    if ((bool)calamityMod.Call("GetBossDowned", "astrumaureus") && astrumaureusDialogue == 0)
                    {
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        astrumaureusDialogue = 1;
                    }
                    if ((bool)calamityMod.Call("GetBossDowned", "plaguebringergoliath") && plaguebringerDialogue == 0)
                    {
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        plaguebringerDialogue = 1;
                    }
                    if ((bool)calamityMod.Call("GetBossDowned", "ravager") && ravagerDialogue == 0)
                    {
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        ravagerDialogue = 1;
                    }
                    if ((bool)calamityMod.Call("GetBossDowned", "astrumdeus") && astrumdeusDialogue == 0)
                    {
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        astrumdeusDialogue = 1;
                    }
                }

                if (NPC.downedQueenSlime && QueenSlimeDialogue == 0)
                {
                    QueenSlimeDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;

                }
                if (DownedBossSystem.downedNalhaun && nalhaunDialogue == 0)
                {
                    nalhaunDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;



                    InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                    NewStellarArrayAbility = true;


                }
                if (DownedBossSystem.downedDioskouroi && dioskouroiDialogue == 0)
                {
                    dioskouroiDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;

                    //NewStellarNova = true;
                    //if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                    //NewStellarArrayAbility = true;


                }
                if (DownedBossSystem.downedPenth && penthDialogue == 0)
                {
                    penthDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;

                    NewStellarNova = true;
                    InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                    NewStellarArrayAbility = true;


                }
                if (DownedBossSystem.downedArbiter && arbiterDialogue == 0)
                {
                    arbiterDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;

                    //NewStellarNova = true;
                    if (Main.expertMode)
                    {
                        //if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                        //NewStellarArrayAbility = true;
                    }
                }
                if (DownedBossSystem.downedTsuki && tsukiyomiDialogue == 0)
                {
                    //tsukiyomiDialogue = 1;
                    //NewDiskDialogue = true;
                }

                if (tsukiyomiDialogue >= 1)
                {
                    if (edingenesisquasar == 0)
                    {
                    InGameNotificationsTracker.AddNotification(new NewNovaNotification());

                        edingenesisquasar = 1;
                    }
                }


                if (NPC.downedMechBoss2 && DestroyerDialogue == 0)//The Destroyer
                {
                    DestroyerDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                    if (bloomingflames == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                        NewStellarArrayAbility = true;
                    }


                }
                if (NPC.downedMechBoss3 && SkeletronPrimeDialogue == 0)//Skeletron Prime
                {
                    SkeletronPrimeDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                    if (bloomingflames == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                        NewStellarArrayAbility = true;
                    }


                }
                if (SkeletronPrimeDialogue == 2 && TwinsDialogue == 2 && DestroyerDialogue == 2 && AllMechsDefeatedDialogue == 0)//All Mech Bosses Defeated + Dialogue read
                {
                    AllMechsDefeatedDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;




                }
                if (NPC.downedPlantBoss && PlanteraDialogue == 0)
                {
                    PlanteraDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                    InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                    NewStellarArrayAbility = true;
                    //if (Main.netMode != NetmodeID.Server){Main.NewText(Language.GetTextValue("You have acquired a new Stellar Nova!"), 190, 100, 247);}

                }
                if (NPC.downedGolemBoss && GolemDialogue == 0)
                {
                    GolemDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                    if (Main.expertMode)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                        NewStellarArrayAbility = true;
                    }

                }
                if (NPC.downedEmpressOfLight && EmpressDialogue == 0)
                {
                    EmpressDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;


                }
                if (NPC.downedAncientCultist && CultistDialogue == 0)
                {
                    CultistDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                    if (Main.expertMode)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                        NewStellarArrayAbility = true;
                    }

                }
                if (NPC.downedMoonlord && MoonLordDialogue == 0)
                {
                    MoonLordDialogue = 1;


                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;

                    InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                    NewStellarArrayAbility = true;


                }
                if (NPC.downedFishron && DukeFishronDialogue == 0)
                {
                    DukeFishronDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                }
                if ((EverlastingLightEvent.isEverlastingLightPreviewActive || EverlastingLightEvent.isEverlastingLightActive) && warriorBossItemDialogue == 0 && vagrantDialogue == 2 && !SubworldSystem.AnyActive())
                {
                    warriorBossItemDialogue = 1;
                    if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue("Common.HarshLight"), 239, 221, 106); }
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());

                }
                if (EverlastingLightEvent.isEverlastingLightActive && !onEverlastingLightText && !SubworldSystem.AnyActive())
                {
                    onEverlastingLightText = true;
                    if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue("Common.EverlastingLight"), 239, 221, 106); }


                }
                if (DownedBossSystem.downedWarrior && WarriorOfLightDialogue == 0)
                {
                    WarriorOfLightDialogue = 1;
                    if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue("Common.EverlastingLightEnd"), 239, 221, 106); }
                    if (ModLoader.TryGetMod("BossChecklist", out Mod BossChecklist))
                    {


                    }


                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;
                    if (BossChecklist != null)
                    {
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.TsukiyomiBossChecklist"), 241, 255, 180); }

                    }

                    InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                    NewStellarArrayAbility = true;


                }
                if (DownedBossSystem.downedVagrant && vagrantDialogue == 0)
                {
                    vagrantDialogue = 1;

                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;

                    InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                    NewStellarArrayAbility = true;

                    InGameNotificationsTracker.AddNotification(new NewNovaNotification());
                    NewStellarNova = true;


                }

                if (DownedBossSystem.downedThespian && thespianDialogue == 0)
                {
                    thespianDialogue = 1;

                    InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;

                    


                }
                if (DownedBossSystem.downedStarfarers && starfarerPostBattleDialogue == 0)
                {
                    starfarerPostBattleDialogue = 1;

                    InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;




                }
                if (NPC.downedBoss1 && NPC.downedSlimeKing && NPC.downedBoss2 && NPC.downedBoss3 && NPC.downedQueenBee && NPC.downedQueenSlime && NPC.downedEmpressOfLight && Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss && NPC.downedFishron && NPC.downedMoonlord && AllVanillaBossesDefeatedDialogue == 0)
                {
                    //AllVanillaBossesDefeatedDialogue = 1;
                    //                    InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    //NewDiskDialogue = true;

                    //if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.ArrayAbility"), 190, 100, 247); }
                    //NewStellarArrayAbility = true;


                }
                if (NPC.downedBoss1 && NPC.downedSlimeKing && NPC.downedBoss2 && NPC.downedBoss3 && NPC.downedQueenBee && NPC.downedQueenSlime && NPC.downedEmpressOfLight && Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss && NPC.downedFishron && NPC.downedMoonlord && DownedBossSystem.downedWarrior && Main.expertMode && EverythingDefeatedDialogue == 0)
                {
                    //Expert mode only
                    EverythingDefeatedDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;

                    InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                    NewStellarArrayAbility = true;


                }

                if (Player.ZoneUnderworldHeight && SkeletonWeaponDialogue == 2 && HellWeaponDialogue == 0)
                {
                    HellWeaponDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;

                }
                if (Player.GetModPlayer<CelestialCartographyPlayer>().stellaglyphTier >= 2 && Stellaglyph2WeaponDialogue == 0)
                {
                    Stellaglyph2WeaponDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;

                }
                if (SkeletonWeaponDialogue == 2 && NanomachineWeaponDialogue == 0)
                {
                    NanomachineWeaponDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;

                }
                //Boss Spawn items
                if (vagrantBossItemDialogue == 0 && NPC.downedBoss1)
                {
                    vagrantBossItemDialogue = 1;
                    InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                }
                if (dioskouroiBossItemDialogue == 0 && SkeletonDialogue == 2 && vagrantDialogue == 2)
                {
                    dioskouroiBossItemDialogue = 1;
                    InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());

                }
                if (penthBossItemDialogue == 0 && (SkeletronPrimeDialogue == 2 || TwinsDialogue == 2 || DestroyerDialogue == 2) && vagrantDialogue == 2)
                {
                    penthBossItemDialogue = 1;
                    InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());

                }
                if (starfarerBossItemDialogue == 0 && DownedBossSystem.downedPenth && DownedBossSystem.downedThespian && DownedBossSystem.downedDioskouroi && DownedBossSystem.downedVagrant && NPC.downedPlantBoss)
                {
                    starfarerBossItemDialogue = 1;
                    InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());

                }
                if (starfarerPostBattleDialogue == 0 && DownedBossSystem.downedStarfarers)
                {
                    starfarerPostBattleDialogue = 1;
                    InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());

                }
                if (nalhaunBossItemDialogue == 0 && GolemDialogue == 2 && vagrantDialogue == 2)
                {

                    nalhaunBossItemDialogue = 1;
                    InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());

                }

                //Zone specific weapons do not have delay
                if (Player.ZoneHallow && GoldWeaponDialogue == 0)
                {
                    GoldWeaponDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;

                    return;

                }
                if (Player.ZoneGraveyard && FarewellWeaponDialogue == 0)
                {
                    FarewellWeaponDialogue = 1;
                                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                    NewDiskDialogue = true;

                    return;

                }
                if (WeaponDialogueTimer <= 0)//7200 = 2 min in between 
                {//The order of these should not matter.
                    if (vagrantDialogue == 2 && TrickspinWeaponDialogue == 0)
                    {
                        TrickspinWeaponDialogue = 1;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                    }
                    if (NPC.downedBoss2 && SoldierWeaponDialogue == 0)
                    {
                        SoldierWeaponDialogue = 1;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                    }
                    if (PlanteraWeaponDialogue == 2 && DreamerWeaponDialogue == 0)
                    {
                        DreamerWeaponDialogue = 1;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                    }
                    if (DukeFishronWeaponDialogue == 2 && KineticWeaponDialogue == 0)
                    {
                        KineticWeaponDialogue = 1;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                    }
                    if (SkeletonDialogue == 2 && SkeletonWeaponDialogue == 0)
                    {
                        SkeletonWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;

                    }
                    if (tsukiyomiDialogue == 2 && ArchitectWeaponDialogue == 0)
                    {
                        ArchitectWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;


                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;


                    }
                    if (ArchitectWeaponDialogue == 2 && CosmicDestroyerWeaponDialogue == 0)
                    {
                        CosmicDestroyerWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;


                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;



                    }
                    if (CosmicDestroyerWeaponDialogue == 2 && KarnaWeaponDialogue == 0)
                    {
                        KarnaWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;


                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;



                    }
                    if (MurasamaWeaponDialogue == 0 && NPC.downedEmpressOfLight && Main.masterMode && DownedBossSystem.downedVagrant)
                    {
                        //Obtained from Arbitration now.

                        //MurasamaWeaponDialogue = 1;
                        //                    InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        //NewDiskDialogue = true;
                        //WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        //return;
                    }
                    if (MercyWeaponDialogue == 0 && NPC.downedGolemBoss)
                    {
                        MercyWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;
                    }
                    if (ThespianWeaponDialogue == 0 && thespianDialogue == 2)
                    {
                        ThespianWeaponDialogue = 1;
                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;
                    }
                    if (DragaliaWeaponDialogue == 0 && ThespianWeaponDialogue == 2)
                    {
                        DragaliaWeaponDialogue = 1;
                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;
                    }
                    if (WavedancerWeaponDialogue == 0 && ThespianWeaponDialogue == 2)
                    {
                        WavedancerWeaponDialogue = 1;
                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;
                    }
                    if (ClarentWeaponDialogue == 0 && DownedBossSystem.downedNalhaun)
                    {
                        ClarentWeaponDialogue = 1;
                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;
                    }
                    if (GundbitWeaponDialogue == 0 && LunaticCultistWeaponDialogue == 2)
                    {
                        GundbitWeaponDialogue = 1;
                        InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;
                    }
                    if (SakuraWeaponDialogue == 0 && NPC.downedEmpressOfLight)
                    {
                        SakuraWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;
                    }
                    if (EternalWeaponDialogue == 0 && NPC.downedMoonlord)
                    {
                        EternalWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;
                    }
                    if (DaemonWeaponDialogue == 0 && NPC.downedMoonlord)
                    {
                        DaemonWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;
                    }
                    if (OzmaWeaponDialogue == 0 && NPC.downedAncientCultist)
                    {
                        OzmaWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;
                    }
                    if (UrgotWeaponDialogue == 0 && NPC.downedQueenSlime)
                    {
                        UrgotWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;
                    }
                    if (BloodWeaponDialogue == 0 && NPC.downedHalloweenKing)
                    {
                        BloodWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;
                    }
                    if (MorningStarWeaponDialogue == 0 && NPC.downedDeerclops)
                    {
                        MorningStarWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;
                    }
                    if (VirtueWeaponDialogue == 0 && NPC.downedMoonlord)
                    {
                        VirtueWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;
                    }
                    if (QueenSlimeWeaponDialogue == 0 && NPC.downedQueenSlime)
                    {
                        QueenSlimeWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;
                    }
                    if (NeedlepointWeaponDialogue == 0 && NPC.downedEmpressOfLight)
                    {
                        NeedlepointWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;
                    }
                    if (eyeDialogue == 2 && EyeBossWeaponDialogue == 0)
                    {
                        EyeBossWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;

                    }
                    if (corruptBossDialogue == 2 && CorruptBossWeaponDialogue == 0)
                    {
                        CorruptBossWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);
                        return;

                    }
                    //Nalhaun weapons have been moved...
                    if (dioskouroiDialogue == 2 && NalhaunWeaponDialogue == 0)
                    {
                        NalhaunWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    //Vagrant weapons have been moved...
                    if (QueenSlimeWeaponDialogue == 2 && VagrantWeaponDialogue == 0)
                    {
                        VagrantWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (BeeBossDialogue == 2 && QueenBeeWeaponDialogue == 0)
                    {
                        QueenBeeWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (SkeletonWeaponDialogue == 2 && OceanWeaponDialogue == 0 && Player.ZoneBeach)
                    {
                        OceanWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (SkeletonWeaponDialogue == 2 && MiseryWeaponDialogue == 0)
                    {
                        MiseryWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (slimeDialogue == 2 && KingSlimeWeaponDialogue == 0)
                    {
                        KingSlimeWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (WallOfFleshDialogue == 2 && WallOfFleshWeaponDialogue == 0)
                    {
                        WallOfFleshWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;
                    }
                    if (WallOfFleshWeaponDialogue == 2 && LumaWeaponDialogue == 0)
                    {
                        LumaWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;
                    }
                    if ((TwinsDialogue == 2 || DestroyerDialogue == 2 || SkeletronPrimeDialogue == 2) && MechBossWeaponDialogue == 0)
                    {
                        MechBossWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;
                    }
                    if (TwinsDialogue == 2 && DestroyerDialogue == 2 && SkeletronPrimeDialogue == 2 && AllMechBossWeaponDialogue == 0 && MechBossWeaponDialogue == 2 && AllMechBossWeaponDialogue == 0)
                    {
                        AllMechBossWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (MechBossWeaponDialogue == 2 && HullwroughtWeaponDialogue == 0)
                    {
                        HullwroughtWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (HullwroughtWeaponDialogue == 2 && MonadoWeaponDialogue == 0)
                    {
                        MonadoWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }

                    if (PlanteraDialogue == 2 && PlanteraWeaponDialogue == 0)
                    {
                        PlanteraWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (NPC.downedChristmasIceQueen && FrostMoonWeaponDialogue == 0)
                    {
                        FrostMoonWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (GolemDialogue == 2 && GolemWeaponDialogue == 0)
                    {
                        GolemWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (penthDialogue == 2 && PenthesileaWeaponDialogue == 0)
                    {
                        PenthesileaWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (PenthesileaWeaponDialogue == 2 && MuseWeaponDialogue == 0)
                    {
                        MuseWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (PlanteraWeaponDialogue == 2 && KifrosseWeaponDialogue == 0)
                    {
                        KifrosseWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    //Moved from Arbitration to Nalhaun.
                    if (nalhaunDialogue == 2 && ArbitrationWeaponDialogue == 0)
                    {
                        ArbitrationWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (ArbitrationWeaponDialogue == 2 && LevinstormWeaponDialogue == 0)
                    {
                        LevinstormWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (ArbitrationWeaponDialogue == 2 && ClaimhWeaponDialogue == 0)
                    {
                        ClaimhWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (DukeFishronDialogue == 2 && DukeFishronWeaponDialogue == 0)
                    {
                        DukeFishronWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (DukeFishronWeaponDialogue == 2 && ManiacalWeaponDialogue == 0)
                    {
                        ManiacalWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (CultistDialogue == 2 && LunaticCultistWeaponDialogue == 0)
                    {
                        LunaticCultistWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (MoonLordDialogue == 2 && MoonLordWeaponDialogue == 0)
                    {
                        MoonLordWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (MoonLordWeaponDialogue == 2 && ShadowlessWeaponDialogue == 0)
                    {
                        ShadowlessWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (WarriorOfLightDialogue == 2 && WarriorWeaponDialogue == 0)
                    {
                        WarriorWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (WarriorOfLightDialogue == 2 && AuthorityWeaponDialogue == 0)
                    {
                        AuthorityWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (WarriorOfLightDialogue == 2 && RedMageWeaponDialogue == 0)
                    {
                        RedMageWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (WarriorOfLightDialogue == 2 && BlazeWeaponDialogue == 0)
                    {
                        BlazeWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (WarriorOfLightDialogue == 2 && PickaxeWeaponDialogue == 0)
                    {
                        PickaxeWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (AllMechsDefeatedDialogue == 2 && HardwareWeaponDialogue == 0)
                    {
                        HardwareWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (LunaticCultistWeaponDialogue == 2 && CatalystWeaponDialogue == 0)
                    {
                        CatalystWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (CatalystWeaponDialogue == 2 && UmbraWeaponDialogue == 0)
                    {
                        UmbraWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (SaltwaterWeaponDialogue == 0 && NPC.downedPirates)
                    {
                        SaltwaterWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;
                    }
                    if (ClockWeaponDialogue == 0 && vagrantDialogue == 2)
                    {
                        ClockWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;
                    }
                    if (SanguineWeaponDialogue == 0 && Player.HasItem(ItemID.GuideVoodooDoll) && Player.difficulty == PlayerDifficultyID.Hardcore)
                    {
                        SanguineWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;
                    }
                    if (GoldlewisWeaponDialogue == 0 && NPC.downedMartians)
                    {
                        GoldlewisWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;
                    }
                    if (ChaosWeaponDialogue == 0 && NPC.downedQueenSlime)
                    {
                        ChaosWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;
                    }
                    if (GolemWeaponDialogue == 2 && SilenceWeaponDialogue == 0)
                    {
                        SilenceWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }
                    if (MoonLordWeaponDialogue == 2 && SoulWeaponDialogue == 0)
                    {
                        SoulWeaponDialogue = 1;
                                            InGameNotificationsTracker.AddNotification(new DiskDialogueNotification());
                        NewDiskDialogue = true;
                        WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                        return;

                    }

                    WeaponDialogueTimer = Main.rand.Next(3600, 7200);

                }




                if (NPC.downedSlimeKing && Main.expertMode == true)
                {

                    if (aquaaffinity == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        aquaaffinity = 1;
                    }
                    if(stayTheCourse == 0)
                    {
                        stayTheCourse = 1;

                    }
                }
                if (NPC.downedBoss1)
                {

                    if (starshower == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        starshower = 1;
                    }
                    if(spectralNail == 0)
                    {
                        spectralNail = 1;

                    }
                }
                if (NPC.downedBoss2) // Eater/Brain
                {
                    if (ironskin == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        ironskin = 1;
                    }
                    if(armsthrift == 0)
                    {
                        armsthrift = 1;

                    }
                }
                if (NPC.downedQueenBee)
                {
                    if (evasionmastery == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        evasionmastery = 1;
                    }
                    if(mysticIncision == 0)
                    {
                        mysticIncision = 1;

                    }
                }
                if (NPC.downedBoss3) // Skeletron
                {
                    if (inneralchemy == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        inneralchemy = 1;
                    }
                    if (catharsis == 0)
                    {
                        catharsis = 1;

                    }
                }
                if (Player.statLifeMax >= 400)
                {
                    if (healthyConfidence == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        healthyConfidence = 1;
                    }
                }
                if (DownedBossSystem.downedVagrant)
                {
                    novaGaugeUnlocked = true;
                    if (prototokia == 0)
                    {
                        chosenStellarNova = 1;
                        prototokia = 1;
                    }
                }
                if (NPC.downedBoss2)
                {
                    if (unlimitedbladeworks == 0)
                    {
                    InGameNotificationsTracker.AddNotification(new NewNovaNotification());
                        NewStellarNova = true;
                        unlimitedbladeworks = 1;
                    }

                }
                if (NPC.downedBoss3 || Player.ZoneMeteor)
                {
                    if (guardianslight == 0)
                    {
                    InGameNotificationsTracker.AddNotification(new NewNovaNotification());
                        NewStellarNova = true;
                        guardianslight = 1;
                    }

                }
                if (DownedBossSystem.downedNalhaun)
                {
                    if (butchersdozen == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        butchersdozen = 1;
                    }
                    if(swiftstrikeTheory == 0)
                    {
                        swiftstrikeTheory = 1;

                    }
                    if (laevateinn == 0)
                    {
                    InGameNotificationsTracker.AddNotification(new NewNovaNotification());
                        NewStellarNova = true;
                        laevateinn = 1;
                    }
                }
                if(DownedBossSystem.downedThespian)
                {

                }
                if(DownedBossSystem.downedDioskouroi)
                {
                    if (celestialevanesence == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        celestialevanesence = 1;
                    }
                    if (arborealEchoes == 0)
                    {
                        arborealEchoes = 1;

                    }
                }
                if (DownedBossSystem.downedPenth)
                {
                    if (mysticforging == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        mysticforging = 1;
                    }
                    if(lavenderRefrain == 0)
                    {
                        lavenderRefrain = 1;
                    }
                    if (gardenofavalon == 0)
                    {
                    InGameNotificationsTracker.AddNotification(new NewNovaNotification());
                        NewStellarNova = true;
                        gardenofavalon = 1;
                    }
                }
                if (NPC.downedMechBossAny)
                {
                    if (bloomingflames == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        bloomingflames = 1;
                    }


                }
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                {

                    if (astralmantle == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                        NewStellarArrayAbility = true;
                        astralmantle = 1;
                    }
                }

                if (NPC.downedPlantBoss)
                {
                    if (afterburner == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        afterburner = 1;
                    }

                }
                if (NPC.downedHalloweenKing)
                {

                    if (livingdead == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        livingdead = 1;
                    }
                    if(kineticConversion == 0)
                    {
                        kineticConversion = 1;

                    }
                }
                if (NPC.downedChristmasIceQueen)
                {
                    if (hikari == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        hikari = 1;
                    }
                    if(fabledFashion == 0)
                    {
                        fabledFashion = 1;

                    }
                }
                if (NPC.downedGolemBoss)
                {
                }
                if (NPC.downedGolemBoss && Main.expertMode == true)
                {

                    if (weaknessexploit == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        weaknessexploit = 1;
                    }
                }
                if (NPC.downedAncientCultist)
                {

                }
                if (NPC.downedMoonlord)
                {
                    if (umbralentropy == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        umbralentropy = 1;
                    }
                    if(kiTwinburst == 0)
                    {
                        kiTwinburst = 1;

                    }

                }
                if (NPC.downedMechBossAny)
                {
                    if (kiwamiryuken == 0)
                    {
                        kiwamiryuken = 1;
                    }


                }
                if (DownedBossSystem.downedPenth && Main.expertMode)
                {
                    if (flashfreeze == 0)
                    {

                        flashfreeze = 1;
                    }
                    if(inevitableEnd == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        inevitableEnd = 1;

                    }

                }

                if (NPC.downedMoonlord && Main.expertMode == true)
                {
                    if (keyofchronology == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        keyofchronology = 1;
                    }

                }
                if (DownedBossSystem.downedStarfarers)
                {


                    if (artofwar == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                        NewStellarArrayAbility = true;
                        artofwar = 1;
                    }

                }
                if (DownedBossSystem.downedThespian && Main.expertMode)
                {


                    if (aprismatism == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                        NewStellarArrayAbility = true;
                        aprismatism = 1;
                    }

                }
                if (NPC.downedMartians)
                {
                    if (avataroflight == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        avataroflight = 1;
                    }



                }
                if (DownedBossSystem.downedWarrior)
                {
                    if (beyondtheboundary == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());
                        NewStellarArrayAbility = true;
                        beyondtheboundary = 1;
                    }



                }
                if (NPC.downedBoss1 && NPC.downedSlimeKing && NPC.downedBoss2 && NPC.downedBoss3 && NPC.downedQueenBee && Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss && NPC.downedFishron && NPC.downedMoonlord)
                {
                    if (beyondinfinity == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        beyondinfinity = 1;
                    }

                }
                if (NPC.downedBoss1 && NPC.downedSlimeKing && NPC.downedBoss2 && NPC.downedBoss3 && NPC.downedQueenBee && Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && NPC.downedPlantBoss && NPC.downedGolemBoss && NPC.downedFishron && NPC.downedMoonlord && DownedBossSystem.downedWarrior && Main.expertMode == true)
                {
                    if (unbridledradiance == 0)
                    {
                        InGameNotificationsTracker.AddNotification(new ArrayAbilityNotification());

                        unbridledradiance = 1;
                    }
                }
                // 


            }
        }
        private void SetupVNDialogue()
        {
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
                    dialogue = " ";
                    dialogueScrollTimer = 0;
                    dialogueScrollNumber = 0;

                    VNDialogueChoice1 = " ";
                    VNDialogueChoice2 = " ";
                    VNDialogueChoice3 = " ";

                    VNDialogueThirdOption = false;
                }
                dialogue = LangHelper.Wrap((string)VNScenes.SetupVNSystem(sceneID, sceneProgression)[13], 50);
                //animatedDialogue = dialogue.Substring(0, dialogueScrollNumber);


            }
        }
        private void StarfarerDialogueVisibility()
        {
            if (starfarerDialogue)
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
            if (VNDialogueActive)
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
        }
        private void StellarArrayVisibility()
        {
            if (stellarArray)
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
            if (stellarArray)
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
        }
        private void StarfarerPromptVisibility()
        {
            if (promptIsActive)
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
            if (promptVisibility > 0.5)
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
        }
        private void StarfarerSelectionAnimation()
        {
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
        }
        private void WarriorOfLightUndertale()
        {
            if (WarriorOfLightActive == false && undertaleActive == true)
            {
                undertaleActive = false;
            }
            if (undertalePrep)
            {
                undertaleiFrames = 120;
                heartX = 380;
                heartY = 160;

                undertalePrep = false;
            }
            if (damageTakenInUndertale == true)
            {
                damageTakenInUndertale = false;
                SoundEngine.PlaySound(StarsAboveAudio.SFX_TakingDamage, Player.Center);

            }
        }
        private void StellarNova()
        {
            novaDamageMod = 0;
            novaCritChanceMod = 0;
            novaCritDamageMod = 0;
            novaChargeMod = 0;
            if (affixItem1 != null && affixItem2 != null && affixItem3 != null)
            {
                //TODO: replace with a better system.
                //Just in case the idea of "different slots provide different bonuses" becomes true.
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<RefulgentPrism>() ||
                    affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<RefulgentPrism>() ||
                    affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<RefulgentPrism>())
                {
                    novaDamageMod += 0.2;//20%
                    novaCritChanceMod -= 14;
                    novaCritDamageMod -= 0.1;
                    novaChargeMod += 5;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<EverflamePrism>() ||
                    affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<EverflamePrism>() ||
                    affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<EverflamePrism>())
                {
                    novaDamageMod += 0.1;
                    novaCritChanceMod += 7;
                    novaCritDamageMod += 0.1;
                    novaChargeMod -= 15;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<CrystallinePrism>() ||
                    affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<CrystallinePrism>() ||
                    affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<CrystallinePrism>())
                {
                    novaDamageMod -= 0.2;
                    novaCritChanceMod -= 7;
                    novaCritDamageMod += 0.3;
                    //novaChargeMod -= 15;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<VerdantPrism>() ||
                    affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<VerdantPrism>() ||
                    affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<VerdantPrism>())
                {
                    //novaDamageMod += 50;
                    novaCritChanceMod += 21;
                    //novaCritDamageMod += 225;
                    novaChargeMod -= 15;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<RadiantPrism>() ||
                     affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<RadiantPrism>() ||
                     affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<RadiantPrism>())
                {
                    novaDamageMod -= 0.1;
                    novaCritChanceMod -= 7;
                    novaCritDamageMod -= 0.1;
                    novaChargeMod += 15;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<ApocryphicPrism>() ||
                    affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<ApocryphicPrism>() ||
                    affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<ApocryphicPrism>())
                {
                    novaDamageMod += 0.2;
                    novaCritChanceMod -= 14;
                    novaCritDamageMod += 0.1;
                    novaChargeMod -= 5;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<AlchemicPrism>() ||
                     affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<AlchemicPrism>() ||
                     affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<AlchemicPrism>())
                {
                    novaDamageMod -= 0.1;
                    novaCritChanceMod += 14;
                    novaCritDamageMod += 0.1;
                    novaChargeMod -= 10;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<CastellicPrism>() ||
                     affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<CastellicPrism>() ||
                     affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<CastellicPrism>())
                {
                    novaDamageMod += 0.3;
                    novaCritChanceMod -= 7;
                    novaCritDamageMod -= 0.2;
                    //novaChargeMod -= 10;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<LucentPrism>() ||
                    affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<LucentPrism>() ||
                    affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<LucentPrism>())
                {
                    novaDamageMod -= 0.3;
                    //novaCritChanceMod -= 7;
                    novaCritDamageMod += 0.1;
                    novaChargeMod += 10;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<PhylacticPrism>() ||
                    affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<PhylacticPrism>() ||
                    affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<PhylacticPrism>())
                {
                    novaDamageMod -= 0.1;
                    novaCritChanceMod += 21;
                    //novaCritDamageMod += 75;
                    novaChargeMod -= 10;
                }

                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<LightswornPrism>() ||
                     affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<LightswornPrism>() ||
                     affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<LightswornPrism>())
                {
                    lightswornPrism = true;
                }
                else
                {
                    lightswornPrism = false;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<BurnishedPrism>() ||
                    affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<BurnishedPrism>() ||
                    affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<BurnishedPrism>())
                {
                    burnishedPrism = true;
                }
                else
                {
                    burnishedPrism = false;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<GeminiPrism>() ||
                     affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<GeminiPrism>() ||
                     affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<GeminiPrism>())
                {
                    geminiPrism = true;
                }
                else
                {
                    geminiPrism = false;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<SpatialPrism>() ||
                    affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<SpatialPrism>() ||
                    affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<SpatialPrism>())
                {
                    spatialPrism = true;
                }
                else
                {
                    spatialPrism = false;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<PaintedPrism>() ||
                    affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<PaintedPrism>() ||
                    affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<PaintedPrism>())
                {
                    paintedPrism = true;
                }
                else
                {
                    paintedPrism = false;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<VoidsentPrism>() ||
                    affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<VoidsentPrism>() ||
                    affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<VoidsentPrism>())
                {
                    voidsentPrism = true;
                }
                //1.1.6 prisms
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<RoyalSlimePrism>() ||
                    affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<RoyalSlimePrism>() ||
                    affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<RoyalSlimePrism>())
                {
                    royalSlimePrism = true;
                }
                else
                {
                    royalSlimePrism = false;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<MechanicalPrism>() ||
                    affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<MechanicalPrism>() ||
                    affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<MechanicalPrism>())
                {
                    mechanicalPrism = true;
                }
                else
                {
                    mechanicalPrism = false;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<OvergrownPrism>() ||
                    affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<OvergrownPrism>() ||
                    affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<OvergrownPrism>())
                {
                    overgrownPrism = true;
                }
                else
                {
                    overgrownPrism = false;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<LihzahrdPrism>() ||
                    affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<LihzahrdPrism>() ||
                    affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<LihzahrdPrism>())
                {
                    lihzahrdPrism = true;
                }
                else
                {
                    lihzahrdPrism = false;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<TyphoonPrism>() ||
                    affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<TyphoonPrism>() ||
                    affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<TyphoonPrism>())
                {
                    typhoonPrism = true;
                }
                else
                {
                    typhoonPrism = false;
                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<EmpressPrism>() ||
                    affixItem2.ModItem != null && !affixItem2.IsAir && affixItem2.type == ItemType<EmpressPrism>() ||
                    affixItem3.ModItem != null && !affixItem3.IsAir && affixItem3.type == ItemType<EmpressPrism>())
                {
                    empressPrism = true;
                }
                else
                {
                    empressPrism = false;
                }
                if (affixItem1 != null && !affixItem1.IsAir && affixItem1.type == ItemType<LuminitePrism>() ||
                    affixItem2 != null && !affixItem2.IsAir && affixItem2.type == ItemType<LuminitePrism>() ||
                    affixItem3 != null && !affixItem3.IsAir && affixItem3.type == ItemType<LuminitePrism>())
                {
                    luminitePrism = true;
                }
                else
                {
                    luminitePrism = false;
                }
                //Tier 3 Prisms
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<PrismOfTheRuinedKing>())
                {
                    ruinedKingPrism = true;

                }
                else
                {
                    ruinedKingPrism = false;

                }
                if (affixItem1.ModItem != null && !affixItem1.IsAir && affixItem1.type == ItemType<PrismOfTheCosmicPhoenix>())
                {
                    cosmicPhoenixPrism = true;

                }
                else
                {
                    cosmicPhoenixPrism = false;

                }
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

                if (starfarerArmorEquipped.type == ItemType<SeventhSigilAutumnAttire>())
                {
                    starfarerOutfit = 5;
                }
                if (starfarerArmorEquipped.type == ItemType<GarmentsOfWinterRainAttire>())
                {
                    starfarerOutfit = 6;
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
                if (starfarerVanityEquipped.type == ItemType<SeventhSigilAutumnAttire>())
                {
                    starfarerOutfitVanity = 5;
                }
                if (starfarerVanityEquipped.type == ItemType<GarmentsOfWinterRainAttire>())
                {
                    starfarerOutfitVanity = 6;
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

            SetupStellarNovas NovaSetup = new SetupStellarNovas();

            if (chosenStellarNova != 0)
            {
                novaDamage = NovaSetup.GetNovaDamage(chosenStellarNova, baseNovaDamageAdd);
                novaGaugeMax = NovaSetup.GetNovaCost(chosenStellarNova);
                novaCritChance = NovaSetup.GetNovaCritChance(chosenStellarNova);
                novaCritDamage = NovaSetup.GetNovaCritDamageMod(chosenStellarNova, baseNovaDamageAdd);

                abilityName = NovaSetup.GetInfo(chosenStellarNova, "AbilityName", baseNovaDamageAdd);
                abilitySubName = NovaSetup.GetInfo(chosenStellarNova, "AbilitySubName", baseNovaDamageAdd);
                if (chosenStellarNova == 7)//Guardian's Light
                {
                    if (MeleeAspect == 2)
                    {
                        abilityDescription = NovaSetup.GetInfo(chosenStellarNova, "AbilityDescription.Melee", baseNovaDamageAdd);
                    }
                    else if (RangedAspect == 2)
                    {
                        abilityDescription = NovaSetup.GetInfo(chosenStellarNova, "AbilityDescription.Ranged", baseNovaDamageAdd);
                    }
                    else if (MagicAspect == 2)
                    {
                        abilityDescription = NovaSetup.GetInfo(chosenStellarNova, "AbilityDescription.Magic", baseNovaDamageAdd);
                    }
                    else if (SummonAspect == 2)
                    {
                        abilityDescription = NovaSetup.GetInfo(chosenStellarNova, "AbilityDescription.Summon", baseNovaDamageAdd);
                    }
                    else
                    {
                        abilityDescription = NovaSetup.GetInfo(chosenStellarNova, "AbilityDescription.Other", baseNovaDamageAdd);
                    }
                }
                else
                {
                    abilityDescription = NovaSetup.GetInfo(chosenStellarNova, "AbilityDescription", baseNovaDamageAdd);
                }


                if (chosenStarfarer == 1)
                {
                    starfarerBonus = NovaSetup.GetInfo(chosenStellarNova, "AstralBonus", baseNovaDamageAdd);

                }
                if (chosenStarfarer == 2)
                {
                    starfarerBonus = NovaSetup.GetInfo(chosenStellarNova, "UmbralBonus", baseNovaDamageAdd);

                }

                if (chosenStellarNova != 4)
                {
                    baseStats = "" +
                    $"{novaDamage} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.BaseDamage") +
                    $"\n{novaGaugeMax} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.BaseEnergyCost") +
                    $"\n" +
                    $"\n{Math.Round(novaDamage * (1 + novaDamageMod), 5)} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.Damage") +
                    $"\n{novaCritChance + novaCritChanceMod}% " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.CritChance") +
                    $"\n{Math.Round(novaCritDamage * (1 + novaCritDamageMod), 5)} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.CritDamage") +
                    $"\n{novaGaugeMax - novaChargeMod} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.EnergyCost");
                }
                else
                {
                    baseStats = "" +
                    $"{novaDamage} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.BaseHealStrength") +
                    $"\n{novaGaugeMax} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.BaseEnergyCost") +
                    $"\n" +
                    $"\n{Math.Round(novaDamage * (1 + novaDamageMod), 5)} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.HealStrength") +
                    $"\n{novaCritChance + novaCritChanceMod}% " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.CritChance") +
                    $"\n{Math.Round(novaCritDamage * (1 + novaCritDamageMod), 5)} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.CritHealStrength") +
                    $"\n{novaGaugeMax - novaChargeMod} " + LangHelper.GetTextValue("StellarNova.StellarNovaInfo.EnergyCost");
                }



            }

            abilityDescription = LangHelper.Wrap(abilityDescription, 85);
            starfarerBonus = LangHelper.Wrap(starfarerBonus, 85);
        }
        private void AspectedDamageModification()
        {
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
                if (HealerAspect == 0)
                {
                    HealerAspect = 1;
                }
                if (BardAspect == 0)
                {
                    BardAspect = 1;
                }
                if (ThrowerAspect == 0)
                {
                    ThrowerAspect = 1;
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
                    RogueAspect = 1;

                    BardAspect = 1;
                    HealerAspect = 1;
                    ThrowerAspect = 1;

                }
                if (AspectLocked == 2)
                {
                    MeleeAspect = 1;
                    MagicAspect = 2;
                    RangedAspect = 1;
                    SummonAspect = 1;
                    RogueAspect = 1;

                    BardAspect = 1;
                    HealerAspect = 1;
                    ThrowerAspect = 1;
                }
                if (AspectLocked == 3)
                {
                    MeleeAspect = 1;
                    MagicAspect = 1;
                    RangedAspect = 2;
                    SummonAspect = 1;
                    RogueAspect = 1;

                    BardAspect = 1;
                    HealerAspect = 1;
                    ThrowerAspect = 1;
                }
                if (AspectLocked == 4)
                {
                    MeleeAspect = 1;
                    MagicAspect = 1;
                    RangedAspect = 1;
                    SummonAspect = 2;

                    RogueAspect = 1;

                    BardAspect = 1;
                    HealerAspect = 1;
                    ThrowerAspect = 1;


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

                        BardAspect = 1;
                        HealerAspect = 1;
                        ThrowerAspect = 1;

                    }
                    else
                    {
                        AspectLocked = 0;

                        MeleeAspect = 1;
                        MagicAspect = 1;
                        RangedAspect = 1;
                        SummonAspect = 1;
                        RogueAspect = 1;

                    }

                }
                if (AspectLocked == 6)
                {
                    if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
                    {

                        MeleeAspect = 1;
                        MagicAspect = 1;
                        RangedAspect = 1;
                        SummonAspect = 1;
                        RogueAspect = 1;
                        BardAspect = 2;
                        HealerAspect = 1;
                        ThrowerAspect = 1;
                    }
                    else
                    {
                        AspectLocked = 0;

                        MeleeAspect = 1;
                        MagicAspect = 1;
                        RangedAspect = 1;
                        SummonAspect = 1;
                        RogueAspect = 1;

                        BardAspect = 1;
                        HealerAspect = 1;
                        ThrowerAspect = 1;
                    }

                }
                if (AspectLocked == 7)
                {
                    if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
                    {

                        MeleeAspect = 1;
                        MagicAspect = 1;
                        RangedAspect = 1;
                        SummonAspect = 1;
                        RogueAspect = 1;
                        BardAspect = 1;
                        HealerAspect = 2;
                        ThrowerAspect = 1;
                    }
                    else
                    {
                        AspectLocked = 0;

                        MeleeAspect = 1;
                        MagicAspect = 1;
                        RangedAspect = 1;
                        SummonAspect = 1;
                        RogueAspect = 1;

                        BardAspect = 1;
                        HealerAspect = 1;
                        ThrowerAspect = 1;
                    }

                }
                if (AspectLocked == 8)
                {
                    if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
                    {

                        MeleeAspect = 1;
                        MagicAspect = 1;
                        RangedAspect = 1;
                        SummonAspect = 1;
                        RogueAspect = 1;
                        BardAspect = 1;
                        HealerAspect = 1;
                        ThrowerAspect = 2;
                    }
                    else
                    {
                        AspectLocked = 0;

                        MeleeAspect = 1;
                        MagicAspect = 1;
                        RangedAspect = 1;
                        SummonAspect = 1;
                        RogueAspect = 1;

                        BardAspect = 1;
                        HealerAspect = 1;
                        ThrowerAspect = 1;
                    }

                }

            }
        }
        private void MysticForging()
        {
            if (mysticforging == 2)
            {
                Player.GetAttackSpeed(DamageClass.Generic) += MathHelper.Lerp(0f, 0.15f, Player.GetCritChance(DamageClass.Generic) / 100);

            }
        }
        private void ButchersDozen()
        {
            if (inCombat < 0)
            {
                butchersDozenKills = 0;
            }
            if (butchersDozenKills >= 12 && !Player.dead && Player.active)
            {
                if (!Player.HasBuff(BuffType<ButchersDozen>()))
                {
                    /*
                    if (chosenStarfarer == 1)
                    {
                        if (Player.ownedProjectileCounts[Mod.Find<ModProjectile>("AsphodeneBurst").Type] < 1)
                            Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("AsphodeneBurst").Type, 0, 0, Player.whoAmI, 0, 1);

                    }
                    if (chosenStarfarer == 2)
                    {
                        if (Player.ownedProjectileCounts[Mod.Find<ModProjectile>("EridaniBurst").Type] < 1)
                            Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("EridaniBurst").Type, 0, 0, Player.whoAmI, 0, 1);

                    }*/
                    if (starfarerPromptCooldown <= 0)
                    {
                        starfarerPromptActive("onButchersDozen");
                    }

                }
                Player.AddBuff(BuffType<ButchersDozen>(), 240);
                butchersDozenKills = 0;
            }
        }
        private void BetweenTheBoundary()
        {
            betweenTheBoundaryTimer--;
            if (betweenTheBoundaryTimer <= 0 && beyondtheboundary == 2 && !stellarArray)
            {
                if (betweenTheBoundaryEbb)
                {
                    Player.AddBuff(BuffType<Ebb>(), 480);
                    betweenTheBoundaryEbb = false;
                }
                else
                {
                    Player.AddBuff(BuffType<Flow>(), 480);
                    betweenTheBoundaryEbb = true;
                }


                betweenTheBoundaryTimer = 480;

            }
        }
        private void HealthyConfidence()
        {
            if (healthyConfidence == 2 && Player.active && !Player.dead)
            {
                if (healthyConfidenceHealAmount > 0)
                {
                    healthyConfidenceHealTimer++;
                    if (healthyConfidenceHealTimer >= 60)
                    {
                        healthyConfidenceHealTimer = 0;
                        Player.statLife += (int)(healthyConfidenceHealAmount * 0.1);
                        if ((int)(healthyConfidenceHealAmount * 0.1) > 0)
                        {
                            Player.HealEffect((int)(healthyConfidenceHealAmount * 0.1));

                        }
                        healthyConfidenceHealAmount -= (int)(healthyConfidenceHealAmount * 0.1);

                    }
                }
            }
        }
        private void BeyondInfinity()
        {
            if (inCombat > 0 && beyondinfinity == 2)
            {
                //beyondInfinityTimer++;
            }
            if (inCombat <= 0)
            {
                //beyondInfinityTimer = 0;
                //beyondInfinityDamageMod = 0;
            }
            if (beyondInfinityTimer >= 120 && beyondinfinity == 2)
            {
                //beyondInfinityTimer = 0;
                //beyondInfinityDamageMod += 0.1f;
                //beyondInfinityDamageMod = MathHelper.Clamp(beyondInfinityDamageMod, 0, 1f);
            }
        }
        private void InnerAlchemy()
        {
            if (timeAfterGettingHit >= 720 && inneralchemy == 2)
            {
                Player.AddBuff(BuffType<InnerAlchemy>(), 10);
            }
        }
        private void DialogueEnemySpawnModifier()
        {
            if (VNDialogueActive || starfarerDialogue)
            {
                Player.AddBuff(BuffType<Conversationalist>(), 10);
            }
        }
        private void BossEnemySpawnModifier()
        {
            if (!BossEnemySpawnModDisabled)
            {
                for (int i = 0; i <= Main.maxNPCs; i++)
                {
                    if (Main.npc[i].boss && Main.npc[i].active && Main.npc[i].ModNPC?.Mod == ModLoader.GetMod("StarsAbove"))
                    {
                        Player.AddBuff(BuffType<BossEnemySpawnMod>(), 10);
                    }

                }

            }
        }
        private void DialogueScroll()
        {
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
                animatedDescription = LangHelper.Wrap(animatedDescription, 55);
            }
            if (starfarerMenuActive && chosenStarfarer != 0)
            {
                novaGauge = 0;
                animatedStarfarerMenuDialogue = starfarerMenuDialogue.Substring(0, starfarerMenuDialogueScrollNumber);//Prompt dialogue increment magic
                animatedStarfarerMenuDialogue = LangHelper.Wrap(animatedStarfarerMenuDialogue, 46);
            }
        }
        private void DrillMountBug()
        {
            if (Player.HeldItem.ModItem?.Mod == ModLoader.GetMod("StarsAbove"))//Drill Mount Bug
            {
                Player.buffImmune[BuffID.DrillMount] = true;
                Player.ClearBuff(BuffID.DrillMount);



            }
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
            if(kineticConversion == 2)
            {
                if (Player.velocity.Length() >= 15f && ! Player.HasBuff(BuffType<KineticConversionCooldown>()))
                {
                    Player.AddBuff(BuffType<Invincibility>(), 60);
                    Player.AddBuff(BuffType<KineticConversionCooldown>(), 60 * 12);

                }
            }
            LavenderRefrain();
        }
        public override void PostUpdate()
        {
            if(catharsis == 2)
            {
                if (Player.statLife < oldHP)
                {
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && !npc.boss && npc.Distance(Player.Center) < 100)
                        {
                            npc.SimpleStrikeNPC((int)(Player.GetWeaponDamage(Player.HeldItem) * 0.2), 0, false, 0, DamageClass.Generic, true, 0);

                        }
                    }
                }
            }
            oldHP = Player.statLife;
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

            BossStarfarerPrompts();

            OtherModBossStarfarerPrompts();

            //If the boss isn't listed...
            UnknownBossStarfarerPrompts();

            //Biomes
            BiomePrompts();
            ModdedBiomePrompts();








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

            if (Main.LocalPlayer.HasBuff(BuffType<AstarteDriver>()))
            {
                novaGauge = 0;
            }
            if (astarteDriverAttacks >= 5)
            {
                astarteDriverAttacks = 5;
            }
            astarteDriverCooldown--;
            ryukenTimer--;

            trueNovaGaugeMax = novaGaugeMax - novaChargeMod;
        }
        private void BiomePrompts()
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
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
        }
        private void ModdedBiomePrompts()
        {
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

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
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

        }
        private void OtherModBossStarfarerPrompts()
        {
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
            if (ModLoader.TryGetMod("SpiritMod", out Mod SpiritMod))
            {
                if (NPC.AnyNPCs(SpiritMod.Find<ModNPC>("Scarabeus").Type) && !seenScarabeus)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onScarabeus");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(SpiritMod.Find<ModNPC>("MoonWizard").Type) && !seenMoonJellyWizard)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onMoonJellyWizard");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(SpiritMod.Find<ModNPC>("ReachBoss").Type) && !seenVinewrathBane)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onVinewrathBane");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(SpiritMod.Find<ModNPC>("AncientFlyer").Type) && !seenAncientAvian)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onAncientAvian");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(SpiritMod.Find<ModNPC>("SteamRaiderHead").Type) && !seenStarplateVoyager)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onStarplateVoyager");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(SpiritMod.Find<ModNPC>("Infernon").Type) && !seenInfernon)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onInfernon");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(SpiritMod.Find<ModNPC>("Dusking").Type) && !seenDusking)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onDusking");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(SpiritMod.Find<ModNPC>("Atlas").Type) && !seenAtlas)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onAtlas");
                    seenUnknownBossTimer = 300;
                }
            }
            //Secrets of the Shadows Mod Bosses
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
            if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
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
            }

            //Wrath of the Gods
            if (ModLoader.TryGetMod("NoxusBoss", out Mod wrathOfTheGods))
            {
                if (NPC.AnyNPCs(wrathOfTheGods.Find<ModNPC>("NoxusEgg").Type) && !seenNoxusEgg)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onNoxusEgg");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(wrathOfTheGods.Find<ModNPC>("EntropicGod").Type) && !seenEntropicGod)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onEntropicGod");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(wrathOfTheGods.Find<ModNPC>("NamelessDeityBoss").Type) && !seenNamelessDeity)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onNamelessDeity");
                    seenUnknownBossTimer = 300;
                }
            }
            //Starlight River
            if (ModLoader.TryGetMod("StarlightRiver", out Mod starlightRiver))
            {
                if (NPC.AnyNPCs(starlightRiver.Find<ModNPC>("Glassweaver").Type) && !seenGlassweaver)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onGlassweaver");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(starlightRiver.Find<ModNPC>("VitricBoss").Type) && !seenCeiros)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onCeiros");
                    seenUnknownBossTimer = 300;
                }
                if (NPC.AnyNPCs(starlightRiver.Find<ModNPC>("SquidBoss").Type) && !seenAuroracle)
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onAuroracle");
                    seenUnknownBossTimer = 300;
                }
            }
        }
        private void UnknownBossStarfarerPrompts()
        {
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
        }
        private void BossStarfarerPrompts()
        {
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
            if (NPC.AnyNPCs(NPCType<WarriorOfLightBoss>()) && !seenWarriorOfLight)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onWarriorOfLight");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCType<NPCs.Vagrant.VagrantBoss>()) && !seenVagrant)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onVagrant");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCType<NalhaunBoss>()) && !seenNalhaun)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onNalhaun");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCType<PenthesileaBoss>()) && !seenPenth)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onPenth");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCType<ArbitrationBoss>()) && !seenArbiter)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onArbiter");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCType<ThespianBoss>()) && !seenThespian)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onThespian");
                seenUnknownBossTimer = 300;
            }
            if (NPC.AnyNPCs(NPCType<StarfarerBoss>()) && !seenStarfarers)
            {
                if (starfarerPromptCooldown > 0)
                {
                    starfarerPromptCooldown = 0;
                }
                starfarerPromptActive("onStarfarerBoss");
                seenUnknownBossTimer = 300;
            }
        }

        private void OnKillEnemy(NPC npc)
        {
            if (aquaaffinity == 2)//Cyclic Hunter
            {

                if (ammoRecycleCooldown <= 0)
                {
                    Player.AddBuff(BuffType<AmmoRecycle>(), 30);
                    ammoRecycleCooldown = 120;
                    Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                    CombatText.NewText(textPos, new Color(81, 62, 247, 240), $"{12 + (int)(npc.lifeMax * 0.05)}", false, false);
                    Player.statMana += 12 + (int)(npc.lifeMax * 0.05);
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

                if (!npc.SpawnedFromStatue)
                {
                    if (Player.HasBuff(BuffType<AstarteDriver>()))
                    {
                        Player.AddBuff(BuffType<AstarteDriver>(), 1500);
                    }
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
                    NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Player.whoAmI, newPosition.X, newPosition.Y, 1, 0, 0);

                }
            }
        }


        private void PenthTeleport(NPC npc)
        {
            

        }
        public static bool SameTeam(Player player1, Player player2)
        {

            if (player1.whoAmI == player2.whoAmI) return true;

            if (player1.team > 0 && player1.team != player2.team) return false;

            if (player1.hostile && player2.hostile && (player1.team == 0 || player2.team == 0)) return false;

            return true;
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Player.GetModPlayer<BossPlayer>().QTEActive && novaDrain <= 0)
            {
                if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
                {
                    if (NPC.AnyNPCs(calamityMod.Find<ModNPC>("DevourerofGodsBody").Type) && StarsAbove.novaKey.JustPressed)
                    {
                        if (Main.netMode != NetmodeID.Server && Main.myPlayer == Player.whoAmI) { Main.NewText(LangHelper.GetTextValue($"Common.BlockedNova"), 241, 255, 180); }
                        return;
                    }
                }

                if (chosenStellarNova == 6 && Player.ownedProjectileCounts[ProjectileType<UnlimitedBladeWorksBackground>()] >= 1)
                {
                    return;
                }

                EdinGenesisQuasar();
                if (squallReady && Player.ownedProjectileCounts[ProjectileType<SilenceSquall1>()] <= 0)
                {
                    squallReady = false;
                    //Silence and Squall
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_SilenceSquall2, Player.Center);
                    Vector2 Target = Vector2.Normalize(Player.DirectionTo(Player.GetModPlayer<StarsAbovePlayer>().playerMousePos)) * 30f;
                    Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y), Target, ProjectileType<SilenceSquall2>(), novaDamage, 0, Player.whoAmI);

                    for (int d = 0; d < 37; d++)//Visual effects
                    {
                        Vector2 perturbedSpeed = new Vector2(Target.X, Target.Y).RotatedByRandom(MathHelper.ToRadians(8));
                        float scale = 1f - Main.rand.NextFloat() * 1f;
                        perturbedSpeed = perturbedSpeed * scale;
                        int dustIndex = Dust.NewDust(Player.Center, 0, 0, DustID.GemSapphire, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 2f);
                        Main.dust[dustIndex].noGravity = true;

                    }
                }
                if (chosenStellarNova == 7 && StarsAbove.novaKey.JustPressed && !stellarArray && !starfarerDialogue && Player.HasBuff(BuffType<BearerOfLight>()) || Player.HasBuff(BuffType<BearerOfDarkness>()))
                {
                    if (goldenGunShots > 0)
                    {
                        goldenGunShots--;
                        FireGoldenGunBullet();
                    }

                }
                if (chosenStellarNova == 1 && StarsAbove.novaKey.JustPressed && !stellarArray && !starfarerDialogue && Main.LocalPlayer.HasBuff(BuffType<PrototokiaTricast>()))//prototokia Tricast
                {

                    if (Player.whoAmI == Main.myPlayer)
                    {
                        //
                        for (int i = 0; i < Player.CountBuffs(); i++)
                            if (Player.buffType[i] == BuffType<PrototokiaTricast>())
                            {
                                Player.DelBuff(i);


                            }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(Player.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default, 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(Player.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default, 1.5f);
                        }
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_prototokiaActive, Player.Center);

                        Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y), Vector2.Zero, ProjectileType<Prototokia3>(), novaDamage, 4, Player.whoAmI, 0, 1);                                                                                                                                                             //Vector2 mousePosition = Main.MouseWorld;
                                                                                                                                                                                                                                                                                                                                                                           //Projectile.NewProjectile(Player.GetSource_FromThis(),new Vector2(player.Center.X - 100, player.Center.Y), Vector2.Zero, mod.ProjectileType("prototokia2"), novaDamage + novaDamageMod, 4, player.whoAmI, 0, 1);                                                                                                                                                                                                                                                                                                                                             //Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
                                                                                                                                                                                                                                                                                                                                                                           //Projectile.NewProjectile(Player.GetSource_FromThis(),player.Center.X, player.Center.Y - 200, (Main.MouseWorld).ToRotation(), direction, ProjectileID.StarWrath, novaDamage + novaDamageMod, 0, player.whoAmI, 0f);

                    }
                }
                //dualCast
                if (chosenStellarNova == 1 && StarsAbove.novaKey.JustPressed && !stellarArray && !starfarerDialogue && Main.LocalPlayer.HasBuff(BuffType<PrototokiaDualcast>()))//prototokia Dualcast
                {
                    if (Player.whoAmI == Main.myPlayer)
                    {
                        //
                        for (int i = 0; i < Player.CountBuffs(); i++)
                            if (Player.buffType[i] == BuffType<PrototokiaDualcast>())
                            {
                                Player.DelBuff(i);


                            }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(Player.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default, 1.5f);
                        }
                        for (int d = 0; d < 105; d++)
                        {
                            Dust.NewDust(Player.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default, 1.5f);
                        }
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_prototokiaActive, Player.Center);

                        Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y), Vector2.Zero, ProjectileType<Prototokia2>(), novaDamage, 4, Player.whoAmI, 0, 1);                                                                                                                                                             //Vector2 mousePosition = Main.MouseWorld;
                                                                                                                                                                                                                                                                                                                                                                           //Projectile.NewProjectile(Player.GetSource_FromThis(),new Vector2(player.Center.X - 100, player.Center.Y), Vector2.Zero, mod.ProjectileType("prototokia2"), novaDamage + novaDamageMod, 4, player.whoAmI, 0, 1);                                                                                                                                                                                                                                                                                                                                             //Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
                        if (chosenStarfarer == 1)
                        {
                            Player.AddBuff(BuffType<PrototokiaTricast>(), 600);

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
                                Dust.NewDust(Player.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default, 1.5f);
                            }
                            for (int d = 0; d < 105; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default, 1.5f);
                            }
                            if (chosenStarfarer == 1)
                            {
                                Player.AddBuff(BuffType<KiwamiRyuken>(), 60);

                            }
                            else
                            {
                                Player.AddBuff(BuffType<KiwamiRyuken>(), 60);

                            }

                            //Projectile.NewProjectile(Player.GetSource_FromThis(),new Vector2(player.Center.X, player.Center.Y - 860), Vector2.Zero, mod.ProjectileType("Laevateinn"), novaDamage + novaDamageMod, 4, player.whoAmI, 0, 1);//The 1 here means that ai1 will be set to 1. this is good for the first cast.
                            //Vector2 mousePosition = Main.MouseWorld;
                            //Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
                            //Projectile.NewProjectile(Player.GetSource_FromThis(),player.Center.X, player.Center.Y - 200, (Main.MouseWorld).ToRotation(), direction, ProjectileID.StarWrath, novaDamage + novaDamageMod, 0, player.whoAmI, 0f);
                        }
                    }
                }
                else
                //This is the Stellar Nova code (barring unique ones like prototokia dualcast or kiwami ryuken
                if (novaGauge == trueNovaGaugeMax && StarsAbove.novaKey.JustPressed && !stellarArray && !starfarerDialogue && chosenStellarNova != 0)
                {
                    StellarNovaCutIn();
                    //Player.GetModPlayer<StarsAbovePlayer>().activateShockwaveEffect = true;


                    if (Player.whoAmI == Main.myPlayer)
                    {
                        //Activate the Stellar Novas here.
                        if (chosenStellarNova == 1)//Prototokia Aster
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_prototokiaActive, Player.Center);
                            Player.AddBuff(BuffType<PrototokiaDualcast>(), 600);
                            for (int d = 0; d < 105; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default, 1.5f);
                            }
                            for (int d = 0; d < 105; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default, 1.5f);
                            }

                            Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y), Vector2.Zero, ProjectileType<Prototokia>(), novaDamage, 4, Player.whoAmI, 0, 1);//The 1 here means that ai1 will be set to 1. this is good for the first cast.
                                                                                                                                                                                                                 //Vector2 mousePosition = Main.MouseWorld;
                                                                                                                                                                                                                 //Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
                                                                                                                                                                                                                 //Projectile.NewProjectile(Player.GetSource_FromThis(),player.Center.X, player.Center.Y - 200, (Main.MouseWorld).ToRotation(), direction, ProjectileID.StarWrath, novaDamage + novaDamageMod, 0, player.whoAmI, 0f);
                            onActivateStellarNova();
                        }
                        if (chosenStellarNova == 2)//Ars Laevateinn
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_prototokiaActive, Player.Center);
                            for (int d = 0; d < 105; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default, 1.5f);
                            }
                            for (int d = 0; d < 105; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default, 1.5f);
                            }

                            Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y - 860), Vector2.Zero, Mod.Find<ModProjectile>("Laevateinn").Type, novaDamage, 4, Player.whoAmI, 0, 1);//The 1 here means that ai1 will be set to 1. this is good for the first cast.
                            Player.AddBuff(BuffType<SurtrTwilight>(), 600);                                                                                                        //Vector2 mousePosition = Main.MouseWorld;
                                                                                                                                                                                   //Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
                            onActivateStellarNova();                                                                                                                                                           //Projectile.NewProjectile(Player.GetSource_FromThis(),player.Center.X, player.Center.Y - 200, (Main.MouseWorld).ToRotation(), direction, ProjectileID.StarWrath, novaDamage + novaDamageMod, 0, player.whoAmI, 0f);
                        }
                        if (chosenStellarNova == 4)//The Garden of Avalon
                        {
                            //Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/prototokiaActive"));
                            for (int d = 0; d < 105; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default, 1.5f);
                            }
                            for (int d = 0; d < 105; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default, 1.5f);
                            }

                            Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y - 860), Vector2.Zero, Mod.Find<ModProjectile>("GardenOfAvalon").Type, novaDamage, 4, Player.whoAmI, 0, 1);//The 1 here means that ai1 will be set to 1. this is good for the first cast.
                            onActivateStellarNova();
                            //player.AddBuff(BuffType<Buffs.GardenOfAvalon>(), (novaDamage / 100));                                                                                                                                                                             //Vector2 mousePosition = Main.MouseWorld;
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_GardenOfAvalonActivated, Player.Center);
                            //activateGardenBuff();


                            //Garden of Avalon buffs.
                            Player.AddBuff(BuffType<Buffs.StellarNovas.GardenOfAvalon>(), 8 * 60);
                            if (chosenStarfarer == 2)
                            {
                                Player.AddBuff(BuffType<DreamlikeCharisma>(), 8 * 60);

                            }


                            if (chosenStarfarer == 1)
                            {
                                Player.AddBuff(BuffType<Invincibility>(), 4 * 60);
                                Player.statLife += 100;

                            }
                            else
                            {
                                Player.AddBuff(BuffType<Invincibility>(), 2 * 60);
                            }
                            int uniqueCrit = Main.rand.Next(100);
                            if (uniqueCrit <= novaCritChance + novaCritChanceMod)
                            {
                                Rectangle textPos2 = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height); //Mana Heal
                                CombatText.NewText(textPos2, new Color(255, 132, 2, 240), $"Critical cast!", false, false);

                                Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 40, Player.width, Player.height); //Heal
                                CombatText.NewText(textPos, new Color(63, 221, 53, 240), $"{(int)(novaCritDamage * (1 + novaCritDamageMod))}", false, false);
                                Player.statLife += (int)(novaCritDamage * (1 + novaCritDamageMod));
                                Player.AddBuff(BuffType<SolemnAegis>(), 15 * 60);
                            }
                            else
                            {

                            }


                        }
                        if (chosenStellarNova == 5)//Edin Shugra Quasar
                        {


                            onActivateStellarNova();
                            astarteCutsceneProgress = 180;
                            Player.AddBuff(BuffType<AstarteDriverPrep>(), 180);
                            Player.AddBuff(BuffType<Invincibility>(), 400);


                        }
                        if (chosenStellarNova == 6)//Unlimited Blade Works
                        {


                            onActivateStellarNova();
                            SoundEngine.PlaySound(StarsAboveAudio.SFX_summoning, Player.Center);
                            Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y), Vector2.Zero, ProjectileType<UnlimitedBladeWorksBackground>(), novaDamage, 0, Player.whoAmI, 0, trueNovaGaugeMax / 10 * 60);

                        }
                        if (chosenStellarNova == 7)//Guardian's Light
                        {
                            onActivateStellarNova();

                            if (MeleeAspect == 2)
                            {
                                //Thundercrash
                                Player.AddBuff(BuffType<BearerOfLight>(), 60 * 10);
                                SoundEngine.PlaySound(StarsAboveAudio.SFX_ThundercrashStart, Player.Center);

                                //Give player velocity towards their cursor, give Invincibility, give a active hitbox that detonates on the first foe struck (pierce 1)
                                Vector2 Leap = Vector2.Normalize(Player.DirectionTo(Player.GetModPlayer<StarsAbovePlayer>().playerMousePos)) * 10f;
                                Player.velocity = Leap;
                                Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y), Vector2.Zero, ProjectileType<ThundercrashDamage>(), novaDamage, 0, Player.whoAmI);
                                Player.AddBuff(BuffType<ThundercrashActive>(), 60);
                                Player.AddBuff(BuffType<Invincibility>(), 180);
                                return;
                            }
                            else if (RangedAspect == 2)
                            {
                                Player.AddBuff(BuffType<BearerOfLight>(), 60 * 10);

                                //Golden Gun
                                //Fire 1 shot and grant 2 more shots later

                                FireGoldenGunBullet();
                                goldenGunShots = 2;
                                return;
                            }
                            else if (MagicAspect == 2)
                            {
                                Player.AddBuff(BuffType<BearerOfLight>(), 60 * 10);

                                //Nova Bomb


                                SoundEngine.PlaySound(StarsAboveAudio.SFX_NovaBomb, Player.Center);
                                Vector2 Target = Vector2.Normalize(Player.DirectionTo(Player.GetModPlayer<StarsAbovePlayer>().playerMousePos)) * 10f;
                                Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y), Target, ProjectileType<NovaBomb>(), novaDamage, 0, Player.whoAmI);

                                for (int d = 0; d < 57; d++)//Visual effects
                                {
                                    Vector2 perturbedSpeed = new Vector2(Target.X, Target.Y).RotatedByRandom(MathHelper.ToRadians(20));
                                    float scale = 2f - Main.rand.NextFloat() * 1f;
                                    perturbedSpeed = perturbedSpeed * scale;
                                    int dustIndex = Dust.NewDust(Player.Center, 0, 0, DustID.GemAmethyst, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 2f);
                                    Main.dust[dustIndex].noGravity = true;

                                }
                                return;
                            }
                            else if (SummonAspect == 2)
                            {
                                Player.AddBuff(BuffType<BearerOfDarkness>(), 60 * 10);
                                SoundEngine.PlaySound(StarsAboveAudio.SFX_Needlestorm, Player.Center);

                                //Needlestorm
                                Vector2 Target = Vector2.Normalize(Player.DirectionTo(Player.GetModPlayer<StarsAbovePlayer>().playerMousePos)) * 30f;

                                float numberProjectiles = 5;
                                float adjustedRotation = MathHelper.ToRadians(72);

                                for (int i = 0; i < numberProjectiles; i++)
                                {
                                    Vector2 perturbedSpeed = Target.RotatedBy(MathHelper.Lerp(-adjustedRotation, adjustedRotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<WovenNeedle>(), novaDamage, 0, Main.myPlayer);
                                }
                                for (int d = 0; d < 37; d++)//Visual effects
                                {
                                    Vector2 perturbedSpeed = new Vector2(Target.X, Target.Y).RotatedByRandom(MathHelper.ToRadians(68));
                                    float scale = 1f - Main.rand.NextFloat() * 1f;
                                    perturbedSpeed = perturbedSpeed * scale;
                                    int dustIndex = Dust.NewDust(Player.Center, 0, 0, DustID.GemEmerald, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 1f);
                                    Main.dust[dustIndex].noGravity = true;

                                }
                                return;
                            }
                            else
                            {

                                //Silence and Squall
                                SoundEngine.PlaySound(StarsAboveAudio.SFX_SilenceSquall2, Player.Center);
                                Vector2 Target = Vector2.Normalize(Player.DirectionTo(Player.GetModPlayer<StarsAbovePlayer>().playerMousePos)) * 30f;

                                Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y), Target, ProjectileType<SilenceSquall1>(), novaDamage, 0, Player.whoAmI);

                                for (int d = 0; d < 37; d++)//Visual effects
                                {
                                    Vector2 perturbedSpeed = new Vector2(Target.X, Target.Y).RotatedByRandom(MathHelper.ToRadians(8));
                                    float scale = 1f - Main.rand.NextFloat() * 1f;
                                    perturbedSpeed = perturbedSpeed * scale;
                                    int dustIndex = Dust.NewDust(Player.Center, 0, 0, DustID.GemSapphire, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 2f);
                                    Main.dust[dustIndex].noGravity = true;

                                }
                                squallReady = true;
                                Player.AddBuff(BuffType<BearerOfDarkness>(), 60 * 10);

                                return;
                            }



                            // Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y), Vector2.Zero, ProjectileType<UnlimitedBladeWorksBackground>(), novaDamage, 0, Player.whoAmI, 0, (trueNovaGaugeMax / 10) * 60);

                        }
                    }

                }
            }
        }

        private void FireGoldenGunBullet()
        {
            Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y), Vector2.Zero, ProjectileType<GoldenGunHeld>(), 0, 0, Player.whoAmI);


            SoundEngine.PlaySound(StarsAboveAudio.SFX_FireGoldenGun, Player.Center);
            Vector2 Target = Vector2.Normalize(Player.DirectionTo(Player.GetModPlayer<StarsAbovePlayer>().playerMousePos)) * 40f;
            Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y), Target, ProjectileType<GoldenGunBullet>(), novaDamage, 0, Player.whoAmI);

            for (int d = 0; d < 37; d++)//Visual effects
            {
                Vector2 perturbedSpeed = new Vector2(Target.X, Target.Y).RotatedByRandom(MathHelper.ToRadians(8));
                float scale = 1f - Main.rand.NextFloat() * 1f;
                perturbedSpeed = perturbedSpeed * scale;
                int dustIndex = Dust.NewDust(Player.Center, 0, 0, DustID.GemTopaz, perturbedSpeed.X, perturbedSpeed.Y, 150, default, 2f);
                Main.dust[dustIndex].noGravity = true;

            }
        }

        private void StellarNovaCutIn()
        {
            NovaCutInTimer = 140;
            NovaCutInVelocity = 20;
            NovaCutInX = 0;
            NovaCutInOpacity = 0;


            StellarNovaVoice();
        }

        private void StellarNovaVoice()
        {
            //TEMP until voice acting
            if (Main.rand.NextBool(5) && chosenStellarNova != 7)//1 in 5 chance to play a Nova specific line. (No unique quotes for Guardian's Light)
            {
                novaDialogue = LangHelper.Wrap(LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.StellarNovaQuotes." + $"{chosenStarfarer}" + ".Special" + $"{chosenStellarNova}"), 20);


            }
            else
            {
                if (Main.rand.NextBool(100))
                {
                    string novaQuote = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.StellarNovaQuotes." + $"{chosenStarfarer}" + $".10");
                    novaDialogue = LangHelper.Wrap(novaQuote, 20);


                    return;
                }
                else
                {
                    randomNovaDialogue = Main.rand.Next(0, 9);
                    string novaQuote = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.StellarNovaQuotes." + $"{chosenStarfarer}" + $".{randomNovaDialogue + 1}");
                    novaDialogue = LangHelper.Wrap(novaQuote, 20);


                }
            }
            return;//TEMP

            //If the ModConfig's voices are enabled, continue.
            if (!voicesDisabled)
            {
                if (Main.rand.NextBool(5) && chosenStellarNova != 7)//1 in 5 chance to play a Nova specific line. (No unique quotes for Guardian's Light)
                {
                    novaDialogue = LangHelper.Wrap(LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.StellarNovaQuotes." + $"{chosenStarfarer}" + ".Special" + $"{chosenStellarNova}"), 20);

                    switch (chosenStellarNova)
                    {
                        case 1:
                            if (chosenStarfarer == 1)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.ANSpecial1, Player.Center);
                            }
                            else if (chosenStarfarer == 2)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.ENSpecial1, Player.Center);

                            }
                            break;
                        case 2:
                            if (chosenStarfarer == 1)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.ANSpecial2, Player.Center);
                            }
                            else if (chosenStarfarer == 2)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.ENSpecial2, Player.Center);

                            }
                            break;
                        case 3:
                            if (chosenStarfarer == 1)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.ANSpecial3, Player.Center);
                            }
                            else if (chosenStarfarer == 2)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.ENSpecial3, Player.Center);

                            }
                            break;
                        case 4:
                            if (chosenStarfarer == 1)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.ANSpecial4, Player.Center);
                            }
                            else if (chosenStarfarer == 2)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.ENSpecial4, Player.Center);

                            }
                            break;
                        case 5:
                            if (chosenStarfarer == 1)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.ANSpecial5, Player.Center);
                            }
                            else if (chosenStarfarer == 2)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.ENSpecial5, Player.Center);

                            }
                            break;
                        case 6:
                            if (chosenStarfarer == 1)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.ANSpecial6, Player.Center);
                            }
                            else if (chosenStarfarer == 2)
                            {
                                SoundEngine.PlaySound(StarsAboveAudio.ENSpecial6, Player.Center);

                            }
                            break;
                    }
                }
                else
                {
                    if (Main.rand.NextBool(100))
                    {
                        string novaQuote = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.StellarNovaQuotes." + $"{chosenStarfarer}" + $".10");
                        novaDialogue = LangHelper.Wrap(novaQuote, 20);

                        //1 in 20 chance for a rare line to play.
                        if (chosenStarfarer == 1)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.AN10, Player.Center);

                        }
                        else if (chosenStarfarer == 2)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.EN10, Player.Center);

                        }
                        return;
                    }
                    else
                    {
                        randomNovaDialogue = Main.rand.Next(0, 9);
                        string novaQuote = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.StellarNovaQuotes." + $"{chosenStarfarer}" + $".{randomNovaDialogue + 1}");
                        novaDialogue = LangHelper.Wrap(novaQuote, 20);



                    }
                    if (randomNovaDialogue == 0)
                    {
                        if (chosenStarfarer == 1)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.AN1, Player.Center);

                        }
                        else if (chosenStarfarer == 2)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.EN1, Player.Center);

                        }
                    }
                    else if (randomNovaDialogue == 1)
                    {
                        if (chosenStarfarer == 1)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.AN2, Player.Center);

                        }
                        else if (chosenStarfarer == 2)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.EN2, Player.Center);

                        }
                    }
                    else if (randomNovaDialogue == 2)
                    {
                        if (chosenStarfarer == 1)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.AN3, Player.Center);

                        }
                        else if (chosenStarfarer == 2)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.EN3, Player.Center);

                        }
                    }
                    else if (randomNovaDialogue == 3)
                    {
                        if (chosenStarfarer == 1)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.AN4, Player.Center);

                        }
                        else if (chosenStarfarer == 2)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.EN4, Player.Center);

                        }
                    }
                    else if (randomNovaDialogue == 4)
                    {
                        if (chosenStarfarer == 1)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.AN5, Player.Center);

                        }
                        else if (chosenStarfarer == 2)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.EN5, Player.Center);

                        }
                    }
                    else if (randomNovaDialogue == 5)
                    {
                        if (chosenStarfarer == 1)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.AN6, Player.Center);

                        }
                        else if (chosenStarfarer == 2)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.EN6, Player.Center);

                        }
                    }
                    else if (randomNovaDialogue == 6)
                    {
                        if (chosenStarfarer == 1)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.AN7, Player.Center);

                        }
                        else if (chosenStarfarer == 2)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.EN7, Player.Center);

                        }
                    }
                    else if (randomNovaDialogue == 7)
                    {
                        if (chosenStarfarer == 1)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.AN8, Player.Center);

                        }
                        else if (chosenStarfarer == 2)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.EN8, Player.Center);

                        }
                    }
                    else if (randomNovaDialogue == 8)
                    {
                        if (chosenStarfarer == 1)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.AN9, Player.Center);

                        }
                        else if (chosenStarfarer == 2)
                        {
                            SoundEngine.PlaySound(StarsAboveAudio.EN9, Player.Center);

                        }
                    }
                    else if (randomNovaDialogue == 9)
                    {

                    }

                }
            }
            else
            {
                if (Main.rand.NextBool(5) && chosenStellarNova != 7)//1 in 5 chance to play a Nova specific line. (No unique quotes for Guardian's Light)
                {
                    novaDialogue = LangHelper.Wrap(LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.StellarNovaQuotes." + $"{chosenStarfarer}" + ".Special" + $"{chosenStellarNova}"), 20);

                    
                }
                else
                {
                    if (Main.rand.NextBool(100))
                    {
                        string novaQuote = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.StellarNovaQuotes." + $"{chosenStarfarer}" + $".10");
                        novaDialogue = LangHelper.Wrap(novaQuote, 20);

                        
                        return;
                    }
                    else
                    {
                        randomNovaDialogue = Main.rand.Next(0, 9);
                        string novaQuote = LangHelper.GetTextValue($"StellarNova.StellarNovaDialogue.StellarNovaQuotes." + $"{chosenStarfarer}" + $".{randomNovaDialogue + 1}");
                        novaDialogue = LangHelper.Wrap(novaQuote, 20);


                    }
                }
            }
        }

        private void EdinGenesisQuasar()
        {
            //Edin Genesis Quasar Casts
            if (chosenStellarNova == 5 && StarsAbove.novaKey.JustPressed && !stellarArray && !starfarerDialogue && astarteDriverAttacks > 0 && Main.LocalPlayer.HasBuff(BuffType<AstarteDriver>()) && astarteDriverCooldown < 0)
            {

                if (Player.whoAmI == Main.myPlayer)
                {
                    //
                    astarteDriverCooldown = 120;
                    astarteDriverAttacks--;

                    for (int d = 0; d < 105; d++)
                    {
                        Dust.NewDust(Player.Center, 0, 0, 269, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default, 1.5f);
                    }
                    for (int d = 0; d < 105; d++)
                    {
                        Dust.NewDust(Player.Center, 0, 0, 90, 0f + Main.rand.Next(-65, 65), 0f + Main.rand.Next(-65, 65), 150, default, 1.5f);
                    }
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_LimitBreakActive, Player.Center);
                    Vector2 mousePosition = Player.DirectionTo(Player.GetModPlayer<StarsAbovePlayer>().playerMousePos) * Main.rand.Next(20, 22);
                    for (int i = 0; i < 10; i++)
                    {
                        int type = Main.rand.Next(new int[] { ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, mousePosition.X, mousePosition.Y, type, novaDamage / 10, 3, Player.whoAmI, 0f);

                    }


                    int numberProjectiles = 10;
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = mousePosition.RotatedByRandom(MathHelper.ToRadians(40));
                        int type = Main.rand.Next(new int[] { ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });

                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, novaDamage / 10, 3, Player.whoAmI);
                    }
                    Vector2 shotKnockback = Vector2.Normalize(mousePosition) * 15f * -1f;
                    Player.velocity = shotKnockback;
                    if (chosenStarfarer == 2)
                    {
                        Player.AddBuff(BuffType<Invincibility>(), 60);
                    }
                    //Vector2 mousePosition = Main.MouseWorld;
                    //Projectile.NewProjectile(Player.GetSource_FromThis(),new Vector2(player.Center.X - 100, player.Center.Y), Vector2.Zero, mod.ProjectileType("prototokia2"), novaDamage + novaDamageMod, 4, player.whoAmI, 0, 1);                                                                                                                                                                                                                                                                                                                                             //Vector2 direction = Vector2.Normalize(mousePosition - player.Center);
                    //Projectile.NewProjectile(Player.GetSource_FromThis(),player.Center.X, player.Center.Y - 200, (Main.MouseWorld).ToRotation(), direction, ProjectileID.StarWrath, novaDamage + novaDamageMod, 0, player.whoAmI, 0f);

                }
            }
        }

        public override void PreUpdateBuffs()
        {
            if (seenIntroCutscene && chosenStarfarer == 0)
            {
                StarfarerSelectionVisibility += 0.03f;
                VNDialogueActive = false;
            }
            else
            {
                StarfarerSelectionVisibility -= 0.1f;

            }
            StarfarerSelectionVisibility = MathHelper.Clamp(StarfarerSelectionVisibility, 0, 2f);


            if (SubworldSystem.Current != null)
            {

            }
            else
            {
                if (!NPC.AnyNPCs(NPCType<TsukiyomiBoss>()))
                {
                    if (Player.HasBuff(BuffType<MoonTurmoil>()))
                    {
                        Player.ClearBuff(BuffType<MoonTurmoil>());
                    }
                    if (Player.HasBuff(BuffType<ChaosTurmoil>()))
                    {
                        Player.ClearBuff(BuffType<ChaosTurmoil>());
                    }
                }

            }

            if (stellarArray == false)
            {

                if (ironskin == 2)
                {
                    Player.statDefense += 6;

                }
                if (healthyConfidence == 2)
                {

                }
                if (bloomingflames == 2)
                {
                    if (Player.statLife < 100 || Player.HasBuff(BuffType<InfernalEnd>()))
                    {
                        Player.GetDamage(DamageClass.Generic) += 0.5f;

                    }
                }
                if (astralmantle == 2)
                {
                    Player.statDefense += Math.Min(Player.statMana / 10, (int)(Player.statLifeMax2 * 0.05));


                }

                if (avataroflight == 2)
                {
                    Player.statLifeMax2 += Player.statManaMax2 / 2;
                    if (Player.statLife >= 500)
                    {
                        Player.statDefense += 10;
                        Player.GetDamage(DamageClass.Generic) += 0.05f;
                    }
                }
                if (hikari == 2)
                {
                    Player.GetDamage(DamageClass.Generic) += 0.01f * (Player.statLifeMax2 / 20);
                    Player.statDefense += Player.statLifeMax2 / 20;
                    //player.moveSpeed *= 1 + (0.02f * (player.statLifeMax2 / 20));
                }
                if (celestialevanesence == 2)
                {
                    Player.GetCritChance(DamageClass.Generic) += Player.statMana / 20;

                }
                if (afterburner == 2)
                {
                    if (Player.statMana <= 40 && !Main.LocalPlayer.HasBuff(BuffType<AfterburnerCooldown>()) && !Main.LocalPlayer.HasBuff(BuffType<Afterburner>()))
                    {
                        Player.statMana += 150;
                        Player.AddBuff(BuffType<Afterburner>(), 240);

                    }
                }

            }

            for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.active && npc.type == NPCType<PenthesileaBoss>())
                {

                    PenthTeleport(npc);
                    break;
                }
            }

            if (stellarSickness == true)
            {
                Player.AddBuff(BuffType<StellarSickness>(), 3600);

                stellarSickness = false;
            }
            screenShakeTimerGlobal--;



            if (Player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 1)
            {
                Player.AddBuff(BuffType<AsphodeneBlessing>(), 2);

            }
            if (Player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)
            {
                Player.AddBuff(BuffType<EridaniBlessing>(), 2);

            }


            if (Main.LocalPlayer.HasBuff(BuffType<ButterflyTrance>()))
            {

                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.position;
                int playerWidth = Main.LocalPlayer.width;
                int playerHeight = Main.LocalPlayer.height;

                Dust.NewDust(position, playerWidth, playerHeight, 164, 0f, 0f, 150, default, 1.5f);




            }

            if (inWarriorOfLightFightTimer > 0)
            {
                Player.AddBuff(BuffType<Determination>(), 2);
            }

            if (Main.LocalPlayer.HasBuff(BuffType<LivingDead>()))
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.position;
                int playerWidth = Main.LocalPlayer.width;
                int playerHeight = Main.LocalPlayer.height;
                for (int d = 0; d < 5; d++)
                {
                    dust = Main.dust[Dust.NewDust(position, playerWidth, playerHeight, 258, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                }


            }

            if (Main.LocalPlayer.HasBuff(BuffType<KiwamiRyuken>()))
            {

                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.position;
                int playerWidth = Main.LocalPlayer.width;
                int playerHeight = Main.LocalPlayer.height;
                for (int d = 0; d < 5; d++)
                {
                    Dust.NewDust(position, playerWidth, playerHeight, 206, 0f, 0f, 150, default, 1.5f);
                }



            }
            if (Main.LocalPlayer.HasBuff(BuffType<KiwamiRyukenConfirm>()))
            {


                for (int i = 0; i < 90; i++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * ryukenTimer * 12);
                    offset.Y += (float)(Math.Cos(angle) * ryukenTimer * 12);

                    Dust d = Dust.NewDustPerfect(Player.Center + offset, 90, Player.velocity, 20, default, 0.4f);

                    d.fadeIn = 1f;
                    d.noGravity = true;
                }


            }


            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Afterburner>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<AfterburnerCooldown>(), 20 * 60);


                    }
                }

            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<TwincastActive>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        novaGauge += trueNovaGaugeMax / 2;

                    }
                }
            if (Main.LocalPlayer.HasBuff(BuffType<Bladeforged>()))
            {
                for (int i2 = 0; i2 < 5; i2++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 30f);
                    offset.Y += (float)(Math.Cos(angle) * 30f);

                    Dust d2 = Dust.NewDustPerfect(Player.Center + offset, DustID.Flare, Player.velocity, 200, default, 0.7f);
                    d2.fadeIn = 0.0001f;
                    d2.noGravity = true;
                }

                if (Player.velocity.Y == 0)
                {
                    for (int i3 = 0; i3 < 50; i3++)
                    {

                        Dust d = Main.dust[Dust.NewDust(new Vector2(Player.Center.X - Player.width, Player.Center.Y + Player.height / 2), Player.width * 2 - 3, 0, DustID.Flare, 0, Main.rand.Next(-5, -2), 150, default, 0.3f)];
                        d.fadeIn = 0.3f;
                        d.noLight = true;
                        d.noGravity = true;
                    }
                }

            }
            if (Main.LocalPlayer.HasBuff(BuffType<LeftDebuff>()))
            {
                for (int i = 0; i < 2; i++)
                {//



                    Vector2 vector32 = new Vector2(Player.Center.X - 500, Player.Center.Y);
                    for (int i3 = 0; i3 < 50; i3++)
                    {
                        Vector2 position = Vector2.Lerp(Player.Center, vector32, (float)i3 / 50);
                        Dust d = Dust.NewDustPerfect(position, 20, null, 240, default, 0.3f);
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

                        Dust d2 = Dust.NewDustPerfect(vector32 + offset, 20, Player.velocity, 200, default, 0.7f);
                        d2.fadeIn = 0.0001f;
                        d2.noGravity = true;
                    }
                }
            }
            if (Main.LocalPlayer.HasBuff(BuffType<RightDebuff>()))
            {
                for (int i = 0; i < 2; i++)
                {//



                    Vector2 vector32 = new Vector2(Player.Center.X + 500, Player.Center.Y);
                    for (int i3 = 0; i3 < 50; i3++)
                    {
                        Vector2 position = Vector2.Lerp(Player.Center, vector32, (float)i3 / 50);
                        Dust d = Dust.NewDustPerfect(position, 20, null, 240, default, 0.3f);
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

                        Dust d2 = Dust.NewDustPerfect(vector32 + offset, 20, Player.velocity, 200, default, 0.7f);
                        d2.fadeIn = 0.0001f;
                        d2.noGravity = true;
                    }
                }
            }
            if (Main.LocalPlayer.HasBuff(BuffType<RedPaint>()))
            {
                for (int i = 0; i < 5; i++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 30f);
                    offset.Y += (float)(Math.Cos(angle) * 30f);

                    Dust d = Dust.NewDustPerfect(Player.Center + offset, 219, Player.velocity, 200, default, 0.7f);
                    d.fadeIn = 0.0001f;
                    d.noGravity = true;
                }
            }
            if (Main.LocalPlayer.HasBuff(BuffType<BluePaint>()))
            {
                for (int i = 0; i < 5; i++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 30f);
                    offset.Y += (float)(Math.Cos(angle) * 30f);

                    Dust d = Dust.NewDustPerfect(Player.Center + offset, 221, Player.velocity, 200, default, 0.7f);
                    d.fadeIn = 0.0001f;
                    d.noGravity = true;
                }
            }
            if (Main.LocalPlayer.HasBuff(BuffType<YellowPaint>()))
            {
                for (int i = 0; i < 5; i++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 30f);
                    offset.Y += (float)(Math.Cos(angle) * 30f);

                    Dust d = Dust.NewDustPerfect(Player.Center + offset, 222, Player.velocity, 200, default, 0.7f);
                    d.fadeIn = 0.0001f;
                    d.noGravity = true;
                }
            }
            if (Main.LocalPlayer.HasBuff(BuffType<Pyretic>()))
            {
                for (int i = 0; i < 30; i++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 70f);
                    offset.Y += (float)(Math.Cos(angle) * 70f);

                    Dust d = Dust.NewDustPerfect(Player.Center + offset, 90, Player.velocity, 200, default, 0.7f);
                    d.fadeIn = 1f;
                    d.noGravity = true;
                }
            }
            if (Main.LocalPlayer.HasBuff(BuffType<DeepFreeze>()))
            {
                for (int i = 0; i < 30; i++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 70f);
                    offset.Y += (float)(Math.Cos(angle) * 70f);

                    Dust d = Dust.NewDustPerfect(Player.Center + offset, 135, Player.velocity, 200, default, 0.7f);
                    d.fadeIn = 1f;
                    d.noGravity = true;
                }
            }
            if (Main.LocalPlayer.HasBuff(BuffType<Invincibility>()))//Invincibility VFX
            {
                for (int i = 0; i < 12; i++)
                {

                }

            }

            if (Player.HasBuff(BuffType<AstarteDriverPrep>()))//Astarte Effects
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
            if (Player.HasBuff(BuffType<AstarteDriver>()))//Astarte Effects
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
            if (Player.HasBuff(BuffType<ThundercrashActive>()))//
            {
                Player.AddBuff(BuffType<Invisibility>(), 2);
                Vector2 Leap = Vector2.Normalize(Player.DirectionTo(Player.GetModPlayer<StarsAbovePlayer>().playerMousePos)) * 20f;
                Player.velocity = Leap;
                if (playerMousePos.X < Player.Center.X)
                {
                    Player.direction = -1;
                }
                for (int d = 0; d < 10; d++)
                {
                    Dust du = Main.dust[Dust.NewDust(Player.Center, 5, 5, DustID.Electric, 0f, 0f, 150, default, 2f)];
                    du.noGravity = true;
                }
                for (int d = 0; d < 2; d++)
                {
                    Dust du = Main.dust[Dust.NewDust(Player.Center, 0, 0, DustID.FireworkFountain_Blue, 0f, 0f, 150, default, 0.8f)];
                    du.noGravity = true;

                }
            }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<LeftDebuff>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        //player.velocity = Vector2.Zero;
                        Vector2 vector32 = new Vector2(Player.position.X - 500, Player.position.Y);
                        Player.Teleport(vector32, 1, 0);
                        NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Player.whoAmI, vector32.X, vector32.Y, 1, 0, 0);

                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<RightDebuff>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        //player.velocity = Vector2.Zero;
                        Vector2 vector32 = new Vector2(Player.position.X + 500, Player.position.Y);
                        Player.Teleport(vector32, 1, 0);
                        NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Player.whoAmI, vector32.X, vector32.Y, 1, 0, 0);
                    }
                }


            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<LivingDead>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<LivingDeadCooldown>(), 14400);//7200 is 2 minutes, 14400 is 4 minutes.

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
                if (Player.buffType[i] == BuffType<StarshieldBuff>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<StarshieldCooldown>(), 1200);//
                    }
                }

            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Pyretic>())
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
                if (Player.buffType[i] == BuffType<AstarteDriverPrep>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        //Change

                        astarteDriverAttacks = 3;
                        Player.AddBuff(BuffType<AstarteDriver>(), 1500);
                        SoundEngine.PlaySound(StarsAboveAudio.SFX_summoning, Player.Center);

                    }

                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<AstarteDriver>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        //Change
                        WhiteFade = 20;


                    }
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
                if (Player.buffType[i] == BuffType<DeepFreeze>())
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
                if (Player.buffType[i] == BuffType<KiwamiRyuken>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        if (chosenStarfarer == 2)
                        {
                            novaGauge = trueNovaGaugeMax / 2 + 10;
                        }
                        else
                        {
                            novaGauge += trueNovaGaugeMax / 2;
                        }

                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<KiwamiRyukenConfirm>())
                {
                    if (Player.buffTime[i] == 1)
                    {

                        Player.DelBuff(i);
                        float launchSpeed = -13f;
                        Vector2 mousePosition = Main.MouseWorld;
                        Vector2 direction = Vector2.Normalize(mousePosition - Player.Center);
                        Player.velocity = direction * launchSpeed;

                        Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y), direction, Mod.Find<ModProjectile>("kiwamiryukenconfirm").Type, novaDamage, 40, Player.whoAmI, 0, 1);                                                                                                                                                             //Vector2 mousePosition = Main.MouseWorld;

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
            if(stayTheCourse == 2)
            {
                Player.GetCritChance(DamageClass.Generic) += (100 * stayTheCourseStacks);

            }
            if(fabledFashion == 2)
            {
                if(Player.statLifeMax > 151)
                {
                    Player.statLifeMax2 -= 150;

                }
                Player.GetDamage(DamageClass.Generic) -= 0.5f;
                Player.statDefense *= 0.5f;
            }
        }
        public override bool CanUseItem(Item item)
        {
            if(VNDialogueActive)
            {
                return false;
            }
            return base.CanUseItem(item);
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            if (proj.type == Mod.Find<ModProjectile>("RedSplatterDamage").Type)
            {
                if (Main.LocalPlayer.HasBuff(BuffType<RedPaint>()))
                {

                    return false;
                }
            }
            if (proj.type == Mod.Find<ModProjectile>("YellowSplatterDamage").Type)
            {
                if (Main.LocalPlayer.HasBuff(BuffType<YellowPaint>()))
                {

                    return false;
                }
            }
            if (proj.type == Mod.Find<ModProjectile>("BlueSplatterDamage").Type)
            {
                if (Main.LocalPlayer.HasBuff(BuffType<BluePaint>()))
                {

                    return false;
                }
            }
            return true;
        }
        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {


        }
        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            if (proj.type == Mod.Find<ModProjectile>("BlazingSkies").Type
                || proj.type == Mod.Find<ModProjectile>("SaberDamage").Type
                || proj.type == Mod.Find<ModProjectile>("SolemnConfiteorDamage").Type
                || proj.type == Mod.Find<ModProjectile>("TheBitterEnd").Type
                               )
            {
                if (Main.expertMode == true)
                {
                    Main.LocalPlayer.AddBuff(BuffType<Vulnerable>(), 1800);
                }
            }

        }
        public override bool ImmuneTo(PlayerDeathReason damageSource, int cooldownCounter, bool dodgeable)
        {

            if (Player.HasBuff(BuffType<Invincibility>()))
            {
                return true;
            }
            return false;
        }
        public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        {
            if(arborealEchoes == 2)
            {
                healValue = (int)(healValue * 1.3);
            }
            
            base.GetHealLife(item, quickHeal, ref healValue);
        }
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (inevitableEnd == 2)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && !npc.boss && npc.Distance(Player.Center) < 1000)
                    {
                        npc.SimpleStrikeNPC(Player.GetWeaponDamage(Player.HeldItem), 0, true, 0, DamageClass.Generic, false, 0);
                    }
                }
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player p = Main.player[i];
                    if (p.active && p.Distance(Player.Center) < 1000 && !p.dead)
                    {
                        p.Hurt(PlayerDeathReason.ByCustomReason(LangHelper.GetTextValue($"DeathReason.InevitableEnd", Player.name)), Player.GetWeaponDamage(Player.HeldItem), 0);
                    }
                }
                for (int i = 0; i < 100; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(Player.Center.X, Player.Center.Y), 0, 0, DustID.LifeDrain, 0f + Main.rand.Next(-36, 36), 0f + Main.rand.Next(-36, 36), 100, default, 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }
            }
            base.Kill(damage, hitDirection, pvp, damageSource);
        }
        public override void OnHurt(Player.HurtInfo info)
        {
            if (!Main.dedServ)
            {
                inCombat = 1200;
                timeAfterGettingHit = 0;
            }
            if (starfarerOutfit == 4) // Aegis of Hope's Legacy
            {
                hopesBrilliance++;
                Player.AddBuff(BuffType<NascentAria>(), 180);
                Player.AddBuff(BuffID.Ironskin, 480);
                Player.AddBuff(BuffID.Regeneration, 480);
                Player.AddBuff(BuffID.Endurance, 480);
            }
            
            if(lavenderRefrain == 2)
            {
                inCombat = 1200;
                timeAfterGettingHit = 0;

                if (Player.statMana > 0)
                {
                    Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                    CombatText.NewText(textPos, new Color(122, 113, 153, 255), $"{Player.statMana}", false, false);
                    //If mana isn't enough to mitigate all the damage (as in Consumable Dodge)
                    info.Damage -= Player.statMana;
                    Player.statMana = 0;
                    Player.manaRegenDelay = 480;
                    Player.immune = true;
                    Player.immuneTime = 60;
                    lavenderRefrainMaxManaReduction -= 0.1f;
                }
            }
            if (ruinedKingPrism)
            {
                if (novaGauge >= 2)
                {
                    novaGauge -= 2;
                }
            }
            if (beyondInfinityDamageMod > 0)
            {
                beyondInfinityDamageMod = 0;
            }
            if (healthyConfidence == 2)
            {
                healthyConfidenceHealAmount += (int)(info.Damage * 0.15);
            }
            if (hikari == 2)
            {
                Player.AddBuff(BuffType<NullRadiance>(), 360);
            }
            if (bloomingflames == 2)
            {
                Player.AddBuff(BuffType<InfernalEnd>(), 180);
            }
            if (info.Damage >= 250 && info.Damage < Player.statLife)
            {
                if (Main.rand.Next(0, 8) == 0)
                {
                    starfarerPromptActive("onTakeHeavyDamage");

                }
            }
            if (Player.HasBuff(BuffType<SpatialStratagemCooldown>()) && artofwar == 2)
            {
                Player.AddBuff(BuffType<SpatialStratagemCooldown>(), 1800);
            }
            if (!Player.HasBuff(BuffType<StarshieldBuff>()) && !Player.HasBuff(BuffType<StarshieldCooldown>()) && starshower == 2)
            {
                Player.AddBuff(BuffType<StarshieldBuff>(), 120);
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ProjectileType<Starshield>(), 0, 0, Player.whoAmI);

                Player.statMana += 20;
                Player.ManaEffect(20);
            }
            if (Player.HasBuff(BuffType<UniversalManipulation>()))
            {
                int index = Player.FindBuffIndex(BuffType<UniversalManipulation>());
                if (index > -1)
                {
                    Player.DelBuff(index);
                }
            }
            if (cosmicPhoenixPrism)
            {
                if (Player.statLife - info.Damage < 50 && !Main.LocalPlayer.HasBuff(BuffType<GoingSupercriticalCooldown>()))
                {
                    novaGauge = novaGaugeMax;
                    Main.LocalPlayer.AddBuff(BuffType<GoingSupercriticalCooldown>(), 7200);
                }
            }
            if (starfarerOutfit == 1)
            {
                Player.AddBuff(BuffType<Vulnerable>(), 240);
            }
            if (Player.GetModPlayer<StarsAbovePlayer>().ironskin == 2)
            {
                if (info.Damage >= 100)
                {
                    info.Damage -= 30;
                }
                if (Player.statLife < 100)
                {
                    info.Damage = (int)(info.Damage * 0.8f);
                }
            }
        }
        public override bool ConsumableDodge(Player.HurtInfo info)
        {
            if(lavenderRefrain == 2)
            {
                if(Player.statMana >= info.Damage)
                {
                    
                    Player.immune = true;
                    Player.immuneTime = 60;

                    inCombat = 1200;
                    timeAfterGettingHit = 0;

                    Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                    CombatText.NewText(textPos, new Color(122, 113, 153, 255), $"{info.Damage}", false, false);
                    Player.statMana -= info.Damage;
                    Player.manaRegenDelay = 480;
                    lavenderRefrainMaxManaReduction -= 0.1f;
                    return true;
                }
            }
            if (Player.HasBuff(BuffType<SolemnAegis>()))
            {
                for (int d = 0; d < 16; d++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.FireworkFountain_Blue, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default, 1.5f);
                }
                Player.immune = true;
                Player.immuneTime = 30;
                int index = Player.FindBuffIndex(BuffType<SolemnAegis>());
                if (index > -1)
                {
                    Player.DelBuff(index);
                }
                return true;
            }
            if (Main.LocalPlayer.HasBuff(BuffType<FlashOfEternity>()))
            {
                Player.immune = true;
                Player.immuneTime = 30;
                int index = Player.FindBuffIndex(BuffType<FlashOfEternity>());
                if (index > -1)
                {
                    Player.DelBuff(index);
                }
                return true;
            }
            if (Player.HasBuff(BuffType<TimelessPotential>()) && !Player.HasBuff(BuffType<TimelessPotentialCooldown>()))
            {
                if (info.Damage > Player.statLife)
                {
                    Player.statLife = 50;
                    Player.AddBuff(BuffType<Invincibility>(), 120);
                    Player.AddBuff(BuffType<TimelessPotentialCooldown>(), 7200);
                    return true;
                }
                if (Main.rand.Next(0, 101) <= 10)
                {
                    Player.immuneTime = 30;
                    return true;
                }

            }
            if (Player.GetModPlayer<StarsAbovePlayer>().keyofchronology == 2)
            {
                if (info.Damage >= 200 && !Main.LocalPlayer.HasBuff(BuffType<KeyOfChronologyCooldown>()))
                {
                    if (starfarerPromptCooldown > 0)
                    {
                        starfarerPromptCooldown = 0;
                    }
                    starfarerPromptActive("onKeyOfChronology");
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_TimeEffect, Player.Center);
                    for (int d = 0; d < 12; d++)
                    {
                        Dust.NewDust(Player.position, 0, 0, 113, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default, 1.5f);
                    }
                    Player.Heal(info.Damage);
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && !npc.boss && npc.Distance(Player.Center) < 1000)
                        {
                            npc.AddBuff(BuffType<Stun>(), 300);
                        }
                    }

                    Player.AddBuff(BuffType<Invincibility>(), 300);
                    Player.AddBuff(BuffType<KeyOfChronologyCooldown>(), 7200);

                    if (starfarerOutfit == 4)
                    {
                        Player.ClearBuff(BuffID.PotionSickness);
                        Player.potionDelay = 0;
                        Player.potionDelayTime = 0;
                        Player.AddBuff(BuffID.Heartreach, 1800);

                    }

                    return true;
                }

                return false;
            }
            if (Player.HasBuff(BuffType<KiwamiRyuken>()))
            {
                StellarNovaCutIn();
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
                Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y), direction, Mod.Find<ModProjectile>("kiwamiryukenstun").Type, 1, 0, Player.whoAmI, 0, 1);
                onActivateStellarNova();
                SoundEngine.PlaySound(StarsAboveAudio.SFX_CounterImpact, Player.Center);
                return false;
            }
            return false;
        }
        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (evasionmastery == 2)
            {
                if (Main.rand.Next(0, 101) <= 3 && Player.immuneTime <= 0)
                {
                    Player.immune = true;
                    Player.immuneTime = 30;
                    return true;
                }
            }
            if (Main.LocalPlayer.HasBuff(BuffType<DashInvincibility>()))
            {
                return true;
            }
            return false;
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)/* tModPorter Override ImmuneTo, FreeDodge or ConsumableDodge instead to prevent taking damage */
        {
            if (!Player.HasBuff(BuffType<SpatialStratagemCooldown>()) && artofwar == 2)
            {
                Player.AddBuff(BuffType<SpatialStratagemCooldown>(), 1800);
                Player.AddBuff(BuffType<SpatialStratagem>(), 120);
                Player.AddBuff(BuffType<SpatialStratagemActive>(), 120);
                modifiers.FinalDamage -= 0.5f;
            }
            if (Player.HasBuff(BuffType<SpatialStratagem>()))
            {
                modifiers.FinalDamage -= 0.5f;
            }
            if (Player.GetModPlayer<StarsAbovePlayer>().starfarerOutfit == 3)
            {
                modifiers.FinalDamage -= 0.1f;
                novaGauge += 3;
            }
            if(stayTheCourse == 2)
            {
                modifiers.FinalDamage -= stayTheCourseStacks;
            }
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {

            if (SubworldSystem.Current != null)
            {
                int randomMessage = Main.rand.Next(0, 5);
                if (randomMessage == 0)
                {//Localize me later.
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

            if (Main.LocalPlayer.HasBuff(BuffType<LivingDead>()))
            {
                playSound = false;
                genGore = false;
                Player.statLife = 1;

                return false;
            }
            if (Main.LocalPlayer.HasBuff(BuffType<SpatialBurn>()))
            {

                damageSource = PlayerDeathReason.ByCustomReason(Player.name + " couldn't handle the vacuum of space.");

                return true;
            }

            if (livingdead == 2)
            {

                if (!Main.LocalPlayer.HasBuff(BuffType<LivingDead>()))
                {
                    if (!Main.LocalPlayer.HasBuff(BuffType<LivingDeadCooldown>()))
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
                                Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("AsphodeneBurst").Type, 0, 0, Player.whoAmI, 0, 1);

                        }
                        if (chosenStarfarer == 2)
                        {
                            if (Player.ownedProjectileCounts[Mod.Find<ModProjectile>("EridaniBurst").Type] < 1)
                                Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("EridaniBurst").Type, 0, 0, Player.whoAmI, 0, 1);

                        }*/
                        Player.AddBuff(BuffType<LivingDead>(), 360);
                        Dust dust;
                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        Vector2 position = Player.Center;
                        dust = Main.dust[Dust.NewDust(position, 0, 0, 247, 0f, 0f, 0, new Color(255, 0, 0), 1f)];


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
                        promptExpression = 2;
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
                            promptExpression = 5;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.19", Player.name); //One more defeated!
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.20", Player.name); //That takes care of that.
                        }
                        if (randomDialogue == 3)
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.21", Player.name); //Another one down.
                        }
                        if (randomDialogue == 4)
                        {
                            promptExpression = 5;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.22", Player.name); //You're great at this!
                        }
                        if (randomDialogue == 5)
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.23", Player.name); //Nice work. That's one down.
                        }
                        if (randomDialogue == 6)
                        {
                            promptExpression = 4;
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
                            promptExpression = 4;
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
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.30", Player.name); //That was so easy!
                        }
                        if (randomDialogue == 13)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.31", Player.name); //Enough of that one.
                        }
                        if (randomDialogue == 14)
                        {
                            promptExpression = 5;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.32", Player.name); //An easy victory.
                        }
                        if (randomDialogue == 15)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.33", Player.name); //Right, that's that.
                        }
                        if (randomDialogue == 16)
                        {
                            promptExpression = 4;
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
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.40", Player.name); //Thank goodness we killed it in time.
                        }
                    }
                    if (eventPrompt == "onKillBossEnemy")
                    {
                        starfarerPromptActiveTimer = 150;
                        randomDialogue = Main.rand.Next(0, 3);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.41", Player.name); //Finally. You got it!
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 5;
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
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.44", Player.name); //A decisive crit!
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 5;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.45", Player.name); //They felt that one!
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.46", Player.name); //A perfectly-timed attack.
                        }
                        if (randomDialogue == 3)
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.47", Player.name); //Nice, you hit their weak spot.
                        }
                        if (randomDialogue == 4)
                        {
                            promptExpression = 5;
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
                            promptExpression = 2;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.51", Player.name); //That's.. not good.
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.52", Player.name); //Ouch...
                        }
                        if (randomDialogue == 3)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.53", Player.name); //That wasn't good at all..
                        }
                        if (randomDialogue == 4)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.54", Player.name); //You should probably heal after that one.
                        }
                        if (randomDialogue == 5)
                        {
                            promptExpression = 3;
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
                            promptExpression = 2;
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
                            promptExpression = 5;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.60", Player.name); //An incredibly strong foe draws near! Let's give them a fight to remember!
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.61", Player.name); //A strong foe draws near. It's time to fight.
                        }
                        if (randomDialogue == 3)
                        {
                            promptExpression = 1;
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
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.64", Player.name); //The.. eyeball.. approaches. Watch yourself- it's a big one. It gets stronger when it's on its last legs, I think.
                        seenEyeOfCthulhu = true;
                    }
                    if (eventPrompt == "onKingSlime")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.65", Player.name); //It's the lord of the slimes! I think it has a teleportation-like ability..
                        seenKingSlime = true;
                    }
                    if (eventPrompt == "onEaterOfWorlds")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.66", Player.name); //A colossal worm! Watch where it emerges; it'll try and hit your blind spots!
                        seenEaterOfWorlds = true;
                    }
                    if (eventPrompt == "onBrainOfCthulhu")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.67", Player.name); //This thing is trying to attack your mind directly..! Don't be fooled by the mirages!
                        seenBrainOfCthulhu = true;
                    }
                    if (eventPrompt == "onQueenBee")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.68", Player.name); //Watch out.. the Queen Bee is awake! Make sure to dodge the horizontal charges!
                        seenQueenBee = true;
                    }
                    if (eventPrompt == "onSkeletron")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.69", Player.name); //Don't underestimate this monster! Stay away from his skull and arms!
                        seenSkeletron = true;
                    }
                    if (eventPrompt == "onWallOfFlesh")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.70", Player.name); //That thing is massive! If you can, try and build a path to fight it on.
                        seenWallOfFlesh = true;
                    }
                    if (eventPrompt == "onTwins")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.71", Player.name); //There's two giant eyeballs coming your way! Let's see... The red one will shoot at you, and the green one charges.
                        seenTwins = true;
                    }
                    if (eventPrompt == "onDeerclops")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.72", Player.name); //What in the world is that thing? A deer? It's only got one eye... aim for it!
                        seenDeerclops = true;
                    }
                    if (eventPrompt == "onQueenSlime")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.73", Player.name); //It's another colossal slime! It looks like it's going to summon some minions!
                        seenQueenSlime = true;
                    }
                    if (eventPrompt == "onEmpress")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.74", Player.name); //A Hallow-aspected foe has appeared! Maybe leave the butterfly alone next time...?
                        seenEmpress = true;
                    }
                    if (eventPrompt == "onDestroyer")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.75", Player.name); //A giant mechanical worm.. What can we do..? How about trying to attack multiple parts of it at once?
                        seenDestroyer = true;
                    }
                    if (eventPrompt == "onSkeletronPrime")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.76", Player.name); //It's a more advanced version of Skeletron.. Try going for the arms first, instead of the head.
                        seenSkeletronPrime = true;
                    }
                    if (eventPrompt == "onPlantera")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.77", Player.name); //Plantera is awake! Be mindful of its vines. It would be great to have a huge arena to fight it in.
                        seenPlantera = true;
                    }
                    if (eventPrompt == "onGolem")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.78", Player.name); //An ancient mechanical monster.. Watch out for the traps in the temple while fighting it.
                        seenGolem = true;
                    }
                    if (eventPrompt == "onDukeFishron")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.79", Player.name); //You reeled in something crazy! Something tells me you should stay near the sea!
                        seenDukeFishron = true;
                    }
                    if (eventPrompt == "onLunaticCultist")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.80", Player.name); //It's one of those Lunatic Cultists.. Stop them before they unleash a calamity..!
                        seenCultist = true;
                    }
                    if (eventPrompt == "onMoonLord")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.81", Player.name); //I can't believe it.. it's the Moon Lord! This is the final battle! We have to win this!
                        seenMoonLord = true;
                    }
                    if (eventPrompt == "onWarriorOfLight")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.82", Player.name); //The Warrior of Light approaches.. When he breaks his limits, prepare yourself!
                        seenWarriorOfLight = true;
                    }
                    if (eventPrompt == "onVagrant")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.83", Player.name); //Something about this foe seems familiar... Attacks won't work; just survive for as long as you can!
                        seenVagrant = true;
                    }
                    if (eventPrompt == "onNalhaun")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.84", Player.name); //Don't underestimate this foe..! Keep grabbing the stolen lifeforce he's taking from you!
                        seenNalhaun = true;
                    }
                    if (eventPrompt == "onPenth")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.85", Player.name); //This witch attacks with paint! Mind what color you're doused in!
                        seenPenth = true;
                    }
                    if (eventPrompt == "onArbiter")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.86", Player.name); //This thing changes its attack patterns! Take note of its stances, or else!
                        seenArbiter = true;
                    }
                    if (eventPrompt == "onThespian")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.202", Player.name); //This thing changes its attack patterns! Take note of its stances, or else!
                        seenThespian = true;
                    }
                    if (eventPrompt == "onStarfarerBoss")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.203", Player.name); //This thing changes its attack patterns! Take note of its stances, or else!
                        seenStarfarers = true;
                    }
                    //Calamity mod bosses!
                    if (eventPrompt == "onDesertScourge")
                    {
                        promptExpression = 1;
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
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.89", Player.name); //A corrupted beast draws near! Kill its minions quickly, lest it overwhelm you!
                        seenHiveMind = true;
                    }
                    if (eventPrompt == "onPerforators")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.90", Player.name); //What in the world is that thing? Aim for that disgusting Hive before it's too late!
                        seenPerforators = true;
                    }
                    if (eventPrompt == "onSlimeGod")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.91", Player.name); //So this is the source of all the world's slimes. Don't get hasty; I'm certain those slimes will split when hurt!
                        seenSlimeGod = true;
                    }
                    //Hardmode Calamity Bosses
                    if (eventPrompt == "onCryogen")
                    {
                        promptExpression = 1;
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
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.94", Player.name); //It's a giant sea serpent! Hang on... This thing is SERIOUSLY dangerous!
                        }

                        seenAquaticScourge = true;
                    }
                    if (eventPrompt == "onBrimstoneElemental")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.95", Player.name); //The flames have brought forth a demonic spirit! Take care to watch your footing, lest you succumb to lava!
                        seenBrimstoneElemental = true;
                    }
                    if (eventPrompt == "onCalamitas")
                    {
                        promptExpression = 1;
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
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.98", Player.name); //...Do you hear that? Whatever it is.. it sounds really dangerous.
                        }

                        seenSiren = true;
                    }
                    if (eventPrompt == "onAnahita")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.99", Player.name); //That demon of the sea is fighting back! I hope you're ready for this...
                        seenAnahita = true;
                    }
                    if (eventPrompt == "onLeviathan")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.100", Player.name); //Whoa. This thing is enormous!! Who knew she had this up her sleeve..?
                        seenLeviathan = true;
                    }
                    if (eventPrompt == "onAstrumAureus")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.101", Player.name); //The Astral Infection has corrupted whatever this was. Don't underestimate it. The Infection can do anything...
                        seenAstrumAureus = true;
                    }
                    if (eventPrompt == "onPlaguebringer")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.102", Player.name); //Oh, great.. a giant robotic bug. It kind of looks like the Queen Bee, so try and remember her attacks!
                        seenPlaguebringer = true;
                    }
                    if (eventPrompt == "onRavager")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.103", Player.name); //This is.. an amalgamation of flesh and machinery.. Above everything, try and stay away from it!
                        seenRavager = true;
                    }
                    if (eventPrompt == "onAstrumDeus")
                    {
                        promptExpression = 1;
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
                        promptExpression = 1;
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
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.112", Player.name); //The acid ocean has spat out a monster! Wait until it gets tired to strike!
                        seenOldDuke = true;
                    }
                    if (eventPrompt == "onDog")
                    {
                        promptExpression = 1;
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
                        promptExpression = 3;
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
                        promptExpression = 1;
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

                        promptExpression = 1;
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
                        promptExpression = 1;
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
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.125", Player.name);
                        //promptDialogue = $"If I had a nickel for times you've thrown voodoo dolls into lava... Never mind." +
                        //                $" Mutant's super upset, super strong, and super coming straight for you.";
                        seenMutant = true;
                    }
                    if (eventPrompt == "onScarabeus")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.194", Player.name);
                        seenScarabeus = true;
                    }
                    if (eventPrompt == "onMoonJellyWizard")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.195", Player.name);
                        seenMoonJellyWizard = true;
                    }
                    if (eventPrompt == "onVinewrathBane")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.196", Player.name);
                        seenVinewrathBane = true;
                    }
                    if (eventPrompt == "onAncientAvian")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.197", Player.name);
                        seenAncientAvian = true;
                    }
                    if (eventPrompt == "onStarplateVoyager")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.198", Player.name);
                        seenStarplateVoyager = true;
                    }
                    if (eventPrompt == "onInfernon")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.199", Player.name);
                        seenInfernon = true;
                    }
                    if (eventPrompt == "onDusking")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.200", Player.name);
                        seenDusking = true;
                    }
                    if (eventPrompt == "onAtlas")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.201", Player.name);
                        seenAtlas = true;
                    }
                    //Wrath of the Gods
                    if (eventPrompt == "onNoxusEgg")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.204", Player.name);
                        seenNoxusEgg = true;
                    }
                    if (eventPrompt == "onEntropicGod")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.205", Player.name);
                        seenEntropicGod = true;
                    }
                    if (eventPrompt == "onNamelessDeity")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.206", Player.name);
                        seenNamelessDeity = true;
                    }
                    //Starlight River
                    if (eventPrompt == "onGlassweaver")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.207", Player.name);
                        seenGlassweaver = true;
                    }               
                    if (eventPrompt == "onAuroracle")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.208", Player.name);
                        seenAuroracle = true;
                    }
                    if (eventPrompt == "onCeiros")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.209", Player.name);
                        seenCeiros = true;
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
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.135", Player.name); //The Abyssal Shadows are converging! I sense powerful dark magic from this sea creature..
                        seenAbyssion = true;
                    }
                    if (eventPrompt == "onPrimordials")
                    {
                        promptExpression = 5;
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
                        promptExpression = 2;
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
                        promptExpression = 5;
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
                        promptExpression = 2;
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
                        promptExpression = 5;
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
                        promptExpression = 5;
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
                        promptExpression = 1;
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
                        promptExpression = 5;
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
                            promptExpression = 5;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.160", Player.name); //Time and space bend to my will!
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 4;
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
                            promptExpression = 1;
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
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.169", Player.name); //Let's show them our power.
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 4;
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
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.173", Player.name); //Just tell me when.
                        }
                        if (randomDialogue == 5)
                        {
                            promptExpression = 5;
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
                        promptExpression = 3;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.177", Player.name); //We've made it. This planet is strange.. The surface has been wiped clean... What happened?
                        seenBleachedPlanet = true;
                    }
                    if (eventPrompt == "onConfluence")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Asphodene.178", Player.name); //She was so close all along... Use the Mnemonic Sigil on the arena's center to begin.
                        seenConfluence = true;
                    }
                    if (eventPrompt == "onCity")
                    {
                        promptExpression = 3;
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
                        promptExpression = 2;
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
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.20", Player.name); //Well fought.
                        }
                        if (randomDialogue == 3)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.21", Player.name); //Another one down.
                        }
                        if (randomDialogue == 4)
                        {
                            promptExpression = 5;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.22", Player.name); //You're pretty good at this.
                        }
                        if (randomDialogue == 5)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.23", Player.name); //Nice work.
                        }
                        if (randomDialogue == 6)
                        {
                            promptExpression = 4;
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
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.30", Player.name); //Wasn't even a problem.
                        }
                        if (randomDialogue == 13)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.31", Player.name); //We got that taken care of.
                        }
                        if (randomDialogue == 14)
                        {
                            promptExpression = 5;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.32", Player.name); //Good going.
                        }
                        if (randomDialogue == 15)
                        {
                            promptExpression = 0;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.33", Player.name); //Right, that's that.
                        }
                        if (randomDialogue == 16)
                        {
                            promptExpression = 4;
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
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.38", Player.name); //That was a little close for comfort..
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.39", Player.name); //That's over with, but we're still in trouble.
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.40", Player.name); //That was close.
                        }
                    }
                    if (eventPrompt == "onKillBossEnemy")
                    {
                        randomDialogue = Main.rand.Next(0, 3);
                        starfarerPromptActiveTimer = 150;
                        if (randomDialogue == 0)
                        {
                            promptExpression = 5;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.41", Player.name); //Finally. It's defeated.
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 4;
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
                            promptExpression = 5;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.44", Player.name); //Perfect attack!
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 5;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.45", Player.name); //A critical hit!
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.46", Player.name); //Expertly done.
                        }
                        if (randomDialogue == 3)
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.47", Player.name); //Nice, you hit their weak spot.
                        }
                        if (randomDialogue == 4)
                        {
                            promptExpression = 5;
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
                            promptExpression = 2;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.51", Player.name); //Are you alright?
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.52", Player.name); //That doesn't look good.
                        }
                        if (randomDialogue == 3)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.53", Player.name); //This could be going better..
                        }
                        if (randomDialogue == 4)
                        {
                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.54", Player.name); //Oww..
                        }
                        if (randomDialogue == 5)
                        {
                            promptExpression = 2;
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
                            promptExpression = 2;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.57", Player.name); //That is.. not good.
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 1;
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
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.60", Player.name); //I can sense powerful energy approaching. Ready or not, it's time for a fight.
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.61", Player.name); //A strong opponent draws near. Prepare yourself.
                        }
                        if (randomDialogue == 3)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.62", Player.name); //Stay alert, {0}. Something powerful is on its way.
                        }
                        if (randomDialogue == 4)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.63", Player.name); //Danger approaches. We will show no mercy.
                        }
                    }
                    if (eventPrompt == "onEyeOfCthulhu")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.64", Player.name); //Here it comes. Whatever it is, it's dangerous. Take extra care when it's weak; it'll be forced into a frenzy.
                        seenEyeOfCthulhu = true;
                    }
                    if (eventPrompt == "onKingSlime")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.65", Player.name); //That's a.. giant slime. It seems to be able to move really quickly. Watch for that.
                        seenKingSlime = true;
                    }
                    if (eventPrompt == "onEaterOfWorlds")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.66", Player.name); //This must be the Eater of Worlds. It'll try and suprise you from below.
                        seenEaterOfWorlds = true;
                    }
                    if (eventPrompt == "onBrainOfCthulhu")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.67", Player.name); //Watch out. It's trying to attack your mind itself. Pay close attention to the mirages.
                        seenBrainOfCthulhu = true;
                    }
                    if (eventPrompt == "onQueenBee")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.68", Player.name); //That giant bee is attacking! Don't get caught in the honey!
                        seenQueenBee = true;
                    }
                    if (eventPrompt == "onSkeletron")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.69", Player.name); //The Clothier has turned into.. this.. Stay away from its skull and arms.
                        seenSkeletron = true;
                    }
                    if (eventPrompt == "onWallOfFlesh")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.70", Player.name); //This thing is incredibly strong.. Whatever you do, keep running..!
                        seenWallOfFlesh = true;
                    }
                    if (eventPrompt == "onTwins")
                    {
                        promptExpression = 1;
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
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.73", Player.name); //Looks to be another type of gigantic slime. I have a feeling it'll use minions to do its bidding.
                        seenQueenSlime = true;
                    }
                    if (eventPrompt == "onEmpress")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.74", Player.name); //Something dangerous stirs in the Hallow... It looks to draw overwhelming power during the daytime!
                        seenEmpress = true;
                    }
                    if (eventPrompt == "onDestroyer")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.75", Player.name); //The Destroyer approaches.. Let's try using area-of-effect attacks against it.
                        seenDestroyer = true;
                    }
                    if (eventPrompt == "onSkeletronPrime")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.76", Player.name); //Skeletron is back, and better than ever! Try prioritizing the appendages first.
                        seenSkeletronPrime = true;
                    }
                    if (eventPrompt == "onPlantera")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.77", Player.name); //The menace of the Jungle is attacking..! Don't get stuck on your surroundings!
                        seenPlantera = true;
                    }
                    if (eventPrompt == "onGolem")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.78", Player.name); //The Lizhard's beast is awake! Mind the Jungle Temple's traps during this fight.
                        seenGolem = true;
                    }
                    if (eventPrompt == "onDukeFishron")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.79", Player.name); //You've reeled in a dangerous foe! Stay near the sea lest it become enraged!
                        seenDukeFishron = true;
                    }
                    if (eventPrompt == "onLunaticCultist")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.80", Player.name); //It's a Lunatic Cultist.. You have to defeat them before they can summon calamity!
                        seenCultist = true;
                    }
                    if (eventPrompt == "onMoonLord")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.81", Player.name); //The Moon Lord.. We come face to face with a god. We have to win this! There's no other option!
                        seenMoonLord = true;
                    }
                    if (eventPrompt == "onWarriorOfLight")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.82", Player.name); //The Warrior of Light approaches.. His Limit Breaks are incredibly strong!
                        seenWarriorOfLight = true;
                    }
                    if (eventPrompt == "onVagrant")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.83", Player.name); //Something about this foe seems familiar... Your attacks won't work; just try and survive!
                        seenVagrant = true;
                    }
                    if (eventPrompt == "onNalhaun")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.84", Player.name); //Don't underestimate this king of eld! Take back the lifeforce he's stealing!
                        seenNalhaun = true;
                    }
                    if (eventPrompt == "onPenth")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.85", Player.name); //She's covering you with paint. Take careful note of your color!
                        seenPenth = true;
                    }
                    if (eventPrompt == "onArbiter")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.86", Player.name); //It seems to be able to swap forms! Try to memorize its attacks..!
                        seenArbiter = true;
                    }
                    if (eventPrompt == "onThespian")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.204", Player.name); //This thing changes its attack patterns! Take note of its stances, or else!
                        seenThespian = true;
                    }
                    if (eventPrompt == "onStarfarerBoss")
                    {
                        promptExpression = 0;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.205", Player.name); //This thing changes its attack patterns! Take note of its stances, or else!
                        seenStarfarers = true;
                    }
                    //Calamity Mod Bosses

                    if (eventPrompt == "onDesertScourge")
                    {
                        promptExpression = 1;
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
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.89", Player.name); //The corruption has spewed out a new beast. Look out for its barrage of minions.
                        seenHiveMind = true;
                    }
                    if (eventPrompt == "onPerforators")
                    {
                        promptExpression = 1;
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
                        promptExpression = 1;
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
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.93", Player.name); //A colossal serpent makes its way towards us.. It may overwhelm you.. don't get reckless.
                        }

                        seenAquaticScourge = true;
                    }
                    if (eventPrompt == "onBrimstoneElemental")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.94", Player.name); //That Charred Idol has summoned a flaming spirit.. It can teleport everywhere- don't lose track of it.
                        seenBrimstoneElemental = true;
                    }
                    if (eventPrompt == "onCalamitas")
                    {
                        promptExpression = 1;
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

                            promptExpression = 3;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.97", Player.name); //Can you hear that, {0}? Something's singing in the ocean?

                        }

                        seenSiren = true;
                    }
                    if (eventPrompt == "onAnahita")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.98", Player.name); //Ah.. It looks like you've angered her. I hope you're prepared for a fight..
                        seenAnahita = true;
                    }
                    if (eventPrompt == "onLeviathan")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.99", Player.name); //What in the world.. It's massive! Take caution; the playing field has changed!
                        seenLeviathan = true;
                    }
                    if (eventPrompt == "onAstrumAureus")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.100", Player.name); //It's a mechanical beast, but it's been corrupted by the Astral Infection. Stay cautious.
                        seenAstrumAureus = true;
                    }
                    if (eventPrompt == "onPlaguebringer")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.101", Player.name); //Ugh. It's a disgusting bug, but now it has artillery. When it gets wounded, those missiles will likely detonate.
                        seenPlaguebringer = true;
                    }
                    if (eventPrompt == "onRavager")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.102", Player.name); //Yuck. It's a huge pile of flesh and bones. From what I can tell, getting too close will end you fast.
                        seenRavager = true;
                    }
                    if (eventPrompt == "onAstrumDeus")
                    {
                        promptExpression = 1;
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
                        promptExpression = 1;
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
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.112", Player.name); //The Devourer of Gods has appeared! You must give this battle your all!
                        seenDog = true;
                    }
                    if (eventPrompt == "onYharon")
                    {
                        promptExpression = 4;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.113", Player.name); //The Jungle Dragon roars. Let's throw a wrench in Yharim's plans.
                        seenYharon = true;
                    }
                    if (eventPrompt == "onYharonDespawn")
                    {
                        promptExpression = 3;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.114", Player.name); //Where's it going? Were we.. not strong enough?
                        seenYharonDespawn = true;
                    }
                    if (eventPrompt == "onSupremeCalamitas")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.115", Player.name); //No way.. Its power is ineffable. {Player.name}.. We have to win this!
                        seenSupremeCalamitas = true;
                    }
                    //Draedon Update
                    if (eventPrompt == "onDraedon")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.116", Player.name); //Draedon is here... Be mindful of his tactics. His knowledge spans the world over.
                        seenDraedon = true;
                    }
                    if (eventPrompt == "onArtemis")
                    {
                        promptExpression = 1;
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
                        promptExpression = 1;
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
                        promptExpression = 1;
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
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.123", Player.name); //Oh, you just HAD to throw that thing into lava, did you? Well, good luck, because Mutant is pissed. Didn't see that coming- wait, I did.


                        seenMutant = true;
                    }
                    if (eventPrompt == "onScarabeus")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.196", Player.name);
                        seenScarabeus = true;
                    }
                    if (eventPrompt == "onMoonJellyWizard")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.197", Player.name);
                        seenMoonJellyWizard = true;
                    }
                    if (eventPrompt == "onVinewrathBane")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.198", Player.name);
                        seenVinewrathBane = true;
                    }
                    if (eventPrompt == "onAncientAvian")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.199", Player.name);
                        seenAncientAvian = true;
                    }
                    if (eventPrompt == "onStarplateVoyager")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.200", Player.name);
                        seenStarplateVoyager = true;
                    }
                    if (eventPrompt == "onInfernon")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.201", Player.name);
                        seenInfernon = true;
                    }
                    if (eventPrompt == "onDusking")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.202", Player.name);
                        seenDusking = true;
                    }
                    if (eventPrompt == "onAtlas")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.203", Player.name);
                        seenAtlas = true;
                    }
                    //Wrath of the Gods
                    if (eventPrompt == "onNoxusEgg")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.206", Player.name);
                        seenNoxusEgg = true;
                    }
                    if (eventPrompt == "onEntropicGod")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.207", Player.name);
                        seenEntropicGod = true;
                    }
                    if (eventPrompt == "onNamelessDeity")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.208", Player.name);
                        seenNamelessDeity = true;
                    }
                    //Starlight River
                    if (eventPrompt == "onGlassweaver")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.209", Player.name);
                        seenGlassweaver = true;
                    }
                    if (eventPrompt == "onAuroracle")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.210", Player.name);
                        seenAuroracle = true;
                    }
                    if (eventPrompt == "onCeiros")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.211", Player.name);
                        seenCeiros = true;
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
                        promptExpression = 1;
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
                        promptExpression = 3;
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
                        promptExpression = 5;
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
                        promptExpression = 2;
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
                        promptExpression = 5;
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
                        promptExpression = 3;
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
                        promptExpression = 1;
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
                            promptExpression = 5;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.158", Player.name); //Time and space bend to my will!
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 4;
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
                            promptExpression = 1;
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
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.164", Player.name); //They'll all fall before us.
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.165", Player.name); //Like lambs to slaughter!
                        }
                        if (randomDialogue == 2)
                        {
                            promptExpression = 4;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.166", Player.name); //We'll finish them all off!
                        }
                    }
                    //
                    if (eventPrompt == "onStellarNovaCharged")
                    {
                        randomDialogue = Main.rand.Next(0, 6);

                        if (randomDialogue == 0)
                        {
                            promptExpression = 1;
                            promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.167", Player.name); //I'm ready when you are.
                        }
                        if (randomDialogue == 1)
                        {
                            promptExpression = 4;
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
                            promptExpression = 4;
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
                        promptExpression = 3;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.175", Player.name); //This is it. An entire planet that's devoid of color? The surface looks like it was wiped away somehow...
                        seenBleachedPlanet = true;
                    }
                    if (eventPrompt == "onConfluence")
                    {
                        promptExpression = 1;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.176", Player.name); //She was here the whole time...? Well.. I'm ready when you are. Use the Sigil in the middle.
                        seenConfluence = true;
                    }
                    if (eventPrompt == "onCity")
                    {
                        promptExpression = 3;
                        promptDialogue = LangHelper.GetTextValue($"Dialogue.PromptDialogue.Eridani.177", Player.name); //It looks like we can't explore this yet... Maybe next time.
                        seenCity = true;
                    }
                }
                promptDialogue = LangHelper.Wrap(promptDialogue, 78);
            }
        }
        public void StellarNovaEnergy()
        {
            novaGaugeChangeAlpha -= 0.1f;
            novaGaugeChangeAlphaSlow -= 0.01f;

            //Drain the gauge upon usage of the Stellar Nova.
            novaDrain = MathHelper.Clamp(novaDrain, 0, 1);
            if (novaDrain > 0)
            {
                novaDrain -= 0.05f;
                novaGauge = (int)MathHelper.Lerp(0, trueNovaGaugeMax, novaDrain);
                return;
            }

            if (inCombat > 0)
            {
                if (chosenStellarNova != 0)
                {
                    //Every x ticks, the gauge charges by 1.
                    if (novaGaugeChargeTimer >= 60)
                    {
                        //Natural charge rate.
                        novaGauge++;
                        //Special visuals on the gauge
                        novaGaugeChangeAlpha = 1f;
                        novaGaugeChangeAlphaSlow = 1f;

                        //Reset the timer.
                        novaGaugeChargeTimer = 0;
                        //If any effect speeds up the Nova gauge charging, add it here.
                        NovaChargeModifiers();

                    }
                }

                //If the gauge is unlocked and not max, increase the timer before the gauge charges.
                if (novaGaugeUnlocked && novaGauge < trueNovaGaugeMax)
                {
                    novaGaugeChargeTimer++;

                }

            }
            else
            {
                novaGaugeChargeTimer = 0;
                if (unbridledradiance != 2)
                {
                    //Unbridled Radiance prevents Nova drain (this is with it disabled.)
                    novaGaugeLossTimer++;
                    if (novaGaugeLossTimer >= 35)
                    {
                        novaGaugeLossTimer = 0;
                        novaGauge--;
                    }
                }
            }

            if (novaGauge == trueNovaGaugeMax)
            {
                if (novaReadyInfo)
                {
                    gaugeChargeAnimation = true;
                    novaReadyInfo = false;
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_superReadySFX, Player.Center);
                    Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                    CombatText.NewText(textPos, new Color(255, 0, 125, 240), LangHelper.GetTextValue("CombatText.StellarNovaReady"), false, false);
                    if (Main.rand.Next(0, 5) == 0)
                    {
                        starfarerPromptActive("onStellarNovaCharged");
                    }
                }

            }
            else if (novaGauge < trueNovaGaugeMax)
            {
                novaReadyInfo = true;

            }

            novaGauge = (int)MathHelper.Clamp(novaGauge, 0, trueNovaGaugeMax);
        }

        private void NovaChargeModifiers()
        {
            if (unbridledradiance == 2)
            {
                //Unbridled Radiance doubles Nova gain.
                novaGauge++;
            }
            if (avataroflight == 2 && Player.statLife >= 500)
            {
                //Decrease the time spent charging
                novaGaugeChargeTimer += 2;
            }
            if (astralmantle == 2 && Player.statMana > 200)
            {
                //Decrease the time spent charging
                novaGaugeChargeTimer += 5;
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
            Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("SpaceBurstFX").Type, 0, 0, Player.whoAmI, 0, 1);
            Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("SpaceBurstFX2").Type, 0, 0, Player.whoAmI, 0, 1);

            if (chosenStellarNova != 7)
            {
                activateShockwaveEffect = true;
            }

            //Drain the Stellar Nova gauge.
            novaDrain = 1f;

            if (chosenStarfarer == 1)
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("AsphodeneBurstFX").Type, 0, 0, Player.whoAmI, 0, 1);
                Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("AsphodeneBurstFX2").Type, 0, 0, Player.whoAmI, 0, 1);
                Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("AsphodeneBurst" + starfarerOutfitVisible).Type, 0, 0, Player.whoAmI, 0, 1);
            }
            if (chosenStarfarer == 2)
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("EridaniBurstFX").Type, 0, 0, Player.whoAmI, 0, 1);
                Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("EridaniBurstFX2").Type, 0, 0, Player.whoAmI, 0, 1);
                Projectile.NewProjectile(Player.GetSource_FromThis(), new Vector2(Player.Center.X, Player.Center.Y - 500), Vector2.Zero, Mod.Find<ModProjectile>("EridaniBurst" + starfarerOutfitVisible).Type, 0, 0, Player.whoAmI, 0, 1);
            }
            if (ruinedKingPrism)
            {
                Player.AddBuff(BuffType<SovereignDominion>(), 900);
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
                Player.AddBuff(BuffType<Lightblessed>(), 480);
            }

            if (spatialPrism)
            {
                Player.AddBuff(BuffID.Regeneration, 720);
                Player.AddBuff(BuffID.ManaRegeneration, 720);
                Player.AddBuff(BuffID.Heartreach, 720);
            }
            if (voidsentPrism)
            {
                Vector2 placement2 = new Vector2(Player.Center.X, Player.Center.Y);
                Projectile.NewProjectile(Player.GetSource_FromThis(), placement2.X, placement2.Y, 0, 0, Mod.Find<ModProjectile>("radiate").Type, 0, 0f, 0);
                Projectile.NewProjectile(Player.GetSource_FromThis(), placement2.X, placement2.Y, 0, 0, Mod.Find<ModProjectile>("VoidsentBurst").Type, baseNovaDamageAdd / 10, 0f, 0);
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
            if (geminiPrism)
            {
                if (!Player.HasBuff(BuffType<GeminiPrismCooldown>()))
                {
                    Player.AddBuff(BuffType<GeminiPrismCooldown>(), 7200);

                    Player.AddBuff(BuffType<TwincastActive>(), 300);


                    //novaGauge = trueNovaGaugeMax / 2;
                }

            }

        }
        private void ModifyHitEnemyWithNovaCrit(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (royalSlimePrism)
            {
                modifiers.FinalDamage *= 1.4f;
            }
        }
        private void ModifyHitEnemyWithNovaNoCrit(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (royalSlimePrism)
            {
                modifiers.FinalDamage *= 0.8f;
            }
        }
        private void ModifyHitEnemyWithNova(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (empressPrism)
            {
                modifiers.SetCrit();
                modifiers.FinalDamage *= 0.7f;
            }
            if (burnishedPrism)
            {
                if (target.boss)
                {
                    modifiers.DisableCrit();
                }
                else
                {
                    modifiers.FinalDamage *= 1.4f;
                }
            }
            if (starfarerOutfit == 4 && hopesBrilliance > 0)
            {
                for (int i = 0; i < hopesBrilliance / 10; i++)
                {
                    modifiers.FinalDamage *= 1.02f;
                }

                hopesBrilliance = 0;
            }
            if (luminitePrism)
            {
                if (trueNovaGaugeMax >= 200)
                {
                    modifiers.FinalDamage *= 1.5f;
                }
            }
        }
        private void OnEnemyHitWithNova(NPC target, int nova, ref int damage, ref bool crit)
        {
            if (paintedPrism)
            {
                target.AddBuff(BuffID.Ichor, 720);
                target.AddBuff(BuffID.Frostburn, 720);
                target.AddBuff(BuffID.Poisoned, 720);
                target.AddBuff(BuffID.OnFire, 720);
                target.AddBuff(BuffID.Bleeding, 720);
                target.AddBuff(BuffID.Confused, 720);
                target.AddBuff(BuffID.Poisoned, 720);
                target.AddBuff(BuffID.BetsysCurse, 720);
                target.AddBuff(BuffID.Venom, 720);
                target.AddBuff(BuffID.ShadowFlame, 720);
                target.AddBuff(BuffID.Midas, 720);
                target.AddBuff(BuffID.Oiled, 720);

            }
            if (mechanicalPrism && Player.HasBuff(BuffType<MechanicalPrismBuff>()))
            {
                Player.ClearBuff(BuffType<MechanicalPrismBuff>());
                target.AddBuff(BuffType<Stun>(), 120);
                Vector2 placement2 = new Vector2(target.Center.X, target.Center.Y);

                Projectile.NewProjectile(Player.GetSource_FromThis(), placement2.X, placement2.Y, 0, 0, Mod.Find<ModProjectile>("VoidsentBurst").Type, damage / 10, 0f, 0);
            }
        }


        public override void ResetEffects()
        {




            Observatory = false;
            SeaOfStars = false;

            IsVoidActive = false;



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


        }



        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            base.ModifyDrawInfo(ref drawInfo);
        }



    }

};