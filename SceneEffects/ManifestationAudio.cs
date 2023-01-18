using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.TheOnlyThingIKnowForReal;
using StarsAbove.Buffs.Manifestation;

namespace StarsAbove.SceneEffects
{
    public class ManifestationAudio : ModSceneEffect
	{
        public static SceneEffectPriority setPriority = SceneEffectPriority.BossMedium;

        public override bool IsSceneEffectActive(Player player)
        {
            if (player.HasBuff(BuffType<EGOManifestedBuff>()))
            {
                return true;
            }
            else
            {
                return false;
            }
            
            
            
        }
        
        

		public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/RedMistTheme");
        public override SceneEffectPriority Priority => setPriority;


    }
}
