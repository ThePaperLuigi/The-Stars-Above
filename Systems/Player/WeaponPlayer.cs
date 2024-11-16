using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using StarsAbove.Buffs;
using StarsAbove.Buffs.Accessories;
using StarsAbove.Buffs.Boss;
using StarsAbove.Buffs.Celestial.BrilliantSpectrum;
using StarsAbove.Buffs.Celestial.CatalystMemory;
using StarsAbove.Buffs.Celestial.TheOnlyThingIKnowForReal;
using StarsAbove.Buffs.Celestial.UltimaThule;
using StarsAbove.Buffs.Magic.CarianDarkMoon;
using StarsAbove.Buffs.Magic.CloakOfAnArbiter;
using StarsAbove.Buffs.Magic.DraggedBelow;
using StarsAbove.Buffs.Magic.HunterSymphony;
using StarsAbove.Buffs.Magic.LightUnrelenting;
using StarsAbove.Buffs.Magic.Ozma;
using StarsAbove.Buffs.Magic.ParadiseLost;
using StarsAbove.Buffs.Magic.RedMage;
using StarsAbove.Buffs.Magic.StygianNymph;
using StarsAbove.Buffs.Magic.SupremeAuthority;
using StarsAbove.Buffs.Magic.TwinStarsOfAlbiero;
using StarsAbove.Buffs.Magic.VenerationOfButterflies;
using StarsAbove.Buffs.Magic.VermillionDaemon;
using StarsAbove.Buffs.Magic.VisionOfEuthymia;
using StarsAbove.Buffs.Melee.AshenAmbition;
using StarsAbove.Buffs.Melee.BurningDesire;
using StarsAbove.Buffs.Melee.Drachenlance;
using StarsAbove.Buffs.Melee.InugamiRipsaw;
using StarsAbove.Buffs.Melee.LiberationBlazing;
using StarsAbove.Buffs.Melee.ManiacalJustice;
using StarsAbove.Buffs.Melee.PenthesileaMuse;
using StarsAbove.Buffs.Melee.SakuraVengeance;
using StarsAbove.Buffs.Melee.ShadowlessCerulean;
using StarsAbove.Buffs.Melee.SkyStrikerBuffs;
using StarsAbove.Buffs.Melee.Umbra;
using StarsAbove.Buffs.Melee.Unforgotten;
using StarsAbove.Buffs.Misc;
using StarsAbove.Buffs.Other.ArchitectsLuminance;
using StarsAbove.Buffs.Other.DreadmotherDarkIdol;
using StarsAbove.Buffs.Other.Farewells;
using StarsAbove.Buffs.Other.LegendaryShield;
using StarsAbove.Buffs.Other.Nanomachina;
using StarsAbove.Buffs.Other.Phasmasaber;
using StarsAbove.Buffs.Other.Suistrume;
using StarsAbove.Buffs.Ranged.CosmicDestroyer;
using StarsAbove.Buffs.Ranged.CrimsonOutbreak;
using StarsAbove.Buffs.Ranged.Genocide;
using StarsAbove.Buffs.Ranged.InheritedCaseM4A1;
using StarsAbove.Buffs.Ranged.QuisUtDeus;
using StarsAbove.Buffs.Ranged.TwoCrownBow;
using StarsAbove.Buffs.StellarArray;
using StarsAbove.Buffs.StellarNovas;
using StarsAbove.Buffs.Summon.Apalistik;
using StarsAbove.Buffs.Summon.CaesuraOfDespair;
using StarsAbove.Buffs.Summon.Chronoclock;
using StarsAbove.Buffs.Summon.DragaliaFound;
using StarsAbove.Buffs.Summon.Kifrosse;
using StarsAbove.Buffs.Summon.KroniicPrincipality;
using StarsAbove.Buffs.Summon.StarphoenixFunnel;
using StarsAbove.Buffs.Summon.Takonomicon;
using StarsAbove.Buffs.TagDamage;
using StarsAbove.Dusts;
using StarsAbove.Items.Armor.CloakOfAnArbiter;
using StarsAbove.Items.Armor.DraggedBelow;
using StarsAbove.Items.Armor.DreadmotherClaw;
using StarsAbove.Items.Armor.LegendaryShield;
using StarsAbove.Items.Armor.Manifestation;
using StarsAbove.Items.Armor.NeopursuantArmor;
using StarsAbove.Items.Weapons.Celestial;
using StarsAbove.Items.Weapons.Magic;
using StarsAbove.Items.Weapons.Melee;
using StarsAbove.Items.Weapons.Other;
using StarsAbove.Items.Weapons.Summon;
using StarsAbove.Projectiles.Celestial.BuryTheLight;
using StarsAbove.Projectiles.Celestial.IgnitionAstra;
using StarsAbove.Projectiles.Celestial.UltimaThule;
using StarsAbove.Projectiles.Extra;
using StarsAbove.Projectiles.Magic.AegisDriver;
using StarsAbove.Projectiles.Magic.CarianDarkMoon;
using StarsAbove.Projectiles.Magic.CloakOfAnArbiter;
using StarsAbove.Projectiles.Magic.DreamersInkwell;
using StarsAbove.Projectiles.Magic.EyeOfEuthymia;
using StarsAbove.Projectiles.Magic.HunterSymphony;
using StarsAbove.Projectiles.Magic.StygianNymph;
using StarsAbove.Projectiles.Magic.SupremeAuthority;
using StarsAbove.Projectiles.Magic.TwinStars;
using StarsAbove.Projectiles.Magic.VenerationOfButterflies;
using StarsAbove.Projectiles.Magic.VoiceOfTheFallen;
using StarsAbove.Projectiles.Melee.AshenAmbition;
using StarsAbove.Projectiles.Melee.BloodBlade;
using StarsAbove.Projectiles.Melee.ClaimhSolais;
using StarsAbove.Projectiles.Melee.Drachenlance;
using StarsAbove.Projectiles.Melee.Hullwrought;
using StarsAbove.Projectiles.Melee.LiberationBlazing;
using StarsAbove.Projectiles.Melee.Naganadel;
using StarsAbove.Projectiles.Melee.Pigment;
using StarsAbove.Projectiles.Melee.RexLapis;
using StarsAbove.Projectiles.Melee.ShadowlessCerulean;
using StarsAbove.Projectiles.Melee.SkyStriker;
using StarsAbove.Projectiles.Melee.Unforgotten;
using StarsAbove.Projectiles.Melee.Xenoblade;
using StarsAbove.Projectiles.Melee.YunlaiStiletto;
using StarsAbove.Projectiles.Other.Hawkmoon;
using StarsAbove.Projectiles.Ranged.CosmicDestroyer;
using StarsAbove.Projectiles.Ranged.CrimsonOutbreak;
using StarsAbove.Projectiles.Ranged.ForceOfNature;
using StarsAbove.Projectiles.Ranged.Genocide;
using StarsAbove.Projectiles.Ranged.Huckleberry;
using StarsAbove.Projectiles.Ranged.InheritedCaseM4A1;
using StarsAbove.Projectiles.Ranged.IzanagisEdge;
using StarsAbove.Projectiles.Ranged.Tartaglia;
using StarsAbove.Projectiles.StellarNovas;
using StarsAbove.Projectiles.Summon.Apalistik;
using StarsAbove.Projectiles.Summon.Chronoclock;
using StarsAbove.Projectiles.Summon.HollowheartAlbion;
using StarsAbove.Projectiles.Summon.KazimierzSeraphim;
using StarsAbove.Projectiles.Summon.KeyOfTheSinner;
using StarsAbove.Projectiles.Summon.KroniicPrincipality;
using StarsAbove.Projectiles.Summon.PhantomInTheMirror;
using StarsAbove.Projectiles.Summon.Starchild;
using StarsAbove.Projectiles.Summon.StarphoenixFunnel;
using StarsAbove.Projectiles.Summon.Takodachi;
using StarsAbove.Utilities;
using SubworldLibrary;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static Terraria.ModLoader.ModContent;

namespace StarsAbove.Systems
{
    public class WeaponPlayer : ModPlayer
    {
        /* Miscellaneous weapon code (gauges, projectiles, etc.)
         * Also includes accessories.
         * Moved from StarsAbovePlayer because honestly, that file was incredibly bloated.
         * */

        public int WeaponGaugeOffset;

        #region Weapon/Accessory Variables
        //Unused?
        public bool farEdgeOfFateKillDrone;
        public float farEdgeOfFateDronePositionX;
        public float farEdgeOfFateDronePositionY;
        public int starblessedCooldown;
        public bool rexLapisSpear;
        public bool catalyzedWeapon = false;
        public bool enigmaticCatalyst = false;
        public bool celestialFoci = false;
        public bool celesteBlessing = false;
        public int catalystBonus = 0;
        public bool TruesilverSlashing = false;

        //Whisper of the Fallen
        public int whisperShotCount = 0;

        //Izanagi's Edge
        public int izanagiPerfect = 0;
        public bool edgeHoned = false;

        //Two Crown Bow
        public bool twoCrownBowHeld;
        public float terminationGauge;

        public bool dreadmotherHeld;
        public bool dreadmotherMinion;
        public int dreadmotherShieldStacks;
        public float dreadmotherGauge;
        public Vector2 dreadmotherMagicCenter;

        public bool wolvesbaneHeld;
        public float wolvesbaneGauge;

        //Naganadel
        public bool naganadelWeapon1Summoned;
        public bool naganadelWeapon2Summoned;
        public bool naganadelWeapon3Summoned;
        public bool naganadelWeapon4Summoned;
        public bool naganadelWeapon5Summoned;
        public Vector2 naganadelWeaponPosition;


        //Nanomachina
        public int nanomachinaShieldHP;
        public int nanomachinaShieldHPMax;
        public float nanomachinaGauge;

        public bool DragaliaFoundHeld;
        public float DragonshiftGauge;

        //The Kiss of Death
        public bool KissOfDeathHeld;
        public int overdriveGauge;

        //Brilliant Spectrum
        public bool BrilliantSpectrumHeld;
        public float refractionGauge;
        public float refractionGaugeMax = 100;
        public int refractionGaugeTimer = 0;

        //Yunlai Stilletto
        public bool yunlaiTeleport;

        //Phantom In The Mirror
        public bool phantomTeleport;
        public bool phantomKill;
        public Vector2 phantomSavedPosition;

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

        //Trickspin Two-Step
        public bool TrickspinReady = false;
        public Vector2 TrickspinCenter;

        public bool CloakOfAnArbiterHeld;

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

        public bool LegendaryShieldHeld;
        public bool LegendaryShieldEquippedAsAccessory;

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

        public bool RebellionHeld;
        public int rebellionGauge;
        public bool rebellionState1;
        public bool rebellionState2;
        public bool rebellionState3;
        public float rebellionGaugeMaxBuff;//After reaching max Rebellion, this empowers the next Clarent Blood Arthur cast.
        public Vector2 rebellionTarget;

        public bool phasmasaberHeld;

        public bool roguegarbEquipped;
        public bool plasteelEquipped;
        public bool enviroSavantActive;


        //Starphoenix Funnel
        public int alignmentStacks;

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

        //M4A1
        public bool M4A1Held;
        public double M4A1deg;
        public int M4A1UseTimer;
        public List<int> AuxiliaryGuns = new List<int>();
        public List<int> ActiveGuns = new List<int>();

        //Supreme Authority
        public int SupremeAuthorityConsumedNPCs;
        public int SupremeAuthorityEncroachingStacks;

        //Bury The Light
        public bool BuryTheLightHeld = false;
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

        //Gundbits
        public bool GundbitsActive = false;

        //For all guns
        public Vector2 MuzzlePosition;

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

        //SoliloquyOfSovereignSeas
        public bool SoliloquyMinions = false;
        public bool ousiaAligned = false;

        public bool sugarballMinions = false;
        public int sugarballMinionType = 0;//0 is char, 1 is squirt, 2 is bulba

        //Sunset of the Sun God
        public Vector2 karnaTarget;

        //Wavedancer
        public bool wavedancerHeld;
        public Vector2 wavedancerTarget;
        public Vector2 wavedancerPosition;
        public bool WavedancerMinion = false;

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

        //Maniacal Justice
        public int LVStacks = 0;

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

        //ParadiseLost
        public int paradiseLostDrainTimer;
        public bool paradiseLostActive;
        public float paradiseLostAnimationTimer = 0;
        public float paradiseLostAnimationFloat1 = 0;
        public float paradiseLostAnimationFloat2 = 0;


        //Penthesilea's Muse
        public bool paintVisible = false;
        public int chosenColor = 0;//0 = red | 1 = orange | 2 = yellow | 3 = green | 4 = blue | 5 = purple
        public Vector2 inkedFoePosition;
        public bool paintTargetActive;
        public int targetPaintColor;

        //Dreamer's Inkwell
        public bool InkwellHeld;
        public int InkwellInk = 0;
        public float InkwellUIRotation;
        public float InkwellUIAlpha;
        public float InkwellUIAdjustment;
        public int InkwellMana;

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


        //Kevesi and Agnian Farewells
        public bool KevesiFarewellInInventory;
        public bool AgnianFarewellInInventory;

        //Umbra
        public int UmbraGauge;

        //Saltwater Scourge
        public bool SaltwaterScourgeHeld;

        //Dragged Below
        public bool DraggedBelowHeld;
        public int DraggedBelowCorruption;
        public int DraggedBelowCorruptionTimer;
        public bool DraggedBelowInCorruption;

        public Vector2 DraggedBelowPosition1;
        public Vector2 DraggedBelowPosition2;
        public Vector2 DraggedBelowTarget;
        public Vector2 DraggedBelowTarget2;

        //New weapons have a white flash when their gauge is charged.
        public float gaugeChangeAlpha = 0f;
        #endregion
        #region Accessories/Pets
        public bool luciferium;
        public bool Glitterglue;
        public bool DragonwardTalisman;
        public bool AlienCoral;
        public bool ToMurder;
        public bool PerfectlyGenericAccessory;
        public bool GaleflameFeather;
        public bool InertCoating;
        public bool corn;
        public bool LivingDead = false;
        public bool darkHourglass = false;
        public bool willpowerOfZagreus = false;

        //Pets
        public bool Ghost;
        public bool PekoraPet;
        public bool KasumiPet;
        public bool WarriorPet;
        public bool EreshkigalPet;
        public bool MajimaPet;
        public bool VitchPet;
        public bool EspeonPet;
        public bool UmbreonPet;
        public bool BloopPet;
        public bool BubbaPet;
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

        public bool FireflyPet;
        public bool NeuroPet;
        public bool AigisPet;
        #endregion

        public override void SetStaticDefaults()
        {

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            
            if (Glitterglue)
            {
                if (Main.rand.Next(0, 100) > 95)
                {
                    target.AddBuff(BuffType<Glitterglued>(), 240);
                }
            }
            if (sakuraHeld)
            {
                if (!bladeWill)
                {
                    if (Main.rand.Next(15) == 1)
                    {
                        //player.QuickSpawnItem(null,mod.ItemType("RedOrb"));
                        int k = Item.NewItem(Player.GetSource_OnHit(target), (int)target.position.X + Main.rand.Next(-20, 20), (int)target.position.Y + Main.rand.Next(-20, 20), target.width, target.height, Mod.Find<ModItem>("RedOrb").Type, 1, false);
                        if (Main.netMode == 1)
                        {
                            NetMessage.SendData(21, -1, -1, null, k, 1f);
                        }

                    }
                    if (Main.rand.Next(15) == 1)
                    {
                        //player.QuickSpawnItem(null,mod.ItemType("BlueOrb"));
                        int k = Item.NewItem(Player.GetSource_OnHit(target), (int)target.position.X + Main.rand.Next(-20, 20), (int)target.position.Y + Main.rand.Next(-20, 20), target.width, target.height, Mod.Find<ModItem>("BlueOrb").Type, 1, false);
                        if (Main.netMode == 1)
                        {
                            NetMessage.SendData(21, -1, -1, null, k, 1f);
                        }

                    }
                    if (Main.rand.Next(15) == 1)
                    {
                        //player.QuickSpawnItem(null,mod.ItemTypeif("YellowOrb"));
                        int k = Item.NewItem(Player.GetSource_OnHit(target), (int)target.position.X + Main.rand.Next(-20, 20), (int)target.position.Y + Main.rand.Next(-20, 20), target.width, target.height, Mod.Find<ModItem>("YellowOrb").Type, 1, false);
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
                        int k = Item.NewItem(Player.GetSource_OnHit(target), (int)target.position.X + Main.rand.Next(-20, 20), (int)target.position.Y + Main.rand.Next(-20, 20), target.width, target.height, Mod.Find<ModItem>("BladeOrb").Type, 1, false);
                        if (Main.netMode == 1)
                        {
                            NetMessage.SendData(21, -1, -1, null, k, 1f);
                        }

                    }
                }
            }
            if (!target.active && luciferium)
            {
                Player.AddBuff(BuffType<SatedAnguish>(), 900);
            }
            if (!target.active)
            {
                OnKillEnemy(target);
            }
            if (Player.HasBuff(BuffType<RealizedNanomachinaBuff>()))
            {
                if (!Player.HasBuff(BuffType<NanomachinaLeechCooldown>()))
                {
                    Player.statLife += 5;
                    Player.statMana += 5;
                    Player.HealEffect(5);
                    Player.ManaEffect(5);
                    nanomachinaGauge++;
                    Player.AddBuff(BuffType<NanomachinaLeechCooldown>(), 360);
                }
            }

            if (target.HasBuff(BuffType<InfernalBleed>()))
            {
                if (target.life - Math.Min(-(target.life - target.lifeMax) * 0.02, 250) > 1)
                {
                    target.life -= (int)Math.Min(-(target.life - target.lifeMax) * 0.02, 250);
                    Rectangle textPos = new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height);
                    CombatText.NewText(textPos, new Color(234, 0, 0, 100), $"{(int)Math.Min(-(target.life - target.lifeMax) * 0.02, 250)}", false, false);
                }

                Player.AddBuff(BuffID.Rage, 480);
            }
            if (Player.HasBuff(BuffType<SurtrTwilight>()))
            {
                target.AddBuff(BuffID.OnFire, 480);
            }

        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (plasteelEquipped)
            {
                modifiers.CritDamage += 0.08f;
            }
            if (target.HasBuff(BuffType<Glitterglued>()) || Player.HasBuff(BuffType<TimelessPotential>()))
            {
                if(modifiers.DamageType != DamageClass.Summon)
                {
                    if (Main.rand.Next(0, 100) > 70)
                    {
                        modifiers.SetCrit();
                    }
                }
                
            }
            if (Player.HasBuff(BuffType<AmaterasuGrace>()) && target.HasBuff(BuffID.Frostburn))
            {
                modifiers.SourceDamage += 0.5f;
            }
            if (Player.HasBuff(BuffType<DegradedSingularity>()))
            {
                for (int i = 0; i < 12; i++)
                {
                    if (target.buffType[i] > 0 && BuffID.Sets.IsATagBuff[target.buffType[i]] && target.buffType[i] != BuffType<FairyTagDamage>())
                    {
                        modifiers.CritDamage += 0.1f;
                        break;
                    }

                }
            }
            if (Player.HasBuff(BuffType<EmbodiedSingularity>()))
            {
                modifiers.CritDamage += 1.5f;
            }
        }


        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Item, consider using OnHitNPC instead */
        {
            var player = Player.GetModPlayer<StarsAbovePlayer>();

            if (item.ModItem is LiberationBlazing)
            {
                if (Player.HasBuff(BuffType<CoreOfFlames>()))
                {
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_electroSmack, Player.Center);


                    SoundEngine.PlaySound(StarsAboveAudio.SFX_electroSmack, Player.Center);

                    Projectile.NewProjectile(Player.GetSource_ItemUse(item), target.Center.X, target.Center.Y, 0, 0, ProjectileType<ScarletOutburst>(), damageDone, 0, Player.whoAmI, 0f);
                }
            }


            if (Glitterglue)
            {
                if (Main.rand.Next(0, 100) > 95)
                {
                    target.AddBuff(BuffType<Glitterglued>(), 240);
                }
            }
            if (Player.HasBuff(BuffType<AstarteDriver>()) && player.starfarerOutfit == 3)
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, ProjectileType<StarfarerFollowUp>(), damageDone / 3, 0, Player.whoAmI);

            }


            if (!target.active)
            {
                OnKillEnemy(target);
            }
        }
        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Item, consider using ModifyHitNPC instead */
        {
            var player = Player.GetModPlayer<StarsAbovePlayer>();

            if (item.ModItem is ArchitectLuminance && player.MeleeAspect == 2)
            {
                modifiers.CritDamage.Flat += Player.statDefense;
            }

        }
        public override void OnConsumeMana(Item item, int manaConsumed)
        {
            if (euthymiaActive)
            {
                eternityGauge += manaConsumed;
            }

            base.OnConsumeMana(item, manaConsumed);
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the proj, consider using OnHitNPC instead */
        {
            var player = Player.GetModPlayer<StarsAbovePlayer>();
            if (phasmasaberHeld)
            {
                if (hit.Crit && !target.HasBuff(BuffType<SpiritflameDebuff>()))
                {
                    target.AddBuff(BuffID.ShadowFlame, 60 * 3);

                }
                if (Player.HasBuff(BuffType<SpectralIllusionBuff>()))
                {
                    if (target.HasBuff(BuffID.ShadowFlame))
                    {
                        target.AddBuff(BuffType<SpiritflameDebuff>(), Player.buffTime[Player.FindBuffIndex(BuffType<SpectralIllusionBuff>())] + 60);
                        target.DelBuff(target.FindBuffIndex(BuffID.ShadowFlame));
                    }

                }
                else
                {

                }
            }
            if (Player.HasBuff(BuffType<BoilingBloodBuff>()))
            {
                boilingBloodDamage += damageDone / 4;
            }
            if (proj.type == ProjectileType<SteelTempestSwing>())
            {
                soulUnboundDamage += damageDone;
            }
            if (proj.type == ProjectileType<SteelTempestSwing3>())
            {
                target.AddBuff(BuffType<MortalWounds>(), 600);
                soulUnboundDamage += damageDone;
            }
            if (proj.type == ProjectileType<HornSlash>())
            {
                if (Player.HasBuff(BuffType<HunterSymphonyCooldown>()))
                {
                    Player.buffTime[Player.FindBuffIndex(BuffType<HunterSymphonyCooldown>())] -= 120;
                }
            }
            if (proj.type == ProjectileType<ClaimhSolaisSword>())
            {
                if (hit.Crit)
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
            if (proj.type == ProjectileType<SteelTempestSwing2>())
            {
                Rectangle textPos = new Rectangle((int)target.position.X, (int)target.position.Y - 20, target.width, target.height);
                CombatText.NewText(textPos, new Color(255, 30, 30, 240), $"{damageDone / 2}", false, false);
                if (target.life - damageDone / 2 > 1)
                {
                    target.life -= damageDone / 2;
                }
                soulUnboundDamage += damageDone;
            }
            if (proj.type == ProjectileType<SteelTempestSwing4>())
            {
                if (target.HasBuff(BuffType<MortalWounds>()))
                {


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
                soulUnboundDamage += damageDone;
            }
            if (proj.type == ProjectileType<RexLapisMeteor2>())
            {
                if (!target.HasBuff(BuffType<Petrified>()) && hit.Crit)
                {
                    target.AddBuff(BuffType<Petrified>(), 180);
                }
            }
            if (proj.type != ProjectileType<EuthymiaFollowUp>() && euthymiaActive && euthymiaCooldown <= 0)
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_electroSmack, Player.Center);

                Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0, 0, ProjectileType<EuthymiaFollowUp>(), Math.Min(damageDone / 5, 500), 0, Player.whoAmI, 0f);
                euthymiaCooldown = 120 - eternityGauge / 10;

            }
            if (proj.type == ProjectileType<CatalystKey>() && Player.HasBuff(BuffType<AlignmentBuff>()))
            {
                //SoundEngine.PlaySound(StarsAboveAudio.SFX_electroSmack, Player.Center);

                Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0, 0, ProjectileType<StarphoenixFollowUp>(), Math.Min(damageDone / 5, 500), 0, Player.whoAmI, 0f);

            }
            if (proj.type == ProjectileType<WhisperRound>())
            {
                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 113, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                }
                if (hit.Crit)
                {

                    whisperShotCount = 0;
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
            if (proj.type == ProjectileType<HuckleberryRound>() && !target.active)
            {
                Player.statMana += 12;
                Player.AddBuff(BuffID.Wrath, 100);
                for (int d = 0; d < 4; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 113, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default(Color), 1.5f);
                }
            }
            if (proj.type == ProjectileType<SatanaelRound>())
            {
                for (int d = 0; d < 10; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 115, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                }


            }
            if (proj.type == ProjectileType<TwinStarLaser1>() || proj.type == ProjectileType<TwinStarLaser2>())
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
                if (hit.Crit)
                {
                    Player.AddBuff(BuffType<BinaryMagnitude>(), 5);
                }
            }
            if (proj.type == ProjectileType<SkyStrikerRailgunRound>())
            {
                if (hit.Crit)
                {
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
            if (proj.type == ProjectileType<SkyStrikerSwing1>() || proj.type == ProjectileType<SkyStrikerSwing2>())
            {
                if (hit.Crit)
                {
                    if (target.HasBuff(BuffID.OnFire))
                    {
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
            if (proj.type == ProjectileType<AmiyaSwing1>() || proj.type == ProjectileType<AmiyaSwing2>())
            {
                if (!Player.HasBuff(BuffType<Burnout>()))
                {
                    ceruleanFlameGauge += 1;
                    if (hit.Crit)
                    {
                        ceruleanFlameGauge += 4;
                    }

                }
                if (ceruleanFlameGauge >= 100)//If 'Burnout' then no charge
                {
                    ceruleanFlameGauge = 100;
                }
            }
            if (proj.type == ProjectileType<AmiyaSlash>())
            {
                target.AddBuff(BuffID.Frostburn, 1200);
                target.AddBuff(BuffType<Stun>(), 60);

            }
            if (proj.type == ProjectileType<NaganadelProjectileFinal2>())
            {
                Player.AddBuff(BuffID.Swiftness, 420);

                if (hit.Crit)
                {

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
            if (proj.type == ProjectileType<NaganadelProjectileFinal3>())
            {
                target.AddBuff(BuffID.OnFire, 640);

            }
            if (proj.type == ProjectileType<NaganadelProjectileFinal4>())
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
            if (proj.type == ProjectileType<NaganadelProjectileFinal5>())
            {
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 247, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default(Color), 1.5f);
                }


                target.AddBuff(BuffID.Poisoned, 240);

            }
            if (proj.type == ProjectileType<ScarletOutburst>())
            {

                target.AddBuff(BuffID.OnFire, 180);

            }
            if (proj.type == ProjectileType<ForceOfNatureRound>())
            {
                Player.AddBuff(BuffID.Swiftness, 240);
            }
            if (proj.type == ProjectileType<TakodachiRound>())
            {
                takodachiGauge++;
            }
            if (proj.type == ProjectileType<CosmicDestroyerRound>())
            {
                CosmicDestroyerGauge++;
                if (CosmicDestroyerGauge > 100)
                {
                    CosmicDestroyerGauge = 100;
                }

            }
            if (proj.type == ProjectileType<DarkmoonSword>())
            {
                if (hit.Crit)
                {
                    target.AddBuff(BuffID.Frostburn, 180);
                }

            }
            if (proj.type == ProjectileType<yunlaiSwing>() && !target.active)
            {
                Player.statMana += 80;

                for (int d = 0; d < 4; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 113, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default(Color), 1.5f);
                }
            }
            if (proj.type == ProjectileType<SkyStrikerMeleeClaw>())
            {
                if (target.life < target.lifeMax / 2)
                {
                    Player.AddBuff(BuffID.Wrath, 120);
                }

                if (hit.Crit)
                {

                    Vector2 direction = Vector2.Normalize(target.position - Player.Center);
                    Vector2 velocity = direction * 35f;
                    Vector2 targetPosition = Player.Center;


                    Projectile.NewProjectile(Player.GetSource_FromThis(), targetPosition.X, targetPosition.Y, velocity.X, velocity.Y, ProjectileType<SkyStrikerClaw>(), damageDone, 2f, Player.whoAmI, 0, Main.rand.Next(-200, 200) * 0.001f * Player.gravDir);

                }


            }
            if (proj.type == ProjectileType<ignitionAstraSwing>())
            {
                if (!target.active)
                {
                    for (int d = 0; d < 4; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 113, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default(Color), 1.5f);
                    }
                }
                Dust.NewDust(target.position, target.width, target.height, 21, 0f, 0f, 150, default(Color), 1.5f);
                target.AddBuff(BuffType<Starblight>(), 91020);

            }
            if (proj.type == ProjectileType<tartagliaSwing>())
            {
                SoundEngine.PlaySound(SoundID.Splash, target.position);
                Dust.NewDust(target.position, target.width, target.height, 15, 0f, 0f, 150, default(Color), 1.5f);



            }
            if (proj.type == ProjectileType<NaganadelProjectileFinal1>())
            {
                Player.statMana += 20;

                for (int d = 0; d < 8; d++)
                {


                    if (Main.rand.NextBool(3))
                    {
                        Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, 204,
                            proj.velocity.X * .2f + Main.rand.Next(-5, 5), proj.velocity.Y * .2f + Main.rand.Next(-5, 5), 200, Scale: 1.2f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(96, Main.LocalPlayer);

                        dust.velocity += proj.velocity * 0.3f;
                        dust.velocity *= 0.2f;
                    }
                    if (Main.rand.NextBool(4))
                    {
                        Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, 204,
                            0, 0, 254, Scale: 0.3f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(96, Main.LocalPlayer);
                        dust.velocity += proj.velocity * 0.5f;
                        dust.velocity *= 0.5f;
                    }


                }

            }
            if (proj.type == ProjectileType<NaganadelProjectileFinal2>())
            {


                for (int d = 0; d < 8; d++)
                {


                    if (Main.rand.NextBool(3))
                    {
                        Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, 204,
                            proj.velocity.X * .2f + Main.rand.Next(-30, 30), proj.velocity.Y * .2f + Main.rand.Next(-30, 30), 200, Scale: 1.2f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(66, Main.LocalPlayer);

                        dust.velocity += proj.velocity * 0.3f;
                        dust.velocity *= 0.2f;
                    }
                    if (Main.rand.NextBool(4))
                    {
                        Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, 204,
                            0, 0, 254, Scale: 0.3f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(66, Main.LocalPlayer);
                        dust.velocity += proj.velocity * 0.5f;
                        dust.velocity *= 0.5f;
                    }
                }
            }
            if (proj.type == ProjectileType<NaganadelProjectileFinal3>())
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
                        Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, 204,
                            proj.velocity.X * .2f + Main.rand.Next(-30, 30), proj.velocity.Y * .2f + Main.rand.Next(-30, 30), 200, Scale: 1.2f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(122, Main.LocalPlayer);

                        dust.velocity += proj.velocity * 0.3f;
                        dust.velocity *= 0.2f;
                    }
                    if (Main.rand.NextBool(4))
                    {
                        Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, 204,
                            0, 0, 254, Scale: 0.3f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(122, Main.LocalPlayer);
                        dust.velocity += proj.velocity * 0.5f;
                        dust.velocity *= 0.5f;
                    }
                }
            }
            if (proj.type == ProjectileType<NaganadelProjectileFinal4>())
            {

                for (int i = 0; i < 2 + Main.rand.Next(1, 3); i++)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), proj.position.X, proj.position.Y - 800, 0 + Main.rand.Next(-10, 10), 0 + Main.rand.Next(1, 40), ProjectileID.StarWrath, damageDone / 2, 0, Player.whoAmI, 0f);

                }
                for (int d = 0; d < 8; d++)
                {
                    if (Main.rand.NextBool(3))
                    {
                        Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, 204,
                            proj.velocity.X * .2f + Main.rand.Next(-30, 30), proj.velocity.Y * .2f + Main.rand.Next(-30, 30), 200, Scale: 1.2f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);

                        dust.velocity += proj.velocity * 0.3f;
                        dust.velocity *= 0.2f;
                    }
                    if (Main.rand.NextBool(4))
                    {
                        Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, 204,
                            0, 0, 254, Scale: 0.3f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                        dust.velocity += proj.velocity * 0.5f;
                        dust.velocity *= 0.5f;
                    }
                }
            }
            if (proj.type == ProjectileType<NaganadelProjectileFinal5>())
            {
                Player.statMana += 10;
                Player.AddBuff(BuffID.Swiftness, 120);

                Projectile.NewProjectile(Player.GetSource_FromThis(), proj.position.X, proj.position.Y - 800, 0 + Main.rand.Next(-10, 10), 0 + Main.rand.Next(1, 40), ProjectileID.LunarFlare, damageDone / 2, 0, Player.whoAmI, 0f);


                for (int d = 0; d < 8; d++)
                {


                    if (Main.rand.NextBool(3))
                    {
                        Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, 204,
                            proj.velocity.X * .2f + Main.rand.Next(-30, 30), proj.velocity.Y * .2f + Main.rand.Next(-30, 30), 200, Scale: 1.2f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(120, Main.LocalPlayer);

                        dust.velocity += proj.velocity * 0.3f;
                        dust.velocity *= 0.2f;
                    }
                    if (Main.rand.NextBool(4))
                    {
                        Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, 204,
                            0, 0, 254, Scale: 0.3f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(120, Main.LocalPlayer);
                        dust.velocity += proj.velocity * 0.5f;
                        dust.velocity *= 0.5f;
                    }
                }
            }
            if (proj.type == ProjectileType<ButterflyProjectile>())
            {
                for (int d = 0; d < 8; d++)
                {


                    if (Main.rand.NextBool(3))
                    {
                        Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, 204,
                            proj.velocity.X * .2f + Main.rand.Next(-30, 30), proj.velocity.Y * .2f + Main.rand.Next(-30, 30), 200, Scale: 1.2f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);

                        dust.velocity += proj.velocity * 0.3f;
                        dust.velocity *= 0.2f;
                    }
                    if (Main.rand.NextBool(4))
                    {
                        Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, 204,
                            0, 0, 254, Scale: 0.3f);
                        dust.shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                        dust.velocity += proj.velocity * 0.5f;
                        dust.velocity *= 0.5f;
                    }
                }
            }
            if (proj.type == ProjectileType<ClaimhBurst>())
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
            if (proj.type == ProjectileType<StygianSwing1>())
            {
                for (int d = 0; d < 7; d++)
                {
                    Dust dust = Main.dust[Dust.NewDust(target.position, target.width, target.height, 159, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(103, Main.LocalPlayer);


                }
            }
            if (proj.type == ProjectileType<StygianSwing3>())
            {
                if (Main.LocalPlayer.HasBuff(BuffType<ClawsOfNyx>()))
                {
                    for (int d = 0; d < 4; d++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(target.position, target.width, target.height, 219, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f)];
                        //dust.shader = GameShaders.Armor.GetSecondaryShader(103, Main.LocalPlayer);

                    }
                }
                else
                {
                    for (int d = 0; d < 7; d++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(target.position, target.width, target.height, 159, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f)];
                        dust.shader = GameShaders.Armor.GetSecondaryShader(103, Main.LocalPlayer);


                    }
                }
            }
            if (proj.type == ProjectileType<IzanagiRound>())
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
                    Player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;

                    edgeHoned = false;
                }
                else
                {


                }

            }
            if (proj.type == ProjectileType<HullwroughtRound>())
            {

                player.screenShakeTimerGlobal = -80;
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(proj.position, proj.width, proj.height, 0, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default, 1.5f);
                }

                for (int d = 0; d < 26; d++)
                {
                    Dust.NewDust(proj.position, proj.width, proj.height, 133, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 150, default, 1.5f);
                }
                for (int d = 0; d < 30; d++)
                {
                    Dust.NewDust(proj.position, proj.width, proj.height, 7, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default, 1.5f);
                }
                for (int d = 0; d < 40; d++)
                {
                    Dust.NewDust(proj.position, proj.width, proj.height, 269, 0f + Main.rand.Next(-13, 13), 0f + Main.rand.Next(-13, 13), 150, default, 1.5f);
                }
                for (int d = 0; d < 50; d++)
                {
                    Dust.NewDust(proj.position, proj.width, proj.height, 78, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default, 1.5f);
                }

                // Play explosion sound
                SoundEngine.PlaySound(SoundID.Item89);
                // Smoke Dust spawn
                for (int i = 0; i < 70; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(proj.position.X, proj.position.Y), proj.width, proj.height, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }
                // Fire Dust spawn
                for (int i = 0; i < 80; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(proj.position.X, proj.position.Y), proj.width, proj.height, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 3f);
                    Main.dust[dustIndex].noGravity = true;
                    Main.dust[dustIndex].velocity *= 5f;
                    dustIndex = Dust.NewDust(new Vector2(proj.position.X, proj.position.Y), proj.width, proj.height, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
                    Main.dust[dustIndex].velocity *= 3f;
                }
                // Large Smoke Gore spawn
                for (int g = 0; g < 4; g++)
                {
                    int goreIndex = Gore.NewGore(null, new Vector2(proj.position.X + proj.width / 2 - 24f, proj.position.Y + proj.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(proj.position.X + proj.width / 2 - 24f, proj.position.Y + proj.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(proj.position.X + proj.width / 2 - 24f, proj.position.Y + proj.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(proj.position.X + proj.width / 2 - 24f, proj.position.Y + proj.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                }

            }
            if (proj.type == ProjectileType<DrachenlanceProjectile>())
            {
                if (Player.HasBuff(BuffType<BloodOfTheDragon>()))
                {
                    //Main.LocalPlayer.velocity = Main.LocalPlayer.velocity * -1;
                    for (int d = 0; d < 15; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 21, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                    }


                }
                if (Main.LocalPlayer.HasBuff(BuffType<LifeOfTheDragon>()))
                {
                    //Main.LocalPlayer.velocity = Main.LocalPlayer.velocity * -1;
                    //Main.LocalPlayer.statLife += 50;
                    for (int d = 0; d < 15; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, 258, 0f + Main.rand.Next(-20, 20), 0f + Main.rand.Next(-20, 20), 150, default(Color), 1.5f);
                    }

                }
            }
            if (proj.type == ProjectileType<DarkmoonSwordEmpowered>())
            {

                for (int d = 0; d < 6; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 21, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.9f);
                }



            }
            if (proj.type == ProjectileType<ApalistikProjectile>() || proj.type == ProjectileType<ApalistikUpgradedProjectile>())
            {

                target.AddBuff(BuffType<Riptide>(), 240);


                if (target.HasBuff(BuffType<OceanCulling>()))
                {
                    for (int i = 0; i < 5; i++)
                    {

                        Vector2 vel = new Vector2(Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-4, 4));
                        Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center, vel, ProjectileType<Bubble>(), damageDone / 8, 3, Player.whoAmI, 0, 1);
                    }

                    int index = target.FindBuffIndex(BuffType<OceanCulling>());
                    if (index > -1)
                    {
                        target.DelBuff(index);
                    }
                }
            }
            if (proj.type == ProjectileType<UltimaPlanet1>() || proj.type == ProjectileType<UltimaPlanet2>() || proj.type == ProjectileType<UltimaPlanet3>() || proj.type == ProjectileType<UltimaPlanet4>() || proj.type == ProjectileType<UltimaPlanet5>())
            {
                Player.AddBuff(BuffType<UniversalManipulation>(), 720);

                for (int i = 0; i < 3; i++)
                {

                    Vector2 vel = new Vector2(Main.rand.NextFloat(-4, 4), Main.rand.NextFloat(-4, 4));
                    Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center, vel, ProjectileType<Asteroid>(), damageDone / 4, 3, Player.whoAmI, 0, 1);
                }

            }
            if (proj.type == ProjectileType<UltimaSwing1>())
            {
                if (Player.HasBuff(BuffType<UniversalManipulation>()))
                {
                    int index = Player.FindBuffIndex(BuffType<UniversalManipulation>());
                    if (index > -1)
                    {
                        Player.DelBuff(index);
                    }
                    Player.AddBuff(BuffType<CelestialCacophony>(), 720);
                }
                //Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center.X, target.Center.Y, 0, 0, ProjectileType<UltimaFollowUp>(), 0, 0, Player.whoAmI, 0f);
            }
            if (proj.type == ProjectileType<BlueStarBit>())
            {
                Player.statMana += 10;
                Player.ManaEffect(10);

            }
            if (proj.type == ProjectileType<OrangeStarBit>())
            {
                target.AddBuff(BuffID.OnFire, 240);

            }
            if (proj.type == ProjectileType<PurpleStarBit>())
            {
                target.AddBuff(BuffType<Starblight>(), 240);

            }
            if (proj.type == ProjectileType<PhantomInTheMirrorProjectile>())
            {
                target.AddBuff(BuffType<PhantomTagDamage>(), 240);
                target.AddBuff(BuffID.Frostburn, 120);

            }
            if (proj.type == ProjectileType<BuryTheLightSlash>())
            {


            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Projectile, consider using ModifyHitNPC instead */
        {
            

            if (target.HasBuff(BuffType<IrysGaze>()))
            {
                modifiers.FlatBonusDamage += 50;
                if (proj.minion)
                {
                    int uniqueCrit = Main.rand.Next(100);
                    if (uniqueCrit <= 15)
                    {
                        modifiers.SetCrit();

                    }

                }
            }
            if (proj.type == ProjectileType<AshenAmbitionExecute>())
            {
                if (target.life <= AshenAmbitionExecuteThreshold)
                {
                    modifiers.SetInstantKill();
                    AshenExecuteKilled = true;

                }
            }


            if (proj.type == ProjectileType<HawkmoonRound>())
            {
                modifiers.SetCrit();
                modifiers.FlatBonusDamage += 10;
            }
            if (proj.type == ProjectileType<KazimierzSeraphimProjectile>())
            {
                if (radiance > 0)
                {
                    if (radiance >= 5)
                    {
                        modifiers.SetCrit();

                    }
                    modifiers.FlatBonusDamage += radiance * 10;

                    for (int d = 0; d < 12; d++)
                    {
                        Dust.NewDust(target.position, target.width, target.height, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default(Color), 1.5f);
                    }
                }
                radiance = 0;

            }
            if (proj.type == ProjectileType<TemporalTimepiece2>() || proj.type == ProjectileType<TemporalTimepiece3>())
            {
                if (powderGauge >= 80)
                {
                    modifiers.FlatBonusDamage += 60;
                }
                powderGauge += 5;

            }
            if (proj.type == ProjectileType<SteelTempestSwing>())
            {
                if (Main.rand.Next(0, 101) >= 50)
                {
                    modifiers.SetCrit();
                }
                else
                {
                    modifiers.DisableCrit();
                }

            }
            if (proj.type == ProjectileType<SteelTempestSwing2>())
            {
                modifiers.DisableCrit();
            }
            if (proj.type == ProjectileType<SteelTempestSwing4>())
            {
                if (target.HasBuff(BuffType<MortalWounds>()))
                {
                    modifiers.SourceDamage *= 10;
                    modifiers.SetCrit();
                }
            }


            if (proj.type == ProjectileType<WhisperRound>())
            {
                modifiers.CritDamage /= 2;  //remove vanilla 2x bonus
                modifiers.CritDamage += 15f; //crank that baby up

            }


            if (proj.type == ProjectileType<TakonomiconLaser>())
            {
                modifiers.SourceDamage /= 3;
                takodachiGauge += 3;


            }
            if (proj.type == ProjectileType<TwinStarLaser1>() || proj.type == ProjectileType<TwinStarLaser2>())
            {

                if (Player.statMana > 250)
                {
                    modifiers.SourceDamage += 1.5f;
                }
                modifiers.SourceDamage.Flat += Player.statManaMax2 / 8;

            }

            if (proj.type == ProjectileType<IzanagiRound>())
            {
                if (edgeHoned)
                {
                    modifiers.SourceDamage /= 2;  //remove vanilla 2x bonus
                    modifiers.SourceDamage += 5f; //that's a lot of damage
                    if (Player.GetModPlayer<StarsAbovePlayer>().MeleeAspect != 2)
                    {
                        Player.statMana += 100;
                    }
                }
                else
                {

                    modifiers.CritDamage += 2f;
                }

            }
            if (proj.type == ProjectileType<AmiyaSlashBurst>())
            {
                modifiers.SetCrit();


            }
            if (proj.type == ProjectileType<HullwroughtRound>())
            {
                if (savedHullwroughtShot >= 5)
                {
                    modifiers.SetCrit();
                    target.AddBuff(BuffType<Stun>(), 20);
                }
                modifiers.SourceDamage.Flat += savedHullwroughtShot * 180;


            }
            if (proj.type == ProjectileType<BloodSlash1>() || proj.type == ProjectileType<BloodSlash2>() || proj.type == ProjectileType<BladeArtDragon>())
            {
                if (Player.statLife < Player.statLifeMax2)
                {
                    modifiers.CritDamage *= -(Player.statLife / Player.statLifeMax2) + 1;
                }



            }


            if (proj.type == ProjectileType<DarkmoonSwordEmpowered>())
            {

                target.AddBuff(BuffID.Frostburn, 180);


            }

            if (proj.type == ProjectileType<CosmicDestroyerRound2>())
            {
                if (target.life < target.lifeMax / 2)
                {
                    modifiers.SetCrit();
                }


            }
            if (proj.type == ProjectileType<SkyStrikerRailgunRound>())
            {

                modifiers.CritDamage += 2f;




            }
            if (proj.type == ProjectileType<SkyStrikerSwing1>() || proj.type == ProjectileType<SkyStrikerSwing2>())
            {
                modifiers.CritDamage.Flat += 50;

            }

            if (proj.type == ProjectileType<AmiyaSwingE1>() || proj.type == ProjectileType<AmiyaSwingE2>())
            {
                modifiers.SetCrit();

                //target.AddBuff(BuffType<Buffs.Stun>(), 120);

            }

            if (proj.type == ProjectileType<OutbreakRound>())
            {

                if (target.HasBuff(BuffType<NanitePlague>()))
                {
                    target.GetGlobalNPC<StarsAboveGlobalNPC>().NanitePlagueLevel++;
                    modifiers.SourceDamage.Flat += target.GetGlobalNPC<StarsAboveGlobalNPC>().NanitePlagueLevel;
                    modifiers.CritDamage /= 2;  //remove vanilla 2x bonus
                    modifiers.CritDamage += 3f; //crank that baby up


                }
                else
                {
                    target.AddBuff(BuffType<NanitePlague>(), 360);


                }

            }
            if (proj.type == ProjectileType<NaganadelProjectileFinal1>())
            {
                modifiers.SourceDamage /= 2;
                for (int d = 0; d < 15; d++)
                {
                    Dust.NewDust(target.position, target.width, target.height, 172, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                }

            }
            if (proj.type == ProjectileType<NaganadelProjectileFinal2>())
            {

                modifiers.CritDamage /= 2;  //remove vanilla 2x bonus
                modifiers.CritDamage += 6f; //crank that baby up

            }
            if (proj.type == ProjectileType<NaganadelProjectileFinal3>())
            {
                if (target.HasBuff(BuffID.OnFire))
                {
                    modifiers.SourceDamage *= 2;
                }
            }
            if (proj.type == ProjectileType<NaganadelProjectileFinal5>())
            {

                modifiers.CritDamage /= 2;  //remove vanilla 2x bonus
                modifiers.CritDamage += 3f; //crank that baby up

            }


            if (proj.type == ProjectileType<ButterflyProjectile>())
            {
                if (target.HasBuff(BuffID.Confused))
                {

                    modifiers.SourceDamage += 12;

                }
                target.AddBuff(BuffID.Confused, 340);

            }
            if (proj.type == ProjectileType<MelusineBeam>())
            {
                if (target.HasBuff(BuffID.Frostburn))
                {
                    modifiers.SourceDamage.Flat += 50;
                    modifiers.SetCrit();
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
                        int dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 3f);
                        Main.dust[dustIndex].noGravity = true;
                        Main.dust[dustIndex].velocity *= 5f;
                        dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 6, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
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
                    int dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }

                // Large Smoke Gore spawn
                for (int g = 0; g < 2; g++)
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
            if (proj.type == ProjectileType<ArondightBeam>())
            {
                if (target.HasBuff(BuffID.OnFire))
                {
                    modifiers.SourceDamage.Flat += 50;
                    modifiers.SetCrit();
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
                        int dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 20, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 3f);
                        Main.dust[dustIndex].noGravity = true;
                        Main.dust[dustIndex].velocity *= 5f;
                        dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 20, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
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
                    int dustIndex = Dust.NewDust(new Vector2(target.Center.X, target.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }
                // Fire Dust spawn

                // Large Smoke Gore spawn
                for (int g = 0; g < 2; g++)
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
            if (proj.type == ProjectileType<MonadoEmpoweredCritSwing>())
            {

                modifiers.SetCrit();

            }
            if (proj.type == ProjectileType<RiptideBolt>())
            {

                target.AddBuff(BuffType<Riptide>(), 720);

            }
            if (proj.type == ProjectileType<IzanagiRound>())
            {
                if (edgeHoned)
                {

                }
                else
                {
                    izanagiPerfect += 1;

                }

            }
            if (proj.type == ProjectileType<Bubble>())
            {
                if (target.HasBuff(BuffType<Riptide>()))
                {
                    modifiers.SetCrit();
                }
                else
                {
                    if (Main.rand.Next(0, 101) <= 30)//
                    {
                        modifiers.SetCrit();
                    }

                }

            }
            if (proj.type == ProjectileType<Starchild>() && target.type != NPCID.TargetDummy)
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
            if (proj.minion && proj.type != ProjectileType<ApalistikProjectile>() && proj.type != ProjectileType<ApalistikUpgradedProjectile>() && (Main.LocalPlayer.HeldItem.ModItem is Apalistik || Main.LocalPlayer.HeldItem.ModItem is ApalistikUpgraded))
            {
                if (Main.rand.Next(0, 101) <= 10 && !Player.HasBuff(BuffType<ComboCooldown>()))//
                {
                    target.AddBuff(BuffType<OceanCulling>(), 240);
                    Player.AddBuff(BuffType<ComboCooldown>(), 60);
                }

            }
            if (proj.type == ProjectileType<StygianSwing1>())
            {
                //duality += 10;

            }
            if (proj.type == ProjectileType<PaintSwingR>())
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center, Vector2.Zero, ProjectileType<SplatterRed>(), 0, 0, Player.whoAmI, 0, 1);
                target.AddBuff(BuffID.OnFire, 240);
                if (target.HasBuff(BuffType<RedPaint>()))
                {
                    modifiers.SetCrit();
                }
                else
                {
                    if (target.HasBuff(BuffType<GreenPaint>()))
                    {
                        modifiers.SourceDamage /= 3;

                    }
                    else
                    {
                        modifiers.SourceDamage /= 2;

                    }
                }

            }
            if (proj.type == ProjectileType<PaintSwingO>())
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center, Vector2.Zero, ProjectileType<SplatterOrange>(), 0, 0, Player.whoAmI, 0, 1);
                target.AddBuff(BuffID.Ichor, 240);
                if (target.HasBuff(BuffType<OrangePaint>()))
                {
                    modifiers.SetCrit();
                }
                else
                {
                    if (target.HasBuff(BuffType<BluePaint>()))
                    {
                        modifiers.SourceDamage /= 3;

                    }
                    else
                    {
                        modifiers.SourceDamage /= 2;

                    }
                }
            }
            if (proj.type == ProjectileType<PaintSwingY>())
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center, Vector2.Zero, ProjectileType<SplatterYellow>(), 0, 0, Player.whoAmI, 0, 1);
                target.AddBuff(BuffID.Midas, 240);
                if (target.HasBuff(BuffType<YellowPaint>()))
                {
                    modifiers.SetCrit();
                }
                else
                {
                    if (target.HasBuff(BuffType<PurplePaint>()))
                    {
                        modifiers.SourceDamage /= 3;

                    }
                    else
                    {
                        modifiers.SourceDamage /= 2;

                    }
                }
            }
            if (proj.type == ProjectileType<PaintSwingG>())
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center, Vector2.Zero, ProjectileType<SplatterGreen>(), 0, 0, Player.whoAmI, 0, 1);
                target.AddBuff(BuffID.CursedInferno, 240);
                if (target.HasBuff(BuffType<GreenPaint>()))
                {
                    modifiers.SetCrit();
                }
                else
                {
                    if (target.HasBuff(BuffType<RedPaint>()))
                    {
                        modifiers.SourceDamage /= 3;

                    }
                    else
                    {
                        modifiers.SourceDamage /= 2;

                    }
                }
            }
            if (proj.type == ProjectileType<PaintSwingB>())
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center, Vector2.Zero, ProjectileType<SplatterBlue>(), 0, 0, Player.whoAmI, 0, 1);
                target.AddBuff(BuffID.Frostburn, 240);
                if (target.HasBuff(BuffType<BluePaint>()))
                {
                    modifiers.SetCrit();
                }
                else
                {
                    if (target.HasBuff(BuffType<OrangePaint>()))
                    {
                        modifiers.SourceDamage /= 3;

                    }
                    else
                    {
                        modifiers.SourceDamage /= 2;

                    }

                }
            }
            if (proj.type == ProjectileType<PaintSwingP>())
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), target.Center, Vector2.Zero, ProjectileType<SplatterPurple>(), 0, 0, Player.whoAmI, 0, 1);
                target.AddBuff(BuffID.Venom, 240);
                if (target.HasBuff(BuffType<PurplePaint>()))
                {
                    modifiers.SetCrit();
                }
                else
                {
                    if (target.HasBuff(BuffType<YellowPaint>()))
                    {
                        modifiers.SourceDamage /= 3;

                    }
                    else
                    {
                        modifiers.SourceDamage /= 2;

                    }
                }
            }
            if (proj.minion && proj.type != ProjectileType<ApalistikProjectile>() && proj.type != ProjectileType<ApalistikUpgradedProjectile>())
            {
                if (target.HasBuff(BuffType<Riptide>()))
                {

                    modifiers.SourceDamage += 0.3f;

                    for (int d = 0; d < 30; d++)
                    {
                        Dust.NewDust(target.Center, 0, 0, 15, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 28; d++)
                    {
                        Dust.NewDust(target.Center, 0, 0, DustType<bubble>(), 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-15, 15), 0, default(Color), 1.5f);
                    }

                    int index = target.FindBuffIndex(BuffType<Riptide>());
                    if (index > -1)
                    {
                        target.DelBuff(index);
                    }
                }

            }
            if (proj.type == ProjectileType<ApalistikProjectile>() || proj.type == ProjectileType<ApalistikUpgradedProjectile>())
            {
                if (target.HasBuff(BuffType<OceanCulling>()))
                {
                    modifiers.SourceDamage += 0.5f;
                }
            }
            if (proj.type == ProjectileType<UltimaPlanet1>() || proj.type == ProjectileType<UltimaPlanet2>() || proj.type == ProjectileType<UltimaPlanet3>() || proj.type == ProjectileType<UltimaPlanet4>() || proj.type == ProjectileType<UltimaPlanet5>())
            {
                modifiers.SetCrit();

            }

            if (proj.type == ProjectileType<tartagliaSwing>())
            {
                if (target.HasBuff(BuffType<Riptide>()))
                {
                    modifiers.SourceDamage.Flat += 90;
                }

            }
            if (proj.type == ProjectileType<Starchild>())
            {
                modifiers.SourceDamage.Flat += Player.statDefense;

            }
            if (proj.type == ProjectileType<ClaimhBurst>())
            {
                modifiers.SetCrit();
                modifiers.SourceDamage.Flat += Player.statDefense * 20 + radiance * 50;

            }
            if (proj.type == ProjectileType<YellowStarBit>())
            {
                target.AddBuff(BuffType<Stun>(), 60);

            }
            if (proj.type == ProjectileType<GreenStarBit>())
            {
                if (Main.rand.Next(0, 101) <= 30)
                {
                    modifiers.SetCrit();
                }

            }

            if (proj.type == ProjectileType<BloodstainedCrescent>())
            {

                if (target.HasBuff(BuffID.Frostburn))
                {
                    Player.statMana += 90;
                    modifiers.SourceDamage.Flat += 200;
                    int index = target.FindBuffIndex(BuffID.Frostburn);
                    if (index > -1)
                    {
                        target.DelBuff(index);
                    }
                }



            }
            if (proj.type == ProjectileType<BuryTheLightSlash>())
            {

            }
            if (proj.type == ProjectileType<BuryTheLightSlash2>())
            {
                if (proj.owner == Player.whoAmI)
                {
                    if (!target.boss && target.CanBeChasedBy())
                    {
                        for (int d = 0; d < 26; d++)
                        {
                            Dust.NewDust(target.Center, 0, 0, DustID.Electric, 0f + Main.rand.Next(-16, 16), 0f + Main.rand.Next(-16, 16), 150, default(Color), 1.5f);
                        }
                        modifiers.SetInstantKill();
                    }
                    else
                    {
                        modifiers.SetCrit();
                        modifiers.SourceDamage += 100f;
                        modifiers.FinalDamage.Flat += (int)(target.lifeMax * 0.1);
                    }

                }
            }

            if (proj.type == ProjectileType<AegisDriverOn>())
            {

                if (target.HasBuff(BuffID.OnFire))
                {
                    target.AddBuff(BuffID.OnFire, 640);
                    modifiers.SourceDamage.Flat += 5;

                }
                target.AddBuff(BuffID.OnFire, 640);

                if (aegisGauge >= 100)
                {
                    modifiers.SourceDamage += 1;
                    modifiers.SetCrit();
                    aegisGauge = 0;
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

            }
            if (proj.type == ProjectileType<GenocideArtillery>() || proj.type == ProjectileType<GenocideRound>() || proj.type == ProjectileType<GenocideRoundBlast>() || proj.type == ProjectileType<GenocideRoundFinalBlast>() || proj.type == ProjectileType<GenocideArtilleryBlast>())
            {

                if (target.HasBuff(BuffType<MortalWounds>()))
                {

                    modifiers.SourceDamage += 0.5f;

                }



            }
            if (proj.type == ProjectileType<GenocideRound>() || proj.type == ProjectileType<GenocideRoundBlast>() || proj.type == ProjectileType<GenocideRoundFinalBlast>())
            {
                Player.AddBuff(BuffType<GenocideBuff>(), 240);
            }


        }

        public override void PostUpdateRunSpeeds()
        {
            if (Player.HasBuff(BuffType<ParadiseLostBuff>()))
            {
                Player.maxRunSpeed *= 0.2f;
                Player.accRunSpeed *= 0.2f;
            }
            if (BrilliantSpectrumHeld)
            {
                Player.maxRunSpeed *= refractionGauge / refractionGaugeMax;
                Player.accRunSpeed *= refractionGauge / refractionGaugeMax;
            }
            if (Player.HasBuff(BuffType<SpectrumAbsorption>()))
            {
                Player.maxRunSpeed *= 0.8f;
                Player.accRunSpeed *= 0.8f;
            }
            if (Player.HasBuff(BuffType<Mortality>()))
            {
                Player.maxRunSpeed *= 0.9f;
                Player.accRunSpeed *= 0.9f;
            }
            if (Player.HasBuff(BuffType<VitalitySong>()))
            {
                Player.maxRunSpeed *= 1.15f;
                //Player.runAcceleration += 1.15f;
            }
            if (Player.HasBuff(BuffType<Alacrity>()))
            {
                Player.maxRunSpeed *= 1.1f;
                Player.accRunSpeed *= 1.1f;
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
            if (Player.HasBuff(BuffType<SakuraHeavenBuff>()))
            {
                Player.maxRunSpeed *= 1.20f;
                Player.accRunSpeed *= 1.20f;
            }
            if (Player.HasBuff(BuffType<ElementalChaos>()))
            {
                Player.maxRunSpeed *= 1.20f;
                Player.accRunSpeed *= 1.20f;
            }
            base.PostUpdateRunSpeeds();
        }
        public override void ModifyScreenPosition()
        {
            var player = Player.GetModPlayer<StarsAbovePlayer>();

            if (judgementCutTimer < 0 && judgementCutTimer > -100)
            {
                Main.screenPosition += new Vector2(Main.rand.Next(-player.screenShakeVelocity / 100, player.screenShakeVelocity / 100), Main.rand.Next(-player.screenShakeVelocity / 100, player.screenShakeVelocity / 100));
                if (player.screenShakeVelocity >= 100)
                {
                    player.screenShakeVelocity -= 10;

                }
            }
            else
            {
                player.screenShakeVelocity = 1000;

            }

            base.ModifyScreenPosition();
        }
        public override void PreUpdateBuffs()
        {
            var player = Player.GetModPlayer<StarsAbovePlayer>();
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
                SymphonySongsPlayed++;
                SoundEngine.PlaySound(StarsAboveAudio.SFX_HuntingHornBasic, Player.Center);
                Player.AddBuff(BuffType<HunterSymphonyCooldown>(), 1200);
                HunterSongPlaying = 0;
            }


            BuryTheLight();

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

            if (judgementGauge > 100)
            {
                judgementGauge = 100;
            }

            if (celestialFoci)
            {

                Player.respawnTimer = 480;//8 seconds



            }

            //foreach (Player castPlayer in Main.player)
            //{ 
            Suistrume();
            if (Main.LocalPlayer.HasBuff(BuffType<ButterflyTrance>()))
            {

                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.position;
                int playerWidth = Main.LocalPlayer.width;
                int playerHeight = Main.LocalPlayer.height;

                Dust.NewDust(position, playerWidth, playerHeight, 164, 0f, 0f, 150, default, 1.5f);




            }
            if (Main.LocalPlayer.HasBuff(BuffType<ButterflyTrance>()))
            {

                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.position;
                int playerWidth = Main.LocalPlayer.width;
                int playerHeight = Main.LocalPlayer.height;

                Dust.NewDust(position, playerWidth, playerHeight, 164, 0f, 0f, 150, default, 1.5f);




            }

            if (Main.LocalPlayer.HasBuff(BuffType<StellarListener>()))
            {

                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.position;
                int playerWidth = Main.LocalPlayer.width;
                int playerHeight = Main.LocalPlayer.height;

                Dust.NewDust(position, playerWidth, playerHeight, 206, 0f, 0f, 150, default, 1.5f);
                if (!(Player.velocity == Vector2.Zero))
                {
                    if (Main.rand.NextBool(5))
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, DustType<MusicNote>(), 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default, 1.5f);
                    }
                }



            }
            if (Main.LocalPlayer.HasBuff(BuffType<StellarPerformanceCooldown>()))
            {
                stellarPerformanceCooldown = true;

            }
            else
            {
                stellarPerformanceCooldown = false;

            }

            if (Main.LocalPlayer.HasBuff(BuffType<BloodOfTheDragon>()))
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


            if (Main.LocalPlayer.HasBuff(BuffType<LifeOfTheDragon>()))
            {

                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Main.LocalPlayer.position;
                int playerWidth = Main.LocalPlayer.width;
                int playerHeight = Main.LocalPlayer.height;
                for (int d = 0; d < 5; d++)
                {
                    Dust.NewDust(position, playerWidth, playerHeight, 258, 0f, 0f, 150, default, 1.5f);
                }


            }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<LimitBreak>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<LimitBreakCooldown>(), 3600);


                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<CallOfTheStarsBuff>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<CallOfTheStarsCooldown>(), 60 * 180);


                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<DeifiedBuff>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        for (int ib = 0; ib < Main.maxNPCs; ++ib)
                        {
                            NPC npc = Main.npc[ib];
                            if (npc.active && npc.boss)
                            {
                                Player.AddBuff(BuffType<Mortality>(), 60 * 15);

                            }
                        }


                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<SpecialAttackBuff>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<ManiacalJusticeCooldown>(), 60 * 30);


                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<CosmicConception>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<CosmicConceptionCooldown>(), 7200);


                    }
                }

            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Voidform>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Vector2 placement2 = new Vector2(Player.Center.X, Player.Center.Y);
                        Projectile.NewProjectile(Player.GetSource_FromThis(), placement2.X, placement2.Y, 0, 0, ProjectileType<radiate>(), 0, 0f, 0);
                        Player.AddBuff(BuffType<CosmicRecoil>(), 60);
                        player.screenShakeTimerGlobal = 0;
                        for (int i2 = 0; i2 < 70; i2++)
                        {

                            Vector2 vel = new Vector2(Main.rand.NextFloat(-9, 9), Main.rand.NextFloat(-9, 9));
                            int type = Main.rand.Next(new int[] { ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });

                            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, vel, type, Player.GetWeaponDamage(Player.HeldItem), 6, Player.whoAmI, 0, 1);
                        }


                    }
                }

            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<WrathfulCeruleanFlame>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<Burnout>(), 1200);


                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<StellarTerminationPreBuff>())
                {
                    terminationGauge -= 2;
                    if (terminationGauge < 0)
                    {
                        terminationGauge = 0;
                    }
                    for (int ia = 0; ia < 2; ia++)
                    {
                        Vector2 vector = new Vector2(
                            Main.rand.Next(-2048, 2048) * (0.003f * 56) - 10,
                            Main.rand.Next(-2048, 2048) * (0.003f * 56) - 10);
                        Dust d = Main.dust[Dust.NewDust(
                            Player.MountedCenter + vector, 1, 1,
                            DustID.GemAmethyst, 0, 0, 255,
                            new Color(0.8f, 0.4f, 1f), 1f)];
                        d.velocity = -vector / 16;
                        d.velocity -= Player.velocity / 8;
                        d.noGravity = true;

                    }
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<StellarTerminationBuff>(), 120);
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<StellarTerminationBuff>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        for (int j = 0; j < Main.maxNPCs; j++)
                        {
                            NPC npc = Main.npc[j];
                            if (npc.active && npc.HasBuff(BuffType<StellarTerminationBuff>()))
                            {
                                for (int d = 0; d < 50; d++)
                                {
                                    Dust.NewDust(npc.Center, 0, 0, DustID.GemAmethyst, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default, 1f);

                                    Dust.NewDust(npc.Center, 0, 0, DustID.Smoke, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default, 0.5f);
                                }
                                for (int d = 0; d < 30; d++)
                                {
                                    Dust.NewDust(npc.Center, 0, 0, DustID.FireworkFountain_Pink, 0f + Main.rand.Next(-9, 9), 0f + Main.rand.Next(-9, 9), 150, default, 0.8f);

                                }

                                npc.SimpleStrikeNPC(Player.GetWeaponDamage(Player.HeldItem) * 3, 0, false, 0, DamageClass.Ranged, true, 0);
                                npc.DelBuff(npc.FindBuffIndex(BuffType<StellarTerminationBuff>()));
                            }
                        }
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Afterburner>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<AfterburnerCooldown>(), 1500);


                    }
                }

            if (Player.HasBuff(BuffType<LimitBreak>()))
            {
                Vector2 position = Main.LocalPlayer.position;
                int playerWidth = Main.LocalPlayer.width;
                int playerHeight = Main.LocalPlayer.height;

                for (int d = 0; d < 5; d++)
                {

                    Dust.NewDust(position, playerWidth, playerHeight, 206, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 150, default, 1.5f);

                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.

                    dust = Main.dust[Dust.NewDust(position, playerWidth, playerHeight, 206, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 0, new Color(255, 255, 255), 1f)];
                    dust.shader = GameShaders.Armor.GetSecondaryShader(81, Main.LocalPlayer);

                }
                if (Player.statMana < Player.statManaMax2)
                {
                    Player.statMana += 2;
                    Player.statLife -= 2;
                    if (Player.statLife <= 0)
                    {
                        if (!Main.LocalPlayer.HasBuff(BuffType<LivingDead>()))
                        {
                            for (int d = 0; d < 50; d++)
                            {
                                Dust dust;
                                Dust.NewDust(position, playerWidth, playerHeight, 106, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default, 1.5f);
                                dust = Main.dust[Dust.NewDust(position, playerWidth, playerHeight, 206, 0f + Main.rand.Next(-2, 2), 0f + Main.rand.Next(-2, 2), 0, new Color(255, 255, 255), 1f)];
                                dust.shader = GameShaders.Armor.GetSecondaryShader(81, Player);

                            }
                        }
                        Player.KillMe(PlayerDeathReason.ByCustomReason(Player.name + "'s body was broken, along with their limits."), 500, 0);




                    }
                }



            }

            if (Player.HasBuff(BuffType<FlashOfEternity>()) && Player.whoAmI == Main.myPlayer)
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
                            DustType<Butterfly>(), 0, 0, 255,
                            new Color(1f, 1f, 1f), 1.5f)];
                        // d.shader = GameShaders.Armor.GetSecondaryShader(77, Main.LocalPlayer);
                        d.velocity = -vector / 16;
                        d.velocity -= Player.velocity / 8;
                        d.noLight = true;
                        d.noGravity = true;
                    }

                }

            }
            if (Player.HasBuff(BuffType<ShadowWallBuff>()) && Player.whoAmI == Main.myPlayer)
            {
                float dustAmount = 15f;
                for (int i = 0; (float)i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                    spinningpoint5 = spinningpoint5.RotatedBy(Player.velocity.ToRotation());
                    int dust = Dust.NewDust(Player.Center, 0, 0, DustID.Shadowflame);
                    Main.dust[dust].scale = 0.9f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = Player.Center + spinningpoint5;
                    Main.dust[dust].velocity = Player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
                }

            }

            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<EyeOfEuthymiaBuff>())
                {
                    if (Player.buffTime[i] == 600)
                    {
                        Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                        CombatText.NewText(textPos, new Color(255, 0, 125, 240), "The Eye of Euthymia has 10 seconds left!", false, false);
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<CoreOfFlames>())
                {
                    if (Player.buffTime[i] == 300)
                    {
                        Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                        CombatText.NewText(textPos, new Color(255, 255, 125, 240), "The Core of Flames has 5 seconds left!", false, false);
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<BoilingBloodBuff>())
                {
                    if (Player.buffTime[i] == 300)
                    {
                        Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                        CombatText.NewText(textPos, new Color(255, 255, 125, 240), "Boiling Blood has 5 seconds left!", false, false);
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<CoreOfFlamesCooldown>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                        CombatText.NewText(textPos, new Color(255, 255, 125, 240), "Liberation Blazing is ready to strike!", false, false);
                    }
                }

            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<SoulUnbound>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        //Spawn the attacking projectile here.
                        Vector2 placement2 = new Vector2(Player.Center.X, Player.Center.Y);
                        Projectile.NewProjectile(Player.GetSource_FromThis(), placement2.X, placement2.Y, 0, 0, ProjectileType<radiateChaos>(), 0, 0f, 0);
                        Projectile.NewProjectile(Player.GetSource_FromThis(), placement2.X, placement2.Y, 0, 0, ProjectileType<UnforgottenBurst>(), soulUnboundDamage / 3, 0f, 0);
                        Player.AddBuff(BuffType<SoulUnboundCooldown>(), 1320);
                        Player.Teleport(soulUnboundLocation, 1, 0);
                        NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Player.whoAmI, soulUnboundLocation.X, soulUnboundLocation.Y, 1, 0, 0);
                        soulUnboundActive = false;
                        soulUnboundDamage = 0;
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<SeabornWrath>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<SeabornCooldown>(), 1800);//7

                    }
                }

            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<BoilingBloodBuff>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<BoilingBloodCooldown>(), 1200);


                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<Retribution>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        SpectralArsenal = 0;
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<ArtificeSirenBuff>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<ArtificeSirenCooldown>(), 3600);//3600 is 1 min
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
                            NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Player.whoAmI, AshenAmbitionOldPosition.X, AshenAmbitionOldPosition.Y, 1, 0, 0);
                        }
                        Player.Teleport(AshenAmbitionOldPosition, 1, 0);

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
                if (Player.buffType[i] == BuffType<MagitonOverheat>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<Overheated>(), 60);//7200 is 2 minutes
                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<TakodachiLaserBuff>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<TakodachiLaserBuffCooldown>(), 3600);//3600 is 1 minute
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
                if (Player.buffType[i] == BuffType<MoonlitGreatblade>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<MoonlitCooldown>(), 480);

                    }
                }
            for (int i = 0; i < Player.CountBuffs(); i++)
                if (Player.buffType[i] == BuffType<BerserkerMode>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        Player.AddBuff(BuffType<BerserkerModeCooldown>(), 1800);

                    }
                }




        }

        private void Suistrume()
        {
            if (stellarPerformanceStart == true)
            {
                stellarPerformanceActive = true;
                stellarPerformancePrep = true;
                stellarPerformancePostPrep = false;
                stellarPerformanceEnding = false;
                stellarPerformanceSongTimer = 0;

                //SoundEffectInstance x = Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Custom/SuistrumeSound"));
                //stellarPerformanceSoundInstance = x;
                stellarPerformanceClosingIn = 1000f;
                stellarPerformanceIndicator = false;
                stellarPerformanceStart = false;
            }
            if (stellarPerformanceActive == true)//The song lasts for 5000~ ticks
            {


                stellarPerformanceSongTimer++;
                if (stellarPerformanceSongTimer >= 5061)
                {

                    for (int d = 0; d < 40; d++)
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, 21, 0f + Main.rand.Next(-30, 30), 0f + Main.rand.Next(-30, 30), 150, default, 1.5f);
                    }
                    for (int d = 0; d < 35; d++)
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, 45, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default, 1.5f);
                    }
                    Player.AddBuff(BuffType<StellarPerformanceCooldown>(), 3600);

                    stellarPerformanceEnding = true;
                    stellarPerformancePostPrep = false;
                    PerformanceResourceCurrent = 0;

                    stellarPerformanceActive = false;

                }

            }
            if (stellarPerformancePrep == true)
            {

                for (int i = 0; i < 10; i++)
                {
                    Vector2 vector = new Vector2(
                        Main.rand.Next(-2048, 2048) * (0.003f * stellarPerformanceClosingIn / 6) - 10,
                        Main.rand.Next(-2048, 2048) * (0.003f * stellarPerformanceClosingIn / 6) - 10);
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
                    offset.X += (float)(Math.Sin(angle) * stellarPerformanceClosingIn);
                    offset.Y += (float)(Math.Cos(angle) * stellarPerformanceClosingIn);

                    Dust d = Dust.NewDustPerfect(Player.Center + offset, 45, Player.velocity, 200, default, 0.7f);
                    d.fadeIn = 1f;
                    d.noGravity = true;
                }

                stellarPerformanceClosingIn -= 2;
                stellarPerformancePrepTimer += 0.1f;
                if (stellarPerformancePrepTimer >= 0.401f)
                {
                    PerformanceResourceCurrent++;
                    stellarPerformancePrepTimer = 0;
                }
                if (PerformanceResourceCurrent >= 100)
                {
                    for (int d = 0; d < 100; d++)
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, 21, 0f + Main.rand.Next(-40, 40), 0f + Main.rand.Next(-40, 40), 150, default, 1.5f);
                    }
                    for (int d = 0; d < 35; d++)
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, 45, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default, 1.5f);
                    }
                    stellarPerformancePulseRadius = 0;
                    stellarPerformancePostPrep = true;
                    stellarPerformancePrep = false;

                }
            }
            if (stellarPerformancePostPrep == true)
            {
                stellarPerformanceDepletion++;
                Player.AddBuff(BuffType<StellarPerformance>(), 2);
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
                        if (PerformanceResourceCurrent < 100)
                        {
                            PerformanceResourceCurrent++;
                        }
                    }
                    else
                    {
                        PerformanceResourceCurrent--;
                    }
                    stellarPerformanceDepletion = 0;
                }
                if (Main.rand.NextBool(3))
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, 45, 0f + Main.rand.Next(-30, 30), 0f + Main.rand.Next(-30, 30), 150, default, 1.5f);
                }
                if (!(Player.velocity == Vector2.Zero))
                {
                    if (Main.rand.NextBool(5))
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, DustType<MusicNote>(), 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default, 1.5f);
                    }
                }
                if (Main.rand.NextBool(2))
                {
                    Vector2 position = new Vector2(Player.position.X - 800 / 2, Player.position.Y - 800 / 2);
                    Dust.NewDust(position, 800, 800, DustType<MusicNote>(), 0f + Main.rand.Next(-25, 25), 0f + Main.rand.Next(-25, 25), 150, default, 1.5f);
                }

                if (stellarPerformanceIndicator == true)
                {
                    for (int i = 0; i < 30; i++)
                    {//Circle
                        Vector2 offset = new Vector2();
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        offset.X += (float)(Math.Sin(angle) * 520f);
                        offset.Y += (float)(Math.Cos(angle) * 520f);

                        Dust d = Dust.NewDustPerfect(Player.Center + offset, 45, Player.velocity, 200, default, 0.7f);
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

                    Dust d = Dust.NewDustPerfect(Player.Center + offset, 45, Player.velocity, 200, default, 0.7f);
                    d.fadeIn = 0.2f;
                    d.noGravity = true;
                }
                if (stellarPerformancePulseRadius >= 520)
                {
                    stellarPerformanceIndicator = true;
                    stellarPerformancePulseRadius = 0;
                }


            }
            if (stellarPerformancePostPrep == true)
            {
                Player.AddBuff(BuffType<StellarPerformance>(), 1);


            }
            if (stellarPerformancePostPrep == true)
            {

                if (PerformanceResourceCurrent < 0)
                {
                    stellarPerformanceSoundInstance?.Stop();
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_SuistrumeFail, Player.Center);
                    Player.AddBuff(BuffType<StellarPerformanceCooldown>(), 7200);

                    stellarPerformanceActive = false;
                    stellarPerformanceEnding = true;
                    stellarPerformancePostPrep = false;
                    PerformanceResourceCurrent = 0;

                }

            }
        }

        private void BuryTheLight()
        {
            judgementCutTimer--;
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
        }

        public override void PostUpdateBuffs()
        {
            applySuistrumeBuff();
            if (stellarPerformanceEnding == true)
            {
                Player.statLife = Player.statLifeMax2;
                Player.statMana = Player.statManaMax2;
                stellarPerformanceEnding = false;
            }

        }

        private void applySuistrumeBuff()
        {
            foreach (Player buffPlayer in Main.player)
            {
                if (!buffPlayer.active || buffPlayer.dead) continue;
                if (!buffPlayer.GetModPlayer<WeaponPlayer>().stellarPerformancePostPrep == true)
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
                            otherPlayer.AddBuff(BuffType<StellarListener>(), 2);
                        }
                    }
                }
            }
        }
        public static bool SameTeam(Player player1, Player player2)
        {

            if (player1.whoAmI == player2.whoAmI) return true;

            if (player1.team > 0 && player1.team != player2.team) return false;

            if (player1.hostile && player2.hostile && (player1.team == 0 || player2.team == 0)) return false;

            return true;
        }
        public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        {
            if(Player.HasBuff(BuffType<LegendaryShieldRaisedCooldown>()))
            {
                healValue = (int)(healValue * 0.2f);
            }
            base.GetHealLife(item, quickHeal, ref healValue);
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (stellarPerformancePostPrep == true)
            {
                if (info.Damage > 20)
                {
                    PerformanceResourceCurrent -= 20;

                }
                else
                {
                    PerformanceResourceCurrent -= info.Damage;
                }
            }
            if(M4A1Held)
            {
                if(ActiveGuns.Count > 0)
                {
                    int randomChoice = Main.rand.Next(ActiveGuns.Count); //Choose a random weapon from the list
                    int WeaponType = ActiveGuns[randomChoice];//'save' the ProjectileID of that weapon
                    ActiveGuns.Remove(WeaponType);//Add this weapon to the active guns pool
                    for(int i = 0; i >= Main.maxProjectiles; i++)
                    {
                        if (Main.projectile[i].type == WeaponType)
                        {
                            Main.projectile[i].Kill();
                        }
                    }
                    AuxiliaryGuns.Add(randomChoice);
                }
            }
            if (Player.HasBuff(BuffType<LegendaryShieldRaised>()))
            {
                Vector2 dustPosition = Player.Center;

                float dustAmount = 40f;
                for (int i = 0; i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(14f, 15f);
                    spinningpoint5 = spinningpoint5.RotatedBy(Player.velocity.ToRotation() + MathHelper.ToRadians(90));
                    int dust = Dust.NewDust(dustPosition, 0, 0, DustID.GemEmerald);
                    Main.dust[dust].scale = 1f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = dustPosition + spinningpoint5;
                    Main.dust[dust].velocity = Player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 10f;
                }
                for (int d = 0; d < 16; d++)
                {
                    Dust.NewDust(Player.Center, 0, 0, DustID.GemTopaz, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.7f);
                }
                for (int d = 0; d < 16; d++)
                {
                    Dust.NewDust(Player.Center, 0, 0, DustID.GemEmerald, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.7f);
                }
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && npc.Distance(Player.Center) < 1000 && npc.CanBeChasedBy())
                    {
                        npc.SimpleStrikeNPC(info.Damage * (1 + Player.statDefense/150), 0, false, 0, DamageClass.Generic, false, 0);
                        for (int d = 0; d < 16; d++)
                        {
                            Dust.NewDust(npc.Center, 0, 0, DustID.GemEmerald, 0f + Main.rand.Next(-7, 7), 0f + Main.rand.Next(-7, 7), 150, default(Color), 0.7f);
                        }
                    }
                }
                SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact, Player.Center);
                Player.Heal(info.SourceDamage);
            }
            if (RebellionHeld)
            {
                rebellionGauge++;
            }
            if (DraggedBelowHeld)
            {
                DraggedBelowCorruption++;
            }
            if (euthymiaActive)
            {
                eternityGauge -= info.Damage / 2;
            }
            if (Player.HasBuff(BuffType<JetstreamBloodshed>()) && !Player.HasBuff(BuffType<GuntriggerParry>()))
            {
                Player.ClearBuff(BuffType<JetstreamBloodshed>());
            }

            if (Player.HasBuff(BuffType<Bedazzled>()))//If the player has a Prismic...
            {
                CatalystPrismicHP -= (int)(info.Damage * 0.8);//The Prismic absorbs 80% of the damage taken.
                info.Damage = (int)(info.Damage * 0.2);
                //VFX to show the interaction.
                for (int i = 0; i < 100; i++)
                {
                    Vector2 position = Vector2.Lerp(Player.Center, CatalystPrismicPosition, (float)i / 100);
                    Dust d = Dust.NewDustPerfect(position, DustID.PurpleCrystalShard, null, 240, default, 0.6f);
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
            base.OnHurt(info);
        }
        public override bool ImmuneTo(PlayerDeathReason damageSource, int cooldownCounter, bool dodgeable)
        {

            return base.ImmuneTo(damageSource, cooldownCounter, dodgeable);
        }
        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (Player.HasBuff(BuffType<DragonshiftActiveBuff>()) && !Player.immune)
            {
                Player.immune = true;
                Player.immuneTime = 30;
                DragonshiftGauge -= 10;
                return true;
            }
            if (Player.HasBuff(BuffType<SpectrumAbsorption>()) && Player.immuneTime <= 0)
            {
                SoundEngine.PlaySound(SoundID.Item130, Player.Center);

                gaugeChangeAlpha = 1f;
                int adjustedDamage = 0;
                adjustedDamage = info.SourceDamage / 3;
                refractionGauge += adjustedDamage;
                if (Player.HasBuff<SpectrumBlazeAffinity>())
                {
                    refractionGauge += 3;
                }
                float dustAmount = 26f;
                if (refractionGauge < 20)
                {
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(Player.velocity.ToRotation());
                        int dust = Dust.NewDust(Player.Center, 0, 0, DustID.GemTopaz);
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = Player.Center + spinningpoint5;
                        Main.dust[dust].velocity = Player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
                    }
                }
                if (refractionGauge >= 20 && refractionGauge < 90)
                {
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(Player.velocity.ToRotation());
                        int dust = Dust.NewDust(Player.Center, 0, 0, DustID.GemDiamond);
                        Main.dust[dust].color = new Color(51, 255, 147);
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = Player.Center + spinningpoint5;
                        Main.dust[dust].velocity = Player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
                    }
                }
                if (refractionGauge >= 90)
                {
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                        spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                        spinningpoint5 = spinningpoint5.RotatedBy(Player.velocity.ToRotation());
                        int dust = Dust.NewDust(Player.Center, 0, 0, DustID.GemSapphire);
                        Main.dust[dust].scale = 2f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = Player.Center + spinningpoint5;
                        Main.dust[dust].velocity = Player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
                    }
                }
                Player.immune = true;

                Player.immuneTime = 30;
                return true;
            }
            if (Player.HasBuff(BuffType<SpecialAttackBuff>()))
            {
                if (Main.rand.Next(0, 101) <= 25)
                {
                    Player.immune = true;
                    Player.immuneTime = 30;
                    return true;
                }

            }
            return base.FreeDodge(info);
        }
        public override bool ConsumableDodge(Player.HurtInfo info)
        {
            if (M4A1Held)
            {
                return false;
            }
            if (Player.HasBuff(BuffType<ShadowWallBuff>()))
            {
                for (int d = 0; d < 16; d++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.Shadowflame, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default, 1.5f);
                }
                Player.immune = true;
                Player.immuneTime = 30;
                int index = Player.FindBuffIndex(BuffType<ShadowWallBuff>());
                if (index > -1)
                {
                    Player.DelBuff(index);
                }
                return true;
            }
            if (Player.HasBuff(BuffType<CallOfTheStarsBuff>()))
            {
                for (int d = 0; d < 16; d++)
                {
                    Dust.NewDust(Player.position, Player.width, Player.height, DustID.GemAmethyst, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default, 1.5f);
                }
                Player.immune = true;
                Player.immuneTime = 30;
                int index = Player.FindBuffIndex(BuffType<CallOfTheStarsBuff>());
                if (index > -1)
                {
                    Player.DelBuff(index);
                }
                Player.AddBuff(BuffType<CallOfTheStarsCooldown>(), 60 * 180);
                return true;
            }
            if (Player.HasBuff(BuffType<GuntriggerParry>()))
            {

                Player.AddBuff(BuffType<JetstreamBloodshed>(), 10);
                Player.ClearBuff(BuffType<ImpactRecoil>());
                Player.AddBuff(BuffType<Invincibility>(), 20);
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0, 0, ProjectileType<fastRadiate>(), 0, 0, Player.whoAmI, 0f);
                SoundEngine.PlaySound(StarsAboveAudio.SFX_GuntriggerParry, Player.Center);

                if (Player.statLife <= 100)
                {
                    Player.ClearBuff(BuffType<GuntriggerParryCooldown>());
                }
                return true;
            }
            if (Player.ownedProjectileCounts[ProjectileType<FragmentOfTimeMinion>()] >= 1 && !Player.HasBuff(BuffType<TimeBubbleCooldown>()))
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];

                    if (proj.owner == Player.whoAmI &&
                        proj.type == ProjectileType<TimePulse>() && proj.Distance(Player.Center) <= proj.ai[0] && proj.active)
                    {
                        Player.AddBuff(BuffType<Invincibility>(), 20);
                        Player.AddBuff(BuffType<TimeBubbleCooldown>(), 1200);
                        proj.Kill();
                        return true;
                    }
                }
            }

            if (Player.HasBuff(BuffType<RealizedNanomachinaBuff>()))
            {
                if (nanomachinaShieldHP > 0)
                {
                    if (info.Damage > nanomachinaShieldHP)
                    {
                        Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                        CombatText.NewText(textPos, new Color(122, 113, 153, 255), $"-{nanomachinaShieldHP}", false, false);

                        info.Damage -= nanomachinaShieldHP;
                        Player.ClearBuff(BuffType<RealizedNanomachinaBuff>());
                        //The shield breaks!
                        for (int d = 0; d < 32; d++)
                        {
                            Dust.NewDust(Player.Center, 0, 0, DustID.Flare, 0f + Main.rand.Next(-12, 12), 0f + Main.rand.Next(-12, 12), 150, default, 1f);
                        }
                        for (int d = 0; d < 12; d++)
                        {
                            Dust.NewDust(Player.Center, 0, 0, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-2, 2), 150, default, 0.5f);
                        }
                        SoundEngine.PlaySound(SoundID.NPCHit34, Player.Center);

                    }
                    else
                    {
                        if (info.Damage - nanomachinaShieldHP <= 0) //If shield is more than the damage...
                        {
                            Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                            CombatText.NewText(textPos, new Color(122, 113, 153, 255), $"-{info.Damage}", false, false);
                            nanomachinaShieldHP -= info.Damage;
                            //Mimic I-frames
                            Player.immune = true;
                            Player.immuneTime = 60;

                            //Clash sound effects and dust
                            for (int d = 0; d < 12; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-2, 2), 150, default, 0.5f);
                            }
                            for (int d = 0; d < 12; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, DustID.Flare, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-2, 2), 150, default, 0.5f);
                            }
                            SoundEngine.PlaySound(SoundID.NPCHit53, Player.Center);

                            return true;
                        }
                        else
                        {

                        }
                    }

                }
                if (Player.HasBuff(BuffType<EchoStringRites>()))
                {
                    if (info.DamageSource.SourceNPCIndex == 0)
                    {

                    }
                    else
                    {
                        int dodgeChance = 0;
                        for (int i = 0; i < 12; i++)
                        {
                            NPC npc = Main.npc[info.DamageSource.SourceNPCIndex];
                            if (npc.buffType[i] > 0 && Main.debuff[npc.buffType[i]])
                            {
                                dodgeChance++;
                                continue;
                            }
                            if (Main.rand.Next(0, 101) < dodgeChance)
                            {
                                return true;
                            }

                            //target.buffTime[i] += 20 * 60;
                        }
                    }



                }
            }
            return base.ConsumableDodge(info);
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)/* tModPorter Override ImmuneTo, FreeDodge or ConsumableDodge instead to prevent taking damage */
        {
            if (Player.HasBuff(BuffType<Mortality>()))
            {
                modifiers.FinalDamage *= 2;
            }
            if (Player.HasBuff(BuffType<DeifiedBuff>()))
            {
                modifiers.FinalDamage /= 2;
            }
            if (Player.HasBuff(BuffType<SpecialAttackBuff>()))
            {
                modifiers.FinalDamage *= 3;
            }
            if (LVStacks > 0)
            {
                modifiers.FinalDamage.Flat -= LVStacks;
                LVStacks = 0;
            }
            if (Player.HasBuff(BuffType<SakuraEarthBuff>()) || Player.HasBuff(BuffType<ElementalChaos>()))
            {
                modifiers.FinalDamage *= 0.75f;
            }

            if (Main.LocalPlayer.HasBuff(BuffType<ButterflyTrance>()))
            {
                modifiers.FinalDamage /= 2;
            }
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
            if (Player.HasBuff(BuffType<AtrophiedDeifiedBuff>()))
            {
                damageSource = PlayerDeathReason.ByCustomReason(Player.name + " " + LangHelper.GetTextValue($"DeathReason.SupremeAuthority"));
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player player = Main.player[i];
                    if (player.active)
                    {
                        player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
                        player.AddBuff(BuffID.PotionSickness, 60 * 120);
                        player.potionDelayTime = 60 * 120;
                    }


                }
            }
            if (Main.LocalPlayer.HasBuff(BuffType<LivingDead>()))
            {
                playSound = false;
                genGore = false;
                Player.statLife = 1;

                return false;
            }

            if (stellarPerformanceActive == true)
            {


                stellarPerformanceSoundInstance?.Stop();
                SoundEngine.PlaySound(StarsAboveAudio.SFX_SuistrumeFail, Player.Center);
                stellarPerformanceActive = false;
                stellarPerformanceEnding = true;
                stellarPerformancePostPrep = false;
                PerformanceResourceCurrent = 0;

            }

            if (willpowerOfZagreus)
            {

                if (!Main.LocalPlayer.HasBuff(BuffType<DeathDefianceCooldown>()))
                {


                    Dust dust;
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 position = Player.Center;
                    dust = Main.dust[Dust.NewDust(position, 0, 0, 247, 0f, 0f, 0, new Color(255, 0, 0), 1f)];

                    Player.AddBuff(BuffType<DeathDefianceCooldown>(), 28800);

                    Player.statLife = Player.statLifeMax / 2;
                    return false;


                }
            }

            if (darkHourglass)
            {

                if (!Main.LocalPlayer.HasBuff(BuffType<LivingDead>()))
                {
                    if (!Main.LocalPlayer.HasBuff(BuffType<LivingDeadCooldown>()))
                    {
                        Player.AddBuff(BuffType<LivingDead>(), 600);
                        Dust dust;
                        // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        Vector2 position = Main.LocalPlayer.Center;
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

            if (MajimaPet)
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_BakaMitai, Player.Center);
            }
            if (Ghost)
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_GuardianDown, Player.Center);
            }

            return true;
        }

        public override void OnRespawn()
        {
            if (luciferium)
            {
                Player.AddBuff(BuffID.PotionSickness, 3600);
            }
            if (soulUnboundActive)
            {
                soulUnboundActive = false;
            }

        }
        public override void ResetEffects()
        {
            WeaponGaugeOffset = 0;
            if (!DragaliaFoundHeld)
            {
                DragonshiftGauge = 0;
            }
            if (!RebellionHeld)
            {
                rebellionGauge = 0;
            }
            if (Player.HasBuff(BuffType<EmbodiedSingularity>()) && !CloakOfAnArbiterHeld)
            {
                Player.ClearBuff(BuffType<EmbodiedSingularity>());
                Player.GetModPlayer<BossPlayer>().WhiteAlpha = 1f;
            }
            if (!M4A1Held || Player.GetModPlayer<StarsAbovePlayer>().inCombat < 0 || Player.HasBuff(BuffType<InheritedCaseActive>()))
            {
                ActiveGuns.Clear();
                AuxiliaryGuns = [
                    ProjectileType<StAR15Gun>(),
                    ProjectileType<RO635Gun>(),
                    ProjectileType<M4Sop2Gun>(),
                    ProjectileType<M16A1Gun>(),
                ];

            }
            enviroSavantActive = false;
            plasteelEquipped = false;
            roguegarbEquipped = false;
            phasmasaberHeld = false;
            wolvesbaneHeld = false;
            dreadmotherMinion = false;
            dreadmotherHeld = false;
            CloakOfAnArbiterHeld = false;
            LegendaryShieldHeld = false;
            LegendaryShieldEquippedAsAccessory = false;
            M4A1Held = false;
            wavedancerHeld = false;
            RebellionHeld = false;
            DragaliaFoundHeld = false;
            DraggedBelowHeld = false;
            KevesiFarewellInInventory = false;
            AgnianFarewellInInventory = false;
            BuryTheLightHeld = false;
            SaltwaterScourgeHeld = false;
            InkwellHeld = false;
            BrilliantSpectrumHeld = false;
            KissOfDeathHeld = false;
            BurningDesireHeld = false;
            GoldenKatanaHeld = false;
            IrminsulHeld = false;
            AshenAmbitionHeld = false;
            VermillionDaemonHeld = false;
            SakuraVengeanceHeld = false;
            ChemtankHeld = false;
            HunterSymphonyHeld = false;
            SkyStrikerHeld = false;

            euthymiaActive = false;
            SoulReaverHeld = false;

            seraphimHeld--;
            kroniicHeld--;
            albionHeld--;
            if (Player.statMana <= 0)
            {
                phantomTeleport = false;
            }
            starblessedCooldown--;
            RobotSpiderMinion = false;
            SatanaelMinion = false;
            GundbitsActive = false;
            WavedancerMinion = false;
            AlucardSwordMinion1 = false;
            AlucardSwordMinion2 = false;
            AlucardSwordMinion3 = false;
            TakodachiMinion = false;
            SoliloquyMinions = false;
            sugarballMinions = false;
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
            AigisPet = false;
            NeuroPet = false;
            FireflyPet = false;
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
            luciferium = false;
            Glitterglue = false;
            PerfectlyGenericAccessory = false;
            DragonwardTalisman = false;
            GaleflameFeather = false;
            ToMurder = false;
            AlienCoral = false;

        }
        public override void FrameEffects()
        {
            if (Player.active && !Player.dead)
            {
                if (SkyStrikerHeld)
                {
                    if (Player.HasBuff(BuffType<StrikerAttackBuff>()))
                    {

                        Player.legs = EquipLoader.GetEquipSlot(Mod, "AfterburnerBottom", EquipType.Legs);
                        Player.body = EquipLoader.GetEquipSlot(Mod, "AfterburnerTop", EquipType.Body);
                        Player.wings = EquipLoader.GetEquipSlot(Mod, "AfterburnerWings", EquipType.Wings);
                    }
                    if (Player.HasBuff(BuffType<StrikerShieldBuff>()))
                    {
                        Player.legs = EquipLoader.GetEquipSlot(Mod, "ShieldBottom", EquipType.Legs);
                        Player.body = EquipLoader.GetEquipSlot(Mod, "ShieldTop", EquipType.Body);
                        Player.wings = EquipLoader.GetEquipSlot(Mod, "ShieldWings", EquipType.Wings);
                    }
                    if (Player.HasBuff(BuffType<StrikerMeleeBuff>()))
                    {
                        Player.legs = EquipLoader.GetEquipSlot(Mod, "MeleeBottom", EquipType.Legs);
                        Player.body = EquipLoader.GetEquipSlot(Mod, "MeleeTop", EquipType.Body);
                        Player.wings = EquipLoader.GetEquipSlot(Mod, "MeleeWings", EquipType.Wings);
                    }
                    if (Player.HasBuff(BuffType<StrikerSniperBuff>()))
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
                if (rebellionGauge >= 5)
                {
                    if (rebellionState1 == false)
                    {
                        //effects
                        float dustAmount = 33f;
                        float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
                        for (int i = 0; i < dustAmount; i++)
                        {
                            Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                            spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                            spinningpoint5 = spinningpoint5.RotatedBy(Player.velocity.ToRotation() + randomConstant);
                            int dust = Dust.NewDust(Player.Center, 0, 0, DustID.LifeDrain);
                            Main.dust[dust].scale = 1.5f;
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].position = Player.Center + spinningpoint5;
                            Main.dust[dust].velocity = Player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
                        }
                        Player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
                        SoundEngine.PlaySound(SoundID.Item52, Player.Center);

                    }
                    Player.statDefense += 5;

                    rebellionState1 = true;
                    Player.legs = EquipLoader.GetEquipSlot(Mod, "RebellionLegs", EquipType.Legs);
                }
                else
                {
                    rebellionState1 = false;
                }
                if (rebellionGauge >= 10)
                {
                    if (rebellionState2 == false)
                    {
                        //effects
                        float dustAmount = 33f;
                        float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
                        for (int i = 0; i < dustAmount; i++)
                        {
                            Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                            spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                            spinningpoint5 = spinningpoint5.RotatedBy(Player.velocity.ToRotation() + randomConstant);
                            int dust = Dust.NewDust(Player.Center, 0, 0, DustID.LifeDrain);
                            Main.dust[dust].scale = 1.5f;
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].position = Player.Center + spinningpoint5;
                            Main.dust[dust].velocity = Player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
                        }
                        SoundEngine.PlaySound(SoundID.Item53, Player.Center);

                        Player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
                    }
                    Player.statDefense += 5;

                    rebellionState2 = true;
                    Player.body = EquipLoader.GetEquipSlot(Mod, "RebellionBody", EquipType.Body);
                }
                else
                {
                    rebellionState2 = false;
                }
                if (rebellionGauge >= 15)
                {
                    if (rebellionState3 == false)
                    {
                        //effects
                        float dustAmount = 33f;
                        float randomConstant = MathHelper.ToRadians(Main.rand.Next(0, 360));
                        for (int i = 0; i < dustAmount; i++)
                        {
                            Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                            spinningpoint5 += -Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                            spinningpoint5 = spinningpoint5.RotatedBy(Player.velocity.ToRotation() + randomConstant);
                            int dust = Dust.NewDust(Player.Center, 0, 0, DustID.LifeDrain);
                            Main.dust[dust].scale = 1.5f;
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].position = Player.Center + spinningpoint5;
                            Main.dust[dust].velocity = Player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 6f;
                        }
                        SoundEngine.PlaySound(SoundID.AbigailUpgrade, Player.Center);

                        Player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -90;
                    }
                    rebellionState3 = true;
                    Player.statDefense += 10;
                    Player.head = EquipLoader.GetEquipSlot(Mod, "RebellionHead", EquipType.Head);

                }
                else
                {
                    rebellionState3 = false;
                }
                if (DraggedBelowHeld)
                {
                    Player.UpdateVisibleAccessories(new Item(ItemType<DraggedBelowGloves>()), false);

                }
                if (dreadmotherHeld && Player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
                {
                    Player.UpdateVisibleAccessories(new Item(ItemType<DreadmotherClaw>()), false);

                }
                if (CloakOfAnArbiterHeld)
                {
                    Player.UpdateVisibleAccessories(new Item(ItemType<CloakOfAnArbiterCape>()), false);

                }
                if ((LegendaryShieldHeld || LegendaryShieldEquippedAsAccessory) && !Player.HasBuff(BuffType<LegendaryShieldRaised>()))
                {
                    Player.UpdateVisibleAccessories(new Item(ItemType<LegendaryShieldAccessory>()), false);

                }
                
            }
        }
        public override void HideDrawLayers(PlayerDrawSet drawInfo)
        {
            if (Player.HasBuff(BuffType<Ignited>()))//
            {
                foreach (var layer in PlayerDrawLayerLoader.Layers)
                {

                    layer.Hide();

                }
            }
            if (Player.HasBuff(BuffType<FlashOfEternity>()))//
            {
                foreach (var layer in PlayerDrawLayerLoader.Layers)
                {

                    layer.Hide();

                }
            }
            if (Player.HasBuff(BuffType<Voidform>()))//
            {
                foreach (var layer in PlayerDrawLayerLoader.Layers)
                {

                    layer.Hide();

                }
            }
            if (Player.HasBuff(BuffType<IrysBuff>()))//
            {
                foreach (var layer in PlayerDrawLayerLoader.Layers)
                {

                    layer.Hide();

                }
            }
            if (Player.HasBuff(BuffType<Invisibility>()) || Player.HasBuff(BuffType<ParadiseLostBuff>()))//
            {
                foreach (var layer in PlayerDrawLayerLoader.Layers)
                {

                    layer.Hide();

                }
            }
            if (Player.HasBuff(BuffType<DragonshiftActiveBuff>()))//
            {
                foreach (var layer in PlayerDrawLayerLoader.Layers)
                {
                    if (layer.ToString() == "MountFront" || layer.ToString() == "MountBack")
                    {
                        continue;
                    }
                    layer.Hide();
                }
            }

            base.HideDrawLayers(drawInfo);
        }
        private void OnKillEnemy(NPC npc)
        {
            if (enviroSavantActive)
            {
                Player.Heal(2);
            }
            if(dreadmotherHeld && Player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
            {
                Player.AddBuff(BuffType<Invincibility>(), 10);
                Player.Heal((int)(Player.statLifeMax2 * 0.02f));
            }
            if (Player.HasBuff(BuffType<RealizedNanomachinaBuff>()))
            {
                nanomachinaGauge += 5;
            }
            if (Player.HasBuff(BuffType<OffSeersJourney>()))
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
        public override void PreUpdate()
        {
            var player = Player.GetModPlayer<StarsAbovePlayer>();
            //New weapon gauges.
            gaugeChangeAlpha -= 0.1f;

            //# of active minions
            activeMinions = (int)Player.slotsMinions;

            //Values that reset out of combat
            if (player.inCombat < 0)
            {
                LVStacks--;
                if (LVStacks < 0)
                {
                    LVStacks = 0;
                }
                SoulReaverSouls = 0;
                //nanomachinaGauge = 0;
            }

            //Weapon PreUpdates
            DreamersInkwell();
            CatalystMemory();
            OzmaAscendant();
            VermilionRiposte();
            CosmicDestroyer();
            ArchitectLuminance();
            Takonomicon(player);
            TwinStarsOfAlbiero(player);
            AshenAmbition();
            SunsetOfTheSunGod();
            Wavedancer();
            VermilionDaemon();
            SupremeAuthority();
            SakuraVengenace();
            ArmamentsOfTheSkyStriker();
            StygianNymph();
            EyeOfEuthymia();
            PhantomInTheMirror();
            KroniicPrincipality();
            Nanomachina();
            DraggedBelow();
            ParadiseLost();

            if(dreadmotherShieldStacks >= 20)
            {
                dreadmotherShieldStacks = 0;
                Player.AddBuff(BuffType<ShadowWallBuff>(), 20 * 60);

                SoundEngine.PlaySound(SoundID.Item15, Player.Center);
                float dustAmount = 60f;
                for (int i = 0; (float)i < dustAmount; i++)
                {
                    Vector2 spinningpoint5 = Vector2.UnitX * 0f;
                    spinningpoint5 += -Vector2.UnitY.RotatedBy((float)i * ((float)Math.PI * 2f / dustAmount)) * new Vector2(4f, 4f);
                    spinningpoint5 = spinningpoint5.RotatedBy(Player.velocity.ToRotation());
                    int dust = Dust.NewDust(Player.Center, 0, 0, DustID.Shadowflame);
                    Main.dust[dust].scale = 2f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = Player.Center + spinningpoint5;
                    Main.dust[dust].velocity = Player.velocity * 0f + spinningpoint5.SafeNormalize(Vector2.UnitY) * 20f;
                }
            }
            if (M4A1Held)
            {
                M4A1deg += 0 + MathHelper.Lerp(0.5f, 1.5f, EaseHelper.InOutQuad((float)(M4A1UseTimer / 100f)));
                M4A1UseTimer--;
                M4A1UseTimer = (int)MathHelper.Clamp(M4A1UseTimer, 0, 100);
            }

            //Radiance cap (used by certain weapons.)
            if (radiance > 10)
            {
                radiance = 10;
            }
        }

        private void ParadiseLost()
        {
            if (paradiseLostActive)
            {
                Player.AddBuff(BuffType<ParadiseLostBuff>(), 2);
                if (Player.statLife < 10)
                {
                    paradiseLostActive = false;
                    Player.GetModPlayer<BossPlayer>().WhiteAlpha = 1f;
                    Player.AddBuff(BuffType<Vulnerable>(), 60 * 60);
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && npc.friendly && npc.townNPC && npc.HasBuff(BuffType<ApostleBuff>()))
                        {
                            npc.DelBuff(npc.FindBuffIndex(BuffType<ApostleBuff>()));

                        }
                    }
                }
                else
                {
                    Player.lifeRegenTime = 0;
                    paradiseLostDrainTimer++;
                    if (paradiseLostDrainTimer > 5)
                    {
                        Player.statLife--;
                        paradiseLostDrainTimer = 0;
                    }
                }
                if (Player.HeldItem.type != ItemType<ParadiseLost>())
                {
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && npc.friendly && npc.townNPC && npc.HasBuff(BuffType<ApostleBuff>()))
                        {
                            npc.DelBuff(npc.FindBuffIndex(BuffType<ApostleBuff>()));

                        }
                    }
                    paradiseLostActive = false;
                    Player.GetModPlayer<BossPlayer>().WhiteAlpha = 1f;
                    Player.AddBuff(BuffType<Vulnerable>(), 60 * 60);
                }
            }


            paradiseLostAnimationTimer -= 0.005f;
            if (paradiseLostAnimationTimer > 0.7f)
            {
                paradiseLostAnimationFloat1 += 0.02f;

            }
            //Part 2: unleash power animation
            else if (paradiseLostAnimationTimer > 0.6f && paradiseLostAnimationTimer < 0.7f)
            {
                paradiseLostAnimationFloat1 -= 0.2f;


            }
            else if (paradiseLostAnimationTimer > 0)
            {
                paradiseLostAnimationFloat1 += 0.01f;

            }
            else
            {
                paradiseLostAnimationFloat1 = 0f;
            }
            paradiseLostAnimationFloat1 = MathHelper.Clamp(paradiseLostAnimationFloat1, 0f, 1f);

            paradiseLostAnimationTimer = MathHelper.Clamp(paradiseLostAnimationTimer, 0f, 1f);
        }

        private void DraggedBelow()
        {
            DraggedBelowTarget = Player.GetModPlayer<StarsAbovePlayer>().playerMousePos;
            DraggedBelowTarget2 = Vector2.Lerp(DraggedBelowTarget2, Player.GetModPlayer<StarsAbovePlayer>().playerMousePos, 0.02f);
            if (DraggedBelowHeld)
            {
                DraggedBelowCorruptionTimer++;

                if (DraggedBelowCorruptionTimer >= 10)
                {
                    if (Player.HasBuff(BuffType<DraggedBelowCorruption>()))
                    {
                        DraggedBelowCorruption--;
                    }
                    else
                    {
                        DraggedBelowCorruption++;
                    }
                    DraggedBelowCorruptionTimer = 0;
                }
                if (DraggedBelowCorruption >= 100 && !DraggedBelowInCorruption)
                {
                    DraggedBelowInCorruption = true;
                    Player.ClearBuff(BuffType<DraggedBelowCooldown>());
                    SoundEngine.PlaySound(SoundID.Roar, Player.Center);
                    Player.GetModPlayer<StarsAbovePlayer>().screenShakeTimerGlobal = -80;
                    for (int d = 0; d < 30; d++)
                    {
                        Dust.NewDust(Player.Center, 0, 0, DustID.FireworkFountain_Red, 0f + Main.rand.Next(-10, 10), 0f + Main.rand.Next(-10, 10), 150, default, 1f);
                    }
                    for (int d = 0; d < 30; d++)
                    {
                        Dust.NewDust(Player.Center, 0, 0, DustID.LifeDrain, 0f + Main.rand.Next(-15, 15), 0f + Main.rand.Next(-15, 15), 150, default, 1f);
                    }
                }
                if (DraggedBelowInCorruption)
                {
                    Player.AddBuff(BuffType<DraggedBelowCorruption>(), 10);
                    Dust.NewDust(Player.Center, 0, 0, DustID.LifeDrain, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default, 0.7f);

                    if (DraggedBelowCorruption <= 0)
                    {
                        DraggedBelowInCorruption = false;
                        //effects
                        for (int d = 0; d < 15; d++)
                        {
                            Dust.NewDust(Player.Center, 0, 0, DustID.Smoke, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default, 1f);
                        }
                    }
                }
                DraggedBelowCorruption = (int)MathHelper.Clamp(DraggedBelowCorruption, 0, 100);
            }
        }
        private void DreamersInkwell()
        {
            InkwellMana = Player.statManaMax2;
            for (int i = 0; i < Player.ownedProjectileCounts[ProjectileType<InkwellEarthInk>()]; i++)
            {
                InkwellMana -= 4;
            }
            for (int i = 0; i < Player.ownedProjectileCounts[ProjectileType<InkwellAirInk>()]; i++)
            {
                InkwellMana -= 1;
            }
            for (int i = 0; i < Player.ownedProjectileCounts[ProjectileType<InkwellFireInk>()]; i++)
            {
                InkwellMana -= 1;
            }
            for (int i = 0; i < Player.ownedProjectileCounts[ProjectileType<InkwellWaterInk>()]; i++)
            {
                InkwellMana -= 1;
            }
            if (InkwellHeld && StarsAbove.weaponActionKey.Old)
            {
                InkwellUIAlpha += 0.1f;

            }
            else
            {

                InkwellUIAlpha -= 0.1f;
            }
            InkwellMana = (int)MathHelper.Clamp(InkwellMana, 0, Player.statManaMax2);
            if (InkwellUIAdjustment > 0)
            {
                InkwellUIRotation += InkwellUIAdjustment;
                InkwellUIAdjustment -= 0.3f;
            }
            InkwellUIAdjustment = MathHelper.Clamp(InkwellUIAdjustment, 0, 100);
            InkwellUIRotation += 0.5f;
            if (InkwellUIRotation > 360)
            {
                InkwellUIRotation = 0;
            }
            InkwellUIAlpha = MathHelper.Clamp(InkwellUIAlpha, 0, 1);
        }

        private void BrilliantSpectrum()
        {
            if (BrilliantSpectrumHeld)
            {
                refractionGaugeTimer++;
                refractionGauge = Math.Clamp(refractionGauge, 0, refractionGaugeMax);
                if (Player.GetModPlayer<StarsAbovePlayer>().inCombat <= 0)
                {
                    refractionGauge--;
                }
                if (refractionGaugeTimer > 60)
                {
                    if (Player.HasBuff(BuffID.Frostburn) || Player.HasBuff(BuffID.Frostburn2) || Player.HasBuff(BuffID.Chilled) || Player.HasBuff(BuffID.Frozen))
                    {
                        refractionGauge--;

                    }
                    refractionGauge--;
                    refractionGaugeTimer = 0;
                }

            }
            else
            {
                refractionGauge = 0;
            }

        }

        private void SupremeAuthority()
        {
            if (!Player.HasBuff(BuffType<DeifiedBuff>()))
            {
                SupremeAuthorityConsumedNPCs = 0;

            }
            else
            {
                if (Player.ownedProjectileCounts[ProjectileType<AuthorityLantern2>()] < 1)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0, 0, ProjectileType<AuthorityLantern2>(), 0, 0, Player.whoAmI, 0f);
                }
                if (Player.ownedProjectileCounts[ProjectileType<AuthorityLantern1>()] < 1)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center.X, Player.Center.Y, 0, 0, ProjectileType<AuthorityLantern1>(), 0, 0, Player.whoAmI, 0f);
                }
            }
            if (SupremeAuthorityConsumedNPCs > 0)
            {
                Player.AddBuff(BuffType<DarkAura>(), 10);
            }

        }
        private void SunsetOfTheSunGod()
        {
            karnaTarget = Vector2.Lerp(karnaTarget, Main.MouseWorld, 0.01f);

        }
        private void Wavedancer()
        {
            wavedancerTarget = Vector2.Lerp(wavedancerTarget, Main.MouseWorld, 0.1f);
        }
        private void Nanomachina()
        {
            if (!Player.HasBuff(BuffType<RealizedNanomachinaBuff>()))
            {
                nanomachinaGauge = 0;
                nanomachinaShieldHP = 0;
                nanomachinaShieldHPMax = 0;
            }
            if (nanomachinaGauge >= 100)
            {
                //Shield was restored!!
                for (int d = 0; d < 32; d++)
                {
                    Dust.NewDust(Player.Center, 0, 0, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-12, 12), 0f + Main.rand.Next(-12, 12), 150, default, 1f);
                }
                SoundEngine.PlaySound(StarsAboveAudio.SFX_GuntriggerParryPrep, Player.Center);
                Player.AddBuff(BuffType<RealizedNanomachinaBuff>(), 1200);
                nanomachinaShieldHP = nanomachinaShieldHPMax;
                nanomachinaGauge = 0;
            }
        }

        private void PhantomInTheMirror()
        {
            if (phantomTeleport)
            {
                Player.statMana--;
                Player.manaRegenDelay = 220;
            }
        }

        private void KroniicPrincipality()
        {
            kroniicTimer--;
            if (kroniicTimer < 0)
            {
                if (powderGaugeIndicatorOn)
                {
                    Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                    CombatText.NewText(textPos, new Color(255, 255, 125, 240), "The Timeframe vanishes...", false, false);
                    powderGaugeIndicatorOn = false;
                    kroniicTeleport = false;
                    Player.AddBuff(BuffType<KroniicPrincipalityCooldown>(), 3600);//7200 is 2 minutes
                }

            }
            if (kroniicTeleport)
            {
                for (int i = 0; i < 30; i++)
                {//Circle
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 44);
                    offset.Y += (float)(Math.Cos(angle) * 44);

                    Dust d = Dust.NewDustPerfect(kroniicSavedPosition + offset, 20, Vector2.Zero, 200, default, 0.7f);
                    d.fadeIn = 0.1f;
                    d.noGravity = true;
                }
            }
            powderGauge = Math.Clamp(powderGauge, 0, 100);

        }

        private void EyeOfEuthymia()
        {
            euthymiaCooldown--;
            if (eternityGauge < 500)
            {
                eternityGauge++;
            }
            eternityGauge = Math.Clamp(eternityGauge, 0, 1000);
            if (!euthymiaActive)
            {
                eternityGauge = 0;
            }
        }

        private void StygianNymph()
        {
            duality = Math.Clamp(duality, 0, 100);
        }

        private void ArmamentsOfTheSkyStriker()
        {
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
                    int dustIndex = Dust.NewDust(new Vector2(Player.Center.X, Player.Center.Y), 0, 0, 31, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-6, 6), 100, default, 2f);
                    Main.dust[dustIndex].velocity *= 1.4f;
                }
                // Fire Dust spawn

                // Large Smoke Gore spawn
                for (int g = 0; g < 2; g++)
                {
                    int goreIndex = Gore.NewGore(null, new Vector2(Player.position.X + Player.width / 2 - 24f, Player.position.Y + Player.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(Player.position.X + Player.width / 2 - 24f, Player.position.Y + Player.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(Player.position.X + Player.width / 2 - 24f, Player.position.Y + Player.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                    goreIndex = Gore.NewGore(null, new Vector2(Player.position.X + Player.width / 2 - 24f, Player.position.Y + Player.height / 2 - 24f), default, Main.rand.Next(61, 64), 1f);
                    Main.gore[goreIndex].scale = 1.5f;
                    Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                    Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                }
            }
        }

        private void SakuraVengenace()
        {
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
        }

        private void VermilionDaemon()
        {
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
        }

        private void AshenAmbition()
        {
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
        }

        private void TwinStarsOfAlbiero(StarsAbovePlayer player)
        {
            if (Player.HasBuff(BuffType<TwinStarsBuff>()))
            {
                starTarget = Vector2.Lerp(starTarget, player.playerMousePos, 0.05f);
                starTarget2 = Vector2.Lerp(starTarget2, starTarget, 0.05f);

            }
        }

        private void Takonomicon(StarsAbovePlayer player)
        {
            if (Player.HasBuff(BuffType<TakodachiLaserBuff>()))
            {
                takoTarget = Vector2.Lerp(takoTarget, player.playerMousePos, 0.05f);

            }
        }

        private void ArchitectLuminance()
        {
            if (Player.HasBuff(BuffType<ArtificeSirenBuff>()))
            {
                sirenTarget = Vector2.Lerp(sirenTarget, sirenEnemy, 0.05f);

            }
        }

        private void CosmicDestroyer()
        {
            if (Player.HasBuff(BuffType<MagitonOverheat>()) && CosmicDestroyerRounds <= 0)
            {
                for (int i = 0; i < Player.CountBuffs(); i++)
                    if (Player.buffType[i] == BuffType<MagitonOverheat>())
                    {
                        Player.DelBuff(i);


                    }

                Player.AddBuff(BuffType<Overheated>(), 60);

            }
            if (CosmicDestroyerGaugeVisibility > 0f)
            {
                CosmicDestroyerGaugeVisibility -= 0.05f;

            }
            else
            {
                CosmicDestroyerGaugeVisibility = 0f;
            }
        }

        private void VermilionRiposte()
        {
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
        }

        private void OzmaAscendant()
        {
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
        }

        private void CatalystMemory()
        {
            if (CatalystMemoryProgress < 0)
            {
                CatalystMemoryProgress = 0;
            }
            CatalystMemoryProgress--;
        }

        public override void PostUpdate()
        {
            BrilliantSpectrum();



        }

    }

};