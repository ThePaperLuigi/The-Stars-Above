
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
using StarsAbove.Buffs.SubworldModifiers;
using Terraria.Graphics.Shaders;

namespace StarsAbove.SceneEffects
{
	public class MoonSceneEffect : ModSceneEffect
	{
        public override bool IsSceneEffectActive(Player player)
        {
			
            return true;
        }
        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (player.HasBuff<MoonTurmoil>())
            {
                SkyManager.Instance.Activate("StarsAbove:MoonSky");
                Main.dayTime = false;
                Main.time = 18000;
                Main.cloudAlpha = 0f;
                Main.resetClouds = true;
                Main.moonPhase = 4;
            }

            if (!player.HasBuff<MoonTurmoil>())
            {
                SkyManager.Instance.Deactivate("StarsAbove:MoonSky");
            }
        }
        

		


    }
}
