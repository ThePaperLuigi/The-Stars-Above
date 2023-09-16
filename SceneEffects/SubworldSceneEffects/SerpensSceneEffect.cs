using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using StarsAbove.Buffs.SubworldModifiers;
using SubworldLibrary;
using StarsAbove.Subworlds.ThirdRegion;

namespace StarsAbove.SceneEffects.SubworldSceneEffects
{
    public class SerpensSceneEffect : ModSceneEffect
    {
        public override bool IsSceneEffectActive(Player player)
        {

            return true;
        }
        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (SubworldSystem.IsActive<Serpens>())
            {
                Main.dayTime = false;
                Main.time = 18000;
                Main.cloudAlpha = 0f;

                Main.numClouds = 0;
                Main.numCloudsTemp = 0;
                Main.cloudBGAlpha = 0f;

                Main.moonPhase = 4;
                player.ZoneCorrupt = false;
                player.ManageSpecialBiomeVisuals("Nebula", true);
            }

        }





    }
}
