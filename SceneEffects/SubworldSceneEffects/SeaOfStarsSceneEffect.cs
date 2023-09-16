using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using StarsAbove.Buffs.SubworldModifiers;
using SubworldLibrary;
using StarsAbove.Subworlds.ThirdRegion;

namespace StarsAbove.SceneEffects.SubworldSceneEffects
{
    public class SeaOfStarsSceneEffect : ModSceneEffect
    {
        public override bool IsSceneEffectActive(Player player)
        {

            return true;
        }
        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (SubworldSystem.IsActive<CygnusAsteroids>()
                || SubworldSystem.IsActive<MiningStationAries>()
                || SubworldSystem.IsActive<BleachedPlanet>()
                || SubworldSystem.IsActive<Pyxis>()
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





    }
}
