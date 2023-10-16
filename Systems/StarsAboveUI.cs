using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using StarsAbove.UI;
using StarsAbove.UI.Undertale;
using StarsAbove.UI.Starfarers;

using StarsAbove.UI.StellarNova;
using StarsAbove.UI.CosmicDestroyerGauge;
using StarsAbove.UI.Hawkmoon;
using StarsAbove.UI.JudgementGauge;
using StarsAbove.UI.StarfarerMenu;

using StarsAbove.UI.VN;
using StarsAbove.UI.CelestialCartography;
using StarsAbove.UI.IrminsulDream;
using StarsAbove.UI.CutsceneUI;
using StarsAbove.UI.QTEBar;
using StarsAbove.UI.InkwellGauge;
using StarsAbove.UI.BlackSilenceGauge;
using StarsAbove.UI.SkyStrikerGauge;
using StarsAbove.UI.InkGauge;
using StarsAbove.UI.Cosmoturgy;

namespace StarsAbove.Systems
{

    [Autoload(Side = ModSide.Client)]
    public class StarsAboveUI : ModSystem
    {



        public static ModKeybind novaKey;


        private UserInterface _CutsceneUI;
        internal CutsceneUI CutsceneUI;

        private UserInterface _QTEBar;
        internal QTEBar QTEBar;

        private UserInterface _ButterflyResourceBarUserInterface;
        internal ButterflyResourceBar ButterflyResourceBar;

        private UserInterface _DraggedBelowBarUserInterface;
        internal DraggedBelowBar DraggedBelowBar;

        private UserInterface _DragonshiftGaugeUserInterface;
        internal DragonshiftGauge DragonshiftGauge;

        private UserInterface _DreamersInkwellUIUserInterface;
        internal DreamersInkwellGauge DreamersInkwellUI;

        private UserInterface _NanomachinaGaugeUserInterface;
        internal NanomachinaGauge NanomachinaGauge;

        private UserInterface _RhythmGaugeUserInterface;
        internal RhythmGauge RhythmGauge;

        private UserInterface _RenegadeGaugeUserInterface;
        internal RenegadeGauge RenegadeGauge;

        private UserInterface _BlackSilenceGaugeUserInterface;
        internal BlackSilenceGauge BlackSilenceGauge;

        private UserInterface _VengeanceGaugeUserInterface;
        internal VengeanceGauge VengeanceGauge;

        private UserInterface _EternalGaugeUserInterface;
        internal EternalGauge EternalGauge;

        private UserInterface _EmotionGaugeUserInterface;
        internal EmotionGauge EmotionGauge;

        private UserInterface _TemperatureGaugeUserInterface;
        internal TemperatureGauge TemperatureGauge;

        private UserInterface _OverdriveGaugeUserInterface;
        internal OverdriveGauge OverdriveGauge;

        private UserInterface _UmbraGaugeUserInterface;
        internal UmbraGauge UmbraGauge;

        private UserInterface _RadGaugeUserInterface;
        internal RadGauge RadGauge;

        private UserInterface _IrminsulCursorUserInterface;
        internal IrminsulCursor IrminsulCursor;

        private UserInterface _RedMageGaugeUserInterface;
        internal RedMageGauge RedMageGauge;

        private UserInterface _AreaNameUIUserInterface;
        internal AreaNameUI AreaNameUI;

        private UserInterface _BowChargeUserInterface;
        internal BowCharge BowCharge;

        private UserInterface _PowderGaugeUserInterface;
        internal PowderGauge PowderGauge;

        private UserInterface _StellarPerformanceBarUserInterface;
        internal StellarPerformanceBar StellarPerformanceBar;


        private UserInterface _WarriorCastBarUserInterface;
        internal WarriorCastBar WarriorCastBar;

        private UserInterface _NalhaunCastBarUserInterface;
        internal NalhaunCastBar NalhaunCastBar;

        private UserInterface _VagrantCastBarUserInterface;
        internal VagrantCastBar VagrantCastBar;

        private UserInterface _PenthCastBarUserInterface;
        internal PenthCastBar PenthCastBar;

        private UserInterface _ArbiterCastBarUserInterface;
        internal ArbiterCastBar ArbiterCastBar;

        private UserInterface _TsukiyomiCastBarUserInterface;
        internal TsukiyomiCastBar TsukiyomiCastBar;

        private UserInterface _CastorCastBarUserInterface;
        internal CastorCastBar CastorCastBar;

        private UserInterface _PolluxCastBarUserInterface;
        internal PolluxCastBar PolluxCastBar;

        private UserInterface _SpectrumGaugeUserInterface;
        internal SpectrumGauge SpectrumGauge;

        private UserInterface _HawkmoonGaugeUserInterface;
        internal HawkmoonGauge HawkmoonGauge;

        private UserInterface _HawkmoonReloadGaugeUserInterface;
        internal HawkmoonReloadGauge HawkmoonReloadGauge;

        private UserInterface _HawkmoonBulletIndicatorGaugeUserInterface;
        internal HawkmoonBulletIndicatorGauge HawkmoonBulletIndicatorGauge;

        private UserInterface _JudgementGaugeUserInterface;
        internal JudgementGauge JudgementGauge;

        private UserInterface _VirtueGaugeUserInterface;
        internal VirtueGauge VirtueGauge;

        private UserInterface _AegisGaugeUserInterface;
        internal AegisGauge AegisGauge;

        private UserInterface _CeruleanFlameGaugeUserInterface;
        internal CeruleanFlameGauge CeruleanFlameGauge;

        private UserInterface _EternityGaugeUserInterface;
        internal EternityGauge EternityGauge;

        private UserInterface _DualityGaugeUserInterface;
        internal DualityGauge DualityGauge;

        private UserInterface _InkGaugeUserInterface;
        internal InkGauge InkGauge;

        private UserInterface _CosmicDestroyerGaugeUserInterface;
        internal CosmicDestroyerGauge CosmicDestroyerGauge;

        private UserInterface _SkyStrikerGaugeUserInterface;
        internal SkyStrikerGauge SkyStrikerGauge;

        private UserInterface _StarfarerSelectionUserInterface;
        internal StarfarerSelection StarfarerSelection;

        private UserInterface _StarfarerTextUserInterface;
        internal StarfarerText StarfarerText;

        private UserInterface _MainMenuIconUserInterface;
        internal MainMenuIcon MainMenuIcon;

        private UserInterface _StellarArrayUserInterface;
        internal StellarArray StellarArray;

        private UserInterface _StellarNovaCutInUserInterface;
        internal StellarNovaCutIn StellarNovaCutIn;

        private UserInterface _StellarNovaUIUserInterface;
        internal StellarNovaUI StellarNovaUI;

        private UserInterface _CosmoturgyUIUserInterface;
        internal CosmoturgyUI CosmoturgyUI;

        private UserInterface _BattleBoxUserInterface;
        internal BattleBox BattleBox;

        private UserInterface _StellarNovaGaugeUserInterface;
        internal StellarNovaGauge StellarNovaGauge;

        private UserInterface _TakodachiGaugeUserInterface;
        internal TakodachiGauge TakodachiGauge;

        private UserInterface _StarfarerPromptUserInterface;
        internal StarfarerPrompt StarfarerPrompt;

        private UserInterface _StarfarerMenuUserInterface;
        internal StarfarerMenu StarfarerMenu;

        private UserInterface _CelestialCompassUserInterface;
        internal CelestialCompass CelestialCompass;

        private UserInterface _VNUserInterface;
        internal VN VN;


        public override void Load()
        {

            if (!Main.dedServ)
            {
                CutsceneUI = new CutsceneUI();
                _CutsceneUI = new UserInterface();
                _CutsceneUI.SetState(CutsceneUI);

                QTEBar = new QTEBar();
                _QTEBar = new UserInterface();
                _QTEBar.SetState(QTEBar);

                ButterflyResourceBar = new ButterflyResourceBar();
                _ButterflyResourceBarUserInterface = new UserInterface();
                _ButterflyResourceBarUserInterface.SetState(ButterflyResourceBar);

                DragonshiftGauge = new DragonshiftGauge();
                _DragonshiftGaugeUserInterface = new UserInterface();
                _DragonshiftGaugeUserInterface.SetState(DragonshiftGauge);

                DraggedBelowBar = new DraggedBelowBar();
                _DraggedBelowBarUserInterface = new UserInterface();
                _DraggedBelowBarUserInterface.SetState(DraggedBelowBar);

                DreamersInkwellUI = new DreamersInkwellGauge();
                _DreamersInkwellUIUserInterface = new UserInterface();
                _DreamersInkwellUIUserInterface.SetState(DreamersInkwellUI);

                NanomachinaGauge = new NanomachinaGauge();
                _NanomachinaGaugeUserInterface = new UserInterface();
                _NanomachinaGaugeUserInterface.SetState(NanomachinaGauge);

                TakodachiGauge = new TakodachiGauge();
                _TakodachiGaugeUserInterface = new UserInterface();
                _TakodachiGaugeUserInterface.SetState(TakodachiGauge);

                VengeanceGauge = new VengeanceGauge();
                _VengeanceGaugeUserInterface = new UserInterface();
                _VengeanceGaugeUserInterface.SetState(VengeanceGauge);

                EternalGauge = new EternalGauge();
                _EternalGaugeUserInterface = new UserInterface();
                _EternalGaugeUserInterface.SetState(EternalGauge);

                UmbraGauge = new UmbraGauge();
                _UmbraGaugeUserInterface = new UserInterface();
                _UmbraGaugeUserInterface.SetState(UmbraGauge);

                EmotionGauge = new EmotionGauge();
                _EmotionGaugeUserInterface = new UserInterface();
                _EmotionGaugeUserInterface.SetState(EmotionGauge);

                TemperatureGauge = new TemperatureGauge();
                _TemperatureGaugeUserInterface = new UserInterface();
                _TemperatureGaugeUserInterface.SetState(TemperatureGauge);

                OverdriveGauge = new OverdriveGauge();
                _OverdriveGaugeUserInterface = new UserInterface();
                _OverdriveGaugeUserInterface.SetState(OverdriveGauge);

                RhythmGauge = new RhythmGauge();
                _RhythmGaugeUserInterface = new UserInterface();
                _RhythmGaugeUserInterface.SetState(RhythmGauge);

                BlackSilenceGauge = new BlackSilenceGauge();
                _BlackSilenceGaugeUserInterface = new UserInterface();
                _BlackSilenceGaugeUserInterface.SetState(BlackSilenceGauge);

                RenegadeGauge = new RenegadeGauge();
                _RenegadeGaugeUserInterface = new UserInterface();
                _RenegadeGaugeUserInterface.SetState(RenegadeGauge);

                RadGauge = new RadGauge();
                _RadGaugeUserInterface = new UserInterface();
                _RadGaugeUserInterface.SetState(RadGauge);

                IrminsulCursor = new IrminsulCursor();
                _IrminsulCursorUserInterface = new UserInterface();
                _IrminsulCursorUserInterface.SetState(IrminsulCursor);

                AreaNameUI = new AreaNameUI();
                _AreaNameUIUserInterface = new UserInterface();
                _AreaNameUIUserInterface.SetState(AreaNameUI);

                RedMageGauge = new RedMageGauge();
                _RedMageGaugeUserInterface = new UserInterface();
                _RedMageGaugeUserInterface.SetState(RedMageGauge);

                BowCharge = new BowCharge();
                _BowChargeUserInterface = new UserInterface();
                _BowChargeUserInterface.SetState(BowCharge);

                PowderGauge = new PowderGauge();
                _PowderGaugeUserInterface = new UserInterface();
                _PowderGaugeUserInterface.SetState(PowderGauge);

                StellarPerformanceBar = new StellarPerformanceBar();
                _StellarPerformanceBarUserInterface = new UserInterface();
                _StellarPerformanceBarUserInterface.SetState(StellarPerformanceBar);

                WarriorCastBar = new WarriorCastBar();
                _WarriorCastBarUserInterface = new UserInterface();
                _WarriorCastBarUserInterface.SetState(WarriorCastBar);

                NalhaunCastBar = new NalhaunCastBar();
                _NalhaunCastBarUserInterface = new UserInterface();
                _NalhaunCastBarUserInterface.SetState(NalhaunCastBar);

                VagrantCastBar = new VagrantCastBar();
                _VagrantCastBarUserInterface = new UserInterface();
                _VagrantCastBarUserInterface.SetState(VagrantCastBar);

                PenthCastBar = new PenthCastBar();
                _PenthCastBarUserInterface = new UserInterface();
                _PenthCastBarUserInterface.SetState(PenthCastBar);

                ArbiterCastBar = new ArbiterCastBar();
                _ArbiterCastBarUserInterface = new UserInterface();
                _ArbiterCastBarUserInterface.SetState(ArbiterCastBar);

                TsukiyomiCastBar = new TsukiyomiCastBar();
                _TsukiyomiCastBarUserInterface = new UserInterface();
                _TsukiyomiCastBarUserInterface.SetState(TsukiyomiCastBar);

                CastorCastBar = new CastorCastBar();
                _CastorCastBarUserInterface = new UserInterface();
                _CastorCastBarUserInterface.SetState(CastorCastBar);

                PolluxCastBar = new PolluxCastBar();
                _PolluxCastBarUserInterface = new UserInterface();
                _PolluxCastBarUserInterface.SetState(PolluxCastBar);

                SpectrumGauge = new SpectrumGauge();
                _SpectrumGaugeUserInterface = new UserInterface();
                _SpectrumGaugeUserInterface.SetState(SpectrumGauge);

                HawkmoonGauge = new HawkmoonGauge();
                _HawkmoonGaugeUserInterface = new UserInterface();
                _HawkmoonGaugeUserInterface.SetState(HawkmoonGauge);

                HawkmoonReloadGauge = new HawkmoonReloadGauge();
                _HawkmoonReloadGaugeUserInterface = new UserInterface();
                _HawkmoonReloadGaugeUserInterface.SetState(HawkmoonReloadGauge);

                HawkmoonBulletIndicatorGauge = new HawkmoonBulletIndicatorGauge();
                _HawkmoonBulletIndicatorGaugeUserInterface = new UserInterface();
                _HawkmoonBulletIndicatorGaugeUserInterface.SetState(HawkmoonBulletIndicatorGauge);

                JudgementGauge = new JudgementGauge();
                _JudgementGaugeUserInterface = new UserInterface();
                _JudgementGaugeUserInterface.SetState(JudgementGauge);

                VirtueGauge = new VirtueGauge();
                _VirtueGaugeUserInterface = new UserInterface();
                _VirtueGaugeUserInterface.SetState(VirtueGauge);

                AegisGauge = new AegisGauge();
                _AegisGaugeUserInterface = new UserInterface();
                _AegisGaugeUserInterface.SetState(AegisGauge);

                CeruleanFlameGauge = new CeruleanFlameGauge();
                _CeruleanFlameGaugeUserInterface = new UserInterface();
                _CeruleanFlameGaugeUserInterface.SetState(CeruleanFlameGauge);

                EternityGauge = new EternityGauge();
                _EternityGaugeUserInterface = new UserInterface();
                _EternityGaugeUserInterface.SetState(EternityGauge);

                DualityGauge = new DualityGauge();
                _DualityGaugeUserInterface = new UserInterface();
                _DualityGaugeUserInterface.SetState(DualityGauge);

                CosmicDestroyerGauge = new CosmicDestroyerGauge();
                _CosmicDestroyerGaugeUserInterface = new UserInterface();
                _CosmicDestroyerGaugeUserInterface.SetState(CosmicDestroyerGauge);

                InkGauge = new InkGauge();
                _InkGaugeUserInterface = new UserInterface();
                _InkGaugeUserInterface.SetState(InkGauge);

                SkyStrikerGauge = new SkyStrikerGauge();
                _SkyStrikerGaugeUserInterface = new UserInterface();
                _SkyStrikerGaugeUserInterface.SetState(SkyStrikerGauge);

                StarfarerSelection = new StarfarerSelection();
                _StarfarerSelectionUserInterface = new UserInterface();
                _StarfarerSelectionUserInterface.SetState(StarfarerSelection);

                StarfarerText = new StarfarerText();
                _StarfarerTextUserInterface = new UserInterface();
                _StarfarerTextUserInterface.SetState(StarfarerText);

                MainMenuIcon = new MainMenuIcon();
                _MainMenuIconUserInterface = new UserInterface();
                _MainMenuIconUserInterface.SetState(MainMenuIcon);

                StellarArray = new StellarArray();
                _StellarArrayUserInterface = new UserInterface();
                _StellarArrayUserInterface.SetState(StellarArray);

                StellarNovaCutIn = new StellarNovaCutIn();
                _StellarNovaCutInUserInterface = new UserInterface();
                _StellarNovaCutInUserInterface.SetState(StellarNovaCutIn);

                StellarNovaUI = new StellarNovaUI();
                _StellarNovaUIUserInterface = new UserInterface();
                _StellarNovaUIUserInterface.SetState(StellarNovaUI);

                CosmoturgyUI = new CosmoturgyUI();
                _CosmoturgyUIUserInterface = new UserInterface();
                _CosmoturgyUIUserInterface.SetState(CosmoturgyUI);

                BattleBox = new BattleBox();
                _BattleBoxUserInterface = new UserInterface();
                _BattleBoxUserInterface.SetState(BattleBox);

                StellarNovaGauge = new StellarNovaGauge();
                _StellarNovaGaugeUserInterface = new UserInterface();
                _StellarNovaGaugeUserInterface.SetState(StellarNovaGauge);

                StarfarerPrompt = new StarfarerPrompt();
                _StarfarerPromptUserInterface = new UserInterface();
                _StarfarerPromptUserInterface.SetState(StarfarerPrompt);

                StarfarerMenu = new StarfarerMenu();
                _StarfarerMenuUserInterface = new UserInterface();
                _StarfarerMenuUserInterface.SetState(StarfarerMenu);

                CelestialCompass = new CelestialCompass();
                _CelestialCompassUserInterface = new UserInterface();
                _CelestialCompassUserInterface.SetState(CelestialCompass);

                VN = new VN();
                _VNUserInterface = new UserInterface();
                _VNUserInterface.SetState(VN);
            }


        }
        public override void UpdateUI(GameTime gameTime)
        {


            _CutsceneUI?.Update(gameTime);
            _QTEBar?.Update(gameTime);

            _NanomachinaGaugeUserInterface?.Update(gameTime);
            _ButterflyResourceBarUserInterface?.Update(gameTime);
            _DragonshiftGaugeUserInterface?.Update(gameTime);
            _DraggedBelowBarUserInterface?.Update(gameTime);

            _DreamersInkwellUIUserInterface?.Update(gameTime);

            _VengeanceGaugeUserInterface?.Update(gameTime);
            _EternalGaugeUserInterface?.Update(gameTime);
            _UmbraGaugeUserInterface?.Update(gameTime);
            _EmotionGaugeUserInterface?.Update(gameTime);
            _TemperatureGaugeUserInterface?.Update(gameTime);

            _OverdriveGaugeUserInterface?.Update(gameTime);

            _BlackSilenceGaugeUserInterface?.Update(gameTime);
            _RenegadeGaugeUserInterface?.Update(gameTime);
            _RhythmGaugeUserInterface?.Update(gameTime);

            _TakodachiGaugeUserInterface?.Update(gameTime);

            _RadGaugeUserInterface?.Update(gameTime);
            _IrminsulCursorUserInterface?.Update(gameTime);
            _AreaNameUIUserInterface?.Update(gameTime);
            _RedMageGaugeUserInterface?.Update(gameTime);

            _BowChargeUserInterface?.Update(gameTime);

            _PowderGaugeUserInterface?.Update(gameTime);
            _StellarPerformanceBarUserInterface?.Update(gameTime);
            _WarriorCastBarUserInterface?.Update(gameTime);
            _HawkmoonGaugeUserInterface?.Update(gameTime);
            _HawkmoonReloadGaugeUserInterface?.Update(gameTime);
            _HawkmoonBulletIndicatorGaugeUserInterface?.Update(gameTime);
            _JudgementGaugeUserInterface?.Update(gameTime);
            _VirtueGaugeUserInterface?.Update(gameTime);
            _AegisGaugeUserInterface?.Update(gameTime);

            _CeruleanFlameGaugeUserInterface?.Update(gameTime);
            _EternityGaugeUserInterface?.Update(gameTime);
            _DualityGaugeUserInterface?.Update(gameTime);
            _CosmicDestroyerGaugeUserInterface?.Update(gameTime);
            _InkGaugeUserInterface?.Update(gameTime);
            _SkyStrikerGaugeUserInterface?.Update(gameTime);
            _StarfarerSelectionUserInterface?.Update(gameTime);
            _StarfarerTextUserInterface?.Update(gameTime);
            _MainMenuIconUserInterface?.Update(gameTime);
            _StellarArrayUserInterface?.Update(gameTime);
            _StellarNovaCutInUserInterface?.Update(gameTime);
            _StellarNovaUIUserInterface?.Update(gameTime);
            _CosmoturgyUIUserInterface?.Update(gameTime);

            _BattleBoxUserInterface?.Update(gameTime);
            _StellarNovaGaugeUserInterface?.Update(gameTime);
            _NalhaunCastBarUserInterface?.Update(gameTime);
            _ArbiterCastBarUserInterface?.Update(gameTime);
            _TsukiyomiCastBarUserInterface?.Update(gameTime);
            _CastorCastBarUserInterface?.Update(gameTime);
            _PolluxCastBarUserInterface?.Update(gameTime);
            _VagrantCastBarUserInterface?.Update(gameTime);
            _PenthCastBarUserInterface?.Update(gameTime);
            _SpectrumGaugeUserInterface?.Update(gameTime);
            _StarfarerPromptUserInterface?.Update(gameTime);
            _CelestialCompassUserInterface?.Update(gameTime);
            _StarfarerMenuUserInterface?.Update(gameTime);

            _VNUserInterface?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {

            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Interface Logic 2"));
            int MouseIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Cursor"));

            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Battle Box",
                    delegate
                    {
                        _BattleBoxUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Starfarer Selection",
                    delegate
                    {
                        _StarfarerSelectionUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Asphodene Text",
                    delegate
                    {
                        _StarfarerTextUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Eridani Text",
                    delegate
                    {
                        _MainMenuIconUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: The Stellar Array",
                    delegate
                    {
                        _StellarArrayUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Stellar Nova Cut-In",
                    delegate
                    {
                        _StellarNovaCutInUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );



                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Stellar Nova Gauge",
                    delegate
                    {
                        _StellarNovaGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );


                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Nanomachina Gauge",
                    delegate
                    {
                        _NanomachinaGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Butterfly Resource Bar",
                    delegate
                    {
                        _ButterflyResourceBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Dragonshift Gauge",
                    delegate
                    {
                        _DragonshiftGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Dragged Below Bar",
                    delegate
                    {
                        _DraggedBelowBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Dreamer's Inkwell Gauge",
                    delegate
                    {
                        _DreamersInkwellUIUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Rhythm Gauge",
                    delegate
                    {
                        _RhythmGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Black Silence Gauge",
                    delegate
                    {
                        _BlackSilenceGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Renegade Gauge",
                    delegate
                    {
                        _RenegadeGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Vengeance Gauge",
                    delegate
                    {
                        _VengeanceGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );

                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Eternal Gauge",
                    delegate
                    {
                        _EternalGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Umbra Gauge",
                    delegate
                    {
                        _UmbraGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Emotion Gauge",
                    delegate
                    {
                        _EmotionGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Temperature Gauge",
                    delegate
                    {
                        _TemperatureGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Overdrive Gauge",
                    delegate
                    {
                        _OverdriveGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Takodachi Gauge",
                    delegate
                    {
                        _TakodachiGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Rad Gauge",
                    delegate
                    {
                        _RadGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Red Mage Gauge",
                    delegate
                    {
                        _RedMageGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Area Name",
                    delegate
                    {
                        _AreaNameUIUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Irminsul Cursor",
                    delegate
                    {
                        _IrminsulCursorUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Bow Charge",
                    delegate
                    {
                        _BowChargeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Powder Gauge",
                    delegate
                    {
                        _PowderGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Stellar Performance Bar",
                    delegate
                    {
                        _StellarPerformanceBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Warrior Of Light Cast Bar",
                    delegate
                    {
                        _WarriorCastBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Hawkmoon Reload Gauge",
                    delegate
                    {
                        _HawkmoonReloadGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Hawkmoon Gauge",
                    delegate
                    {
                        _HawkmoonGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Hawkmoon Bullet Indicator Gauge",
                    delegate
                    {
                        _HawkmoonBulletIndicatorGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Judgement Gauge",
                    delegate
                    {
                        _JudgementGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Virtue Gauge",
                    delegate
                    {
                        _VirtueGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Aegis Gauge",
                    delegate
                    {
                        _AegisGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Cerulean Flame Gauge",
                    delegate
                    {
                        _CeruleanFlameGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Eternity Gauge",
                    delegate
                    {
                        _EternityGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Duality Gauge",
                    delegate
                    {
                        _DualityGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: CosmicDestroyer Gauge",
                    delegate
                    {
                        _CosmicDestroyerGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Ink Gauge",
                    delegate
                    {
                        _InkGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: SkyStriker Gauge",
                    delegate
                    {
                        _SkyStrikerGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Nalhaun Cast Bar",
                    delegate
                    {
                        _NalhaunCastBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Vagrant Cast Bar",
                    delegate
                    {
                        _VagrantCastBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Penth Cast Bar",
                    delegate
                    {
                        _PenthCastBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Arbiter Cast Bar",
                    delegate
                    {
                        _ArbiterCastBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Tsukiyomi Cast Bar",
                    delegate
                    {
                        _TsukiyomiCastBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Castor Cast Bar",
                    delegate
                    {
                        _CastorCastBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Pollux Cast Bar",
                    delegate
                    {
                        _PolluxCastBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Life Force Bar",
                    delegate
                    {
                        _SpectrumGaugeUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: QTE Bar",
                    delegate
                    {
                        _QTEBar.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Starfarer Prompts",
                    delegate
                    {
                        _StarfarerPromptUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );

                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Celestial Compass",
                    delegate
                    {
                        _CelestialCompassUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Visual Novel",
                    delegate
                    {
                        _VNUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );

                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Starfarer Menu",
                    delegate
                    {
                        _StarfarerMenuUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
            int resourceBarIndex2 = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Interface Logic 2"));
            if (resourceBarIndex2 != -1)
            {


            }

            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (MouseTextIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                       "StarsAbove: Stellar Nova UI",
                       delegate
                       {
                           _StellarNovaUIUserInterface.Draw(Main.spriteBatch, new GameTime());
                           return true;
                       },
                       InterfaceScaleType.UI)
                   );
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                       "StarsAbove: Cosmoturgy UI",
                       delegate
                       {
                           _CosmoturgyUIUserInterface.Draw(Main.spriteBatch, new GameTime());
                           return true;
                       },
                       InterfaceScaleType.UI)
                   );
                /*layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
					"AutoTrash: Auto Trash List",
					delegate {
						if (AutoTrashListUI.visible)
						{
							AutoTrash.autoTrashUserInterface.Update(Main._drawInterfaceGameTime);
							ModContent.GetInstance<AutoTrash>().autoTrashListUI.Draw(Main.spriteBatch);
						}
						return true;
					},
					InterfaceScaleType.UI)
				);*/


            }

            int TopIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Interface Logic 4"));
            if (TopIndex != -1)
            {

                layers.Insert(TopIndex, new LegacyGameInterfaceLayer(
                    "StarsAbove: Cutscene UI",
                    delegate
                    {
                        _CutsceneUI.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );

            }
        }
    }
}