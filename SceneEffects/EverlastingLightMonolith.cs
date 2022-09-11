
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
using SubworldLibrary;
using StarsAbove.Subworlds;

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
