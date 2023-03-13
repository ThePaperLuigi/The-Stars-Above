using Terraria.Audio;
using Terraria.ModLoader;

namespace StarsAbove
{
    public class StarsAboveAudio : ModSystem
	{
        #region Starfarer Voice Lines

        //Story Voice Lines
        #region Story Voice Lines

        #endregion

        //Stellar Nova Lines
        #region Asphodene Voice Lines
        /*public static readonly SoundStyle AsphodeneNovaLines = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/AN0")
		{
			Variants = new[]
            {
				0,2,3,4,5
            }
		};*/ //This is a good system, but in order to sync the voice lines with the dialogue box, I can't use it.

        public static readonly SoundStyle AN1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AN1")
		{

		};
		public static readonly SoundStyle AN2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AN2")
		{

		};
		public static readonly SoundStyle AN3 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AN3")
		{

		};
		public static readonly SoundStyle AN4 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AN4")
		{

		};
		public static readonly SoundStyle AN5 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AN5")
		{

		};
		public static readonly SoundStyle AN6 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AN6")
		{

		};
		public static readonly SoundStyle AN7 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AN7")
		{

		};
		public static readonly SoundStyle AN8 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AN8")
		{

		};
		public static readonly SoundStyle AN9 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AN9")
		{

		};
		public static readonly SoundStyle AN10 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AN10")
		{

		};

		public static readonly SoundStyle ANSpecial1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/UniqueLines/ANSpecial1")
		{

		};
		public static readonly SoundStyle ANSpecial2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/UniqueLines/ANSpecial2")
		{

		};
		public static readonly SoundStyle ANSpecial3 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/UniqueLines/ANSpecial3")
		{

		};
		public static readonly SoundStyle ANSpecial4 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/UniqueLines/ANSpecial4")
		{

		};
		public static readonly SoundStyle ANSpecial5 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/UniqueLines/ANSpecial5")
		{

		};


		#endregion
		#region Eridani Voice Lines
		public static readonly SoundStyle EN1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/EN1")
		{

		};
		public static readonly SoundStyle EN2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/EN2")
		{

		};
		public static readonly SoundStyle EN3 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/EN3")
		{

		};
		public static readonly SoundStyle EN4 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/EN4")
		{

		};
		public static readonly SoundStyle EN5 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/EN5")
		{

		};
		public static readonly SoundStyle EN6 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/EN6")
		{

		};
		public static readonly SoundStyle EN7 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/EN7")
		{

		};
		public static readonly SoundStyle EN8 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/EN8")
		{

		};
		public static readonly SoundStyle EN9 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/EN9")
		{

		};
		public static readonly SoundStyle EN10 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/EN10")
		{

		};

		public static readonly SoundStyle ENSpecial1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/UniqueLines/ENSpecial1")
		{

		};
		public static readonly SoundStyle ENSpecial2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/UniqueLines/ENSpecial2")
		{

		};
		public static readonly SoundStyle ENSpecial3 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/UniqueLines/ENSpecial3")
		{

		};
		public static readonly SoundStyle ENSpecial4 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/UniqueLines/ENSpecial4")
		{

		};
		public static readonly SoundStyle ENSpecial5 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/UniqueLines/ENSpecial5")
		{

		};
		#endregion

		#endregion

		#region Boss Voice Lines

		#region Vagrant Voice Lines

		#endregion

		#region Nalhaun Voice Lines
		public static readonly SoundStyle Nalhaun_AndNowTheScalesWillTip = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/AndNowTheScalesWillTip")
		{

		};
		public static readonly SoundStyle Nalhaun_AThousandBolts = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/AThousandBolts")
		{

		};
		public static readonly SoundStyle Nalhaun_ComeShowMeMore = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/ComeShowMeMore")
		{

		};
		public static readonly SoundStyle Nalhaun_EscapeIsNotSoEasilyGranted = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/EscapeIsNotSoEasilyGranted")
		{

		};
		public static readonly SoundStyle Nalhaun_EvenTheStrongestShields = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/EvenTheStrongestShields")
		{

		};
		public static readonly SoundStyle Nalhaun_Fools = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/Fools")
		{

		};
		public static readonly SoundStyle Nalhaun_MyDefenses = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/MyDefenses")
		{

		};
		public static readonly SoundStyle Nalhaun_NalhaunDeathQuote = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/NalhaunDeathQuote")
		{

		};
		public static readonly SoundStyle Nalhaun_NalhaunIntroQuote = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/NalhaunIntroQuote")
		{

		};
		public static readonly SoundStyle Nalhaun_PityDisplay = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/PityDisplay")
		{

		};
		public static readonly SoundStyle Nalhaun_RuinationIsCome = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/RuinationIsCome")
		{

		};
		public static readonly SoundStyle Nalhaun_TheGodsWillNotBeWatching = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/TheGodsWillNotBeWatching")
		{

		};
		public static readonly SoundStyle Nalhaun_TheHeartsOfMen = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/TheHeartsOfMen")
		{

		};
		public static readonly SoundStyle Nalhaun_UponMyHolyBlade = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/UponMyHolyBlade")
		{

		};
		public static readonly SoundStyle Nalhaun_WereYouExpectingRust = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/WereYouExpectingRust")
		{

		};
		#endregion

		#region Penthesilea Voice Lines
		public static readonly SoundStyle Penthesilea_AlrightMyTurn = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/AlrightMyTurn")
		{

		};
		public static readonly SoundStyle Penthesilea_CutThrough = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/CutThrough")
		{

		};
		public static readonly SoundStyle Penthesilea_Enough = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/Enough")
		{

		};
		public static readonly SoundStyle Penthesilea_HowFoolish = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/HowFoolish")
		{

		};
		public static readonly SoundStyle Penthesilea_HowFun = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/HowFun")
		{

		};
		public static readonly SoundStyle Penthesilea_INeedntHoldBack = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/INeedntHoldBack")
		{

		};
		public static readonly SoundStyle Penthesilea_ISenseTheirResolve = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/ISenseTheirResolve")
		{

		};
		public static readonly SoundStyle Penthesilea_IveEnduredFarWorse = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/IveEnduredFarWorse")
		{

		};
		public static readonly SoundStyle Penthesilea_OutOfMyWay = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/OutOfMyWay")
		{

		};
		public static readonly SoundStyle Penthesilea_PenthLaugh = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/PenthLaugh")
		{

		};
		public static readonly SoundStyle Penthesilea_QuickQuick = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/QuickQuick")
		{

		};
		public static readonly SoundStyle Penthesilea_ThisllCheerMeUp = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/ThisllCheerMeUp")
		{

		};
		public static readonly SoundStyle Penthesilea_ToPieces = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/ToPieces")
		{

		};
		public static readonly SoundStyle Penthesilea_ToShreds = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/ToShreds")
		{

		};
		public static readonly SoundStyle Penthesilea_TryAgainIDareYou = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/TryAgainIDareYou")
		{

		};
		public static readonly SoundStyle Penthesilea_WithHaste = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/WithHaste")
		{

		};
		public static readonly SoundStyle Penthesilea_WhateverItTakes = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/WhateverItTakes")
		{

		};
		#endregion

		#region Arbitration Voice Lines
		public static readonly SoundStyle Arbitration_ABlightTakesThisLand = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Arbitration/ABlightTakesThisLand")
		{

		};
		public static readonly SoundStyle Arbitration_ArbiterGrunt = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Arbitration/ArbiterGrunt")
		{

		};
		public static readonly SoundStyle Arbitration_ArbiterLaugh = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Arbitration/ArbiterLaugh")
		{
			Variants = new[]
			{
				1,2,3,4
			}
		};
		
		public static readonly SoundStyle Arbitration_DepriveThemOfLife = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Arbitration/DepriveThemOfLife")
		{

		};
		public static readonly SoundStyle Arbitration_DespairInOurPresence = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Arbitration/DespairInOurPresence")
		{

		};
		public static readonly SoundStyle Arbitration_FateCanNotBeAverted = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Arbitration/FateCanNotBeAverted")
		{

		};
		public static readonly SoundStyle Arbitration_FulfillDestiny = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Arbitration/FulfillDestiny")
		{

		};
		public static readonly SoundStyle Arbitration_LongHaveWeWaited = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Arbitration/LongHaveWeWaited")
		{

		};
		public static readonly SoundStyle Arbitration_Oblivion = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Arbitration/Oblivion")
		{

		};
		public static readonly SoundStyle Arbitration_PierceTheVeil = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Arbitration/PierceTheVeil")
		{

		};
		public static readonly SoundStyle Arbitration_SoItMustBe = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Arbitration/SoItMustBe")
		{

		};
		public static readonly SoundStyle Arbitration_TheEndOfDaysDrawsNear = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Arbitration/TheEndOfDaysDrawsNear")
		{

		};
		public static readonly SoundStyle Arbitration_TheFirstAreWeTheLastAreWe = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Arbitration/TheFirstAreWeTheLastAreWe")
		{

		};
		public static readonly SoundStyle Arbitration_TheyAreRightToFear = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Arbitration/TheyAreRightToFear")
		{

		};
		public static readonly SoundStyle Arbitration_WasIsAndWillForeverBe = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Arbitration/WasIsAndWillForeverBe")
		{

		};
		public static readonly SoundStyle Arbitration_WeTranscendTime = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Arbitration/WeTranscendTime")
		{

		};
		
		#endregion

		#region Warrior Of Light Voice Lines
		public static readonly SoundStyle WarriorOfLight_AFeebleShieldProtectsNothing = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/AFeebleShieldProtectsNothing")
		{

		};
		public static readonly SoundStyle WarriorOfLight_AnswerMyCall = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/AnswerMyCall")
		{

		};
		public static readonly SoundStyle WarriorOfLight_BegoneSpawnOfShadow = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/BegoneSpawnOfShadow")
		{

		};
		public static readonly SoundStyle WarriorOfLight_CladInPrayerIAmInvincible = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/CladInPrayerIAmInvincible")
		{

		};
		public static readonly SoundStyle WarriorOfLight_ComeShowMeYourStrength = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/ComeShowMeYourStrength")
		{

		};
		public static readonly SoundStyle WarriorOfLight_DarknessMustBeDestroyed = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/DarknessMustBeDestroyed")
		{

		};
		public static readonly SoundStyle WarriorOfLight_Fall = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/Fall")
		{

		};
		public static readonly SoundStyle WarriorOfLight_FinalPhaseGrunt = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/FinalPhaseGrunt")
		{

		};
		public static readonly SoundStyle WarriorOfLight_ForVictoryIRenderUpMyAll = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/ForVictoryIRenderUpMyAll")
		{

		};
		public static readonly SoundStyle WarriorOfLight_GleamingSteelLightMyPath = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/GleamingSteelLightMyPath")
		{

		};
		public static readonly SoundStyle WarriorOfLight_HaveAtYou = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/HaveAtYou")
		{

		};
		public static readonly SoundStyle WarriorOfLight_HopeGrantMeStrength = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/HopeGrantMeStrength")
		{

		};
		public static readonly SoundStyle WarriorOfLight_IAmSalvationGivenForm = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/IAmSalvationGivenForm")
		{

		};
		public static readonly SoundStyle WarriorOfLight_ItsTimeWeSettledThis = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/ItsTimeWeSettledThis")
		{

		};
		public static readonly SoundStyle WarriorOfLight_IveToyedWithYouLongEnough = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/IveToyedWithYouLongEnough")
		{

		};
		public static readonly SoundStyle WarriorOfLight_IWillNotFall = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/IWillNotFall")
		{

		};
		public static readonly SoundStyle WarriorOfLight_IWillStrikeYouDown = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/IWillStrikeYouDown")
		{

		};
		public static readonly SoundStyle WarriorOfLight_LetsTrySomethingElse = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/LetsTrySomethingElse")
		{

		};
		public static readonly SoundStyle WarriorOfLight_LightClaimYou = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/LightClaimYou")
		{

		};
		public static readonly SoundStyle WarriorOfLight_MankindsFirstHeroAndHisFinalHope = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/MankindsFirstHeroAndHisFinalHope")
		{

		};
		public static readonly SoundStyle WarriorOfLight_MySoulKnowsNoSurrender = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/MySoulKnowsNoSurrender")
		{

		};
		public static readonly SoundStyle WarriorOfLight_RadiantBraver = new ($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/RadiantBraver")
		{

		};
		public static readonly SoundStyle WarriorOfLight_RefulgentEther = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/RefulgentEther")
		{

		};
		public static readonly SoundStyle WarriorOfLight_TheBitterEnd = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/TheBitterEnd")
		{

		};
		public static readonly SoundStyle WarriorOfLight_TheGameIsUp = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/TheGameIsUp")
		{

		};
		public static readonly SoundStyle WarriorOfLight_TheLightWillCleanseYourSins = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/TheLightWillCleanseYourSins")
		{

		};
		public static readonly SoundStyle WarriorOfLight_ToMeWarriorsOfLight = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/ToMeWarriorsOfLight")
		{

		};
		public static readonly SoundStyle WarriorOfLight_WarriorOfLightDeathQuote = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/WarriorOfLightDeathQuote")
		{

		};
		public static readonly SoundStyle WarriorOfLight_WarriorOfLightDefeated = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/WarriorOfLightDefeated")
		{

		};
		public static readonly SoundStyle WarriorOfLight_WarriorOfLightIntroQuote = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/WarriorOfLightIntroQuote")
		{

		};
		public static readonly SoundStyle WarriorOfLight_YourDemiseShallBeOurSalvation = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/YourDemiseShallBeOurSalvation")
		{

		};
		public static readonly SoundStyle WarriorOfLight_YoureNoMatchForMe = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/YoureNoMatchForMe")
		{

		};
		public static readonly SoundStyle WarriorOfLight_YoureNotGoingAnywhere = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/YoureNotGoingAnywhere")
		{

		};
		public static readonly SoundStyle WarriorOfLight_YourLifeIsMineForTheTaking = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/YourLifeIsMineForTheTaking")
		{

		};
		public static readonly SoundStyle WarriorOfLight_YouStillStand = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/YouStillStand")
		{

		};

		#endregion

		#region Tsukiyomi Voice Lines
		public static readonly SoundStyle Tsukiyomi_DeathOfAThousandStars = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/ThousandStars")
		{

		};
		public static readonly SoundStyle Tsukiyomi_Struggle = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/Struggle")
		{

		};
		public static readonly SoundStyle Tsukiyomi_NowhereYouCanRun = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/NowhereYouCanRun")
		{

		};
		public static readonly SoundStyle Tsukiyomi_AfraidOfTheDark = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/AfraidOfTheDark")
		{

		};
		public static readonly SoundStyle Tsukiyomi_TryHarder = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/TryHarderThanThat")
		{

		};
		public static readonly SoundStyle Tsukiyomi_ForgettingSomething = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/ForgettingSomething")
		{

		};
		public static readonly SoundStyle Tsukiyomi_Journey = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/EndOfYourJourney")
		{

		};
		public static readonly SoundStyle Tsukiyomi_Stronger = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/StrongerThanIHoped")
		{

		};
		public static readonly SoundStyle Tsukiyomi_Insignificant = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/Insignificant")
		{

		};
		//Aspected weapon voice lines.
		public static readonly SoundStyle Tsukiyomi_CarianDarkMoon = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/CarianDarkMoon")
		{

		};
		public static readonly SoundStyle Tsukiyomi_BuryTheLight = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/BuryTheLight")
		{

		};
		public static readonly SoundStyle Tsukiyomi_TheOnlyThingIKnowForReal = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/TheOnlyThingIKnowForReal")
		{

		};
		public static readonly SoundStyle Tsukiyomi_VoiceOfTheOutbreak = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/VoiceOfTheOutbreak")
		{

		};
		public static readonly SoundStyle Tsukiyomi_ShadowlessCerulean = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/ShadowlessCerulean")
		{

		};
		public static readonly SoundStyle Tsukiyomi_DeathInFourActs = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/DeathInFourActs")
		{

		};
		public static readonly SoundStyle Tsukiyomi_CaesuraOfDespair = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/CaesuraOfDespair")
		{

		};
		public static readonly SoundStyle Tsukiyomi_StygianNymph = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/StygianNymph")
		{


		};
		public static readonly SoundStyle Tsukiyomi_MementoMuse = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/MementoMuse")
		{

		};
		public static readonly SoundStyle Tsukiyomi_LuminaryWand = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/LuminaryWand")
		{

		};
		public static readonly SoundStyle Tsukiyomi_Takonomicon = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/Takonomicon")
		{

		};
		public static readonly SoundStyle Tsukiyomi_KeyOfTheKingsLaw = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/KeyOfTheKingsLaw")
		{

		};
		/*
		public static readonly SoundStyle Tsukiyomi_ThisMessIsMine = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/ThisMessIsMine")
		{

		};
		public static readonly SoundStyle Tsukiyomi_WhyDoYouWantToGetInvolved = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/WhyDoYouWantToGetInvolved")
		{

		};
		public static readonly SoundStyle Tsukiyomi_LetsJustGetThisOverWith = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/LetsJustGetThisOverWith")
		{

		};

		public static readonly SoundStyle Tsukiyomi_AfraidOfTheDark = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/AfraidOfTheDark")
		{

		};
		public static readonly SoundStyle Tsukiyomi_AreYouGettingTired = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/AreYouGettingTired")
		{

		};
		public static readonly SoundStyle Tsukiyomi_CanYouJustStandStill = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/CanYouJustStandStill")
		{

		};
		public static readonly SoundStyle Tsukiyomi_DeathOfAThousandStars = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/DeathOfAThousandStars")
		{

		};
		public static readonly SoundStyle Tsukiyomi_DoYouReallyCallYourselfAHero = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/DoYouReallyCallYourselfAHero")
		{

		};
		public static readonly SoundStyle Tsukiyomi_HaveToTryHarder = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/HaveToTryHarder")
		{

		};
		public static readonly SoundStyle Tsukiyomi_IRefuseToLetYouWin = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/IRefuseToLetYouWin")
		{

		};
		public static readonly SoundStyle Tsukiyomi_IWillBridgeTheGap = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/IWillBridgeTheGap")
		{

		};
		public static readonly SoundStyle Tsukiyomi_JustGettingStarted = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/JustGettingStarted")
		{

		};
		public static readonly SoundStyle Tsukiyomi_JustGiveUp = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/JustGiveUp")
		{

		};
		public static readonly SoundStyle Tsukiyomi_NobodyWillRememberYou = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/NobodyWillRememberYou")
		{

		};
		public static readonly SoundStyle Tsukiyomi_NoIdeaWhoYoureDealingWith = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/NoIdeaWhoYoureDealingWith")
		{

		};
		public static readonly SoundStyle Tsukiyomi_PerhapsIWasWrong = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/PerhapsIWasWrong")
		{

		};
		public static readonly SoundStyle Tsukiyomi_Struggle = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/Struggle")
		{

		};
		public static readonly SoundStyle Tsukiyomi_TearYouApart = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/TearYouApart")
		{

		};
		public static readonly SoundStyle Tsukiyomi_TheOnlyReasonYouFight = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/TheOnlyReasonYouFight")
		{

		};
		public static readonly SoundStyle Tsukiyomi_ThereIsNowhereYouCanRun = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/ThereIsNowhereYouCanRun")
		{

		};
		public static readonly SoundStyle Tsukiyomi_ToDustYouWillReturn = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/ToDustYouWillReturn")
		{

		};
		public static readonly SoundStyle Tsukiyomi_WhyHaventYouDiedYet = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/WhyHaventYouDiedYet")
		{

		};
		public static readonly SoundStyle Tsukiyomi_YouAreNothing = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/YouAreNothing")
		{

		};
		public static readonly SoundStyle Tsukiyomi_YouCantKeepDodging = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/YouCantKeepDodging")
		{

		};
		public static readonly SoundStyle Tsukiyomi_YoureEnjoyingThis = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/YoureEnjoyingThis")
		{

		};
		public static readonly SoundStyle Tsukiyomi_YourMission = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/YourMission")
		{

		};
		public static readonly SoundStyle Tsukiyomi_YourTimeInThisWorld = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/YourTimeInThisWorld")
		{

		};
		public static readonly SoundStyle Tsukiyomi_YouveComeToDefeatMe = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/YouveComeToDefeatMe")
		{

		};*/
		#endregion


		#endregion

		#region Weapon Sound Effects

		public static readonly SoundStyle SFX_KevesiTune = new($"{nameof(StarsAbove)}/Sounds/SFX/KevesiTune")
		{

		};
		public static readonly SoundStyle SFX_AgnianTune = new($"{nameof(StarsAbove)}/Sounds/SFX/AgnianTune")
		{

		};
		public static readonly SoundStyle SFX_BlackSilencePistol = new($"{nameof(StarsAbove)}/Sounds/SFX/BlackSilencePistol")
		{

		};
		public static readonly SoundStyle SFX_BlackSilenceRifle = new($"{nameof(StarsAbove)}/Sounds/SFX/BlackSilenceRifle")
		{

		};
		public static readonly SoundStyle SFX_BlackSilenceMace = new($"{nameof(StarsAbove)}/Sounds/SFX/BlackSilenceMace")
		{

		};
		public static readonly SoundStyle SFX_BlackSilenceAxe = new($"{nameof(StarsAbove)}/Sounds/SFX/BlackSilenceAxe")
		{

		};
		public static readonly SoundStyle SFX_BlackSilenceKatana = new($"{nameof(StarsAbove)}/Sounds/SFX/BlackSilenceKatana")
		{

		};
		public static readonly SoundStyle SFX_BlackSilenceGreatsword = new($"{nameof(StarsAbove)}/Sounds/SFX/BlackSilenceGreatsword")
		{

		};
		public static readonly SoundStyle SFX_BlackSilenceSwing = new($"{nameof(StarsAbove)}/Sounds/SFX/BlackSilenceSwing")
		{

		};
		public static readonly SoundStyle SFX_BlackSilenceDurandalHit = new($"{nameof(StarsAbove)}/Sounds/SFX/BlackSilenceDurandalHit")
		{

		};



		public static readonly SoundStyle SFX_PrismicSpawn = new($"{nameof(StarsAbove)}/Sounds/SFX/PrismicSpawn")
		{

		};
		public static readonly SoundStyle SFX_CatalystIdle = new($"{nameof(StarsAbove)}/Sounds/SFX/CatalystIdle")
		{

		};
		public static readonly SoundStyle SFX_CatalystIgnition = new($"{nameof(StarsAbove)}/Sounds/SFX/CatalystIgnition")
		{

		};
		public static readonly SoundStyle SFX_CatalystExtinguish = new($"{nameof(StarsAbove)}/Sounds/SFX/CatalystExtinguish")
		{

		};
		public static readonly SoundStyle SFX_CatalystSwing = new($"{nameof(StarsAbove)}/Sounds/SFX/CatalystSwing")
		{

		};
		public static readonly SoundStyle SFX_PrismicBreak = new($"{nameof(StarsAbove)}/Sounds/SFX/PrismicBreak")
		{

		};
		public static readonly SoundStyle SFX_PrismicPowerBreak = new($"{nameof(StarsAbove)}/Sounds/SFX/PrismicPowerBreak")
		{

		};
		public static readonly SoundStyle SFX_DealmakerShoot = new($"{nameof(StarsAbove)}/Sounds/SFX/DealmakerShoot")
		{

		};
		public static readonly SoundStyle SFX_DealmakerCharged = new($"{nameof(StarsAbove)}/Sounds/SFX/DealmakerCharged")
		{

		};
		public static readonly SoundStyle SFX_BlazeAttack = new($"{nameof(StarsAbove)}/Sounds/SFX/BlazeChainsawAttack")
		{

		};
		public static readonly SoundStyle SFX_BoilingBloodStart = new($"{nameof(StarsAbove)}/Sounds/SFX/BoilingBloodStart")
		{

		};
		public static readonly SoundStyle SFX_BoilingBloodEnd = new($"{nameof(StarsAbove)}/Sounds/SFX/BoilingBloodEnd")
		{

		};
		public static readonly SoundStyle SFX_BlazeEquip = new($"{nameof(StarsAbove)}/Sounds/SFX/BlazeEquip")
		{

		};
		public static readonly SoundStyle SFX_HuntingHornBasic = new($"{nameof(StarsAbove)}/Sounds/SFX/huntingHornBasic")
		{

		};
		public static readonly SoundStyle SFX_HuntingHornFinal = new($"{nameof(StarsAbove)}/Sounds/SFX/huntingHornFinal")
		{

		};
		public static readonly SoundStyle SFX_RDMCharge = new($"{nameof(StarsAbove)}/Sounds/SFX/RDMCharge")
		{

		};
		public static readonly SoundStyle SFX_RDMCast = new($"{nameof(StarsAbove)}/Sounds/SFX/RDMCast")
		{

		};
		public static readonly SoundStyle SFX_Scorch = new($"{nameof(StarsAbove)}/Sounds/SFX/Scorch")
		{

		};
		public static readonly SoundStyle SFX_Resolution = new($"{nameof(StarsAbove)}/Sounds/SFX/Resolution")
		{

		};
		public static readonly SoundStyle SFX_Redoublement = new($"{nameof(StarsAbove)}/Sounds/SFX/Redoublement")
		{

		};
		public static readonly SoundStyle SFX_Reprise = new($"{nameof(StarsAbove)}/Sounds/SFX/Reprise")
		{

		};
		public static readonly SoundStyle SFX_Verholy = new($"{nameof(StarsAbove)}/Sounds/SFX/Verholy")
		{

		};
		public static readonly SoundStyle SFX_Verflare = new($"{nameof(StarsAbove)}/Sounds/SFX/Verflare")
		{

		};
		#endregion

		#region Miscellaneous Sound Effects
		public static readonly SoundStyle SFX_Laevateinn = new($"{nameof(StarsAbove)}/Sounds/SFX/Laevateinn")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_GuntriggerParryPrep = new($"{nameof(StarsAbove)}/Sounds/SFX/GuntriggerParryPrep")
		{

		};
		public static readonly SoundStyle SFX_GuntriggerParry = new($"{nameof(StarsAbove)}/Sounds/SFX/GuntriggerParry")
		{

		};
		public static readonly SoundStyle SFX_PrepDarkness = new($"{nameof(StarsAbove)}/Sounds/SFX/PrepDarkness")
		{

		};
		public static readonly SoundStyle SFX_TeleportPrep = new($"{nameof(StarsAbove)}/Sounds/SFX/TeleportPrep")
		{

		};
		public static readonly SoundStyle SFX_textsoundeffect = new($"{nameof(StarsAbove)}/Sounds/SFX/textsoundeffect")
		{

		};
		public static readonly SoundStyle SFX_textsoundeffect2 = new($"{nameof(StarsAbove)}/Sounds/SFX/textsoundeffect2")
		{

		};
		public static readonly SoundStyle SFX_textsoundeffect3 = new($"{nameof(StarsAbove)}/Sounds/SFX/textsoundeffect3")
		{

		};
		public static readonly SoundStyle SFX_theofaniaActive = new($"{nameof(StarsAbove)}/Sounds/SFX/theofaniaActive")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_TimeEffect = new($"{nameof(StarsAbove)}/Sounds/SFX/TimeEffect")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_TitanCast = new($"{nameof(StarsAbove)}/Sounds/SFX/TitanCast")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_TitanPrep = new($"{nameof(StarsAbove)}/Sounds/SFX/TitanPrep")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_TruesilverSlash = new($"{nameof(StarsAbove)}/Sounds/SFX/TruesilverSlash")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_Umbral = new($"{nameof(StarsAbove)}/Sounds/SFX/Umbral")
		{

		};
		public static readonly SoundStyle SFX_WhisperShot = new($"{nameof(StarsAbove)}/Sounds/SFX/WhisperShot")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_YunlaiSwing0 = new($"{nameof(StarsAbove)}/Sounds/SFX/YunlaiSwing0")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_YunlaiSwing1 = new($"{nameof(StarsAbove)}/Sounds/SFX/YunlaiSwing1")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_AlbionBlast = new($"{nameof(StarsAbove)}/Sounds/SFX/AlbionBlast")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_AmiyaSlash = new($"{nameof(StarsAbove)}/Sounds/SFX/AmiyaSlash")
		{

		};
		public static readonly SoundStyle SFX_AshenExecute = new($"{nameof(StarsAbove)}/Sounds/SFX/AshenExecute")
		{

		};
		public static readonly SoundStyle SFX_AshenExecute1 = new($"{nameof(StarsAbove)}/Sounds/SFX/AshenExecute1")
		{

		};
		public static readonly SoundStyle SFX_AshenExecute2 = new($"{nameof(StarsAbove)}/Sounds/SFX/AshenExecute2")
		{

		};
		public static readonly SoundStyle SFX_AshenExecute3 = new($"{nameof(StarsAbove)}/Sounds/SFX/AshenExecute3")
		{

		};
		public static readonly SoundStyle SFX_AshenExecute4 = new($"{nameof(StarsAbove)}/Sounds/SFX/AshenExecute4")
		{

		};
		public static readonly SoundStyle SFX_BakaMitai = new($"{nameof(StarsAbove)}/Sounds/SFX/BakaMitai")
		{

		};
		public static readonly SoundStyle SFX_BlasterFire = new($"{nameof(StarsAbove)}/Sounds/SFX/BlasterFire")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_BlasterPrep = new($"{nameof(StarsAbove)}/Sounds/SFX/BlasterPrep")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_bowstring = new($"{nameof(StarsAbove)}/Sounds/SFX/bowstring")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_BuryTheLightPrep = new($"{nameof(StarsAbove)}/Sounds/SFX/BuryTheLightPrep")
		{

		};
		public static readonly SoundStyle SFX_CelestialConception = new($"{nameof(StarsAbove)}/Sounds/SFX/CelestialConception")
		{

		};
		public static readonly SoundStyle SFX_CounterFinish = new($"{nameof(StarsAbove)}/Sounds/SFX/CounterFinish")
		{

		};
		public static readonly SoundStyle SFX_CounterImpact = new($"{nameof(StarsAbove)}/Sounds/SFX/CounterImpact")
		{

		};
		public static readonly SoundStyle SFX_Death = new($"{nameof(StarsAbove)}/Sounds/SFX/Death")
		{

		};
		public static readonly SoundStyle SFX_DeathInFourActsFinish = new($"{nameof(StarsAbove)}/Sounds/SFX/DeathInFourActsFinish")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_DeathInFourActsReload = new($"{nameof(StarsAbove)}/Sounds/SFX/DeathInFourActsReload")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_DeathInFourActsShoot = new($"{nameof(StarsAbove)}/Sounds/SFX/DeathInFourActsShoot")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_electroSmack = new($"{nameof(StarsAbove)}/Sounds/SFX/electroSmack")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_EnterDarkness = new($"{nameof(StarsAbove)}/Sounds/SFX/EnterDarkness")
		{

		};
		public static readonly SoundStyle SFX_GardenOfAvalonActivated = new($"{nameof(StarsAbove)}/Sounds/SFX/GardenOfAvalonActivated")
		{

		};
		public static readonly SoundStyle SFX_GuardianDown = new($"{nameof(StarsAbove)}/Sounds/SFX/GuardianDown")
		{

		};
		public static readonly SoundStyle SFX_GunbladeImpact = new($"{nameof(StarsAbove)}/Sounds/SFX/GunbladeImpact")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_HolyStab = new($"{nameof(StarsAbove)}/Sounds/SFX/HolyStab")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_HuckleberryReload = new($"{nameof(StarsAbove)}/Sounds/SFX/HuckleberryReload")
		{

		};
		public static readonly SoundStyle SFX_HuckleberryShoot = new($"{nameof(StarsAbove)}/Sounds/SFX/HuckleberryShoot")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_HullwroughtBlast = new($"{nameof(StarsAbove)}/Sounds/SFX/HullwroughtBlast")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_HullwroughtLoad = new($"{nameof(StarsAbove)}/Sounds/SFX/HullwroughtLoad")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_iceCracking = new ($"{nameof(StarsAbove)}/Sounds/SFX/iceCracking")
		{

		};
		public static readonly SoundStyle SFX_InugamiCharge = new($"{nameof(StarsAbove)}/Sounds/SFX/InugamiCharge")
		{

		};
		public static readonly SoundStyle SFX_izanagiEquipped = new($"{nameof(StarsAbove)}/Sounds/SFX/izanagiEquipped")
		{

		};
		public static readonly SoundStyle SFX_izanagiReload = new($"{nameof(StarsAbove)}/Sounds/SFX/izanagiReload")
		{

		};
		public static readonly SoundStyle SFX_izanagiReloadBuff = new($"{nameof(StarsAbove)}/Sounds/SFX/izanagiReloadBuff")
		{

		};
		public static readonly SoundStyle SFX_izanagiShoot = new($"{nameof(StarsAbove)}/Sounds/SFX/izanagiShoot")
		{
			PitchVariance = 0.1f,

		};
		public static readonly SoundStyle SFX_izanagiShootBuff = new($"{nameof(StarsAbove)}/Sounds/SFX/izanagiShootBuff")
		{

		};
		public static readonly SoundStyle SFX_LegendarySlash = new($"{nameof(StarsAbove)}/Sounds/SFX/LegendarySlash")
		{

		};
		public static readonly SoundStyle SFX_LimitBreakActive = new($"{nameof(StarsAbove)}/Sounds/SFX/LimitBreakActive")
		{

		};
		public static readonly SoundStyle SFX_LimitBreakCharge = new($"{nameof(StarsAbove)}/Sounds/SFX/LimitBreakCharge")
		{

		};
		public static readonly SoundStyle SFX_MuseFinish = new($"{nameof(StarsAbove)}/Sounds/SFX/MuseFinish")
		{

		};
		public static readonly SoundStyle SFX_MusePing = new($"{nameof(StarsAbove)}/Sounds/SFX/MusePing")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_outbreakShoot = new($"{nameof(StarsAbove)}/Sounds/SFX/outbreakShoot")
		{

		};
		public static readonly SoundStyle SFX_PhaseChange = new($"{nameof(StarsAbove)}/Sounds/SFX/PhaseChange")
		{

		};
		public static readonly SoundStyle SFX_RadGunFail = new($"{nameof(StarsAbove)}/Sounds/SFX/RadGunFail")
		{

		};
		public static readonly SoundStyle SFX_RadGunSuccess = new($"{nameof(StarsAbove)}/Sounds/SFX/RadGunSuccess")
		{

		};
		public static readonly SoundStyle SFX_ScytheImpact = new($"{nameof(StarsAbove)}/Sounds/SFX/ScytheImpact")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_skofnungSwing = new($"{nameof(StarsAbove)}/Sounds/SFX/skofnungSwing")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_spinConstant = new($"{nameof(StarsAbove)}/Sounds/SFX/spinConstant")
		{

		};
		public static readonly SoundStyle SFX_splat = new($"{nameof(StarsAbove)}/Sounds/SFX/splat")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_StarbitCollected = new($"{nameof(StarsAbove)}/Sounds/SFX/StarbitCollected")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_StarbitShoot = new($"{nameof(StarsAbove)}/Sounds/SFX/StarbitShoot")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_StarfarerChosen = new($"{nameof(StarsAbove)}/Sounds/SFX/StarfarerChosen")
		{

		};
		public static readonly SoundStyle SFX_SuistrumeFail = new($"{nameof(StarsAbove)}/Sounds/SFX/SuistrumeFail")
		{

		};
		public static readonly SoundStyle SFX_summoning = new($"{nameof(StarsAbove)}/Sounds/SFX/summoning")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_superReadySFX = new($"{nameof(StarsAbove)}/Sounds/SFX/superReadySFX")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_swordAttackFinish = new($"{nameof(StarsAbove)}/Sounds/SFX/swordAttackFinish")
		{

		};
		public static readonly SoundStyle SFX_SwordBreak = new($"{nameof(StarsAbove)}/Sounds/SFX/SwordBreak")
		{

		};
		public static readonly SoundStyle SFX_swordSpin = new($"{nameof(StarsAbove)}/Sounds/SFX/swordSpin")
		{
			PitchVariance = 0.1f,

		};
		public static readonly SoundStyle SFX_swordStab = new($"{nameof(StarsAbove)}/Sounds/SFX/swordStab")
		{
			PitchVariance = 0.1f,
		};
		public static readonly SoundStyle SFX_TakingDamage = new($"{nameof(StarsAbove)}/Sounds/SFX/TakingDamage")
		{

		};
		public static readonly SoundStyle SFX_TeleportFinisher = new($"{nameof(StarsAbove)}/Sounds/SFX/TeleportFinisher")
		{
			PitchVariance = 0.1f,
		}; 
		public static readonly SoundStyle SFX_WarriorStun = new($"{nameof(StarsAbove)}/Sounds/SFX/WarriorStun")
		{

		};
		#endregion

	}
}
