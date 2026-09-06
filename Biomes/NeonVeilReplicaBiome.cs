
using Microsoft.Xna.Framework;
using StarsAbove.Subworlds;
using StarsAbove.Subworlds.ThirdRegion;
using StarsAbove.Tiles.CyberWorld;
using SubworldLibrary;
using System;
using Terraria;
using Terraria.GameContent.Personalities;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace StarsAbove.Biomes
{
    
    public class NeonVeilReplicaBiome : ModBiome
	{
		// Use SetStaticDefaults to assign the display name
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Sea of Stars");

		}
        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.Find<ModSurfaceBackgroundStyle>("StarsAbove/NeonVeilReplicaBackgroundStyle");


        // Select all the scenery
        //public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("StarsAbove/ExampleWaterStyle"); // Sets a water style for when inside this biome
        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Normal;

		// Select Music
		public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Biomes/NeonVeilTheme");

		// Populate the Bestiary Filter
		public override string BestiaryIcon => "StarsAbove/Biomes/NeonVeilBestiaryIcon";
		public override string BackgroundPath => "StarsAbove/Biomes/NeonVeilBiome";
		public override Color? BackgroundColor => Color.White;

        public override bool IsBiomeActive(Player player)
        {
            bool b1 = player.GetModPlayer<StarsAbovePlayer>().NeonVeilReplicaActive;

            return b1;
        }
        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

    }
}
