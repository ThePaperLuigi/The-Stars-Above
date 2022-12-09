using Terraria;
using Terraria.ModLoader;
using SubworldLibrary;
using StarsAbove.Subworlds;
using Terraria.Graphics.Effects;

namespace StarsAbove.SceneEffects
{
    public class ObservatorySceneEffect : ModSceneEffect
	{
        public override bool IsSceneEffectActive(Player player) 
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
        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (SubworldSystem.IsActive<Observatory>())
            {
                Main.dayTime = true;
                Main.time = 12000;
                Main.cloudAlpha = 0f;
                
                Main.moonPhase = 4;
            }


        }

        //public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/EverlastingLight");
        public override SceneEffectPriority Priority => SceneEffectPriority.Event;


    }
}
