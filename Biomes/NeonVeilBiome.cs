
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
    public class NeonVeilTileCount : ModSystem
    {
        public int tileCount;

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            tileCount = tileCounts[ModContent.TileType<DeepAsphalt>()];
        }
    }

    public class NeonVeilBiome : ModBiome
	{

		// Use SetStaticDefaults to assign the display name
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Sea of Stars");

		}


		// Select all the scenery
		//public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("StarsAbove/ExampleWaterStyle"); // Sets a water style for when inside this biome
		public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.Find<ModSurfaceBackgroundStyle>("StarsAbove/NeonVeilBackgroundStyle");
		public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Normal;

		// Select Music
		public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/MareLamentorum");

		// Populate the Bestiary Filter
		public override string BestiaryIcon => "StarsAbove/Biomes/SeaOfStarsBestiaryIcon";
		public override string BackgroundPath => "StarsAbove/Biomes/SeaOfStarsBiomeMapBackground";
		public override Color? BackgroundColor => Color.White;

		
		// Calculate when the biome is active.
		public override bool IsBiomeActive(Player player) {
            bool b1 = ModContent.GetInstance<NeonVeilTileCount>().tileCount >= 40;

            bool b2 = player.ZoneUnderworldHeight;
            return b1 && b2;
        }
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeLow;

    }
}
