using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using StarsAbove.Buffs.SubworldModifiers;
using StarsAbove.Buffs;

namespace StarsAbove.SceneEffects
{
    public class AstarteDriverEffect : ModSceneEffect
	{
        public override bool IsSceneEffectActive(Player player)
        {
			
            return true;
        }
        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (player.HasBuff<AstarteDriver>() || player.HasBuff<ChaosTurmoil>())
            {
                SkyManager.Instance.Activate("StarsAbove:EdinGenesisQuasarSky");
                
            }

            if (!player.HasBuff<AstarteDriver>() || player.HasBuff<ChaosTurmoil>())
            {
               SkyManager.Instance.Deactivate("StarsAbove:EdinGenesisQuasarSky");
            }
        }
        

		


    }
}
