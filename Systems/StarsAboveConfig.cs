using Microsoft.Xna.Framework;
using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace StarsAbove
{
    public class StarsAboveConfigClient : ModConfig
	{
		//public new string Name => "Stars Above Config (Client)";
		
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[Header("$Mods.StarsAbove.Config.StellarNovaHeader")]

		public bool DisableStellarNovaCutIns;

		public bool EnableStarfarerVoices;

		public bool DisableStellarNovaDialogue;

		[Header("$Mods.StarsAbove.Config.PopUpDialogueHeader")]

		public bool DisableStarfarerCommentary;

		public bool DisableStarfarerCommentaryBuffs;

		public bool DisableStarfarerCommentaryCombat;

		[Increment(1)]
		[Range(1, 60)]
		[DefaultValue(8)]
		[Slider] // The Slider attribute makes this field be presented with a slider rather than a text input. The default ticks is 1.
		public int StarfarerPromptCooldown;

		[Increment(5)]
		[Range(0, 300)]
		[DefaultValue(100)]
		[Slider] // The Slider attribute makes this field be presented with a slider rather than a text input. The default ticks is 1.
		public int CommentaryTimer;

		[DefaultValue(typeof(Vector2), "-1360, -510")]
		[Range(-1920f, 0f)]
		public Vector2 PromptLoc { get; set; }


		[Header("$Mods.StarsAbove.Config.DialogueHeader")]

		[Increment(1)]
		[Range(1, 5)]
		[DefaultValue(2)]
		[Slider] // The Slider attribute makes this field be presented with a slider rather than a text input. The default ticks is 1.
		public int DialogueScrollValue;

		[Increment(1)]
		[Range(1, 5)]
		[DefaultValue(3)]
		[Slider] // The Slider attribute makes this field be presented with a slider rather than a text input. The default ticks is 1.
		public int DialogueAudio;

		public bool InstantText;

		[Increment(0.1f)]
		[Range(0f, 2f)]
		[DefaultValue(1.5)]
		[Slider]
		public float MovingDialogueAmount;


		[Header("$Mods.StarsAbove.Config.MiscHeader")]
		public bool EnableMusicOverride;

		//[Label("$Mods.StarsAbove.Config.EnablePlayerWorldLock.Label")]
		//[Tooltip("$Mods.StarsAbove.Config.EnablePlayerWorldLock.Tooltip")]
		//public bool EnablePlayerWorldLock;
		/*
		[Label("$Mods.StarsAbove.Config.EnableAprilFools.Label")]
		[Tooltip("$Mods.StarsAbove.Config.EnableAprilFools.Tooltip")]
		public bool EnableAprilFools;*/

		public bool DisableWeaponCutIns;

		//[Label("Show mod origin in tooltip")]
		//public bool ShowModOriginTooltip;

		public override void OnChanged()
		{
			// Here we use the OnChanged hook to initialize ExampleUI.visible with the new values.
			// We maintain both ExampleUI.visible and ShowCoinUI as separate values so ShowCoinUI can act as a default while ExampleUI.visible can change within a play session.
			//UI.StarfarerMenu.StarfarerMenu.ShadesVisible = EnableAprilFools;
			//UI.StellarNova.StellarNovaCutIn.ShadesVisible = EnableAprilFools;
			UI.EmotionGauge.AnimationDisabled = DisableWeaponCutIns;

			UI.Starfarers.StarfarerPrompt.PromptPos = PromptLoc;
			//UI.StellarNovaGauge.NovaGaugePos = NovaGaugeLoc;
			UI.StellarNova.StellarNovaCutIn.Visible = DisableStellarNovaCutIns;
			UI.StellarNova.StellarNovaCutIn.disableDialogue = DisableStellarNovaDialogue;
			UI.StarfarerMenu.StarfarerMenu.AdjustmentFactor = MovingDialogueAmount;
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