using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using StarsAbove.Buffs.Pets;
using StarsAbove.Systems;

namespace StarsAbove.SceneEffects
{
    public class SuistrumeAudioEasterEgg : ModSceneEffect
	{
        public static SceneEffectPriority setPriority = SceneEffectPriority.BossMedium;
        public override bool IsSceneEffectActive(Player player)
        {
            if (player.GetModPlayer<WeaponPlayer>().stellarPerformanceActive && player.HasBuff(BuffType<SuiseiPetBuff>()))
            {
                return true;
            }
            else
            {
                return false;
            }
            
            
            
        }
        
       

		public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/NextColorPlanetEasterEgg");
        public override SceneEffectPriority Priority => setPriority;


    }
}
