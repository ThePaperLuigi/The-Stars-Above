using Terraria;
using Terraria.ModLoader;

namespace StarsAbove.SceneEffects
{
    public class SuistrumeAudio : ModSceneEffect
	{

        public static SceneEffectPriority setPriority = SceneEffectPriority.BossLow;
        public override bool IsSceneEffectActive(Player player)
        {
            if (player.GetModPlayer<WeaponPlayer>().stellarPerformanceActive)
            {
                return true;
            }
            else
            {
                return false;
            }
            
            
            
        }

       



        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/NextColorPlanet");
        public override SceneEffectPriority Priority => setPriority;


    }
}
