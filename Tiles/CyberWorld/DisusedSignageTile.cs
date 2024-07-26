using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using StarsAbove.Utilities;
using StarsAbove.Items.Placeable.CyberWorld;

namespace StarsAbove.Tiles.CyberWorld
{
    public class DisusedSignageTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileLighted[Type] = false;
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
			TileObjectData.newTile.CoordinateHeights = new[]{16, 16, 16, 18};
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(Type);

			AddMapEntry(new Color(200, 200, 111), CreateMapEntryName());

			TileID.Sets.DisableSmartCursor[Type] = true;
			AdjTiles = new int[]{ TileID.Torches };
		}

		
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
		}
		
		
	}
}