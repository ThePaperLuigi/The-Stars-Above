using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using StarsAbove.Buffs.SubworldModifiers;
using SubworldLibrary;
using StarsAbove.Biomes;

namespace StarsAbove.SceneEffects.SubworldSceneEffects
{
    public class LyraSceneEffect : ModSceneEffect
    {
        public static SceneEffectPriority setPriority = SceneEffectPriority.BossLow;

        public override bool IsSceneEffectActive(Player player)
        {

            return false;

        }
        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (player.InModBiome<LyraBiome>())
            {
                Main.dayTime = false;
                Main.time = 7000;
                Main.cloudAlpha = 0f;

                Main.numClouds = 0;
                Main.numCloudsTemp = 0;
                Main.cloudBGAlpha = 0f;

                Main.moonPhase = 4;

                player.ZoneSnow = false;
                player.ZoneHallow = false;


            }
            else
            {
                //SkyManager.Instance.Deactivate("StarsAbove:CorvusSky");
            }

        }

        public override SceneEffectPriority Priority => setPriority;

    }
}
