using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace StarsAbove.Systems
{
    public class StarsAboveAudio : ModSystem
    {
        public static float bossVoiceVolume = 1f;
        public static float voiceVolume = 0.6f;
        #region Starfarer Voice Lines

        //Story Voice Lines
        #region Story Voice Lines

        #endregion

        //Combat Voice Lines
        #region Combat Voice Lines
        public static readonly SoundStyle AsphodeneAccident0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneAccident0")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AsphodeneBossAngry0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneBossAngry0")
        {
            Volume = voiceVolume

        };
        public static readonly SoundStyle AsphodeneBossAngry1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneBossAngry1")
        {
            Volume = voiceVolume


        };
        public static readonly SoundStyle AsphodeneBossAngry2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneBossAngry2")
        {
            Volume = voiceVolume

        };
        public static readonly SoundStyle AsphodeneBossAngry3 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneBossAngry3")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AsphodeneBossLowHP0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneBossLowHP0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneBossLowHP1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneBossLowHP1")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AsphodeneBossNeutral0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneBossNeutral0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneBossNeutral1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneBossNeutral1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneBossNeutral2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneBossNeutral2")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AsphodeneBossPerfect = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneBossPerfect")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AsphodeneBossSuprised0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneBossSuprised0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneBossSuprised1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneBossSuprised1")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AsphodeneBossWorried0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneBossWorried0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneBossWorried1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneBossWorried1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneBossWorried2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneBossWorried2")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneBossWorried3 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneBossWorried3")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AsphodeneVictory0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneVictory0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneVictory1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneVictory1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniAccident0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniAccident0")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EridaniBossAngry0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniBossAngry0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniBossAngry1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniBossAngry1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniBossAngry2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniBossAngry2")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EridaniBossLowHP0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniBossLowHP0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniBossLowHP1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniBossLowHP1")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EridaniBossNeutral0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniBossNeutral0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniBossNeutral1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniBossNeutral1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniBossNeutral2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniBossNeutral2")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniBossNeutral3 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniBossNeutral3")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniBossNeutral4 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniBossNeutral4")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniBossNeutral5 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniBossNeutral5")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EridaniBossPerfect = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniBossPerfect")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EridaniBossSuprised0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniBossSuprised0")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EridaniBossWorried0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniBossWorried0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniBossWorried1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniBossWorried1")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EridaniVictory0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniVictory0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniVictory1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniVictory1")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AsphodeneAmbush0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneAmbush0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneHurtAccident0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneHurtAccident0")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AsphodeneHurtMajor0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneHurtMajor0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneHurtMajor1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneHurtMajor1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneHurtMajor2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneHurtMajor2")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneHurtMinor0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneHurtMinor0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneHurtMinor1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneHurtMinor1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneHurtMinor2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneHurtMinor2")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AsphodeneReady0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/AsphodeneReady0")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EridaniAmbush0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniAmbush0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniHurtAccident0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniHurtAccident0")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EridaniHurtMajor0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniHurtMajor0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniHurtMajor1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniHurtMajor1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniHurtMajor2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniHurtMajor2")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniHurtMinor0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniHurtMinor0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniHurtMinor1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniHurtMinor1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniHurtMinor2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniHurtMinor2")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EridaniReady0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/CombatVoiceLines/EridaniReady0")
        {
            Volume = voiceVolume
        };
        #endregion

        //Dialogue Voice Lines
        #region Dialogue Voice Lines
        public static readonly SoundStyle AExplore0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneExplore0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AExplore1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneExplore1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AExplore2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneExplore2")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AExplore3 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneExplore3")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AExplore4 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneExplore4")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AExploreRare0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneExploreRare0")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle ALaugh0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneLaugh0")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle ALight0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneLight0")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle ARain0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneRain0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle ARain1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneRain1")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EExplore0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniExplore0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EExplore1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniExplore1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EExplore2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniExplore2")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EExplore3 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniExplore3")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EExplore4 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniExplore4")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EExploreRare0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniExploreRare0")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle ELaugh0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniLaugh0")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle ELight0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniLight0")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle ERain0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniRain0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle ERain1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniRain1")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AsphodeneDialogueStart0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneDialogueStart0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneDialogueStart1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneDialogueStart1")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AsphodeneIdle0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneIdle0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneIdle1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneIdle1")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AsphodeneNewUnlock0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneNewUnlock0")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AsphodeneNewUnlock1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneNewUnlock1")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AsphodeneNewUnlock2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneNewUnlock2")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AsphodeneNewUnlock3 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneNewUnlock3")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AsphodeneNewUnlock4 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneNewUnlock4")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EridaniDialogueStart0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniDialogueStart0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniDialogueStart1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniDialogueStart1")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EridaniIdle0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniIdle0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniIdle1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniIdle1")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EridaniNewUnlock0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniNewUnlock0")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EridaniNewUnlock1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniNewUnlock1")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EridaniNewUnlock2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniNewUnlock2")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EridaniNewUnlock3 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniNewUnlock3")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EridaniNewUnlock4 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniNewUnlock4")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle AsphodeneAngry0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneAngry0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneAngry1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneAngry1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneDeadInside0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneDeadInside0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneWorried0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneWorried0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneSmug0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneSmug0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneSmug1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneSmug1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneHappy0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneHappy0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneHappy1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneHappy1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneNeutral0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneNeutral0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneThinking0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneThinking0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneThinking1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneThinking1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneThinking2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneThinking2")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneDialogueEnd0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneDialogueEnd0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AsphodeneDialogueEnd1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/AsphodeneDialogueEnd1")
        {
            Volume = voiceVolume
        };

        public static readonly SoundStyle EridaniAngry0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniAngry0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniAngry1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniAngry1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniDeadInside0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniDeadInside0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniWorried0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniWorried0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniSmug0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniSmug0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniSmug1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniSmug1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniHappy0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniHappy0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniHappy1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniHappy1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniNeutral0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniNeutral0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniThinking0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniThinking0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniThinking1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniThinking1")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniThinking2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniThinking2")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniDialogueEnd0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniDialogueEnd0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle EridaniDialogueEnd1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/DialogueVoiceLines/EridaniDialogueEnd1")
        {
            Volume = voiceVolume
        };

        #endregion
        //Other Voice Lines
        #region Other Voice Lines
        public static readonly SoundStyle AOutfit0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/AsphodeneOutfit0")
        {
            Volume = voiceVolume
        };
        public static readonly SoundStyle AOutfit1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/AsphodeneOutfit1")
        {
            Volume = voiceVolume

        };
        public static readonly SoundStyle AOutfit2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/AsphodeneOutfit2")
        {
            Volume = voiceVolume

        };
        public static readonly SoundStyle EOutfit0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/EridaniOutfit0")
        {
            Volume = voiceVolume

        };
        public static readonly SoundStyle EOutfit1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/EridaniOutfit1")
        {
            Volume = voiceVolume

        };
        public static readonly SoundStyle EOutfit2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/EridaniOutfit2")
        {
            Volume = voiceVolume

        };

        public static readonly SoundStyle AsphodeneMenu0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/AsphodeneMenu0")
        {
            Volume = voiceVolume

        };
        public static readonly SoundStyle AsphodeneMenu1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/AsphodeneMenu1")
        {
            Volume = voiceVolume

        };
        public static readonly SoundStyle AsphodeneMenu2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/AsphodeneMenu2")
        {
            Volume = voiceVolume

        };
        public static readonly SoundStyle AsphodeneMenu3 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/AsphodeneMenu3")
        {
            Volume = voiceVolume

        };
        public static readonly SoundStyle AsphodeneMenu4 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/AsphodeneMenu4")
        {
            Volume = voiceVolume

        };
        public static readonly SoundStyle AsphodeneMenu5 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/AsphodeneMenu5")
        {
            Volume = voiceVolume

        };
        public static readonly SoundStyle AsphodeneMenuCombat0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/AsphodeneMenuCombat0")
        {
            Volume = voiceVolume

        };

        public static readonly SoundStyle EridaniMenu0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/EridaniMenu0")
        {
            Volume = voiceVolume


        };
        public static readonly SoundStyle EridaniMenu1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/EridaniMenu1")
        {
            Volume = voiceVolume

        };
        public static readonly SoundStyle EridaniMenu2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/EridaniMenu2")
        {
            Volume = voiceVolume

        };
        public static readonly SoundStyle EridaniMenu3 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/EridaniMenu3")
        {
            Volume = voiceVolume

        };
        public static readonly SoundStyle EridaniMenu4 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/EridaniMenu4")
        {
            Volume = voiceVolume

        };
        public static readonly SoundStyle EridaniMenu5 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/EridaniMenu5")
        {
            Volume = voiceVolume

        };
        public static readonly SoundStyle EridaniMenuCombat0 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/OtherVoiceLines/EridaniMenuCombat0")
        {
            Volume = voiceVolume

        };
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

        public SoundStyle AS1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AS1")
        {
            
             
        };
        public SoundStyle AS2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AS2")
        {
             
        };
        public SoundStyle AS3 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AS3")
        {
             
        };
        public SoundStyle AS4 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AS4")
        {
             
        };
        public SoundStyle AS5 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AS5")
        {
             
        };
        public SoundStyle AS6 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AS6")
        {
             
        };
        public SoundStyle AS7 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AS7")
        {
             
        };
        public SoundStyle AS8 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AS8")
        {
             
        }; 
        public SoundStyle AS9 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AS9")
        {
             
        };
        public SoundStyle AS10 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AS10")
        {
             
        };
        public SoundStyle AS11 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AS11")
        {
             
        };
        public SoundStyle AS12 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AS12")
        {
             
        };
        public SoundStyle AS13 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AS13")
        {
             
        };
        public SoundStyle AS14 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AS14")
        {
             
        };
        public SoundStyle AS15 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AS15")
        {
             
        };
        public SoundStyle AS16 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AS16")
        {
             
        };
        public SoundStyle AS17 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AS17")
        {
             
        };
        public SoundStyle AS18 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/AS18")
        {
             
        };
        public SoundStyle ASJoke = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/ASJoke")
        {
             
        };
        /*
        public static readonly SoundStyle ASSpecial1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/UniqueLines/ASSpecial1")
        {

        };
        public static readonly SoundStyle ASSpecial2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/UniqueLines/ASSpecial2")
        {

        };
        public static readonly SoundStyle ASSpecial3 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/UniqueLines/ASSpecial3")
        {

        };
        public static readonly SoundStyle ASSpecial4 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/UniqueLines/ASSpecial4")
        {

        };
        public static readonly SoundStyle ASSpecial5 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/UniqueLines/ASSpecial5")
        {

        };
        public static readonly SoundStyle ASSpecial6 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Asphodene/UniqueLines/ASSpecial6")
        {

        };*/

        #endregion
        #region Eridani Voice Lines
        public SoundStyle ER1 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ER1")
        {
             
        };
        public SoundStyle ER2 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ER2")
        {
             
        };
        public   SoundStyle ER3 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ER3")
        {
             
        };
        public   SoundStyle ER4 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ER4")
        {
             
        };
        public   SoundStyle ER5 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ER5")
        {
             
        };
        public   SoundStyle ER6 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ER6")
        {
             
        };
        public   SoundStyle ER7 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ER7")
        {
             
        };
        public   SoundStyle ER8 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ER8")
        {
             
        };
        public   SoundStyle ER9 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ER9")
        {
             
        };
        public   SoundStyle ER10 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ER10")
        {
             
        };
        public   SoundStyle ER11 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ER11")
        {
             
        };
        public   SoundStyle ER12 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ER12")
        {
             
        };
        public   SoundStyle ER13 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ER13")
        {
             
        };
        public   SoundStyle ER14 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ER14")
        {
             
        };
        public   SoundStyle ER15 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ER15")
        {
             
        };
        public   SoundStyle ER16 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ER16")
        {
             
        };
        public   SoundStyle ER17 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ER17")
        {
             
        };
        public   SoundStyle ER18 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ER18")
        {
             
        };
        public   SoundStyle ERJoke = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/ERJoke")
        {
             
        };
        /*
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
        public static readonly SoundStyle ENSpecial6 = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/StellarNovaLines/Eridani/UniqueLines/ENSpecial6")
        {

        };*/
        #endregion

        #endregion

        #region Boss Voice Lines

        #region Vagrant Voice Lines

        #endregion

        #region Nalhaun Voice Lines
        public static readonly SoundStyle Nalhaun_AndNowTheScalesWillTip = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/AndNowTheScalesWillTip")
        {
           Volume = bossVoiceVolume
        };
        public static readonly SoundStyle Nalhaun_AThousandBolts = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/AThousandBolts")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Nalhaun_ComeShowMeMore = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/ComeShowMeMore")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Nalhaun_EscapeIsNotSoEasilyGranted = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/EscapeIsNotSoEasilyGranted")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Nalhaun_EvenTheStrongestShields = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/EvenTheStrongestShields")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Nalhaun_Fools = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/Fools")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Nalhaun_MyDefenses = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/MyDefenses")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Nalhaun_NalhaunDeathQuote = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/NalhaunDeathQuote")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Nalhaun_NalhaunIntroQuote = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/NalhaunIntroQuote")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Nalhaun_PityDisplay = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/PityDisplay")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Nalhaun_RuinationIsCome = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/RuinationIsCome")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Nalhaun_TheGodsWillNotBeWatching = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/TheGodsWillNotBeWatching")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Nalhaun_TheHeartsOfMen = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/TheHeartsOfMen")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Nalhaun_UponMyHolyBlade = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/UponMyHolyBlade")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Nalhaun_WereYouExpectingRust = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Nalhaun/WereYouExpectingRust")
        {
            Volume = bossVoiceVolume

        };
        #endregion

        #region Penthesilea Voice Lines
        public static readonly SoundStyle Penthesilea_AlrightMyTurn = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/AlrightMyTurn")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Penthesilea_HandsOn = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/HandsOn")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Penthesilea_HelloLittlePaintbrush = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/HelloLittlePaintbrush")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Penthesilea_FasterThanThat = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/FasterThanThat")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Penthesilea_UnderestimatedYou = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/UnderestimatedYou")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Penthesilea_DontGetTooDirty = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/DontGetTooDirty")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Penthesilea_IDontThinkYoullLikeWhatComesNext = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/IDontThinkYoullLikeWhatComesNext")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Penthesilea_MoreWhereThatCameFrom = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/MoreWhereThatCameFrom")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Penthesilea_MyNextCanvas = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/MyNextCanvas")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Penthesilea_QuicklyNow = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/QuicklyNow")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Penthesilea_RainButPrettier = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/RainButPrettier")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Penthesilea_TooMuchColor = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/TooMuchColor")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Penthesilea_WhatColor = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/WhatColor")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Penthesilea_WouldntItBeSoFunny = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/WouldntItBeSoFunny")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Penthesilea_WrappedThingsUp = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Penthesilea/WrappedThingsUp")
        {
            Volume = bossVoiceVolume


        };

        #endregion

        #region Arbitration Voice Lines
        public static readonly SoundStyle Arbitration_Voice = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Arbitration/ArbitrationReverse")
        {
            Volume = bossVoiceVolume,

            Variants = new[]
            {
                1,2,3,4,5,6
            }
        };

        #endregion
        #region Starfarer Boss Voice Lines
        public static readonly SoundStyle StarfarerBoss_Intro = new($"{nameof(StarsAbove)}/Sounds/StarfarerVoiceLines/BossLines/StarfarerIntro")
        {
            Volume = bossVoiceVolume,

            Variants = new[]
            {
                1,2
            }
        };

        #endregion

        #region Warrior Of Light Voice Lines
        public static readonly SoundStyle WarriorOfLight_EveryDream = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/EveryDream")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_FlamesOfBattle = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/FlamesOfBattle")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_NowToTakeYourMeasure = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/NowToTakeYourMeasure")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_WillLightEmbrace = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/WillLightEmbrace")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_TheseMagicksAreNotForYou = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/TheseMagicksAreNotForYou")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_JudgedWorthyToExist = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/JudgedWorthyToExist")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_ForVictory = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/ForVictory")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_PowerFromBeyond = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/PowerFromBeyond")
        {
            Volume = bossVoiceVolume

        };

        public static readonly SoundStyle WarriorOfLight_BegoneSpawnOfShadow = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/BegoneSpawnOfShadow")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_CladInPrayerIAmInvincible = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/CladInPrayerIAmInvincible")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_DarknessMustBeDestroyed = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/DarknessMustBeDestroyed")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_FinalPhaseGrunt = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/FinalPhaseGrunt")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_ForVictoryIRenderUpMyAll = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/ForVictoryIRenderUpMyAll")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_GleamingSteelLightMyPath = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/GleamingSteelLightMyPath")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_HopeGrantMeStrength = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/HopeGrantMeStrength")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_IAmSalvationGivenForm = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/IAmSalvationGivenForm")
        {
            Volume = bossVoiceVolume

        };

        public static readonly SoundStyle WarriorOfLight_IWillNotFall = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/IWillNotFall")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_IWillStrikeYouDown = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/IWillStrikeYouDown")
        {
            Volume = bossVoiceVolume

        };

        public static readonly SoundStyle WarriorOfLight_MankindsFirstHeroAndHisFinalHope = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/MankindsFirstHeroAndHisFinalHope")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_MySoulKnowsNoSurrender = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/MySoulKnowsNoSurrender")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_RadiantBraver = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/RadiantBraver")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_RefulgentEther = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/RefulgentEther")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_TheBitterEnd = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/TheBitterEnd")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_ToMeWarriorsOfLight = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/ToMeWarriorsOfLight")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_WarriorOfLightDeathQuote = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/WarriorOfLightDeathQuote")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_WarriorOfLightDefeated = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/WarriorOfLightDefeated")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_WarriorOfLightIntroQuote = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/WarriorOfLightIntroQuote")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_YourDemiseShallBeOurSalvation = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/YourDemiseShallBeOurSalvation")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle WarriorOfLight_YouStillStand = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/WarriorOfLight/YouStillStand")
        {
            Volume = bossVoiceVolume

        };

        #endregion

        #region Tsukiyomi Voice Lines
        public static readonly SoundStyle Tsukiyomi_DeathOfAThousandStars = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/ThousandStars")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_Struggle = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/Struggle")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_NowhereYouCanRun = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/NowhereYouCanRun")
        {
            Volume = bossVoiceVolume


        };
        public static readonly SoundStyle Tsukiyomi_AfraidOfTheDark = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/AfraidOfTheDark")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_TryHarder = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/TryHarderThanThat")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_ForgettingSomething = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/ForgettingSomething")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_Journey = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/EndOfYourJourney")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_Stronger = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/StrongerThanIHoped")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_Insignificant = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/Insignificant")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_TakeThisOutside = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/TakeThisOutside")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_ThousandStars = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/ThousandStars")
        {
            Volume = bossVoiceVolume

        };
        //Aspected weapon voice lines.
        public static readonly SoundStyle Tsukiyomi_CarianDarkMoon = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/CarianDarkMoon")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_BuryTheLight = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/BuryTheLight")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_TheOnlyThingIKnowForReal = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/TheOnlyThingIKnowForReal")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_VoiceOfTheOutbreak = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/VoiceOfTheOutbreak")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_ShadowlessCerulean = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/ShadowlessCerulean")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_DeathInFourActs = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/DeathInFourActs")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_CaesuraOfDespair = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/CaesuraOfDespair")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_StygianNymph = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/StygianNymph")
        {

            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_MementoMuse = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/MementoMuse")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_LuminaryWand = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/LuminaryWand")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_Takonomicon = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/Takonomicon")
        {
            Volume = bossVoiceVolume

        };
        public static readonly SoundStyle Tsukiyomi_KeyOfTheKingsLaw = new($"{nameof(StarsAbove)}/Sounds/VoiceLines/Tsukiyomi/KeyOfTheKingsLaw")
        {
            Volume = bossVoiceVolume

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
        public static readonly SoundStyle SFX_ArbiterShockwave = new($"{nameof(StarsAbove)}/Sounds/SFX/SFX_ArbiterShockwave")
        {
            PitchVariance = 0.1f,
        };
        public static readonly SoundStyle SFX_ArbiterLock = new($"{nameof(StarsAbove)}/Sounds/SFX/SFX_ArbiterLock")
        {
            PitchVariance = 0.1f,
        };
        public static readonly SoundStyle SFX_ArbiterChain = new($"{nameof(StarsAbove)}/Sounds/SFX/SFX_ArbiterChain")
        {
            PitchVariance = 0.1f,
        };
        public static readonly SoundStyle SFX_ArbiterPillar = new($"{nameof(StarsAbove)}/Sounds/SFX/SFX_ArbiterPillar")
        {
            PitchVariance = 0.1f,
        };
        public static readonly SoundStyle SFX_OrbitalTrain = new($"{nameof(StarsAbove)}/Sounds/SFX/Train")
        {

        };
        public static readonly SoundStyle SFX_GundamLaser = new($"{nameof(StarsAbove)}/Sounds/SFX/GundamLaser")
        {

        };
        public static readonly SoundStyle SFX_ShockAndAweRocket = new($"{nameof(StarsAbove)}/Sounds/SFX/ShockAndAweRocket")
        {

        };
        public static readonly SoundStyle SFX_Deify = new($"{nameof(StarsAbove)}/Sounds/SFX/Deify")
        {

        };
        public static readonly SoundStyle SFX_Disappear = new($"{nameof(StarsAbove)}/Sounds/SFX/Disappear")
        {

        };
        public static readonly SoundStyle SFX_DisappearPrep = new($"{nameof(StarsAbove)}/Sounds/SFX/DisappearPrep")
        {

        };
        public static readonly SoundStyle SFX_AbsoluteEye = new($"{nameof(StarsAbove)}/Sounds/SFX/AbsoluteEye")
        {

        };
        public static readonly SoundStyle SFX_ManiacalSlash = new($"{nameof(StarsAbove)}/Sounds/SFX/ManiacalSlash")
        {

        };
        public static readonly SoundStyle SFX_ManiacalSlashWarning = new($"{nameof(StarsAbove)}/Sounds/SFX/ManiacalSlashWarning")
        {

        };
        public static readonly SoundStyle SFX_ManiacalSlashSpecial = new($"{nameof(StarsAbove)}/Sounds/SFX/ManiacalSlashSpecial")
        {

        };
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
        public static readonly SoundStyle SFX_Trumpet = new($"{nameof(StarsAbove)}/Sounds/SFX/Trumpet")
        {
            PitchVariance = 0.3f,
            
        };
        public static readonly SoundStyle SFX_FFTransformation = new($"{nameof(StarsAbove)}/Sounds/SFX/FFTransformSFX")
        {
            Volume = 0.8f,
        };
        public static readonly SoundStyle SFX_SpeedrunEasterEgg = new($"{nameof(StarsAbove)}/Sounds/SFX/SpeedrunEasterEgg")
        {

        };
        public static readonly SoundStyle SFX_VoidExplosion = new($"{nameof(StarsAbove)}/Sounds/SFX/VoidExplosion")
        {
            PitchVariance = 0.1f,
        };
        public static readonly SoundStyle SFX_ThundercrashStart = new($"{nameof(StarsAbove)}/Sounds/SFX/ThundercrashStart")
        {
            PitchVariance = 0.1f,
        };
        public static readonly SoundStyle SFX_ThundercrashEnd = new($"{nameof(StarsAbove)}/Sounds/SFX/ThundercrashEnd")
        {
            PitchVariance = 0.1f,
        };
        public static readonly SoundStyle SFX_SilenceSquall1 = new($"{nameof(StarsAbove)}/Sounds/SFX/SilenceSquall1")
        {
            PitchVariance = 0.1f,
        };
        public static readonly SoundStyle SFX_SilenceSquall2 = new($"{nameof(StarsAbove)}/Sounds/SFX/SilenceSquall2")
        {
            PitchVariance = 0.1f,
        };
        public static readonly SoundStyle SFX_NovaBomb = new($"{nameof(StarsAbove)}/Sounds/SFX/NovaBomb")
        {
            PitchVariance = 0.1f,
        };
        public static readonly SoundStyle SFX_Needlestorm = new($"{nameof(StarsAbove)}/Sounds/SFX/Needlestorm")
        {
            PitchVariance = 0.1f,
        };
        public static readonly SoundStyle SFX_FireGoldenGun = new($"{nameof(StarsAbove)}/Sounds/SFX/FireGoldenGun")
        {
            PitchVariance = 0.1f,
        };
        public static readonly SoundStyle SFX_Laevateinn = new($"{nameof(StarsAbove)}/Sounds/SFX/Laevateinn")
        {
            PitchVariance = 0.1f,
        };
        public static readonly SoundStyle SFX_LamentClashLose = new($"{nameof(StarsAbove)}/Sounds/SFX/LimbusCoinLose")
        {

        };
        public static readonly SoundStyle SFX_LamentClashWin = new($"{nameof(StarsAbove)}/Sounds/SFX/LimbusCoinWin")
        {

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
        public static readonly SoundStyle SFX_prototokiaActive = new($"{nameof(StarsAbove)}/Sounds/SFX/prototokiaActive")
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
            Volume = 0.5f
        };
        public static readonly SoundStyle SFX_AshenExecute1 = new($"{nameof(StarsAbove)}/Sounds/SFX/AshenExecute1")
        {
            Volume = 0.5f

        };
        public static readonly SoundStyle SFX_AshenExecute2 = new($"{nameof(StarsAbove)}/Sounds/SFX/AshenExecute2")
        {
            Volume = 0.5f

        };
        public static readonly SoundStyle SFX_AshenExecute3 = new($"{nameof(StarsAbove)}/Sounds/SFX/AshenExecute3")
        {
            Volume = 0.5f

        };
        public static readonly SoundStyle SFX_AshenExecute4 = new($"{nameof(StarsAbove)}/Sounds/SFX/AshenExecute4")
        {
            Volume = 0.5f

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
        public static readonly SoundStyle SFX_iceCracking = new($"{nameof(StarsAbove)}/Sounds/SFX/iceCracking")
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
