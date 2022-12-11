using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using StarsAbove.Buffs.SubworldModifiers;
using SubworldLibrary;

namespace StarsAbove.SceneEffects
{
    public class TucanaSceneEffect : ModSceneEffect
	{
        public override bool IsSceneEffectActive(Player player)
        {
			
            return true;
        }
        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (SubworldSystem.IsActive<Tucana>())
            {
                
                //player.ManageSpecialBiomeVisuals("HeatDistortion", true);
            }

        }
        

		


    }
}
