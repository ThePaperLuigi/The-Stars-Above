
using Microsoft.Xna.Framework;
using SubworldLibrary;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace StarsAbove.Biomes
{
    public class SeaOfStarsBiome : ModBiome
	{

		// Use SetStaticDefaults to assign the display name
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Sea of Stars");

		}


		// Select all the scenery
		//public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("StarsAbove/ExampleWaterStyle"); // Sets a water style for when inside this biome
		public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.Find<ModSurfaceBackgroundStyle>("StarsAbove/SeaOfStarsBackgroundStyle");
		public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Normal;

		// Select Music
		public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/MareLamentorum");

		// Populate the Bestiary Filter
		public override string BestiaryIcon => "StarsAbove/Biomes/SeaOfStarsBestiaryIcon";
		public override string BackgroundPath => "StarsAbove/Biomes/SeaOfStarsBiomeMapBackground";
		public override Color? BackgroundColor => Color.White;

		
		// Calculate when the biome is active.
		public override bool IsBiomeActive(Player player) {
			if(SubworldSystem.IsActive<CygnusAsteroids>() 
				|| SubworldSystem.IsActive<MiningStationAries>() 
				|| SubworldSystem.IsActive<Serpens>() 
				|| SubworldSystem.IsActive<Scorpius>())
            {
				return true;
            }
			return false;
		}
	}
}
