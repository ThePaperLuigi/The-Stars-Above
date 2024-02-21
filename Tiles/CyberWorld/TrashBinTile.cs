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
    public class TrashBinTile : ModTile
	{
		public static int counter = 0;
		public override void SetStaticDefaults()
		{
			Main.tileLighted[Type] = false;
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(Type);

			AddMapEntry(new Color(200, 200, 111), CreateMapEntryName());

			TileID.Sets.DisableSmartCursor[Type] = true;
			AdjTiles = new int[]{ TileID.TrashCan };
		}

		
		public override bool RightClick(int i, int j)
		{
			for (int k = 0; k < 255; k++)
			{
			Player player = Main.player[k];
			if (player.active)
				{
					if(Main.rand.NextBool(35))
					{
                        player.QuickSpawnItem(player.GetSource_FromThis(), ItemID.Worm);

                    }

                }
			}
			return true;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(new EntitySource_TileBreak(i,j),i * 16, j * 16, 16, 32, ModContent.ItemType<TrashDefault>());
        }
		
		public override void MouseOver(int i, int j)
		{
			int whoAmI = 0;
			Player player = Main.player[whoAmI];
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = ModContent.ItemType<TrashDefault>();
		}
	}
}