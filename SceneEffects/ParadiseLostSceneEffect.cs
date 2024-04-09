using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using StarsAbove.Buffs.SubworldModifiers;
using StarsAbove.Buffs;
using StarsAbove.Buffs.Magic.CloakOfAnArbiter;
using StarsAbove.Buffs.Magic.ParadiseLost;

namespace StarsAbove.SceneEffects
{
    public class ParadiseLostSceneEffect : ModSceneEffect
	{
        public static SceneEffectPriority setPriority = SceneEffectPriority.BossMedium;

        public override bool IsSceneEffectActive(Player player)
        {
            if (player.HasBuff(ModContent.BuffType<ParadiseLostBuff>()))
            {
                return true;
            }
            else
            {
                return false;
            }



        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Special/ThirdWarning");
        public override SceneEffectPriority Priority => setPriority;

        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (player.HasBuff<ParadiseLostBuff>())
            {
                SkyManager.Instance.Activate("StarsAbove:ParadiseLostSky");
                
            }

            if (!player.HasBuff<ParadiseLostBuff>())
            {
               SkyManager.Instance.Deactivate("StarsAbove:ParadiseLostSky");
            }
        }
        

		


    }
}
