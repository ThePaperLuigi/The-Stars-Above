using Microsoft.Xna.Framework;
using StarsAbove.Dialogue;
using StarsAbove.Items.Consumables;
using StarsAbove.UI.Starfarers;
using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace StarsAbove.Systems
{
    public class StarsAboveConfigClient : ModConfig
    {
        //public new string Name => "Stars Above Config (Client)";

        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("$Mods.StarsAbove.Configs.StellarNovaHeader")]

        public bool DisableStellarNovaCutIns;

        public bool DisableStarfarerVoices;

        [Increment(1)]
        [Range(3, 20)]
        [DefaultValue(5)]
        [Slider] // The Slider attribute makes this field be presented with a slider rather than a text input. The default ticks is 1.
        public int StarfarerVoiceCooldown;

        public bool DisableStellarNovaDialogue;

        [Increment(1)]
        [DrawTicks]
        [Slider]
        [DefaultValue(StellarNovaCutscene.Session)]
        public StellarNovaCutscene StellarNovaCutsceneOption;

        [Header("$Mods.StarsAbove.Configs.PopUpDialogueHeader")]

        public bool DisableStarfarerCommentary;

        public bool DisableStarfarerCommentaryBuffs;

        public bool DisableStarfarerCommentaryCombat;

        [Increment(1)]
        [Range(1, 60)]
        [DefaultValue(4)]
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

        [DefaultValue(typeof(Vector2), "0.77, 0.01")]
        [Range(0f, 1f)]
        public Vector2 NovaGaugeLoc { get; set; }

        [Header("$Mods.StarsAbove.Configs.DialogueHeader")]

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


        [Header("$Mods.StarsAbove.Configs.MiscHeader")]
        public bool ForceNeonVeilShader;

        public bool EnableMusicOverride;
        public bool DisableBlur;
        public bool DisableShockwaveEffect;
        public bool DisableScreenShake;

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

            UI.StellarNovaGauge.NovaGaugePos = NovaGaugeLoc;
            UI.Starfarers.StarfarerPrompt.PromptPos = PromptLoc;
            //UI.StellarNovaGauge.NovaGaugePos = NovaGaugeLoc;
            UI.StellarNova.StellarNovaCutIn.Visible = DisableStellarNovaCutIns;
            UI.StellarNova.StellarNovaCutIn.disableDialogue = DisableStellarNovaDialogue;
            UI.StarfarerMenu.StarfarerMenu.AdjustmentFactor = MovingDialogueAmount;
            UI.Starfarers.StarfarerText.AdjustmentFactor = MovingDialogueAmount;

            //StarsAbovePlayer.noLockedCamera = DisableLockedCamera;
            StarsAbovePlayer.disablePrompts = DisableStarfarerCommentary;
            StarsAbovePlayer.starfarerPromptCooldownMax = StarfarerPromptCooldown;
            StarsAbovePlayer.globalVoiceDelayMax = StarfarerVoiceCooldown;

            StarsAbovePlayer.instantText = InstantText;
            StarsAbovePlayer.disablePromptsBuffs = DisableStarfarerCommentaryBuffs;
            StarsAbovePlayer.disablePromptsCombat = DisableStarfarerCommentaryCombat;
            StarsAbovePlayer.starfarerPromptActiveTimerSetting = CommentaryTimer;

            StarsAbovePlayer.voicesDisabled = DisableStarfarerVoices;
            SpatialDisk.voicesDisabled = DisableStarfarerVoices;
            StarsAboveDialogueSystem.voicesDisabled = DisableStarfarerVoices;
            StarfarerText.voicesDisabled = DisableStarfarerVoices;
            UI.StarfarerMenu.StarfarerMenu.voicesDisabled = DisableStarfarerVoices;

            // Depricated!
            //StarsAbovePlayer.ForceNeonVeilShader = ForceNeonVeilShader;

            StarsAbovePlayer.shockwaveEffectDisabled = DisableShockwaveEffect;
            StarsAbovePlayer.disableScreenShake = DisableScreenShake;
            StarsAbovePlayer.dialogueScrollTimerMax = DialogueScrollValue;
            StarsAbovePlayer.dialogueAudio = DialogueAudio;
            StarsAbovePlayer.disableBlur = DisableBlur;


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

    public enum StellarNovaCutscene
    { 
        Never,
        Session,
        Always
    }
}