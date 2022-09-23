using Terraria;
using Terraria.ModLoader;
using SubworldLibrary;
using StarsAbove.Subworlds;

namespace StarsAbove.SceneEffects
{
    public class ObservatoryBackground : ModSceneEffect
	{
        public override bool IsSceneEffectActive(Player player) //Disabled for now.
        {
            if (!SubworldSystem.IsActive<Observatory>())
            {
                //return false;
            }
            if (SubworldSystem.IsActive<Observatory>())
            {
                //return true;
            }
            
            return false;
        }
        
        
		//public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/EverlastingLight");
        public override SceneEffectPriority Priority => SceneEffectPriority.Event;


    }
}
