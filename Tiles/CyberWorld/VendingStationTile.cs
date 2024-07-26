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
    public class VendingStationTile : ModTile
	{
		public static int counter = 0;
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
			AdjTiles = new int[]{ TileID.Books };
		}

		
		
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			Tile tile = Main.tile[i, j];
			if (tile.TileFrameX < 66)
			{
				r = 0.9f;
				g = 0.9f;
				b = 0.9f;
			}
		}
		
		public override bool RightClick(int i, int j)
		{
			for (int k = 0; k < 255; k++)
			{
			Player player = Main.player[k];
			if (player.active)
				{
					if(Main.rand.NextBool(5))
					{
                        switch (Main.rand.Next(2))
                        {
                            case 0:
                                player.QuickSpawnItem(player.GetSource_FromThis(), Mod.Find<ModItem>("Onigiri").Type);
                                counter++;
                                break;
                            case 1:
                                player.QuickSpawnItem(player.GetSource_FromThis(), ItemID.JojaCola);
                                counter++;
                                break;

                        }
                    }
					
				}
			}
			return true;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
		}
		
		public override void MouseOver(int i, int j)
		{
			int whoAmI = 0;
			Player player = Main.player[whoAmI];
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = ModContent.ItemType<VendingStation>();
        }
	}
}