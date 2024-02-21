using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace StarsAbove.Tiles.CyberWorld
{
	// This file contains 3 classes and shows off using inheritance to share code between classes.
	// Terraria has many tiles that are purely decorative and do not drop items when broken.
	// These tiles go by many names, such as ambient tiles, background tiles, piles, detritus, and rubble. We will use the term rubble because of the recently added Rubblemaker item. 
	// The Rubblemaker (https://terraria.wiki.gg/wiki/Rubblemaker) is a special item that can place these decorative tiles. The tile placed by the Rubblemaker looks the same as the original rubble tile but behaves slightly differently.

	// Example1x1RubbleBase is an abstract class, it is not an actual tile, but the other 2 classes in this file will reuse the Texture and SetStaticDefaults code shown here because they inherit from it. 

	public abstract class TrashRubbleBase : ModTile
	{
		// We want both tiles to use the same texture
		public override string Texture => "StarsAbove/Tiles/CyberWorld/TrashRubble";

		public override void SetStaticDefaults() {
			Main.tileFrameImportant[Type] = true;
			Main.tileNoFail[Type] = true;
			Main.tileObsidianKill[Type] = true;

			DustType = DustID.Stone;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(Type);

			AddMapEntry(new Color(152, 171, 198));
		}
	}

	// This is the fake tile that will be placed by the Rubblemaker.
	public class TrashRubbleFake : TrashRubbleBase
    {
		public override void SetStaticDefaults() {
			// Call to base SetStaticDefaults. Must inherit static defaults from base type 
			base.SetStaticDefaults();

			// Add rubble variant, all existing styles, to Rubblemaker, allowing to place this tile by consuming ExampleBlock
			FlexibleTileWand.RubblePlacementSmall.AddVariations(ModContent.ItemType<Items.Placeable.CyberWorld.DeepAsphalt>(), Type, 0, 1, 2, 3, 4, 5);

			// Tiles placed by Rubblemaker drop the item used to place them.
			RegisterItemDrop(ModContent.ItemType<Items.Placeable.CyberWorld.DeepAsphalt>());
		}
	}

	// This is the natural tile, this version is placed during world generation in the RubbleWorldGen class.
	public class TrashRubbleNatural : TrashRubbleBase
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();

			// By default, the TileObjectData.Style1x1 tile we copied in Example1x1RubbleBase has LavaDeath = true. Natural rubble tiles don't have this behavior, so we want to be immune to lava.
			TileObjectData.GetTileData(Type, 0).LavaDeath = false;
		}

		public override void DropCritterChance(int i, int j, ref int wormChance, ref int grassHopperChance, ref int jungleGrubChance) {
			wormChance = 10;
		}
	}
}