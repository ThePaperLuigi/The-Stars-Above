using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using StarsAbove.Buffs.SubworldModifiers;
using StarsAbove.Buffs;
using StarsAbove.Buffs.Magic.CloakOfAnArbiter;

namespace StarsAbove.SceneEffects
{
    public class CloakOfAnArbiterSceneEffect : ModSceneEffect
	{
        public static SceneEffectPriority setPriority = SceneEffectPriority.BossMedium;

        public override bool IsSceneEffectActive(Player player)
        {
            if (player.HasBuff(ModContent.BuffType<EmbodiedSingularity>()))
            {
                return true;
            }
            else
            {
                return false;
            }



        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Special/SingularityArbiterTheme");
        public override SceneEffectPriority Priority => setPriority;

        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (player.HasBuff<EmbodiedSingularity>())
            {
                SkyManager.Instance.Activate("StarsAbove:ArbiterSky");
                
            }

            if (!player.HasBuff<EmbodiedSingularity>())
            {
               SkyManager.Instance.Deactivate("StarsAbove:ArbiterSky");
            }
        }
        

		


    }
}
