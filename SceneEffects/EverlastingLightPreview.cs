using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using SubworldLibrary;
using StarsAbove.Subworlds;

namespace StarsAbove.SceneEffects
{
    public class EverlastingLightPreview : ModSceneEffect
	{
        public override bool IsSceneEffectActive(Player player)
        {
            SkyManager.Instance.Deactivate("StarsAbove:EverlastingLightPreview");

           
            if (EverlastingLightEvent.isEverlastingLightPreviewActive && !EverlastingLightEvent.isEverlastingLightActive && (SubworldSystem.Current == null))
            {
                SkyManager.Instance.Activate("StarsAbove:EverlastingLightPreview");

                return true;
            }
            
            
            return false;
        }
        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (isActive && Main.dayTime)
            {

               // Main.time = 18000;
               // Main.dayTime = true;
            }
            else
            {
                //SkyManager.Instance.Deactivate("StarsAbove:EverlastingLight");

            }


        }
        

		//public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/EverlastingLight");
        public override SceneEffectPriority Priority => SceneEffectPriority.Event;


    }
}
