using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using StarsAbove.Buffs.SubworldModifiers;
using SubworldLibrary;
using StarsAbove.Subworlds.ThirdRegion;
using StarsAbove.Subworlds;

namespace StarsAbove.SceneEffects.SubworldSceneEffects
{
    public class SeaOfStarsSceneEffect : ModSceneEffect
    {
        public static SceneEffectPriority setPriority = SceneEffectPriority.BossLow;

        public override bool IsSceneEffectActive(Player player)
        {
            if (SubworldSystem.IsActive<CygnusAsteroids>()
                || SubworldSystem.IsActive<MiningStationAries>()
                || SubworldSystem.IsActive<BleachedPlanet>()
                || SubworldSystem.IsActive<Pyxis>()
                || SubworldSystem.IsActive<DreamingCity>()
                || SubworldSystem.IsActive<Celestia>()
                || SubworldSystem.IsActive<FallenTheranhad>()
                || SubworldSystem.IsActive<FaintArchives>()
                || SubworldSystem.IsActive<UltraPlant>()
                || SubworldSystem.IsActive<Katabasis>()
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (SubworldSystem.IsActive<CygnusAsteroids>()
                || SubworldSystem.IsActive<MiningStationAries>()
                || SubworldSystem.IsActive<BleachedPlanet>()
                || SubworldSystem.IsActive<Pyxis>()
                || SubworldSystem.IsActive<DreamingCity>()
                || SubworldSystem.IsActive<Celestia>()
                || SubworldSystem.IsActive<FallenTheranhad>()
                || SubworldSystem.IsActive<FaintArchives>()
                || SubworldSystem.IsActive<UltraPlant>()
                || SubworldSystem.IsActive<Katabasis>()
                )
            {
                Main.dayTime = false;
                Main.time = 18000;
                Main.cloudAlpha = 0f;

                Main.numClouds = 0;
                Main.numCloudsTemp = 0;
                Main.cloudBGAlpha = 0f;

                Main.moonPhase = 4;
            }

        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/MareLamentorum");
        public override SceneEffectPriority Priority => setPriority;



    }
}
