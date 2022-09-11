using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;

namespace StarsAbove
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

		[Label("Disable damage penalty for Aspected Damage modification")]
		[Tooltip("10% damage penalty when Aspected Damage modification is enabled is removed.")]
		public bool DisableTypePenalty;

		[Label("Disable global damage modifiers for mod compatibility")]
		[Tooltip("Disables the 20% increase to Aspected Weapon damage when the Calamity Mod is enabled.")]
		public bool DisableCalamityWeaponBuffs;

		[Label("Disable Aspected Weapon restrictions")]
		[Tooltip("Disables the restriction for Astral and Umbral weapons being wielded by other players.")]
		public bool DisableWeaponRestrictions;

		[Increment(1)]
		[Range(5, 60)]
		[DefaultValue(15)]
		[Slider] // The Slider attribute makes this field be presented with a slider rather than a text input. The default ticks is 1.
		[Label("Combat Timer")]
		[Tooltip("When striking a foe or getting struck, this is the time in seconds you are 'in combat.' Affects Stellar Nova gauge and using the Starfarer Menu.")]
		public int CombatTimer;


		//[Label("Show mod origin in tooltip")]
		//public bool ShowModOriginTooltip;

		public override void OnChanged()
		{
			// Here we use the OnChanged hook to initialize ExampleUI.visible with the new values.
			// We maintain both ExampleUI.visible and ShowCoinUI as separate values so ShowCoinUI can act as a default while ExampleUI.visible can change within a play session.
			StarsAboveGlobalItem.disableAspectPenalty = DisableTypePenalty;
			StarsAboveGlobalItem.disableWeaponRestriction = DisableWeaponRestrictions;
			StarsAboveGlobalItem.disableCalamityWeaponBuffs = DisableCalamityWeaponBuffs;
			StarsAbovePlayer.inCombatMax = CombatTimer*60;
		}
	}
}