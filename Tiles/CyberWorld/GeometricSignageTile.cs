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
    public class GeometricSignageTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileLighted[Type] = true;
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

		
		
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
            r = 183/255f;
            g = 250/255f;
            b = 249/255f;
        }
		
		
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(new EntitySource_TileBreak(i,j),i * 16, j * 16, 16, 32, ModContent.ItemType<VendingStation>());
		}
		
		
	}
}