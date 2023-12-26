using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using StarsAbove.Buffs.SubworldModifiers;
using SubworldLibrary;
using StarsAbove.Biomes;
using StarsAbove.Systems;
using StarsAbove.Subworlds.ThirdRegion;

namespace StarsAbove.SceneEffects.SubworldSceneEffects
{
    public class DreamingCitySceneEffect : ModSceneEffect
    {
        public static SceneEffectPriority setPriority = SceneEffectPriority.BossLow;

        public override bool IsSceneEffectActive(Player player)
        {
            if (SubworldSystem.IsActive<DreamingCity>())
            {
                return true;
            }
            return false;

        }
        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (SubworldSystem.IsActive<DreamingCity>())
            {
                SkyManager.Instance.Activate("StarsAbove:DreamingCitySky");

                Main.dayTime = false;
                Main.time = 18000;
                Main.cloudAlpha = 0f;

                Main.numClouds = 0;
                Main.numCloudsTemp = 0;
                Main.cloudBGAlpha = 0f;

                Main.moonPhase = 4;

                
            }
            else
            {
                SkyManager.Instance.Deactivate("StarsAbove:DreamingCitySky");

            }

        }



        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/CosmicVoyage/DreamingCityAmbient");
        public override SceneEffectPriority Priority => setPriority;

    }
}
