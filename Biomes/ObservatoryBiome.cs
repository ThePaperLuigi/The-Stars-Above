
using Microsoft.Xna.Framework;
using StarsAbove.Subworlds;
using SubworldLibrary;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ModLoader;

namespace StarsAbove.Biomes
{
    public class ObservatoryBiome : ModBiome
	{
		// Select all the scenery
		//public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("StarsAbove/ExampleWaterStyle"); // Sets a water style for when inside this biome
		public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.Find<ModSurfaceBackgroundStyle>("StarsAbove/ObservatoryBackgroundStyle");
		public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Normal;

		// Select Music
		public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/ElpisDay");

		// Populate the Bestiary Filter
		public override string BestiaryIcon => base.BestiaryIcon;
		public override string BackgroundPath => "StarsAbove/Biomes/SeaOfStarsBiomeMapBackground";
		public override Color? BackgroundColor => base.BackgroundColor;

		// Use SetStaticDefaults to assign the display name
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("The Observatory");
		}

		// Calculate when the biome is active.
		public override bool IsBiomeActive(Player player) {
			if(SubworldSystem.IsActive<Observatory>() || SubworldSystem.IsActive<EternalConfluence>())
			{
				player.GetModPlayer<StarsAbovePlayer>().Observatory = true;
				return true;//true
            }
			return false;
		}
	}
}
