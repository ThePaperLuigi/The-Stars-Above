using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;

namespace StarsAbove.SceneEffects
{
    public class EverlastingLightMonolith : ModSceneEffect
	{
        public override bool IsSceneEffectActive(Player player)
        {
            if(player.GetModPlayer<StarsAbovePlayer>().lightMonolith)
            {
                return true;
            }
            else
            {
                

                return false;
            }
            
            return false;
        }
        public override void SpecialVisuals(Player player, bool isActive)
        {
            if(isActive)
            {
                SkyManager.Instance.Activate("StarsAbove:EverlastingLight");

            }
            else
            {
                SkyManager.Instance.Deactivate("StarsAbove:EverlastingLight");
            }
            

        }
        

		public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/EverlastingLight");
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeLow;


    }
}
