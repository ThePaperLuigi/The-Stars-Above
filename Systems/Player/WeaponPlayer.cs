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
using StarsAbove.Buffs.Umbra;
using StarsAbove.Projectiles.Chronoclock;
using StarsAbove.Buffs.Chronoclock;
using StarsAbove.Buffs.Nanomachina;
using StarsAbove.Buffs.ManiacalJustice;
using StarsAbove.Buffs.SupremeAuthority;

namespace StarsAbove
{
    public class WeaponPlayer: ModPlayer
    {
        /* Miscellaneous weapon code (gauges, projectiles, etc.)
         * Also includes accessories.
         * Moved from StarsAbovePlayer because honestly, that file was incredibly bloated.
         * */

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
        

        //The Kiss of Death
        public bool KissOfDeathHeld;
        public int overdriveGauge;


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

        //Supreme Authority
        public int SupremeAuthorityConsumedNPCs;
        public int SupremeAuthorityEncroachingStacks;
        
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

        //Sunset of the Sun God
        public Vector2 KarnaTarget;

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


        //Kevesi and Agnian Farewells
        public bool KevesiFarewellInInventory;
        public bool AgnianFarewellInInventory;

        //Umbra
        public int UmbraGauge;

        //Saltwater Scourge
        public bool SaltwaterScourgeHeld;

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
        #endregion
        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Item, consider using OnHitNPC instead */
        {
            var player = Player.GetModPlayer<StarsAbovePlayer>();

            

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
            if (sakuraHeld)
            {
                if (!bladeWill)
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
            if (Player.HasBuff(BuffType<AstarteDriver>()) && player.starfarerOutfit == 3)
            {
                Projectile.NewProjectile(null, target.Center.X, target.Center.Y, 0f, 0f, ProjectileType<StarfarerFollowUp>(), damage / 3, knockback, Player.whoAmI);

            }


            if (!target.active)
            {
                OnKillEnemy(target);
            }
            base.OnHitNPCWithItem(item, target, damage, knockback, crit);
        }
        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Item, consider using ModifyHitNPC instead */
        {
            var player = Player.GetModPlayer<StarsAbovePlayer>();
            if (!target.active && luciferium)
            {
                Player.AddBuff(BuffType<SatedAnguish>(), 900);
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
            if (Player.HasBuff(BuffType<Buffs.Kifrosse.AmaterasuGrace>()) && target.HasBuff(BuffID.Frostburn))
            {
                damage = damage + (damage / 2);
            }
           
            
            if (Player.HasBuff(BuffType<Buffs.SurtrTwilight>()))
            {
                target.AddBuff(BuffID.OnFire, 480);
            }
            if (crit && item.ModItem is ArchitectLuminance && player.MeleeAspect == 2)
            {
                damage += Player.statDefense;
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
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Projectile, consider using ModifyHitNPC instead */
        {
            //Crit rerolling
            if (target.HasBuff(BuffType<Glitterglued>()) || Player.HasBuff(BuffType<TimelessPotential>()))
            {
                if (!crit)
                {
                    if (Main.rand.Next(0, 100) > 70)
                    {
                        crit = true;
                    }
                }
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
                    if (crit)
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
                    if (Player.GetModPlayer<StarsAbovePlayer>().MeleeAspect != 2)
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
                    izanagiPerfect += 1;

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
            if (proj.type == Mod.Find<ModProjectile>("Starchild").Type && target.type != NPCID.TargetDummy)
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

                if (aegisGauge >= 100)
                {
                    damage *= 2;
                    crit = true;
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
            

        }
        public override void PostUpdateRunSpeeds()
        {
            if(Player.HasBuff(BuffType<Mortality>()))
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
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Projectile, consider using OnHitNPC instead */
        {
            var player = Player.GetModPlayer<StarsAbovePlayer>();
            if (Player.HasBuff(BuffType<BoilingBloodBuff>()))
            {
                boilingBloodDamage += damage / 4;
            }

            if (!target.active && luciferium)
            {
                Player.AddBuff(BuffType<SatedAnguish>(), 900);
            }
            
            if (projectile.type != Mod.Find<ModProjectile>("EuthymiaFollowUp").Type && euthymiaActive && euthymiaCooldown <= 0)
            {
                SoundEngine.PlaySound(StarsAboveAudio.SFX_electroSmack, Player.Center);

                Projectile.NewProjectile(null, target.Center.X, target.Center.Y, 0, 0, Mod.Find<ModProjectile>("EuthymiaFollowUp").Type, Math.Min(damage / 5, 500), 0, Player.whoAmI, 0f);
                euthymiaCooldown = 120 - (eternityGauge / 10);

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


                    edgeHoned = false;
                }
                else
                {


                }

            }
            if (projectile.type == Mod.Find<ModProjectile>("HullwroughtRound").Type)
            {

                player.screenShakeTimerGlobal = -80;
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
                SoundEngine.PlaySound(SoundID.Item89);
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
                        Dust.NewDust(Player.position, Player.width, Player.height, 21, 0f + Main.rand.Next(-30, 30), 0f + Main.rand.Next(-30, 30), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 35; d++)
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, 45, 0f + Main.rand.Next(-5, 5), 0f + Main.rand.Next(-5, 5), 150, default(Color), 1.5f);
                    }
                    Player.AddBuff(BuffType<Buffs.StellarPerformanceCooldown>(), 3600);

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

                    Dust d = Dust.NewDustPerfect(Player.Center + offset, 45, Player.velocity, 200, default(Color), 0.7f);
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
                        Dust.NewDust(Player.position, Player.width, Player.height, 21, 0f + Main.rand.Next(-40, 40), 0f + Main.rand.Next(-40, 40), 150, default(Color), 1.5f);
                    }
                    for (int d = 0; d < 35; d++)
                    {
                        Dust.NewDust(Player.position, Player.width, Player.height, 45, 0f + Main.rand.Next(-45, 45), 0f + Main.rand.Next(-45, 45), 150, default(Color), 1.5f);
                    }
                    stellarPerformancePulseRadius = 0;
                    stellarPerformancePostPrep = true;
                    stellarPerformancePrep = false;

                }
            }
            if (stellarPerformancePostPrep == true)
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

                if (stellarPerformanceIndicator == true)
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
                    stellarPerformanceIndicator = true;
                    stellarPerformancePulseRadius = 0;
                }


            }
            if (stellarPerformancePostPrep == true)
            {
                Player.AddBuff(BuffType<Buffs.StellarPerformance>(), 1);


            }
            if (stellarPerformancePostPrep == true)
            {

                if (PerformanceResourceCurrent < 0)
                {
                    stellarPerformanceSoundInstance?.Stop();
                    SoundEngine.PlaySound(StarsAboveAudio.SFX_SuistrumeFail, Player.Center);
                    Player.AddBuff(BuffType<Buffs.StellarPerformanceCooldown>(), 7200);

                    stellarPerformanceActive = false;
                    stellarPerformanceEnding = true;
                    stellarPerformancePostPrep = false;
                    PerformanceResourceCurrent = 0;

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
                stellarPerformanceCooldown = true;

            }
            else
            {
                stellarPerformanceCooldown = false;

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
                if (Player.buffType[i] == BuffType<DeifiedBuff>())
                {
                    if (Player.buffTime[i] == 1)
                    {
                        for (int ib = 0; ib < Main.maxNPCs; ++ib)
                        {
                            NPC npc = Main.npc[ib];
                            if (npc.active && npc.boss)
                            {
                                Player.AddBuff(BuffType<Mortality>(), 60*15);

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
                        player.screenShakeTimerGlobal = 0;
                        for (int i2 = 0; i2 < 70; i2++)
                        {

                            Vector2 vel = new Vector2(Main.rand.NextFloat(-9, 9), Main.rand.NextFloat(-9, 9));
                            int type = Main.rand.Next(new int[] { ProjectileID.NebulaArcanum, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze2, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.NebulaBlaze1, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.StarWrath, ProjectileID.Starfury, ProjectileID.Starfury, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.VenomBullet, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, ProjectileID.Meteor1, ProjectileID.Meteor2, ProjectileID.Meteor3, });

                            Projectile.NewProjectile(null, Player.Center, vel, type, Player.GetWeaponDamage(Player.HeldItem), 6, Player.whoAmI, 0, 1);
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

            if (Player.HasBuff(BuffType<Buffs.FlashOfEternity>()) && Player.whoAmI == Main.myPlayer)
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
                        NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, (float)Player.whoAmI, soulUnboundLocation.X, soulUnboundLocation.Y, 1, 0, 0);
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
                        Player.AddBuff(BuffType<Buffs.ArtificeSirenCooldown>(), 3600);//3600 is 1 min
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
                            NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, (float)Player.whoAmI, AshenAmbitionOldPosition.X, AshenAmbitionOldPosition.Y, 1, 0, 0);
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
                            otherPlayer.AddBuff(BuffType<Buffs.StellarListener>(), 2);
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
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)/* tModPorter Override ImmuneTo, FreeDodge or ConsumableDodge instead to prevent taking damage */
        {
            if (Player.HasBuff(BuffType<Buffs.Invincibility>()))
            {
                return false;
            }
            if (Player.HasBuff(BuffType<Mortality>()))
            {
                damage *= 2;
            }
            if (Player.HasBuff(BuffType<DeifiedBuff>()))
            {
                damage /= 2;
            }
            if (Player.HasBuff(BuffType<SpecialAttackBuff>()))
            {
                if(Main.rand.Next(0,101) <= 25)
                {
                    Player.immune = true;
                    Player.immuneTime = 30;
                    return false;
                }
                damage *= 3;
            }
            if(LVStacks > 0)
            {
                damage -= LVStacks;
                LVStacks = 0;
            }
            if(Player.HasBuff(BuffType<RealizedNanomachinaBuff>()))
            {
                if(nanomachinaShieldHP > 0)
                {
                    if (damage > nanomachinaShieldHP)
                    {
                        Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                        CombatText.NewText(textPos, new Color(122, 113, 153, 255), $"-{nanomachinaShieldHP}", false, false);

                        damage -= nanomachinaShieldHP;
                        Player.ClearBuff(BuffType<RealizedNanomachinaBuff>());
                        //The shield breaks!
                        for (int d = 0; d < 32; d++)
                        {
                            Dust.NewDust(Player.Center, 0, 0, DustID.Flare, 0f + Main.rand.Next(-12, 12), 0f + Main.rand.Next(-12, 12), 150, default(Color), 1f);
                        }
                        for (int d = 0; d < 12; d++)
                        {
                            Dust.NewDust(Player.Center, 0, 0, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-2, 2), 150, default(Color), 0.5f);
                        }
                        SoundEngine.PlaySound(SoundID.NPCHit34, Player.Center);

                    }
                    else
                    {
                        if (damage - nanomachinaShieldHP <= 0) //If shield is more than the damage...
                        {
                            Rectangle textPos = new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height);
                            CombatText.NewText(textPos, new Color(122, 113, 153, 255), $"-{damage}", false, false);
                            nanomachinaShieldHP -= damage;
                            //Mimic I-frames
                            Player.immune = true;
                            Player.immuneTime = 60;

                            //Clash sound effects and dust
                            for (int d = 0; d < 12; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-2, 2), 150, default(Color), 0.5f);
                            }
                            for (int d = 0; d < 12; d++)
                            {
                                Dust.NewDust(Player.Center, 0, 0, DustID.Flare, 0f + Main.rand.Next(-6, 6), 0f + Main.rand.Next(-2, 2), 150, default(Color), 0.5f);
                            }
                            SoundEngine.PlaySound(SoundID.NPCHit53, Player.Center);

                            return false;
                        }
                        else
                        {

                        }
                    }
                    
                }
            }
            if(Player.ownedProjectileCounts[ProjectileType<FragmentOfTimeMinion>()] >= 1 && !Player.HasBuff(BuffType<TimeBubbleCooldown>()))
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];

                    if (proj.owner == Player.whoAmI &&
                        (proj.type == ProjectileType<TimePulse>()) && proj.Distance(Player.Center) <= proj.ai[0] && proj.active)
                    {
                        Player.AddBuff(BuffType<Invincibility>(), 20);
                        Player.AddBuff(BuffType<TimeBubbleCooldown>(), 1200);
                        proj.Kill();
                        return false;
                    }
                }
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
            if (Player.HasBuff(BuffType<Bedazzled>()))//If the player has a Prismic...
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
            
            if (Player.HasBuff(BuffType<TimelessPotential>()))
            {
                if (damage > Player.statLife && !Player.HasBuff(BuffType<TimelessPotentialCooldown>()))
                {
                    Player.statLife = 50;
                    Player.AddBuff(BuffType<Invincibility>(), 120);
                    Player.AddBuff(BuffType<TimelessPotentialCooldown>(), 7200);
                    return false;
                }
                if (Main.rand.Next(0, 101) <= 10)
                {
                    Player.immuneTime = 30;
                    return false;
                }

            }
            
            if (euthymiaActive)
            {
                eternityGauge -= damage / 2;
            }
            
           

            if (Player.HasBuff(BuffType<Buffs.SakuraVengeance.SakuraEarthBuff>()) || Player.HasBuff(BuffType<Buffs.SakuraVengeance.ElementalChaos>()))
            {
                damage = (int)Math.Round(damage * 0.75);
            }

            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.ButterflyTrance>()))
            {
                damage /= 2;

                return true;
            }
            


            if (Player.HasBuff(BuffType<Buffs.DashInvincibility>()))
            {
                damage = 0;

                return true;
            }
            if (stellarPerformancePostPrep == true)
            {
                if (damage > 20)
                {
                    PerformanceResourceCurrent -= 20;

                }
                else
                {
                    PerformanceResourceCurrent -= damage;
                }
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
            if(Player.HasBuff(BuffType<AtrophiedDeifiedBuff>()))
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
            if (Main.LocalPlayer.HasBuff(BuffType<Buffs.LivingDead>()))
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
                      
            return true;
        }

        public override void OnRespawn()
        {



            if (luciferium)
            {
                Player.AddBuff(BuffID.PotionSickness, 3600);
            }
            base.OnRespawn(Player);

        }
        public override void ResetEffects()
        {


            KevesiFarewellInInventory = false;
            AgnianFarewellInInventory = false;

            SaltwaterScourgeHeld = false;

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
        private void OnKillEnemy(NPC npc)
        {
            if(Player.HasBuff(BuffType<RealizedNanomachinaBuff>()))
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
            activeMinions = (int)(Player.slotsMinions);

            //Values that reset out of combat
            if (player.inCombat < 0)
            {
                LVStacks--;
                if(LVStacks < 0)
                {
                    LVStacks = 0;
                }
                SoulReaverSouls = 0;
                //nanomachinaGauge = 0;
            }

            //Weapon PreUpdates
            CatalystMemory();
            OzmaAscendant();
            VermilionRiposte();
            CosmicDestroyer();
            ArchitectLuminance();
            Takonomicon(player);
            TwinStarsOfAlbiero(player);
            AshenAmbition();
            SunsetOfTheSunGod();
            VermilionDaemon();
            SupremeAuthority();
            SakuraVengenace();
            ArmamentsOfTheSkyStriker();
            StygianNymph();
            EyeOfEuthymia();
            PhantomInTheMirror();
            KroniicPrincipality();
            Nanomachina();

            //Radiance cap (used by certain weapons.)
            if (radiance > 10)
            {
                radiance = 10;
            }



        }
        private void SupremeAuthority()
        {
            if(!Player.HasBuff(BuffType<DeifiedBuff>()))
            {
                SupremeAuthorityConsumedNPCs = 0;
                
            }
            else
            {
                if (Player.ownedProjectileCounts[ProjectileType<Projectiles.SupremeAuthority.AuthorityLantern2>()] < 1)
                {
                    Projectile.NewProjectile(null, Player.Center.X, Player.Center.Y, 0, 0, ProjectileType<Projectiles.SupremeAuthority.AuthorityLantern2>(), 0, 0, Player.whoAmI, 0f);
                }
                if (Player.ownedProjectileCounts[ProjectileType<Projectiles.SupremeAuthority.AuthorityLantern1>()] < 1)
                {
                    Projectile.NewProjectile(null, Player.Center.X, Player.Center.Y, 0, 0, ProjectileType<Projectiles.SupremeAuthority.AuthorityLantern1>(), 0, 0, Player.whoAmI, 0f);
                }
            }
            if(SupremeAuthorityConsumedNPCs > 0)
            {
                Player.AddBuff(BuffType<DarkAura>(), 10);
            }
           
        }
        private void SunsetOfTheSunGod()
        {
            KarnaTarget = Vector2.Lerp(KarnaTarget, Main.MouseWorld, 0.01f);

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
                    Dust.NewDust(Player.Center, 0, 0, DustID.FireworkFountain_Yellow, 0f + Main.rand.Next(-12, 12), 0f + Main.rand.Next(-12, 12), 150, default(Color), 1f);
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
                    Player.AddBuff(BuffType<Buffs.KroniicPrincipalityCooldown>(), 3600);//7200 is 2 minutes
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

                    Dust d = Dust.NewDustPerfect(kroniicSavedPosition + offset, 20, Vector2.Zero, 200, default(Color), 0.7f);
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

            
           
        }

    }

};