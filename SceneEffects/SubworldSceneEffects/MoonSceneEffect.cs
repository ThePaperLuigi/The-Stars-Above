using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using StarsAbove.Buffs.SubworldModifiers;

namespace StarsAbove.SceneEffects.SubworldSceneEffects
{
    public class MoonSceneEffect : ModSceneEffect
    {
        public override bool IsSceneEffectActive(Player player)
        {

            return true;
        }
        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (player.HasBuff<MoonTurmoil>() && !player.HasBuff<ChaosTurmoil>())
            {
                SkyManager.Instance.Activate("StarsAbove:MoonSky");
                Main.dayTime = false;
                Main.time = 18000;
                Main.cloudAlpha = 0f;
                Main.resetClouds = true;
                Main.moonPhase = 4;
            }

            if (!player.HasBuff<MoonTurmoil>() || player.HasBuff<ChaosTurmoil>())
            {
                SkyManager.Instance.Deactivate("StarsAbove:MoonSky");
            }
        }





    }
}
