
using Microsoft.Xna.Framework;
using SubworldLibrary;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace StarsAbove.Biomes
{
    public class BleachedWorldBiome : ModBiome
	{
		// Select all the scenery
		//public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("StarsAbove/ExampleWaterStyle"); // Sets a water style for when inside this biome
		public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.Find<ModSurfaceBackgroundStyle>("StarsAbove/BleachedWorldBackgroundStyle");
		public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Normal;

		// Select Music
		public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/MareLamentorum");

		// Populate the Bestiary Filter
		public override string BestiaryIcon => base.BestiaryIcon;
		public override string BackgroundPath => "StarsAbove/Biomes/SeaOfStarsBiomeMapBackground";
		public override Color? BackgroundColor => base.BackgroundColor;

		// Use SetStaticDefaults to assign the display name
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("The Bleached World");
		}

		// Calculate when the biome is active.
		public override bool IsBiomeActive(Player player) {
			if (SubworldSystem.IsActive<BleachedPlanet>())
            {
				return true;
            }
			return false;
		}
	}
}
