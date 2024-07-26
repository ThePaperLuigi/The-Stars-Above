using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using SubworldLibrary;
using StarsAbove.Subworlds;
using StarsAbove.Systems;
using StarsAbove.NPCs.WarriorOfLight;

namespace StarsAbove.SceneEffects
{
    public class EverlastingLight : ModSceneEffect
	{
        public override bool IsSceneEffectActive(Player player)
        {
            if (DownedBossSystem.downedWarrior)
            {
                //SkyManager.Instance.Deactivate("StarsAbove:EverlastingLight");
                //return false;
            }
            if (EverlastingLightEvent.isEverlastingLightActive && (SubworldSystem.Current == null))
            {
                Main.time = 18000;
                Main.dayTime = true;
                SkyManager.Instance.Activate("StarsAbove:EverlastingLight");
                return true;
            }
            
            
            return false;
        }
        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (isActive)
            {
                

                
            }
            else
            {
                //SkyManager.Instance.Deactivate("StarsAbove:EverlastingLight");

            }


        }
        

		public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/EverlastingLight");
        public override SceneEffectPriority Priority => SceneEffectPriority.Event;


    }
}
