using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using StarsAbove.Buffs.SubworldModifiers;
using SubworldLibrary;
using StarsAbove.Biomes;

namespace StarsAbove.SceneEffects
{
    public class CorvusSceneEffect : ModSceneEffect
    {
        public static SceneEffectPriority setPriority = SceneEffectPriority.BossLow;

        public override bool IsSceneEffectActive(Player player)
        {
            if (player.GetModPlayer<StarsAbovePlayer>().inCombat > 0 && player.InModBiome<CorvusBiome>())
            {
                return true;
            }
            return false;
           
        }
        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (player.InModBiome<CorvusBiome>())
            {
                Main.dayTime = false;
                Main.time = 18000;
                Main.cloudAlpha = 0f;

                Main.numClouds = 0;
                Main.numCloudsTemp = 0;
                Main.cloudBGAlpha = 0f;

                Main.moonPhase = 4;
                
                player.ZoneSnow = false;
                if (player.GetModPlayer<StarsAbovePlayer>().inCombat > 0)
                {
                    
                    player.ManageSpecialBiomeVisuals("Blizzard", Main.UseStormEffects);
                    //In combat, change the music and add the Blizzard effect.
                }
                SkyManager.Instance.Activate("StarsAbove:CorvusSky");
            }
            else
            {
                SkyManager.Instance.Deactivate("StarsAbove:CorvusSky");
            }
            if (isActive)
            {
               

            }
            else
            {
                
            }
        }



        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/GarlemaldExpress");
        public override SceneEffectPriority Priority => setPriority;

    }
}
