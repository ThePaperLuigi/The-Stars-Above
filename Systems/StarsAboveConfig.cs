using Microsoft.Xna.Framework;
using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

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

		[Label("$Mods.StarsAbove.Config.DisableStellarNovaCutIns.Label")]
		[Tooltip("$Mods.StarsAbove.Config.DisableStellarNovaCutIns.Tooltip")]
		public bool DisableStellarNovaCutIns;

		[Label("$Mods.StarsAbove.Config.DisableStellarNovaDialogue.Label")]
		[Tooltip("$Mods.StarsAbove.Config.DisableStellarNovaDialogue.Tooltip")]
		public bool DisableStellarNovaDialogue;

		[Label("$Mods.StarsAbove.Config.DisableStarfarerCommentary.Label")]
		[Tooltip("$Mods.StarsAbove.Config.DisableStarfarerCommentary.Tooltip")]
		public bool DisableStarfarerCommentary;

		[Label("$Mods.StarsAbove.Config.DisableStarfarerCommentaryBuffs.Label")]
		[Tooltip("$Mods.StarsAbove.Config.DisableStarfarerCommentaryBuffs.Tooltip")]
		public bool DisableStarfarerCommentaryBuffs;

		[Label("$Mods.StarsAbove.Config.DisableStarfarerCommentaryCombat.Label")]
		[Tooltip("$Mods.StarsAbove.Config.DisableStarfarerCommentaryCombat.Tooltip")]
		public bool DisableStarfarerCommentaryCombat;

		[Increment(1)]
		[Range(1, 60)]
		[DefaultValue(8)]
		[Slider] // The Slider attribute makes this field be presented with a slider rather than a text input. The default ticks is 1.
		[Label("$Mods.StarsAbove.Config.StarfarerPromptCooldown.Label")]
		[Tooltip("$Mods.StarsAbove.Config.StarfarerPromptCooldown.Tooltip")]
		public int StarfarerPromptCooldown;

		[Increment(1)]
		[Range(1, 5)]
		[DefaultValue(2)]
		[Slider] // The Slider attribute makes this field be presented with a slider rather than a text input. The default ticks is 1.
		[Label("$Mods.StarsAbove.Config.DialogueScrollValue.Label")]
		[Tooltip("$Mods.StarsAbove.Config.DialogueScrollValue.Tooltip")]
		public int DialogueScrollValue;

		[Increment(1)]
		[Range(1, 5)]
		[DefaultValue(3)]
		[Slider] // The Slider attribute makes this field be presented with a slider rather than a text input. The default ticks is 1.
		[Label("$Mods.StarsAbove.Config.DialogueAudio.Label")]
		[Tooltip("$Mods.StarsAbove.Config.DialogueAudio.Tooltip")]
		public int DialogueAudio;

		[Label("$Mods.StarsAbove.Config.InstantText.Label")]
		[Tooltip("$Mods.StarsAbove.Config.InstantText.Tooltip")]
		public bool InstantText;

		[Label("$Mods.StarsAbove.Config.DisableDraggingDialogue.Label")]
		[Tooltip("$Mods.StarsAbove.Config.DisableDraggingDialogue.Tooltip")]
		public bool DisableDraggingDialogue;

		//[Label("No locked camera in the final boss fight")]
		//[Tooltip("Disables the locked camera in the final boss fight.")]
		//public bool DisableLockedCamera;

		[DefaultValue(typeof(Vector2), "-1360, -510")]
		[Range(-1920f, 0f)]
		[Label("$Mods.StarsAbove.Config.PromptLoc.Label")]
		[Tooltip("$Mods.StarsAbove.Config.PromptLoc.Tooltip")]
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
		[Label("$Mods.StarsAbove.Config.CommentaryTimer.Label")]
		[Tooltip("$Mods.StarsAbove.Config.CommentaryTimer.Tooltip")]
		public int CommentaryTimer;

		/*[Label("Disable Suistrume audio")]
		[Tooltip("Disables music change from Suistrume's buff.")]//Mute suistrume
		public bool EnableSuistrumeAudio;*/

		[Label("$Mods.StarsAbove.Config.EnableStarfarerVoices.Label")]
		[Tooltip("$Mods.StarsAbove.Config.EnableStarfarerVoices.Tooltip")]
		public bool EnableStarfarerVoices;

		[Label("$Mods.StarsAbove.Config.EnableMusicOverride.Label")]
		[Tooltip("$Mods.StarsAbove.Config.EnableMusicOverride.Tooltip")]
		public bool EnableMusicOverride;

		[Label("$Mods.StarsAbove.Config.EnablePlayerWorldLock.Label")]
		[Tooltip("$Mods.StarsAbove.Config.EnablePlayerWorldLock.Tooltip")]
		public bool EnablePlayerWorldLock;

		[Label("$Mods.StarsAbove.Config.EnableAprilFools.Label")]
		[Tooltip("$Mods.StarsAbove.Config.EnableAprilFools.Tooltip")]
		public bool EnableAprilFools;

		[Label("$Mods.StarsAbove.Config.DisableWeaponCutIn.Label")]
		[Tooltip("$Mods.StarsAbove.Config.DisableWeaponCutIn.Tooltip")]
		public bool DisableWeaponCutIns;

		//[Label("Show mod origin in tooltip")]
		//public bool ShowModOriginTooltip;

		public override void OnChanged()
		{
			// Here we use the OnChanged hook to initialize ExampleUI.visible with the new values.
			// We maintain both ExampleUI.visible and ShowCoinUI as separate values so ShowCoinUI can act as a default while ExampleUI.visible can change within a play session.
			UI.StarfarerMenu.StarfarerMenu.ShadesVisible = EnableAprilFools;
			UI.StellarNova.StellarNovaCutIn.ShadesVisible = EnableAprilFools;
			UI.EmotionGauge.AnimationDisabled = DisableWeaponCutIns;

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
				SceneEffects.ManifestationAudio.setPriority = SceneEffectPriority.BossHigh + 1;


			}
			//StarsAbove.sharedAudio = EnableSuistrumeAudio;
		}
	}
}