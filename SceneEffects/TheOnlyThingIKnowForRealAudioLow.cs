using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.TheOnlyThingIKnowForReal;

namespace StarsAbove.SceneEffects
{
    public class TheOnlyThingIKnowForRealAudioLow : ModSceneEffect
	{
        public static SceneEffectPriority setPriority = SceneEffectPriority.BossHigh;
        public override bool IsSceneEffectActive(Player player)
        {
            if (player.HasBuff(BuffType<JetstreamBloodshed>()) && player.statLife <= 100)
            {
                return true;
            }
            else
            {
                return false;
            }
            
            
            
        }
        
        
		public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/TheOnlyThingIKnowForReal");
        public override SceneEffectPriority Priority => setPriority;


    }
}
