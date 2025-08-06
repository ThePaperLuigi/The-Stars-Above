using StarsAbove.Items.Consumables;
using StarsAbove.NPCs;
using StarsAbove.NPCs.AttackLibrary;
using StarsAbove.Systems;
using StarsAbove.UI.CelestialCartography;
using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace StarsAbove.Systems
{
    // This file contains 2 real ModConfigs (and also a bunch of fake ModConfigs showcasing various ideas). One is set to ConfigScope.ServerSide and the other ConfigScope.ClientSide
    // ModConfigs contain Public Fields and Properties that represent the choices available to the user. 
    // Those Fields or Properties will be presented to users in the Config menu.
    // DONT use static members anywhere in this class (except for an automatically assigned field named Instance with the same Type as the ModConfig class, if you'd rather write "MyConfigClass.Instance" instead of "ModContent.GetInstance<MyConfigClass>()"), tModLoader maintains several instances of ModConfig classes which will not work well with static properties or fields.

    /// <summary>
    /// ExampleConfigServer has Server-wide effects. Things that happen on the server, on the world, or influence autoload go here
    /// ConfigScope.ServerSide ModConfigs are SHARED from the server to all clients connecting in MP.
    /// </summary>

    public class StarsAboveServersideConfig : ModConfig
    {

        //public new string Name => "Stars Above Config (Server)";
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("$Mods.StarsAbove.Config.DisableTypePenalty.Label")]
        [Tooltip("$Mods.StarsAbove.Config.DisableTypePenalty.Tooltip")]
        public bool DisableTypePenalty;

        [Label("$Mods.StarsAbove.Config.DisableBossAggro.Label")]
        [Tooltip("$Mods.StarsAbove.Config.DisableBossAggro.Tooltip")]
        public bool DisableBossAggro;



        [Label("$Mods.StarsAbove.Config.DisableWeaponRestrictions.Label")]
        [Tooltip("$Mods.StarsAbove.Config.DisableWeaponRestrictions.Tooltip")]
        public bool DisableWeaponRestrictions;

        [Label("$Mods.StarsAbove.Config.DisableBossEnemySpawnMod.Label")]
        [Tooltip("$Mods.StarsAbove.Config.DisableBossEnemySpawnMod.Tooltip")]
        //[DefaultValue(true)]
        public bool DisableBossEnemySpawnMod;

        [Label("$Mods.StarsAbove.Config.DisableManaSicknessChange.Label")]
        [Tooltip("$Mods.StarsAbove.Config.DisableManaSicknessChange.Tooltip")]
        //[DefaultValue(true)]
        public bool DisableManaSicknessChange;

        [Label("$Mods.StarsAbove.Config.ColorblindBoss.Label")]
        [Tooltip("$Mods.StarsAbove.Config.ColorblindBoss.Tooltip")]
        [DefaultValue(false)]
        public bool ColorblindBoss;

        [Label("$Mods.StarsAbove.Config.SubworldCompatibility.Label")]
        [Tooltip("$Mods.StarsAbove.Config.SubworldCompatibility.Tooltip")]
        [DefaultValue(false)]
        public bool MultiplayerFallbackSubworlds;

        [Increment(1)]
        [Range(5, 60)]
        [DefaultValue(15)]
        [Slider] // The Slider attribute makes this field be presented with a slider rather than a text input. The default ticks is 1.
        [Label("$Mods.StarsAbove.Config.CombatTimer.Label")]
        [Tooltip("$Mods.StarsAbove.Config.CombatTimer.Tooltip")]
        public int CombatTimer;


        //[Label("Show mod origin in tooltip")]
        //public bool ShowModOriginTooltip;

        public override void OnChanged()
        {
            // Here we use the OnChanged hook to initialize ExampleUI.visible with the new values.
            // We maintain both ExampleUI.visible and ShowCoinUI as separate values so ShowCoinUI can act as a default while ExampleUI.visible can change within a play session.
            StarsAboveGlobalItem.disableAspectPenalty = DisableTypePenalty;
            StarsAboveGlobalItem.disableWeaponRestriction = DisableWeaponRestrictions;
            StarsAbovePlayer.inCombatMax = CombatTimer * 60;
            StarsAbovePlayer.BossEnemySpawnModDisabled = DisableBossEnemySpawnMod;
            StarsAboveGlobalBuff.DisableManaSicknessChange = DisableManaSicknessChange;
            BossPlayer.disableBossAggro = DisableBossAggro;
            AttackLibrary.ColorblindEnabled = ColorblindBoss;
            CelestialCompass.MultiplayerFallbackSubworlds = MultiplayerFallbackSubworlds;
            DemonicCrux.MPCompat = MultiplayerFallbackSubworlds;

        }
    }
}