
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarsAbove.Items.Consumables;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Generation;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;
using StarsAbove.SceneEffects.CustomSkies;
using Terraria.Graphics.Effects;
using StarsAbove.Buffs;

namespace StarsAbove.SceneEffects
{
	public class SuistrumeAudio : ModSceneEffect
	{

        public static SceneEffectPriority setPriority = SceneEffectPriority.BossLow;
        public override bool IsSceneEffectActive(Player player)
        {
            if (player.GetModPlayer<StarsAbovePlayer>().stellarPerformanceActive)
            {
                return true;
            }
            else
            {
                return false;
            }
            
            
            
        }

       



        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/NextColorPlanet");
        public override SceneEffectPriority Priority => setPriority;


    }
}
