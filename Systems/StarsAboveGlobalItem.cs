
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Items;
using StarsAbove.Items.Weapons;
using StarsAbove.Items.Weapons.Summon;
using StarsAbove.Items.Weapons.Ranged;
using StarsAbove.Items.Weapons.Other;
using StarsAbove.Items.Weapons.Celestial;
using StarsAbove.Items.Weapons.Melee;
using StarsAbove.Items.Weapons.Magic;
using StarsAbove.Items.Essences;
using StarsAbove.Prefixes;
using SubworldLibrary;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using StarsAbove.Items.Prisms;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent;
using StarsAbove.Items.Armor.StarfarerArmor;
using StarsAbove.Items.Materials;
using StarsAbove.Utilities;
using Terraria.UI.Chat;
using StarsAbove.Buffs;
using StarsAbove.Buffs.StellarNovas;
using StarsAbove.Items.Loot;
using System.Reflection.Metadata;
using StarsAbove.Items.Consumables;
using StarsAbove.Systems;

namespace StarsAbove.Systems
{

    public class StarsAboveGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            return base.Clone(item, itemClone);
        }

        public int oldType;// 1 is melee, 2 is ranged, 3 is magic, 4 is summon
        public DamageClass oldDamageClass;
        //public int aspect; //0 is Astral, 1 is Umbral, 2 is Dual Aspected.
        public bool spatialWeapon;
        public bool loadItem = true;

        public static List<int> AstralWeapons = new List<int>();
        public static List<int> UmbralWeapons = new List<int>();
        public static List<int> SpatialWeapons = new List<int>();
        public static List<int> Prisms = new List<int>();
        public static List<int> Outfits = new List<int>();
        public static List<int> GlowingItems = new List<int>();
        public static List<int> Essences = new List<int>();
        public static List<int> WeaponsUnaffectedByAspectedDamagePenalty = new List<int>();
        public static List<int> ScalingWeapons = new List<int>();

        public override void SetStaticDefaults()
        {
            AstralWeapons = new List<int>() {
                ItemType<CarianDarkMoon>(),
                ItemType<NeoDealmaker>(),
                ItemType<DerFreischutz>(),
                ItemType<Persephone>(),
                ItemType<Skofnung>(),
                ItemType<AegisDriver>(),
                ItemType<KarlanTruesilver>(),
                ItemType<IzanagiEdge>(),
                ItemType<VenerationOfButterflies>(),
                ItemType<RideTheBull>(),
                ItemType<CrimsonOutbreak>(),
                ItemType<StygianNymph>(),
                ItemType<CrimsonKey>(),
                ItemType<PhantomInTheMirror>(),
                ItemType<PleniluneGaze>(),
                ItemType<VisionOfEuthymia>(),
                ItemType<RexLapis>(),
                ItemType<LiberationBlazing>(),
                ItemType<Suistrume>(),
                ItemType<KeyOfTheKingsLaw>(),
                ItemType<HunterSymphony>(),
                ItemType<KevesiFarewell>(),
                ItemType<PodZero42>(),
                ItemType<GossamerNeedle>(),
                ItemType<DevotedHavoc>(),

			    //ModContent.ItemType<EssenceOf>(),
			    ItemType<EssenceOfTheDarkMoon>(),
                ItemType<EssenceOfBitterfrost>(),
                ItemType<EssenceOfButterflies>(),
                ItemType<EssenceOfDuality>(),
                ItemType<EssenceOfEuthymia>(),
                ItemType<EssenceOfIzanagi>(),
                ItemType<EssenceOfLiberation>(),
                ItemType<EssenceOfSilverAsh>(),
                ItemType<EssenceOfSin>(),
                ItemType<EssenceOfStarsong>(),
                ItemType<EssenceOfTheAegis>(),
                ItemType<EssenceOfTheAnomaly>(),
                ItemType<EssenceOfTheBull>(),
                ItemType<EssenceOfTheFreeshooter>(),
                ItemType<EssenceOfTheMoonlitAdepti>(),
                ItemType<EssenceOfThePhantom>(),
                ItemType<EssenceOfTheSwarm>(),
                ItemType<EssenceOfTheTreasury>(),
                ItemType<EssenceOfTheUnderworldGoddess>(),
                ItemType<EssenceOfTheUnyieldingEarth>(),
                ItemType<EssenceOfTheHunt>(),
                ItemType<EssenceOfFarewells>(),
                ItemType<EssenceOfTheAutomaton>(),
                ItemType<EssenceOfTheHallownest>(),
                ItemType<EssenceOfEnergy>(),

            };
            UmbralWeapons = new List<int>() {
                ItemType<KonpakuKatana>(),
                ItemType<AshenAmbition>(),
                ItemType<DeathInFourActs>(),
                ItemType<KazimierzSeraphim>(),
                ItemType<InugamiRipsaw>(),
                ItemType<RadGun>(),
                ItemType<EveryMomentMatters>(),
                ItemType<Hawkmoon>(),
                ItemType<MementoMuse>(),
                ItemType<Drachenlance>(),
                ItemType<VoiceOfTheFallen>(),
                ItemType<CaesuraOfDespair>(),
                ItemType<CrimsonSakuraAlpha>(),
                ItemType<HollowheartAlbion>(),
                ItemType<Tartaglia>(),
                ItemType<KroniicAccelerator>(),
                ItemType<YunlaiStiletto>(),
                ItemType<Unforgotten>(),
                ItemType<Naganadel>(),
                ItemType<LightUnrelenting>(),
                ItemType<SparkblossomBeacon>(),
                ItemType<IrminsulDream>(),
                ItemType<AgnianFarewell>(),
                ItemType<DraggedBelow>(),
                ItemType<SoliloquyOfSovereignSeas>(),

                ItemType<EssenceOfAlpha>(),
                ItemType<EssenceOfAsh>(),
                ItemType<EssenceOfAzakana>(),
                ItemType<EssenceOfDeathsApprentice>(),
                ItemType<EssenceOfDrivingThunder>(),
                ItemType<EssenceOfFingers>(),
                ItemType<EssenceOfIRyS>(),
                ItemType<EssenceOfLunarDominion>(),
                ItemType<EssenceOfPerfection>(),
                ItemType<EssenceOfStyle>(),
                ItemType<EssenceOfSurpassingLimits>(),
                ItemType<EssenceOfTheDragonslayer>(),
                ItemType<EssenceOfTheFallen>(),
                ItemType<EssenceOfTheGardener>(),
                ItemType<EssenceOfTheHarbinger>(),
                ItemType<EssenceOfTheHawkmoon>(),
                ItemType<EssenceOfTheHollowheart>(),
                ItemType<EssenceOfThePegasus>(),
                ItemType<EssenceOfTheSharpshooter>(),
                ItemType<EssenceOfTime>(),
                ItemType<EssenceOfStaticShock>(),
                ItemType<EssenceOfOffseeing>(),
                ItemType<EssenceOfTheVoid>(),
                ItemType<EssenceOfHydro>(),
                ItemType<EssenceOfNature>(),

            };
            SpatialWeapons = new List<int>(){
                ItemType<Apalistik>(),
                ItemType<MiserysCompany>(),
                ItemType<AncientBook>(),
                ItemType<LuminaryWand>(),
                ItemType<DreadnoughtChemtank>(),
                ItemType<ApalistikUpgraded>(),
                ItemType<Xenoblade>(),
                ItemType<SkyStrikerArms>(),
                ItemType<ForceOfNature>(),
                ItemType<Hullwrought>(),
                ItemType<PenthesileaMuse>(),
                ItemType<Kifrosse>(),
                ItemType<Genocide>(),
                ItemType<Mercy>(),
                ItemType<ArachnidNeedlepoint>(),
                ItemType<SakuraVengeance>(),
                ItemType<TheOnlyThingIKnowForReal>(),
                ItemType<TwinStars>(),
                ItemType<Ozma>(),
                ItemType<ClaimhSolais>(),
                ItemType<MorningStar>(),

                ItemType<EternalStar>(),
                ItemType<VermillionDaemon>(),
                ItemType<ShadowlessCerulean>(),
                ItemType<HullwroughtMKII>(),
                ItemType<IgnitionAstra>(),
                ItemType<BuryTheLight>(),
                ItemType<ArchitectLuminance>(),
                ItemType<CosmicDestroyer>(),
                ItemType<VirtuesEdge>(),
                ItemType<UltimaThule>(),
                ItemType<BloodBlade>(),
                ItemType<RedMage>(),
                ItemType<BurningDesire>(),
                ItemType<EverlastingPickaxe>(),
                ItemType<CatalystMemory>(),
                ItemType<ElCapitansHardware>(),
                ItemType<BlackSilenceWeapon>(),
                ItemType<SoulReaver>(),
                ItemType<GoldenKatana>(),
                ItemType<Manifestation>(),

			    //Stars Above v1.3
			    ItemType<Umbra>(),
                ItemType<SaltwaterScourge>(),
                ItemType<AdornmentOfTheChaoticGod>(),
                ItemType<Chronoclock>(),
                ItemType<KissOfDeath>(),

			    //Stars Above v1.4
			    ItemType<Nanomachina>(),
                ItemType<LevinstormAxe>(),
                ItemType<SanguineDespair>(),
                ItemType<SunsetOfTheSunGod>(),
                ItemType<ManiacalJustice>(),
                ItemType<SupremeAuthority>(),

			    //Stars Above v1.5
			    ItemType<DreamersInkwell>(),
                ItemType<BrilliantSpectrum>(),
                ItemType<ShockAndAwe>(),
                ItemType<TrickspinTwoStep>(),

                //Stars Above v2.0
                ItemType<DragaliaFound>(),
                ItemType<GundbitStaves>(),
                ItemType<Wavedancer>(),
                ItemType<RebellionBloodArthur>(),
                
                //Stars Above v2.1
                ItemType<LegendaryShield>(),
                ItemType<Wolvesbane>(),
                ItemType<WolvesbaneRearmed>(),
                ItemType<WolvesbaneAwakened>(),
                ItemType<CandiedSugarball>(),
                ItemType<OrbitalExpresswayPlush>(),
                ItemType<StringOfCurses>(),
                ItemType<Phasmasaber>(),
                ItemType<StarphoenixFunnel>(),
                ItemType<CloakOfAnArbiter>(),
                ItemType<TwoCrownBow>(),
                ItemType<InheritedCaseM4A1>(),
                ItemType<DreadmotherDarkIdol>(),
                ItemType<QuisUtDeus>(),
                ItemType<ParadiseLost>(),

                ItemType<EssenceOfAdagium>(),
                ItemType<EssenceOfBloodshed>(),
                ItemType<EssenceOfChemtech>(),
                ItemType<EssenceOfEternity>(),
                ItemType<EssenceOfFoxfire>(),
                ItemType<EssenceOfInk>(),
                ItemType<EssenceOfLuminance>(),
                ItemType<EssenceOfMisery>(),
                ItemType<EssenceOfOuterGods>(),
                ItemType<EssenceOfRadiance>(),
                ItemType<EssenceOfSakura>(),
                ItemType<EssenceOfTechnology>(),
                ItemType<EssenceOfTheAerialAce>(),
                ItemType<EssenceOfTheAscendant>(),
                ItemType<EssenceOfTheBeginningAndEnd>(),
                ItemType<EssenceOfExplosions>(),
                ItemType<EssenceOfTheBionis>(),
                ItemType<EssenceOfTheChimera>(),
                ItemType<EssenceOfTheCosmos>(),
                ItemType<EssenceOfTheFuture>(),
                ItemType<EssenceOfBlasting>(),
                ItemType<EssenceOfTheGunlance>(),
                ItemType<EssenceOfTheObservatory>(),
                ItemType<EssenceOfTheOcean>(),
                ItemType<EssenceOfTwinStars>(),
                ItemType<EssenceOfVampirism>(),
                ItemType<EssenceOfDestiny>(),
                ItemType<EssenceOfBlood>(),
                ItemType<EssenceOfLifethirsting>(),
                ItemType<EssenceOfBalance>(),
                ItemType<EssenceOfTheOverwhelmingBlaze>(),
                ItemType<EssenceOfTheAbyss>(),
                ItemType<EssenceOfTheRenegade>(),
                ItemType<EssenceOfQuantum>(),
                ItemType<EssenceOfSilence>(),
                ItemType<EssenceOfSouls>(),
                ItemType<EssenceOfGold>(),
                ItemType<EssenceOfMimicry>(),
                ItemType<EssenceOfTheTimeless>(),
                ItemType<EssenceOfPiracy>(),
                ItemType<EssenceOfAbsoluteChaos>(),
                ItemType<EssenceOfTheWatch>(),
                ItemType<EssenceOfTheBehemothTyphoon>(),
                ItemType<EssenceOfLightning>(),
                ItemType<EssenceOfNanomachines>(),
                ItemType<EssenceOfDespair>(),
                ItemType<EssenceOfMania>(),
                ItemType<EssenceOfSurya>(),
                ItemType<EssenceOfAuthority>(),
                ItemType<EssenceOfKinetics>(),
                ItemType<EssenceOfDreams>(),
                ItemType<EssenceOfTheSoldier>(),
                ItemType<EssenceOfSpinning>(),

                ItemType<EssenceOfKingslaying>(),
                ItemType<EssenceOfFirepower>(),
                ItemType<EssenceOfTheDragon>(),
                ItemType<EssenceOfDancingSeas>(),

                ItemType<EssenceOfTheShield>(),
                ItemType<EssenceOfWolves>(),
                ItemType<EssenceOfSugar>(),
                ItemType<EssenceOfCookies>(),
                ItemType<EssenceOfNecrosis>(),
                ItemType<EssenceOfChionicEnergy>(),
                ItemType<EssenceOfThePhoenix>(),
                ItemType<EssenceOfASingularity>(),
                ItemType<EssenceOfTheHuntress>(),
                ItemType<EssenceOfTheRifle>(),
                ItemType<EssenceOfTheDarkMaker>(),
                ItemType<EssenceOfTheStars>(),
                ItemType<EssenceOfTheWhiteNight>(),
            };
            Prisms = new List<int>() {

                ItemType<PrismaticCore>(),

                ItemType<AuricExaltPrism>(),
                ItemType<BloodyBanquetPrism>(),
                ItemType<CrescentMeteorPrism>(),
                ItemType<DeadbloomPrism>(),
                ItemType<DreadMechanicalPrism>(),
                ItemType<LucidDreamerPrism>(),
                ItemType<LuminousHallowPrism>(),
                ItemType<RoyalSunrisePrism>(),
                
            };
            WeaponsUnaffectedByAspectedDamagePenalty = new List<int>() {
                ItemType<ArchitectLuminance>(),
                ItemType<SkyStrikerArms>(),
                ItemType<SunsetOfTheSunGod>(),
                ItemType<DreadmotherDarkIdol>(),
            };
            Outfits = new List<int>() {
                ItemType<FaerieVoyagerAttire>(),
                ItemType<StellarCasualAttire>(),
                ItemType<AegisOfHopesLegacy>(),
                ItemType<CelestialPrincessGenesis>(),
                ItemType<FamiliarLookingAttire>(),
                ItemType<SeventhSigilAutumnAttire>(),
                ItemType<GarmentsOfWinterRainAttire>(),
                ItemType<RenegadeTechnomancerSynthweave>(),
            };
            ScalingWeapons = new List<int>() {
                ItemType<AncientBook>(),
                ItemType<Wolvesbane>(),
                ItemType<WolvesbaneAwakened>(),
                ItemType<BrilliantSpectrum>(),
                ItemType<Apalistik>(),
                ItemType<ApalistikUpgraded>(),
                ItemType<DragaliaFound>(),
            };
            GlowingItems = new List<int>() {
                ItemType<TotemOfLightEmpowered>(),
                ItemType<BlackSilenceWeapon>(),
                ItemType<AdornmentOfTheChaoticGod>(),
                ItemType<Chronoclock>(),
                ItemType<BrilliantSpectrum>(),
                ItemType<ProgenitorWish>(),
                ItemType<Phasmasaber>(),
                ItemType<ElectrumScissors>(),
            };
            Essences = new List<int>() {
                ItemType<EssenceOfLifethirsting>(),
                ItemType<EssenceOfTheDarkMoon>(),
                ItemType<EssenceOfAdagium>(),
                ItemType<EssenceOfBloodshed>(),
                ItemType<EssenceOfChemtech>(),
                ItemType<EssenceOfEternity>(),
                ItemType<EssenceOfFoxfire>(),
                ItemType<EssenceOfInk>(),
                ItemType<EssenceOfLuminance>(),
                ItemType<EssenceOfMisery>(),
                ItemType<EssenceOfOuterGods>(),
                ItemType<EssenceOfRadiance>(),
                ItemType<EssenceOfSakura>(),
                ItemType<EssenceOfTechnology>(),
                ItemType<EssenceOfTheAerialAce>(),
                ItemType<EssenceOfTheAscendant>(),
                ItemType<EssenceOfTheBeginningAndEnd>(),
                ItemType<EssenceOfExplosions>(),
                ItemType<EssenceOfTheBionis>(),
                ItemType<EssenceOfTheChimera>(),
                ItemType<EssenceOfTheCosmos>(),
                ItemType<EssenceOfTheFuture>(),
                ItemType<EssenceOfBlasting>(),
                ItemType<EssenceOfTheGunlance>(),
                ItemType<EssenceOfTheObservatory>(),
                ItemType<EssenceOfTheOcean>(),
                ItemType<EssenceOfTwinStars>(),
                ItemType<EssenceOfVampirism>(),
                ItemType<EssenceOfDestiny>(),
                ItemType<EssenceOfAlpha>(),
                ItemType<EssenceOfAsh>(),
                ItemType<EssenceOfAzakana>(),
                ItemType<EssenceOfDeathsApprentice>(),
                ItemType<EssenceOfDrivingThunder>(),
                ItemType<EssenceOfFingers>(),
                ItemType<EssenceOfIRyS>(),
                ItemType<EssenceOfLunarDominion>(),
                ItemType<EssenceOfPerfection>(),
                ItemType<EssenceOfStyle>(),
                ItemType<EssenceOfSurpassingLimits>(),
                ItemType<EssenceOfTheDragonslayer>(),
                ItemType<EssenceOfTheFallen>(),
                ItemType<EssenceOfTheGardener>(),
                ItemType<EssenceOfTheHarbinger>(),
                ItemType<EssenceOfTheHawkmoon>(),
                ItemType<EssenceOfTheHollowheart>(),
                ItemType<EssenceOfThePegasus>(),
                ItemType<EssenceOfTheSharpshooter>(),
                ItemType<EssenceOfTime>(),
                ItemType<EssenceOfStaticShock>(),
                ItemType<EssenceOfBitterfrost>(),
                ItemType<EssenceOfButterflies>(),
                ItemType<EssenceOfDuality>(),
                ItemType<EssenceOfEuthymia>(),
                ItemType<EssenceOfIzanagi>(),
                ItemType<EssenceOfLiberation>(),
                ItemType<EssenceOfSilverAsh>(),
                ItemType<EssenceOfSin>(),
                ItemType<EssenceOfStarsong>(),
                ItemType<EssenceOfTheAegis>(),
                ItemType<EssenceOfTheAnomaly>(),
                ItemType<EssenceOfTheBull>(),
                ItemType<EssenceOfTheFreeshooter>(),
                ItemType<EssenceOfTheMoonlitAdepti>(),
                ItemType<EssenceOfThePhantom>(),
                ItemType<EssenceOfTheSwarm>(),
                ItemType<EssenceOfTheTreasury>(),
                ItemType<EssenceOfTheUnderworldGoddess>(),
                ItemType<EssenceOfTheUnyieldingEarth>(),
                ItemType<EssenceOfTheHunt>(),
                ItemType<EssenceOfBlood>(),
                ItemType<EssenceOfBalance>(),
                ItemType<EssenceOfTheOverwhelmingBlaze>(),
                ItemType<EssenceOfTheAbyss>(),
                ItemType<EssenceOfTheRenegade>(),
                ItemType<EssenceOfQuantum>(),
                ItemType<EssenceOfSilence>(),
                ItemType<EssenceOfSouls>(),
                ItemType<EssenceOfGold>(),
                ItemType<EssenceOfFarewells>(),
                ItemType<EssenceOfOffseeing>(),
                ItemType<EssenceOfMimicry>(),
                ItemType<EssenceOfTheAutomaton>(),
                ItemType<EssenceOfNature>(),
                ItemType<EssenceOfTheTimeless>(),
                ItemType<EssenceOfPiracy>(),
                ItemType<EssenceOfAbsoluteChaos>(),
                ItemType<EssenceOfTheWatch>(),
                ItemType<EssenceOfDespair>(),
                ItemType<EssenceOfTheBehemothTyphoon>(),
                ItemType<EssenceOfLightning>(),
                ItemType<EssenceOfNanomachines>(),
                ItemType<EssenceOfMania>(),
                ItemType<EssenceOfSurya>(),
                ItemType<EssenceOfTheVoid>(),
                ItemType<EssenceOfTheHallownest>(),
                ItemType<EssenceOfKinetics>(),
                ItemType<EssenceOfTheSoldier>(),
                ItemType<EssenceOfSpinning>(),
                ItemType<EssenceOfDreams>(),
                ItemType<EssenceOfAuthority>(),

                ItemType<EssenceOfHydro>(),
                ItemType<EssenceOfFirepower>(),
                ItemType<EssenceOfKingslaying>(),
                ItemType<EssenceOfDancingSeas>(),
                ItemType<EssenceOfEnergy>(),
                ItemType<EssenceOfTheDragon>(),

                ItemType<EssenceOfTheShield>(),
                ItemType<EssenceOfWolves>(),
                ItemType<EssenceOfSugar>(),
                ItemType<EssenceOfCookies>(),
                ItemType<EssenceOfNecrosis>(),
                ItemType<EssenceOfChionicEnergy>(),
                ItemType<EssenceOfThePhoenix>(),
                ItemType<EssenceOfASingularity>(),
                ItemType<EssenceOfTheHuntress>(),
                ItemType<EssenceOfTheRifle>(),
                ItemType<EssenceOfTheDarkMaker>(),
                ItemType<EssenceOfTheStars>(),
                ItemType<EssenceOfTheWhiteNight>(),

            };
            base.SetStaticDefaults();
        }
  
        public static bool disableAspectPenalty;
        public static bool disableCalamityWeaponBuffs;
        public static bool disableWeaponRestriction = false;

        public override void SetDefaults(Item item)
        {
            //if (item.type == ItemID.CopperShortsword)
            //{  Here we make sure to only change Copper Shortsword by checking item.type in an if statement
            //item.damage = 50;       // Changed original CopperShortsword's damage to 50!
            //}



            if (item.DamageType == GetInstance<CelestialDamageClass>())
            {
                spatialWeapon = true;
            }

            oldDamageClass = item.DamageType;

            if (AstralWeapons.Contains(item.type))
            {

            }
            if (UmbralWeapons.Contains(item.type))
            {

            }
            if (SpatialWeapons.Contains(item.type))
            {

            }

            if (loadItem)
            {

                loadItem = false;
            }

        }

        public override bool PreDrawTooltipLine(Item item, DrawableTooltipLine line, ref int yOffset)
        {
            if (item.OriginalRarity == GetInstance<StellarRarity>().Type)
            {
                if (!line.OneDropLogo)
                {
                    // You are not allowed to change these, modders should use ModifyTooltips to modify them
                    //line.text = "you shall not pass...";
                    //line.oneDropLogo = false;
                    //line.color = Color.AliceBlue;
                    //line.overrideColor = Color.AliceBlue;
                    //line.isModifier = false;
                    //line.isModifierBad = false;
                    //line.index = 1;

                    // Let's draw the item name centered so it's in the middle, and let's add a form of separator
                    string sepText = "✧ ✦ ✧"; // This is our separator, which will go between the item name and the rest
                    float sepHeight = line.Font.MeasureString(sepText).Y; // Height of our separator

                    // If our line text equals our item name, this is our tooltip line for the item name
                    // if (line.text == item.HoverName)
                    // What is more accurate to check is the layer name and mod
                    if (line.Name == "ItemName" && line.Mod == "Terraria")
                    // We check for Terraria so we modify the vanilla tooltip and not a modded one
                    // This could be important, in case some mod does a lot of custom work and removes the standard tooltip
                    // For tooltip layers, check the documentation for TooltipLine
                    {
                        // Our offset is half the width of our box, minus the padding of one side
                        //float boxOffset = boxSize.X / 2 - paddingForBox;
                        // The X coordinate where we draw is where the line would draw, plus the box offset,
                        // which would place the START of the string at the center, so we subtract half of the line width to center it completely
                        float drawX = line.X + (int)line.Font.MeasureString(line.Text).X / 2 - line.Font.MeasureString(sepText).X / 2;
                        float drawY = line.Y + sepHeight / 2;

                        // Note how our line object has many properties we can use for drawing
                        // Here we draw the separator, note that it'd make more sense to use PostDraw for this, but either will work
                        ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, line.Font, sepText,
                            new Vector2(drawX, drawY), line.Color, line.Rotation, line.Origin, line.BaseScale, line.MaxWidth, line.Spread);

                        // Here we do the same thing as we did for drawX, which will center our ItemName tooltip
                        //line.X += (int)boxOffset - (int)line.Font.MeasureString(line.Text).X / 2;
                        // yOffset affects the offset that is added every next line, so this will cause the line to come after the separator to be drawn slightly lower
                        yOffset = (int)sepHeight / 4;
                    }
                    else
                    {
                        // Reset the offset for other lines
                        //float boxOffset = boxSize.X / 2 - paddingForBox;
                        //line.X = 0;
                        yOffset = 0;
                        //line.X += (int)boxOffset - (int)line.Font.MeasureString(line.Text).X / 2;
                    }
                }
            }
            if (item.OriginalRarity == GetInstance<StellarSpoilsRarity>().Type)
            {
                if (!line.OneDropLogo)
                {
                    // You are not allowed to change these, modders should use ModifyTooltips to modify them
                    //line.text = "you shall not pass...";
                    //line.oneDropLogo = false;
                    //line.color = Color.AliceBlue;
                    //line.overrideColor = Color.AliceBlue;
                    //line.isModifier = false;
                    //line.isModifierBad = false;
                    //line.index = 1;

                    // Let's draw the item name centered so it's in the middle, and let's add a form of separator
                    string sepText = "✦"; // This is our separator, which will go between the item name and the rest
                    float sepHeight = line.Font.MeasureString(sepText).Y; // Height of our separator

                    // If our line text equals our item name, this is our tooltip line for the item name
                    // if (line.text == item.HoverName)
                    // What is more accurate to check is the layer name and mod
                    if (line.Name == "ItemName" && line.Mod == "Terraria")
                    // We check for Terraria so we modify the vanilla tooltip and not a modded one
                    // This could be important, in case some mod does a lot of custom work and removes the standard tooltip
                    // For tooltip layers, check the documentation for TooltipLine
                    {
                        // Our offset is half the width of our box, minus the padding of one side
                        //float boxOffset = boxSize.X / 2 - paddingForBox;
                        // The X coordinate where we draw is where the line would draw, plus the box offset,
                        // which would place the START of the string at the center, so we subtract half of the line width to center it completely
                        float drawX = line.X + (int)line.Font.MeasureString(line.Text).X / 2 - line.Font.MeasureString(sepText).X / 2;
                        float drawY = line.Y + sepHeight / 2;

                        // Note how our line object has many properties we can use for drawing
                        // Here we draw the separator, note that it'd make more sense to use PostDraw for this, but either will work
                        ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, line.Font, sepText,
                            new Vector2(drawX, drawY), line.Color, line.Rotation, line.Origin, line.BaseScale, line.MaxWidth, line.Spread);

                        // Here we do the same thing as we did for drawX, which will center our ItemName tooltip
                        //line.X += (int)boxOffset - (int)line.Font.MeasureString(line.Text).X / 2;
                        // yOffset affects the offset that is added every next line, so this will cause the line to come after the separator to be drawn slightly lower
                        yOffset = (int)sepHeight / 4;
                    }
                    else
                    {
                        // Reset the offset for other lines
                        //float boxOffset = boxSize.X / 2 - paddingForBox;
                        //line.X = 0;
                        yOffset = 0;
                        //line.X += (int)boxOffset - (int)line.Font.MeasureString(line.Text).X / 2;
                    }
                }
            }
            return true;
        }
        public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (Prisms.Contains(item.type) || Outfits.Contains(item.type) || GlowingItems.Contains(item.type) || Essences.Contains(item.type))
            {
                Texture2D texture = TextureAssets.Item[item.type].Value;
                for (int i = 0; i < 4; i++)
                {
                    Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                    spriteBatch.Draw(texture, position + offsetPositon, null, Main.DiscoColor, 0, origin, scale, SpriteEffects.None, 0f);
                }

                float time = Main.GlobalTimeWrappedHourly;
                float timer = item.timeSinceItemSpawned / 240f + time * 0.04f;

                time %= 4f;
                time /= 2f;

                if (time >= 1f)
                {
                    time = 2f - time;
                }

                time = time * 0.5f + 0.5f;

                for (float i = 0f; i < 1f; i += 0.25f)
                {
                    float radians = (i + timer) * MathHelper.TwoPi;

                    Main.spriteBatch.Draw(texture, position + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(90, 70, 255, 50), 0, origin, scale, SpriteEffects.None, 0);
                }

                for (float i = 0f; i < 1f; i += 0.34f)
                {
                    float radians = (i + timer) * MathHelper.TwoPi;

                    Main.spriteBatch.Draw(texture, position + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(140, 120, 255, 77), 0, origin, scale, SpriteEffects.None, 0);

                }



                return true;
            }
            return true;
        }
        public override bool PreDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            if (Prisms.Contains(item.type) || Outfits.Contains(item.type) || GlowingItems.Contains(item.type) || Essences.Contains(item.type))
            {
                // Draw the periodic glow effect behind the item when dropped in the world (hence PreDrawInWorld)
                Texture2D texture = TextureAssets.Item[item.type].Value;

                Rectangle frame;

                if (Main.itemAnimations[item.type] != null)
                {
                    // In case this item is animated, this picks the correct frame
                    frame = Main.itemAnimations[item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
                }
                else
                {
                    frame = texture.Frame();
                }

                Vector2 frameOrigin = frame.Size() / 2f;
                Vector2 offset = new Vector2(item.width / 2 - frameOrigin.X, item.height - frame.Height);
                Vector2 drawPos = item.position - Main.screenPosition + frameOrigin + offset;
                for (int i = 0; i < 4; i++)
                {
                    Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                    spriteBatch.Draw(texture, drawPos + offsetPositon, null, Main.DiscoColor, 0, frameOrigin, scale, SpriteEffects.None, 0f);
                }
                float time = Main.GlobalTimeWrappedHourly;
                float timer = item.timeSinceItemSpawned / 240f + time * 0.04f;

                time %= 4f;
                time /= 2f;

                if (time >= 1f)
                {
                    time = 2f - time;
                }

                time = time * 0.5f + 0.5f;

                for (float i = 0f; i < 1f; i += 0.25f)
                {
                    float radians = (i + timer) * MathHelper.TwoPi;

                    spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(90, 70, 255, 50), rotation, frameOrigin, scale, SpriteEffects.None, 0);
                }

                for (float i = 0f; i < 1f; i += 0.34f)
                {
                    float radians = (i + timer) * MathHelper.TwoPi;

                    spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(140, 120, 255, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
                }


                return true;
            }

            return true;
        }
        public override void AddRecipes()
        {

        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            
            if (item.OriginalRarity == GetInstance<StellarSpoilsRarity>().Type && item.type != ItemType<StellarRemnant>() && item.type != ItemType<StellarSpoils>())
            {
                TooltipLine tooltip = new TooltipLine(Mod, "StarsAbove: StellarSpoils", LangHelper.GetTextValue("Common.CanBeShimmeredSpoils")) { OverrideColor = Color.White };
                tooltips.Add(tooltip);
                //
            }

            if (item.ModItem?.Mod == ModLoader.GetMod("StarsAbove") && item.damage > 0)
            {


            }

            //if (tooltip.Name.Equals("Tooltip0"))



        }

        public DamageClass getOldDamageClass()
        {
            return oldDamageClass;
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            oldDamageClass = item.GetGlobalItem<StarsAboveGlobalItem>().getOldDamageClass();
            //damage += 0.2f;
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                


            }
            //If the weapon is from stars above or aprismatism is active, and the weapon deals damage...
            if ((item.ModItem?.Mod == ModLoader.GetMod("StarsAbove") || player.GetModPlayer<StarsAbovePlayer>().aprismatism == 2) && item.damage > 0)
            { //
                if (item.DamageType != GetInstance<CelestialDamageClass>())
                {
                    if (player.GetModPlayer<StarsAbovePlayer>().RogueAspect == 2)
                    {
                        if (ModLoader.TryGetMod("CalamityMod", out Mod calamityModX))
                        {
                            if (oldDamageClass != calamityMod.Find<DamageClass>("RogueDamageClass"))
                            {
                                if (ModLoader.TryGetMod("CalamityMod", out Mod calamityModx))
                                {
                                    damage = player.GetTotalDamage(calamityMod.Find<DamageClass>("RogueDamageClass"));

                                }

                                if (!disableAspectPenalty)
                                {
                                    damage -= 0.1f;
                                }

                            }
                            else
                            {

                            }
                        }

                    }
                    else if (player.GetModPlayer<StarsAbovePlayer>().BardAspect == 2)
                    {
                        if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
                        {
                            if (oldDamageClass != thoriumMod.Find<DamageClass>("BardDamage"))
                            {
                                damage = player.GetTotalDamage(thoriumMod.Find<DamageClass>("BardDamage"));

                                if (!disableAspectPenalty)
                                {
                                    damage -= 0.1f;
                                }

                            }
                            else
                            {

                            }
                        }

                    }
                    else if(player.GetModPlayer<StarsAbovePlayer>().HealerAspect == 2)
                    {
                        if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
                        {
                            if (oldDamageClass != thoriumMod.Find<DamageClass>("HealerDamage"))
                            {
                                damage = player.GetTotalDamage(thoriumMod.Find<DamageClass>("HealerDamage"));

                                if (!disableAspectPenalty)
                                {
                                    damage -= 0.1f;
                                }

                            }
                            else
                            {

                            }
                        }

                    }
                    else if(player.GetModPlayer<StarsAbovePlayer>().ThrowerAspect == 2)
                    {
                        if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
                        {
                            if (oldDamageClass != DamageClass.Throwing)
                            {
                                damage = player.GetTotalDamage(DamageClass.Throwing);

                                if (!disableAspectPenalty)
                                {
                                    damage -= 0.1f;
                                }

                            }
                            else
                            {

                            }
                        }

                    }

                    //if the player is melee aspected
                    else if(player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
                    {

                        
                        //if the old damage class is not melee
                        if (oldDamageClass != DamageClass.Melee && oldDamageClass != DamageClass.MeleeNoSpeed)
                        {
                            //damage is the total damage of melee? might need changing to fix scaling.
                            damage = player.GetTotalDamage(DamageClass.Melee);

                            //reduce damage if aspect penalty is false.
                            if (!disableAspectPenalty)
                            {
                                damage -= 0.15f;
                            }

                        }
                        else
                        {

                        }

                    }
                    else if(player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
                    {
                        if (oldDamageClass != DamageClass.Magic && oldDamageClass != DamageClass.MagicSummonHybrid)
                        {
                            damage = player.GetTotalDamage(DamageClass.Magic);
                            if (!disableAspectPenalty)
                            {
                                damage -= 0.1f;
                            }
                        }
                        else
                        {

                        }


                    }
                    else if(player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
                    {
                        if (oldDamageClass != DamageClass.Ranged)
                        {
                            damage = player.GetTotalDamage(DamageClass.Ranged);
                            if (!disableAspectPenalty)
                            {
                                damage -= 0.1f;
                            }
                        }
                        else
                        {

                        }
                    }
                    else if(player.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
                    {
                        if (oldDamageClass != DamageClass.Summon && oldDamageClass != DamageClass.MagicSummonHybrid && oldDamageClass != DamageClass.SummonMeleeSpeed)
                        {
                            damage = player.GetTotalDamage(DamageClass.Summon);

                            if (!disableAspectPenalty)
                            {
                                damage -= 0.1f;
                            }
                        }
                        else
                        {

                        }
                    }
                }


            }

            if (!disableAspectPenalty && 
                ((player.HasBuff(BuffType<BearerOfLight>()) || (player.HasBuff(BuffType<BearerOfDarkness>())) && player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer == 2)))
            {
                if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
                {
                    damage += 0.15f;

                }
                else
                {
                    damage += 0.1f;

                }
            }
            //TODO: create a list with weapons unaffected by penalty
            if (player.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2 || player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2 || player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
            {
                if(WeaponsUnaffectedByAspectedDamagePenalty.Contains(item.type) && !disableAspectPenalty)
                {
                    damage += 0.1f;
                }
                
            }
            if (player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2 || player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
            {
                if ((item.ModItem is Phasmasaber || item.ModItem is Wolvesbane || item.ModItem is WolvesbaneAwakened || item.ModItem is WolvesbaneRearmed) && !disableAspectPenalty) //Ranged weapons
                {
                    damage += 0.1f;
                }

            }
            if (player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2 || player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
            {
                if ((item.ModItem is LevinstormAxe || item.ModItem is GossamerNeedle) && !disableAspectPenalty) //Ranged weapons
                {
                    damage += 0.1f;
                }

            }
            if (player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2 || player.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
            {
                if (item.ModItem is Hawkmoon && !disableAspectPenalty) //Ranged weapons
                {
                    damage += 0.1f;
                }

            }
            if (player.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2 || player.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2 || player.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
            {
                if (item.ModItem is CosmicDestroyer && !disableAspectPenalty) //Ranged weapons
                {
                    damage += 0.1f;
                }

            }
            if (player.GetModPlayer<WeaponPlayer>().PerfectlyGenericAccessory && item.ModItem?.Mod == ModLoader.GetMod("StarsAbove") && item.damage > 0)
            {
                damage += 0.08f;
            }


        }
        public override void HoldItem(Item item, Player player)
        {
            if ((item.ModItem?.Mod == ModLoader.GetMod("StarsAbove") || player.GetModPlayer<StarsAbovePlayer>().aprismatism == 2) && item.damage > 0 && item.DamageType != GetInstance<CelestialDamageClass>())
            { //

                if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MeleeAspect == 2)
                {
                    item.DamageType = DamageClass.Melee;

                }
                if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().MagicAspect == 2)
                {

                    item.DamageType = DamageClass.Magic;

                }
                if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RangedAspect == 2)
                {

                    item.DamageType = DamageClass.Ranged;

                }
                if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().SummonAspect == 2)
                {

                    item.DamageType = DamageClass.Summon;

                }
                if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().RogueAspect == 2)
                {
                    if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
                    {

                        item.DamageType = calamityMod.Find<DamageClass>("RogueDamageClass");

                    }

                }
                if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().BardAspect == 2)
                {
                    if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
                    {

                        item.DamageType = thoriumMod.Find<DamageClass>("BardDamage");

                    }

                }
                if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().HealerAspect == 2)
                {
                    if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
                    {

                        item.DamageType = thoriumMod.Find<DamageClass>("HealerDamage");

                    }

                }
                if (Main.LocalPlayer.GetModPlayer<StarsAbovePlayer>().ThrowerAspect == 2)
                {
                    if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
                    {

                        item.DamageType = DamageClass.Throwing;

                    }

                }


            }
            

            base.HoldItem(item, player);
        }
        public override void UpdateInventory(Item item, Player player)
        {
            if (item.ModItem?.Mod == ModLoader.GetMod("StarsAbove") || player.GetModPlayer<StarsAbovePlayer>().aprismatism == 2)
            {

            }
            if(oldDamageClass != null)
            {
                item.DamageType = oldDamageClass;

            }
        }


        public override bool CanUseItem(Item item, Player player)
        {


            if (player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer != 1 && player.whoAmI == Main.myPlayer && item.ModItem?.Mod == ModLoader.GetMod("StarsAbove"))
            {
                if (AstralWeapons.Contains(item.type) && !disableWeaponRestriction)//Eridani
                {
                    if (item.type == ItemType<KevesiFarewell>())
                    {
                        return true;
                    }

                    //Add this to localization later.($"Common.DiskReady")
                    if (Main.netMode != NetmodeID.Server) { Main.NewText(LangHelper.GetTextValue($"Common.AstralLocked"), 241, 255, 180); }
                    player.itemTime = 60;

                    return false;
                }
            }
            if (player.GetModPlayer<StarsAbovePlayer>().chosenStarfarer != 2 && player.whoAmI == Main.myPlayer && item.ModItem?.Mod == ModLoader.GetMod("StarsAbove"))
            {
                if (UmbralWeapons.Contains(item.type) && !disableWeaponRestriction)//Asphodene
                {
                    if (item.type == ItemType<AgnianFarewell>())
                    {
                        return true;
                    }

                    if (Main.netMode != NetmodeID.Server) { Main.NewText(LangHelper.GetTextValue($"Common.UmbralLocked"), 241, 255, 180); }
                    player.itemTime = 60;

                    return false;
                }
            }
            if (item.type == ItemID.RodofDiscord
                || item.type == ItemID.RodOfHarmony
                || item.type == ItemID.DirtBomb
                || item.type == ItemID.RopeCoil
                || item.type == ItemID.SilkRopeCoil
                || item.type == ItemID.VineRopeCoil
                || item.type == ItemID.WebRopeCoil
                || item.type == ItemID.WetBomb
                || item.type == ItemID.TeleportationPotion
                || item.type == ItemID.Clentaminator
                || item.type == ItemID.LavaBomb
                || item.type == ItemID.HoneyBomb
                || item.type == ItemID.DirtStickyBomb
                )
            {
                if (SubworldSystem.Current != null)
                {
                    return false;
                }

            }
            if (item.type == ItemID.RodofDiscord
                || item.type == ItemID.RodOfHarmony

                )
            {
                if (player.HasBuff(BuffType<EverlastingLight>()))
                {
                    return false;
                }

            }
            if (player.HasBuff(BuffID.DrillMount) && item.ModItem?.Mod == ModLoader.GetMod("StarsAbove"))
            {
                return false;
            }
            return base.CanUseItem(item, player);
        }

    }
}