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

	public class StarsAboveConfigClient : ModConfig
	{
		//public new string Name => "Stars Above Config (Client)";
		
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[Label("Disable Stellar Nova cut-ins")]
		[Tooltip("Disables the portrait of your Starfarer when activating your Stellar Nova.")]
		public bool DisableStellarNovaCutIns;

		[Label("Disable Stellar Nova dialogue box")]
		[Tooltip("Disables the dialogue box during the Stellar Nova cut-in.")]
		public bool DisableStellarNovaDialogue;

		[Label("Disable Starfarer commentary")]
		[Tooltip("Disable Starfarer Prompts entirely.")]
		public bool DisableStarfarerCommentary;

		[Label("Disable Starfarer commentary on buffs/debuffs")]
		[Tooltip("You will no longer recieve Starfarer Prompts for getting buffed/debuffed.")]
		public bool DisableStarfarerCommentaryBuffs;

		[Label("Disable Starfarer commentary for combat events")]
		[Tooltip("You will no longer recieve Starfarer Prompts for killing foes, etc.")]
		public bool DisableStarfarerCommentaryCombat;

		[Increment(1)]
		[Range(1, 60)]
		[DefaultValue(8)]
		[Slider] // The Slider attribute makes this field be presented with a slider rather than a text input. The default ticks is 1.
		[Label("Starfarer Prompt Cooldown")]
		[Tooltip("The time between Starfarer Prompts in minutes. If a trigger for a prompt occurs during this time, it is ignored. Does not apply to unique prompts (Bosses, etc.)")]
		public int StarfarerPromptCooldown;

		[Increment(1)]
		[Range(1, 5)]
		[DefaultValue(2)]
		[Slider] // The Slider attribute makes this field be presented with a slider rather than a text input. The default ticks is 1.
		[Label("Starfarer Text Speed")]
		[Tooltip("The time for dialogue text to increment in frames. Below 2, it is recommended to disable dialogue sound effects.")]
		public int DialogueScrollValue;

		[Increment(1)]
		[Range(1, 5)]
		[DefaultValue(3)]
		[Slider] // The Slider attribute makes this field be presented with a slider rather than a text input. The default ticks is 1.
		[Label("Starfarer Text Sound Effect")]
		[Tooltip("The sound effect that plays when letters appear during Starfarer Dialogue. 5 is silence.")]
		public int DialogueAudio;

		[Label("Instant Text")]
		[Tooltip("Ignores the Starfarer Text Speed option; all text will appear instantly.")]
		public bool InstantText;

		[Label("Disable dragging Starfarer Dialogue")]
		[Tooltip("Starfarer Dialogue is locked in position.")]
		public bool DisableDraggingDialogue;

		//[Label("No locked camera in the final boss fight")]
		//[Tooltip("Disables the locked camera in the final boss fight.")]
		//public bool DisableLockedCamera;

		[DefaultValue(typeof(Vector2), "-1360, -510")]
		[Range(-1920f, 0f)]
		[Label("Starfarer Prompt positiion")]
		[Tooltip("Position is measured from the bottom right of the screen!")]
		public Vector2 PromptLoc { get; set; }

		//[DefaultValue(typeof(Vector2), "-480, -1005")]
		//[Range(-1920f, 0f)]
		//[Label("Stellar Nova Gauge positiion")]
		//[Tooltip("Position is measured from the bottom right of the screen! Sliders move much farther than you'd expect.")]
		//public Vector2 NovaGaugeLoc { get; set; }

		[Increment(5)]
		[Range(0, 300)]
		[DefaultValue(100)]
		[Slider] // The Slider attribute makes this field be presented with a slider rather than a text input. The default ticks is 1.
		[Label("Starfarer Battle Commentary timer")]
		[Tooltip("The time for random Starfarer Prompts to stay on your screen.")]
		public int CommentaryTimer;

		/*[Label("Disable Suistrume audio")]
		[Tooltip("Disables music change from Suistrume's buff.")]//Mute suistrume
		public bool EnableSuistrumeAudio;*/

		[Label("Disable Stellar Nova voice lines")]
		[Tooltip("Disables voice lines for Asphodene and Eridani during Stellar Novas. (Courtesy of 15.ai)")]
		public bool EnableStarfarerVoices;

		[Label("Weapon music overrides all other music")]
		[Tooltip("When enabled, weapons that change the game's music will have priority over nearly everything else.")]
		public bool EnableMusicOverride;

		[Label("Enable Player Progress World Lock")]
		[Tooltip("Enables character progression being tied to the first world they joined.")]
		public bool EnablePlayerWorldLock;

		[Label("Enable April Fools mode")]
		[Tooltip("Adds radical sunglasses to Asphodene and Eridani in the Starfarer menu and during Stellar Novas.")]
		public bool EnableAprilFools;


		//[Label("Show mod origin in tooltip")]
		//public bool ShowModOriginTooltip;

		public override void OnChanged()
		{
			// Here we use the OnChanged hook to initialize ExampleUI.visible with the new values.
			// We maintain both ExampleUI.visible and ShowCoinUI as separate values so ShowCoinUI can act as a default while ExampleUI.visible can change within a play session.
			UI.StarfarerMenu.StarfarerMenu.ShadesVisible = EnableAprilFools;
			UI.StellarNova.StellarNovaCutIn.ShadesVisible = EnableAprilFools;

			UI.Starfarers.StarfarerPrompt.PromptPos = PromptLoc;
			//UI.StellarNovaGauge.NovaGaugePos = NovaGaugeLoc;
			UI.StellarNova.StellarNovaCutIn.Visible = DisableStellarNovaCutIns;
			UI.StellarNova.StellarNovaCutIn.disableDialogue = DisableStellarNovaDialogue;
			UI.Starfarers.AsphodeneText.Draggable = DisableDraggingDialogue;
			UI.Starfarers.EridaniText.Draggable = DisableDraggingDialogue;
			//StarsAbovePlayer.noLockedCamera = DisableLockedCamera;
			StarsAbovePlayer.disablePrompts = DisableStarfarerCommentary;
			StarsAbovePlayer.starfarerPromptCooldownMax = StarfarerPromptCooldown;
			StarsAbovePlayer.instantText = InstantText;
			StarsAbovePlayer.disablePromptsBuffs = DisableStarfarerCommentaryBuffs;
			StarsAbovePlayer.disablePromptsCombat = DisableStarfarerCommentaryCombat;
			StarsAbovePlayer.starfarerPromptActiveTimerSetting = CommentaryTimer;
			StarsAbovePlayer.voicesEnabled = EnableStarfarerVoices;
			StarsAbovePlayer.dialogueScrollTimerMax = DialogueScrollValue;
			StarsAbovePlayer.dialogueAudio = DialogueAudio;
			StarsAbovePlayer.enableWorldLock = EnablePlayerWorldLock;
			if (EnableMusicOverride)
            {
				SceneEffects.SuistrumeAudio.setPriority = SceneEffectPriority.BossHigh + 1;
				SceneEffects.SuistrumeAudioEasterEgg.setPriority = SceneEffectPriority.BossHigh + 1;
				SceneEffects.TheOnlyThingIKnowForRealAudio.setPriority = SceneEffectPriority.BossHigh + 1;
				SceneEffects.TheOnlyThingIKnowForRealAudioLow.setPriority = SceneEffectPriority.BossHigh + 1;

			}
			//StarsAbove.sharedAudio = EnableSuistrumeAudio;
		}
	}
}